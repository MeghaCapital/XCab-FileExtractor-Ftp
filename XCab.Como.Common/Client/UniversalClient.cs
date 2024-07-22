using Core;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using System.Dynamic;
using xcab.como.common.Logging.TextFileLog;

namespace xcab.como.common.Client
{
	public class UniversalClient : IUniversalClient
	{
		private static readonly IComoTextFileGenerator textFileLog;
		private string apiToken;
		private string endpoint;

		static UniversalClient()
		{
			UniversalClient.textFileLog = new ComoTextFileGenerator("C:\\Logs\\SVC\\", "XCAB_COMO-" + DateTime.Now.ToString("yyyyMMdd"));
		}

		public UniversalClient()
		{

		}

		public void Initialise(string apiToken)
		{
			this.apiToken = apiToken;
		}

		public void UseEndpoint(string endpoint)
		{
			this.endpoint = endpoint;
		}

		public async Task<IDictionary<string, object>> GetAsync(string operation, string query)
		{
			var uri = new Uri(this.endpoint);
			using (GraphQLHttpClient client = new GraphQLHttpClient(uri, new NewtonsoftJsonSerializer()))
			{
				client.HttpClient.DefaultRequestHeaders.Add("api-token", this.apiToken);
				var request = new GraphQLRequest
				{
					Query = query,
					OperationName = operation
				};
				try
				{
					var response = Task.Run(async () => await client.SendQueryAsync<dynamic>(request)).Result;

					//var obj = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(response));
					//var objData = ((IDictionary<string, dynamic>)obj)["Data"];
					//return (IDictionary<string, object>)JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(objData));

					return JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(response.Data));
				}
				catch (Exception)
				{
					//Log
				}
			}
			return null;
		}

		public async Task<T> GetAsync<T>(string operation, string query)
		{
			var uri = new Uri(this.endpoint);
			using (GraphQLHttpClient client = new GraphQLHttpClient(uri, new NewtonsoftJsonSerializer()))
			{
				client.HttpClient.DefaultRequestHeaders.Add("api-token", this.apiToken);
				var request = new GraphQLRequest
				{
					Query = query,
					OperationName = operation
				};
				try
				{
					var response = Task.Run(async () => await client.SendQueryAsync<T>(request)).Result;
					return response.Data;
				}
				catch (Exception)
				{
					//Log
				}
			}
			return default(T);
		}

		public async Task<IDictionary<string, object>> SetAsync(string operation, string query, dynamic variable)
		{
			var uri = new Uri(this.endpoint);
			using (GraphQLHttpClient client = new GraphQLHttpClient(uri, new NewtonsoftJsonSerializer()))
			{
				client.HttpClient.DefaultRequestHeaders.Add("api-token", this.apiToken);
				//var jobVariable = System.Text.Json.JsonSerializer.Serialize(variable);
				//var jobContainer = new JobContainer() { job = job };
				//string jsobContainerJson = JsonConvert.SerializeObject(jobContainer);
				//string json = JsonConvert.SerializeObject(variable);
				var bookJobRequest = new GraphQLRequest
				{
					Query = query,
					OperationName = operation,
					Variables = variable
				};
				try
				{
					var response = Task.Run(async () => await client.SendMutationAsync<dynamic>(bookJobRequest)).Result;

					//var response = await client.SendMutationAsync<CreateBookJobResponse>(bookJobRequest);
					//var obj = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(response));
					//var objData = ((IDictionary<string, dynamic>)obj)["Data"];
					//return (IDictionary<string, object>)JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(objData));

					return JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(response.Data));
				}
				catch (Exception)
				{
					//Log
				}
			}
			return null;
		}

		public async Task<T> SendQueryAsync<T>(string operation, string query, dynamic variable)
		{
			var uri = new Uri(this.endpoint);
			using (GraphQLHttpClient client = new GraphQLHttpClient(uri, new NewtonsoftJsonSerializer()))
			{
				client.HttpClient.DefaultRequestHeaders.Add("api-token", this.apiToken);
				var request = new GraphQLRequest
				{
					Query = query,
					OperationName = operation,
					Variables = variable
				};
				try
				{
					var response = await client.SendQueryAsync<T>(request);
					return response.Data;
				}
				catch (Exception)
				{
					//Log
				}
			}
			return default(T);
		}

		public async Task<string> SendQueryAsync(string operation, string query, dynamic variable)
		{
			var uri = new Uri(this.endpoint);
			using (GraphQLHttpClient client = new GraphQLHttpClient(uri, new NewtonsoftJsonSerializer()))
			{
				client.HttpClient.DefaultRequestHeaders.Add("api-token", this.apiToken);
				var request = new GraphQLRequest
				{
					Query = query,
					OperationName = operation,
					Variables = variable
				};
				try
				{
					var response = await client.SendQueryAsync<dynamic>(request);
					return JsonConvert.SerializeObject(response.Data);
				}
				catch (Exception e)
				{
					await Logger.Log($"Exception occurred in SendQueryAsync method. Operation:{operation}, Query:{query}, Variable:{variable}. Message: {e.Message}", nameof(UniversalClient));
				}
			}
			return string.Empty;
		}
	}
}
