using Core;
using Dapper;
using Data.Repository.EntityRepositories.BookingFileExtractor.Interface;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.BookingFileExtractor
{
    public class XCabAuthorizedAccountsRepository : IXCabAuthorizedAccountsRepository
    {
        public List<string> GetCSRAuthorizedAccounts()
        {
            var csrAuthorizedAccounts = new List<string>();
            var sql = "select AccountCode from xCabAuthorizedAccounts where LoginId = 142 and Enabled = 1";
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    csrAuthorizedAccounts = (List<string>)connection.Query<string>(sql);
                }
                catch (Exception ex)
                {
                    Logger.Log("Error while extracting account codes for CSR generic csv process.", $"{Name()}: {System.Reflection.MethodBase.GetCurrentMethod()}");
                }
            }
            return csrAuthorizedAccounts;
        }

        private string Name()
        {
            return this.GetType().ToString();
        }
    }
}
