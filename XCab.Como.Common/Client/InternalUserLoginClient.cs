using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using xcab.como.common.Data.Request;
using xcab.como.common.Data.Response;

namespace xcab.como.common.Client
{
    public class InternalUserLoginClient : IInternalUserLoginClient
    {
        public async Task<AccessTokenResponse> GetAccessTokenAsync()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(ComoApiConstants.BaseComoUrl + ComoApiConstants.InternalUserLoginEndpoint);

                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(new AccessTokenRequest
                {
                    Username = ComoApiConstants.Username,
                    Password = ComoApiConstants.Password
                }), Encoding.UTF8, "application/json");
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
                {
                    Version = HttpVersion.Version10,
                    Content = httpContent
                };
                try
                {
                    using (HttpResponseMessage httpResponse = Task.Run(async () => await client.SendAsync(httpRequestMessage)).Result)
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            var response = await httpResponse.Content.ReadAsStringAsync();
                            var accessTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(response);
                            return accessTokenResponse;
                           // return JsonConvert.DeserializeObject<AccessTokenResponse>(Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()).Result);
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
