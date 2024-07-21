using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.BarcodeScan.V2
{
    public class BarcodeExceptionDetails
    {
        public string Barcode { get; set; }
        public string Reason { get; set; }
        public string AuthName { get; set; }

    }
}
