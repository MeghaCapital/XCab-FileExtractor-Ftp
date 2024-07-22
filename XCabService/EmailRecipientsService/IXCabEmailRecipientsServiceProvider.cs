namespace XCabService.EmailRecipientsService
{
	public interface IXCabEmailRecipientsServiceProvider
	{
		Task<string> GetStoreEmailAddress(string storeId, int loginId, string stateId, string accountCode);
	}
}