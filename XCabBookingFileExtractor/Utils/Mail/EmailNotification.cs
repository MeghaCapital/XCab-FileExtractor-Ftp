using Core;
using Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace XCabBookingFileExtractor.Utils.Mail
{
    public static class EmailNotification
    {
        public static void SendBookingNotification(ICollection<Booking> lstBooking, string clientName, string ediFilename, string emailList)
        {
            var dump = "";
            dump =
                "StateId, Caller, DriverNumber,JobDate(or Advance Date),Service Code,Ref1,Ref2,FromDetail1,FromDetail2,FromDetail3,FromDetail4,FromSuburb,FromPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToSuburb,ToPostcode,TotalItem,TotalWeight,TotalVolume\n";
            // var index = 1;
            var builder = new System.Text.StringBuilder();
            builder.Append(dump);
            foreach (var orderedBooking in lstBooking.OrderBy(x => x.PreAllocatedDriverNumber))
            {
                try
                {

                    builder.Append(
                        orderedBooking.StateId + ","
                        + orderedBooking.Caller + ","
                        + orderedBooking.PreAllocatedDriverNumber + ","
                        + (orderedBooking.AdvanceDateTime == DateTime.MinValue ? orderedBooking.DespatchDateTime.ToString(CultureInfo.CurrentCulture) : orderedBooking.AdvanceDateTime.ToString(CultureInfo.CurrentCulture)) + ","
                        + orderedBooking.ServiceCode + ","
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
                        + (orderedBooking.lstItems?.Count > 0 ? (orderedBooking.lstItems.ToList()[0]?.Quantity == null ? orderedBooking.lstItems?.Count : orderedBooking.lstItems.Sum(x => x.Quantity)) : 0) + ","
                        + orderedBooking.TotalWeight + ","
                        + orderedBooking.TotalVolume + "\n");
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred while creating a file for internal notification:" + ediFilename +
                        " Exception Details:" + e.Message, $"EmailNotification");
                }
                /*  dump = orderedBooking.lstItems?.Aggregate(dump,
                      (current, item) => current + "'" + item.Barcode + "',");*/
                /*    dump = dump?.Substring(0, dump.Length - 1);
                    dump += "\n";*/
                //  index++;
            }
            try
            {
                dump = builder.ToString();
                var pathNameOrdered = $"OrderedRouteFile{DateTime.Now.Ticks}.csv";
                File.WriteAllText(pathNameOrdered,
                    dump, Encoding.ASCII);

                if(!string.IsNullOrEmpty(emailList))
                {
                    var htmlText =
                    clientName + " Routes are attached with this email. These Routes will appear in the TMS for the designated Advance Date"
                    + "\nEDI Filename: " + ediFilename;
                                    SendMail.SendHtmlEMailWithAttachment(htmlText, pathNameOrdered,
                                        "XCab: " + clientName + " Route Summary", emailList);
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while sending Routes as attachment:" + ediFilename +
                    " Exception Details:" + e.Message, $"EmailNotification");
            }

        }

        public static bool SendDelievryTimeSlotNotification(string email, string clientName, ICollection<Booking> lstBooking)
        {
            bool isNotificationSend = true;
            IEmailTemplateProvider emailTemplateProvider = new EmailTemplateProvider();

            try
            {
                foreach (var booking in lstBooking)
                {
                    string htmlText = emailTemplateProvider.GetDelievryTimeSlotEmailText_V1(clientName, booking);
                    string subject = $"Order Number - {booking.Ref1} updates";
                    SendMail.SendHtmlEmail(htmlText, email, subject);
                }
            }
            catch (Exception ex)
            {
                isNotificationSend = false;
                Logger.Log(
                    $"Exception Occurred while sending email to id: {email} for delivery time slot. Exception Details:" + ex.Message, "SendDelievryTimeSlotNotification");
            }
            return isNotificationSend;
        }

    }
}
