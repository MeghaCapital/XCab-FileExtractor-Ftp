using Data.Entities.Ilogix;
using Data.Model;
using Data.Model.Poc;
using System;
using System.Collections.Generic;

namespace Data.Repository.SecondaryRepositories.Interfaces
{
    public interface IAppVehicleChecklistImageRepository
    {
        TplusWebApi GetImage(int imageId);
        ICollection<Entities.Ilogix.PocImage> GetCheckListImage(int stateId, string jobnumber, DateTime responseDate, checkListSubJobFilter subJobFilter = checkListSubJobFilter.All);
    }
}
