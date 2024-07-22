using Core;

namespace XCabBookingFileExtractor.Utils.Mail
{
    public interface IEmailTemplateProvider
    {
        string GetDelievryTimeSlotEmailText(string clientName, Booking booking);
        string GetDelievryTimeSlotEmailText_V1(string clientName, Booking booking);
    }
}
