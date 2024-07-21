using System;

namespace Data.Entities.Ilogix
{
    public class PocImage
    {
        public string JobNumber { get; set; }
        public string SubJobNumber { get; set; }
        public byte[] pocImage { get; set; }
        public string FileName { get; set; }
        public string TPLUS_JobNumber { get; set; }
        public DateTime JobDateTime { get; set; }
        public string HtmlLink { get; set; }
    }
}
