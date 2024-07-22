using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace xcab.como.tracker.Client
{
    public interface IImageClient
    {
        void Initialise(string apiToken);

        void UseEndpoint(string endpoint);

        Task<byte[]> GetZipAsync(IEnumerable<int> documentIds);

        Task<byte[]> GetImageAsync(string imageSecret);
    }
}
