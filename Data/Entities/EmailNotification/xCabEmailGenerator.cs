
namespace Data.Entities.EmailNotification
{
    public class XCabEmailGenerator
    {
        public int Id { get; set; }
        public string ToAddresses { get; set; }
        public string CcAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSent { get; set; }
        public bool Requeue { get; set; }
    }
}
