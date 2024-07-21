using Core;
using Dapper;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories
{
    public class XCabNotificationsRepository : IXCabNotificationsRepository
    {
        public void Insert(Notification xCabNotification)
        {

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql =
                        @"
                        INSERT INTO xCabNotifications(BookingId, SmsNumber, EmailAddress)
                        VALUES (@BookingId,@SmsNumber,@EmailAddress)";

                    connection.Execute(sql, new
                    {
                        BookingId = xCabNotification.BookingId,
                        SmsNumber = xCabNotification.SMSNumber,
                        EmailAddress = xCabNotification.EmailAddress

                    });
                }
                catch (Exception e)
                {
                    Logger.Log(
                       "Exception Occurred in XCabNotificationss: Insert, message: " +
                       e.Message, "XCabNotificationsRepository");
                }

            }
        }

    }
}
