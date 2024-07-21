using Core;
using Dapper;
using Data.Entities.EmailNotification;
using Data.Repository.EntityRepositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository
{
    public class XCabEmailClientTemplateRepository : IXCabEmailClientTemplateRepository
    {
        public async Task<XCabEmailClientTemplate> GetXCabEmailClientTemplate(int emailClientId)
        {
            XCabEmailClientTemplate xCabEmailClientTemplate = null;
            var dbArgs = new DynamicParameters();
            dbArgs.Add("EmailClientId", emailClientId);
            try
            {
                //get a list of accounts that we need to create notifications
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    const string sql = @" select Id, IncludePod,IncludePoc, IncludeStaticMap, 
                                     DisablePickupNotifications, ClientLogoFileName, IncludeRef1
                                     From xCabEmailCLientTemplate
                                     WHERE EmailClientId=@EmailClientId AND Active=1";
                    xCabEmailClientTemplate = ((List<XCabEmailClientTemplate>)await connection.QueryAsync<XCabEmailClientTemplate>(sql, dbArgs)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                     "Exception Occurred in XCabEmailClientTemplateRepository: GetXCabEmailClientTemplate, message: " +
                     ex.Message, "XCabEmailClientTemplateRepository");
            }
            return xCabEmailClientTemplate;
        }
    }
}
