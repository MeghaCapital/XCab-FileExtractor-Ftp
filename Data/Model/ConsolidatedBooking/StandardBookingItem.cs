using System.Collections.Generic;

namespace Data.Model.ConsolidatedBooking
{
    /// <summary>
    ///     Item attached to the booking request
    /// </summary>
    public class StandardBookingItem : BaseBookingItem
    {
        /// <summary>
        ///     Item Description
        /// </summary>
        public override string Description { get; set; }

        /// <summary>
        ///     Item Barcode
        /// </summary>
        public override List<string> Barcode { get; set; }

        /// <summary>
        ///     Item Weight in Kgs
        /// </summary>
        public override double? Weight { get; set; }

        /// <summary>
        ///     Item Volume in CC
        /// </summary>
        public override double? Volume { get; set; }

        /// <summary>
        ///     Item Length in cm
        /// </summary>
        public override double? Length { get; set; }

        /// <summary>
        ///     Item Width in cm
        /// </summary>
        public override double? Width { get; set; }

        /// <summary>
        ///     Item Height in cm
        /// </summary>
        public override double? Height { get; set; }
    }
}