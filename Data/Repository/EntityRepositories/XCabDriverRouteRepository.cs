using Dapper;

using Data.Model.Driver;
using Data.Repository.EntityRepositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabDriverRouteRepository : IXCabDriverRouteRepository
    {
        public XCabDriverRoute GetXCabDriverRoutes(int driverNumber, string logininId)
        {
            XCabDriverRoute driverRoute = null;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("DriverNumber", driverNumber);
                dynamicParams.Add("LoginId", logininId);
                const string sql = @"SELECT Id, LoginId, StateId, RouteName, DriverNumber, IsConsolidationAllowed, IsQueueAllocateAllowed
                                    FROM XCabDriverRoutes
                            WHERE Active =1 AND DriverNumber=@DriverNumber AND LoginId = @LoginId";
                driverRoute = connection.Query<XCabDriverRoute>(sql, dynamicParams).FirstOrDefault();
            }
            return driverRoute;
        }

        public XCabDriverRoute GetXCabDriverRoutesForRouteName(string routeName, string logininId)
        {
            XCabDriverRoute driverRoute = null;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("RouteName", routeName);
                dynamicParams.Add("LoginId", logininId);
                const string sql = @"SELECT Id, LoginId, StateId, RouteName, DriverNumber, IsConsolidationAllowed, AccountCode
                                    FROM XCabDriverRoutes
                            WHERE Active =1 AND RouteName=@RouteName AND LoginId = @LoginId";
                driverRoute = connection.Query<XCabDriverRoute>(sql, dynamicParams).FirstOrDefault();
            }
            return driverRoute;
        }

        public XCabDriverRoute GetXCabDriverRoutesForRouteName(string routeName, string logininId, int state, string accountCode)
        {
            XCabDriverRoute driverRoute = null;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("RouteName", routeName);
                dynamicParams.Add("LoginId", logininId);
                dynamicParams.Add("StateId", state);
                dynamicParams.Add("AccountCode", accountCode);
                const string sql = @"SELECT Id, LoginId, StateId, RouteName, DriverNumber, IsConsolidationAllowed, IsQueueAllocateAllowed,AccountCode
                                    FROM XCabDriverRoutes
                                    WHERE Active =1 AND RouteName=@RouteName AND LoginId = @LoginId AND StateId = @StateId AND UPPER(AccountCode) = UPPER(@AccountCode)";
                driverRoute = connection.Query<XCabDriverRoute>(sql, dynamicParams).FirstOrDefault();
            }

            return driverRoute;
        }
    }
}
