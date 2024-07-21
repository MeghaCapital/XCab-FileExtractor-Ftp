namespace Core
{
    public class ValidatedBooking
    {

        public string ErrorDescription { get; set; }

        public string ValidatedSuburb { get; set; }

        public string ValidatedPostcode { get; set; }

        public Booking Booking { get; set; }
    }
}
