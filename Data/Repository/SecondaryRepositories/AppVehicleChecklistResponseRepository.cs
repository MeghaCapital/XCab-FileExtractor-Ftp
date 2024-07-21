using Dapper;
using Data.Model;
using Data.Repository.SecondaryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.SecondaryRepositories
{
    public class AppVehicleChecklistResponseRepository : IAppVehicleChecklistResponseRepository
    {
        public List<ChecklistImage> Get(string jobNumber, string subJobNumber, int stateId)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("StateId", stateId);
                    dbArgs.Add("JobNumber", jobNumber);
                    dbArgs.Add("SubJobNumber", subJobNumber);
                    const string sql = @"SELECT 
	                                        [c].[id] AS ChecklistId,
	                                        [i].[Image] AS Image
                                        FROM 
	                                        [driver].[VehcileChecklistResponse] [r]
	                                        LEFT OUTER JOIN [Images].[driver].[VehicleChecklistImage] [i] on [i].[ChecklistResponseId] = [r].[id]
	                                        LEFT OUTER JOIN [driver].[VehicleChecklist] [c] on [r].[ChecklistId] = [c].[id]
	                                        LEFT OUTER JOIN [driver].[VehicleGroup] [g] on [c].[VehicleGroupId] = [g].[id]	
                                        WHERE 
	                                        [r].[JobNumber] = @JobNumber
	                                        and [r].[SubJobNumber] = @SubJobNumber
	                                        and [g].StateId = @StateId";
                    return connection.Query<ChecklistImage>(sql, dbArgs).ToList();
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
