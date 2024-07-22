using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.tracker.Data.Models
{
    public class TrackingEvent
    {
        public ETrackingEvent Name { get; set; }

        public DateTime? Time { get; set; }

        public int MobileId { get; set; }

        public bool IsAllocated { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
