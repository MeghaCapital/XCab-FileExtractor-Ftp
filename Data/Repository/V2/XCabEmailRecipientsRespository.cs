using Core;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Data.Repository.V2
{
	public class XCabEmailRecipientsRespository : IXCabEmailRecipientsRespository
	{
		public async Task<string> GetStoreEmailAddress(string storeId, int loginId, int stateId, string accountCode)
		{
			var storeEmailAddress = string.Empty;
			var dbArgs = new DynamicParameters();
			dbArgs.Add("StoreId", storeId);
			dbArgs.Add("AccountCode", accountCode);
			dbArgs.Add("LoginId", loginId);
			dbArgs.Add("StateId", stateId);
			var sql = $@"SELECT EmailAddress
								FROM dbo.XCabEmailRecipients 
							WHERE StoreId = @StoreId
								AND LoginId = @LoginId
								AND StateId = @StateId
								AND AccountCode = @AccountCode
								AND Active = 1";
			using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
			{
				try
				{
					await connection.OpenAsync();
					storeEmailAddress = await connection.QueryFirstOrDefaultAsync<string>(sql, dbArgs);
				}
				catch (Exception ex)
				{
					await Logger.Log($"Exception Occurred in XCabEmailRecipientsRespository: GetStoreEmailAddress, message: {ex.Message}", nameof(XCabEmailRecipientsRespository));
				}
			}
			return storeEmailAddress;
		}
	}
}
