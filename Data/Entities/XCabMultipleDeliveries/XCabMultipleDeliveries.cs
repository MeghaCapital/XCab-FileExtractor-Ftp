using System;

namespace Data.Entities.XCabMultipleDeliveries
{
    public class XCabMultipleDeliveries
    {
        public string PrimaryJobNumber { get; set; }
        public string DriverNumber { get; set; }

        public DateTime JobDate { get; set; }
        public string AccountCode { get; set; }

        public string DeliverySuburb { get; set; }

        public string Ref1 { get; set; }

        public string Ref2 { get; set; }
        public string LegNumber { get; set; }
        public string TotalLegs { get; set; }
        public DateTime? DeliveryArrive { get; set; }

        public DateTime? DeliveryComplete { get; set; }

        public DateTime Eta { get; set; }
        public string Id { get; set; }
    }
}
