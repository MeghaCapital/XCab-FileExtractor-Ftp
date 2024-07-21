using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
	public class CcrXCabTrackingJob
	{
		public int BookingId { get; set; }
		public int LoginId { get; set; }
		public int StateId { get; set; }
		public string AccountCode { get; set; }
		public string JobNumber { get; set; }
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
		public DateTime JobBookingDay { get; set; }
		public DateTime JobAllocationDate { get; set; }
		public string FromDetail1 { get; set; }
		public string FromDetail2 { get; set; }
		public string FromDetail3 { get; set; }
		public string FromDetail4 { get; set; }
		public string FromDetail5 { get; set; }
		public string FromSuburb { get; set; }
		public string FromPostcode { get; set; }
		public string ToDetail1 { get; set; }
		public string ToDetail2 { get; set; }
		public string ToDetail3 { get; set; }
		public string ToDetail4 { get; set; }
		public string ToDetail5 { get; set; }
		public string ToSuburb { get; set; }
		public string ToPostcode { get; set; }

        public string Ref1 { get; set; }

        public string Ref2 { get; set; }

        public string ConsignmentNumber { get; set; }

        public int DriverId { get; set; }

        public string ServiceCode { get; set; }

        public string UserName { get; set; }

        public string TrackingFolderName { get; set; }

        public string PodName { get; set; }

		public string Remoteftphostname { get; set; }

		public string Remotetrackingfoldername { get; set; }

		public string RemoteFtpUserName { get; set; }

		public string RemoteFtpPassword { get; set; }
    }
}
