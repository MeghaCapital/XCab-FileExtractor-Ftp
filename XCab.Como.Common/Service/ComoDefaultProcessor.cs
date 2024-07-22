using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xcab.como.common.Client;
using xcab.como.common.Data.Response;
using xcab.como.common.Logging;
using xcab.como.common.Logging.TextFileLog;

namespace xcab.como.common.Service
{
    public class ComoDefaultProcessor : IComoDefaultProcessor
    {
        private static readonly IIdentityClient identityClient;
        //private static readonly IIdentityCollectionManager identityCollectionManager;
        private static readonly IApiTokenClient apiTokenClient;
        private static readonly IInternalUserLoginClient internalUserLoginClient;
        private static readonly IUniversalClient client;
        private static readonly IComoTextFileGenerator textFileLog;
        private readonly string apiToken;

        public IUniversalClient Client
        {
            get
            {
                return ComoDefaultProcessor.client;
            }
        }

        public string ApiToken
        {
            get
            {
                return this.apiToken;
            }
        }

        public IComoTextFileGenerator Log
        {
            get
            {
                return ComoDefaultProcessor.textFileLog;
            }
        }

        static ComoDefaultProcessor()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ComoDefaultProcessor.identityClient = new IdentityClient();
            //ComoDefaultProcessor.identityCollectionManager = new IdentityCollectionManager();
            ComoDefaultProcessor.apiTokenClient = new ApiTokenClient();
            ComoDefaultProcessor.internalUserLoginClient = new InternalUserLoginClient();
            ComoDefaultProcessor.client = new UniversalClient();
            ComoDefaultProcessor.textFileLog = new ComoTextFileGenerator("C:\\Logs\\SVC\\", "XCAB_COMO-" + DateTime.Now.ToString("yyyyMMdd"));
        }

        public ComoDefaultProcessor()
        {
            AccessTokenResponse accessTokenResponse = null;
            try
            {
                accessTokenResponse = Task.Run(async () => await ComoDefaultProcessor.internalUserLoginClient.GetAccessTokenAsync()).Result;
            }
            catch (Exception ex)
            {
                ComoDefaultProcessor.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Access token: " + ex.Message, Constants.ErrorList.Error);
            }
            string accessToken = accessTokenResponse.AccessToken;
            if (!string.IsNullOrEmpty(accessToken))
            {
                ApiTokenResponse apiTokenresponse = null;
                try
                {
                    apiTokenresponse = Task.Run(async () => await ComoDefaultProcessor.apiTokenClient.CreateApiTokenAsync(accessToken)).Result;
                }
                catch (Exception ex)
                {
                    ComoDefaultProcessor.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Api token: " + ex.Message, Constants.ErrorList.Error);
                }
                string apiToken = apiTokenresponse.Payload.ApiToken;
                if (!string.IsNullOrEmpty(apiToken))
                {
                    this.apiToken = apiToken;
                    ComoDefaultProcessor.identityClient.Initialise(this.apiToken);
                }
            }
        }

        protected async Task<int> RetrieveEntityAsync(EEntities entity, string filters)
        {
			try
			{
                string entityFirstLower = entity.ToString().FirstToLower();
                var response = Task.Run(async () => await ComoDefaultProcessor.identityClient.LoadInstance(entityFirstLower, filters)).Result;
                var respList = response[entityFirstLower];
                if (respList != null)
                {                    
                    var respKeyValuePairContainer = JArray.Parse(JsonConvert.SerializeObject(respList)).First;
                    var respKeyValuePair = respKeyValuePairContainer.First;
                    return (int)respKeyValuePair.Last;
                }
            }
			catch (Exception e)
			{
                await Logger.Log($"Exception occurred in RetrieveEntityAsync when identifying the ID for the {entity} entity, message: " + e.Message, nameof(ComoDefaultProcessor));
            }
            return 0;
        }
    }
}
