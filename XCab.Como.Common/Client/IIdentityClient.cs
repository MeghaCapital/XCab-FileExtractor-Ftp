using System.Collections.Generic;
using System.Threading.Tasks;

namespace xcab.como.common.Client
{
    public interface IIdentityClient
    {
        void Initialise(string apiToken);

        Task<IDictionary<string, object>> LoadInstance(string entityName, string filters);

        //Task<bool> LoadInstance(string entityName, string property, string name, Dictionary<string, int> nodeCount = null);
    }
}
