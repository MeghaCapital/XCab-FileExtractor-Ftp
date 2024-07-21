namespace Data.Model.ConsolidatedBooking
{
    /// <summary>
    ///     Contact Information for the request
    /// </summary>
    public class RequestBookingContactInformation
    {
        /// <summary>
        ///     Contact Name for the Booking
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Phone number for the booking
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}