using Core;
using CsvHelper;
using CsvHelper.Configuration;
using Data;
using Data.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace XCabBookingFileExtractor.Officeworks
{
    public class OfficeworksTextFileHelper : IOfficeworksTextFileHelper
    {
        public ICollection<Booking> ConvertTextFile(string filePath, string stateAbrrv, string jobType)
        {
            var bookings = new List<Booking>();
            var booking = new Booking();
            var itemList = new List<Item>();
            var remarks = new List<string>();
            int lineNumber = 0;
            var errorList = string.Empty;

            if (!File.Exists(filePath))
                return bookings;

            string text = File.ReadAllText(filePath);
            var cvsFile = (filePath).Replace(".text", string.Empty) + ".csv";
            File.WriteAllText(cvsFile, text.Replace("\""," "));

            var listOfExtract = new List<OfficeworksTextRow>();
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = "|",
                    HasHeaderRecord = false,
                    MissingFieldFound = null,
                    BadDataFound = null,
                };
                using (var reader = new StreamReader(cvsFile))
                using (var csvReader = new CsvReader(reader, config))
                {
                    listOfExtract = csvReader.GetRecords<OfficeworksTextRow>().ToList();
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    $"Exception Occurred while converting text file to CSV for Officeworks, Exception: {e.Message}", "OfficeworksBooking");
                return bookings;
            }

            try
            {
                var nextWorkingDay = DateTime.Parse(new CalculateDates().GetNextWorkingDayInclusiveSaturday(DateTime.Now, 1, stateAbrrv, true).ToString("dd/MM/yyyy ") + (jobType == "RET" ? "09:30 AM" : "06:00 AM"));

                foreach (var extract in listOfExtract)
                {
                    lineNumber += 1;
                    if (string.IsNullOrWhiteSpace(extract.Column01) && (extract.Column02 == "H" || extract.Column02 == "P" || extract.Column02 == "D"))
                    {

                        if (extract.Column02 == "H")
                        {
                            if (booking != null && !string.IsNullOrEmpty(booking.Ref1))
                            {
                                if (!string.IsNullOrEmpty(booking.FromPostcode) && !string.IsNullOrEmpty(booking.Ref2))
                                {
                                    if (itemList.Count > 0)
                                        booking.lstItems = itemList;

                                    if (remarks.Count > 0)
                                    {
                                        var tempRemarks = new List<string>();
                                        foreach (var item in remarks.GroupBy(x => x).ToList())
                                        {
                                            tempRemarks.Add("Qty " + remarks.Where(x => x == item.Key).ToList().Count + " " + item.Key);
                                        }

                                        tempRemarks.Add(DgDefaultLineGoodsInstr);
                                        tempRemarks.Add(DgDefaultLinePackagingInstr);

                                        booking.Remarks = tempRemarks;
                                    }

                                    bookings.Add(booking);
                                }
                                booking = new Booking();
                                itemList = new List<Item>();
                                remarks = new List<string>();
                            }

                            booking.Ref1 = extract.Column03.Trim();
                            booking.TotalWeight = extract.Column07.Trim();
                            booking.ATL = extract.Column11.Trim().ToLower().Contains("authority to leave") ? true : false;
                            booking.ExtraDelInformation = extract.Column11.Trim().Replace("* Authority to Leave", string.Empty).Replace("* ", string.Empty).Trim();
                            booking.DespatchDateTime = nextWorkingDay;

                            try
                            {
                                if (!string.IsNullOrEmpty(extract.Column12))
                                {
                                    var listOfExtras = new List<ExtraFields>();
                                    var extraItem = new ExtraFields();

                                    extraItem.Key = "ProvidedRoute";
                                    extraItem.Value = extract.Column12.Trim();
                                    listOfExtras.Add(extraItem);

                                    booking.lstExtraFields = listOfExtras;
                                }
                            }
                            catch (Exception)
                            {
                                errorList += "Line : " + lineNumber + " " + createRow(extract) + "<br>";
                            }
                        }
                        else if (extract.Column02 == "P")
                        {
                            try
                            {
                                booking.FromDetail1 = extract.Column06.Substring(0, 35).Trim();
                                booking.FromDetail2 = jobType == "RET" ? extract.Column06.Substring(70, 35).Trim() : extract.Column06.Substring(35, 35).Trim();
                                booking.FromSuburb = jobType == "RET" ? extract.Column06.Substring(105, 35).Trim().ToUpper() : extract.Column06.Substring(70, 35).Trim().ToUpper();
                                booking.FromPostcode = jobType == "RET" ? extract.Column06.Substring(144, 4).Trim() : extract.Column06.Substring(109, 4).Trim();
                                booking.FromDetail3 = jobType == "RET" ? extract.Column06.Substring(35, 35).Trim() : null;

                                if (booking.lstExtraFields != null && booking.lstExtraFields.Count > 0)
                                {
                                    if (string.IsNullOrEmpty(booking.FromDetail3))
                                    {
                                        booking.FromDetail3 = booking.lstExtraFields.Find(x => x.Key == "ProvidedRoute") == null ? null : booking.lstExtraFields.Find(x => x.Key == "ProvidedRoute").Value;
                                    }
                                    else
                                    {
                                        booking.FromDetail4 = booking.lstExtraFields.Find(x => x.Key == "ProvidedRoute") == null ? null : booking.lstExtraFields.Find(x => x.Key == "ProvidedRoute").Value;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                errorList += "Line : " + lineNumber + " " + createRow(extract) + "<br>";
                            }
                        }
                        else if (extract.Column02 == "D")
                        {
                            try
                            {
                                booking.ToDetail1 = extract.Column07.Substring(0, 35).Trim();
                                booking.ToDetail2 = jobType == "RET" ? extract.Column07.Substring(35, 35).Trim() : extract.Column07.Substring(70, 35).Trim();
                                booking.ToSuburb = jobType == "RET" ? extract.Column07.Substring(70, 35).Trim().ToUpper() : extract.Column07.Substring(105, 35).Trim().ToUpper();
                                booking.ToPostcode = jobType == "RET" ? extract.Column07.Substring(109, 4).Trim() : extract.Column07.Substring(145, 4).Trim();
                                booking.ToDetail3 = jobType == "RET" ? null : extract.Column07.Substring(35, 35).Trim();
                                booking.Ref2 = jobType == "RET" ? extract.Column12.Trim() : extract.Column10.Trim();
                                booking.Caller = extract.Column05.Trim();

                                if (!string.IsNullOrEmpty(extract.Column15))
                                {
                                    remarks.Add("Class " + extract.Column15.Substring(7, extract.Column15.Length - 7) + " SubCls " + extract.Column15.Substring(7, extract.Column15.Length - 7) + " Pack Grp  " + extract.Column15.Substring(4, 3).Trim() + " UnNum " + extract.Column15.Substring(0, 4));
                                }

                                var item = new Item() { Barcode = jobType == "RET" ? string.Empty : extract.Column12.Trim(), Quantity = 1 };
                                itemList.Add(item);
                            }
                            catch (Exception)
                            {
                                errorList += "Line : " + lineNumber + " " + createRow(extract) + "<br>";
                            }
                        }
                    }
                }

                //Add last job details
                if (booking != null && !string.IsNullOrEmpty(booking.FromPostcode) && !string.IsNullOrEmpty(booking.Ref2))
                {
                    booking.lstItems = itemList;

                    if (remarks.Count > 0)
                    {
                        var tempRemarks = new List<string>();
                        foreach (var item in remarks.GroupBy(x => x).ToList())
                        {
                            tempRemarks.Add("Qty " + remarks.Where(x => x == item.Key).ToList().Count + " " + item.Key);
                        }

                        tempRemarks.Add(DgDefaultLineGoodsInstr);
                        tempRemarks.Add(DgDefaultLinePackagingInstr);

                        booking.Remarks = tempRemarks;
                    }

                    bookings.Add(booking);
                }

                booking = null;
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    $"Exception Occurred while extracting data form CSV file which was converted from text file for Officeworks. File : " + filePath + " Line : " + lineNumber + " Exception: " + e.Message, "OfficeworksBooking");
                return bookings;
            }

            try
            {
                if (!string.IsNullOrEmpty(errorList))
                {
                    var toEmailAddress = string.Empty;

                    switch (stateAbrrv)
                    {
                        case "VIC":
                            toEmailAddress = "ContractsVIC@capitaltransport.com.au";
                            break;
                        case "NSW":
                            toEmailAddress = "ContractsSYD@capitaltransport.com.au";
                            break;
                        case "QLD":
                            toEmailAddress = "ContractsQLD@capitaltransport.com.au";
                            break;
                        case "SA":
                            toEmailAddress = "ContractsSA@capitaltransport.com.au";
                            break;
                        case "WA":
                            toEmailAddress = "ContractsWA@capitaltransport.com.au";
                            break;
                        default:
                            toEmailAddress = "";
                            break;
                    }

                    var emailBody = "Following Officeworks file contains some erroneous data  <br><br>File : " + Path.GetFileName(filePath) + "<br><br>" + errorList + "<br><br></font><font face=\"georgia\" color=\"red\">Above bookings may need to be manually created in TPlus.<br>Please contact IT department before creating bookings.<font>";
                    SendMail.SendHtmlEMailWithAttachment(emailBody, null, "Erroneous Data in Officeworks Booking File", toEmailAddress, "Software-Development@capitaltransport.com.au");
                    //toEmailAddress = "dwijayasiri@capitaltransport.com.au";
                    //SendMail.SendHtmlEMailWithAttachment(emailBody, null, "Erroneous Data in Officeworks Booking File", toEmailAddress, "dwijayasiri@capitaltransport.com.au");
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    $"Error occured while sending email with the error list, Exception: {e.Message}", "OfficeworksBooking");
                return bookings;
            }

            try
            {
                if (File.Exists(cvsFile))
                    File.Delete(cvsFile);
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    $"Cannot delet Officeworks temp CSV file, Exception: {e.Message}", "OfficeworksBooking");
                return bookings;
            }

            return bookings;
        }

        private string createRow(OfficeworksTextRow row)
        {
            return (row.Column01 + "|" + row.Column02 + "|" + row.Column03 + "|" + row.Column04 + "|" + row.Column05 +
              "|" + row.Column06 + "|" + row.Column07 + "|" + row.Column08 + "|" + row.Column09 + "|" + row.Column10 +
              "|" + row.Column11 + "|" + row.Column12 + "|" + row.Column13 + "|" + row.Column14 + "|" + row.Column15 +
              "|" + row.Column16 + "|" + row.Column17 + "|" + row.Column18 + "|" + row.Column19 + "|" + row.Column20 + "|" + row.Column20 + "|");
        }

        private const string DgDefaultLinePackagingInstr = "Packaged in accordance with the latest DG code. Make sure you have DG docs placed in your door holder.";
        private const string DgDefaultLineGoodsInstr = "Goods to be secured inside gates or sides with no more than 30% of the highest layer above top of sides.";

    }
}
