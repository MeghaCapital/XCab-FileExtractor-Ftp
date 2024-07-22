using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using xcab.como.common.Client;
using xcab.como.common.Logging;
using xcab.como.common.Logging.TextFileLog;

namespace xcab.como.common.Struct
{
    public class IdentityList
    {
        private readonly KeyValueList<IdentityClient> kvl;
        private readonly NestedKeyValueList<IdentityClient> nkvl;
        private static readonly IComoTextFileGenerator textFileLog;

        static IdentityList()
        {
            IdentityList.textFileLog = new ComoTextFileGenerator("C:\\Logs\\Struct\\", "XCAB_COMO-" + DateTime.Now.ToString("yyyyMMdd"));
        }

        public IdentityList(IIdentityClient ic)
        {
            IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Start deserialising graphql json response.", Constants.ErrorList.Information);

            kvl = new KeyValueList<IdentityClient>((IdentityClient)ic);

            if (kvl.Count == 0)
            {
                nkvl = new NestedKeyValueList<IdentityClient>((IdentityClient)ic);
                kvl = null;
            }

            IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Complete deserialising graphql json response.", Constants.ErrorList.Information);
        }

        public int? Id<E>(E entityEnum, Dictionary<string, string> filters = null)
        {
            IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Start retrieving id.", Constants.ErrorList.Information);

            int entity = Convert.ToInt32(entityEnum);
            if (entity == (int)EEntities.Accounts)
            {
                IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Entered logic for client code.", Constants.ErrorList.Information);

                List<object> idCandidates = new List<object>();
                for (int i = 0; i < this.kvl.Count; i++)
                {
                    KeyValuePair<string, object> kvp = JsonConvert.DeserializeObject<KeyValuePair<string, object>>(JsonConvert.SerializeObject(this.kvl[i].Value));
                    string kvpVal = JsonConvert.SerializeObject(kvp.Value);
                    List<KeyValuePair<string, object>> lkvp = JsonConvert.DeserializeObject<List<KeyValuePair<string, object>>>(kvpVal.Substring(1, kvpVal.Length - 2));
                    var obj = new ExpandoObject() as IDictionary<string, object>;
                    for (int j = 0; j < lkvp.Count; j++)
                    {
                        kvp = JsonConvert.DeserializeObject<KeyValuePair<string, object>>(JsonConvert.SerializeObject(lkvp[j]));
                        object innerKvpVal = kvp.Value;
                        int currentId;
                        if (int.TryParse(innerKvpVal.ToString(), out currentId))
                        {
                            obj.Add(kvp.Key, currentId);
                        }
                        else
                        {
                            KeyValuePair<string, object> innerMostKvp = JsonConvert.DeserializeObject<KeyValuePair<string, object>>(JsonConvert.SerializeObject(innerKvpVal));
                            obj.Add(innerMostKvp.Key, innerMostKvp.Value);
                        }
                    }
                    idCandidates.Add(obj);
                }

                IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Retrieve unflitered anonymous object list. Json: " + JsonConvert.SerializeObject(idCandidates), Constants.ErrorList.Information);

                foreach (KeyValuePair<string, string> filter in filters)
                {
                    idCandidates.RemoveAll(o => !((IDictionary<string, object>)o).ContainsKey(filter.Key));
                    idCandidates.RemoveAll(o => !string.Equals(((IDictionary<string, object>)o)[filter.Key].ToString().Split(' ').Last().Trim(new Char[] { '(', ')' }), filter.Value, StringComparison.InvariantCultureIgnoreCase));
                }

                IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Retrieve filtered anonymous object list. Json: " + JsonConvert.SerializeObject(idCandidates), Constants.ErrorList.Information);

                return int.Parse(((IDictionary<string, object>)idCandidates.FirstOrDefault())["id"].ToString());
            }
            else if ((entity == (int)EEntities.JobBookingPhases) || (entity == (int)EEntities.Services) || (entity == (int)EEntities.SubJobCreationMethods))
            {
                IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Entered logic for one of job booking phases, services or sub job creation method.", Constants.ErrorList.Information);

                KeyValuePair<string, object> kvp = JsonConvert.DeserializeObject<KeyValuePair<string, object>>(JsonConvert.SerializeObject(this.kvl[0]));
                return int.Parse(kvp.Value.ToString());
            }
            else if (entity == (int)EEntities.Suburbs)
            {
                IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Entered logic for suburbs.", Constants.ErrorList.Information);

                List<object> idCandidates = new List<object>();
                for (int i = 0; i < this.nkvl.Count; i++)
                {
                    List<KeyValuePair<string, object>> lkvp = JsonConvert.DeserializeObject<List<KeyValuePair<string, object>>>(JsonConvert.SerializeObject(this.nkvl[i]));
                    var obj = new ExpandoObject() as IDictionary<string, object>;
                    for (int j = 0; j < lkvp.Count; j++)
                    {
                        KeyValuePair<string, object> kvp = JsonConvert.DeserializeObject<KeyValuePair<string, object>>(JsonConvert.SerializeObject(lkvp[j]));
                        object kvpVal = kvp.Value;
                        int currentId;
                        if (int.TryParse(kvpVal.ToString(), out currentId))
                        {
                            obj.Add(kvp.Key, currentId);
                        }
                        else
                        {
                            KeyValuePair<string, object> innerKvp = JsonConvert.DeserializeObject<KeyValuePair<string, object>>(JsonConvert.SerializeObject(kvpVal));
                            obj.Add(innerKvp.Key, innerKvp.Value);
                        }
                    }
                    idCandidates.Add(obj);
                }

                IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Retrieve unflitered anonymous object list. Json: " + JsonConvert.SerializeObject(idCandidates), Constants.ErrorList.Information);

                foreach (KeyValuePair<string, string> filter in filters)
                {
                    idCandidates.RemoveAll(o => !((IDictionary<string, object>)o).ContainsKey(filter.Key));
                    idCandidates.RemoveAll(o => !string.Equals(((IDictionary<string, object>)o)[filter.Key].ToString(), filter.Value, StringComparison.InvariantCultureIgnoreCase));
                }

                IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Retrieve flitered anonymous object list. Json: " + JsonConvert.SerializeObject(idCandidates), Constants.ErrorList.Information);

                return int.Parse(((IDictionary<string, object>)idCandidates.FirstOrDefault())["id"].ToString());
            }

            IdentityList.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Complete retrieving id.", Constants.ErrorList.Information);

            return null;
        }
    }
}
