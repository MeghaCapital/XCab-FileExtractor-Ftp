namespace XCab.Como.Booker.Data.Variable
{
	public class RouteRequest
	{
		public string RequestId { get; set; }
		public string DespatchDateUtc { get; set; }
		public int AccountId { get; set; }
		public int ServiceId { get; set; }
		public int FromSuburbId { get; set; }
		public List<Leg> Legs { get; set; }
	}
}
