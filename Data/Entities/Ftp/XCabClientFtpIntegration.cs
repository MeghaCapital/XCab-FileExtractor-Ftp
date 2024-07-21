namespace Data.Entities.Ftp
{
    public class XCabClientFtpIntegration
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public string BookingsFolderName { get; set; }
        public string TrackingFolderName { get; set; }
        public string TrackingSchemaName { get; set; }
        public bool Active { get; set; }
        public int StateId { get; set; }
        public string ClientCode { get; set; }
        public string ErrorFolderName { get; set; }
        public string BookingSchemaName { get; set; }
    }
}
