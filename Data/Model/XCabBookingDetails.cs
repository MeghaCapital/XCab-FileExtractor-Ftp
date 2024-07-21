using System;

namespace Data.Model
{
    public class XCabBookingDetails
    {
        public bool UsesSoap { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string TrackingFolderName { get; set; }

        public string TrackingSchemaName { get; set; }

        public bool IsbookingOnlyClient { get; set; }

        public string BookingsFolderName { get; set; }

        public int BookingId { get; set; }

        public int StateId { get; set; }

        public string AccountCode { get; set; }

        public string ServiceCode { get; set; }

        public string FromSuburb { get; set; }

        public string FromPostcode { get; set; }

        public string FromDetail1 { get; set; }

        public string FromDetail2 { get; set; }

        public string FromDetail3 { get; set; }

        public string FromDetail4 { get; set; }

        public string FromDetail5 { get; set; }

        public string ToSuburb { get; set; }

        public string ToPostcode { get; set; }

        public string ToDetail1 { get; set; }

        public string ToDetail2 { get; set; }

        public string ToDetail3 { get; set; }

        public string ToDetail4 { get; set; }

        public string ToDetail5 { get; set; }

        public string Ref1 { get; set; }

        public string Ref2 { get; set; }

        public DateTime DespatchDateTime { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal TotalVolume { get; set; }

        public int TotalItems { get; set; }

        public string ConsignmentNumber { get; set; }

        public int PreAllocatedDriverNumber { get; set; }

        public string Caller { get; set; }

        public string ExtraDelInformation { get; set; }

        public string ExtraPuInformation { get; set; }

        public bool IsQueued { get; set; }

        public bool ATL { get; set; }

        public bool BarcodesAllowed { get; set; }

        public DateTime TimeSlotStart { get; set; }

        public int TimeSlotDuration { get; set; }

        public int RouteId { get; set; }

        public int LegNumber { get; set; }

        public bool RouteToCustomer { get; set; }

        public string UNSEmail { get; set; }

        public string UNSSMSNumber { get; set; }

        public int NumOfRemarks { get; set; }

        public int NumOfItems { get; set; }

        public int NumContacts { get; set; }

        public int LoginId { get; set; }

        public bool UsingComo { get; set; }

        public bool SkipFtpAccess { get; set; }

        public bool TimeSlotsAllowed { get; set; }

        public string Route { get; set; }

        public string DropSequence { get; set; }
    }
}
