using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xcab.como.common.Client;
using xcab.como.common.Logging.TextFileLog;

namespace xcab.como.common.Service
{
    public interface IComoDefaultProcessor
    {
        IUniversalClient Client { get; }

        IComoTextFileGenerator Log { get; }

        string ApiToken { get; }

        //Task<int> RetrieveEntityAsync(EEntities entity, string entityName, string property, string search, Dictionary<string, string> filters = null);
    }
}
