using Core;
using Dapper;
using Data.Entities.EmailNotification;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.Email
{
    public class XCabEmailGeneratorrepository
    {

        public ICollection<XCabEmailGenerator> GetEmailToSend()
        {
            ICollection<XCabEmailGenerator> emails = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = $@"SELECT [Id]
                                  ,[ToAddresses]
                                  ,[CCAddresses]
                                  ,[Subject]
                                  ,[Body]
                                  ,[IsSent]
                                  ,[Requeue]
                                  ,[DateInserted]
                              FROM [dbo].[xCabEmailGenerator]
                              WHERE IsSent = 0 OR Requeue = 1
                              ORDER BY Id";
                    emails = connection.Query<XCabEmailGenerator>(sql).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Log("Exception Occurred while selecting data from xCabEmailGenerator. Message : " + ex.Message, "XCabEmailGeneratorrepository");
                }
            }
            return emails;
        }

        public bool UpdateSendEmails(int emailId)
        {
            var emailSent = false;
            var sql = "UPDATE [dbo].[xCabEmailGenerator] SET IsSent = 1,Requeue = 0,LastUpdated = GETDATE() where id = @id";

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    connection.Execute(sql, new { id = emailId });
                }
                catch (Exception ex)
                {
                    Logger.Log("Exception Occurred while selecting data from xCabEmailGenerator. Message : " + ex.Message, "XCabEmailGeneratorrepository");
                    emailSent = false;
                }
            }
            return emailSent;
        }

    }
}
