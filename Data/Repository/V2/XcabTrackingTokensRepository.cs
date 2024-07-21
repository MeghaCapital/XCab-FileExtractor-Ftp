using Dapper;
using Data.Model.Tracking;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public class XcabTrackingTokensRepository : IXCabTrackingTokensRepository
    {
        private readonly string _table = "[dbo].[xCabTrackingTokens]";
        private readonly string _allColumns = @"[Id]
                                                ,[GloballyUniqueId]
                                                ,[JobNumber]
                                                ,[DateFrom]
                                                ,[DateTo]
                                                ,[Reference1]
                                                ,[Reference2]
                                                ,[DateCreated]
                                                ,[DateExpiry]";
        private readonly string _setterColumns = @"[GloballyUniqueId]
                                                ,[JobNumber]
                                                ,[DateFrom]
                                                ,[DateTo]
                                                ,[Reference1]
                                                ,[Reference2]";

        public async Task <IEnumerable<XCabTrackingToken>> GetTokesinBulk()
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"SELECT 
	                                    {_allColumns}
                                    FROM 
	                                    {_table}
                                    WHERE
                                        [DateExpiry] > GETDATE()";
                    return await connection.QueryAsync<XCabTrackingToken>(sql);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: GetTokesinBulk, message: " + ex.Message , nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<XCabTrackingToken> GetTokenForId(long id)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"SELECT 
	                                    {_allColumns}
                                    FROM 
	                                    {_table}
                                    WHERE
	                                    [Id] = {Convert.ToString(id)}
                                        AND [DateExpiry] > GETDATE()";
                    return await connection.QueryFirstOrDefaultAsync<XCabTrackingToken>(sql);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: GetTokenForId, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<XCabTrackingToken> GetTokenForJobNumberAndReferences(string jobNumber, string reference1, string reference2)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"SELECT 
	                                    {_allColumns}
                                    FROM 
	                                    {_table}
                                    WHERE
	                                    [JobNumber] = '{jobNumber}'
                                        AND [Reference1] = '{reference1}'
                                        AND [Reference2] = '{reference2}'
                                        AND [DateExpiry] > GETDATE()";
                    return await connection.QueryFirstOrDefaultAsync<XCabTrackingToken>(sql);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: GetTokenForJobNumberAndReferences, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<int?> BulkInsert(IEnumerable<XCabTrackingToken> tokens)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"INSERT INTO
	                                    {_table}
	                                    (
		                                    {_setterColumns}
	                                    )
                                    {string.Join(",\r\n", tokens.Select(
                                                    token => "VALUES (" +
                                                        Guid.NewGuid().ToString("D") + "," +
                                                        token.JobNumber + "," +
                                                        token.DateFrom.ToString("yyyy-MM-dd HH:mm:ss.fff") + "," +
                                                        token.DateTo.ToString("yyyy-MM-dd HH:mm:ss.fff") + "," +
                                                        token.Reference1 + "," +
                                                        token.Reference2 +
                                                    ")"
                                                ))}";
                    return await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: BulkInsert, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<bool?> Insert(XCabTrackingToken token)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"INSERT INTO
	                                    {_table}
	                                    (
		                                    {_setterColumns}
	                                    )
                                    VALUES
	                                    (
		                                    '{token.GloballyUniqueId}', 
		                                    '{token.JobNumber}', 
		                                    '{token.DateFrom.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 
		                                    '{token.DateTo.ToString("yyyy-MM-dd HH:mm:ss.fff")}',
                                            '{token.Reference1}',
                                            '{token.Reference2}'
	                                    )";
                    return await connection.ExecuteAsync(sql) == 1 ? true : false;
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: Insert, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<int?> BulkUpdate(Dictionary<string, string> setters)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"UPDATE
	                                    {_table}
                                    SET
	                                    {string.Join(",\r\n", setters.Select(kv => "[" + kv.Key + "] = '" + kv.Value + "'"))}";
                    return await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: BulkUpdate, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<int?> BulkUpdateTokens(IEnumerable<XCabTrackingToken> tokens, Dictionary<string, string> setters)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"UPDATE
	                                    {_table}
                                    SET
	                                    {string.Join(",\r\n", setters.Select(kv => "[" + kv.Key + "] = '" + kv.Value + "'"))}
                                    WHERE
	                                    [Id] IN ({string.Join(",", tokens.Select(t => t.Id))})";
                    return await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: BulkUpdateTokens, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<bool?> UpdateXCabTrackingSchedule(long id, Dictionary<string, string> setters)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"UPDATE
	                                    [dbo].[XCabTrackingSchedule]
                                    SET
	                                    {string.Join(",\r\n", setters.Select(kv => "[" + kv.Key + "] = '" + kv.Value + "'"))}
                                    WHERE
	                                    [Id] = {Convert.ToString(id)}";
                    return await connection.ExecuteAsync(sql) == 1 ? true : false;
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: UpdateXCabTrackingSchedule, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<int?> TruncateXCabTrackAndTraceTokens()
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"TRUNCATE TABLE {_table}";
                    return await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: TruncateXCabTrackAndTraceTokens, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<int?> BulkDeleteXCabTrackAndTraceTokens(Dictionary<string, string> conditions)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"DELETE FROM
	                                    {_table}
                                    WHERE
	                                    {string.Join(" AND ", conditions.Select(kv => "[" + kv.Key + "] = '" + kv.Value + "'"))}";
                    return await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: BulkDeleteXCabTrackAndTraceTokens, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }

        public async Task<bool?> DeleteXCabTrackAndTraceTokens(long id)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = $@"DELETE FROM
	                                    {_table}
                                    WHERE
	                                    [Id] = {Convert.ToString(id)}";
                    return await connection.ExecuteAsync(sql) == 1 ? true : false;
                }
                catch (Exception ex)
                {
                    Core.Logger.Log("Exception Occurred in XCabTrackAndTraceTokensRepository: BulkDeleteXCabTrackAndTraceTokens, message: " + ex.Message, nameof(XcabTrackingTokensRepository));
                }
            }
            return null;
        }
    }
}
