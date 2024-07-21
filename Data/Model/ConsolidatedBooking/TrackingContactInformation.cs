namespace Data.Model.ConsolidatedBooking
{
    public class TrackingContactInformation
    {
        /// <summary>
        /// Mobile number to be attached to the booking – this number could be used to get job tracking notifications. 
        /// </summary>
        public string? SmsNumber { get; set; }
        /// <summary>
        /// Email Address for notifications
        /// </summary>
        public string? EmailAddress { get; set; }
    }
}