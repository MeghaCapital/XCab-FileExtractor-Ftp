using System;

namespace Data.Entities.Ilogix
{

    public class IlogixJobLegLookups : IlogixJobLeg
    {
        public DateTime UploadDateTime { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string FromDetail4 { get; set; }
        public string iLogixWWWLink { get; set; }
        public string ToDetail1 { get; set; }
        public int Tplus_JobNumber { get; set; }
    }
}
