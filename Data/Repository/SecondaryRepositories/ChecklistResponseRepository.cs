using Dapper;
using Data.Model;
using Data.Repository.SecondaryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Data.Repository.SecondaryRepositories
{
    public class ChecklistResponseRepository<T> : IChecklistResponseRepository where T : struct, IConvertible
    {
        private T schema;

        public ChecklistResponseRepository()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Please provide Enum");
            }
        }

        public void UpdateSchema(object schema)
        {
            if (schema is T)
            {
                this.schema = (T)schema;
            }
            else
            {
                try
                {
                    this.schema = (T)Convert.ChangeType(schema, typeof(T));
                }
                catch (InvalidCastException)
                {
                    //Log
                }
            }
        }

        public IEnumerable<TplusWebApi> Get(string token)
        {
            if (Convert.ToInt32(this.schema) == (int)ESchema.App)
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    try
                    {
                        connection.Open();
                        byte[] data = Convert.FromBase64String(token);
                        List<string> whereConditions = Encoding.UTF8.GetString(data).Split('_').ToList();
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("LatestChecklist", whereConditions[0]);
                        dbArgs.Add("JobDate", whereConditions[4]);
                        const string sql = @"SELECT TOP 50
	                                            [g].[StateId] AS State,
	                                            [r].[JobNumber] AS JobNumber,
	                                            [r].[SubJobNumber] AS SubJobNumber,
	                                            [r].[DateTimeSelected] AS JobDate,
	                                            [c].[id] AS ChecklistId,
	                                            [i].[Image] AS Image
                                            FROM 
	                                            [driver].[VehcileChecklistResponse] [r]
	                                            LEFT OUTER JOIN [Images].[driver].[VehicleChecklistImage] [i] on [i].[ChecklistResponseId] = [r].[id]
	                                            LEFT OUTER JOIN [driver].[VehicleChecklist] [c] on [r].[ChecklistId] = [c].[id]
	                                            LEFT OUTER JOIN [driver].[VehicleGroup] [g] on [c].[VehicleGroupId] = [g].[id]	
                                            WHERE 
	                                            [r].[ChecklistId] = @LatestChecklist
	                                            AND [r].[DateTimeSelected] > @JobDate
                                            ORDER BY
	                                            [r].[DateTimeSelected] asc";
                        return connection.Query<TplusWebApi>(sql, dbArgs);
                    }
                    catch (Exception)
                    {
                        // LoggingManager.Log(
                        //  "Exception Occurred while retrieving data from table: xCabClientSetting, method: GetXCabClientSetting, exception:" + e.Message,LogLevel.Error);
                    }
                }
            }
            return null;
        }

        public IEnumerable<int> Get()
        {
            if (Convert.ToInt32(this.schema) == (int)ESchema.Ilogix)
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    try
                    {
                        connection.Open();
                        const string sql = @"select distinct id from [driver].[VehicleChecklist] order by id asc";
                        return connection.Query<int>(sql);
                    }
                    catch (Exception)
                    {
                        // LoggingManager.Log(
                        //  "Exception Occurred while retrieving data from table: xCabClientSetting, method: GetXCabClientSetting, exception:" + e.Message,LogLevel.Error);
                    }
                }
            }
            return null;
        }
    }
}
