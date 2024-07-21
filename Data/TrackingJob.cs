using Core;
using System;

namespace Data
{
    /// <summary>
    /// Structure to hold Tracking Job Information
    /// Modification History:
    /// Date        Version     Modified By     Description
    /// 08/02/2016  1.5         Rahul Sinha     Added Consignmnet Number field (As required to support Tracking for Machship)
    /// </summary>    
    public class TrackingJob
    {
        public string JobNumber { get; set; }
        public DateTime? UploadDateTime { get; set; }
        public DateTime? JobAllocationDate { get; set; }
        public int StateId { get; set; }
        public int DriverId { get; set; }
        public string AccountCode { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string ConsignmentNumber { get; set; }
        public Location eventLocation { get; set; }
        public LoginDetails LoginDetails { get; set; }
        public string eventDateTime { get; set; }
        public ETrackingEvent CurrentTrackingEvent { get; set; }
        public int BookingId { get; set; }
        public PodUrl podUrl { get; set; }
        public string FromDetail1 { get; set; }
        public string FromDetail2 { get; set; }
        public string FromDetail3 { get; set; }
        public string FromDetail4 { get; set; }
        public string FromDetail5 { get; set; }
        public string FromSuburb { get; set; }
        public string FromPostcode { get; set; }
        public string ToDetail1 { get; set; }
        public string ToDetail2 { get; set; }
        public string ToDetail3 { get; set; }
        public string ToDetail4 { get; set; }
        public string ToDetail5 { get; set; }
        public string ToSuburb { get; set; }
        public string ToPostcode { get; set; }
        public double TotalWeight { get; set; }
        public double TotalVolume { get; set; }
        public int ExternalClientIntegrationId { get; set; }
        public DateTime? DeliveryEta { get; set; }
        public string PickupArriveLocation { get; set; }
        public DateTime? PickupArrive { get; set; }
        public string PickupCompleteLocation { get; set; }
        public DateTime? PickupComplete { get; set; }
        public string DeliveryArriveLocation { get; set; }
        public DateTime? DeliveryArrive { get; set; }
        public string DeliveryCompleteLocation { get; set; }
        public DateTime? DeliveryComplete { get; set; }
        public int? ComoJobId { get; set; }
        public bool UsingComo { get; set; }
        public string ServiceCode { get; set; }

        public class PodUrl
        {
            public string PodJobNumber { get; set; }
            public string PodSubJobNumber { get; set; }
        }
        public string TplusPodTime { get; set; }
        public override string ToString()
        {
            return "Job:" + JobNumber + ",JobBookingDay:" + UploadDateTime + ",TrackingEvent:" + CurrentTrackingEvent.ToString();
        }
    }
    public class Location
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    public enum ETrackingEvent
    {
        JOB_BOOKED,
        PICKUP_ARRIVE,
        PICKUP_COMPLETE,
        DELIVERY_ARRIVE,
        DELIVERY_COMPLETE,
        CANCELLED
    }


}
