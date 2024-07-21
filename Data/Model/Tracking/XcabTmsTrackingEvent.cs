using Core;
using Data.Api.TrackingEvents;

namespace Data.Model.Tracking
{
    public class XcabTmsTrackingEvent : TmsTrackingEvents
    {
        public Core.LoginDetails LoginDetails { get; set; }
    }
}
