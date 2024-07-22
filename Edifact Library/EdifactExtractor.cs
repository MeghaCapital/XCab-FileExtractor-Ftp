using Core;
using Core.Google;
using Core.Helpers;
using Data;
using EDIFACT;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Edifact_Library
{
    public class ConsolidatedJob
    {
        public List<Booking> ConsolidatedBookings { get; set; }
        public IEnumerable<IGrouping<string, Booking>> GroupedJobs { get; set; }
    }

    public class EdifactExtractor
    {
        /**
         * This field is useful as GWA cannot send individual item weights - so we average the total weight of the
         * consignment across all indivdual bookings
         */
        public static bool GwaAverageWeightAcrossItems = true;

        /*        public static int GetStateId(string key)
                {
                    var stateId = -1;
                    if (key == null || key.Length <= 0)
                        return stateId;
                    if (key.StartsWith("2"))
                        stateId = 2;
                    else if (key.StartsWith("3"))
                        stateId = 1;
                    else if (key.StartsWith("4"))
                        stateId = 3;
                    else if (key.StartsWith("5"))
                        stateId = 4;
                    else if (key.StartsWith("6"))
                        stateId = 5;
                    else
                        stateId = -1;
                    return stateId;
                }*/

        public static SegmentCollection[] AssignAverageWeights(SegmentCollection[] segments)
        {
            var consignmentItemDictionary = new Dictionary<string, int>();
            foreach (var segment in segments)
            {
                //iterate through every segment (i.e. line) and extract individual tags
                double totalWeight = 0;
                double totalVolume = 0;
                var connote = "";
                foreach (Segment seg in segment)
                    if (seg.Name == "MEA")
                    {
                        var fc = seg.Fields;
                        if (fc.Item(2).Value == "KGM" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "G" &&
                            totalWeight == 0)
                        {
                            var weight = "";
                            weight = fc.Item(3).Value;
                            Debug.WriteLine("Total Weight" + weight);
                            totalWeight = double.Parse(weight);
                            //booking.TotalWeight = weight;
                        }
                        else if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ" &&
                                 totalVolume == 0)
                        //   &&!totalVolumeFound)
                        //else if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ")
                        {
                            var volume = "";
                            volume = fc.Item(3).Value;
                            Debug.WriteLine("Total Volume" + volume);
                            totalVolume = double.Parse(volume);
                            //  booking.TotalVolume = fc.Item(3).Value;
                            // totalVolumeFound = true;
                        }
                        //find the number of items attached with the booking
                    }
                    else if (seg.Name == "RFF")
                    {
                        var fc = seg.Fields;
                        if (fc.Item(0).Value == "CN")
                            connote = fc.Item(1).Value;
                    }
                    else if (seg.Name == "CNT")
                    {
                        var fc = seg.Fields;
                        if (fc.Item(0).Value == "11")
                        {
                            var items = fc.Item(1).Value;
                            consignmentItemDictionary[connote] = Convert.ToInt32(items);
                        }
                    }
            }
            //once the dictionary has the total number of items per consignment we can distribute the weight & volume accordingly
            foreach (var segment in segments)
            {
                var connote = "";
                double totalVolume = 0;
                double totalWeight = 0;
                foreach (Segment seg in segment)
                    if (seg.Name == "MEA")
                    {
                        var fc = seg.Fields;
                        if (fc.Item(2).Value == "KGM" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "G" &&
                            totalWeight == 0)
                        {
                            var weight = "";
                            weight = fc.Item(3).Value;
                            //                          Debug.WriteLine("Total Weight" + weight);
                            totalWeight = double.Parse(weight);
                            //booking.TotalWeight = weight;
                        }
                        else if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ" &&
                                 totalVolume == 0)
                        //   &&!totalVolumeFound)
                        //else if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ")
                        {
                            var volume = "";
                            volume = fc.Item(3).Value;
                            //                            Debug.WriteLine("Total Volume" + volume);
                            totalVolume = double.Parse(volume);
                            //  booking.TotalVolume = fc.Item(3).Value;
                            // totalVolumeFound = true;
                        }
                        if (fc.Item(2).Value == "KGM" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "AAB")
                        {
                            var weight = "";
                            weight = fc.Item(3).Value;
                            if (double.Parse(weight) == 0)
                                if (connote.Length > 0)
                                {
                                    var totalItems = consignmentItemDictionary[connote];
                                    var avgWeight = totalWeight / totalItems * 1.0;
                                    //Debug.Write("Average Weight Set:" + avgWeight);
                                    fc.Item(3).Value = avgWeight.ToString("#.##");
                                }
                            // item.Weight = Convert.ToDouble(weight);
                            //Debug.WriteLine("Item Weight" + weight);
                        }
                        if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ")
                        {
                            var volume = "";
                            volume = fc.Item(3).Value;
                            if (double.Parse(volume) == 0)
                                if (connote.Length > 0)
                                {
                                    var totalItems = consignmentItemDictionary[connote];
                                    var avgVolume = totalVolume / totalItems * 1.0;
                                    //    Debug.Write("Average Volume Set:" + avgVolume);
                                    fc.Item(3).Value = avgVolume.ToString("#.##");
                                }
                            //item.Cubic = Convert.ToDouble(volume);
                            //Debug.WriteLine("Item Volume" + volume);
                        }
                        //find the number of items attached with the booking
                    }
                    else if (seg.Name == "RFF")
                    {
                        var fc = seg.Fields;
                        if (fc.Item(0).Value == "CN")
                            connote = fc.Item(1).Value;
                    }
            }
            return segments;
        }


        public static List<Booking> DistributeTotalWeightVolume(List<Booking> lstBooking)
        {
            var lstProcessedBookings = new List<Booking>();
            foreach (var booking in lstBooking)
            {
                //for each booking extract the items
                var totalWeight = booking.lstItems.Sum(x => x.Weight);
                var totalVolume = booking.lstItems.Sum(x => x.Cubic);
                //if (totalWeight != null)
                booking.TotalWeight = totalWeight.ToString();
                //if (totalVolume != null)
                booking.TotalVolume = totalVolume.ToString();
                lstProcessedBookings.Add(booking);
            }
            return lstProcessedBookings;
        }

        public static Booking DistributeTotalWeightVolume(Booking booking)
        {
            //for each booking extract the items
            var totalWeight = booking.lstItems.Sum(x => x.Weight);
            var totalVolume = booking.lstItems.Sum(x => x.Cubic);
            // if (totalWeight != null)
            booking.TotalWeight = totalWeight.ToString();
            //if (totalVolume != null)
            booking.TotalVolume = totalVolume.ToString();
            return booking;
        }

        public static ConsolidatedJob ConsolidateB2CBookings(List<Booking> lstBooking)
        {
            var consolidatedJobs = new ConsolidatedJob();

            try
            {
                var consolidatedBookings = lstBooking.GroupBy(x => x.ToDetail2).Select(
                       x => new Booking
                       {
                           AccountCode = x.ToList()[0].AccountCode,
                           //Ref1 = x.ToList()[0].Ref1,
                           Ref1 = x.Count().ToString() + " Invoices",
                           Ref2 = x.ToList()[0].ToDetail1.Replace('/', ' '), //store customer name as reference
                           Caller = x.ToList()[0].Caller,
                           FromDetail1 = x.ToList()[0].FromDetail1,
                           FromDetail2 = x.ToList()[0].FromDetail2,
                           FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                           FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                           FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                           FromSuburb = x.ToList()[0].FromSuburb,
                           FromPostcode = x.ToList()[0].FromPostcode,
                           ToDetail1 = x.ToList()[0].ToDetail1,
                           ToDetail2 = x.ToList()[0].ToDetail2,
                           ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                           ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                           ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                           ToSuburb = x.ToList()[0].ToSuburb,
                           ToPostcode = x.ToList()[0].ToPostcode,
                           StateId = x.ToList()[0].StateId,
                           ServiceCode = x.ToList()[0].ServiceCode,
                           PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                           DriverNumber = x.ToList()[0].DriverNumber,
                           DespatchDateTime = x.ToList()[0].DespatchDateTime,
                           LoginDetails = new LoginDetails
                           {
                               Id = x.ToList()[0].LoginDetails.Id
                           },
                           UploadDateTime = x.ToList()[0].UploadDateTime,
                           TotalItems = x.Sum(c => c.lstItems.Count).ToString(),
                           TotalWeight = x.Sum(c => Convert.ToDouble(c.TotalWeight)).ToString(),
                           TotalVolume = x.Sum(c => Convert.ToDouble(c.TotalVolume)).ToString(),
                           lstItems = x.SelectMany(y => y.lstItems).ToList(),
                           Notification = x.ToList()[0].Notification,
                           lstExtraFields = x.SelectMany(y => y.lstExtraFields).ToList(),
                       }
                   );

                if (consolidatedBookings.Any())
                    consolidatedJobs.ConsolidatedBookings = consolidatedBookings.ToList();

                if (lstBooking.Any())
                    consolidatedJobs.GroupedJobs = lstBooking.GroupBy(x => x.ToDetail2);
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception occurred while consolidating B2C bookings. Exception:" +
                    e.Message, "EdifactExtractor");
                return null;
            }

            return consolidatedJobs;
        }

        public static AddressDetail GetCapitalWarehouseAddress(int StateId)
        {
            var wareHouseAddress = new AddressDetail();

            try
            {
                switch (StateId)
                {
                    case 1:
                        wareHouseAddress.AddressLine1 = "Capital Transport";
                        wareHouseAddress.AddressLine2 = "75-85 Nantilla Rd";
                        wareHouseAddress.Suburb = "Clayton North";
                        wareHouseAddress.Postcode = "3168";
                        break;
                    case 2:
                        wareHouseAddress.AddressLine1 = "Capital Transport";
                        wareHouseAddress.AddressLine2 = "23-29 South St";
                        wareHouseAddress.AddressLine3 = "Unit 3";
                        wareHouseAddress.Suburb = "Rydalmere";
                        wareHouseAddress.Postcode = "2116";
                        break;
                    case 3:
                        wareHouseAddress.AddressLine1 = "Capital Transport";
                        wareHouseAddress.AddressLine2 = "420 Nudgee Rd";
                        wareHouseAddress.AddressLine3 = "Building 19";
                        wareHouseAddress.Suburb = "Hendra";
                        wareHouseAddress.Postcode = "4011";
                        break;
                    case 4:
                        wareHouseAddress.AddressLine1 = "Capital Transport";
                        wareHouseAddress.AddressLine2 = "126 - 130 Richmond Road";
                        wareHouseAddress.Suburb = "Marleston";
                        wareHouseAddress.Postcode = "5033";
                        break;
                    case 5:
                        wareHouseAddress.AddressLine1 = "Capital Transport";
                        wareHouseAddress.AddressLine2 = "959 Abernethy Road";
                        wareHouseAddress.Suburb = "High Wycombe";
                        wareHouseAddress.Postcode = "6057";
                        break;
                }

            }
            catch (Exception)
            {
                //Logger.SendHtmlEmail(
                //    "Error occurred while extracting warehouse details for State " + StateId + "." + e.Message, "EdifactExtractor");
                return null;
            }

            return wareHouseAddress;
        }

        public static List<Booking> Parse(string ediFilename, LoginDetails ld, int mappedStateId, int driverNumber,
            bool createdOrderedList = false,
            bool createCsvDumpFile = false,
            bool isB2CFile = false)
        {
            //var mappedStateId = -1;
            // var driverNumber = -1;
            var lstBooking = new List<Booking>();

#if DEBUG
            driverNumber = 1902;
#endif
            try
            {
                var msg = new EDIMessage(File.ReadAllText(ediFilename), true);

                // var text = File.ReadAllText(ediFilename);

                var segmentCollections = msg.GetAllSegmentCollections(ediFilename);
                if (GwaAverageWeightAcrossItems)
                    segmentCollections = AssignAverageWeights(segmentCollections);
                var deliveryDictionary = new Dictionary<string, Booking>();


                foreach (var segment in segmentCollections)
                {
                    var deliveryDocketNumber = "";
                    var booking = new Booking { StateId = mappedStateId.ToString() };
                    //every booking will have the same state id
                    //assign driver number if there exists a route for the driver
                    if (driverNumber != -1)
                        booking.DriverNumber = driverNumber;
                    else
                        try
                        {
                            //raise a message that there is no Driver assigned to this route - possibly a new driver needs to be setup in XCAB
                            var message =
                                string.Format(
                                    @"Received A File where there is No Route setup in XCAB Db (tablename: xCabDriverRoutes)\n"
                                    + "EDI Filename=" + ediFilename
                                    + ",StateId=" + mappedStateId
                                    + ",Username=" + ld.UserName
                                    + "\n\n"
                                    +
                                    "This file will be created with No Driver Information in our TMS. Operators will have to assign drivers against the jobs manually"
                                    +
                                    "Please create a new Driver Route in the DB:XCAB (tablename: xCabDriverRoutes) for the required state, route & user"
                                );
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception occurred while sending a notification email about driver routes. Exception:" +
                                e.Message, "EdifactExtractor");
                        }
                    var itemStartTag = false;
                    var totalVolumeFound = false;
                    Item item = null;
                    foreach (Segment o in segment)
                    {
                        if (o.Name.ToUpper() == "NAD")
                        {
                            /**
                             * Example

                            Shipper address details
                            NAD+SH+100583::91+FREIGHT ABC+FREIGHT ABC+PO BOX 777+NOWRA++1550'

                            Ship-To Address (Can be either the end customer or a 3PL provider)
                            NAD+ST+H10028::91+ALLFREIGHT+24 SHARP COURT+CAVAN+8262776++5094'

                            Party who supplies goods and/or services
                            NAD+SU+C15::91+Starion Tapware Pty Ltd - TEST 200+Starion Tapware Pty Ltd - TEST 200+Locked Bag 20+Epping++2121’

                            Party to whom merchandise and/or service is sold if different from delivery party.
                            NAD+BY+C12820::91+HW DISTRIBUTION+HW DISTRIBUTION LIMITED+88 BLUE ROAD+ILLAWARRA ++6002

                            Ultimate customer address details (Party to where the goods should be delivered)
                            NAD+UD+DA001::91+GREEN PLUMBING +HW DISTRIBUTION LIMITED+85 YELLOW STREET+ILLAWARRA++6001+AU'

                             * 
                             * */
                            var fc = o.Fields;
                            switch (fc.Item(0).Value.ToUpper())
                            {
                                case "SU":
                                case "SF":
                                    //As from above SH is the shippers address detail or in other words PU Address fields
                                    var pickupAddressLine1 = "";
                                    var pickupAddressLine2 = "";
                                    var pickupAddressLine3 = "";
                                    var pickupPostcode = "";
                                    pickupAddressLine1 = fc.Item(5).Value;
                                    pickupAddressLine2 = fc.Item(6).Value;
                                    pickupAddressLine3 = fc.Item(7).Value;
                                    pickupPostcode = fc.Item(9).Value;
                                    try
                                    {
                                        //on certain files the postcodes were missing
                                        if (pickupPostcode.Length == 0)
                                            pickupPostcode = fc.Item(12).Value;
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.Log(
                                            "Exception Occurred while using EDIFACT Parsing to extract PU Postcode, Element=NAD+SH, fileName:" +
                                            ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                    }
                                    booking.FromDetail1 = pickupAddressLine1.Replace(',', ' ');
                                    booking.FromDetail2 = pickupAddressLine2.Replace(',', ' ');
                                    booking.FromSuburb = pickupAddressLine3.Replace(',', ' ');
                                    booking.FromPostcode = pickupPostcode;
                                    break;
                                case "UD":
                                    //UD is the ultimate delivery section:
                                    //Debug.Write("Found Delivery Address");
                                    //NAD+UD+DA001::91++19 Merivale St+South Brisbane++4101+AU'
                                    //fc.Item(3)= 91: 91+<4th Index:Add Line 1>+<5th Index:Add Line 2>+<6th Index:Add Line 3>+<7th Index:Suburb>+<8th Index:Postcode>+<9th Index:AU>'

                                    var partyNameLine1 = "";
                                    var partyNameLine2 = "";

                                    var deliveryAddressLine1 = "";
                                    var deliveryAddressLine2 = "";
                                    //const string deliveryAddressLine3 = "";

                                    var deliverySuburb = "";
                                    var deliveryPostcode = "";

                                    partyNameLine1 = fc.Item(4).Value;
                                    partyNameLine2 = fc.Item(5).Value;

                                    deliveryAddressLine1 = fc.Item(6).Value;
                                    deliveryAddressLine2 = fc.Item(7).Value; //address line 2 is the suburb
                                    deliverySuburb = fc.Item(7).Value;
                                    //deliveryAddressLine4 = fc.Item(9).Value;
                                    deliveryPostcode = fc.Item(9).Value; //this is the postcode
                                    try
                                    {
                                        //on certain files the postcodes were missing
                                        if (deliveryPostcode.Length == 0 && o.Fields.Count >= 13)
                                            deliveryPostcode = fc.Item(12).Value;
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.Log(
                                            "Exception Occurred while using EDIFACT Parsing to extract DEL Postcode, Element=NAD+UD,fileName:" +
                                            ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                    }
                                    //for TMS we need the customer name as the address line 1
                                    //booking.ToDetail1 = (partyNameLine1 + partyNameLine2).Replace(',', ' ');
                                    //for TMS we need the customer name as the address line 1

                                    //Change required to support customer name 1 that comes in the EDI file instead of Customer name line 2 (partLine2)
                                    //we check if customer name line 1 has a value, if found we just use that otherwise we fall back and use customer name line 2
                                    if (isB2CFile)
                                    {
                                        if (string.IsNullOrEmpty(partyNameLine2))
                                            booking.ToDetail1 = partyNameLine1.Replace(',', ' ').Trim();
                                        else
                                            booking.ToDetail1 = partyNameLine2.Replace(',', ' ').Trim();
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(partyNameLine1))
                                            booking.ToDetail1 = partyNameLine2.Replace(',', ' ').Trim();
                                        else
                                            booking.ToDetail1 = partyNameLine1.Replace(',', ' ').Trim();
                                    }
                                    if (!string.IsNullOrEmpty(deliveryAddressLine1))
                                        booking.ToDetail2 = deliveryAddressLine1.Replace(',', ' ');
                                    if (!string.IsNullOrEmpty(deliveryAddressLine2))
                                        booking.ToDetail3 = deliveryAddressLine2.Replace(',', ' ');
                                    booking.ToSuburb = deliverySuburb;
                                    booking.ToPostcode = deliveryPostcode;
                                    break;
                                case "ST":
                                    //ST is the Ship To (most cases is the ultimate recipient) but can be 3PL provider
                                    //we will use values from ST only when UD is not available
                                    var deliveryAddressLineSt1 = "";
                                    var deliveryAddressLineSt2 = "";
                                    var deliveryAddressLineSt3 = "";
                                    var deliveryPostcodeSt = "";
                                    deliveryAddressLineSt1 = fc.Item(5).Value;
                                    deliveryAddressLineSt2 = fc.Item(6).Value;
                                    deliveryAddressLineSt3 = fc.Item(7).Value;
                                    deliveryPostcodeSt = fc.Item(9).Value;
                                    try
                                    {
                                        //on certain files the postcodes were missing
                                        if (deliveryPostcodeSt.Length == 0)
                                            deliveryPostcodeSt = fc.Item(12).Value;
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.Log(
                                            "Exception Occurred while using EDIFACT Parsing to extract DEL Postcode, Element=NAD+ST,fileName:" +
                                            ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                    }
                                    if (string.IsNullOrEmpty(booking.ToDetail1))
                                        booking.ToDetail1 = deliveryAddressLineSt1.Replace(',', ' ');
                                    if (string.IsNullOrEmpty(booking.ToDetail2))
                                        booking.ToDetail2 = deliveryAddressLineSt2.Replace(',', ' ');
                                    if (string.IsNullOrEmpty(booking.ToDetail3))
                                        booking.ToSuburb = deliveryAddressLineSt3.Replace(',', ' ');
                                    if (string.IsNullOrEmpty(booking.ToPostcode))
                                        booking.ToPostcode = deliveryPostcodeSt;
                                    break;
                            }
                        }
                        if (o.Name.ToUpper() == "COM")
                        {
                            //this is the contact email address: COM+abaltic@gwagroup.com.au:EM' and UNS mobile :COM+0400123456:TE'
                            try
                            {
                                var comValue = o.Fields.Item(0).Value;
                                if (o.Fields.Item(1).Value == "EM" && !string.IsNullOrEmpty(comValue) && comValue.Contains("@"))
                                {
                                    if (booking.Notification == null)
                                    {
                                        var notification = new Notification
                                        {
                                            EmailAddress = comValue
                                        };
                                        booking.Notification = notification;
                                    }
                                    else
                                    {
                                        booking.Notification.EmailAddress = comValue;
                                    }
                                }
                                else if (o.Fields.Item(1).Value == "TE")
                                {
                                    if (booking.Notification == null)
                                    {
                                        var notification = new Notification
                                        {
                                            SMSNumber = comValue.Trim().Replace(" ", string.Empty)
                                        };
                                        booking.Notification = notification;
                                    }
                                    else
                                    {
                                        booking.Notification.SMSNumber = comValue.Trim().Replace(" ", string.Empty);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.Log(
                               "Exception Occurred while using EDIFACT Parsing to extract email notifications, Element=COM, fileName:" +
                               ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                            }
                        }
                        if (o.Name.ToUpper() == "RFF")
                            try
                            {
                                var fc = o.Fields;
                                switch (fc.Item(0).Value.ToUpper())
                                {
                                    case "CN":
                                        if (fc.Item(1) != null)
                                            booking.Ref1 = fc.Item(1).Value;
                                        break;
                                    case "ADE":
                                        if (fc.Item(1) != null)
                                            booking.AccountCode = fc.Item(1).Value;
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.Log(
                                    "Exception Occurred while using EDIFACT Parsing to extract References, Element=REF, fileName:" +
                                    ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                            }
                        if (o.Name.ToUpper() == "GIR")
                        {
                            var fc = o.Fields;
                            try
                            {
                                if (fc.Item(0).Value == "3")
                                {
                                    //extract purchase order
                                    var purchaseOrder = fc.Item(1).Value;
                                    if (!string.IsNullOrWhiteSpace(purchaseOrder))
                                    {
                                        var extraField = new ExtraFields { Key = "Purchase Order Number", Value = purchaseOrder.Trim() };
                                        if (booking.lstExtraFields == null)
                                        {
                                            var extraFields = new List<ExtraFields>
                                                {
                                                    extraField
                                                };
                                            booking.lstExtraFields = extraFields;
                                        }
                                        else
                                        {
                                            booking.lstExtraFields.Add(extraField);
                                        }
                                    }

                                }
                            }
                            catch (Exception e)
                            {
                                Logger.Log(
                                        "Exception Occurred while using EDIFACT Parsing to extract PO order number, Element=GIR+3, fileName:" +
                                        ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                            }
                        }
                        if (o.Name.ToUpper() == "DTM")
                        {
                            var fc = o.Fields;
                            if (fc.Item(0).Value == "11")
                                try
                                {
                                    var dt = DateTime.ParseExact(fc.Item(1).Value, "yyyyMMddHHmm",
                                        CultureInfo.InvariantCulture);
                                    booking.DespatchDateTime = dt;
                                }
                                catch (Exception e)
                                {
                                    Logger.Log(
                                        "Exception Occurred while using EDIFACT Parsing to extract Despatch date time, Element=DTM+11, fileName:" +
                                        ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                }
                        }

                        if (o.Name.ToUpper() == "CPS" && !itemStartTag)
                        {
                            itemStartTag = true;
                            item = new Item();
                        }
                        else if (o.Name.ToUpper() == "CPS" && itemStartTag)
                        {
                            itemStartTag = false;
                        }

                        if (o.Name.ToUpper() == "MEA")
                        {
                            var fc = o.Fields;
                            if (item != null)
                            {
                                if (fc.Item(2).Value.ToUpper() == "KGM" && fc.Item(0).Value.ToUpper() == "PD" &&
                                    fc.Item(1).Value.ToUpper() == "AAB")
                                    try
                                    {
                                        var weight = "";
                                        weight = fc.Item(3).Value;
                                        item.Weight = Convert.ToDouble(weight);
                                    }
                                    catch (Exception e)
                                    {
                                        //if there is an issue parsing the weight we just assign 0
                                        item.Weight = 0;
                                        Logger.Log(
                                            "Exception Occurred while using EDIFACT Parsing to extract Item Weight, Element=MEA+PD+AAB+KGM fileName:" +
                                            ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                    }
                                if (fc.Item(2).Value.ToUpper() == "MTQ" && fc.Item(0).Value.ToUpper() == "PD" &&
                                    fc.Item(1).Value.ToUpper() == "ABJ")
                                    try
                                    {
                                        var volume = "";
                                        volume = fc.Item(3).Value;
                                        item.Cubic = volume.Length > 0 ? Convert.ToDouble(volume) : 0;
                                    }
                                    catch (Exception)
                                    {
                                        //if there is an issue parsing the volume we just assign 0
                                        item.Cubic = 0;
                                    }
                            }
                            if (fc.Item(2).Value.ToUpper() == "KGM" && fc.Item(0).Value.ToUpper() == "PD" &&
                                fc.Item(1).Value.ToUpper() == "G")
                                try
                                {
                                    var weight = "";
                                    weight = fc.Item(3).Value;
                                    booking.TotalWeight = weight;
                                }
                                catch (Exception e)
                                {
                                    booking.TotalWeight = "0";
                                    Logger.Log(
                                        "Exception Occurred while using EDIFACT Parsing to extract Total Weight,ELement=MEA+PD+G+KGM, fileName:" +
                                        ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                }
                            else if (fc.Item(2).Value.ToUpper() == "MTQ" && fc.Item(0).Value.ToUpper() == "PD" &&
                                     fc.Item(1).Value.ToUpper() == "ABJ" &&
                                     !totalVolumeFound)
                                try
                                {
                                    booking.TotalVolume = fc.Item(3).Value;
                                    totalVolumeFound = true;
                                }
                                catch (Exception)
                                {
                                    totalVolumeFound = true;
                                    booking.TotalVolume = "0";
                                }
                        }
                        if (o.Name.ToUpper() == "RFF" && itemStartTag)
                        {
                            var fc = o.Fields;
                            if (fc.Item(0).Value == "AAU")
                                try
                                {
                                    deliveryDocketNumber = fc.Item(1).Value;
                                }
                                catch (Exception e)
                                {
                                    Logger.Log(
                                        "Exception Occurred while using EDIFACT Parsing to extract Delivery Docket Number, Element=REF, fileName:" +
                                        ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                }
                        }
                        if (o.Name.ToUpper() == "GIN")
                        {
                            var fc = o.Fields;
                            if (fc.Item(0).Value.ToUpper() == "BJ")
                            {
                                var barcode = "";
                                barcode = fc.Item(1).Value;
                                item.Barcode = barcode;
                                item.Quantity = 1;
                                itemStartTag = false;
                                if (deliveryDictionary.ContainsKey(deliveryDocketNumber))
                                {
                                    var existingbooking = deliveryDictionary[deliveryDocketNumber];
                                    existingbooking.lstItems.Add(item);
                                    var temp = existingbooking;
                                    deliveryDictionary[deliveryDocketNumber] = temp;
                                }
                                else
                                {
                                    var bk = booking.DeepCopy();
                                    bk.lstItems = new List<Item> { item };
                                    var temp = bk;
                                    //assign the login details
                                    temp.LoginDetails = ld;
                                    //also make ref1 as the connote field
                                    temp.ConsignmentNumber = temp.Ref1;
                                    deliveryDictionary.Add(deliveryDocketNumber, temp);
                                }
                            }
                        }
                    }
                }
                if (createCsvDumpFile)
                {
                    var dump = "";
                    var index = 1;
                    //create a header file
                    dump +=
                        "Sl No,StateId,DriverNumber,DespatchDate,Ref1(ConsignmentNumber),Ref2(DeliveryDocketNumber),FromDetail1,FromDetail2,FromDetail3,FromDetail4,FromSuburb,FromPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToSuburb,ToPostcode,TotalItem,TotalWeight,TotalVolume,Items\n";
                    var builder = new System.Text.StringBuilder();
                    builder.Append(dump);
                    foreach (var key in deliveryDictionary.Keys)
                    {
                        var booking = deliveryDictionary[key];
                        //assign the key to be Ref2
                        booking.Ref2 = key;
                        lstBooking.Add(booking);
                        var _booking = DistributeTotalWeightVolume(booking);
                        if (createCsvDumpFile)
                        {
                            builder.Append(index + ","
                                    + _booking.StateId + ","
                                    + _booking.DriverNumber + ","
                                    + _booking.DespatchDateTime.ToString(CultureInfo.InvariantCulture) + ","
                                    + _booking.Ref1 + "," + key + ","
                                    + _booking.FromDetail1 + ","
                                    + _booking.FromDetail2 + ","
                                    + _booking.FromDetail3 + ","
                                    + _booking.FromDetail4 + ","
                                    + _booking.FromSuburb + ","
                                    + _booking.FromPostcode + ","
                                    + _booking.ToDetail1 + ","
                                    + _booking.ToDetail2 + ","
                                    + _booking.ToDetail3 + ","
                                    + _booking.ToDetail4 + ","
                                    + _booking.ToSuburb + ","
                                    + _booking.ToPostcode + ","
                                    //+ booking.TotalItems + ","
                                    + _booking.lstItems.Count + ","
                                    + _booking.TotalWeight + ","
                                    + _booking.TotalVolume + ",");
                            dump = _booking.lstItems.Aggregate(dump,
                                (current, item) =>
                                    current + ("'" + item.Barcode + "(" + item.Weight + "|" + item.Cubic) + ")',");
                            dump = dump.Substring(0, dump.Length - 1);

                            dump += "\n";
                            index++;
                        }
                    }
                    dump = builder.ToString();

                    //if (createCsvDumpFile)
                    var pathName = $"RouteFile{DateTime.Now.Ticks}.csv";

                    File.WriteAllText(pathName,
                        dump);
                    try
                    {
                        if (ld.UserName.ToUpper() == "GWA")
                        {
                            var htmlText =
                                "GWA Extracted Routes are attached with this email. These Routes will appear in the TMS for the designated Advance Date - usually the next working day\n"
                                + "EDI Filename: " + ediFilename;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Log(
                            "Exception Occurred while sending Routes as attachment:" + ediFilename +
                            " Exception Details:" + e.Message, "EdifactExtractor");
                    }
                }
                else
                {
                    foreach (var key in deliveryDictionary.Keys)
                    {
                        var booking = deliveryDictionary[key];
                        //assign the key to be Ref2
                        booking.Ref2 = key;
                        lstBooking.Add(booking);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while using EDIFACT Custom Library to parse a file:" + ediFilename +
                    " Exception Details:" + e.Message, "EdifactExtractor");
            }
            //before we send the list of bookings we need to put them in an order
            if (createdOrderedList && lstBooking != null && lstBooking.Count > 1)
            {
                List<Booking> orderedBookings = null;
                try
                {
                    //sort the list in alphabetical order
                    var sortedList = lstBooking.OrderBy(x => x.ToSuburb).ToList();
                    //also we need to remove any of the duplicate to Detail addresses
                    // var uniqueLegs = lstBooking.GroupBy(x => x.ToDetail1).Select(x => x.First());
                    var matrixLibrary = new MatrixLibrary();
                    //get the farthest location from the PU address (from the fisrt element in the list - usually warehouse location)              
                    var endBooking = matrixLibrary.GetDistanceMatrixEndPoint(sortedList);
                    //remove the start of the booking from the list
                    var startBooking = sortedList[0];
                    //lsBookings.RemoveAt(0);
                    //lsBookings.Remove(endBooking);
                    //  sortedList.Remove(startBooking);
                    // sortedList.Remove(endBooking);
                    var groupedBookings = CollectionsExtension.GetUniqueBookings(sortedList);
                    var uniqueBookings = (from groupedBooking in groupedBookings
                                          select groupedBookings.FirstOrDefault(x => x.Key == groupedBooking.Key)
                        into firstBooking
                                          where firstBooking != null
                                          select firstBooking.FirstOrDefault()).ToList();
                    //after getting a grouped list of bookings get the individual bookings
                    /* orderedBookings = matrixLibrary.GetWaypointOrdereBookings(sortedList, startBooking,
                         endBooking);*/

                    orderedBookings = matrixLibrary.GetWaypointOrdereBookings(uniqueBookings, startBooking,
                        endBooking);
                    //check added here when the returned ordered list is Empty - this happens when Google Directions API return ZERO_RESULTS
                    if (!orderedBookings.Any())
                        orderedBookings = uniqueBookings;
                    var reCreatedOrderedBookings = new List<Booking>();
                    //inflate the list with all jobs in the received order
                    foreach (var orderBooking in orderedBookings)
                    {
                        //add the first job
                        reCreatedOrderedBookings.Add(orderBooking);
                        //using the grouped bookings get all jobs for the key
                        IEnumerable<Booking> allBookingsForSuburb =
                            groupedBookings.Single(x => x.Key == orderBooking.ToSuburb);
                        //add all the remaining ones
                        if (allBookingsForSuburb.Any())
                        {
                            foreach (var indBooking in allBookingsForSuburb)
                                if (!reCreatedOrderedBookings.Contains(indBooking))
                                    reCreatedOrderedBookings.Add(indBooking);
                            if (reCreatedOrderedBookings.Any())
                                orderedBookings = reCreatedOrderedBookings;
                        }
                    }
                    if (createCsvDumpFile)
                    {
                        var dump = "";
                        dump =
                            "Sl No,StateId,DriverNumber,DespatchDate,Ref1(ConsignmentNumber),Ref2(DeliveryDocketNumber),FromDetail1,FromDetail2,FromDetail3,FromDetail4,FromSuburb,FromPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToSuburb,ToPostcode,TotalItem,TotalWeight,TotalVolume,Items\n";
                        var index = 1;
                        var builder = new System.Text.StringBuilder();
                        builder.Append(dump);
                        foreach (var orderedBooking in orderedBookings)
                        {
                            builder.Append(index + ","
                                    + orderedBooking.StateId + ","
                                    + orderedBooking.DriverNumber + ","
                                    + orderedBooking.DespatchDateTime.ToString(CultureInfo.InvariantCulture) + ","
                                    + orderedBooking.Ref1 + ","
                                    + orderedBooking.Ref2 + ","
                                    + orderedBooking.FromDetail1 + ","
                                    + orderedBooking.FromDetail2 + ","
                                    + orderedBooking.FromDetail3 + ","
                                    + orderedBooking.FromDetail4 + ","
                                    + orderedBooking.FromSuburb + ","
                                    + orderedBooking.FromPostcode + ","
                                    + orderedBooking.ToDetail1 + ","
                                    + orderedBooking.ToDetail2 + ","
                                    + orderedBooking.ToDetail3 + ","
                                    + orderedBooking.ToDetail4 + ","
                                    + orderedBooking.ToSuburb + ","
                                    + orderedBooking.ToPostcode + ","
                                    //+ booking.TotalItems + ","
                                    + orderedBooking.lstItems.Count + ","
                                    + orderedBooking.TotalWeight + ","
                                    + orderedBooking.TotalVolume + ",");
                            dump = orderedBooking.lstItems.Aggregate(dump,
                                (current, item) => current + "'" + item.Barcode + "',");
                            dump = dump.Substring(0, dump.Length - 1);
                            dump += "\n";
                            index++;
                        }
                        dump = builder.ToString();
                        var pathNameOrdered = $"OrderedRouteFile{DateTime.Now.Ticks}.csv";
                        File.WriteAllText(pathNameOrdered,
                            dump);
                        try
                        {
                            if (ld.UserName.ToUpper() == "GWA")
                            {
                                var htmlText =
                                    "GWA Ordered Routes are attached with this email. These Routes will appear in the TMS for the designated Advance Date - usually the next working day\n"
                                    + "\nEDI Filename: " + ediFilename;
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception Occurred while sending Routes as attachment:" + ediFilename +
                                " Exception Details:" + e.Message, "EdifactExtractor");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred while using Google Directions API to create an optimized Route, EDI filename:" +
                        ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                }
                return orderedBookings.Count == 0 ? lstBooking : orderedBookings;
            }
            return lstBooking;
        }

        public
            ConsolidatedJob ParseWithConsolidation(string ediFilename, LoginDetails ld, int mappedStateId,
                int driverNumber, bool createdOrderedList = false,
                bool createCsvDumpFile = false)
        {
            var consolidatedJob = new ConsolidatedJob();

            var lstBooking = new List<Booking>();


            try
            {
                //var msg = new EDIMessage(File.ReadAllText(ediFilename).Replace("'","").Replace(","," ").Replace("."," "), true);
                var msg = new EDIMessage(File.ReadAllText(ediFilename), true);
                // var text = File.ReadAllText(ediFilename);

                var segmentCollections = msg.GetAllSegmentCollections(ediFilename);
                if (
                    GwaAverageWeightAcrossItems
)
                    segmentCollections
                        =
                        AssignAverageWeights(segmentCollections);

                var deliveryDictionary = new Dictionary<string, Booking>();


                foreach (
                    var segment
                    in
                    segmentCollections
                )
                {
                    var deliveryDocketNumber = "";
                    var booking = new Booking { StateId = mappedStateId.ToString() };
                    //every booking will have the same state id
                    //assign driver number if there exists a route for the driver
                    if (driverNumber != -1)
                        booking.DriverNumber = driverNumber;
                    else
                        try
                        {
                            //raise a message that there is no Driver assigned to this route - possibly a new driver needs to be setup in XCAB
                            var message =
                                string.Format(
                                    @"Received A File where there is No Route setup in XCAB Db (tablename: xCabDriverRoutes)\n"
                                    + "EDI Filename=" + ediFilename
                                    + ",StateId=" + mappedStateId
                                    + ",Username=" + ld.UserName
                                    + "\n\n"
                                    +
                                    "This file will be created with No Driver Information in our TMS. Operators will have to assign drivers against the jobs manually"
                                    +
                                    "Please create a new Driver Route in the DB:XCAB (tablename: xCabDriverRoutes) for the required state, route & user"
                                );
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception occurred while sending a notification email about driver routes. Exception:" +
                                e.Message, "EdifactExtractor");
                        }
                    var itemStartTag = false;
                    var totalVolumeFound = false;
                    Item item = null;
                    foreach (Segment o in segment)
                        try
                        {
                            if (o.Name.ToUpper() == "NAD")
                            {
                                /**
                                 * Example
                
                                Shipper address details
                                NAD+SH+100583::91+FREIGHT ABC+FREIGHT ABC+PO BOX 777+NOWRA++1550'
                
                                Ship-To Address (Can be either the end customer or a 3PL provider)
                                NAD+ST+H10028::91+ALLFREIGHT+24 SHARP COURT+CAVAN+8262776++5094'
                
                                Party who supplies goods and/or services
                                NAD+SU+C15::91+Starion Tapware Pty Ltd - TEST 200+Starion Tapware Pty Ltd - TEST 200+Locked Bag 20+Epping++2121’
                
                                Party to whom merchandise and/or service is sold if different from delivery party.
                                NAD+BY+C12820::91+HW DISTRIBUTION+HW DISTRIBUTION LIMITED+88 BLUE ROAD+ILLAWARRA ++6002
                
                                Ultimate customer address details (Party to where the goods should be delivered)
                                NAD+UD+DA001::91+GREEN PLUMBING +HW DISTRIBUTION LIMITED+85 YELLOW STREET+ILLAWARRA++6001+AU'
                
                                 * 
                                 * */
                                var fc = o.Fields;
                                switch (fc.Item(0).Value.ToUpper())
                                {
                                    case "SU":
                                    case "SF":
                                        //As from above SH is the shippers address detail or in other words PU Address fields
                                        var pickupAddressLine1 = "";
                                        var pickupAddressLine2 = "";
                                        var pickupAddressLine3 = "";
                                        var pickupPostcode = "";
                                        pickupAddressLine1 = fc.Item(5).Value;
                                        pickupAddressLine2 = fc.Item(6).Value;
                                        pickupAddressLine3 = fc.Item(7).Value;
                                        pickupPostcode = fc.Item(9).Value;
                                        try
                                        {
                                            //on certain files the postcodes were missing
                                            if (pickupPostcode.Length == 0)
                                                pickupPostcode = fc.Item(12).Value;
                                        }
                                        catch (Exception e)
                                        {
                                            Logger.Log(
                                                "Exception Occurred while using EDIFACT Parsing to extract PU Postcode, Element=NAD+SH, fileName:" +
                                                ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                        }
                                        booking.FromDetail1 = pickupAddressLine1.Replace(',', ' ');
                                        booking.FromDetail2 = pickupAddressLine2.Replace(',', ' ');
                                        booking.FromSuburb = pickupAddressLine3.Replace(',', ' ');
                                        booking.FromPostcode = pickupPostcode;
                                        break;
                                    case "UD":
                                        //UD is the ultimate delivery section:
                                        //Debug.Write("Found Delivery Address");
                                        //NAD+UD+DA001::91++19 Merivale St+South Brisbane++4101+AU'
                                        //fc.Item(3)= 91: 91+<4th Index:Add Line 1>+<5th Index:Add Line 2>+<6th Index:Add Line 3>+<7th Index:Suburb>+<8th Index:Postcode>+<9th Index:AU>'

                                        var partyNameLine1 = "";
                                        var partyNameLine2 = "";

                                        var deliveryAddressLine1 = "";
                                        var deliveryAddressLine2 = "";
                                        //const string deliveryAddressLine3 = "";

                                        var deliverySuburb = "";
                                        var deliveryPostcode = "";
                                        try
                                        {
                                            partyNameLine1 = fc.Item(4).Value;
                                            partyNameLine2 = fc.Item(5).Value;
                                        }
                                        catch (Exception e)
                                        {
                                            Logger.Log(
                                                "Exception Occurred while using EDIFACT Parsing to extract Customer Name, Element=NAD+UD,fileName:" +
                                                ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                        }
                                        try
                                        {
                                            deliveryAddressLine1 = fc.Item(6).Value;
                                            deliveryAddressLine2 = fc.Item(7).Value; //address line 2 is the suburb
                                            deliverySuburb = fc.Item(7).Value;
                                            //deliveryAddressLine4 = fc.Item(9).Value;
                                            deliveryPostcode = fc.Item(9).Value; //this is the postcode
                                        }
                                        catch (Exception e)
                                        {
                                            Logger.Log(
                                                "Exception Occurred while using EDIFACT Parsing to extract DEL Suburbs,Postcodes,addlines1-2, Element=NAD+UD,fileName:" +
                                                ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                        }
                                        try
                                        {
                                            //on certain files the postcodes were missing
                                            if (deliveryPostcode.Length == 0 && o.Fields.Count >= 13)
                                                deliveryPostcode = fc.Item(12).Value;
                                        }
                                        catch (Exception e)
                                        {
                                            Logger.Log(
                                                "Exception Occurred while using EDIFACT Parsing to extract DEL Postcode, Element=NAD+UD,fileName:" +
                                                ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                        }
                                        //for TMS we need the customer name as the address line 1
                                        //Change required to support customer name 1 that comes in the EDI file instead of Customer name line 2 (partLine2)
                                        //we check if customer name line 1 has a value, if found we just use that otherwise we fall back and use customer name line 2
                                        if (string.IsNullOrEmpty(partyNameLine1))
                                            booking.ToDetail1 = partyNameLine2.Replace(',', ' ').Trim();
                                        else
                                            booking.ToDetail1 = partyNameLine1.Replace(',', ' ').Trim();
                                        //booking.ToDetail1 = (partyNameLine1 + partyNameLine2).Replace(',', ' ');
                                        if (!string.IsNullOrEmpty(deliveryAddressLine1))
                                            booking.ToDetail2 = deliveryAddressLine1.Replace(',', ' ');
                                        if (!string.IsNullOrEmpty(deliveryAddressLine2))
                                            booking.ToDetail3 = deliveryAddressLine2.Replace(',', ' ');
                                        booking.ToSuburb = deliverySuburb;
                                        booking.ToPostcode = deliveryPostcode;
                                        break;
                                    case "ST":
                                        //ST is the Ship To (most cases is the ultimate recipient) but can be 3PL provider
                                        //we will use values from ST only when UD is not available
                                        var deliveryAddressLineSt1 = "";
                                        var deliveryAddressLineSt2 = "";
                                        var deliveryAddressLineSt3 = "";
                                        var deliveryPostcodeSt = "";
                                        try
                                        {
                                            deliveryAddressLineSt1 = fc.Item(5).Value;
                                            deliveryAddressLineSt2 = fc.Item(6).Value;
                                            deliveryAddressLineSt3 = fc.Item(7).Value;
                                            deliveryPostcodeSt = fc.Item(9).Value;
                                        }
                                        catch (Exception e)
                                        {
                                            Logger.Log(
                                                "Exception Occurred while using EDIFACT Parsing to extract DEL Del Details, Element=NAD+ST,fileName:" +
                                                ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                        }
                                        try
                                        {
                                            //on certain files the postcodes were missing
                                            if (deliveryPostcodeSt.Length == 0)
                                                deliveryPostcodeSt = fc.Item(12).Value;
                                        }
                                        catch (Exception e)
                                        {
                                            Logger.Log(
                                                "Exception Occurred while using EDIFACT Parsing to extract DEL Postcode, Element=NAD+ST,fileName:" +
                                                ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                        }
                                        if (string.IsNullOrEmpty(booking.ToDetail1))
                                            booking.ToDetail1 = deliveryAddressLineSt1.Replace(',', ' ');
                                        if (string.IsNullOrEmpty(booking.ToDetail2))
                                            booking.ToDetail2 = deliveryAddressLineSt2.Replace(',', ' ');
                                        if (string.IsNullOrEmpty(booking.ToDetail3))
                                            booking.ToSuburb = deliveryAddressLineSt3.Replace(',', ' ');
                                        if (string.IsNullOrEmpty(booking.ToPostcode))
                                            booking.ToPostcode = deliveryPostcodeSt;
                                        break;
                                }
                            }
                            //if (o.Name.ToUpper() == "CTA")
                            //{
                            //    //this is the contact phone number: CTA+DL+:Customer Contact'
                            //    if (booking.Notification == null)
                            //    {
                            //        var notification = new Notification();
                            //        notification.SMSNumber = o.Fields.Item(0).Value;
                            //        booking.Notification = notification;
                            //    }
                            //}
                            if (o.Name.ToUpper() == "COM")
                            {
                                //this is the contact email address: COM+abaltic@gwagroup.com.au:EM' and UNS mobile :COM+0400123456:TE'
                                try
                                {
                                    var comValue = o.Fields.Item(0).Value;
                                    if (o.Fields.Item(0).Value == "EM" && !string.IsNullOrEmpty(comValue) && comValue.Contains("@"))
                                    {
                                        if (booking.Notification == null)
                                        {
                                            var notification = new Notification
                                            {
                                                EmailAddress = comValue
                                            };
                                            booking.Notification = notification;
                                        }
                                        else
                                        {
                                            booking.Notification.EmailAddress = comValue;
                                        }
                                    }
                                    else if (o.Fields.Item(0).Value == "TE")
                                    {
                                        if (booking.Notification == null)
                                        {
                                            var notification = new Notification
                                            {
                                                SMSNumber = comValue
                                            };
                                            booking.Notification = notification;
                                        }
                                        else
                                        {
                                            booking.Notification.SMSNumber = comValue;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    Logger.Log(
                                   "Exception Occurred while using EDIFACT Parsing to extract email notifications, Element=COM, fileName:" +
                                   ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                }
                            }
                            if (o.Name.ToUpper() == "RFF")
                                try
                                {
                                    var fc = o.Fields;
                                    switch (fc.Item(0).Value.ToUpper())
                                    {
                                        case "CN":
                                            if (fc.Item(1) != null)
                                                booking.Ref1 = fc.Item(1).Value;
                                            break;
                                        case "ADE":
                                            if (fc.Item(1) != null)
                                                booking.AccountCode = fc.Item(1).Value;
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Logger.Log(
                                        "Exception Occurred while using EDIFACT Parsing to extract References, Element=REF, fileName:" +
                                        ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                }
                            if (o.Name.ToUpper() == "GIR")
                            {
                                var fc = o.Fields;
                                try
                                {
                                    if (fc.Item(0).Value == "3")
                                    {
                                        //extract purchase order
                                        var purchaseOrder = fc.Item(1).Value;
                                        if (!string.IsNullOrWhiteSpace(purchaseOrder))
                                        {
                                            var extraField = new ExtraFields { Key = "Purchase Order Number", Value = purchaseOrder.Trim() };
                                            if (booking.lstExtraFields == null)
                                            {
                                                var extraFields = new List<ExtraFields>
                                                {
                                                    extraField
                                                };
                                                booking.lstExtraFields = extraFields;
                                            }
                                            else
                                            {
                                                booking.lstExtraFields.Add(extraField);
                                            }
                                        }

                                    }
                                }
                                catch (Exception e)
                                {
                                    Logger.Log(
                                            "Exception Occurred while using EDIFACT Parsing to extract PO order number, Element=GIR+3, fileName:" +
                                            ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                }
                            }
                            if (o.Name.ToUpper() == "DTM")
                            {
                                var fc = o.Fields;
                                if (fc.Item(0).Value == "11")
                                    try
                                    {
                                        var dt = DateTime.ParseExact(fc.Item(1).Value, "yyyyMMddHHmm",
                                            CultureInfo.InvariantCulture);
                                        booking.DespatchDateTime = dt;
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.Log(
                                            "Exception Occurred while using EDIFACT Parsing to extract Despatch date time, Element=DTM+11, fileName:" +
                                            ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                    }
                            }

                            if (o.Name.ToUpper() == "CPS" && !itemStartTag)
                            {
                                itemStartTag = true;
                                item = new Item();
                            }
                            else if (o.Name.ToUpper() == "CPS" && itemStartTag)
                            {
                                itemStartTag = false;
                            }

                            if (o.Name.ToUpper() == "MEA")
                            {
                                var fc = o.Fields;
                                if (item != null)
                                {
                                    if (fc.Item(2).Value.ToUpper() == "KGM" && fc.Item(0).Value.ToUpper() == "PD" &&
                                        fc.Item(1).Value.ToUpper() == "AAB")
                                        try
                                        {
                                            var weight = "";
                                            weight = fc.Item(3).Value;
                                            item.Weight = Convert.ToDouble(weight);
                                        }
                                        catch (Exception e)
                                        {
                                            //if there is an issue parsing the weight we just assign 0
                                            item.Weight = 0;
                                            Logger.Log(
                                                "Exception Occurred while using EDIFACT Parsing to extract Item Weight, Element=MEA+PD+AAB+KGM fileName:" +
                                                ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                        }
                                    if (fc.Item(2).Value.ToUpper() == "MTQ" && fc.Item(0).Value.ToUpper() == "PD" &&
                                        fc.Item(1).Value.ToUpper() == "ABJ")
                                        try
                                        {
                                            var volume = "";
                                            volume = fc.Item(3).Value;
                                            item.Cubic = volume.Length > 0 ? Convert.ToDouble(volume) : 0;
                                        }
                                        catch (Exception)
                                        {
                                            //if there is an issue parsing the volume we just assign 0
                                            item.Cubic = 0;
                                        }
                                }
                                if (fc.Item(2).Value.ToUpper() == "KGM" && fc.Item(0).Value.ToUpper() == "PD" &&
                                    fc.Item(1).Value.ToUpper() == "G")
                                    try
                                    {
                                        var weight = "";
                                        weight = fc.Item(3).Value;
                                        booking.TotalWeight = weight;
                                    }
                                    catch (Exception e)
                                    {
                                        booking.TotalWeight = "0";
                                        Logger.Log(
                                            "Exception Occurred while using EDIFACT Parsing to extract Total Weight,ELement=MEA+PD+G+KGM, fileName:" +
                                            ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                    }
                                else if (fc.Item(2).Value.ToUpper() == "MTQ" && fc.Item(0).Value.ToUpper() == "PD" &&
                                         fc.Item(1).Value.ToUpper() == "ABJ" &&
                                         !totalVolumeFound)
                                    try
                                    {
                                        booking.TotalVolume = fc.Item(3).Value;
                                        totalVolumeFound = true;
                                    }
                                    catch (Exception)
                                    {
                                        totalVolumeFound = true;
                                        booking.TotalVolume = "0";
                                    }
                            }
                            if (o.Name.ToUpper() == "RFF" && itemStartTag)
                            {
                                var fc = o.Fields;
                                if (fc.Item(0).Value == "AAU")
                                    try
                                    {
                                        deliveryDocketNumber = fc.Item(1).Value;
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.Log(
                                            "Exception Occurred while using EDIFACT Parsing to extract Delivery Docket Number, Element=REF, fileName:" +
                                            ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                                    }
                            }
                            if (o.Name.ToUpper() == "GIN")
                            {
                                var fc = o.Fields;
                                if (fc.Item(0).Value.ToUpper() == "BJ")
                                {
                                    var barcode = "";
                                    barcode = fc.Item(1).Value;
                                    item.Barcode = barcode;
                                    item.Quantity = 1;
                                    itemStartTag = false;
                                    if (deliveryDictionary.ContainsKey(deliveryDocketNumber))
                                    {
                                        var existingbooking = deliveryDictionary[deliveryDocketNumber];
                                        existingbooking.lstItems.Add(item);
                                        var temp = existingbooking;
                                        deliveryDictionary[deliveryDocketNumber] = temp;
                                    }
                                    else
                                    {
                                        var bk = booking.DeepCopy();
                                        bk.lstItems = new List<Item> { item };
                                        var temp = bk;
                                        //assign the login details
                                        temp.LoginDetails = ld;
                                        //also make ref1 as the connote field
                                        temp.ConsignmentNumber = temp.Ref1;
                                        //assign a default upload date time
                                        temp.UploadDateTime = DateTime.Now;
                                        deliveryDictionary.Add(deliveryDocketNumber, temp);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.Write(e.Message);
                        }
                }
                if (
                    createCsvDumpFile)
                {
                    var dump = "";
                    var index = 1;
                    //create a header file
                    dump
                        +=
                        "Sl No,StateId,DriverNumber,DespatchDate,Ref1(ConsignmentNumber),Ref2(DeliveryDocketNumber),FromDetail1,FromDetail2,FromDetail3,FromDetail4,FromSuburb,FromPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToSuburb,ToPostcode,TotalItem,TotalWeight,TotalVolume,EmailAddress,Items\n";
                    var builder = new System.Text.StringBuilder();
                    builder.Append(dump);
                    foreach (
                        var key
                        in
                        deliveryDictionary.Keys
                    )
                    {
                        var booking = deliveryDictionary[key];
                        //assign the key to be Ref2
                        booking.Ref2 = key;
                        lstBooking.Add(booking);
                        var _booking = DistributeTotalWeightVolume(booking);
                        if (createCsvDumpFile)
                        {
                            builder.Append(index + ","
                                    + _booking.StateId + ","
                                    + _booking.DriverNumber + ","
                                    + _booking.DespatchDateTime.ToString(CultureInfo.InvariantCulture) + ","
                                    + _booking.Ref1 + "," + key + ","
                                    + _booking.FromDetail1 + ","
                                    + _booking.FromDetail2 + ","
                                    + _booking.FromDetail3 + ","
                                    + _booking.FromDetail4 + ","
                                    + _booking.FromSuburb + ","
                                    + _booking.FromPostcode + ","
                                    + _booking.ToDetail1 + ","
                                    + _booking.ToDetail2 + ","
                                    + _booking.ToDetail3 + ","
                                    + _booking.ToDetail4 + ","
                                    + _booking.ToSuburb + ","
                                    + _booking.ToPostcode + ","
                                    //+ booking.TotalItems + ","
                                    + _booking.lstItems.Count + ","
                                    + _booking.TotalWeight + ","
                                    + _booking.TotalVolume + ",");
                            if (_booking.Notification != null && _booking.Notification.EmailAddress != null)
                                builder.Append(_booking.Notification.EmailAddress + ",");
                            else
                                builder.Append(",");
                            dump = _booking.lstItems.Aggregate(dump,
                                (current, item) =>
                                    current + "'" + item.Barcode + "(" + item.Weight + "|" + item.Cubic + ")',");
                            dump = dump.Substring(0, dump.Length - 1);

                            dump += "\n";
                            index++;
                        }
                    }
                    dump = builder.ToString();

                    //if (createCsvDumpFile)
                    var pathName = $"RouteFile{DateTime.Now.Ticks}.csv";

                    File.WriteAllText
                    (
                        pathName
                        ,
                        dump
                    );
                    try
                    {
                        if (ld.UserName.ToUpper() == "GWA")
                        {
                            var htmlText =
                                "GWA Extracted Routes are attached with this email. These Routes will appear in the TMS for the designated Advance Date - usually the next working day\n"
                                + "EDI Filename: " + ediFilename;                           
                        }
                    }
                    catch (
                        Exception e)
                    {
                        Logger.Log
                        ("Exception Occurred while sending Routes as attachment:" +
                         ediFilename
                         + " Exception Details:" +
                         e.Message
                            , "EdifactExtractor");
                    }
                }
                else

                {
                    foreach (
                        var key in deliveryDictionary.Keys)
                    {
                        var booking = deliveryDictionary[key];
                        //assign the key to be Ref2
                        booking.Ref2
                            =
                            key;
                        lstBooking.Add
                        (
                            booking
                        );
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while using EDIFACT Custom Library to parse a file:" + ediFilename +
                    " Exception Details:" + e.Message, "EdifactExtractor");
            }
            if (createdOrderedList && lstBooking != null && lstBooking.Count >= 1)
            {
                var consolidatedBookings = lstBooking.GroupBy(x => x.ToDetail2).Select(
                    x => new Booking
                    {
                        AccountCode = x.ToList()[0].AccountCode,
                        //Ref1 = x.ToList()[0].Ref1,
                        Ref1 = x.Count().ToString() + " Invoices",
                        Ref2 = x.ToList()[0].ToDetail1.Replace('/', ' '), //store customer name as reference
                        Caller = x.ToList()[0].Caller,
                        FromDetail1 = x.ToList()[0].FromDetail1,
                        FromDetail2 = x.ToList()[0].FromDetail2,
                        FromDetail3 = x.ToList()[0].FromDetail3 == null ? "" : x.ToList()[0].FromDetail3,
                        FromDetail4 = x.ToList()[0].FromDetail4 == null ? "" : x.ToList()[0].FromDetail4,
                        FromDetail5 = x.ToList()[0].FromDetail5 == null ? "" : x.ToList()[0].FromDetail5,
                        FromSuburb = x.ToList()[0].FromSuburb,
                        FromPostcode = x.ToList()[0].FromPostcode,
                        ToDetail1 = x.ToList()[0].ToDetail1,
                        ToDetail2 = x.ToList()[0].ToDetail2,
                        ToDetail3 = x.ToList()[0].ToDetail3 == null ? "" : x.ToList()[0].ToDetail3,
                        ToDetail4 = x.ToList()[0].ToDetail4 == null ? "" : x.ToList()[0].ToDetail4,
                        ToDetail5 = x.ToList()[0].ToDetail5 == null ? "" : x.ToList()[0].ToDetail5,
                        ToSuburb = x.ToList()[0].ToSuburb,
                        ToPostcode = x.ToList()[0].ToPostcode,
                        StateId = x.ToList()[0].StateId,
                        ServiceCode = x.ToList()[0].ServiceCode,
                        PreAllocatedDriverNumber = x.ToList()[0].PreAllocatedDriverNumber,
                        DriverNumber = x.ToList()[0].DriverNumber,
                        DespatchDateTime = x.ToList()[0].DespatchDateTime,
                        LoginDetails = new LoginDetails
                        {
                            Id = x.ToList()[0].LoginDetails.Id
                        },
                        UploadDateTime = x.ToList()[0].UploadDateTime,
                        TotalItems = x.Sum(c => c.lstItems.Count).ToString(),
                        TotalWeight = x.Sum(c => Convert.ToDouble(c.TotalWeight)).ToString(),
                        TotalVolume = x.Sum(c => Convert.ToDouble(c.TotalVolume)).ToString(),
                        lstItems = x.SelectMany(y => y.lstItems).ToList(),
                        Notification = x.ToList()[0].Notification,
                        lstExtraFields = x.SelectMany(y => y.lstExtraFields).ToList(),
                    }
                );
                var _groupedBookings = lstBooking.GroupBy(x => x.ToDetail2);


                var consolidatedJobsList = new List<Booking>();

                foreach (var consolidatedBooking in consolidatedBookings)
                    consolidatedJobsList.Add(consolidatedBooking);

                lstBooking = consolidatedJobsList;
                consolidatedJob.ConsolidatedBookings = lstBooking;
                consolidatedJob.GroupedJobs = _groupedBookings;



                List<Booking> orderedBookings = null;
                try
                {
                    //sort the list in alphabetical order
                    var sortedList = lstBooking.OrderBy(x => x.ToSuburb).ToList();
                    //also we need to remove any of the duplicate to Detail addresses
                    // var uniqueLegs = lstBooking.GroupBy(x => x.ToDetail1).Select(x => x.First());
                    var matrixLibrary = new MatrixLibrary();
                    //get the farthest location from the PU address (from the fisrt element in the list - usually warehouse location)              
                    var endBooking = matrixLibrary.GetDistanceMatrixEndPoint(sortedList);
                    //remove the start of the booking from the list
                    var startBooking = sortedList[0];
                    var groupedBookings = CollectionsExtension.GetUniqueBookings(sortedList);
                    var uniqueBookings = (from groupedBooking in groupedBookings
                                          select groupedBookings.FirstOrDefault(x => x.Key == groupedBooking.Key)
                        into firstBooking
                                          where firstBooking != null
                                          select firstBooking.FirstOrDefault()).ToList();
                    //after getting a grouped list of bookings get the individual bookings
                    /* orderedBookings = matrixLibrary.GetWaypointOrdereBookings(sortedList, startBooking,
                                                                                                         endBooking);*/

                    orderedBookings = matrixLibrary.GetWaypointOrdereBookings(uniqueBookings, startBooking,
                        endBooking);
                    //check added here when the returned ordered list is Empty - this happens when Google Directions API return ZERO_RESULTS
                    if (!orderedBookings.Any())
                        orderedBookings = uniqueBookings;
                    var reCreatedOrderedBookings = new List<Booking>();
                    //inflate the list with all jobs in the received order
                    foreach (var orderBooking in orderedBookings)
                    {
                        reCreatedOrderedBookings.Add(orderBooking);
                        //using the grouped bookings get all jobs for the key
                        IEnumerable<Booking> allBookingsForSuburb =
                            groupedBookings.Single(x => x.Key == orderBooking.ToSuburb);
                        //add all the remaining ones
                        if (allBookingsForSuburb.Any())
                            foreach (var indBooking in allBookingsForSuburb)
                                if (!reCreatedOrderedBookings.Contains(indBooking))
                                    reCreatedOrderedBookings.Add(indBooking);
                    }
                    if (createCsvDumpFile)
                    {
                        var dump = "";
                        dump =
                            "Sl No,StateId,DriverNumber,DespatchDate,Ref1(ConsignmentNumber),Ref2(DeliveryDocketNumber),FromDetail1,FromDetail2,FromDetail3,FromDetail4,FromSuburb,FromPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToSuburb,ToPostcode,TotalItem,TotalWeight,TotalVolume,EmailAddress,Items\n";
                        var index = 1;
                        var builder = new System.Text.StringBuilder();
                        builder.Append(dump);
                        foreach (var orderedBooking in reCreatedOrderedBookings)
                        {
                            builder.Append(index + ","
                                    + orderedBooking.StateId + ","
                                    + orderedBooking.DriverNumber + ","
                                    + orderedBooking.DespatchDateTime.ToString(CultureInfo.InvariantCulture) + ","
                                    + orderedBooking.Ref1 + ","
                                    + orderedBooking.Ref2 + ","
                                    + orderedBooking.FromDetail1 + ","
                                    + orderedBooking.FromDetail2 + ","
                                    + orderedBooking.FromDetail3 + ","
                                    + orderedBooking.FromDetail4 + ","
                                    + orderedBooking.FromSuburb + ","
                                    + orderedBooking.FromPostcode + ","
                                    + orderedBooking.ToDetail1 + ","
                                    + orderedBooking.ToDetail2 + ","
                                    + orderedBooking.ToDetail3 + ","
                                    + orderedBooking.ToDetail4 + ","
                                    + orderedBooking.ToSuburb + ","
                                    + orderedBooking.ToPostcode + ","
                                    //+ booking.TotalItems + ","
                                    + orderedBooking.lstItems?.Count + ","
                                    + orderedBooking.TotalWeight + ","
                                    + orderedBooking.TotalVolume + ",");
                            if (orderedBooking.Notification != null && orderedBooking.Notification.EmailAddress != null)
                                builder.Append(orderedBooking.Notification.EmailAddress + ",");
                            else
                                builder.Append(",");
                            dump = orderedBooking.lstItems?.Aggregate(dump,
                                (current, item) => current + "'" + item.Barcode + "',");
                            dump = dump?.Substring(0, dump.Length - 1);
                            dump += "\n";
                            index++;
                        }
                        dump = builder.ToString();
                        var pathNameOrdered = $"OrderedRouteFile{DateTime.Now.Ticks}.csv";
                        File.WriteAllText(pathNameOrdered,
                            dump);
                        try
                        {
                            if (ld.UserName.ToUpper() == "GWA")
                            {
                                var htmlText =
                                    "GWA Ordered Routes are attached with this email. These Routes will appear in the TMS for the designated Advance Date - usually the next working day\n"
                                    + "\nEDI Filename: " + ediFilename;
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception Occurred while sending Routes as attachment:" + ediFilename +
                                " Exception Details:" + e.Message, "EdifactExtractor");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred while using Google Directions API to create an optimized Route, EDI filename:" +
                        ediFilename + " Exception Details:" + e.Message, "EdifactExtractor");
                }

            }
            return consolidatedJob;
        }
    }
}

