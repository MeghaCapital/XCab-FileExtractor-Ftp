using System.Collections.Generic;

namespace Data.Model.ConsolidatedBooking
{
    public class StandardBooking : BaseBooking
    {
        /// <summary>
        ///     List of Items that need to be attached to the booking
        /// </summary>
        public override List<BaseBookingItem> BookingItems { get; set; }
    }
}