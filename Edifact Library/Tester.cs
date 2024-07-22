using Core;
using EDIFACT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Edifact_Library
{
    class Tester
    {



        public static void EdiTesterV1()
        {
            //EDI Tester
            string _ediFilename = @"GWA.edi";

            EDIMessage msg = new EDIMessage(File.ReadAllText(_ediFilename), true);
            // msg.RawMessage = 
            // msg.RawMessage = File.ReadAllText(_ediFilename);
            SegmentCollection[] segmentCollections = msg.GetAllSegmentCollections(_ediFilename);
            XmlDocument[] xmlNodes = msg.SerializeToXml();
            String xmlText = xmlNodes.ToString();
            Dictionary<string, Booking> deliveryDictionary = new Dictionary<string, Booking>();

            List<Booking> lstBooking = new List<Booking>();
            foreach (var segment in segmentCollections)
            {
                var totalWeightFound = false;
                var totalVolumeFound = false;
                //create a new Booking object            
                Booking booking = new Booking();
                //Create a list of Items to hold against a booking
                List<Item> lstItems = new List<Item>();
                //first item is empty
                Item item = null;
                foreach (Segment o in segment)
                {

                    if (o.Name == "CNT")
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "11")
                        {
                            // Debug.Write("Found Number of items");
                            booking.TotalItems = fc.Item(1).Value;
                            Debug.WriteLine("Items for segment:" + fc.Item(1).Value);
                        }
                    }
                    if (o.Name == "DTM")
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "11")
                        {
                            DateTime dt = DateTime.ParseExact(fc.Item(1).Value, "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture);
                            booking.DespatchDateTime = dt;
                        }
                    }
                    if (o.Name == "RFF")
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "CN")
                        {
                            booking.Ref1 = fc.Item(1).Value;
                            Debug.WriteLine("Reference 1 (consignment Number) for segment:" + fc.Item(1).Value);
                        }
                        else if (fc.Item(0).Value == "ADE")
                        {
                            booking.Ref2 = fc.Item(1).Value;
                            Debug.WriteLine("Reference 2 (Shipment Number) for segment:" + fc.Item(1).Value);
                        }

                    }
                    if (o.Name == "NAD")
                    {
                        var fc = o.Fields;
                        /** if (fc.Item(0).Value == "SU")
                         {
                             //Debug.Write("Found Pickup Address");
                             var pickupAddressLine1 = "";
                             var pickupAddressLine2 = "";
                             var pickupAddressLine3 = "";
                             var pickupPostcode = "";
                             pickupAddressLine1 = fc.Item(5).Value;
                             pickupAddressLine2 = fc.Item(6).Value;
                             pickupAddressLine3 = fc.Item(7).Value;
                             pickupPostcode = fc.Item(9).Value;
                             booking.FromDetail1 = pickupAddressLine1.Replace(',',' ');
                             booking.FromDetail2 = pickupAddressLine2.Replace(',', ' ');
                             booking.FromSuburb = pickupAddressLine3.Replace(',', ' ');
                             booking.FromPostcode = pickupPostcode;
                             Debug.WriteLine("PU Address Line 1:" + pickupAddressLine1);
                             Debug.WriteLine("PU Address Line 2:" + pickupAddressLine2);
                             Debug.WriteLine("PU Address Line 3:" + pickupAddressLine3);
                             Debug.WriteLine("PU Postcode:" + pickupPostcode);
                         }
                         * **/
                        if (fc.Item(0).Value == "BY")
                        {
                            //Debug.Write("Found Pickup Address");
                            var pickupAddressLine1 = "";
                            var pickupAddressLine2 = "";
                            var pickupAddressLine3 = "";
                            var pickupPostcode = "";
                            pickupAddressLine1 = fc.Item(5).Value;
                            pickupAddressLine2 = fc.Item(6).Value;
                            pickupAddressLine3 = fc.Item(7).Value;
                            pickupPostcode = fc.Item(9).Value;
                            booking.FromDetail1 = pickupAddressLine1.Replace(',', ' ');
                            booking.FromDetail2 = pickupAddressLine2.Replace(',', ' ');
                            booking.FromSuburb = pickupAddressLine3.Replace(',', ' ');
                            booking.FromPostcode = pickupPostcode;
                            Debug.WriteLine("PU Address Line 1:" + pickupAddressLine1);
                            Debug.WriteLine("PU Address Line 2:" + pickupAddressLine2);
                            Debug.WriteLine("PU Address Line 3:" + pickupAddressLine3);
                            Debug.WriteLine("PU Postcode:" + pickupPostcode);
                        }
                        else if (fc.Item(0).Value == "UD")
                        {
                            //Debug.Write("Found Delivery Address");
                            var deliveryAddressLine1 = "";
                            var deliveryAddressLine2 = "";
                            var deliveryAddressLine3 = "";
                            var deliveryPostcode = "";
                            deliveryAddressLine1 = fc.Item(5).Value;
                            deliveryAddressLine2 = fc.Item(6).Value;
                            deliveryAddressLine3 = fc.Item(7).Value;
                            deliveryPostcode = fc.Item(9).Value;
                            booking.ToDetail1 = deliveryAddressLine1.Replace(',', ' ');
                            booking.ToDetail2 = deliveryAddressLine2.Replace(',', ' ');
                            booking.ToSuburb = deliveryAddressLine3.Replace(',', ' ');
                            booking.ToPostcode = deliveryPostcode;
                            Debug.WriteLine("DEL Address Line 1:" + deliveryAddressLine1);
                            Debug.WriteLine("DEL Address Line 2:" + deliveryAddressLine2);
                            Debug.WriteLine("DEL Address Line 3:" + deliveryAddressLine3);
                            Debug.WriteLine("DEL Postcode:" + deliveryPostcode);
                        }
                    }
                    if (o.Name == "MEA")
                    {
                        var fc = o.Fields;


                        if (item == null)
                            item = new Item();
                        /**    else if (item.Weight != 0 && item.Cubic != 0)
                            {
                                lstItems.Add(item);
                                item = new Item();
                            }
                         * **/
                        if (fc.Item(2).Value == "KGM" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "G" && !totalWeightFound)
                        {
                            var weight = "";
                            weight = fc.Item(3).Value;
                            Debug.WriteLine("Total Weight" + weight);
                            booking.TotalWeight = weight;
                            totalWeightFound = true;
                        }
                        else if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ" && !totalVolumeFound)
                        {
                            var volume = "";
                            volume = fc.Item(3).Value;
                            Debug.WriteLine("Total Volume" + volume);
                            booking.TotalVolume = fc.Item(3).Value;
                            totalVolumeFound = true;
                        }
                        else if (fc.Item(2).Value == "KGM" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "AAB" && totalWeightFound)
                        {
                            var weight = "";
                            weight = fc.Item(3).Value;
                            item.Weight = Convert.ToDouble(weight);
                            Debug.WriteLine("Item Weight" + weight);
                        }
                        else if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ" && totalWeightFound)
                        {
                            var volume = "";
                            volume = fc.Item(3).Value;
                            item.Cubic = Convert.ToDouble(volume);
                            Debug.WriteLine("Item Volume" + volume);

                        }

                    }
                    else if (o.Name == "GIN")
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "BJ")
                        {
                            var barcode = "";
                            barcode = fc.Item(1).Value;
                            item.Barcode = barcode;
                            Debug.WriteLine("Barcode" + barcode);
                            lstItems.Add(item);
                            item = new Item();

                        }
                    }
                }
                booking.lstItems = lstItems;
                lstBooking.Add(booking);
                /**
               // get pickup address from the 11 th segment
                if (segment[10] != null)
                {
                    var pickupAddress = "";
                    //get pickup address
                    FieldCollection fCollection = segment[10].Fields;
                    if (fCollection.Item(5) != null)
                        pickupAddress += fCollection.Item(5).Value + " ";
                    if (fCollection.Item(6) != null)
                        pickupAddress += fCollection.Item(6).Value + " ";
                    if (fCollection.Item(7) != null)
                        pickupAddress += fCollection.Item(7).Value;
                    // List<T> lst = (List<T>) fCollection;

                    Debug.WriteLine("Pickup Address:" + pickupAddress);

                }
                //get delivery address
                if (segment[14]!=null){
                    var deliveryAddress = "";
                    FieldCollection fCollectionDel = segment[14].Fields;
                    if (fCollectionDel.Item(5) != null)
                        deliveryAddress += fCollectionDel.Item(5).Value + " ";
                    if (fCollectionDel.Item(6) != null)
                        deliveryAddress += fCollectionDel.Item(6).Value + " ";
                    if (fCollectionDel.Item(7) != null)
                        deliveryAddress += fCollectionDel.Item(7).Value;
                  

                    Debug.WriteLine("Delivery Address:" + deliveryAddress);
                   
                }
                 //get despatch date time
                if (segment[2] != null)
                {
                    var despatchDateTime = "";
                    FieldCollection fCollectionDespatchDateTime = segment[2].Fields;
                    if (fCollectionDespatchDateTime.Item(1) != null)
                        despatchDateTime += fCollectionDespatchDateTime.Item(1).Value;
                    
                    //DateTime _despatchDateTime = Convert.ToDateTime(despatchDateTime, "yyyyMMdd");
                    //Debug.WriteLine("Despatch Date Time:" + _despatchDateTime.ToString());
                    Debug.WriteLine("Despatch Date Time:" + despatchDateTime);
                }
                //get items
                if (segment[17] != null)
                {

                }
                */
            }

            String dump = "";
            int index = 1;
            foreach (var booking in lstBooking)
            {
                dump += index.ToString() + ","
                       + booking.DespatchDateTime.ToString() + ","
                       + booking.Ref1 + ","
                       + booking.Ref2 + ","
                       + booking.FromDetail1 + ","
                       + booking.FromDetail2 + ","
                       + booking.FromDetail3 + ","
                       + booking.FromDetail4 + ","
                       + booking.FromSuburb + ","
                       + booking.FromPostcode + ","
                       + booking.ToDetail1 + ","
                       + booking.ToDetail2 + ","
                       + booking.ToDetail3 + ","
                       + booking.ToDetail4 + ","
                       + booking.ToSuburb + ","
                       + booking.ToPostcode + ","
                       + booking.TotalItems + ","
                       + booking.TotalWeight + ","
                        + booking.TotalVolume + ","
                    ;
                foreach (var item in booking.lstItems)
                {
                    if (item.Barcode.Length > 0)
                        dump += item.Barcode + " ";
                }
                dump += "\n";
                index++;
            }
            File.WriteAllText("dump.csv", dump);
            //RabbitMqClient.SendMessage();
            // String  test  = "MS00073521";
            //  Console.Write(Regex.Matches(test, @"[a-zA-Z0-9_-]$").Count);
            //string text = System.IO.File.ReadAllText(@"C:\xCabXmlFolder\Processed\Response.xml");
            //ExtractJobNumberFromXml(text);
        }

        public static void Main(string[] args)
        {
            //EdiTesterV1();
            //var lstBookings =  EdifactExtractor.Parse(@"GWA_DESADV_sample EDIFACT file for CAPITAL.edi", true);
            //  var lstBookings = EdifactExtractor.Parse(@"GWA.edi", true,true);
            var ld = new LoginDetails { Id = "9" };
            //   var lstBookings = EdifactExtractor.Parse(@"GWA.edi", ld,true, true);
            //  var lstBookings = EdifactExtractor.Parse(@"QLD-400MD1_20160705080522.edi",ld, true, true);
            // EdiTesterV2();


        }

        public static void EdiTesterV2()
        {
            //EDI Tester
            //string _ediFilename = @"GWA_DESADV_sample EDIFACT file for CAPITAL.edi";
            string _ediFilename = @"GWA.edi";



            EDIMessage msg = new EDIMessage(File.ReadAllText(_ediFilename), true);
            //XmlDocument[] tr = null;

            string text = File.ReadAllText(_ediFilename);
            //  Parser parser = new Parser(ref text);


            //msg.Transform(_ediFilename, ref tr);
            // msg.RawMessage = 
            // msg.RawMessage = File.ReadAllText(_ediFilename);
            SegmentCollection[] segmentCollections = msg.GetAllSegmentCollections(_ediFilename);

            //  XmlDocument[] xmlNodes = msg.SerializeToXml();
            // String xmlText = xmlNodes.ToString();
            Dictionary<string, Booking> deliveryDictionary = new Dictionary<string, Booking>();

            List<Booking> lstBooking = new List<Booking>();
            foreach (SegmentCollection segment in segmentCollections)
            {
                var DeliveryDocketNumber = "";
                Booking booking = new Booking();
                Boolean itemStartTag = false;
                //  Boolean itemFinishTag = false;
                bool totalVolumeFound = false;
                Item item = null;
                foreach (Segment o in segment)
                {
                    if (o.Name == "NAD")
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "BY")
                        {
                            //Debug.Write("Found Pickup Address");
                            var pickupAddressLine1 = "";
                            var pickupAddressLine2 = "";
                            var pickupAddressLine3 = "";
                            var pickupPostcode = "";
                            pickupAddressLine1 = fc.Item(5).Value;
                            pickupAddressLine2 = fc.Item(6).Value;
                            pickupAddressLine3 = fc.Item(7).Value;
                            pickupPostcode = fc.Item(9).Value;
                            booking.FromDetail1 = pickupAddressLine1.Replace(',', ' ');
                            booking.FromDetail2 = pickupAddressLine2.Replace(',', ' ');
                            booking.FromSuburb = pickupAddressLine3.Replace(',', ' ');
                            booking.FromPostcode = pickupPostcode;
                            Debug.WriteLine("PU Address Line 1:" + pickupAddressLine1);
                            Debug.WriteLine("PU Address Line 2:" + pickupAddressLine2);
                            Debug.WriteLine("PU Address Line 3:" + pickupAddressLine3);
                            Debug.WriteLine("PU Postcode:" + pickupPostcode);
                        }
                        else if (fc.Item(0).Value == "UD")
                        {
                            //Debug.Write("Found Delivery Address");
                            var deliveryAddressLine1 = "";
                            var deliveryAddressLine2 = "";
                            var deliveryAddressLine3 = "";
                            var deliveryPostcode = "";
                            deliveryAddressLine1 = fc.Item(5).Value;
                            deliveryAddressLine2 = fc.Item(6).Value;
                            deliveryAddressLine3 = fc.Item(7).Value;
                            deliveryPostcode = fc.Item(9).Value;
                            booking.ToDetail1 = deliveryAddressLine1.Replace(',', ' ');
                            booking.ToDetail2 = deliveryAddressLine2.Replace(',', ' ');
                            booking.ToSuburb = deliveryAddressLine3.Replace(',', ' ');
                            booking.ToPostcode = deliveryPostcode;
                            Debug.WriteLine("DEL Address Line 1:" + deliveryAddressLine1);
                            Debug.WriteLine("DEL Address Line 2:" + deliveryAddressLine2);
                            Debug.WriteLine("DEL Address Line 3:" + deliveryAddressLine3);
                            Debug.WriteLine("DEL Postcode:" + deliveryPostcode);
                        }
                    }
                    if (o.Name == "RFF")
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "CN")
                        {
                            booking.Ref1 = fc.Item(1).Value;
                            Debug.WriteLine("Reference 1 (consignment Number) for segment:" + fc.Item(1).Value);
                        }
                        else if (fc.Item(0).Value == "ADE")
                        {
                            //  booking.Ref2 = fc.Item(1).Value;
                            Debug.WriteLine("Reference 2 (Shipment Number) for segment:" + fc.Item(1).Value);
                        }

                    }
                    if (o.Name == "DTM")
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "11")
                        {
                            DateTime dt = DateTime.ParseExact(fc.Item(1).Value, "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture);
                            booking.DespatchDateTime = dt;
                        }
                    }
                    if (o.Name == "CPS" && !itemStartTag)
                    {
                        itemStartTag = true;
                        item = new Item();

                    }
                    else if (o.Name == "CPS" && itemStartTag == true)
                    {
                        itemStartTag = false;


                        //if (booking.lstItems==null)
                        //   booking.lstItems = new List<Item>();
                        itemStartTag = false;
                        // item = new Item();

                        // itemFinishTag = true;
                    }

                    if (o.Name == "MEA")
                    {
                        var fc = o.Fields;
                        if (item != null)
                        {
                            if (fc.Item(2).Value == "KGM" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "AAB")
                            {
                                var weight = "";
                                weight = fc.Item(3).Value;
                                item.Weight = Convert.ToDouble(weight);
                                Debug.WriteLine("Item Weight" + weight);
                            }
                            if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ")
                            {
                                var volume = "";
                                volume = fc.Item(3).Value;
                                item.Cubic = Convert.ToDouble(volume);
                                Debug.WriteLine("Item Volume" + volume);
                            }
                        }
                        //if (fc.Item(2).Value == "KGM" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "G" && !totalWeightFound)
                        if (fc.Item(2).Value == "KGM" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "G")
                        {
                            var weight = "";
                            weight = fc.Item(3).Value;
                            Debug.WriteLine("Total Weight" + weight);
                            booking.TotalWeight = weight;

                        }
                        else if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ" && !totalVolumeFound)
                        //else if (fc.Item(2).Value == "MTQ" && fc.Item(0).Value == "PD" && fc.Item(1).Value == "ABJ")
                        {
                            var volume = "";
                            volume = fc.Item(3).Value;
                            Debug.WriteLine("Total Volume" + volume);
                            booking.TotalVolume = fc.Item(3).Value;
                            totalVolumeFound = true;
                        }


                    }
                    if (o.Name == "RFF" && itemStartTag)
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "AAU")
                        {
                            DeliveryDocketNumber = fc.Item(1).Value;


                        }
                    }
                    if (o.Name == "GIN")
                    {
                        var fc = o.Fields;
                        if (fc.Item(0).Value == "BJ")
                        {
                            var barcode = "";
                            barcode = fc.Item(1).Value;
                            item.Barcode = barcode;
                            Debug.WriteLine("Barcode" + barcode);
                            itemStartTag = false;
                            //item = new Item();
                            if (deliveryDictionary.ContainsKey(DeliveryDocketNumber))
                            {
                                Booking existingbooking = deliveryDictionary[DeliveryDocketNumber];
                                existingbooking.lstItems.Add(item);
                                Booking temp = existingbooking;
                                deliveryDictionary[DeliveryDocketNumber] = temp;
                            }
                            else
                            {
                                Booking bk = booking.DeepCopy();
                                bk.lstItems = new List<Item>();
                                bk.lstItems.Add(item);
                                Booking temp = bk;
                                deliveryDictionary.Add(DeliveryDocketNumber, temp);

                            }

                        }
                    }
                }
            }

            String dump = "";
            int index = 1;

            foreach (var key in deliveryDictionary.Keys)
            {
                Booking booking = deliveryDictionary[key];
                dump += index.ToString() + ","
                      + booking.DespatchDateTime.ToString() + ","
                      + booking.Ref1 + "," + key + ","
                      + booking.FromDetail1 + ","
                       + booking.FromDetail2 + ","
                       + booking.FromDetail3 + ","
                       + booking.FromDetail4 + ","
                       + booking.FromSuburb + ","
                       + booking.FromPostcode + ","
                       + booking.ToDetail1 + ","
                       + booking.ToDetail2 + ","
                       + booking.ToDetail3 + ","
                       + booking.ToDetail4 + ","
                       + booking.ToSuburb + ","
                       + booking.ToPostcode + ","
                       //+ booking.TotalItems + ","
                       + booking.lstItems.Count + ","
                       + booking.TotalWeight + ","
                        + booking.TotalVolume + ",";
                foreach (var item in booking.lstItems)
                {
                    dump += "'" + item.Barcode + "',";
                }
                dump = dump.Substring(0, dump.Length - 1);
                dump += "\n";
                index++;
            }
            File.WriteAllText("dump4.csv", dump);

            /**foreach (var booking in lstBooking)
            {
                dump += index.ToString() + ","
                       + booking.DespatchDateTime.ToString() + ","
                       + booking.Ref1 + ","
                       + booking.Ref2 + ","
                       + booking.FromDetail1 + ","
                       + booking.FromDetail2 + ","
                       + booking.FromDetail3 + ","
                       + booking.FromDetail4 + ","
                       + booking.FromSuburb + ","
                       + booking.FromPostcode + ","
                       + booking.ToDetail1 + ","
                       + booking.ToDetail2 + ","
                       + booking.ToDetail3 + ","
                       + booking.ToDetail4 + ","
                       + booking.ToSuburb + ","
                       + booking.ToPostcode + ","
                       + booking.TotalItems + ","
                       + booking.TotalWeight + ","
                        + booking.TotalVolume + ","
                    ;
                foreach (var item in booking.lstItems)
                {
                    if (item.Barcode.Length > 0)
                        dump += item.Barcode + " ";
                }
                dump += "\n";
                index++;
            }
            File.WriteAllText("dump.csv", dump);
             * **/
            //RabbitMqClient.SendMessage();
            // String  test  = "MS00073521";
            //  Console.Write(Regex.Matches(test, @"[a-zA-Z0-9_-]$").Count);
            //string text = System.IO.File.ReadAllText(@"C:\xCabXmlFolder\Processed\Response.xml");
            //ExtractJobNumberFromXml(text);
        }
    }
}
