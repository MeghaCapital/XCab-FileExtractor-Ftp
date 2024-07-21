using System;

namespace Core
{
    public class TimeSlot
    {
        /// <summary>
        /// Gets or sets the account code.
        /// </summary>
        /// <value>
        /// The account code.
        /// </value>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// Gets or sets from suburb.
        /// </summary>
        /// <value>
        /// From suburb.
        /// </value>
        public int Duration { get; set; }

        public DateTime ClientRequiredPickupTime { get; set; }

        public DateTime ClientRequiredDeliveryTime { get; set; }
    }
}
