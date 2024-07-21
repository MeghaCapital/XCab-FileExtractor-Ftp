using System;

namespace Core
{
    [Serializable]
    public class Notification
    {
        public int BookingId { get; set; }
        public string SMSNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
