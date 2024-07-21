namespace Data.Api.TrackingEvents.Model
{
    public class TrackingRequest
    {
        public string AccountCode { get; set; }
        public int StateId { get; set; }
        public int XCabBookingId { get; set; }
        public DateTime JobAllocationDateTime { get; set; }

        public int DriverNumber { get; set; }
        public DateTime? JobBookedDate { get; set; }
        public string JobNumber { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string ConsignmentNumber { get; set; }
    }
}
