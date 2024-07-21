

using SelectPdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core
{
    public class Utils
    {
        public Dictionary<String, String> StatesTable = new Dictionary<String, String>();
        public Utils()
        {
            StatesTable.Add("1", "VIC");
            StatesTable.Add("2", "NSW");
            StatesTable.Add("3", "QLD");
            StatesTable.Add("4", "SA");
            StatesTable.Add("5", "WA");
            StatesTable.Add("6", "NAT");
            //Added to support ACT deliveries: 10/12/2015
            StatesTable.Add("7", "ACT");
            //added to support NT dels
            StatesTable.Add("8", "NT");
        }
        public int GetStateId(String stateName)
        {
            int StateId = -1;
            Int32.TryParse(StatesTable.FirstOrDefault(x => x.Value == stateName).Key, out StateId);
            return StateId;
        }

        public static AttachmentFile CreateCSVFileString(ICollection<ValidatedBooking> lstBooking, string fileHeader, string fileName)
        {
            var attachmentFile = new AttachmentFile();

            var fileData = "StateId, Caller, DriverNumber,JobDate(or Advance Date),Service Code,Ref1,Ref2,FromDetail1,FromDetail2,FromDetail3,FromDetail4,FromSuburb,FromPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToSuburb,ToPostcode,TotalItem,TotalWeight,TotalVolume,Caller,ExtraPickupInfor,ExtraDeliveryInfor" + "\n";

            if (!string.IsNullOrEmpty(fileHeader))
                fileData = fileHeader + "\n";

            // var index = 1;
            var builder = new System.Text.StringBuilder();
            builder.Append(fileData);
            foreach (var orderedBooking in lstBooking.OrderBy(x => x.Booking.PreAllocatedDriverNumber))
            {
                try
                {

                    builder.Append(
                        "\"" + orderedBooking.Booking.StateId + "\","
                        + "\"" + orderedBooking.Booking.Caller + "\","
                        + "\"" + orderedBooking.Booking.PreAllocatedDriverNumber + "\","
                        + "\"" + (orderedBooking.Booking.AdvanceDateTime == DateTime.MinValue ? orderedBooking.Booking.DespatchDateTime.ToString(CultureInfo.CurrentCulture) : orderedBooking.Booking.AdvanceDateTime.ToString(CultureInfo.CurrentCulture)) + "\","
                        + "\"" + orderedBooking.Booking.ServiceCode + "\","
                        + "\"" + orderedBooking.Booking.Ref1 + "\","
                        + "\"" + orderedBooking.Booking.Ref2 + "\","
                        + "\"" + orderedBooking.Booking.FromDetail1 + "\","
                        + "\"" + orderedBooking.Booking.FromDetail2 + "\","
                        + "\"" + orderedBooking.Booking.FromDetail3 + "\","
                        + "\"" + orderedBooking.Booking.FromDetail4 + "\","
                        + "\"" + orderedBooking.Booking.FromSuburb + "\","
                        + "\"" + orderedBooking.Booking.FromPostcode + "\","
                        + "\"" + orderedBooking.Booking.ToDetail1 + "\","
                        + "\"" + orderedBooking.Booking.ToDetail2 + "\","
                        + "\"" + orderedBooking.Booking.ToDetail3 + "\","
                        + "\"" + orderedBooking.Booking.ToDetail4 + "\","
                        + "\"" + orderedBooking.Booking.ToSuburb + "\","
                        + "\"" + orderedBooking.Booking.ToPostcode + "\","
                        + "\"" + (orderedBooking.Booking.lstItems?.Count > 0 ? (orderedBooking.Booking.lstItems.ToList()[0]?.Quantity == null ? orderedBooking.Booking.lstItems?.Count : orderedBooking.Booking.lstItems.Sum(x => x.Quantity)) : 0) + "\","
                        + "\"" + orderedBooking.Booking.TotalWeight + "\","
                        + "\"" + orderedBooking.Booking.TotalVolume + "\","
                        + "\"" + orderedBooking.ErrorDescription + "\","
                        + "\"" + orderedBooking.ValidatedSuburb + "\","
                        + "\"" + orderedBooking.ValidatedPostcode + "\"\n");
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred while creating a file for internal notification." +
                        " Exception Details:" + e.Message, $"EmailNotification");
                }
            }
            try
            {
                attachmentFile.Name = fileName;
                attachmentFile.Content = builder.ToString();

                return attachmentFile;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static void SaveHtmlToPdf(string filename, string htmlText)
        {
            try
            {
                HtmlToPdf converter = new HtmlToPdf();

                // set converter options
                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                converter.Options.KeepImagesTogether = true;
                converter.Options.KeepTextsTogether = true;
                converter.Options.MarginTop = 5;
                converter.Options.MarginLeft = 5;
                converter.Options.MarginRight = 5;
                converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;

                PdfDocument doc = converter.ConvertHtmlString(htmlText, "");
                doc.Save(filename);



                doc.Close();
            }
            catch (Exception ex)
            {
                Logger.Log("Exception Occurred in SaveHtmlToPdf, message: " + ex.Message,
                        "Utils");
            }
        }

        public static bool IsContainNumbersOnly(string input)
        {
            var result = Regex.IsMatch(input, "^[0-9]*$");
            return result;
        }

        public static bool IsContainAlphanumericCharactersOnly(string input)
        {
            var result = Regex.IsMatch(input, "^[0-9a-zA-Z]*$");
            return result;
        }
    }
}
