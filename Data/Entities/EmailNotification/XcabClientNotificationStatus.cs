using System;

namespace Data.Entities.EmailNotification
{
    public class XCabClientNotificationStatus
    {
        public int BookingId { get; set; }
        public int LoginId { get; set; }
        public string AccountCode { get; set; }
        public int StateId { get; set; }
        public string JobNumber { get; set; }
        public string SubJobNumber { get; set; }
        public string EmailList { get; set; }
        public string AttachmentFileName { get; set; }
        public bool ReQueue { get; set; }
        public bool Sent { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
