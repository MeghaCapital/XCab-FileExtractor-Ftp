using Dapper;
using Data.Model;
using Data.Repository.SecondaryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.SecondaryRepositories
{
    public class AppVehicleChecklistRepository : IAppVehicleChecklistRepository
    {
        public List<VehicleChecklist> GetAll(IEnumerable<int> vehicleGroupIds)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("VehicleGroups", string.Join(",", vehicleGroupIds));
                    const string sql = @"select distinct id, VehicleGroupId, Question, ShortQuestion, PdaScreen from driver.VehicleChecklist where VehicleGroupId in (
                                        @VehicleGroups
                                        )";
                    return connection.Query<VehicleChecklist>(sql).ToList();
                }
                catch (Exception)
                {
                    // LoggingManager.Log(
                    //  "Exception Occurred while retrieving data from table: xCabClientSetting, method: GetXCabClientSetting, exception:" + e.Message,LogLevel.Error);
                }
            }
            return null;
        }
    }
}
