using Newtonsoft.Json;

namespace Data.Api.Bookings
{
	/// <summary>
	///     Defines the Model associated with a booking request
	/// </summary>
	public class BookingModel
    {
        /// <summary>
        ///     Defines the structure of an online booking request
        /// </summary>
        
        public class OnlineBooking
        {
            /// <summary>
            ///     User Credentials for request authentication
            /// </summary>
            //[Required]
            public UserCredentials UserCredentials { get; set; } = default!;

            /// <summary>
            /// Use special instructions if provided
            /// </summary>
            public string? SpecialInstructions { get; set; }

            /// <summary>
            ///     Request Type: For creating a booking this is set as booking, for others review RequestType
            /// </summary>
            //[Required(ErrorMessage = "The RequestType field is required.")]
            public RequestType? RequestType { get; set; }

            /// <summary>
            ///     Account Code under which the Booking will be made
            /// </summary>
            //[Required]
            public string AccountCode { get; set; } = default!;

            /// <summary>
            ///     Service Code that will be used to create the booking
            /// </summary>
            //[Required]
            public string ServiceCode { get; set; } = default!;

            /// <summary>
            ///     From Suburb (Suburb name from where the goods will be picked up): Please use Standard Australia Post Suburb Names
            /// </summary>
            //[Required]
            public string FromSuburb { get; set; } = default!;

            /// <summary>
            ///     From Postcode (Postcode from where the goods will be picked up)
            /// </summary>
            //[Required]
            public string? FromPostcode { get; set; }

            /// <summary>
            ///     From Address Line 1: This is the first address line from where goods will be picked up. Usually it is the customer
            ///     name
            /// </summary>
            //[Required]
            public string FromDetail1 { get; set; } = default!;

            /// <summary>
            ///     From Address Line 2: This is the second address line from where goods will be picked up. Usually it is the street
            ///     address
            /// </summary>
            //[Required]
            public string? FromDetail2 { get; set; }

            /// <summary>
            ///     From Address Line 3: Any extra address information required to be attached
            /// </summary>
            public string? FromDetail3 { get; set; }

            /// <summary>
            ///     From Address Line 4: Any extra address information required to be attached (Not in Use)
            /// </summary>
            public string? FromDetail4 { get; set; }

            /// <summary>
            ///     From Address Line 5: Any extra address information required to be attached (Not in Use)
            /// </summary>
            public string? FromDetail5 { get; set; }

            /// <summary>
            ///     To Suburb (Suburb name to where the goods will be delivered): Please use Standard Australia Post Suburb Names
            /// </summary>
            //[Required]
            public string ToSuburb { get; set; } = default!;

            /// <summary>
            ///     To Postcode (Postcode to where the goods will be delivered)
            /// </summary>
            //[Required]
            public string? ToPostcode { get; set; }

            /// <summary>
            ///     To Address Line 1: This is the first address line to where goods will be delivered. Usually it is the customer name
            /// </summary>
            //[Required]
            public string ToDetail1 { get; set; } = default!;

            /// <summary>
            ///     To Address Line 2: This is the second address line to where goods will be delivered. Usually it is the street
            ///     address
            /// </summary>
            //[Required]
            public string? ToDetail2 { get; set; }

            /// <summary>
            ///     To Address Line 3: Any extra address information required to be attached
            /// </summary>
            public string? ToDetail3 { get; set; }

            /// <summary>
            ///     To Address Line 4: Any extra address information required to be attached
            /// </summary>
            public string? ToDetail4 { get; set; }

            /// <summary>
            ///     To Address Line 5: Any extra address information required to be attached
            /// </summary>
            public string? ToDetail5 { get; set; }

            /// <summary>
            ///     State for which the booking request is for
            /// </summary>
            //[Required(ErrorMessage = "The State field is required.")]
            public State State { get; set; }

            /// <summary>
            ///     Reference 1 that will be attached with the booking - these could be client references or an unique id that
            ///     reconciles this job at the clients end
            /// </summary>
            public string? Reference1 { get; set; }

            /// <summary>
            ///     Reference 2 that will be attached with the booking - these could be client references or an unique id that
            ///     reconciles this job at the clients end
            /// </summary>
            public string? Reference2 { get; set; }

            /// <summary>
            ///     Consignment Number - currently not in use, please use reference 1 or reference 2
            /// </summary>
            public string? ConsignmentNumber { get; set; }

            /// <summary>
            ///     Total Items attached with the booking
            /// </summary>
            public string? TotalItems { get; set; }

            /// <summary>
            ///     Total Weight of all items that are attached with the booking
            /// </summary>
            public string? TotalWeight { get; set; }

