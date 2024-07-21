using Dapper;
using Data.Entities.Ftp;
using Data.Model;
using Data.Repository.EntityRepositories.Interfaces;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabFtpLoginDetailsRepository : IXCabFtpLoginDetailsRepository
    {
        public ICollection<XCabFtpLoginDetailsRestfulWsModel> GetXCabFtpLoginDetailsRestfulWs()
        {

            ICollection<XCabFtpLoginDetailsRestfulWsModel> xCabRestfulWsClients = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                const string sql = @"SELECT Id, Username,Password FROM xCabFtpLoginDetails WHERE usesrestfulwebservice = 1";

                xCabRestfulWsClients = connection.Query<XCabFtpLoginDetailsRestfulWsModel>(sql).ToList();
            }
            return xCabRestfulWsClients;
        }

        public int IsUserAuthenticatedForRestfulWs(string username, string password)
        {
            var id = -1;
            XCabFtpLoginDetailsRestfulWsModel xCabFtpLoginDetailsRestfulWsModel = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("username", username);
                dynamicParams.Add("password", password);
                const string sql = @"SELECT Id, usesrestfulwebservice FROM xCabFtpLoginDetails
                            WHERE username=@username and sharedkey=@password and usesrestfulwebservice = 1";
                xCabFtpLoginDetailsRestfulWsModel = connection.Query<XCabFtpLoginDetailsRestfulWsModel>(sql, dynamicParams).FirstOrDefault();
            }
            if (xCabFtpLoginDetailsRestfulWsModel != null)
                id = xCabFtpLoginDetailsRestfulWsModel.Id;
            return id;
        }

        public bool IsStagedOnlyBookings(int ftpLoginId, int state, string accountCode, string serviceCode = null)
        {
            var stageBookings = false;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("FtpLoginId", ftpLoginId);
                dynamicParams.Add("State", state);
                dynamicParams.Add("AccountCode", accountCode);
                dynamicParams.Add("ServiceCode", serviceCode);
                const string sql = @"SELECT StageBookingAPIJobs  FROM [dbo].[xCabClientSetting] 
                                    WHERE FtpLoginId = @FtpLoginId AND StateId = @State  AND AccountCode = @AccountCode AND Active = 1 and StageBookingOnServiceCodes = 0
                                    Union 
                                    SELECT StageBookingAPIJobs  FROM [dbo].[xCabClientSetting] cs 
                                           INNER JOIN [dbo].[xCabClientSettingOnServices] co On co.ClientSettingId = cs.Id
                                            WHERE cs.FtpLoginId = @FtpLoginId AND cs.StateId = @State  AND cs.AccountCode = @AccountCode AND COALESCE(co.ServiceCode,'N/A') = @ServiceCode AND cs.Active = 1 AND co.Active = 1 and cs.StageBookingOnServiceCodes = 1";
                stageBookings = connection.Query<bool>(sql, dynamicParams).FirstOrDefault();
            }

            return stageBookings;
        }

        public ICollection<XCabFtpLoginDetails> GetXCabFtpLoginDetailsCsvClients()
        {
            ICollection<XCabFtpLoginDetails> xCabFtpLoginDetails = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                const string sql = @"SELECT Id, Username,Password,BookingSchemaName,BookingsFolderName, ProcessedFolderName, ErrorFolderName, TrackingSchemaName, 
                TrackingFolderName FROM xCabFtpLoginDetails WHERE Active = 1 AND LOWER(BookingSchemaName) LIKE 'csv%' AND usesrestfulwebservice = 0";
                xCabFtpLoginDetails = connection.Query<XCabFtpLoginDetails>(sql).ToList();
            }
            return xCabFtpLoginDetails;
        }

        public ICollection<XCabFtpLoginDetails> GetXCabFtpLoginDetailsForTmsTrackingClients()
        {
            ICollection<XCabFtpLoginDetails> xCabFtpLoginDetails = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                const string sql = @"SELECT Id, Username,Password,BookingSchemaName,BookingsFolderName, ProcessedFolderName, ErrorFolderName, TrackingSchemaName, 
                TrackingFolderName FROM xCabFtpLoginDetails WHERE Active = 1 AND UsingTmsTracking = 1";
                xCabFtpLoginDetails = connection.Query<XCabFtpLoginDetails>(sql).ToList();
            }
            return xCabFtpLoginDetails;
        }

        public async Task<int> IsUserAuthenticatedForRestfulWsAsync(string username, string password)
        {
            var id = -1;
            XCabFtpLoginDetailsRestfulWsModel xCabFtpLoginDetailsRestfulWsModel = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                await connection.OpenAsync();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("username", username);
                dynamicParams.Add("password", password);
                const string sql = @"SELECT Id, usesrestfulwebservice FROM xCabFtpLoginDetails
                            WHERE username=@username and sharedkey=@password and usesrestfulwebservice = 1";
                xCabFtpLoginDetailsRestfulWsModel = await connection.QueryFirstOrDefaultAsync<XCabFtpLoginDetailsRestfulWsModel>(sql, dynamicParams);
            }
            if (xCabFtpLoginDetailsRestfulWsModel != null)
                id = xCabFtpLoginDetailsRestfulWsModel.Id;
            return id;
        }

        public async Task<int> IsUserAuthenticatedForRestfulWsAsyncFromTest(string username, string password)
        {
            var id = -1;
            XCabFtpLoginDetailsRestfulWsModel xCabFtpLoginDetailsRestfulWsModel = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                await connection.OpenAsync();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("username", username);
                dynamicParams.Add("password", password);
                const string sql = @"SELECT Id, usesrestfulwebservice FROM tst.xCabFtpLoginDetails
                            WHERE username=@username and sharedkey=@password and usesrestfulwebservice = 1";
                xCabFtpLoginDetailsRestfulWsModel = await connection.QueryFirstOrDefaultAsync<XCabFtpLoginDetailsRestfulWsModel>(sql, dynamicParams);
            }
            if (xCabFtpLoginDetailsRestfulWsModel != null)
                id = xCabFtpLoginDetailsRestfulWsModel.Id;
            return id;
        }

        public async Task<int> IsUserAuthenticatedForRestfulWsAsync(string username, string password, string apiKey)
        {
            var id = -1;
            XCabFtpLoginDetailsRestfulWsModel xCabFtpLoginDetailsRestfulWsModel = null;

            const string sql = @"SELECT xb.Id 
                                 FROM xCabFtpLoginDetails xb join xCabAuthorizedAccounts xa on xb.id = xa.LoginId 
                                WHERE xb.username=@username and xb.sharedkey=@password and xa.ApiKey = @apiKey and xb.usesrestfulwebservice = 1 and active = 1 and Enabled = 1";
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                await connection.OpenAsync();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("username", username);
                dynamicParams.Add("password", password);
                dynamicParams.Add("apiKey", apiKey);
                xCabFtpLoginDetailsRestfulWsModel = await connection.QueryFirstOrDefaultAsync<XCabFtpLoginDetailsRestfulWsModel>(sql, dynamicParams);
            }
            if (xCabFtpLoginDetailsRestfulWsModel != null)
                id = xCabFtpLoginDetailsRestfulWsModel.Id;
            return id;
        }

        public async Task<int> IsUserAuthenticatedForRestfulWsAsyncFromTest(string username, string password, string apiKey)
        {
            var id = -1;
            XCabFtpLoginDetailsRestfulWsModel xCabFtpLoginDetailsRestfulWsModel = null;

            const string sql = @"SELECT xb.Id 
                                 FROM xCabFtpLoginDetails xb join xCabAuthorizedAccounts xa on xb.id = xa.LoginId 
                                WHERE xb.username=@username and xb.sharedkey=@password and xa.ApiKey = @apiKey and xb.usesrestfulwebservice = 1 and active = 1 and Enabled = 1";
            using (var connection = new SqlConnection(DbSettings.Default.XCabDevDatabase))
            {
                await connection.OpenAsync();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("username", username);
                dynamicParams.Add("password", password);
                dynamicParams.Add("apiKey", apiKey);
                xCabFtpLoginDetailsRestfulWsModel = await connection.QueryFirstOrDefaultAsync<XCabFtpLoginDetailsRestfulWsModel>(sql, dynamicParams);
            }
            if (xCabFtpLoginDetailsRestfulWsModel != null)
                id = xCabFtpLoginDetailsRestfulWsModel.Id;
            return id;
        }
    }
}
