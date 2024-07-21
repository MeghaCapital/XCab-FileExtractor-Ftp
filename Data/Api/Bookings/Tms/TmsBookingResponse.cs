using Core;
using Newtonsoft.Json;

namespace Data.Api.Bookings.Tms
{
    public class TmsBookingResponse
    {
        [JsonProperty("StatusDescription")]
        public string StatusDescription { get; set; }

        [JsonProperty("JobNumber")]
        public string JobNumber { get; set; }

        [JsonIgnore]
        [JsonProperty("JobId")]
        public int JobId { get; set; }

        [JsonProperty("Reference1")]
        public string Reference1 { get; set; }

        [JsonProperty("Reference2")]
        public string Reference2 { get; set; }

        [JsonProperty("State")]
        public EStates State { get; set; }

        [JsonProperty("StatusCode")]
        public StatusCode StatusCode { get; set; }

        [JsonProperty("JobPriceExGst")]
        public string JobPriceExGst { get; set; }

        [JsonProperty("Gst")]
        public string Gst { get; set; }

        [JsonProperty("JobTotalPrice")]
        public string JobTotalPrice { get; set; }

        [JsonProperty("Request")]
        public string Request { get; set; }

        [JsonProperty("Response")]
        public string Response { get; set; }

        [JsonProperty("QuoteRequest")]
        public string QuoteRequest { get; set; }

        [JsonProperty("QuoteResponse")]
        public string QuoteResponse { get; set; }
    }

    public enum StatusCode
    {
        [JsonProperty("Ok")]
        Ok = 1,

        [JsonProperty("AuthenticationFailed")]
        AuthenticationFailed = 2,

        [JsonProperty("InvalidBookingRequest")]
        InvalidBookingRequest = 3,

        [JsonProperty("SystemError")]
        SystemError = 4,

        [JsonProperty("AuthorizationFailed")]
        AuthorizationFailed = 5,

        [JsonProperty("AccessFailed")]
        AccessFailed = 6,
    }
}
