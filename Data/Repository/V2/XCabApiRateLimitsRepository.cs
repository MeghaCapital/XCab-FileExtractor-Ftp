using Dapper;
using Data.Entities.Security;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
    public class XCabApiRateLimitsRepository : IXCabApiRateLimitsRepository
    {
        public async Task<XcabApiRateLimits> GetXcabApiRateLimits(string apiKey, int loginId)
        {
            XcabApiRateLimits xcabApiRateLimits = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                await connection.OpenAsync();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add($"apiKey", apiKey);
                dynamicParams.Add($"LoginId", loginId);

                const string sql = @"SELECT ApiKey, PerSec, PerMin, PerHour, PerDay, PerWeek, StandardLimits FROM xCabApiRateLimits
                            WHERE apiKey=@apiKey AND loginId = @LoginId";
                xcabApiRateLimits = await connection.QueryFirstOrDefaultAsync<XcabApiRateLimits>(sql, dynamicParams);
            }
            return xcabApiRateLimits;
        }

        public async Task<XcabApiRateLimits> GetXcabApiRateLimitsFromTest(string apiKey, int loginId)
        {
            XcabApiRateLimits xcabApiRateLimits = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                await connection.OpenAsync();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add($"apiKey", apiKey);
                dynamicParams.Add($"LoginId", loginId);

                const string sql = @"SELECT ApiKey, PerSec, PerMin, PerHour, PerDay, PerWeek, StandardLimits FROM tst.xCabApiRateLimits
                            WHERE apiKey=@apiKey AND loginId = @LoginId";
                xcabApiRateLimits = await connection.QueryFirstOrDefaultAsync<XcabApiRateLimits>(sql, dynamicParams);
            }
            return xcabApiRateLimits;
        }
    }
}
