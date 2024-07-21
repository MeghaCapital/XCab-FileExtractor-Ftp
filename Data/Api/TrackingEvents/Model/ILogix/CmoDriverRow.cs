using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents.Model.ILogix
{
    public class CmoDriverRow
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime GpsDateTime { get; set; }
        public int DriverNumber { get; set; }

    }
}
