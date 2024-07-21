using Dapper;
using Data.Entities.EmailNotification;
using Data.Repository.EntityRepositories.Interfaces;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Core;

namespace Data.Repository.EntityRepositories
{
    public class XCabEmailClientRepository : IXCabEmailClientRepository
    {
        public async Task<ICollection<XCabEmailClient>> GetXCabEmailClients()
        {
            ICollection<XCabEmailClient> xCabEmailClients = null;
            //get a list of accounts that we need to create notifications
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = @"SELECT E.Id, E.LoginId, E.AccountCode,E.StateId, E.EmailRecipientList,E.CCList,E.InternalEMailListWithoutAttachments,
							E.EmailTitlePrefix, E.WebApiKey, E.BusinessBrand, B.BrandLogoFilename,E.ClientName, E.ClientAddress, B.BrandUrl, B.BrandPhoneNumber, B.BrandEmailSenderAddress, B.BrandName
							FROM xCabEmailClients E
							INNER JOIN xCabFtpLoginDetails F
							On E.LoginId = F.ID
							INNER JOIN xCabBusinessBrand B
							ON E.BusinessBrand=B.ID
							WHERE F.active =1 and E.Active = 1";
#if DEBUG
                    sql += " AND E.AccountCode in( 'ZZTPTEST')";
#endif
                    xCabEmailClients = (List<XCabEmailClient>)await connection.QueryAsync<XCabEmailClient>(sql);
                }
                catch (Exception ex)
                {
                    await Logger.Log(
                   "Exception Occurred in XCabEmailClientRepository: GetXCabEmailClients, message: " +
                   ex.Message, "XCabEmailClientRepository");
                }
            }
            return xCabEmailClients;
        }

        public XCabEmailClient GetClientForId(string id)
        {
            XCabEmailClient xCabEmailClient = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("Id", id);
                const string sql = @"SELECT E.Id, E.LoginId, E.AccountCode,E.StateId, E.EmailRecipientList,E.CCList,E.InternalEMailListWithoutAttachments,
							E.EmailTitlePrefix, E.WebApiKey
							FROM xCabEmailClients E
							INNER JOIN xCabFtpLoginDetails F
							On E.LoginId = F.ID
							WHERE F.active =1 AND E.Id=@Id";
                xCabEmailClient = connection.Query<XCabEmailClient>(sql, dynamicParams).FirstOrDefault();
            }
            return xCabEmailClient;
        }
    }
}
