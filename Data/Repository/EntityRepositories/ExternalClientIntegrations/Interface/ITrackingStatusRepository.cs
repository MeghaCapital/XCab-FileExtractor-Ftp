namespace Data.Repository.EntityRepositories.ExternalClientIntegrations.Interface
{
    public interface ITrackingStatusRepository
    {
        Task UpdateXCabTableWithTrackingUpdate(Model.Tracking.ETrackingEvent eTrackingEvent, string location, string eventDateTime, int bookingId);
    }
}
