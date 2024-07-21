using Dapper;
using Data.Model.FileStatistics;
using Microsoft.Data.SqlClient;
using Core;

namespace Data.Repository.V2
{
    public class XCabFtpFileStatisticsRepository : IXCabFtpFileStatisticsRepository
    {
        public async Task Insert(ICollection<XCabFtpFileStatistics> fileStatistics)
        {
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    var sql = @"INSERT INTO [dbo].[xCabFtpFileStatistics]
                                   (
                                    [Client]
                                   ,[FileName]
                                   ,[IsLatestStatistics]
                                   ,[StatisticsDateTime])
                             VALUES
                                   (@Client
                                   ,@FileName
                                   ,@IsLatestStatistics
                                   ,@StatisticsDateTime)";
                    await connection.OpenAsync();
                    foreach (var fileStat in fileStatistics)
                    {
                        await connection.ExecuteAsync(sql, new
                        {
                            Client = fileStat.Client,
                            FileName = fileStat.FileName,
                            IsLatestStatistics = fileStat.IsLatestStatistics,
                            StatisticsDateTime = fileStat.StatisticsDateTime
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await Logger.Log($"Exception Occurred in XCabFtpFileStatisticsRepository: Insert, message: {ex.Message}", nameof(XCabFtpFileStatisticsRepository));
            }
        }

        public async Task UpdatePreviousStatisics()
        {
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    string sqlQuery = "UPDATE xCabFtpFileStatistics SET IsLatestStatistics = 0";
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(sqlQuery);
                }
            }
            catch (Exception ex)
            {
                await Logger.Log($"Exception Occurred in XCabFtpFileStatisticsRepository: UpdatePreviousStatisics, message: {ex.Message}", nameof(XCabFtpFileStatisticsRepository));
            }
        }
    }
}
