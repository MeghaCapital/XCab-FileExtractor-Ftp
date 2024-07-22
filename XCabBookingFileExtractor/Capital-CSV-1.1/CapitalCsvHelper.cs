using Core;
using CsvHelper;
using CsvHelper.Configuration;
using Data.Entities.GenericIntegration;
using Data.Repository.EntityRepositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace XCabBookingFileExtractor.Capital_CSV_1_1
{
    public class CapitalCsvHelper : ICapitalCsvHelper
    {
        public List<CapitalCsvRow> GetCSVFilecontents(string filePath)
        {
            var records = new List<CapitalCsvRow>();
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    BadDataFound = null,
                    PrepareHeaderForMatch = args => Regex.Replace(args.Header.ToString().ToLower(), @"[\/ ( ) . \- \s]", string.Empty),
                    Delimiter = ","

                };
                using (var reader = new StreamReader(@filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    //csv.Configuration.HasHeaderRecord = true;
                    //csv.Configuration.HeaderValidated = null;
                    //csv.Configuration.MissingFieldFound = null;
                    //csv.Configuration.BadDataFound = null;
                    //csv.Configuration.PrepareHeaderForMatch = header => Regex.Replace(header.ToLower(), @"[\/ ( ) . \- \s]", string.Empty);
                    //csv.Configuration.Delimiter = ",";

                    records = csv.GetRecords<CapitalCsvRow>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    $"Exception Occurred while reading csv file contents for capital CSV-1.1, Exception: {e.Message}", "CapitalCsvHelper");
                return records;
            }
        }

        public List<Booking> ExtractBooking(string accountCode, int stateId, List<CapitalCsvRow> csvRows, ICollection<XCabClientIntegration> defaultAddressDetails)
        {
            var xcabClientIntegrationRepository = new XCabClientIntegrationRepository();
            DateTime advancedDateTime;
            var allBookings = new List<Booking>();
            var invalidBookings = new List<ValidatedBooking>();

            var ftpLoginId = defaultAddressDetails.FirstOrDefault()
                .FtpLoginId.ToString();

            foreach (var csvRow in csvRows)
            {
                var booking = new Booking
                {
                    LoginDetails = new LoginDetails
                    {
                        UserName = accountCode,
                        Id = ftpLoginId,
                        BookingsFolderName = "",
                        ErrorFolderName = "",
                        NotifyCancelJobs = false,
                        Password = "",
                        ProcessedFolderName = "",
                        TrackingFolderName = "",
                        TrackingSchemaName = "",
                        lstAccountCodes = null,
                        lstServiceCodes = null,
                        lstStateIds = null,
                        sendBinaryPod = false,
                        sendPodUrl = false
                    },
                    ServiceCode = csvRow.serviceCode,
                    AccountCode = accountCode,
                    StateId = Convert.ToString(stateId)
                };

                if (!string.IsNullOrWhiteSpace(csvRow.ref1))
                    booking.Ref1 = csvRow.ref1;

                if (!string.IsNullOrWhiteSpace(csvRow.ref2))
                    booking.Ref2 = csvRow.ref2;

                if (!string.IsNullOrWhiteSpace(csvRow.caller))
                    booking.Caller = csvRow.caller;

                var bookingType = "DEL";
                if (!string.IsNullOrWhiteSpace(csvRow.pupdel) && (csvRow.pupdel.Trim().ToUpper().Contains("DEL") || csvRow.pupdel.Trim().ToUpper().Contains("PUP")))
                    bookingType = csvRow.pupdel.ToUpper();
                else
                {
                    Core.Logger.Log(
                     $"Error in delivery type while extracting booking details. Booking type : {csvRow.pupdel} ", "CapitalCsvHelper");
                    continue;
                }

                booking.FromDetail1 = bookingType == "DEL" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine1 : (!string.IsNullOrWhiteSpace(csvRow.addressLine1) ? csvRow.addressLine1 : null);
                booking.FromDetail2 = bookingType == "DEL" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine2 : (!string.IsNullOrWhiteSpace(csvRow.addressLine2) ? csvRow.addressLine2 : null);
                booking.FromDetail3 = bookingType == "DEL" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine3 : (!string.IsNullOrWhiteSpace(csvRow.addressLine3) ? csvRow.addressLine3 : null);
                booking.FromDetail4 = bookingType == "DEL" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine4 : (!string.IsNullOrWhiteSpace(csvRow.addressLine4) ? csvRow.addressLine4 : null);
                booking.FromDetail5 = bookingType == "DEL" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine5 : (!string.IsNullOrWhiteSpace(csvRow.addressLine5) ? csvRow.addressLine5 : null);
                booking.FromSuburb = bookingType == "DEL" ? defaultAddressDetails.FirstOrDefault().DefaultPickupSuburb : (!string.IsNullOrWhiteSpace(csvRow.suburb) ? csvRow.suburb : null);
                booking.FromPostcode = bookingType == "DEL" ? defaultAddressDetails.FirstOrDefault().DefaultPickupPostcode : (!string.IsNullOrWhiteSpace(csvRow.postCode) ? csvRow.postCode : null);

                booking.ToDetail1 = bookingType == "PUP" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine1 : (!string.IsNullOrWhiteSpace(csvRow.addressLine1) ? csvRow.addressLine1 : null);
                booking.ToDetail2 = bookingType == "PUP" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine2 : (!string.IsNullOrWhiteSpace(csvRow.addressLine2) ? csvRow.addressLine2 : null);
                booking.ToDetail3 = bookingType == "PUP" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine3 : (!string.IsNullOrWhiteSpace(csvRow.addressLine3) ? csvRow.addressLine3 : null);
                booking.ToDetail4 = bookingType == "PUP" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine4 : (!string.IsNullOrWhiteSpace(csvRow.addressLine4) ? csvRow.addressLine4 : null);
                booking.ToDetail5 = bookingType == "PUP" ? defaultAddressDetails.FirstOrDefault().DefaultPickupAddressLine5 : (!string.IsNullOrWhiteSpace(csvRow.addressLine5) ? csvRow.addressLine5 : null);
                booking.ToSuburb = bookingType == "PUP" ? defaultAddressDetails.FirstOrDefault().DefaultPickupSuburb : (!string.IsNullOrWhiteSpace(csvRow.suburb) ? csvRow.suburb : null);
                booking.ToPostcode = bookingType == "PUP" ? defaultAddressDetails.FirstOrDefault().DefaultPickupPostcode : (!string.IsNullOrWhiteSpace(csvRow.postCode) ? csvRow.postCode : null);

                if (!string.IsNullOrWhiteSpace(csvRow.pieces))
                    booking.TotalItems = csvRow.pieces;

                if (!string.IsNullOrWhiteSpace(csvRow.weight))
                    booking.TotalWeight = csvRow.weight;

                if (!string.IsNullOrWhiteSpace(csvRow.advanceBookDateTime))
                {
                    if (DateTime.TryParseExact(csvRow.advanceBookDateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out advancedDateTime))
                    {
                        booking.DespatchDateTime = advancedDateTime;
                    }
                }
                else
                {
                    booking.DespatchDateTime = DateTime.Now;
                }

                if (!string.IsNullOrWhiteSpace(csvRow.driverNumber))
                    booking.PreAllocatedDriverNumber = Convert.ToInt32(csvRow.driverNumber);

                List<Item> itemList = new List<Item>();
                if (!string.IsNullOrWhiteSpace(csvRow.barcode))
                {
                    var barcodes = csvRow.barcode.Split(',');

                    foreach (var barcode in barcodes)
                    {
                        var item = new Item
                        {
                            Description = "",
                            Barcode = barcode,
                            Length = 0,
                            Width = 0,
                            Height = 0,
                            Cubic = 0,
                            Weight = 0,
                            Quantity = 1
                        };

                        itemList.Add(item);
                    }
                }

                booking.lstItems = itemList;

                if (!string.IsNullOrWhiteSpace(csvRow.notes))
                {
                    booking.ExtraDelInformation = csvRow.notes;
                    booking.ExtraPuInformation = csvRow.notes;
                }

                if (ftpLoginId == "105")
                {
                    booking.ExtraDelInformation = csvRow.notes + " " + csvRow.additionalDetails1 + " " + csvRow.additionalDetails2;
                }

                allBookings.Add(booking);
            }
            return allBookings;
        }
    }
}
