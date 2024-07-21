namespace Data.Api.Bookings.Tms
{
	public class CustomConfigResponse
    {
        public CustomConfigResponseKey CustomConfigResponseKey { get; set; }
        public string? CustomConfigResponseValue { get; set; }
        public Dictionary<string, string>? CustomFields { get; set; }
    }
    public enum CustomConfigResponseKey
    {
        DistAllStores,
        DistSouthlandStore
    }
}