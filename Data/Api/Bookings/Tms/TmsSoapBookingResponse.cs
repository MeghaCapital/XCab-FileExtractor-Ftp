using Core;

namespace Data.Api.Bookings.Tms
{
	public class TmsSoapBookingResponse
	{
		public string StatusDescription { get; set; }

		public string JobNumber { get; set; }

		public int JobId { get; set; }

		public string Reference1 { get; set; }

		public string Reference2 { get; set; }

		public EStates State { get; set; }

		public string JobPriceExGst { get; set; }

		public string Gst { get; set; }

		public string JobTotalPrice { get; set; }

		public StatusCode StatusCode { get; set; }

		public ICollection<TmsBookingSundry> Sundries { get; set; }

		public string TmsResponse { get; set; }
	}
}
