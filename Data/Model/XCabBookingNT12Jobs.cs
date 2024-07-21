using Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class XCabBookingNT12Jobs : XCabBooking
    {
        public int Distance { get; set; }

        public string ServiceSlot { get; set; }

        public string PodName { get; set; }

        public string GetFileCreationDetails()
        {
            string delimiter = "~";
			return Convert.ToString(Tplus_JobNumber) + delimiter + AccountCode + delimiter
				+ Ref1 + delimiter + Ref2 + delimiter + Caller + delimiter + ServiceSlot + delimiter
				+ DriverNumber + delimiter + FromSuburb.Trim() + delimiter + FromDetail1 + delimiter
				+ FromDetail2 + delimiter + FromDetail3 + delimiter + FromDetail4 + delimiter
				+ FromDetail5 + delimiter + ToSuburb.Trim() + delimiter + ToDetail1 + delimiter
				+ ToDetail2 + delimiter + ToDetail3 + delimiter + ToDetail4 + delimiter
				+ ToDetail5 + delimiter + (Distance <= 0 ? string.Empty : Convert.ToString(Distance)) + delimiter + (UploadDateTime.HasValue ? UploadDateTime.Value.ToString("yyyy-MM-ddTHH:mm") : String.Empty) + delimiter + (string.IsNullOrWhiteSpace(PickupArrive.ToString()) ? string.Empty : PickupArrive.ToString("yyyy-MM-ddTHH:mm")) + delimiter + (string.IsNullOrWhiteSpace(PickupComplete.ToString()) ? string.Empty : PickupComplete.ToString("yyyy-MM-ddTHH:mm")) + delimiter + (string.IsNullOrWhiteSpace(DeliveryArrive.ToString()) ? string.Empty : DeliveryArrive.ToString("yyyy-MM-ddTHH:mm")) + delimiter
				+ (string.IsNullOrWhiteSpace(DeliveryComplete.ToString()) ? string.Empty : DeliveryComplete.ToString("yyyy-MM-ddTHH:mm")) + delimiter + PodName;
        }
    }
}