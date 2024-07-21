namespace Data.Repository.V2
{
	public interface IXCabEmailRecipientsRespository
	{
		Task<string> GetStoreEmailAddress(string storeId, int loginId, int stateId, string accountCode);
	}
}