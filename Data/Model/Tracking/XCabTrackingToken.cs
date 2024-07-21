
namespace Data.Model.Tracking
{
	public class XCabTrackingToken
	{
        public long Id { get; }

        public string GloballyUniqueId { get; set; }

        public string JobNumber { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string Reference1 { get; set; }

        public string Reference2 { get; set; }

        public DateTime DateCreated { get; }

        public DateTime DateExpiry { get; }
    }
}
