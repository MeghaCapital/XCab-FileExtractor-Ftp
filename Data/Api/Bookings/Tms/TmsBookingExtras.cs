using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.Bookings.Tms
{
    public class TmsBookingExtras
    {
        public string PodType { get; set; }

        public string? TrackAndTraceEmailAddress { get; set; }

        public string? TrackAndTraceSmsNumber { get; set; }

        public string PodFax { get; set; }

        public string PodPhone { get; set; }

        public string? SpecialInstructions { get; set; }

        public string PickupSms { get; set; }

        public string? PickupInstructions { get; set; }

        public string? DeliveryInstructions { get; set; }

        public DateTime TimeSlotStartTime { get; set; }
        public int TimeSlotDuration { get; set; }
        public bool? Atl { get; set; }
    }
}
