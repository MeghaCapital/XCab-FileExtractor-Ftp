namespace Data.Model
{
    public class ToshibaTrackingDetail
    {
        public string Reference1 { get; set; }

        public string Reference2 { get; set; }

        public string FromAddressLine1 { get; set; }

        public string FromAddressLine2 { get; set; }

        public string FromSuburb { get; set; }

        public int FromPostcode { get; set; }

        public string ToAddressLine1 { get; set; }

        public string ToAddressLine2 { get; set; }

        public string ToSuburb { get; set; }

        public int ToPostcode { get; set; }

        public string Consignment { get; set; }

        public int JobNumber { get; set; }

        public DateTime JobDate { get; set; }

        public DateTime PickupComplete { get; set; }

        public DateTime DeliveryComplete { get; set; }

        public string AccountCode { get; set; }

        public string CapitalState { get; set; }

        public int StateId { get; set; }

        public string PODSigName { get; set; }

        public string PODImage { get; set; }
    }
}
