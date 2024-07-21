using Data.Model;
using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.Interfaces
{
    public interface IAppVehicleChecklistRepository
    {
        List<VehicleChecklist> GetAll(IEnumerable<int> vehicleGroupIds);
    }
}
