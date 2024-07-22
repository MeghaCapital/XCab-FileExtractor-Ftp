namespace XCab.Como.Booker.Data
{
	public class ComoQuoteRequest
	{
		public string AccountCode { get; set; }
		public string ServiceCode { get; set; }
		public string State { get; set; }
		public string FromSuburb { get; set; }
		public string FromPostcode { get; set; }
		public string ToSuburb { get; set; }
		public string ToPostcode { get; set; }
		public string DespatchDateTime { get; set; }


	}
}
