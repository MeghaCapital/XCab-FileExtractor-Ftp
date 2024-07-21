using Newtonsoft.Json;

namespace Data.Model.Route
{
	public class RouteBarcodesResponse
	{
		[JsonProperty("Barcode")]
		public string Barcode { get; set; }
		[JsonProperty("DropSequence")]
		public string DropSequence { get; set; }
	}
}
