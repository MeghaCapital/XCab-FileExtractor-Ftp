using Core;
using Data.Repository.V2;

namespace XCabService.EmailRecipientsService
{
	public class XCabEmailRecipientsServiceProvider : IXCabEmailRecipientsServiceProvider
	{
		private readonly IXCabEmailRecipientsRespository _xCabEmailRecipientsRespository;
		public XCabEmailRecipientsServiceProvider()
		{
			_xCabEmailRecipientsRespository = new XCabEmailRecipientsRespository();
		}
		public async Task<string> GetStoreEmailAddress(string storeId, int loginId, string stateId, string accountCode)
		{
			var storeEmailAddress = string.Empty;

			try
			{
				storeEmailAddress = await _xCabEmailRecipientsRespository.GetStoreEmailAddress(storeId, loginId, Convert.ToInt32(stateId), accountCode);
			}
			catch (Exception ex)
			{
				await Logger.Log($"Exception Occurred in GetStoreEmailAddress, message: {ex.Message}", nameof(XCabEmailRecipientsServiceProvider));
			}

			return storeEmailAddress;
		}
	}
}
