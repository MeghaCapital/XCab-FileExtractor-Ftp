using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents.Model
{
    public class ItemsNotScanned
    {
        public string Barcode { get; set; }
        public string ExceptionReason { get; set; }
        public string AuthorizationName { get; set; }
    }
}
