using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model.Poc
{
	public class PocImage
	{
        public string JobNumber { get; set; }
        public string TPLUS_JobNumber { get; set; }

        public string SubJobNumber { get; set; }

        public byte[] Image { get; set; }

        public string FileName { get; set; }        

        public DateTime JobDateTime { get; set; }

        public string HtmlLink { get; set; }
    }
}
