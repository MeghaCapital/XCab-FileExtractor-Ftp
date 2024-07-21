using Data.Entities.Ilogix;
using System;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IIlogixJobsRepository
    {
        Task<ICollection<IlogixJobLeg>> GetIlogixJobLegs(string jobnumber, string statePrefix, DateTime jobDate);

        ICollection<IlogixJobLeg> GetILogixJobNumber(string shortJobnumber, DateTime jobDate, string clientCode, string customerCode, string stateCode);

        ICollection<IlogixJobLegLookups> SearchILogixJobNumber(DateTime jobDate, string clientCode, string ref1, string customerCode = "");

        string GetILogixClientCode(string clientcode);
    }
}
