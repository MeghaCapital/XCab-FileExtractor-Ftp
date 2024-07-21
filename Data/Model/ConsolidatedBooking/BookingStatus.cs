using Newtonsoft.Json;

namespace Data.Model.ConsolidatedBooking
{
    public class BookingStatus
    {
        public BookingStatus()
        {
            Description = string.Empty;
            Code = StatusCode.InvalidBookingRequest;
        }

        [JsonProperty("Description")] 
        public string Description { get; set; }

        [JsonProperty("Code")] 
        public StatusCode Code { get; set; }
    }

    /// <summary>
    ///     Defines various StatusCodes used for returning a response for
    ///     a Booking request
    /// </summary>
    public enum StatusCode
    {
        /// <summary>Booking Successfully Created</summary>
        [JsonProperty("Ok")] 
        Ok = 1,

        /// <summary>Authentication Failed</summary>
        [JsonProperty("AuthenticationFailed")] 
        AuthenticationFailed = 2,

        /// <summary>Invalid Booking Request - please check that all required parameters are provided with the request</summary>
        [JsonProperty("InvalidBookingRequest")] 
        InvalidBookingRequest = 3,

        /// <summary>System Error</summary>
        [JsonProperty("SystemError")] 
        SystemError = 4,

        /// <summary>Maximum Bookings Per Request Exceeded</summary>
        [JsonProperty("MaxBookingsPerRequestExceeded")] 
        MaxBookingsPerRequestExceeded = 5,

        /// <summary>Authorization Failed</summary>
        [JsonProperty("AuthorizationFailed")] 
        AuthorizationFailed = 6,

        /// <summary> Booking cannot be created as the requested timeslot does not fit within the ETA for the requested service /// </summary>
        [JsonProperty("ETAError")] 
        ETAError = 7
    }
}