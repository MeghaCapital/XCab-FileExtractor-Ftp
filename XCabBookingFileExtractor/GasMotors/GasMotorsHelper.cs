using Core;
using Data.Entities.GenericIntegration;
using Data.Repository.EntityRepositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace XCabBookingFileExtractor.GasMotors
{
    public class GasMotorsHelper : IGasMotorsHelper
    {
        public List<Booking> ExtractBooking(string accountCode, int mappedStateId, List<GasMotorsCsvRow> csvRows, ICollection<XCabClientIntegration> defaultAddressDetails)
        {
            var xcabClientIntegrationRepository = new XCabClientIntegrationRepository();
            DateTime advancedDateTime;
            var allBookings = new List<Booking>();
            var invalidBookings = new List<ValidatedBooking>();


            var ftpLoginId = defaultAddressDetails.FirstOrDefault()
                .FtpLoginId.ToString();

            // This is temporary arrangement. As gas motors are live. 
            // When we will switch fully to csv process this hardcoding can be remove. 
            if (accountCode.Equals("3GASVIC") || accountCode.Equals("3GASMAK"))
                ftpLoginId = "66";
            foreach (var csvRow in csvRows)
            {
                if (!string.IsNullOrWhiteSpace(csvRow.reference_1) &&
                    csvRow.reference_1.Trim().ToLower().Equals("reference_1"))
                {
                    continue;
                }

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
                    ServiceCode = "CPOD",
                    AccountCode = accountCode,
                    FromDetail1 = defaultAddressDetails.FirstOrDefault()
                        .DefaultPickupAddressLine1,
                    FromDetail2 = defaultAddressDetails.FirstOrDefault()
                        .DefaultPickupAddressLine2,
                    FromDetail3 = defaultAddressDetails.FirstOrDefault()
                        .DefaultPickupAddressLine3,
                    FromDetail4 = defaultAddressDetails.FirstOrDefault()
                        .DefaultPickupAddressLine4,
                    FromSuburb = defaultAddressDetails.FirstOrDefault()
                        .DefaultPickupSuburb,
                    FromPostcode = defaultAddressDetails.FirstOrDefault()
                        .DefaultPickupPostcode,
                    StateId = Convert.ToString(mappedStateId)
                };

                if (!string.IsNullOrWhiteSpace(csvRow.reference_1))
                    booking.Ref1 = csvRow.reference_1;

                if (!string.IsNullOrWhiteSpace(csvRow.reference_2))
                    booking.Ref2 = csvRow.reference_2;

                if (!string.IsNullOrWhiteSpace(csvRow.caller))
                    booking.Caller = csvRow.caller;

                if (!string.IsNullOrWhiteSpace(csvRow.receiver_name))
                    booking.ToDetail1 = csvRow.receiver_name;

                if (!string.IsNullOrWhiteSpace(csvRow.receiver_address_line_1))
                    booking.ToDetail2 = csvRow.receiver_address_line_1;

                if (!string.IsNullOrWhiteSpace(csvRow.receiver_address_line_2))
                    booking.ToDetail3 = csvRow.receiver_address_line_2;

                if (!string.IsNullOrWhiteSpace(csvRow.receiver_address_line_3))
                    booking.ToDetail4 = csvRow.receiver_address_line_3;

                if (!string.IsNullOrWhiteSpace(csvRow.receiver_suburb))
                    booking.ToSuburb = csvRow.receiver_suburb;

                if (!string.IsNullOrWhiteSpace(csvRow.receiver_post_code))
                    booking.ToPostcode = csvRow.receiver_post_code;

                if (!string.IsNullOrWhiteSpace(csvRow.number_of_items))
                    booking.TotalItems = csvRow.number_of_items;

                if (!string.IsNullOrWhiteSpace(csvRow.total_weight_kg))
                    booking.TotalWeight = csvRow.total_weight_kg;

                if (!string.IsNullOrWhiteSpace(csvRow.total_cubic_m3))
                    booking.TotalVolume = csvRow.total_cubic_m3;

                if (!string.IsNullOrWhiteSpace(csvRow.advance_date_time))
                {
                    DateTime.TryParseExact(csvRow.advance_date_time, "dd-MMM-yyyyHHmm", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out advancedDateTime);
                    if (advancedDateTime != DateTime.MinValue)
                    {
                        booking.AdvanceDateTime = advancedDateTime;
                        booking.DespatchDateTime = advancedDateTime;
                    }
                }
                else
                {
                    booking.AdvanceDateTime = DateTime.Now;
                    booking.DespatchDateTime = DateTime.Now;
                }

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

                if (!string.IsNullOrWhiteSpace(csvRow.route))
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(ftpLoginId.ToString()))
                        {
                            var xcabDriverRouteRepository =
                                new XCabDriverRouteRepository();
                            var driverRoute =
                                xcabDriverRouteRepository
                                    .GetXCabDriverRoutesForRouteName(
                                        csvRow.route.Trim(),
                                        ftpLoginId.ToString());
                            if (!string.IsNullOrEmpty(driverRoute?.DriverNumber))
                            {
                                booking.PreAllocatedDriverNumber =
                                    Convert.ToInt32(driverRoute.DriverNumber);
                            }

                            // Use Account Code configured in XCabDriverRoutes.
                            if (!string.IsNullOrWhiteSpace(driverRoute.AccountCode))
                            {
                                booking.AccountCode = driverRoute.AccountCode;
                            }
                            else if (csvRow.route.ToUpper().Equals("GM"))
                            {
                                booking.AccountCode = "3GASMAK";
                            }
                            else
                                booking.AccountCode = "3GASVIC";
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.Log(
                            $"Exception Occurred When using Driver Number for Gas Motors, Account Code : {accountCode}  Exception: {ex.Message}",
                            "GasMotorsCSVBooking");

                    }
                }
                // Avoid invalid bookings
                if (!string.IsNullOrWhiteSpace(csvRow.route) && csvRow.route.Equals("GM")
                                                             && string.IsNullOrWhiteSpace(booking.ToSuburb) &&
                                                             string.IsNullOrWhiteSpace(booking.ToPostcode))
                {
                    var validatedBooking = new ValidatedBooking
                    {
                        Booking = booking
                    };

                    invalidBookings.Add(validatedBooking);
                }
                else
                    allBookings.Add(booking);
            }

            //Send a mail with invalid bookings
            //if (invalidBookings.Count > 0)
            //{
            //    //Create a CSV fiel
            //    var attachments = new List<AttachmentFile>();

            //    string fileHeader = "StateId, Caller, DriverNumber,JobDate(or Advance Date),Service Code,Ref1,Ref2,FromDetail1,FromDetail2,FromDetail3,FromDetail4,FromSuburb,FromPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToSuburb,ToPostcode,TotalItem,TotalWeight,TotalVolume,ErrorDesc,RefinedSuburb,RefinedPostCode";
            //    var attachmentFile = Core.Utils.CreateCSVFileString(invalidBookings, fileHeader, "3GASMAK - " + DateTime.Now.ToString("yyMMddHHmmss") + ".csv");
            //    attachments.Add(attachmentFile);

            //    //Send email attaching the CSV file
            //    string[] emailTo = new string[]
            //    {
            //        !string.IsNullOrWhiteSpace(defaultAddressDetails.FirstOrDefault().EmailList) ? defaultAddressDetails.FirstOrDefault().EmailList : "Software-Development@capitaltransport.com.au"
            //    };

            //    SendMail.SendHtmlEMailWithAttachment(
            //        "Csv file contains data for route GM for 3GASMAK account. This data is ignored and not created bookings as requested by Contracts VIC team.",
            //        attachments,
            //        "XCab :  bookings with erroneous data",
            //        emailTo,
            //        null);
            //}
            return allBookings;
        }
    }
}
