namespace Data.Model.ConsolidatedBooking
{
    public class Address
    {
        /// <summary>
        ///     Suburb (Suburb name): Please use Standard Australia Post Suburb Names
        /// </summary>
        //[Required]
        public string Suburb { get; set; }

        /// <summary>
        ///     Postcode (Postcode)
        /// </summary>
        //[Required]
        public string? Postcode { get; set; }

        /// <summary>
        ///     Address Line 1: This is the first address line. Usually it is the customer name.
        /// </summary>
        //[Required]
        public string Detail1 { get; set; }

        /// <summary>
        ///     Address Line 2: This is the second address line. Usually it is the street address.
        /// </summary>
        //[Required]
        public string? Detail2 { get; set; }

        /// <summary>
        ///     Address Line 3: Any extra address information required to be attached.
        /// </summary>
        public string? Detail3 { get; set; }

        /// <summary>
        ///     Address Line 4: Any extra address information required to be attached
        /// </summary>
        public string? Detail4 { get; set; }

        /// <summary>
        ///     Address Line 5: Any extra address information required to be attached
        /// </summary>
        public string? Detail5 { get; set; }
    }
}