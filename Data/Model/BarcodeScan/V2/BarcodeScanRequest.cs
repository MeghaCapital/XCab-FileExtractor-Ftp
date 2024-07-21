using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.BarcodeScan.V2
{
    public  class BarcodeScanRequest
    {
        public string AccountCode { get; set; }
        public string JobNumber { get; set; }
        public int? ComoJobId { get; set; }
        public int StateId { get; set; }
        public DateTime JobDate { get; set; }
    }
}
