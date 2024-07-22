using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace xcab.como.common.Client
{
    public interface IUniversalClient
    {
        void Initialise(string apiToken);

        void UseEndpoint(string endpoint);

        Task<IDictionary<string, object>> GetAsync(string operation, string query);

        Task<T> GetAsync<T>(string operation, string query);

        Task<IDictionary<string, object>> SetAsync(string operation, string query, dynamic variable);

        Task<T> SendQueryAsync<T>(string operation, string query, dynamic variable);
		Task<string> SendQueryAsync(string operation, string query, dynamic variable);
	}
}
