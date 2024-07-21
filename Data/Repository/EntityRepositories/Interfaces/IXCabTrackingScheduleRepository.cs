using Data.Entities;
using System.Collections.Generic;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabTrackingScheduleRepository
    {
        IEnumerable<XCabTracking> Get(bool enabled);

        void Update(XCabTracking tracking);
    }
}
