using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xcab.como.common;
using xcab.como.common.Client;
using xcab.como.common.Logging.TextFileLog;
using xcab.como.tracker.Data.Response;

namespace xcab.como.tracker.Client
{
	public class RecentlyCompletedJobsClient : IRecentlyCompletedJobsClient
	{
		#region Obsolete methods
		//public RecentlyCompletedJobsResponse GetRecentlyCompletedJobsRest(IRestClient restClient, IRestRequest restRequest, string apiToken, string query, List<int> ids)
		//{
		//    restClient.AddDefaultHeader("api-token", apiToken);
		//    var fullQuery = new GraphQLQuery()
		//    {
		//        query = query,
		//        variables = new
		//        {
		//            id = ids
		//        },
		//    };
		//    string jsonContent = JsonConvert.SerializeObject(fullQuery).Trim();
		//    restRequest.AddHeader("Content-Type", "application/json");
		//    restRequest.AddHeader("Content-Length", jsonContent.Length.ToString());
		//    restRequest.AddJsonBody(jsonContent);
		//    try
		//    {
		//        return new GraphQLResult(restClient.Execute(restRequest).Content).GetData<RecentlyCompletedJobsResponse>();
		//    }
		//    catch (Exception ex)
		//    {
		//        //Log
		//    }
		//    return null;
		//}

		//public async Task<RecentlyCompletedJobsResponse> GetRecentlyCompletedJobsAsync(IRestClient restClient, IRestRequest restRequest, string apiToken, string query, List<int> ids)
		//{
		//    restClient.AddDefaultHeader("api-token", apiToken);
		//    var fullQuery = new GraphQLQuery()
		//    {
		//        query = query,
		//        variables = new
		//        {
		//            id = ids
		//        },
		//    };
		//    string jsonContent = JsonConvert.SerializeObject(fullQuery).Trim();
		//    restRequest.AddHeader("Content-Type", "application/json");
		//    restRequest.AddHeader("Content-Length", jsonContent.Length.ToString());
		//    restRequest.AddJsonBody(jsonContent);
		//    try
		//    {
		//        var restResponse = await restClient.ExecuteAsync(restRequest);
		//        return new GraphQLResult(restResponse.Content).GetData<RecentlyCompletedJobsResponse>();
		//    }
		//    catch (Exception ex)
		//    {
		//        //Log
		//    }
		//    return null;
		//}

		//public RecentlyCompletedJobsResponse GetRecentlyCompletedJobsHttp(string apiToken, string query, List<int> ids)
		//{
		//    var uri = new Uri(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);
		//    var fullQuery = new GraphQLQuery()
		//    {
		//        query = query,
		//        variables = new
		//        {
		//            id = ids
		//        },
		//    };
		//    string jsonContent = JsonConvert.SerializeObject(fullQuery).Trim();
		//    UTF8Encoding encoding = new UTF8Encoding();
		//    byte[] byteArray = encoding.GetBytes(jsonContent);
		//    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
		//    request.Method = "POST";
		//    request.ContentLength = byteArray.Length;
		//    request.ContentType = "application/json";
		//    request.Headers.Add("api-token", apiToken);
		//    using (Stream dataStream = request.GetRequestStream())
		//    {
		//        dataStream.Write(byteArray, 0, byteArray.Length);
		//    }
		//    long length;
		//    try
		//    {
		//        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
		//        {
		//            length = response.ContentLength;
		//            using (Stream responseStream = response.GetResponseStream())
		//            {
		//                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
		//                var json = reader.ReadToEnd();
		//                return new GraphQLResult(json).GetData<RecentlyCompletedJobsResponse>();
		//            }
		//        }
		//    }
		//    catch (WebException ex)
		//    {
		//        //Log
		//    }
		//    return null;
		//}
		#endregion

		private static readonly IComoTextFileGenerator textFileLog;

		static RecentlyCompletedJobsClient()
		{
			RecentlyCompletedJobsClient.textFileLog = new ComoTextFileGenerator(common.Logging.Constants.ProjectType.Service);
		}

		public async Task<RecentlyCompletedJobs> GetRecentlyCompletedJobsHttpAsync(string apiToken, string query)
		{
			var uri = new Uri(ComoApiConstants.BaseComoUrl + ComoApiConstants.BookJobEndpoint);
			using (GraphQLHttpClient client = new GraphQLHttpClient(uri, new NewtonsoftJsonSerializer()))
			{
				client.HttpClient.DefaultRequestHeaders.Add("api-token", apiToken);
				var request = new GraphQLRequest
				{
					Query = query,
					OperationName = "RecentlyCompletedJobs",
					Variables = new
					{
					}
				};
				try
				{
					var response = client.SendQueryAsync<RecentlyCompletedJobs>(request).Result;
					return response.Data;
				}
				catch (Exception e)
				{
					RecentlyCompletedJobsClient.textFileLog.Write(GetType().Name + " - svc/xcab-como - ", "Error occurred when retrieving recently completed jobs: " + e.Message, common.Logging.Constants.ErrorList.Error);
				}
			}
			return null;
		}
	}
}
