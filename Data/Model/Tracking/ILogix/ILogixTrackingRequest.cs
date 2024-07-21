using Core;
using Data.Api.TrackingEvents.Model;

namespace Data.Model.Tracking.ILogix
{
    public class ILogixTrackingRequest : TmsTrackingRequest
    {
        public string JobNumber { get; set; }
        public DateTime JobAllocationDateTime { get; set; }
        public Core.LoginDetails LoginDetails { get; set; }
    }
}
