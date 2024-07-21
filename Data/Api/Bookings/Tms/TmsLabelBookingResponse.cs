using Core;
using Newtonsoft.Json;

namespace Data.Api.Bookings.Tms
{
    public class TmsLabelBookingResponse : TmsBookingResponse
    {
        [JsonProperty("Label")]
        public string? Label { get; set; }
    }
}
