namespace Data.Model.Route
{
	public class RouteBarcodesRequest
	{
		public string Key { get; set; }
		public string RouteCode { get; set; }
		public string AccountCode { get; set; }
		public int StateId { get; set; }
		public string DespatchDate { get; set; }
	}
}
