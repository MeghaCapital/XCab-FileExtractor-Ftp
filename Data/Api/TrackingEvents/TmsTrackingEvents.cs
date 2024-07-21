using Core;
using Data.Model.Poc;
using Data.Model.PodStreamer;

namespace Data.Api.TrackingEvents
{
    public class TmsTrackingEvents
    {
        public int DriverNumber { get; set; }

        public DateTime? UploadDateTime { get; set; }
        public DateTime AllocationDateTime { get; set; }
        public string PickupArriveLatitude { get; set; }
        public string PickupArriveLongitude { get; set; }
        public DateTime PickupArriveDateTime { get; set; }
        public string PickupCompleteLatitude { get; set; }
        public string PickupCompleteLongitude { get; set; }
        public DateTime PickupCompleteDateTime { get; set; }
        public string DeliveryArriveLatitude { get; set; }
        public string DeliveryArriveLongitude { get; set; }
        public DateTime DeliveryArriveDateTime { get; set; }
        public string DeliveryCompleteLatitude { get; set; }
        public string DeliveryCompleteLongitude { get; set; }
        public DateTime DeliveryCompleteDateTime { get; set; }
        public bool Completed { get; set; }
        public int BookingId { get; set; }
        public int StateId { get; set; }
        public PodImage PickupPodImage { get; set; }
        public PodImage DeliveryPodImage { get; set; }
        public ICollection<PocImage> PickupImages { get; set; }
        public ICollection<PocImage> DeliveryImages { get; set; }
        public bool Cancelled { get; set; }
        public bool UsingComo { get; set; }
        public int? ComoJobId { get; set; }
        public ICollection<EEvents> NewEvents { get; set; }
        public string FromDetail1 { get; set; }
        public string FromDetail2 { get; set; }
        public string FromDetail3 { get; set; }
        public string FromDetail4 { get; set; }
        public string FromDetail5 { get; set; }
        public string FromSuburb { get; set; }
        public int FromPostcode { get; set; }
        public string ToDetail1 { get; set; }
        public string ToDetail2 { get; set; }
        public string ToDetail3 { get; set; }
        public string ToDetail4 { get; set; }
        public string ToDetail5 { get; set; }
        public string ToSuburb { get; set; }
        public int ToPostcode { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string ConsignmentNumber { get; set; }

        public string AccountCode { get; set; }

        public int Jobnumber { get; set; }
    }

    public enum EEvents
    {
        PickupArrive,
        PickupCompletion,
        DeliveryArrive,
        DeliveryCompletion
	}
}
