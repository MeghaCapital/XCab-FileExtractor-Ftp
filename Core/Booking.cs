using System;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// Basic Entity class representing an XML Booking
    /// </summary>
    [Serializable]
    public class Booking
    {
        public string AccountCode { get; set; }

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

        public string StateId { get; set; }

        public string Ref1 { get; set; }

        public string Ref2 { get; set; }

        public string ConsignmentNumber { get; set; }

        public List<Item> lstItems { get; set; }

        public List<ContactDetail> lstContactDetail { get; set; }

        public Notification Notification { get; set; }

        public string ServiceCode { get; set; }

        public string TotalItems { get; set; }

        public string TotalWeight { get; set; }


        public string TotalVolume { get; set; }

        public bool UsesSOAP { get; set; }

        public RouteLeg RouteLeg { get; set; }
        /// <summary>
        /// ATL flag
        /// </summary>
        public bool ATL { get; set; }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return "BookingId: " + Id + " AccountCode:" + AccountCode + " ServiceCode:" + ServiceCode + " From Address: " + FromDetail1 + " " + FromDetail2 + " " + FromDetail3 + " " + FromDetail4 + " " + FromDetail5 + " " + FromSuburb + " " + FromPostcode +
                   " To Address: " + ToDetail1 + " " + ToDetail2 + " " + ToDetail3 + " " + ToDetail4 + " " + ToDetail5 + " " + ToSuburb + " " + ToPostcode +
                   " Total Pieces:" + (string.IsNullOrEmpty(TotalItems) ? lstItems.Count.ToString() : TotalItems) +
                   " Total Weight:" + TotalWeight + " Ref1: " + Ref1 + " Ref2: " + Ref2;
        }


        public string Id { get; set; }

        public LoginDetails LoginDetails { get; set; }

        public DateTime DespatchDateTime { get; set; }

        public DateTime UploadDateTime { get; set; }

        public bool IsBookingOnlyClient { get; set; }

        public bool OkToUpload { get; set; }

        public bool UploadedToTplus { get; set; }

        public string TPLUS_JobNumber { get; set; }

        public DateTime AdvanceDateTime { get; set; }

        public int DriverNumber { get; set; }

        public int PreAllocatedDriverNumber { get; set; }

        public string Caller { get; set; }

        public string ExtraPuInformation { get; set; }

        public string ExtraDelInformation { get; set; }

        public string SpecialInstructions { get; set; }

        public bool IsQueued { get; set; }

        public bool BarcodesAllowed { get; set; }

        public TimeSlot PickUpTimeSlot { get; set; }

        public TimeSlot DeliveryTimeSlot { get; set; }

        public List<Sundry> Sundries { get; set; }

        public string TrackAndTraceSmsNumber { get; set; }

        public string TrackAndTraceEmailAddress { get; set; }

        public List<ExtraFields> lstExtraFields { get; set; }

        public List<string> Remarks { get; set; }

        public string ATLInstructions { get; set; }

        public BookingContactInformation BookingContactInformation { get; set; }

        public bool UsingComo { get; set; }

        public int ComoJobId { get; set; }

        public bool SkipFtpAccess { get; set; }

        public Booking ShallowCopy()
        {
            return (Booking)this.MemberwiseClone();
        }

        public Booking DeepCopy()
        {
            var copy = (Booking)this.MemberwiseClone();
            copy.lstItems = new List<Item>();
            return copy;
        }
    }
}

