using Newtonsoft.Json;

namespace Data.Model.ConsolidatedBooking
{
    /// <summary>
    ///     Response to the received consolidated booking request
    /// </summary>
    public class ConsolidatedBookingResponse
    {
        public ConsolidatedBookingResponse()
        {
            Status = new BookingStatus();
        }

        /// <summary>
        ///     Booking Status
        /// </summary>
        [JsonProperty("Status")] 
        public BookingStatus Status { get; set; }

        /// <summary>
        ///     Capital Transport Job Number
        /// </summary>
        [JsonProperty("JobNumber")] 
        public string JobNumber { get; set; }

        /// <summary>
        ///     Reference 1 attached to the Booking - could be an ID to reconcile at client's end
        /// </summary>
        [JsonProperty("Reference1")] 
        public string Reference1 { get; set; }

        /// <summary>
        ///     Reference 2 attached to the Booking - could be an ID to reconcile at client's end
        /// </summary>
        [JsonProperty("Reference2")] 
        public string Reference2 { get; set; }

        /// <summary>
        ///     Status Codes for the Booking Request
        /// </summary>

        [JsonProperty("State")] 
        public RequestState State { get; set; }

        /// <summary>
        ///     Job Price in AUD Excluding GST
        /// </summary>
        [JsonProperty("JobPriceExGst")] 
        public string JobPriceExGst { get; set; }

        /// <summary>
        ///     GST in AUD for the Booking
        /// </summary>
        [JsonProperty("Gst")] 
        public string Gst { get; set; }

        /// <summary>
        ///     Job Total Price Including GST in AUD
        /// </summary>
        [JsonProperty("JobTotalPrice")] 
        public string JobTotalPrice { get; set; }

        /// <summary>
        ///     Returns the ETA on the Booking
        /// </summary>        

        /// <summary>
        ///     String Representation for the Response
        /// </summary>
        /// <returns></returns>        
        override public string ToString()
        {
            return "StatusCode:" + Status.Code + ", Status Description:" + Status.Description + ", State:" +
                   State
                   + ", JobNumber" + JobNumber + ", Job Price Exc Gst:" + JobPriceExGst + ", Gst:" + Gst +
                   //", Total Price:" + JobTotalPrice +", ETA:"+Eta;
                   ", Total Price:" + JobTotalPrice;
        }
    }
}