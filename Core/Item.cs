using System;

namespace Core
{
    /// <summary>
    /// Item DTO
    /// </summary>
    [Serializable]
    public class Item
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public String Description { get; set; }
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public double Length { get; set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height { get; set; }
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the cubic.
        /// </summary>
        /// <value>
        /// The cubic.
        /// </value>
        public double Cubic { get; set; }
        /// <summary>
        /// Gets or sets the barcode.
        /// </summary>
        /// <value>
        /// The barcode.
        /// </value>
        public String Barcode { get; set; }

        /// <summary>
        /// Quantity for the item
        /// </summary>
        public int Quantity { get; set; }

    }
}
