using Core;
using Dapper;
using Data.Model;
using Microsoft.Data.SqlClient;

namespace Data.AccessControl
{
	public class AuthenticationProvider : IAuthenticationProvider
	{
		public async Task<XCabAccessControl> VerifyAuthenticationForBooking(string username, string password, string accountCode, int stateId, bool isTestUser)
		{
			var xCabAccessControl = new XCabAccessControl();
			try
			{
				var dynamicParameters = new DynamicParameters();
				dynamicParameters.Add("UserName", username);
				dynamicParameters.Add("SharedKey", password);
				dynamicParameters.Add("AccountCode", accountCode);
				dynamicParameters.Add("StateId", stateId);
				using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
				{
					await connection.OpenAsync();
					var sql = string.Empty;
					if (isTestUser)
					{
						sql = @"SELECT F.Id, F.username, F.sharedkey, F.usesrestfulwebservice, A.AccountCode, A.StateId, A.Enabled, A.IsPostcodeOptional
					                    FROM tst.xCabFtpLoginDetails F
					                    INNER JOIN tst.xCabAuthorizedAccounts A ON F.id = A.LoginId
					                    WHERE F.username= @UserName
					                    AND F.sharedkey= @SharedKey
					                    AND F.usesrestfulwebservice = 1
					                    AND A.StateId = @StateId
					                    AND A.Enabled = 1";
					}
					else
					{
						sql = @"SELECT F.Id, F.username, F.sharedkey, F.usesrestfulwebservice, A.AccountCode, A.StateId, A.Enabled, A.IsPostcodeOptional
					                    FROM xCabFtpLoginDetails F
					                    INNER JOIN xCabAuthorizedAccounts A ON F.id = A.LoginId
					                    WHERE F.username= @UserName
					                    AND F.sharedkey= @SharedKey
					                    AND F.usesrestfulwebservice = 1
					                    AND A.StateId = @StateId
					                    AND A.Enabled = 1";
					}

					var authorizedInfo = await connection.QueryAsync<XCabAccessControl>(sql, dynamicParameters);
					if (authorizedInfo != null && authorizedInfo.Any())
					{
						var accessInfoForAccountCode = authorizedInfo.Where(x => x.AccountCode.ToUpper() == accountCode.ToUpper());
						if (accessInfoForAccountCode.Any())
						{
							xCabAccessControl = accessInfoForAccountCode.First();
							xCabAccessControl.AccessVerification = EAccessControl.Successful;
							return xCabAccessControl;
						}
						else
						{
							xCabAccessControl.AccessVerification = EAccessControl.AuthorizationFailed;
							return xCabAccessControl;
						}
					}
					else
					{
						xCabAccessControl.AccessVerification = EAccessControl.AuthenticationFailed;
						return xCabAccessControl;
					}
				}
			}
			catch (Exception e)
			{
				await Logger.Log($"ExceptionDetails: Exception occurred in VerifyAuthenticationForBooking (IsTestUser: {isTestUser}), message: {e.Message}", nameof(AuthenticationProvider));
			}
			return xCabAccessControl;
		}

		public async Task<XCabAccessControl> VerifyAuthenticationForTracking(string username, string password, string apiKey, string accountCode, int stateId, bool isTestUser)
		{
			var xCabAccessControl = new XCabAccessControl();
			try
			{
				var dynamicParameters = new DynamicParameters();
				dynamicParameters.Add("UserName", username);
				dynamicParameters.Add("SharedKey", password);
				dynamicParameters.Add("AccountCode", accountCode);
				dynamicParameters.Add("StateId", stateId);
				using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
				{
					await connection.OpenAsync();
					var sql = string.Empty;
					if (isTestUser)
					{
						sql = @"SELECT F.Id, F.username, F.sharedkey, F.usesrestfulwebservice, A.AccountCode, A.StateId, A.ApiKey, A.Enabled
					                    FROM tst.xCabFtpLoginDetails F
					                    INNER JOIN tst.xCabAuthorizedAccounts A ON F.id = A.LoginId
					                    WHERE F.username= @UserName
					                    AND F.sharedkey= @SharedKey
					                    AND A.StateId = @StateId
					                    AND A.Enabled = 1";
					}
					else
					{
						sql = @"SELECT F.Id, F.username, F.sharedkey, F.usesrestfulwebservice, A.AccountCode, A.StateId, A.ApiKey, A.Enabled
					                    FROM xCabFtpLoginDetails F
					                    INNER JOIN xCabAuthorizedAccounts A ON F.id = A.LoginId
					                    WHERE F.username= @UserName
					                    AND F.sharedkey= @SharedKey
					                    AND A.StateId = @StateId
					                    AND A.Enabled = 1";
					}

					var authorizedInfo = await connection.QueryAsync<XCabAccessControl>(sql, dynamicParameters);
					if (authorizedInfo != null && authorizedInfo.Any())
					{
						var accessInfoForAccountCode = authorizedInfo.Where(x => x.AccountCode.ToUpper() == accountCode.ToUpper());
						var accessInfoForApiKey = authorizedInfo.Where(x => x.APIKey == apiKey);
						if (accessInfoForAccountCode.Any() && accessInfoForApiKey.Any())
						{
							xCabAccessControl = accessInfoForAccountCode.First();
							xCabAccessControl.AccessVerification = EAccessControl.Successful;
							return xCabAccessControl;
						}
						else if (accessInfoForAccountCode.Any())
						{
							xCabAccessControl.AccessVerification = EAccessControl.AuthenticationFailed;
							return xCabAccessControl;
						}
						else if (accessInfoForApiKey.Any())
						{
							xCabAccessControl.AccessVerification = EAccessControl.AuthorizationFailed;
							return xCabAccessControl;
						}
					}
					else
					{
						xCabAccessControl.AccessVerification = EAccessControl.AuthenticationFailed;
						return xCabAccessControl;
					}
				}
			}
			catch (Exception e)
			{
				await Logger.Log($"ExceptionDetails: Exception occurred in VerifyAuthenticationForTracking (IsTestUser: {isTestUser}), message: {e.Message}", nameof(AuthenticationProvider));
			}
			return xCabAccessControl;
		}
	}
}
