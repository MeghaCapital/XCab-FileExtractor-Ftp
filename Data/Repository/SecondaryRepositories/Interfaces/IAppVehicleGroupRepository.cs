using Data.Model;
using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.Interfaces
{
    public interface IAppVehicleGroupRepository
    {
        List<VehicleGroup> GetAllActive();
    }
}
