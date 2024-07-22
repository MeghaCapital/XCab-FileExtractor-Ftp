using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using xcab.como.common;

namespace xcab.como.tracker.Client
{
    public class ImageClient : IImageClient
    {
        private string apiToken;
        private string endpoint;

        static ImageClient()
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        }

        public ImageClient()
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

        public async Task<byte[]> GetZipAsync(IEnumerable<int> documentIds)
        {
            byte[] zippedImages = null;

            try
            {
                Uri uri = new Uri(this.endpoint);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri)
                {

                };

                httpRequestMessage.Headers.Host = uri.Host;

                using (HttpClient fileClient = new HttpClient())
                {
                    fileClient.DefaultRequestHeaders.Add("api-token", this.apiToken);
                    
                    using (HttpResponseMessage httpResponse = Task.Run(async () => await fileClient.SendAsync(httpRequestMessage)).Result)
                    {
                        httpResponse.EnsureSuccessStatusCode();
                        zippedImages = Task.Run(async () => await httpResponse.Content.ReadAsByteArrayAsync()).Result;
                    }
                }
            }
            catch (Exception e)
            {
                //Logger.Log.Warning(e, "Network exception");
            }

            return zippedImages;
        }

        //Need method to convert byte[] to zip file
        //Pass zip file as parameter
        private async Task<IEnumerable<byte[]>> GetImagesFromZip(object zip)
        {
            return null;
        }

        public async Task<byte[]> GetImageAsync(string imageSecret)
        {
            byte[] singleImage = null;

            try
            {
                Uri uri = new Uri(this.endpoint);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri)
                {

                };

                httpRequestMessage.Headers.Host = uri.Host;

                using (HttpClient fileClient = new HttpClient())
                {
                    using (HttpResponseMessage httpResponse = Task.Run(async () => await fileClient.SendAsync(httpRequestMessage)).Result)
                    {
                        httpResponse.EnsureSuccessStatusCode();
                        singleImage = Task.Run(async () => await httpResponse.Content.ReadAsByteArrayAsync()).Result;
                    }
                }
            }
            catch (Exception e)
            {
                //Logger.Log.Warning(e, "Network exception");
            }

            return singleImage;
        }
    }
}
