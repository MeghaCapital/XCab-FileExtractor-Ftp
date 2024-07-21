using Dapper;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories
{
    public class XCabDriverRepository : IXCabDriverRepository
    {

        public async Task<ICollection<int>> GetVehicleIdWhereCrane(int driverId)
        {
            ICollection<int> craneDrivers = null;
            using (var connection = new SqlConnection(DbSettings.Default.ReportSqlDatabaseConnectionString))
            {
                await connection.OpenAsync();
                string sql = "";
                {
                    sql = "select DriverId from [dbo].[Drivers] where driverclass like '%crane%' and driverid = @driverId";
                }
                DynamicParameters dp = new DynamicParameters();
                dp.Add("driverId", driverId);

                craneDrivers = (ICollection<int>)await connection.QueryAsync<int>(sql, dp);
            }
            return craneDrivers;
        }

        public async Task<string> GetDriverClass(int driverId)
        {
            string driverClass = null;
            string sql = "select driverclass from [dbo].[Drivers] where driverid = @driverId";

            using (var connection = new SqlConnection(DbSettings.Default.ReportSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    DynamicParameters dp = new();
                    dp.Add("driverId", driverId);

                    driverClass = await connection.QueryFirstOrDefaultAsync<string>(sql, dp);
                }
                catch (Exception ex)
                {
                    await Core.Logger.Log($"Exception Occurred while extracting driver class for {driverId}. Details:{ex.Message}", "XCabDriverRepository");
                }
            }
            return driverClass;
        }
    }
}
