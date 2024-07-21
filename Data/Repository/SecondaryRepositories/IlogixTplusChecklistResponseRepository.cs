using Dapper;
using Data.Repository.SecondaryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.SecondaryRepositories
{
    public class IlogixTplusChecklistResponseRepository : IIlogixTplusChecklistResponseRepository
    {
        public IEnumerable<int> Get()
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"select distinct ChecklistId from ilogix.TplusChecklistResponse order by checklistid desc";
                    return connection.Query<int>(sql).ToList();
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
