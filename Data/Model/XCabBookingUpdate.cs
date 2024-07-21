using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
	public class XCabBookingUpdate
	{
		public int BookingId { get; set; }
		public int LoginId { get; set; }
		public int ExternalClientIntegrationId { get; set; }
		public string TrackingSchemaName { get; set; }
		public DateTime CcrPickupArrive { get; set; }
		public DateTime CcrPickupComplete { get; set; }
		public DateTime CcrDeliveryArrive { get; set; }
		public DateTime CcrDeliveryComplete { get; set; }
		public DateTime XCabPickupArrive { get; set; }
		public DateTime XCabPickupComplete { get; set; }
		public DateTime XCabDeliveryArrive { get; set; }
		public DateTime XCabDeliveryComplete { get; set; }
	}
}
