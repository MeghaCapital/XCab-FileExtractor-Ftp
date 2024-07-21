using static Data.Api.Bookings.BookingModel;

namespace Data.Api.Bookings
{
	public class BookingLabelRequest
	{
		public string AccountCode { get; set; }
		public State State { get; set; }
		public string? ConsignmentNumber { get; set; }
		public DateTime? AdvanceDateTime { get; set; }
		public string? Reference1 { get; set; }
		public string? Reference2 { get; set; }
		public string ServiceCode { get; set; }
		public string? FromPostcode { get; set; }
		public string FromSuburb { get; set; }
		public string FromDetail1 { get; set; }
		public string? FromDetail2 { get; set; }
		public string? FromDetail3 { get; set; }
		public string? FromDetail4 { get; set; }
		public string? FromDetail5 { get; set; }
		public string Runcode { get; set; }
		public string ToSuburb { get; set; }
		public string? ToPostcode { get; set; }
		public string ToDetail1 { get; set; }
		public string? ToDetail2 { get; set; }
		public string? ToDetail3 { get; set; }
		public string? ToDetail4 { get; set; }
		public string? ToDetail5 { get; set; }
		public string ExtraDelInformation { get; set; }
		public List<BookingItem>? BookingItems { get; set; }
		public BookingContactInformation? BookingContactInformation { get; set; }
	}
}


