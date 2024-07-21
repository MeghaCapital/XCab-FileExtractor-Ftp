using Data.Model.BarcodeScan.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.BarcodeScan.V2
{
    public class BarcodeScanDetails
    {
        public int DriverNumber { get; set; }
        public bool IsAdHocBarcode { get; set; }
        public BarcodeScanLegDetails PickupLegDetails { get; set; }
        public BarcodeScanLegDetails DeliveryLegDetails { get; set; }
    }
}
