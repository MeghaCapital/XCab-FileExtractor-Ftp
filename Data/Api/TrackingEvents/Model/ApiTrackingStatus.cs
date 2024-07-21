using System.Text.Json.Serialization;

namespace Data.Api.TrackingEvents.Model
{
    public class ApiTrackingStatus
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ApiTrackingEventType EventType { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string GpsUtcDateTime { get; set; }

        public string GpsUtcDateTimeIso8601Formatted { get; set; }

        public Attachment? Attachment { get; set; }

        public ICollection<Attachment>? PocAttachments { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<string>? ItemsScanned { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<ItemsNotScanned>? ItemsNotScanned { get; set; }
    }
    public enum ApiTrackingEventType
    {
        PickupArrive = 1,
        PickupComplete = 2,
        DeliveryArrive = 3,
        DeliveryComplete = 4,
        JobCancelled = 5,
        ReferenceNotFound = 6,
        None = 7,
        JobBooked = 8,
        Futile = 9,
        ReturningToDepot = 10,
        ReturnedToDepot = 11,
        Expected = 12
    }
    public class Attachment
    {
        public string Image { get; set; }

        public string Name { get; set; }
    }
}
