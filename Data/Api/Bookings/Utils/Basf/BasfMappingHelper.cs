using Core;
using Core.Helpers;
using Data.Api.Bookings.Basf;
using Data.Api.Bookings.Tms;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace Data.Api.Bookings.Utils.Basf
{
    public static class BasfMappingHelper
    {       
        public static string GetStateFromConsignment(XElement element)
        {
            string state = null;
            XDocument xmlDoc = new(element);
            try
            {
                IEnumerable<XElement> e1edl20ElementDel = xmlDoc.Descendants("E1ADRM1").Where(e1edl20 => (string)e1edl20.Element("PARTNER_Q") == "WE");
                IEnumerable<XElement> stateElement = e1edl20ElementDel.Descendants("REGION");
                if (stateElement.FirstOrDefault() != null)
                    state = stateElement.FirstOrDefault().Value;
            }
            catch
            {
                //swallow and cater for this through null checks in the caller
            }
            return state;
        }
        public static TmsBookingRequest GetTmsBookingRequest(XElement element)
        {
            TmsBookingRequest tmsBookingRequest = null;
            try
            {
                XDocument xmlDoc = new(element);
                string state = null;
                IEnumerable<XElement> e1edl20ElementDel = xmlDoc.Descendants("E1ADRM1")
                   .Where(e1edl20 => (string)e1edl20.Element("PARTNER_Q") == "WE");
                IEnumerable<XElement> stateElement = e1edl20ElementDel.Descendants("REGION");
                if (stateElement.FirstOrDefault() != null)
                    state = stateElement.FirstOrDefault().Value;
                tmsBookingRequest = new TmsBookingRequest
                {
                    AccountCode = GetAccountCodeForState(state),
                    ServiceCode = "CPOD",
                    StateId = StateHelpers.GetStateId(state)
                };
                //filter e1arm1 elements where partner_q=SP
                IEnumerable<XElement> e1edl20Element = xmlDoc.Descendants("E1ADRM1")
                    .Where(e1edl20 => (string)e1edl20.Element("PARTNER_Q") == "SP");
                XElement pickupSuburbAttribute = e1edl20Element.Descendants("CITY1").FirstOrDefault();
                string pickupSuburb = pickupSuburbAttribute.Value;

                XElement pickupPostcodeAttribute = e1edl20Element.Descendants("POSTL_COD1").FirstOrDefault();
                string pickupPostcode = pickupPostcodeAttribute.Value;

                XElement pickupAddressLine1Attribute = e1edl20Element.Descendants("NAME1").FirstOrDefault();
                string pickupAddressLine1 = pickupAddressLine1Attribute.Value;

                XElement pickupAddressLine2Attribute = e1edl20Element.Descendants("STREET1").FirstOrDefault();
                string pickupAddressLine2 = pickupAddressLine2Attribute.Value;
                tmsBookingRequest.FromAddressDetail = new AddressDetail
                {
                    Suburb = pickupSuburb,

                    Postcode = pickupPostcode,

                    AddressLine1 = pickupAddressLine1,

                    AddressLine2 = pickupAddressLine2
                };
                //get delivery details
                //Shipping to address is contained within a segment with attribute = WE
                XElement deliverySuburbAttribute = e1edl20ElementDel.Descendants("CITY1").FirstOrDefault();
                string deliverySuburb = deliverySuburbAttribute.Value;
                XElement deliveryPostcodeAttribute = e1edl20ElementDel.Descendants("POSTL_COD1").FirstOrDefault();
                string deliveryPostcode = deliveryPostcodeAttribute.Value;
                XElement deliveryAddressLine1Attribute = e1edl20ElementDel.Descendants("NAME1").FirstOrDefault();
                string deliveryAddressLine1 = deliveryAddressLine1Attribute.Value;
                XElement deliveryAddressLine2Attribute = e1edl20ElementDel.Descendants("STREET1").FirstOrDefault();
                string deliveryAddressLine2 = deliveryAddressLine2Attribute.Value;
                tmsBookingRequest.ToAddressDetail = new AddressDetail
                {
                    Suburb = deliverySuburb,

                    Postcode = deliveryPostcode,

                    AddressLine1 = deliveryAddressLine1,

                    AddressLine2 = deliveryAddressLine2
                };
                //get extra delivery lines
                IEnumerable<XElement> e1txth8Element = xmlDoc.Descendants("E1TXTP8");
                //store as address line 3
                if (e1txth8Element.FirstOrDefault() != null)
                {
                    var addressLine3Elements = e1txth8Element.FirstOrDefault().Elements().Where(x => x.Name == "TDLINE");
                    if (addressLine3Elements.FirstOrDefault() != null)
                    {
                        string deliveryAddressLine3 = addressLine3Elements.FirstOrDefault().Value;
                        tmsBookingRequest.ToAddressDetail.AddressLine3 = deliveryAddressLine3;
                    }
                }
                string deliveryAddressLine4, ref1, ref2, totalWeight, caller;
                DateTime parsedAdvanceDateTime;
                //address line 4 is if we have another E1TXTP8 element
                if (e1txth8Element.Descendants("TDLINE") != null && e1txth8Element.Descendants("TDLINE").Count() > 1)
                {
                    deliveryAddressLine4 = e1txth8Element.Descendants("TDLINE").ElementAt(1).Value;
                    tmsBookingRequest.ToAddressDetail.AddressLine4 = deliveryAddressLine4;
                }
                //get Ref1
                IEnumerable<XElement> e1edl20Elements = xmlDoc.Descendants("E1EDL20");
                IEnumerable<XElement> vbelnElements = e1edl20Elements.Descendants("VBELN");
                if (vbelnElements.FirstOrDefault() != null)
                {
                    ref1 = vbelnElements.FirstOrDefault().Value;
                    tmsBookingRequest.Ref1 = ref1;
                }
                //get Ref2
                //ref2 is stored under the delivery segment only, pickup segment has different value for partner ID
                XElement ref2Attribute = e1edl20ElementDel.Descendants("PARTNER_ID").FirstOrDefault();
                if (ref2Attribute != null)
                {
                    ref2 = ref2Attribute.Value;
                    tmsBookingRequest.Ref2 = ref2;
                }
                //get total weight

                IEnumerable<XElement> totalWeightElement = xmlDoc.Descendants("E1EDL20").Descendants("BTGEW");
                if (totalWeightElement.FirstOrDefault() != null)
                {
                    totalWeight = totalWeightElement.FirstOrDefault().Value;
                    tmsBookingRequest.TotalWeight = totalWeight;
                }
                //get caller
                IEnumerable<XElement> routeElement = xmlDoc.Descendants("E1EDL20").Descendants("ROUTE");
                if (routeElement.FirstOrDefault() != null)
                {
                    caller = routeElement.FirstOrDefault().Value;
                    tmsBookingRequest.Caller = caller;
                }
                //get advance date time
                IEnumerable<XElement> advanceDateTimeElement = xmlDoc.Descendants("E1EDT13").Descendants("NTAN");
                if (advanceDateTimeElement.FirstOrDefault() != null)
                {
                    string advanceDateTime = advanceDateTimeElement.FirstOrDefault().Value;
                    //format for advance date is yyyymmdd
                    parsedAdvanceDateTime = DateTime.ParseExact(advanceDateTime, "yyyymmdd", CultureInfo.InvariantCulture);
                    tmsBookingRequest.AdvanceDateTime = parsedAdvanceDateTime;
                }
                //get items

                IEnumerable<XElement> e1Edl24Elements = xmlDoc.Descendants("E1EDL24");
                var itemCount = e1Edl24Elements.Count();
                var tmsRequestBookingItems = new List<TmsRequestBookingItem>();

                for (int i = 0; i < itemCount; i++)
                {
                    var itemSegment = e1Edl24Elements.ElementAt(i);
                    if (itemSegment != null)
                    {
                        var tmsRequestBookingItem = new TmsRequestBookingItem();
                        string description, barcode, quantity, weight, volume;
                        var descriptionElement = itemSegment.Descendants("ARKTX");
                        if (descriptionElement.FirstOrDefault() != null)
                        {
                            description = descriptionElement.FirstOrDefault().Value;
                            tmsRequestBookingItem.Description = description;
                        }
                        var barcodeElement = itemSegment.Descendants("MATNR");
                        if (barcodeElement.FirstOrDefault() != null)
                        {
                            barcode = barcodeElement.FirstOrDefault().Value;
                            tmsRequestBookingItem.Barcode = barcode;
                        }
                        var quantityElement = itemSegment.Descendants("LGMNG");
                        if (quantityElement.FirstOrDefault() != null)
                        {
                            quantity = quantityElement.FirstOrDefault().Value;
                            if (!string.IsNullOrEmpty(quantity))
                                tmsRequestBookingItem.Quantity = (int)Convert.ToDouble(quantity);
                        }
                        var weightElement = itemSegment.Descendants("NTGEW");
                        if (weightElement.FirstOrDefault() != null)
                        {
                            weight = weightElement.FirstOrDefault().Value;
                            if (!string.IsNullOrEmpty(weight))
                                tmsRequestBookingItem.Weight = Convert.ToDouble(weight);
                        }
                        var volumeElement = itemSegment.Descendants("VOLUM");
                        if (volumeElement.FirstOrDefault() != null)
                        {
                            volume = volumeElement.FirstOrDefault().Value;
                            if (!string.IsNullOrEmpty(volume))
                                tmsRequestBookingItem.Cubic = Convert.ToDouble(volume);
                        }
                        tmsRequestBookingItems.Add(tmsRequestBookingItem);
                    }
                }
                tmsBookingRequest.Items = tmsRequestBookingItems;
            }
            catch (Exception e)
            {
                Logger.Log("Exception occurred when creating TmsBookingRequest for BASF manifest for SOAP request:" + e.Message, nameof(BasfMappingHelper));
            }
            return tmsBookingRequest;
        }
       
        private static string GetAccountCodeForState(string state)
        {
            string accountCode = "";
            switch (state.ToUpper())
            {
                case "VIC":
                    accountCode = "3BASPERM";
                    break;
                case "QLD":
                    accountCode = "BASFCQLD";
                    break;
                case "SA":
                    accountCode = "3BASFSA";
                    break;
                case "WA":
                    accountCode = "BASFCWA";
                    break;
                case "NSW":
                    accountCode = "ZZTPTEST";
                    break;
                default:
                    break;

            }
            return accountCode;
        }
        public static string GetReference1(XElement manifest)
        {
            var xmlDoc = new XDocument(manifest);
            var ref1 = "";
            //get Ref1
            IEnumerable<XElement> e1edl20Elements = xmlDoc.Descendants("E1EDL20");
            IEnumerable<XElement> vbelnElements = e1edl20Elements.Descendants("VBELN");
            if (vbelnElements.FirstOrDefault() != null)
            {
                ref1 = vbelnElements.FirstOrDefault().Value;                
            }
            
            return ref1;
        }
        public static string GetReference2(XElement manifest)
        {
            var xmlDoc = new XDocument(manifest);
            var ref2 = "";
            //ref2 is stored under the delivery segment only, pickup segment has different value for partner ID
            //get Ref2
            //ref2 is stored under the delivery segment only, pickup segment has different value for partner ID
            IEnumerable<XElement> e1edl20ElementDel = xmlDoc.Descendants("E1ADRM1").Where(e1edl20 => (string)e1edl20.Element("PARTNER_Q") == "WE");
            XElement ref2Attribute = e1edl20ElementDel.Descendants("PARTNER_ID").FirstOrDefault();
            if (ref2Attribute != null)
            {
                ref2 = ref2Attribute.Value;                
            }
            return ref2;
        }
    }
}
