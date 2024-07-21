using Core;
using Data.Api.TrackingEvents.Model;

namespace Data.Model.Tracking
{
    public class XcabTmsTrackingRequest : TmsTrackingRequest
    {
        public Core.LoginDetails LoginDetails { get; set; }
    }
}
