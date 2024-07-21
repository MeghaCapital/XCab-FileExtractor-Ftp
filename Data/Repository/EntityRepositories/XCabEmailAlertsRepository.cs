using Dapper;
using Data.Entities.EmailAlerts;
using Data.Repository.EntityRepositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabEmailAlertsRepository : IXCabEmailAlertsRepository
    {
        public XCabEmailAlerts GetXCabEmailAlert(int LoginId, int StateId)
        {
            XCabEmailAlerts xCabEmailAlerts = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("LoginId", LoginId);
                dynamicParams.Add("StateId", StateId);
                const string sql = @"SELECT Id, StateId,ClientName, EmailAddress
                                    FROM xCabEmailAlerts
                            WHERE Active =1 AND LoginId=@LoginId AND StateId = @StateId";
                xCabEmailAlerts = connection.Query<XCabEmailAlerts>(sql, dynamicParams).FirstOrDefault();
            }
            return xCabEmailAlerts;
        }
    }
}
