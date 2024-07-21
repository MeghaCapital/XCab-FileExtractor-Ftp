using Data.Model;
using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.Interfaces
{
    public interface IAppVehicleChecklistResponseRepository
    {
        List<ChecklistImage> Get(string jobNumber, string subJobNumber, int stateId);
    }
}
