using System;

namespace Data.Model.PodStreamer
{
    public class PodImage
    {
        public string JobNumber { get; set; }
        public string SubJobNumber { get; set; }
        public byte[] Image { get; set; }
        public string FileName { get; set; }
        public string TPLUS_JobNumber { get; set; }
        public DateTime JobDateTime { get; set; }
        public string HtmlLink { get; set; }
        public string PodName { get; set; }
    }

}
