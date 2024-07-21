using System;

namespace Data.Entities.Ilogix
{
    public class PodImage
    {
        public string JobNumber { get; set; }
        public string SubJobNumber { get; set; }
        public byte[] podImage { get; set; }
        public string FileName { get; set; }
        public string TPLUS_JobNumber { get; set; }
        public DateTime JobDateTime { get; set; }
        public string HtmlLink { get; set; }
        public string PODName { get; set; }
    }

}
