using Dapper;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.CustomConfig
{
    public class XCabCustomConfigRepository : IXCabCustomConfigRepository
    {

        public async Task<ICollection<XCabCustomConfig>> GetCustomConfig(int ftpLoginId, int stateId, string accountCode)
        {

            ICollection<XCabCustomConfig> xCabCustomConfig = null;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("FtpLoginId", ftpLoginId);
            dynamicParameters.Add("StateId", stateId);
            dynamicParameters.Add("AccountCode", accountCode);

            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    const string sql = @"SELECT * FROM xCabCustomConfig WHERE FtpLoginId=@FtpLoginId
                    AND StateId=@StateId AND AccountCode=@AccountCode AND Active = 1";
                    xCabCustomConfig = (ICollection<XCabCustomConfig>)await connection.QueryAsync<XCabCustomConfig>(sql, dynamicParameters); 

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: GetCustomConfig, exception:" + e.Message, "XCabCustomConfigRepository");
            }
            return xCabCustomConfig;
        }
    }
}
