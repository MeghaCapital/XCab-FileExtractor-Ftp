using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using xcab.como.common.Data.Response;

namespace xcab.como.common.Client
{
    public class ApiTokenClient : IApiTokenClient
    {
        public async Task<ApiTokenResponse> CreateApiTokenAsync(string accessToken)
        {
            var cookieContainer = new CookieContainer();
            using (var client = new HttpClient(new HttpClientHandler() { CookieContainer = cookieContainer }))
            {
                var uri = new Uri(ComoApiConstants.BaseComoUrl + ComoApiConstants.ApiTokenEndpoint);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                StringContent httpContent = new StringContent(ComoApiConstants.ApiUsername, Encoding.UTF8, "application/json");
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
                {
                    Version = HttpVersion.Version10,
                    Content = httpContent
                };
                cookieContainer.Add(uri, new Cookie("session_id", ComoApiConstants.ApiSessionId));
                cookieContainer.Add(uri, new Cookie("refresh_token", ComoApiConstants.ApiRefreshToken));
                try
                {
                    using (HttpResponseMessage httpResponse = Task.Run(async () => await client.SendAsync(httpRequestMessage)).Result)
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            var response = await httpResponse.Content.ReadAsStringAsync();
                            var apiTokenresponse = JsonConvert.DeserializeObject<ApiTokenResponse>(response);
                            return apiTokenresponse;
                            //return JsonConvert.DeserializeObject<ApiTokenResponse>(Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()).Result);
                        }
                    }
                }
                catch (Exception e)
                {
                    //Log
                }
            }
            return null;
        }
    }
}
