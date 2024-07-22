using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.booker.Data.Response
{
    public class XcabJobResponse
    {
        public long JobNumber { get; set; }

        public double JobTotalPrice { get; set; }

        public double JobPriceExGst { get; set; }

        public double Gst { get; set; }

        public int JobId { get; set; }
    }
}
