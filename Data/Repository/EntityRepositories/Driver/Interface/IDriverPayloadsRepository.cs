using Data.Model.Driver.DriverImages;
using System;

namespace Data.Repository.EntityRepositories.Driver.Interface
{
    public interface IDriverPayloadsRepository
    {
        bool ProcessPayload(PodModel pod);
        DriverPayload GetDriverPayload(string accountCode, string domicileState, string jobNumber,
           string subJobNumber, DateTime jobDateTime);
    }
}
