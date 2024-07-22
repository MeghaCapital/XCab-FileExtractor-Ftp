using System.Collections.Generic;

namespace XCabBookingFileExtractor.Utils.Sms
{
    public class SMSContent
    {
        public string AppName { get; set; }
        public string AppKey { get; set; }

        public string Id { get; set; }
        public int StateId { get; set; }
        public string SenderId { get; set; }

        public string MobileNumber { get; set; }
        public string ClientName { get; set; }

        public MessageBox Message { get; set; }

        public class MessageBox
        {
            public int Template { get; set; }
            public string MessageText { get; set; }

            public List<string> TemplateText { get; set; }
        }
    }
}
