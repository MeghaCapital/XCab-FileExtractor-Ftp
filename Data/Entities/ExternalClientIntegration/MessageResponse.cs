using System;

namespace Data.Entities.ExternalClientIntegration
{
    public class MessageResponse
    {
        public int Id { get; set; }
        public string ResponseId { get; set; }
        public int MessagePayloadId { get; set; }
        public string StatusCode { get; set; }
        public DateTime ProcessedDateTime { get; set; }
        public bool PayloadStatusId { get; set; }
        public int TrackEventId { get; set; }
    }
}
