using Data.Entities.Services;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    interface IXCabClientServiceMappingRepository
    {
        ICollection<XCabClientServiceMapping> GetClientServiceMappings(int LoginId);
    }
}
