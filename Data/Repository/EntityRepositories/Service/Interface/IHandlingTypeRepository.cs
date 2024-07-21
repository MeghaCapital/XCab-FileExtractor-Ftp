using Data.Model.ServiceCodes;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Service.Interface
{
    public interface IHandlingTypeRepository
    {
        ICollection<ManualLiftRestrictions> GetManualLiftRestrictions();
    }
}
