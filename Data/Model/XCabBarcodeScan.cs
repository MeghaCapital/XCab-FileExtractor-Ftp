using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class XCabBarcodeScan
    {
        public int SubJobNumber { get; set; }
        public string Barcode { get; set; }    
        public int DriverId { get; set; }
        public bool IsAdHocBracode { get; set; }
        public string AuthName { get; set; }
        public string ExceptionReason { get; set; }
        public DateTime LastModified { get; set; }

    }
}
