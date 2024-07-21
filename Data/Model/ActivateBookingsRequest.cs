namespace Data.Model
{
	public class ActivateBookingsRequest
	{
		public string Key { get; set; }
		public string AccountCode { get; set; }
		public int StateId { get; set; }
		public string RouteCode { get; set; }
		public string DriverNumber { get; set; }
	}
}
