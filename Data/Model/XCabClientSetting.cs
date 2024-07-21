namespace Data.Model
{
    public class XCabClientSetting
    {
        public int Id { get; set; }
        public int FtpLoginId { get; set; }
        public string AccountCode { get; set; }
        public int StateId { get; set; }
        public bool BarocdesAllowed { get; set; }
        public bool StageBookingAPIJobs { get; set; }
        public bool StageBookingOnServiceCodes { get; set; }
        public bool Active { get; set; }
        public bool MapRef3ToRef2 { get; set; }
    }
}
