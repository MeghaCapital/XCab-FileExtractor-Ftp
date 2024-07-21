using Data.Entities.Booking.TimeSlots;

namespace Data.Repository.EntityRepositories.Job.TimeSlots.Interface
{
    public interface IXCabTimeSlotsRepository
    {
        bool Insert(XCabTimeSlots xCabTimeSlots);

        Task<XCabTimeSlots> GetTimeSlot(int bookingId);
    }
}
