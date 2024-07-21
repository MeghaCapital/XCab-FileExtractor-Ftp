using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.Bookings
{
    public class ApiBookingResponse
    {
        [JsonProperty("StatusDescription")]
        public string? StatusDescription { get; set; }

        [JsonProperty("JobNumber")]
        public string JobNumber { get; set; } = default!;

        [JsonProperty("Reference1")]
        public string? Reference1 { get; set; }

        [JsonProperty("Reference2")]
        public string? Reference2 { get; set; }

        [JsonProperty("StatusCode")]
        public BookingModel.StatusCode StatusCode { get; set; }

        [JsonProperty("State")]
        public BookingModel.State State { get; set; }

        [JsonProperty("JobPriceExGst")]
        public string? JobPriceExGst { get; set; }

        [JsonProperty("Gst")]
        public string? Gst { get; set; }

        [JsonProperty("JobTotalPrice")]
        public string? JobTotalPrice { get; set; }

        override public string ToString()
        {
            return "StatusCode:" + StatusCode + ", Status Description:" + StatusDescription + ", State:" +
                   State
                   + ", JobNumber" + JobNumber + ", Job Price Exc Gst:" + JobPriceExGst + ", Gst:" + Gst +
                   //", Total Price:" + JobTotalPrice +", ETA:"+Eta;
                   ", Total Price:" + JobTotalPrice;
        }
    }
}
