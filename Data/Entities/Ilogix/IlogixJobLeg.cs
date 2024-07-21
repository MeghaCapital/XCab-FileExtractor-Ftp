namespace Data.Entities.Ilogix
{
    public class IlogixJobLeg
    {
        public string JobNumber { get; set; }
        public string SubJobNumber { get; set; }
        public string Details { get; set; }
        public int StatusId { get; set; }
        public string MobileId { get; set; }
        public double DateTimePickup { get; set; }
        public double DateTimeDelivered { get; set; }
        public string Comments { get; set; }
        public string Demurruge { get; set; }
        public string DetentionReasonCode { get; set; }
        public string Reference { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DeliveryAddress { get; set; }
        public string TPLUS_JobNumber { get; set; }
        public string JobMonthId { get; set; }
        public string Rfid { get; set; }
        public string ConsignmentNote { get; set; }
        public double Modified { get; set; }
    }
}