            /// <summary>
            ///     Total Volume in CC that will be attached with the booking
            /// </summary>
            public string? TotalVolume { get; set; }

            /// <summary>
            ///     Advance date time that will be used to create bookings for a future date, if this field is not provided
            ///     the bookings will be created for the same day the request is received
            /// </summary>
            public DateTime? AdvanceDateTime { get; set; }


            /// <summary>
            ///     Used to specify if the booking needs to be sent to a particular driver. If no value is supplied, Capital
            ///     will use the best available driver for doing the job
            /// </summary>
           // [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int? DriverNumber { get; set; }

            /// <summary>
            ///     Caller field identifies the name of the person who made the booking.
            /// </summary>
            public string? Caller { get; set; }

            /// <summary>
            ///     Extra Pickup Information that needs to be attached to the booking
            /// </summary>
            public string? ExtraPuInformation { get; set; }

            /// <summary>
            ///     Extra Delivery Information that needs to be attached to the booking
            /// </summary>
            public string? ExtraDelInformation { get; set; }

            /// <summary>
            /// ATL - authority to leave the goods
            /// </summary>
            public bool? ATL { get; set; }

            /// <summary>
            /// ATL instructions
            /// </summary>
            public string? ATLInstructions { get; set; }

            /// <summary>
            ///     List of Items that need to be attached to the booking
            /// </summary>
            public List<BookingItem>? BookingItems { get; set; }

            /// <summary>
            ///     Contact Information to be attached to the booking
            /// </summary>
            public BookingContactInformation? BookingContactInformation { get; set; }


            /// <summary>
            /// Mobile number to be attached to the booking – this number could be used to get job tracking notifications. 
            /// </summary>
            public string? TrackAndTraceSmsNumber { get; set; }
            /// <summary>
            /// Email Address for notifications
            /// </summary>
            public string? TrackAndTraceEmailAddress { get; set; }
            /// <summary>
            /// Runcode which is mapped to a driver internally
            /// </summary>
            public string? Runcode { get; set; }
            /// <summary>
            /// Custom fields
            /// </summary>
            public List<CustomFields>? CustomFields { get; set; }
            /// <summary>
            /// String represntation
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return "AccountCode:" + AccountCode + ",ServiceCode:" + ServiceCode + ",From Address: " + FromDetail1 +
                       " " + FromDetail2 + " " + FromDetail3 + " " + FromDetail4 + " " + FromDetail5 + " " + FromSuburb +
                       " " + FromPostcode +
                       " To Address: " + ToDetail1 + " " + ToDetail2 + " " + ToDetail3 + " " + ToDetail4 + " " +
                       ToDetail5 + " " + ToSuburb + " " + ToPostcode +
                       " Total Weight:" + TotalWeight;
            }
        }

        /// <summary>
        /// Expose just the price component based on quote request
        /// </summary>
        public class QuoteResponse
        {
            /// <summary>
            ///     Reference 1 attached to the Booking - could be an ID to reconcile at client's end
            /// </summary>
            [JsonProperty("Reference1")]
            public string? Reference1 { get; set; }

            /// <summary>
            ///     Reference 2 attached to the Booking - could be an ID to reconcile at client's end
            /// </summary>
            [JsonProperty("Reference2")]
            public string? Reference2 { get; set; }

            /// <summary>
            ///     Status Codes for the Booking Request
            /// </summary>
            [JsonProperty("StatusCode")]
            public StatusCode StatusCode { get; set; }

            /// <summary>
            ///     State for which the Booking was requested
            /// </summary>
            [JsonProperty("State")]
            public State State { get; set; }
            /// <summary>
            ///     Description for the Status
            /// </summary>
            [JsonProperty("StatusDescription")]
            public string? StatusDescription { get; set; }

            /// <summary>
            ///     Job Price in AUD Excluding GST
            /// </summary>
            [JsonProperty("JobPriceExGst")]
            public string? JobPriceExGst { get; set; }

            /// <summary>
            ///     GST in AUD for the Booking
            /// </summary>
            [JsonProperty("Gst")]
            public string? Gst { get; set; }

            /// <summary>
            ///     Job Total Price Including GST in AUD
            /// </summary>
            [JsonProperty("JobTotalPrice")]
            public string? JobTotalPrice { get; set; }
        }

        /// <summary>
        ///     Response to the received booking request
        /// </summary>
        public class BookingResponse
        {
            /// <summary>
            ///     Description for the Status
            /// </summary>
            [JsonProperty("StatusDescription")]
            public string? StatusDescription { get; set; }

