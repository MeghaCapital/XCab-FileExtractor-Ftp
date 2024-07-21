using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Tracking.ILogix
{
    public class ILogixTrackingEvents
    {
        public double DateTimePickup { get; set; }
        public double DateTimeDelivered { get; set; }
        public string Comments { get; set; }
        public string JobNumber { get; set; }
        public string SubJobNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int statusId { get; set; }

    }
}
