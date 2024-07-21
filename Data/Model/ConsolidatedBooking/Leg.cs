namespace Data.Model.ConsolidatedBooking
{
    public class Leg
    {
        /// <summary>
        ///     Address details
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        ///     Extra Information that needs to be attached to the booking
        /// </summary>
        public string? ExtraInformation { get; set; }
    }
}