            /// <summary>
            ///     Capital Transport Job Number
            /// </summary>
            [JsonProperty("JobNumber")]
            public string JobNumber { get; set; } = default!;

            /// <summary>
            ///     Reference 1 attached to the Booking - could be an ID to reconcile at client's end
            /// </summary>
            [JsonProperty("Reference1")]
            public string? Reference1 { get; set; }

            /// <summary>
            ///     Reference 2 attached to the Booking - could be an ID to reconcile at client's end
            /// </summary>
            [JsonProperty("Reference2")]
            public string? Reference2 { get; set; }

            /// <summary>
            ///     Status Codes for the Booking Request
            /// </summary>
            [JsonProperty("StatusCode")]
            public StatusCode StatusCode { get; set; }

            /// <summary>
            ///     State for which the Booking was requested
            /// </summary>
            [JsonProperty("State")]
            public State State { get; set; }

            /// <summary>
            ///     Job Price in AUD Excluding GST
            /// </summary>
            [JsonProperty("JobPriceExGst")]
            public string? JobPriceExGst { get; set; }

            /// <summary>
            ///     GST in AUD for the Booking
            /// </summary>
            [JsonProperty("Gst")]
            public string? Gst { get; set; }

            /// <summary>
            ///     Job Total Price Including GST in AUD
            /// </summary>
            [JsonProperty("JobTotalPrice")]
            public string? JobTotalPrice { get; set; }



            /// <summary>
            ///     String Representation for the Response
            /// </summary>
            /// <returns></returns>
            override public string ToString()
            {
                return "StatusCode:" + StatusCode + ", Status Description:" + StatusDescription + ", State:" +
                       State
                       + ", JobNumber" + JobNumber + ", Job Price Exc Gst:" + JobPriceExGst + ", Gst:" + Gst +
                       //", Total Price:" + JobTotalPrice +", ETA:"+Eta;
                       ", Total Price:" + JobTotalPrice;
            }
        }

        /// <summary>
        /// Response to the received booking request with Label
        /// </summary>
        public class BookingLabelResponse : BookingResponse
        {
            /// <summary>
            /// Base 64 Encoded Label
            /// </summary>
            [JsonProperty("Label")]
            public string? Label { get; set; }

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

            /// <summary>Authorization Failed</summary>
            [JsonProperty("AuthorizationFailed")]
            AuthorizationFailed = 5,

        }

        /// <summary>
        ///     Identifies the type of Request
        /// </summary>
        public enum RequestType
        {
            /// <summary>Request Type = Booking</summary>
            Booking = 1
        }

        /// <summary>
        ///     Defines what Capital State the Booking corresponds for
        /// </summary>
        public enum State
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

            /// <summary>Wa Booking</summary>
            [JsonProperty("Wa")]
            Wa = 5,

            /// <summary>Sa Booking</summary>
            [JsonProperty("Sa")]
            Sa = 4,

            /// <summary>Sa Booking</summary>
            [JsonProperty("Nt")]
            Nt = 9
        }

        /// <summary>
        ///     Description for providing User Credentials
        /// </summary>
        public class UserCredentials
        {
            /// <summary>
            ///     Username to be provided with every request
            /// </summary>
            public string Username { get; set; } = default!;

            /// <summary>
            ///     Shared Key to be provided with every request
            /// </summary>
            public string SharedKey { get; set; } = default!;
        }

        /// <summary>
        ///     Contact Information for the request
        /// </summary>
        public class BookingContactInformation
        {
            /// <summary>
            ///     Contact Name for the Booking
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            ///     List of Phone Numbers for the Booking
            /// </summary>
            public List<string>? PhoneNumbers { get; set; }
        }

        /// <summary>
        ///     Item attached to the booking request
        /// </summary>
        public class BookingItem
        {
            /// <summary>
            ///     Item Description
            /// </summary>
            public string? Description { get; set; }

            /// <summary>
            ///     Item Barcode
            /// </summary>
            public string? Barcode { get; set; }

            /// <summary>
            ///     Item Quantity
            /// </summary>
            public int? Quantity { get; set; }

            /// <summary>
            ///     Item Weight in Kgs
            /// </summary>
            public double? Weight { get; set; }

            /// <summary>
            ///     Item Volume in CC
            /// </summary>
            public double? Volume { get; set; }

            /// <summary>
            ///     Item Length in cm
            /// </summary>
            public double? Length { get; set; }

            /// <summary>
            ///     Item Width in cm
            /// </summary>
            public double? Width { get; set; }

            /// <summary>
            ///     Item Height in cm
            /// </summary>
            public double? Height { get; set; }
        }
    }
}