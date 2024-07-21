using Core;
using Dapper;
using Data.Entities.ExternalClientIntegration;
using Data.Repository.EntityRepositories.Interface.ExternalClientIntegration;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.ExternalClientIntegrations
{
    public class ExternalClientIntegrationRepository : IExternalClientIntegrationRepository
    {       
        public async Task<ExternalClientIntegration> GetExternalClientIntegrationForId(string id)
        {
            ExternalClientIntegration externalClientIntegration = null;
            try
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("Id", id);
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
#if DEBUG
                    const string sql = @"
                        SELECT id
                              ,ClientId
                              ,ApiKey
                              ,SecurityKey
                              ,LiveApiUrl
                              ,TestApiUrl
                              ,UseLiveApi
                              ,Active
                              ,LivePodApiUrl
                              ,TestPodApiUrl
                              ,LivePocApiUrl
                              ,TestPocApiUrl
                              ,BasicAuthUsername
                              ,BasicAuthPassword
                        FROM eint.ExternalClientIntegration
                        WHERE Id=@Id";
#else
                    const string sql = @"
                        SELECT id
                              ,ClientId
                              ,ApiKey
                              ,SecurityKey
                              ,LiveApiUrl
                              ,TestApiUrl
                              ,UseLiveApi
                              ,Active
                              ,LivePodApiUrl
                              ,TestPodApiUrl
                              ,LivePocApiUrl
                              ,TestPocApiUrl
                              ,BasicAuthUsername
                              ,BasicAuthPassword
                        FROM eint.ExternalClientIntegration
                        WHERE Id=@Id AND Active = 1";
#endif
                    externalClientIntegration = await connection.QueryFirstOrDefaultAsync<ExternalClientIntegration>(sql, dbArgs);
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred while retrieving data from table: ExternalClientIntegration, exception:" +
                    e.Message, "ExternalClientIntegrationRepository");
            }
            return externalClientIntegration;
        }
    }
}
