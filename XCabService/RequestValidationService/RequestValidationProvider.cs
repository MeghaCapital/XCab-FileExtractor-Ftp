using Data.Api.Bookings;
using System.Xml.Linq;

namespace XCabService.RequestValidationService
{
    public class RequestValidationProvider : IRequestValidationProvider
    {
        public bool IsRequestValid(BookingModel.OnlineBooking booking, bool IsPostcodeOptional = false)
        {
            bool valid = false;
            if (IsPostcodeOptional)
            {
                valid = !(string.IsNullOrEmpty(booking.AccountCode)
                              || string.IsNullOrEmpty(booking.ServiceCode)
                              || string.IsNullOrEmpty(booking.FromSuburb)
                              || string.IsNullOrEmpty(booking.ToSuburb)
                              || string.IsNullOrEmpty(booking.FromDetail1)
                              || string.IsNullOrEmpty(booking.ToDetail1)
                              );
            }
            else
            {
                valid = !(string.IsNullOrEmpty(booking.AccountCode)
                             || string.IsNullOrEmpty(booking.ServiceCode)
                             || string.IsNullOrEmpty(booking.FromSuburb)
                             || string.IsNullOrEmpty(booking.FromPostcode)
                             || string.IsNullOrEmpty(booking.ToSuburb)
                             || string.IsNullOrEmpty(booking.ToPostcode)
                             || string.IsNullOrEmpty(booking.FromDetail1)
                             || string.IsNullOrEmpty(booking.ToDetail1)
                             );
            }
            return valid;
        }

        public bool IsRequestValid(string accountCode, string serviceCode, string fromSuburb, string fromPostCode, string toSuburb, string toPostCode, string toAddressLine1, string fromAddressLine)
        {
            bool valid = false;
            valid = !(string.IsNullOrEmpty(accountCode)
                          || string.IsNullOrEmpty(serviceCode)
                          || string.IsNullOrEmpty(fromSuburb)
                          || string.IsNullOrEmpty(fromPostCode)
                          || string.IsNullOrEmpty(toSuburb)
                          || string.IsNullOrEmpty(toPostCode)
                          || string.IsNullOrEmpty(toAddressLine1)
                          || string.IsNullOrEmpty(fromAddressLine)
                          );
            return valid;
        }

        public bool IsBasfRequestValid(XElement manifest, bool IsPostcodeOptional = false)
        {
            bool valid = false;
            try
            {
                //check if suburbs and postcodes for pickup and delivery are provided
                XDocument xmlDoc = new(manifest);
                IEnumerable<XElement> e1edl20Element = xmlDoc.Descendants("E1ADRM1")
                    .Where(e1edl20 => (string)e1edl20.Element("PARTNER_Q") == "SP");
                XElement pickupSuburbAttribute = e1edl20Element.Descendants("CITY1").FirstOrDefault();
                string pickupSuburb = pickupSuburbAttribute.Value;

                XElement pickupPostcodeAttribute = e1edl20Element.Descendants("POSTL_COD1").FirstOrDefault();
                string pickupPostcode = pickupPostcodeAttribute.Value;
                XElement pickupAddressLine1Attribute = e1edl20Element.Descendants("NAME1").FirstOrDefault();
                string pickupAddressLine1 = pickupAddressLine1Attribute.Value;

                //get delivery suburb and postcode
                IEnumerable<XElement> e1edl20ElementDel = xmlDoc.Descendants("E1ADRM1")
                   .Where(e1edl20 => (string)e1edl20.Element("PARTNER_Q") == "WE");
                XElement deliverySuburbAttribute = e1edl20ElementDel.Descendants("CITY1").FirstOrDefault();
                string deliverySuburb = deliverySuburbAttribute.Value;
                XElement deliveryPostcodeAttribute = e1edl20ElementDel.Descendants("POSTL_COD1").FirstOrDefault();
                string deliveryPostcode = deliveryPostcodeAttribute.Value;

                XElement deliveryAddressLine1Attribute = e1edl20ElementDel.Descendants("NAME1").FirstOrDefault();
                string deliveryAddressLine1 = deliveryAddressLine1Attribute.Value;
                if (!IsPostcodeOptional)
                {
                    if (!string.IsNullOrEmpty(pickupSuburb) &&
                        !string.IsNullOrEmpty(pickupPostcode) &&
                        !string.IsNullOrEmpty(pickupAddressLine1) &&
                        !string.IsNullOrEmpty(deliverySuburb) &&
                        !string.IsNullOrEmpty(deliveryPostcode) &&
                        !string.IsNullOrEmpty(deliveryAddressLine1)
                        )
                    {
                        valid = true;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(pickupSuburb) &&
                        !string.IsNullOrEmpty(pickupAddressLine1) &&
                        !string.IsNullOrEmpty(deliverySuburb) &&
                        !string.IsNullOrEmpty(deliveryAddressLine1)
                        )
                    {
                        valid = true;
                    }
                }
            } catch (Exception e)
            {
                //TODO: Should this be logged?
            }
            return valid;
        }
    }
}
