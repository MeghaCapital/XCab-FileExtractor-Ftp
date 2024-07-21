using Core;

namespace Data.Api.TrackingEvents.Model
{
	public class ApiTrackAndTraceRequest
	{
		public string AccountCode { get; set; }
		public string References { get; set; }
		public string Username { get; set; }
		public string SharedKey { get; set; }
		public EStates State { get; set; }
	}
}
