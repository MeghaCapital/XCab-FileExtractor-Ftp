using Data.Model.Tracking.ILogix;

namespace Data.Api.TrackingEvents.Repository.ILogix
{
    public interface IILogixTrackingStatusRepository
    {
        Task<ICollection<TmsTrackingEvents>> GetTmsTrackingEvents(ICollection<ILogixTrackingRequest> trackingRequests);
    }
}
