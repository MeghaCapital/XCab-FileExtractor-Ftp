using Core;
using System.Collections.Generic;

namespace Data.Model
{
    public class HpmBooking : Booking
    {
        public ICollection<string> References { get; set; }
    }
}
