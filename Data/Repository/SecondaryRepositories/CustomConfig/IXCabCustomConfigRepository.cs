using Data.Repository.EntityRepositories.CustomConfig;
using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.CustomConfig
{
    interface IXCabCustomConfigRepository
    {
        ICollection<XCabCustomConfig> GetCustomConfig(int ftpLoginId, int stateId, string accountCode);
    }
}
