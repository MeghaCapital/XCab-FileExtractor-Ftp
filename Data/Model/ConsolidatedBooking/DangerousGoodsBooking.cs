using System.Collections.Generic;

namespace Data.Model.ConsolidatedBooking
{
    public class DangerousGoodsBooking : BaseBooking
    {
        /// <summary>
        ///     List of Items that need to be attached to the booking
        /// </summary>
        public override List<BaseBookingItem> BookingItems { get; set; }

        /// <summary>
        /// Whether DG documents for the delivery is included with the goods
        /// </summary>
        public override bool? TransportDocumentWillAccompanyLoad { get; set; }

        /// <summary>
        /// Packaging of the goods are done with accordance to ADG 7.4 code
        /// </summary>
        public override bool? PackagedInAccordanceWithAdg7_4 { get; set; }
    }
}