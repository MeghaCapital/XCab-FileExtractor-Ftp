using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.Driver
{
    public class DriverLocation
    {
        /// <summary>
        /// Driver that has this job on board
        /// </summary>
        public string DriverNumber { get; set; }
        /// <summary>
        /// Status for this Job
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EStatus Status { get; set; }
        /// <summary>
        /// Current Latitude
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Current Longitude
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// UTC Gps DateTime
        /// </summary>
        public string GpsUtcDateTime { get; set; }
        /// <summary>
        /// Reference associated with this Job
        /// </summary>
        public string Reference { get; set; }
        public override string ToString()
        {
            return "Reference:" + Reference + ", DriverNumber:" + DriverNumber + ", Status:" + Status.ToString() + ", Latitude:" + Latitude +
                   ", Longitude:" + Longitude + ", GpsUtcDateTime:" + GpsUtcDateTime;
        }
    }
}
