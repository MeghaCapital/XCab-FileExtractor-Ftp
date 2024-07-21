namespace Data.Entities.EmailNotification
{
    public class XCabEmailClientTemplate
    {
        public int Id { get; set; }
        public bool IncludePod { get; set; }
        public bool IncludePoc { get; set; }
        public int EmailClientId { get; set; }
        public bool IncludeStaticMap { get; set; }
        public bool DisablePickupNotifications { get; set; }
        public string ClientLogoFileName { get; set; }

        public bool? IncludeRef1 { get; set; }
    }
}
