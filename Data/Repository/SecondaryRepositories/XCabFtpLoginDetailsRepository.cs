using Dapper;
using Data.Entities.Ftp;
using Data.Model;
using Data.Repository.SecondaryRepositories.Interfaces;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;


namespace Data.Repository.SecondaryRepositories
{
    public class XCabFtpLoginDetailsRepository : IXCabFtpLoginDetailsRepository
    {
        public ICollection<XCabFtpLoginDetailsRestfulWsModel> GetXCabFtpLoginDetailsRestfulWs()
        {

            ICollection<XCabFtpLoginDetailsRestfulWsModel> xCabRestfulWsClients = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                const string sql = @"SELECT Id, Username,Password FROM tst.xCabFtpLoginDetails WHERE usesrestfulwebservice = 1";

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
                const string sql = @"SELECT Id, usesrestfulwebservice FROM tst.xCabFtpLoginDetails
                            WHERE username=@username and sharedkey=@password and usesrestfulwebservice = 1";
                xCabFtpLoginDetailsRestfulWsModel = connection.Query<XCabFtpLoginDetailsRestfulWsModel>(sql, dynamicParams).FirstOrDefault();
            }
            if (xCabFtpLoginDetailsRestfulWsModel != null)
                id = xCabFtpLoginDetailsRestfulWsModel.Id;
            return id;
        }

        public ICollection<XCabFtpLoginDetails> GetXCabFtpLoginDetailsCsvClients()
        {
            ICollection<XCabFtpLoginDetails> xCabFtpLoginDetails = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                const string sql = @"SELECT Id, Username,Password,BookingSchemaName,BookingsFolderName, ProcessedFolderName, ErrorFolderName, TrackingSchemaName, 
                TrackingFolderName FROM tst.xCabFtpLoginDetails WHERE Active = 1 AND LOWER(BookingSchemaName) LIKE 'csv%' AND usesrestfulwebservice = 0";
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
                TrackingFolderName FROM tst.xCabFtpLoginDetails WHERE Active = 1 AND UsingTmsTracking = 1";
                xCabFtpLoginDetails = connection.Query<XCabFtpLoginDetails>(sql).ToList();
            }
            return xCabFtpLoginDetails;
        }

        public int AuthenticateUser(string username, string password)
        {
            var id = -1;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add($"username", username);
                dynamicParams.Add($"password", password);
                const string sql = @"SELECT Id FROM xCabFtpLoginDetails
                            WHERE username=@username and (password=@password or SharedKey = @password) and Active = 1";
                id = connection.Query<int>(sql, dynamicParams).FirstOrDefault();
            }
            return id;
        }
    }
}
