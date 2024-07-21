using Dapper;
using Data.Model;
using Data.Repository.SecondaryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.SecondaryRepositories
{
    public class AppVehicleGroupRepository : IAppVehicleGroupRepository
    {
        public List<VehicleGroup> GetAllActive()
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"select id from driver.VehicleGroup where Active = 1";
                    return connection.Query<VehicleGroup>(sql).ToList();
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
