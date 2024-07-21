using Data.Entities.ExternalClientIntegration;
using Data.Repository.EntityRepositories.ExternalClientIntegrations.Interface;
using Microsoft.Data.SqlClient;
using Dapper;
using Core;

namespace Data.Repository.EntityRepositories.ExternalClientIntegrations
{
    public class XCabWebhookTrackingAccountsRepository : IXCabWebhookTrackingAccountsRepository
    {
        public async Task<XCabWebhookTrackingAccounts> GetXCabWebhookTrackingAccounts(int externalClientId, string accountCode, int stateId)
        {
            XCabWebhookTrackingAccounts xCabWebhookTrackingAccounts = new();
            var sql = $@" SELECT [LiveTrackingApiKey]
                         ,[TestTrackingApiKey]
                         FROM [XCab].[eint].[xCabWebhookTrackingAccounts]
                        WHERE ExternalClientId = {externalClientId} and AccountCode = '{accountCode}' and StateId = {stateId} and Active = 1";
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    xCabWebhookTrackingAccounts = await connection.QueryFirstAsync<XCabWebhookTrackingAccounts>(sql);
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                    $"Exception Occurred while retrieving data from table: XCabWebhookTrackingAccounts while extracting tracking api key. Exception details:{ex.Message}", Name());
            }
            return xCabWebhookTrackingAccounts;
        }

        private string Name()
        {
            return GetType().Name;
        }
    }
}
