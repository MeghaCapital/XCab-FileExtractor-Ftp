using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using xcab.como.common.Logging;
using xcab.como.common.Logging.TextFileLog;

namespace xcab.como.common.Client
{
    public class IdentityClient : IIdentityClient, ISerialisable
    {
        //private static readonly GraphQLHttpClient graphQL;
        private string json;
        private string xml;
        private static readonly IComoTextFileGenerator textFileLog;
        private string apiToken;

        static IdentityClient()
        {
            IdentityClient.textFileLog = new ComoTextFileGenerator("C:\\Logs\\API\\", "XCAB_COMO-" + DateTime.Now.ToString("yyyyMMdd"));
        }

        private GraphQLHttpClient CreateHttpInstance()
        {
            var a = new GraphQLHttpClient(new Uri(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint), new NewtonsoftJsonSerializer());
            a.HttpClient.DefaultRequestHeaders.Add("api-token", this.apiToken);
            return a;

        }

        public string Json
        {
            get
            {
                return this.json;
            }
        }

        public string Xml
        {
            get
            {
                return this.xml;
            }
        }

        public void Initialise(string apiToken)
        {
            // IdentityClient.graphQL.HttpClient.DefaultRequestHeaders.Add("api-token", apiToken);
            this.apiToken = apiToken;
        }


        public async Task<IDictionary<string, object>> LoadInstance(string entityName, string filters)
        {
            var graphQL = CreateHttpInstance();
            try
            {
                var request = InitialiseRequest(entityName, filters);
                var response = Task.Run(async () => await graphQL.SendQueryAsync<object>(request)).Result;
                return JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(response.Data));
            }
            catch (Exception ex)
            { 
            
            }

            return null;
        }

        //To be deprecated
        public async Task<bool> LoadInstance(string entityName, string property, string name, Dictionary<string, int> nodeCount = null)
        {
            var graphQL = CreateHttpInstance();
            try
            {
                //Api token header could be getting added multiple times
                var request = InitialiseRequest(entityName, property, name);
                GraphQLResponse<object> response = Task.Run(async () => await graphQL.SendQueryAsync<object>(request)).Result;
                //response = graphQL.SendQueryAsync<object>(request).Result;
                Reset();
                List<string> parentSerialised = JsonConvert.SerializeObject(JObject.Parse(JsonConvert.SerializeObject(response.Data)).First.ToObject<JProperty>()).Split(new[] { ':' }, 2).ToList<string>();
                string childSerialised = parentSerialised[1];
                bool insideChild = false;
                bool continueWriting = true;
                string valueString = ",\"Value\":";
                string keyName = parentSerialised[0].Replace("\"", "").Trim();
                int count = 1;
                int innerObjectCount = 0;
                int currentIndex = 0;
                foreach (char c in childSerialised)
                {
                    if (char.Equals(c, '[') && (currentIndex == 0))
                    {
                        if ((nodeCount != null) && (nodeCount.ContainsKey(keyName)))
                        {
                            if (nodeCount[keyName] > 1)
                            {
                                count = nodeCount[keyName];
                                insideChild = true;
                            }
                        }
                    }
                    else if (char.Equals(c, '[') && (currentIndex > 0))
                    {
                        keyName = this.json.Substring(0, this.json.Length - valueString.Length);
                        keyName = keyName.Substring(keyName.LastIndexOf("\"Key\":"));
                        keyName = keyName.Split(new Char[] { '"', ':', ',' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                        if ((nodeCount != null) && (nodeCount.ContainsKey(keyName)))
                        {
                            if (nodeCount[keyName] > 1)
                            {
                                count = nodeCount[keyName];
                                insideChild = true;
                            }
                        }
                    }
                    if (char.Equals(c, '{') && insideChild && continueWriting)
                    {
                        this.json += "[";
                        continueWriting = false;
                    }
                    else if (char.Equals(c, '{') && insideChild)
                    {
                        innerObjectCount++;
                    }
                    if (char.Equals(c, ':'))
                    {
                        this.json += ",\"Value\"";
                    }
                    //else if (char.Equals(c, '}'))
                    //{
                    //    childAsKeyValuePair += "\"";
                    //}
                    if (char.Equals(c, ',') && !string.Equals(this.json.Substring(this.json.Length - 2), "}}", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (string.Equals(this.json.Substring(this.json.Length - 2), "}]", StringComparison.InvariantCultureIgnoreCase))
                        {
                            this.json += ",";
                            count = nodeCount[keyName];
                            insideChild = true;

                        }
                        else
                        {
                            this.json += "},{\"Key\":";
                        }
                    }
                    else
                    {
                        this.json += c;
                    }
                    if ((char.Equals(c, '}') || char.Equals(c, ']')) && insideChild && (innerObjectCount == 0))
                    {
                        this.json += "]";
                        count = 0;
                        insideChild = false;
                        continueWriting = true;
                    }
                    else if (char.Equals(c, '}') && insideChild && (innerObjectCount > 0))
                    {
                        innerObjectCount = (innerObjectCount + 1) % 2;
                    }
                    if (char.Equals(c, '{'))
                    {
                        this.json += "\"Key\":";
                    }
                    //else if (char.Equals(c, ':'))
                    //{
                    //    childAsKeyValuePair += "\"";
                    //}
                    currentIndex++;
                }
            }
            catch (Exception ex)
            {
                IdentityClient.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", entityName.ToUpperInvariant() + " Id: " + ex.Message, Constants.ErrorList.Error);
            }
            finally
            {
                graphQL.Dispose();
            }

            return !string.IsNullOrEmpty(this.json);
        }

        //To be deprecated
        private GraphQLRequest InitialiseRequest(string entityName, string property, string name)
        {
            string query = "query " + entityName + "($name: String!) { " + entityName + "(position:0, limit:0, filters: { generalSearch: $name }";

            if (string.Equals(entityName, "suburbs", StringComparison.InvariantCultureIgnoreCase))
            {
                query += ", orderBy: {name:asc}";
            }

            query += ") { " + property + " } }";

            return new GraphQLRequest
            {
                Query = query,
                OperationName = entityName,
                Variables = new
                {
                    name = name
                }
            };
        }

        private GraphQLRequest InitialiseRequest(string entityName, string filters)
        {
            string query = "{ " + entityName + "(position: 0, limit: 0, filters: " + filters + ") { id } }";

            return new GraphQLRequest
            {
                Query = query
                //OperationName = entityName,
                //Variables = new
                //{
                //    name = name
                //}
            };
        }

        private void Reset()
        {
            this.json = string.Empty;
            this.xml = string.Empty;
        }
    }
}
