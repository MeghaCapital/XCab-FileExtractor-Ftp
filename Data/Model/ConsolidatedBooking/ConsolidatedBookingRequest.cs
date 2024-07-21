using System.Collections.Generic;

namespace Data.Model.ConsolidatedBooking
{
    public class ConsolidatedBookingRequest
    {
        /// <summary>
        ///     Type of booking request
        /// </summary>
        //[Required]
        public BookingRequestType Type { get; set; }

        /// <summary>
        ///     User Credentials for request authentication
        /// </summary>
        //[Required]
        public UserAuthentication UserCredentials { get; set; }

        /// <summary>
        ///     Account Code under which the Booking will be made
        /// </summary>
        //[Required]
        public string AccountCode { get; set; }

        /// <summary>
        ///     State for which the booking request is for
        /// </summary>
        //[Required]
        public RequestState State { get; set; }

        /// <summary>
        ///     The booking which will take place
        /// </summary>
        //[Required]
        public List<BaseBooking> Bookings { get; set; }
    }
}