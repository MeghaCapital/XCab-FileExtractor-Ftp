using Data.Entities.EmailAlerts;

namespace XCabBookingFileExtractor
{
    interface IEmailAlert
    {
        void Alert(string Message, string Title, int LoginId, int StateId, bool ccSoftwareDevteam = false, string fileName = "");
        XCabEmailAlerts GetXCabEmailAlert(int LoginId, int StateId);
        void SendEmailAlert(string MessageText, string Title, string emailAddress, bool ccSoftwareDevteam = false, string fileName = "");
    }
}
