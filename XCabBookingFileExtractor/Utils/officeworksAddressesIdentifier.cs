
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCabBookingFileExtractor.Utils
{
    public class OfficeworksAddressesIdentifier
    {
        public static Core.Booking GetNswPickupAddress()
        {
            var addressDetails = new Core.Booking()
            {
                FromDetail1 = "Officeworks",
                FromDetail2 = "14-54 DENNISTOUN AVE",
                FromPostcode = "2161",
                FromSuburb = "YENNORA",
                AccountCode = "3OWNSW",
                StateId = "2"
            };

            return addressDetails;
        }
    }
}
