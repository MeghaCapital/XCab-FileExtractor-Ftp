using Data.Model.Tracking;

namespace Data.Repository.V2
{
	public interface IXCabTrackingTokensRepository
	{
		Task<IEnumerable<XCabTrackingToken>> GetTokesinBulk();

		Task<XCabTrackingToken> GetTokenForId(long id);

		Task<XCabTrackingToken> GetTokenForJobNumberAndReferences(string jobNumber, string reference1, string reference2);

		Task<int?> BulkInsert(IEnumerable<XCabTrackingToken> tokens);

		Task<bool?> Insert(XCabTrackingToken token);

		Task<int?> BulkUpdate(Dictionary<string, string> setters);

		Task<int?> BulkUpdateTokens(IEnumerable<XCabTrackingToken> tokens, Dictionary<string, string> setters);

		Task<bool?> UpdateXCabTrackingSchedule(long id, Dictionary<string, string> setters);

		Task<int?> TruncateXCabTrackAndTraceTokens();

		Task<int?> BulkDeleteXCabTrackAndTraceTokens(Dictionary<string, string> conditions);

		Task<bool?> DeleteXCabTrackAndTraceTokens(long id);
	}
}
