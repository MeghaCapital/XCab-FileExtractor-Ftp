
using Core;
using Data.Entities.Remark;
using Data.Entities.Sundries;
using System;
using System.Collections.Generic;

namespace Data.Model.ConsolidatedBooking
{
    public class BaseBooking
    {
        /// <summary>
        ///     Pickup leg
        /// </summary>
        public Leg Pickup { get; set; }

        /// <summary>
        ///     Delivery leg
        /// </summary>
        public Leg Delivery { get; set; }

        /// <summary>
        ///     Reference 1 that will be attached with the booking - these could be client references or an unique id that
        ///     reconciles this job at the clients end
        /// </summary>
        public string? Reference1 { get; set; } 

        /// <summary>
        ///     Reference 2 that will be attached with the booking - these could be client references or an unique id that
        ///     reconciles this job at the clients end
        /// </summary>
        public string? Reference2 { get; set; }

        /// <summary>
        ///     Total Items attached with the booking
        /// </summary>
        public string? TotalItems { get; set; }

        /// <summary>
        ///     Total Weight of all items that are attached with the booking
        /// </summary>
        public string? TotalWeight { get; set; }

        /// <summary>
        ///     Total Volume in CC that will be attached with the booking
        /// </summary>
        public string? TotalCubic { get; set; }

        /// <summary>
        ///     Is there authority to leave, if so what are the instructions.
        /// </summary>
        public AuthorityToLeave? ATL { get; set; }

        /// <summary>
        ///     Contact Information to be attached to the booking
        /// </summary>
        public RequestBookingContactInformation? BookingContactInformation { get; set; }

        /// <summary>
        ///     Contact Information attached to the tracking
        /// </summary>
        public TrackingContactInformation? TrackingContactInformation { get; set; }

        /// <summary>
        ///     Used to specify if the booking needs to be sent to a particular driver. If no value is supplied, Capital
        ///     will use the best available driver for doing the job
        /// </summary>
        public int? DriverNumber { get; set; }

        /// <summary>
        ///     Advance date time that will be used to create bookings for a future date, if this field is not provided
        ///     the bookings will be created for the same day the request is received
        /// </summary>
        public DateTime? AdvanceDateTime { get; set; }

        /// <summary>
        ///     Service Code that will be used to create the booking
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Use special instructions if provided
        /// </summary>
        public string? SpecialInstructions { get; set; }

        /// <summary>
        ///     Consignment Number - currently not in use, please use reference 1 or reference 2
        /// </summary>
        public string? ConsignmentNumber { get; set; }

        /// <summary>
        ///     Caller field identifies the name of the person who made the booking.
        /// </summary>
        public string? Caller { get; set; }

        /// <summary>
        ///     Route code for run.
        /// </summary>
        public string? RouteCode { get; set; }

        /// <summary>
        /// Pickup Required Date Time
        /// </summary>
        public DateTime? PickupRequiredDateTime { get; set; }

        /// <summary>
        /// Delivery Required Date Time
        /// </summary>
        public DateTime? DeliveryRequiredDateTime { get; set; }

        /// <summary>
        ///     List of Items that need to be attached to the booking
        /// </summary>
        public virtual List<BaseBookingItem>? BookingItems { get; set; }

        /// <summary>
        ///     List of Sundries that need to be attached to the booking
        /// </summary>
        public virtual List<Sundry>? Sundries { get; set; }

        /// <summary>
        ///     List of Remarks that need to be attached to the booking
        /// </summary>
        public virtual List<Remark>? Remarks { get; set; }
        /// <summary>
        /// Whether DG documents for the delivery is included with the goods
        /// </summary>
        public virtual bool? TransportDocumentWillAccompanyLoad { get; set; }

        /// <summary>
        /// Packaging of the goods are done with accordance to ADG 7.4 code
        /// </summary>
        public virtual bool? PackagedInAccordanceWithAdg7_4 { get; set; }
    }
}