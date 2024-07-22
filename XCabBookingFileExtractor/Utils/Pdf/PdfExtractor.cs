using Core;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace XCabBookingFileExtractor.Utils.Pdf
{
    public class PdfExtractor : IExtractor
    {
        // Regex to identify job row
        static Regex rgxJob = new Regex(@"\(\s\s\s\s\s\s[A-Z]*");
        // Regex to identify the Store name row
        static Regex rgxStore = new Regex(@"\([0-9][0-9]/[0-9][0-9]/[0-9][0-9]*");
        static Regex rgxStoreIText = new Regex(@"[0-9][0-9]/[0-9][0-9]/[0-9][0-9]*");
        static Regex rgxNumber = new Regex(@"[\d]");
        static string RunsheetMatchStr = "Runsheet for Run No";
        private Regex regxRunSheet = new Regex(@"Runsheet for Run No: ([^\s]+)");
        static Regex rgxPostcodeNumber = new Regex(@"[A-Za-z]{2,3}.[\d]{4}");
        /// <summary>
        /// Method to read through PDF and collect booking details
        /// </summary>
        /// <param name="FileLocation">Full file path and name with extension</param>
        public Dictionary<string, List<Booking>> Extract(string FileLocation)
        {
            var routeDictionary = new Dictionary<string, List<Booking>>();
            var bookings = new List<Booking>();
            var store = string.Empty;
            var runNumber = string.Empty;
            try
            {
                // Using plain streams in this instance is more performant as the PDF is not complex
                using (FileStream fs = new FileStream(FileLocation, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // While there is another row to read into
                        while (sr.Peek() >= 0)
                        {
                            var tmp = sr.ReadLine();

                            // 
                            if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
                            {
                                sr.ReadLine(); // Skip formatting line
                                tmp = sr.ReadLine();

                                // If this row contains the store name, save it once. It is then injected into subsequent bookings.
                                if (rgxStore.IsMatch(tmp))
                                {
                                    store = tmp.Substring(55, 60).Trim();
                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    //if (tmp.Contains(RunsheetMatchStr))
                                    //{
                                    //    runNumber = tmp.Substring(77, 2).Trim();
                                    //}

                                    if (regxRunSheet.IsMatch(tmp))
                                    {
                                        var results = regxRunSheet.Matches(tmp);
                                        if (results.Count > 0)
                                        {
                                            runNumber = results[0].Value.Replace("Runsheet for Run No:", "");
                                        }
                                    }
                                }
                                else if (rgxJob.IsMatch(tmp) && !tmp.Contains("Invoice"))
                                {
                                    var tmpBooking = new Booking
                                    {
                                        Ref2 = store,
                                        ToDetail1 = tmp.Substring(37, 28).Trim(),
                                        Ref1 = tmp.Substring(67, 10).Trim(),
                                        //RunNumber = runNumber
                                    };

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    tmpBooking.ToDetail2 = tmp.Substring(37, 28).Trim();

                                    sr.ReadLine(); // Skip formatting line

                                    tmp = sr.ReadLine();
                                    tmp = tmp.Substring(36, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
                                    if (rgxNumber.IsMatch(tmp))
                                    {
                                        //that means we have a string that includes suburb state & postcode
                                        //eg: TOORAK VIC 3142
                                        var strLen = tmp.Length;
                                        tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                        var state = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                        tmpBooking.StateId = "1";
                                        tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                    }
                                    else
                                    {
                                        tmpBooking.ToSuburb = tmp;
                                        //work out state & postcode
                                        sr.ReadLine(); // Skip formatting line
                                        tmp = sr.ReadLine();
                                        tmp = tmp.Substring(36, 28).Trim();

                                        var statePostcode = tmp.Split(' ');
                                        if (statePostcode != null && statePostcode.Length > 0)
                                        {
                                            tmpBooking.StateId = statePostcode[0];
                                            tmpBooking.ToPostcode = statePostcode[1];
                                        }
                                    }

                                    //int strLen = tmp.Length ;

                                    //tmpBooking.Postcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                    //tmpBooking.State = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                    //tmpBooking.Suburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state

                                    bookings.Add(tmpBooking);

                                }
                            }
                        }
                    }
                }
                routeDictionary.Add(runNumber, bookings);
            }
            catch (Exception)
            {
                bookings = null;
                throw;
            }

            return routeDictionary;
        }

        public Dictionary<string, List<Booking>> ExtractForMerBenzCity(string FileLocation)
        {
            var routeDictionary = new Dictionary<string, List<Booking>>();
            var bookings = new List<Booking>();
            var store = string.Empty;
            var runNumber = string.Empty;
            try
            {
                // Using plain streams in this instance is more performant as the PDF is not complex
                using (FileStream fs = new FileStream(FileLocation, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // While there is another row to read into
                        while (sr.Peek() >= 0)
                        {
                            var tmp = sr.ReadLine();

                            // 
                            if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
                            {
                                sr.ReadLine(); // Skip formatting line
                                tmp = sr.ReadLine();

                                // If this row contains the store name, save it once. It is then injected into subsequent bookings.
                                if (rgxStore.IsMatch(tmp))
                                {
                                    store = tmp.Substring(54, 60).Trim();
                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    if (tmp.Contains(RunsheetMatchStr))
                                    {
                                        runNumber = tmp.Substring(77, 2).Trim();
                                    }
                                }
                                else if (rgxJob.IsMatch(tmp) && !tmp.Contains("Invoice"))
                                {
                                    var tmpBooking = new Booking
                                    {
                                        Ref2 = store,
                                        ToDetail1 = tmp.Substring(37, 28).Trim(),
                                        Ref1 = tmp.Substring(67, 10).Trim(),
                                        //RunNumber = runNumber
                                    };

                                    //change Ref2 to indicate the customer name
                                    var customerName = tmp.Substring(7, 28).Trim();
                                    if (!string.IsNullOrEmpty(customerName))
                                        tmpBooking.Ref2 = customerName;

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    tmpBooking.ToDetail2 = tmp.Substring(37, 29).Trim();

                                    sr.ReadLine(); // Skip formatting line

                                    tmp = sr.ReadLine();
                                    tmp = tmp.Substring(36, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
                                    if (rgxNumber.IsMatch(tmp))
                                    {
                                        //that means we have a string that includes suburb state & postcode
                                        //eg: TOORAK VIC 3142
                                        var strLen = tmp.Length;
                                        tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                        var state = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                        tmpBooking.StateId = "1";
                                        tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                    }
                                    else
                                    {
                                        tmpBooking.ToSuburb = tmp;
                                        //work out state & postcode
                                        sr.ReadLine(); // Skip formatting line
                                        tmp = sr.ReadLine();
                                        tmp = tmp.Substring(36, 28).Trim();

                                        var statePostcode = tmp.Split(' ');
                                        if (statePostcode != null && statePostcode.Length > 0)
                                        {
                                            tmpBooking.StateId = statePostcode[0];
                                            tmpBooking.ToPostcode = statePostcode[1];
                                        }
                                    }

                                    bookings.Add(tmpBooking);
                                }
                            }
                        }
                    }
                }
                routeDictionary.Add(runNumber, bookings);
            }
            catch (Exception)
            {
                bookings = null;
                throw;
            }

            return routeDictionary;
        }

        public Dictionary<string, List<Booking>> ExtractForMerBenzToowong(string FileLocation, string stateName, string stateAbbr, string stateId)
        {
            var routeDictionary = new Dictionary<string, List<Booking>>();
            var bookings = new List<Booking>();
            var store = string.Empty;
            var runNumber = string.Empty;
            try
            {
                // Using plain streams in this instance is more performant as the PDF is not complex
                using (FileStream fs = new FileStream(FileLocation, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // While there is another row to read into
                        while (sr.Peek() >= 0)
                        {
                            var tmp = sr.ReadLine();

                            // 
                            if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
                            {
                                sr.ReadLine(); // Skip formatting line
                                tmp = sr.ReadLine();

                                // If this row contains the store name, save it once. It is then injected into subsequent bookings.
                                if (rgxStore.IsMatch(tmp))
                                {
                                    store = tmp.Substring(54, 60).Trim();

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    if (tmp.Contains(RunsheetMatchStr))
                                    {
                                        if (runNumber.Length > 0)
                                        {
                                            if (runNumber != tmp.Substring(76, 3).Trim())
                                            {
                                                var shallowCopyList = new List<Booking>(bookings);
                                                //this would indicate that the PDF contains multiple runsheets
                                                routeDictionary.Add(runNumber, shallowCopyList);
                                                bookings.Clear();
                                            }
                                        }
                                        runNumber = tmp.Substring(76, 3).Trim();
                                    }
                                }
                                else if (rgxJob.IsMatch(tmp) && !tmp.Contains("Invoice"))
                                {
                                    var tmpBooking = new Booking
                                    {
                                        Ref2 = store,
                                        ToDetail1 = tmp.Substring(37, 28).Trim(),
                                        Ref1 = tmp.Substring(67, 10).Trim(),
                                        //RunNumber = runNumber
                                    };

                                    //change Ref2 to indicate the customer name
                                    var customerName = tmp.Substring(7, 28).Trim();
                                    if (!string.IsNullOrEmpty(customerName))
                                        tmpBooking.Ref2 = customerName;

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    tmpBooking.ToDetail2 = tmp.Substring(37, 29).Trim();

                                    sr.ReadLine(); // Skip formatting line

                                    tmp = sr.ReadLine();
                                    tmp = tmp.Substring(36, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
                                    if (tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr).Contains(stateAbbr))
                                    {
                                        tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                        //that means we have a string that includes suburb state & postcode
                                        //eg: TOORAK VIC 3142
                                        var strLen = tmp.Length;
                                        tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                        var state = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                        tmpBooking.StateId = stateId;
                                        tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                    }
                                    else
                                    {
                                        tmpBooking.ToDetail3 = tmp;

                                        // work out state & postcode
                                        sr.ReadLine(); // Skip formatting line
                                        tmp = sr.ReadLine();
                                        tmp = tmp.Substring(36, 28).Trim();

                                        tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                        var statePostcode = tmp.Replace(stateAbbr, "|").Split('|');
                                        if (statePostcode != null && statePostcode.Length == 2)
                                        {
                                            if (string.IsNullOrEmpty(statePostcode[0]))
                                            {
                                                tmpBooking.StateId = stateId;
                                                tmpBooking.ToSuburb = tmpBooking.ToDetail3;
                                                tmpBooking.ToPostcode = statePostcode[1];
                                                tmpBooking.ToDetail3 = string.Empty;
                                            }
                                            else
                                            {
                                                tmpBooking.StateId = stateId;
                                                tmpBooking.ToSuburb = statePostcode[0];
                                                tmpBooking.ToPostcode = statePostcode[1];
                                            }
                                        }
                                        else
                                        {
                                            tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                            var strLen = tmp.Length;
                                            tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                            tmpBooking.StateId = stateId;
                                            tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                        }
                                    }

                                    bookings.Add(tmpBooking);

                                }
                            }
                        }
                    }
                }
                routeDictionary.Add(runNumber, bookings);
            }
            catch (Exception)
            {
                bookings = null;
                throw;
            }

            return routeDictionary;
        }

        public Dictionary<string, List<Booking>> ExtractForMazdaPartsWA(string FileLocation, string stateName, string stateAbbr, string stateId)
        {
            var routeDictionary = new Dictionary<string, List<Booking>>();
            var bookings = new List<Booking>();
            var store = string.Empty;
            var runNumber = string.Empty;
            try
            {
                // Using plain streams in this instance is more performant as the PDF is not complex
                using (FileStream fs = new FileStream(FileLocation, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // While there is another row to read into
                        while (sr.Peek() >= 0)
                        {
                            var tmp = sr.ReadLine();

                            // 
                            if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
                            {
                                sr.ReadLine(); // Skip formatting line
                                tmp = sr.ReadLine();
                                if (tmp == "ET" || tmp.ToLower() == "endstream" || tmp == "")// avoid page breaks
                                {
                                    while (sr.Peek() >= 0)
                                    {
                                        tmp = sr.ReadLine();
                                        if (tmp == "BT")
                                        {
                                            sr.ReadLine();
                                            tmp = sr.ReadLine();
                                            break;
                                        }
                                    }
                                }

                                if (tmp.ToUpper().Contains("<<FF>>"))
                                {
                                    sr.ReadLine(); // Skip additional line
                                    tmp = sr.ReadLine();
                                }

                                // If this row contains the store name, save it once. It is then injected into subsequent bookings.
                                if (rgxStore.IsMatch(tmp))
                                {
                                    store = tmp.Substring(54, 60).Trim();

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    if (tmp.Contains(RunsheetMatchStr))
                                    {
                                        if (runNumber.Length > 0)
                                        {
                                            if (runNumber != tmp.Substring(74, 8).Replace(":", "").Trim())
                                            {
                                                var shallowCopyList = new List<Booking>(bookings);
                                                //this would indicate that the PDF contains multiple runsheets
                                                routeDictionary.Add(runNumber, shallowCopyList);
                                                bookings.Clear();
                                            }
                                        }
                                        runNumber = tmp.Substring(74, 8).Replace(":", "").Trim();
                                    }
                                }
                                else if (rgxJob.IsMatch(tmp) && !tmp.Contains("Invoice"))
                                {
                                    var tmpBooking = new Booking
                                    {
                                        Ref2 = store,
                                        ToDetail1 = tmp.Substring(37, 28).Trim(),
                                        Ref1 = tmp.Substring(67, 10).Trim(),
                                        //RunNumber = runNumber
                                    };

                                    //change Ref2 to indicate the customer name
                                    var customerName = tmp.Substring(7, 28).Trim();
                                    if (!string.IsNullOrEmpty(customerName))
                                        tmpBooking.Ref2 = customerName;

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    if (tmp == "ET" || tmp.ToLower() == "endstream" || tmp == "")// avoid page breaks
                                    {
                                        while (sr.Peek() >= 0)// avoid page breaks
                                        {
                                            tmp = sr.ReadLine();
                                            if (tmp == "BT")
                                            {
                                                sr.ReadLine();
                                                tmp = sr.ReadLine();
                                                break;
                                            }
                                        }
                                    }
                                    tmpBooking.ToDetail2 = tmp.Substring(37, 29).Trim();

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    if (tmp == "ET" || tmp.ToLower() == "endstream" || tmp == "")// avoid page breaks
                                    {
                                        while (sr.Peek() >= 0)// avoid page breaks
                                        {
                                            tmp = sr.ReadLine();
                                            if (tmp == "BT")
                                            {
                                                sr.ReadLine();
                                                tmp = sr.ReadLine();
                                                break;
                                            }
                                        }
                                    }
                                    tmp = tmp.Substring(36, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
                                    if (tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr).Contains(stateAbbr + " "))
                                    {
                                        tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                        //that means we have a string that includes suburb state & postcode
                                        //eg: TOORAK VIC 3142
                                        tmpBooking.ToPostcode = tmp.Substring(tmp.Length - 4, 4).Trim(); // Last four digits
                                        tmpBooking.StateId = stateId;
                                        tmpBooking.ToSuburb = tmp.Substring(0, tmp.Length - 7).Trim(); // First ~n digits less 8 for Postcode and state
                                    }
                                    else
                                    {
                                        tmpBooking.ToDetail3 = tmp;

                                        // work out state & postcode
                                        sr.ReadLine(); // Skip formatting line
                                        tmp = sr.ReadLine();
                                        if (tmp == "ET" || tmp.ToLower() == "endstream" || tmp == "")// avoid page breaks
                                        {
                                            while (sr.Peek() >= 0)// avoid page breaks
                                            {
                                                tmp = sr.ReadLine();
                                                if (tmp == "BT")
                                                {
                                                    sr.ReadLine();
                                                    tmp = sr.ReadLine();
                                                    break;
                                                }
                                            }
                                        }

                                        tmp = tmp.Substring(36, 28).Trim();
                                        if (int.TryParse(tmp.Trim(), out int intPars))
                                        {
                                            tmpBooking.StateId = stateId;
                                            tmpBooking.ToSuburb = tmpBooking.ToDetail3;
                                            tmpBooking.ToPostcode = intPars.ToString();
                                            tmpBooking.ToDetail3 = string.Empty;
                                        }
                                        else
                                        {
                                            tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                            var statePostcode = tmp.Replace(stateAbbr + " ", "|").Split('|');
                                            if (statePostcode != null && statePostcode.Length == 2)
                                            {
                                                if (string.IsNullOrEmpty(statePostcode[0]))
                                                {
                                                    tmpBooking.StateId = stateId;
                                                    tmpBooking.ToSuburb = tmpBooking.ToDetail3;
                                                    tmpBooking.ToPostcode = statePostcode[1];
                                                    tmpBooking.ToDetail3 = string.Empty;
                                                }
                                                else
                                                {
                                                    tmpBooking.StateId = stateId;
                                                    tmpBooking.ToSuburb = statePostcode[0];
                                                    tmpBooking.ToPostcode = statePostcode[1];
                                                }
                                            }
                                            else if (tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr) == stateAbbr)
                                            {
                                                tmpBooking.StateId = stateId;
                                                tmpBooking.ToSuburb = tmpBooking.ToDetail3;
                                                tmpBooking.ToDetail3 = string.Empty;
                                            }
                                            else
                                            {
                                                tmpBooking.StateId = stateId;
                                                tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                                var strLen = tmp.Length;
                                                tmpBooking.ToPostcode = strLen >= 4 ? tmp.Substring(strLen - 4, 4).Trim() : null; // Last four digits
                                                if (int.TryParse(tmpBooking.ToPostcode, out int discard))
                                                {
                                                    tmpBooking.ToSuburb = tmp.Substring(0, strLen - 7).Trim(); // First ~n digits less 8 for Postcode and state
                                                }
                                                else
                                                {
                                                    if (tmpBooking.ToDetail3.Contains(" " + stateAbbr))// odd case
                                                    {
                                                        tmpBooking.ToPostcode = null;
                                                        tmpBooking.ToSuburb = tmpBooking.ToDetail3.Replace(" " + stateAbbr, string.Empty);
                                                        tmpBooking.ToDetail3 = string.Empty;
                                                    }
                                                    else if (tmpBooking.ToDetail3.Trim() == ".")// odd case
                                                    {
                                                        tmpBooking.ToPostcode = null;
                                                        tmpBooking.ToSuburb = tmpBooking.ToDetail2.Trim();
                                                        tmpBooking.ToDetail2 = string.Empty;
                                                        tmpBooking.ToDetail3 = string.Empty;
                                                    }
                                                    else if (tmp == "**")// odd case
                                                    {
                                                        tmpBooking.ToPostcode = null;
                                                        tmpBooking.ToSuburb = tmpBooking.ToDetail3.Trim();
                                                        tmpBooking.ToDetail2 = string.Empty;
                                                        tmpBooking.ToDetail3 = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        tmpBooking.ToPostcode = null;
                                                        tmpBooking.ToSuburb = tmp.Trim(); // First ~n digits less 8 for Postcode and state
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    bookings.Add(tmpBooking);

                                }
                            }
                        }
                    }
                }
                routeDictionary.Add(runNumber, bookings);
            }
            catch (Exception)
            {
                bookings = null;
                throw;
            }

            return routeDictionary;
        }

        private void clearPageBreak(ref StreamReader sr)
        {
            while (sr.Peek() >= 0)
            {
                if (sr.ReadLine() == "BT")
                {
                    sr.ReadLine();
                    break;
                }
            }
        }

        public Dictionary<string, List<Booking>> ExtractForSMGAutomotiveVIC(string FileLocation, string stateName, string stateAbbr, string stateId)
        {
            var routeDictionary = new Dictionary<string, List<Booking>>();
            var bookings = new List<Booking>();
            var store = string.Empty;
            var runNumber = string.Empty;
            try
            {
                // Using plain streams in this instance is more performant as the PDF is not complex
                using (FileStream fs = new FileStream(FileLocation, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // While there is another row to read into
                        while (sr.Peek() >= 0)
                        {
                            var tmp = sr.ReadLine();

                            // 
                            if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
                            {
                                sr.ReadLine(); // Skip formatting line
                                tmp = sr.ReadLine();

                                // If this row contains the store name, save it once. It is then injected into subsequent bookings.
                                if (rgxStore.IsMatch(tmp))
                                {
                                    store = tmp.Substring(52, 60).Trim().Split('-').Length > 0 ? tmp.Substring(52, 60).Trim().Split('-')[0].ToString() : tmp.Substring(52, 60).Trim();

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    if (tmp.Contains(RunsheetMatchStr))
                                    {
                                        if (runNumber.Length > 0)
                                        {
                                            if (runNumber != tmp.Substring(76, 3).Trim())
                                            {
                                                var shallowCopyList = new List<Booking>(bookings);
                                                //this would indicate that the PDF contains multiple runsheets
                                                routeDictionary.Add(runNumber, shallowCopyList);
                                                bookings.Clear();
                                            }
                                        }
                                        runNumber = tmp.Substring(76, 3).Trim();
                                    }
                                }
                                else if (rgxJob.IsMatch(tmp) && !tmp.Contains("Invoice"))
                                {
                                    var tmpBooking = new Booking
                                    {
                                        Ref2 = store,
                                        ToDetail1 = tmp.Replace("\\(", "(").Replace("\\)", ")").Substring(37, 29).Trim(),
                                        Ref1 = tmp.Replace("\\(", "(").Replace("\\)", ")").Substring(67, 10).Trim(),
                                        //RunNumber = runNumber
                                    };

                                    //change Ref2 to indicate the customer name
                                    var customerName = tmp.Substring(7, 28).Trim();

                                    sr.ReadLine(); // Skip formatting line


                                    tmp = sr.ReadLine();

                                    if ((tmp.Trim().ToLower().Contains("capricorn") && tmp.Trim().ToLower().Contains("(") && tmp.Trim().ToLower().Contains(")")) ||
                                        (tmp.Trim().ToLower().Contains("gemini") && (tmp.Trim().ToLower().Contains("group") || tmp.Trim().ToLower().Contains("laverton"))))
                                    {
                                        sr.ReadLine();
                                        tmp = sr.ReadLine();
                                    }

                                    tmpBooking.ToDetail2 = tmp.Substring(37, 29).Trim();

                                    sr.ReadLine(); // Skip formatting line

                                    tmp = sr.ReadLine();
                                    tmp = tmp.Substring(36, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
                                    if (tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr).Contains(stateAbbr))
                                    {
                                        tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                        //that means we have a string that includes suburb state & postcode
                                        //eg: TOORAK VIC 3142
                                        var strLen = tmp.Length;
                                        tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                        tmpBooking.StateId = stateId;
                                        tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                    }
                                    else if (tmp.Split(' ').Length > 0 && int.TryParse(tmp.Split(' ')[tmp.Split(' ').Length - 1].ToString().Trim(), out int postCodeLength) && postCodeLength.ToString().Length == 4)
                                    {
                                        tmpBooking.StateId = stateId;
                                        tmpBooking.ToSuburb = tmp.Trim().Substring(0, tmp.Trim().Length - 4);
                                        tmpBooking.ToPostcode = tmp.Trim().Substring(tmp.Trim().Length - 5);
                                    }
                                    else
                                    {
                                        tmpBooking.ToDetail3 = tmp;

                                        // work out state & postcode
                                        sr.ReadLine(); // Skip formatting line
                                        tmp = sr.ReadLine();
                                        tmp = tmp.Substring(36, 28).Trim();

                                        tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                        var statePostcode = tmp.Replace(stateAbbr, "|").Split('|');
                                        if (statePostcode.Length == 1 && int.TryParse(statePostcode[0], out int tempIntPostCode))
                                        {
                                            tmpBooking.StateId = stateId;
                                            tmpBooking.ToSuburb = tmpBooking.ToDetail3;
                                            tmpBooking.ToPostcode = statePostcode[0];
                                            tmpBooking.ToDetail3 = string.Empty;
                                        }
                                        else if (statePostcode != null && statePostcode.Length == 2)
                                        {
                                            if (string.IsNullOrEmpty(statePostcode[0]))
                                            {
                                                tmpBooking.StateId = stateId;
                                                tmpBooking.ToSuburb = tmpBooking.ToDetail3;
                                                tmpBooking.ToPostcode = statePostcode[1];
                                                tmpBooking.ToDetail3 = string.Empty;
                                            }
                                            else
                                            {
                                                tmpBooking.StateId = stateId;
                                                tmpBooking.ToSuburb = statePostcode[0];
                                                tmpBooking.ToPostcode = statePostcode[1];
                                            }
                                        }
                                        else
                                        {
                                            tmp = tmp.ToUpper().Replace(stateName.ToUpper(), stateAbbr);
                                            var strLen = tmp.Length;
                                            tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                            tmpBooking.StateId = stateId;
                                            tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                        }
                                    }

                                    bookings.Add(tmpBooking);
                                }
                            }
                        }
                    }
                }
                routeDictionary.Add(runNumber, bookings);
                // routeDictionary.Add((routeDictionary.Count + 1).ToString(), bookings);
            }
            catch (Exception)
            {
                bookings = null;
                throw;
            }

            return routeDictionary;
        }
        
        //public Dictionary<string, List<Booking>> ExtractWithIText(string FileLocation)
        //{
        //    var routeDictionary = new Dictionary<string, List<Booking>>();
        //    var bookings = new List<Booking>();
        //    var store = string.Empty;
        //    var runNumber = string.Empty;
        //    try
        //    {

        //        // Using plain streams in this instance is more performant as the PDF is not complex
        //        using (PdfReader reader = new PdfReader(FileLocation))
        //        {
        //            var text = new StringBuilder();

        //            for (int i = 1; i <= reader.NumberOfPages; i++)
        //            {
        //                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
        //            }
        //            // convert string to stream
        //            var byteArray = Encoding.UTF8.GetBytes(text.ToString());
        //            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
        //            using (var stream = new MemoryStream(byteArray))
        //            {
        //                using (StreamReader sr = new StreamReader(stream))
        //                {
        //                    // While there is another row to read into
        //                    while (sr.Peek() >= 0)
        //                    {
        //                        var tmp = sr.ReadLine();

        //                        // 
        //                        // if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
        //                        //{
        //                        // sr.ReadLine(); // Skip formatting line
        //                        // tmp = sr.ReadLine();

        //                        // If this row contains the store name, save it once. It is then injected into subsequent bookings.
        //                        if (rgxStoreIText.IsMatch(tmp))
        //                        {
        //                            store = tmp.Substring(53, 60).Trim();
        //                            //sr.ReadLine(); // Skip formatting line
        //                            tmp = sr.ReadLine();
        //                            if (tmp.Contains(RunsheetMatchStr))
        //                            {
        //                                runNumber = tmp.Substring(76, 2).Trim();
        //                            }
        //                        }
        //                        else if (tmp.Contains("CHARGE") || tmp.Contains("CHEQUE"))
        //                        {
        //                            var tmpBooking = new Booking
        //                            {
        //                                Ref2 = store,
        //                                ToDetail1 = tmp.Substring(36, 28).Trim(),
        //                                Ref1 = tmp.Substring(66, 10).Trim(),
        //                                //RunNumber = runNumber
        //                            };

        //                            //sr.ReadLine(); // Skip formatting line
        //                            tmp = sr.ReadLine(); //move to the next line
        //                            tmpBooking.ToDetail2 = tmp.Substring(35, 28).Trim();

        //                            //sr.ReadLine(); // Skip formatting line

        //                            tmp = sr.ReadLine(); //move to next line
        //                            tmp = tmp.Substring(35, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
        //                            if (rgxNumber.IsMatch(tmp))
        //                            {
        //                                //that means we have a string that includes suburb state & postcode
        //                                //eg: TOORAK VIC 3142
        //                                var strLen = tmp.Length;
        //                                tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
        //                                var state = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
        //                                tmpBooking.StateId = "1";
        //                                tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
        //                            }
        //                            else
        //                            {
        //                                tmpBooking.ToSuburb = tmp;
        //                                //work out state & postcode
        //                                // sr.ReadLine(); // Skip formatting line
        //                                tmp = sr.ReadLine();
        //                                tmp = tmp.Substring(35, 28).Trim();

        //                                var statePostcode = tmp.Split(' ');
        //                                if (statePostcode != null && statePostcode.Length > 0)
        //                                {
        //                                    tmpBooking.StateId = statePostcode[0];
        //                                    tmpBooking.ToPostcode = statePostcode[1];
        //                                }
        //                            }

        //                            //int strLen = tmp.Length ;

        //                            //tmpBooking.Postcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
        //                            //tmpBooking.State = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
        //                            //tmpBooking.Suburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state

        //                            bookings.Add(tmpBooking);

        //                            //}
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        routeDictionary.Add(runNumber, bookings);
        //    }
        //    catch (Exception)
        //    {
        //        bookings = null;
        //        throw;
        //    }

        //    return routeDictionary;
        //}

        public Dictionary<string, List<Booking>> ExtractForGasMak(string FileLocation)
        {
            var routeDictionary = new Dictionary<string, List<Booking>>();
            var bookings = new List<Booking>();
            var store = string.Empty;
            var runNumber = string.Empty;
            try
            {
                // Using plain streams in this instance is more performant as the PDF is not complex
                using (FileStream fs = new FileStream(FileLocation, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // While there is another row to read into
                        while (sr.Peek() >= 0)
                        {
                            var tmp = sr.ReadLine();

                            // 
                            if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
                            {
                                sr.ReadLine(); // Skip formatting line
                                tmp = sr.ReadLine();

                                // If this row contains the store name, save it once. It is then injected into subsequent bookings.
                                if (rgxStore.IsMatch(tmp))
                                {
                                    store = tmp.Substring(54, 60).Trim();
                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    //if (tmp.Contains(RunsheetMatchStr))
                                    //{
                                    //    runNumber = tmp.Substring(76, 2).Trim();
                                    //}

                                    if (regxRunSheet.IsMatch(tmp))
                                    {
                                        var results = regxRunSheet.Matches(tmp);
                                        if (results != null && results.Count > 0)
                                        {
                                            runNumber = results[0].Value.Replace("Runsheet for Run No:", "");
                                        }
                                    }
                                }
                                else if (rgxJob.IsMatch(tmp) && !tmp.Contains("Invoice"))
                                {
                                    var tmpBooking = new Booking
                                    {
                                        Ref2 = store,
                                        ToDetail1 = tmp.Substring(37, 28).Trim(),
                                        Ref1 = tmp.Substring(67, 10).Trim(),
                                        //RunNumber = runNumber
                                    };

                                    //change Ref2 to indicate the customer name
                                    var customerName = tmp.Substring(7, 28).Trim();
                                    if (!string.IsNullOrEmpty(customerName))
                                        tmpBooking.Ref2 = customerName;

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    tmpBooking.ToDetail2 = tmp.Substring(37, 29).Trim();

                                    sr.ReadLine(); // Skip formatting line

                                    tmp = sr.ReadLine();
                                    tmp = tmp.Substring(36, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
                                    if (rgxNumber.IsMatch(tmp))
                                    {
                                        //that means we have a string that includes suburb state & postcode
                                        //eg: TOORAK VIC 3142
                                        var strLen = tmp.Length;
                                        tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                        var state = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                        tmpBooking.StateId = "1";
                                        tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                    }
                                    else
                                    {
                                        tmpBooking.ToSuburb = tmp;
                                        //work out state & postcode
                                        sr.ReadLine(); // Skip formatting line
                                        tmp = sr.ReadLine();
                                        tmp = tmp.Substring(36, 28).Trim();

                                        var statePostcode = tmp.Split(' ');
                                        if (statePostcode != null && statePostcode.Length > 0)
                                        {
                                            tmpBooking.StateId = statePostcode[0];
                                            tmpBooking.ToPostcode = statePostcode[1];
                                        }
                                    }

                                    //int strLen = tmp.Length ;

                                    //tmpBooking.Postcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                    //tmpBooking.State = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                    //tmpBooking.Suburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state

                                    bookings.Add(tmpBooking);

                                }
                            }
                        }
                    }
                }
                routeDictionary.Add(runNumber, bookings);
            }
            catch (Exception)
            {
                bookings = null;
                throw;
            }

            return routeDictionary;
        }

        public Dictionary<string, List<Booking>> ExtractForPattersonCheney(string FileLocation)
        {
            var routeDictionary = new Dictionary<string, List<Booking>>();
            var bookings = new List<Booking>();
            var store = string.Empty;
            var runNumber = string.Empty;
            try
            {
                // Using plain streams in this instance is more performant as the PDF is not complex
                using (FileStream fs = new FileStream(FileLocation, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // While there is another row to read into
                        while (sr.Peek() >= 0)
                        {
                            var tmp = sr.ReadLine();

                            // 
                            if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
                            {
                                sr.ReadLine(); // Skip formatting line
                                tmp = sr.ReadLine();

                                // If this row contains the store name, save it once. It is then injected into subsequent bookings.
                                if (rgxStore.IsMatch(tmp))
                                {
                                    store = tmp.Substring(55, 60).Trim();
                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();

                                    if (regxRunSheet.IsMatch(tmp))
                                    {
                                        var results = regxRunSheet.Matches(tmp);
                                        if (results.Count > 0)
                                        {
                                            runNumber = results[0].Value.Replace("Runsheet for Run No:", "");
                                        }
                                    }
                                }
                                else if (rgxJob.IsMatch(tmp) && !tmp.Contains("Invoice"))
                                {
                                    var tmpBooking = new Booking
                                    {
                                        Ref2 = store,
                                        ToDetail1 = tmp.Substring(37, 28).Trim(),
                                        Ref1 = tmp.Substring(67, 10).Trim(),
                                        //RunNumber = runNumber
                                    };

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    tmpBooking.ToDetail2 = tmp.Substring(37, 28).Trim();

                                    sr.ReadLine(); // Skip formatting line

                                    tmp = sr.ReadLine();
                                    tmp = tmp.Substring(36, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
                                    if (rgxPostcodeNumber.IsMatch(tmp))
                                    {
                                        //that means we have a string that includes suburb state & postcode
                                        //eg: TOORAK VIC 3142
                                        var strLen = tmp.Length;
                                        tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                        var state = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                        tmpBooking.StateId = "1";
                                        tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                    }
                                    else
                                    {
                                        //Bayswater Automotive Service 
                                        //35 Power Road
                                        //Bayswater
                                        //VIC 3153
                                        var addressLine3 = tmp.Replace("(", "").Replace(")", "").Replace("Tj", "").Trim();
                                        sr.ReadLine(); // Skip formatting line
                                        tmp = sr.ReadLine();
                                        var addressLine4 = tmp;
                                        addressLine4 = addressLine4.Replace("(", "").Replace(")", "").Replace("Tj", "").Trim();
                                        var suburbPostcodeRegex = new Regex(@"[A-Za-z]{1,}.[A-Za-z]{2,3}.[\d]{4}", RegexOptions.IgnoreCase);

                                        // Bayswater Automotive Service 
                                        // 35 Power Road
                                        //Factory 4
                                        //Bayswater VIC 3153
                                        if (suburbPostcodeRegex.IsMatch(addressLine4))
                                        {
                                            // Uncomment this if street address is on line 2.
                                            //var toDetails2 = tmpBooking.ToDetail2;
                                            //tmpBooking.ToDetail3 = toDetails2;
                                            //tmpBooking.ToDetail2 = addressLine3;
                                            tmpBooking.ToDetail3 = addressLine3;

                                            var statePostcode = addressLine4.Split(' ');
                                            if (statePostcode != null && statePostcode.Length >= 3)
                                            {
                                                tmpBooking.ToSuburb = statePostcode[0];
                                                tmpBooking.StateId = "1";
                                                tmpBooking.ToPostcode = statePostcode[2];
                                            }
                                        }
                                        //Bayswater Automotive Service 
                                        //35 Power Road
                                        //Bayswater
                                        //VIC 3153
                                        else
                                        {
                                            tmpBooking.ToSuburb = addressLine3;
                                            var statePostcode = addressLine4.Split(' ');
                                            if (statePostcode != null && statePostcode.Length >= 2)
                                            {
                                                tmpBooking.StateId = "1";
                                                tmpBooking.ToPostcode = statePostcode[1];
                                            }
                                        }
                                    }

                                    bookings.Add(tmpBooking);

                                }
                            }
                        }
                    }
                }
                routeDictionary.Add(runNumber, bookings);
            }
            catch (Exception)
            {
                bookings = null;
                throw;
            }

            return routeDictionary;
        }

        public Dictionary<string, List<Booking>> ExtractForJeffersonAutomotive(string FileLocation)
        {
            var routeDictionary = new Dictionary<string, List<Booking>>();
            var bookings = new List<Booking>();
            var store = string.Empty;
            var runNumber = string.Empty;
            try
            {
                // Using plain streams in this instance is more performant as the PDF is not complex
                using (FileStream fs = new FileStream(FileLocation, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        // While there is another row to read into
                        while (sr.Peek() >= 0)
                        {
                            var tmp = sr.ReadLine();

                            // 
                            if (string.Compare(tmp, "BT", StringComparison.Ordinal) == 0)
                            {
                                sr.ReadLine(); // Skip formatting line
                                tmp = sr.ReadLine();

                                // If this row contains the store name, save it once. It is then injected into subsequent bookings.
                                if (rgxStore.IsMatch(tmp))
                                {
                                    store = tmp.Substring(55, 60).Trim();
                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();
                                    //if (tmp.Contains(RunsheetMatchStr))
                                    //{
                                    //    runNumber = tmp.Substring(77, 2).Trim();
                                    //}

                                    if (regxRunSheet.IsMatch(tmp))
                                    {
                                        var results = regxRunSheet.Matches(tmp);
                                        if (results.Count > 0)
                                        {
                                            runNumber = results[0].Value.Replace("Runsheet for Run No:", "");
                                        }
                                    }
                                }
                                else if (rgxJob.IsMatch(tmp) && !tmp.Contains("Invoice"))
                                {
                                    var tmpBooking = new Booking
                                    {
                                        Ref2 = store,
                                        ToDetail1 = tmp.Substring(37, 28).Trim(),
                                        Ref1 = tmp.Substring(67, 10).Trim(),
                                        //RunNumber = runNumber
                                    };

                                    sr.ReadLine(); // Skip formatting line
                                    tmp = sr.ReadLine();

                                    if ((tmp.Trim().ToLower().Contains("paint") && tmp.Trim().ToLower().Contains("(") && tmp.Trim().ToLower().Contains(")"))
                                        || (tmp.Trim().ToLower().Contains("greensborough") && tmp.Trim().ToLower().Contains("(") && tmp.Trim().ToLower().Contains(")")))
                                    {
                                        sr.ReadLine();
                                        tmp = sr.ReadLine();
                                    }
                                    tmpBooking.ToDetail2 = tmp.Substring(37, 28).Trim();

                                    sr.ReadLine(); // Skip formatting line

                                    tmp = sr.ReadLine();
                                    tmp = tmp.Substring(36, 28).Trim(); // Getting a tightly formatted string so that we can substr() backwards for postcode/state
                                    if (rgxNumber.IsMatch(tmp))
                                    {
                                        //that means we have a string that includes suburb state & postcode
                                        //eg: TOORAK VIC 3142
                                        var strLen = tmp.Length;
                                        tmpBooking.ToPostcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                        var state = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                        tmpBooking.StateId = "1";
                                        tmpBooking.ToSuburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state
                                    }
                                    else
                                    {
                                        tmpBooking.ToSuburb = tmp;
                                        //work out state & postcode
                                        sr.ReadLine(); // Skip formatting line
                                        tmp = sr.ReadLine();
                                        tmp = tmp.Substring(36, 28).Trim();

                                        var statePostcode = tmp.Split(' ');
                                        if (statePostcode != null && statePostcode.Length > 1)
                                        {
                                            tmpBooking.StateId = statePostcode[0];
                                            tmpBooking.ToPostcode = statePostcode[1];
                                        }
                                        else if (statePostcode != null && statePostcode.Length > 0)
                                        {
                                            tmpBooking.StateId = "1";
                                            tmpBooking.ToPostcode = statePostcode[0];
                                        }
                                    }

                                    //int strLen = tmp.Length ;

                                    //tmpBooking.Postcode = tmp.Substring(strLen - 4, 4).Trim(); // Last four digits
                                    //tmpBooking.State = tmp.Substring(strLen - 8, 3).Trim(); // (First three digits) of the last 8 digits
                                    //tmpBooking.Suburb = tmp.Substring(0, strLen - 8).Trim(); // First ~n digits less 8 for Postcode and state

                                    bookings.Add(tmpBooking);

                                }
                            }
                        }
                    }
                }
                routeDictionary.Add(runNumber, bookings);
            }
            catch (Exception)
            {
                bookings = null;
                throw;
            }

            return routeDictionary;
        }
    }
}
