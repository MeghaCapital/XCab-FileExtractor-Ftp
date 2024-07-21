using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.CustomConfig
{
    public interface IXCabCustomConfigRepository
    {
        Task<ICollection<XCabCustomConfig>> GetCustomConfig(int ftpLoginId, int stateId, string accountCode);
    }
}
