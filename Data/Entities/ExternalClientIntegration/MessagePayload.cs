using System;

namespace Data.Entities.ExternalClientIntegration
{
    public class MessagePayload
    {
        public int Id { get; set; }
        public DateTime Processed { get; set; }
        public int ExternalClientIntegrationId { get; set; }
        public int GeocodedLocationId { get; set; }
        public int PrimaryJobId { get; set; }
        public string PayloadId { get; set; }
        public string TrackingEvent { get; set; }
    }
}
