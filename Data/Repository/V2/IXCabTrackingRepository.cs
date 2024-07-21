using Data.Model;

namespace Data.Repository.V2
{
    public interface IXCabTrackingRepository
    {
        Task<ICollection<ToshibaTrackingDetail>> GetToshibaEventsToUpdate();
    }
}
