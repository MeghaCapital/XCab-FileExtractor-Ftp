namespace Data.Entities.ExternalClientIntegration
{
    public class XCabWebhookTrackingAccounts
    {
        public int Id { get; set; }
        public int ExternalClientId { get; set; }
        public string AccountCode { get; set; }
        public int StateId { get; set; }
        public string LiveTrackingApiKey { get; set; }
        public string TestTrackingApiKey { get; set; }
        public bool Active { get; set; }
    }
}
