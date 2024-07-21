using Data.Model;
using System;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabBookingNT12JobsRepository
    {
        IEnumerable<XCabBookingNT12Jobs> Get(string accountCode, int state, int loginId);

        int? Get(string jobNumber, int stateId, string accountCode, string fromSuburb, string fromPostcode, string toSuburb, string toPostcode, DateTime dateInserted);

        string Get(int stateId, string jobNumber, DateTime dateInserted);
    }
}
