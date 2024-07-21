using System;

namespace Data.Entities.Tplus
{
    public class TplusJobEntity
    {
        public string ClientCode { get; set; }
        public DateTime? JobDate { get; set; }
        public string JobNumber { get; set; }
        public int BookingId { get; set; }
        public int LoginId { get; set; }
        public int StateId { get; set; }
        public DateTime DateInserted { get; set; }
        public string FromSuburb { get; set; }
        public string ToSuburb { get; set; }
        public string FromPostcode { get; set; }
        public string ToPostcode { get; set; }
        public string FromDetail1 { get; set; }
        public string FromDetail2 { get; set; }
        public string FromDetail3 { get; set; }
        public string FromDetail4 { get; set; }
        public string FromDetail5 { get; set; }
        public string ToDetail1 { get; set; }
        public string ToDetail2 { get; set; }
        public string ToDetail3 { get; set; }
        public string ToDetail4 { get; set; }
        public string ToDetail5 { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string ServiceCode { get; set; }
        public bool UploadedToTplus { get; set; }
        public DateTime? DeliveryEta { get; set; }
        public DateTime? AdvanceDateTime { get; set; }
        public DateTime? DespatchDateTime { get; set; }
        public string DriverNumber { get; set; }
        public override string ToString()
        {
            return "Job#:" + JobNumber + ",BookingId:" + BookingId + ",StateId:" + StateId + ",Ref1:" + Ref1 + ",Ref2:" + Ref2 + ",LoginId:" + LoginId + ",FromSuburb:" + FromSuburb + "FromPostcode:" + FromPostcode + ",ToSuburb:" + ToSuburb + ",ToPostcode:" + ToPostcode;
        }
    }
}
