using Core;
using System;
using XCabBookingFileExtractor.Utils.Common;

namespace XCabBookingFileExtractor.Utils.Mail
{
    public class EmailTemplateProvider : IEmailTemplateProvider
    {
        public string GetDelievryTimeSlotEmailText(string clientName, Booking booking)
        {
            string html = "";
            try
            {
                string timeSlot =
                    CommonHelper.GetDeliveryTimeSlot(
                        "11 AM", "9 AM - 11 AM", "02 PM",
                        "12 PM - 2 PM",
                        "05 PM", "3 PM - 5 PM");


                string ref1 = booking.Ref1;
                string address = booking.ToDetail2 + "," +
                                 booking.ToSuburb + " " +
                                 booking.ToPostcode;


                html += $@"<br />";
                html += $"<p>Hi there! </p><p>Your goods ordered from <b>{clientName}</b>, order number <b>{ref1}</b> will be delivered to <b>{address}</b> between <b>{timeSlot}</b>  today. If this is not suitable, please contact the Capital Transport team on 13 14 80</p>";

                return html;
            }
            catch (Exception ex)
            {
                Logger.Log(
                    $"Exception Occurred while generating email text. Exception Details:" + ex.Message, "GetDelievryTimeSlotEmailText");
            }
            return html;
        }

        public string GetDelievryTimeSlotEmailText_V1(string clientName, Booking booking)
        {
            string html = "";
            try
            {
                string timeSlot =
                    CommonHelper.GetDeliveryTimeSlot(
                        "11 AM", "9 AM - 11 AM", "02 PM",
                        "12 PM - 2 PM",
                        "05 PM", "3 PM - 5 PM");


                string ref1 = booking.Ref1;
                string address = booking.ToDetail2 + "," +
                                 booking.ToSuburb + " " +
                                 booking.ToPostcode;


                html += $@"<br />";
                html += $"<p>Hi,</p>";
                html +=
                    $"<p>Your goods ordered from <b>{clientName}</b>, order number <b>{ref1}</b> will be delivered between <b>{timeSlot}</b> today.</p>";
                html += "Your goods are being delivered to the following address:";
                html += $@"<p><b>{booking.ToDetail1}</b></p>";
                html += $@"<p><b>{booking.ToDetail2}</b></p>";
                html += $@"<p><b>{booking.ToSuburb}, {booking.ToPostcode}</b></p>";
                html += $@"<p><i>Reference1:</i><b>{booking.Ref1}</b></p>";
                html += $@"<p><i>Reference2:</i><b>{booking.Ref2}</b></p>";

                html += "If this is not suitable, please contact the Capital Transport team on 13 14 80</p>";

                return html;
            }
            catch (Exception ex)
            {
                Logger.Log(
                    $"Exception Occurred while generating email text. Exception Details:" + ex.Message, "GetDelievryTimeSlotEmailText");
            }
            return html;
        }


    }
}
