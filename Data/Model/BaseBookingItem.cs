using System.Collections.Generic;

namespace Data.Model
{
    public class BaseBookingItem
    {
        /// <summary>
        ///     Item Quantity
        /// </summary>
        public virtual int? Quantity { get; set; }

        /// <summary>
        ///     Item Description
        /// </summary>
        public virtual string? Description { get; set; }

        /// <summary>
        ///     Item Barcode
        /// </summary>
        public virtual List<string> Barcode { get; set; }

        /// <summary>
        ///     Item Weight in Kgs
        /// </summary>
        public virtual double? Weight { get; set; }

        /// <summary>
        ///     Item Volume in CC
        /// </summary>
        public virtual double? Volume { get; set; }

        /// <summary>
        ///     Item Length in cm
        /// </summary>
        public virtual double? Length { get; set; }

        /// <summary>
        ///     Item Width in cm
        /// </summary>
        public virtual double? Width { get; set; }

        /// <summary>
        ///     Item Height in cm
        /// </summary>
        public virtual double? Height { get; set; }

        /// <summary>
        ///     Dangerous Goods Items
        /// </summary>
        public virtual List<DangerousGoodBookingItem>? BookingDgItems { get; set; }
    }
}