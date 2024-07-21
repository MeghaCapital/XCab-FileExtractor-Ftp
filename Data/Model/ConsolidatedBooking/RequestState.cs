using Newtonsoft.Json;

namespace Data.Model.ConsolidatedBooking
{
    /// <summary>
    ///     Defines what Capital State the Booking corresponds for
    /// </summary>
    public enum RequestState
    {
        /// <summary>Vic Booking</summary>
        [JsonProperty("Vic")]
        Vic = 1,

        /// <summary>Nsw Booking</summary>
        [JsonProperty("Nsw")] 
        Nsw = 2,

        /// <summary>Qld Booking</summary>
        [JsonProperty("Qld")] 
        Qld = 3,

        /// <summary>Sa Booking</summary>
        [JsonProperty("Sa")] 
        Sa = 4,

        /// <summary>Wa Booking</summary>
        [JsonProperty("Wa")] 
        Wa = 5
    }
}