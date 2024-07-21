using Core;

namespace Data.Api.TrackingEvents.Model
{
    public class TmsTrackingRequest: TrackingRequest
    {
        
        public int? ComoJobId { get; set; }
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
    }
}
