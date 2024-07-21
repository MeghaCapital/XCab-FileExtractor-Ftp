namespace Data.Entities.Ftp
{
    public class XCabFtpLoginDetails
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BookingsFolderName { get; set; }
        public string ProcessedFolderName { get; set; }
        public string ErrorFolderName { get; set; }
        public string BookingSchemaName { get; set; }
        public string TrackingSchemaName { get; set; }
        public string TrackingFolderName { get; set; }

    }
}