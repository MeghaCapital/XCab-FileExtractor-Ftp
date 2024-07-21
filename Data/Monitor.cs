using Core;

namespace Data
{
    /// <summary>
    /// Provides structure for Monitoring of Jobs
    /// Modification History:
    /// Date        Version     Modified By     Description
    /// 08/02/2016  1.5         Rahul Sinha     Added Consignmnet Number field (As required to support Tracking for Machship)
    /// </summary>
    public class Monitor
    {
        public string JobNumber { get; set; }
        public string StateId { get; set; }
        public int BookingId { get; set; }
        public string ClientCode { get; set; }
        public string JobBookingDate { get; set; }
        public string JobAllocationDate { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string DriverNumber { get; set; }
        public string AccountCode { get; set; }
        public LoginDetails LoginDetails { get; set; }
        public string TplusPodTime { get; set; }
        public string ConsignmentNumber { get; set; }
        public bool SkipFtpAccess { get; set; }
        public int ExternalClientIntegrationId { get; set; }
    }
}
