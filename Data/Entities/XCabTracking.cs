using System;

namespace Data.Entities
{
    public class XCabTracking
    {
        public int Id { get; set; }

        public int LoginId { get; set; }

        public string AccountCode { get; set; }

        public int StateId { get; set; }

        public string EmailAddress { get; set; }

        public bool Enabled { get; set; }

        public DateTime LastRunTime { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }
    }
}
