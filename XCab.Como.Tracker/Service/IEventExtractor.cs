using Core;
using Data.Api.TrackingEvents;
using Data.Api.TrackingEvents.Model;

namespace xcab.como.tracker.Service
{
    public interface IEventExtractor
    {
        public TmsTrackingEvents GetAllocationAndCancelEventsForComoJobs(TmsTrackingRequest trackingRequest);

        public TmsTrackingEvents GetTrackingEventsForComoJobs(TmsTrackingRequest trackingRequest);
    }
}
