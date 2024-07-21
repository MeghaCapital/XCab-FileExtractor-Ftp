using Data.Entities;
using Data.Model;

namespace Data.Repository.EntityRepositories.Interfaces
{
    public interface IXCabTrackingScheduleJobsRepository
    {
        void Insert(XCabTracking tracking, XCabBookingNT12Jobs booking);
    }
}
