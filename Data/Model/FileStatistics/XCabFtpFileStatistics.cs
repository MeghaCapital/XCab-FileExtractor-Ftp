
namespace Data.Model.FileStatistics
{
    public class XCabFtpFileStatistics
    {
        public int Id { get; set; }
        public string Client { get; set; }       
        public string FileName { get; set; }
        public bool IsLatestStatistics { get; set; }
        public DateTime StatisticsDateTime { get; set; }
    }
}
