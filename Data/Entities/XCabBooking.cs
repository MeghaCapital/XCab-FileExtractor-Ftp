using Core;
using Data.Entities.Booking.TimeSlots;
using Data.Entities.ConsolidatedReferences;
using Data.Entities.Sundries;
using Data.Model.Route;
using System;
using System.Collections.Generic;

namespace Data.Entities
{
    /// <summary>
    /// Enity class describing a booking from XCab Database
    /// </summary>
    public class XCabBooking
    {
        public int BookingId { get; set; }
        public int StateId { get; set; }
        public int LoginId { get; set; }
        public string AccountCode { get; set; }
        public string ServiceCode { get; set; }
        public string FromSuburb { get; set; }
        public string FromPostcode { get; set; }
        public string ToPostcode { get; set; }
        public string FromDetail1 { get; set; }
        public string FromDetail2 { get; set; }
        public string FromDetail3 { get; set; }
        public string FromDetail4 { get; set; }
        public string FromDetail5 { get; set; }
        public string ToSuburb { get; set; }
        public string ToDetail1 { get; set; }
        public string ToDetail2 { get; set; }
        public string ToDetail3 { get; set; }
        public string ToDetail4 { get; set; }
        public string ToDetail5 { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public DateTime? UploadDateTime { get; set; }
        public int Tplus_JobNumber { get; set; }
        public string PickupArriveLocation { get; set; }
        public string PickupCompleteLocation { get; set; }
        public string DeliveryArriveLocation { get; set; }
        public string DeliveryCompleteLocation { get; set; }
        public DateTime PickupArrive { get; set; }
        public DateTime PickupComplete { get; set; }
        public DateTime DeliveryArrive { get; set; }
        public DateTime DeliveryComplete { get; set; }
        public bool Completed { get; set; }
        public string DriverNumber { get; set; }
        public string ExtraPuInformation { get; set; }
        public string ExtraDelInformation { get; set; }
        public string PreAllocatedDriverNumber { get; set; }
        public string Caller { get; set; }
        public bool Cancelled { get; set; }
        public bool OkToUpload { get; set; } = true;
        public string TotalItems { get; set; }
        public string TotalWeight { get; set; }
        public string TotalVolume { get; set; }
        public List<Item> lstItems { get; set; }
        public DateTime TPLUS_JobAllocationDate { get; set; }
        public DateTime? AdvanceDateTime { get; set; }
        public DateTime DespatchDateTime { get; set; }
        public bool IsQueued { get; set; }
        public string SpecialInstructions { get; set; }
        public string ConsignmentNumber { get; set; }
        public List<ContactDetail> lstContactDetail { get; set; }
        public bool UploadedToTplus { get; set; }
        public RouteLeg RouteLeg { get; set; }
        public bool ATL { get; set; }
        public List<string> Remarks { get; set; }
        public XCabSundry Sundries { get; set; }
        public XCabTimeSlots XCabTimeSlots { get; set; }
        public Notification Notification { get; set; }
        public bool UsingComo { get; set; }
        public int? ComoJobId { get; set; }
        public List<XCabClientReferences>? XCabClientReferences { get; set; }
        public bool IsNtJob { get; set; }
        public XCabBookingRoute XCabBookingRoute { get; set; }
        public string? ETA { get; set; }
    }
}
