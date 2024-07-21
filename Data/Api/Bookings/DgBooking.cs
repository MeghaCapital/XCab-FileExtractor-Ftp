namespace Data.Api.Bookings
{
	public class DgBooking : BookingModel.OnlineBooking
    {
        public ICollection<DgBookingItem>? DgBookingItems;
        public bool? TransportDocumentWillAccompanyLoad { get; set; }
        public bool? PackagedInAccordanceWithAdg7_4 { get; set; }
    }
}