using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.Bookings.Tms
{   
    public class TmsRequestBookingItem
    {
        public string? Barcode { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Cubic { get; set; }
        public double? Weight { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }

    }
}
