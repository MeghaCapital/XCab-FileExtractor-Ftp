using Core;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Data.Repository.V2
{
    public class XCabAsnReleaseScheduleRepository : IXCabAsnReleaseScheduleRepository
    {
        public async Task<List<AsnReleaseSchedules>> GetAsnReleaseSchedules()
        {
            var asnReleaseSchedules = new List<AsnReleaseSchedules>();
            try
            {
                var sql = @"SELECT StateId,AccountCode,Convert(time,StartTime) StartTime,RunMinutes FROM xCabAsnReleaseSchedule WHERE Active =0";

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    asnReleaseSchedules = (List<AsnReleaseSchedules>)connection.QueryAsync<AsnReleaseSchedules>(sql).Result;
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred in XCabAsnReleaseScheduleRepository: GetAsnReleaseSchedules, message: " +
                    e.Message, "XCabAsnReleaseScheduleRepository");
            }
            return asnReleaseSchedules;
        }
    }
}
