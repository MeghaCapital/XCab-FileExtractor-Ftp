using Core;
using Dapper;
using Data.Entities.EmailNotification;
using Microsoft.Data.SqlClient;
using Data.Repository.EntityRepositories.Interfaces;

namespace Data.Repository.EntityRepositories
{
    public class XCabClientNotificationStatusRepository : IXCabClientNotificationStatusRepository
    {
        public async Task<ICollection<XCabClientNotificationStatus>> GetClientNotificationStatuses(string BookingId)
        {
            ICollection<XCabClientNotificationStatus> clientNotificationStatuses = null;
            try
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("BookingId", BookingId);
                //get a list of accounts that we need to create notifications
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    const string sql = @" select  BookingId,
                                      LoginId
                                      ,AccountCode
                                      ,StateId
                                      ,JobNumber
                                      ,SubJobNumber
                                      ,EmailList
                                      ,AttachmentFilename
                                      ,ReQueue
                                      ,Sent
                                      ,LastUpdated From xCabClientNotificationStatus
                                     WHERE BookingId=@BookingId";
                    clientNotificationStatuses = (List<XCabClientNotificationStatus>)await connection.QueryAsync<XCabClientNotificationStatus>(sql, dbArgs);
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                    "Exception Occurred in XCabCLientNotificationStatusRepository: GetClientNotificationStatuses, message: " +
                    ex.Message, "XCabClientNotificationStatusRepository");
            }
            return clientNotificationStatuses;
        }

        public async Task UpdateClientNotificationStatuses(
            ICollection<XCabClientNotificationStatus> xcabClientNotificationStatuses)
        {
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();

                    //check if this is an update to an existing Booking Id

                    const string sqlIfRowExists =
                        "SELECT Id FROM xCabClientNotificationStatus WHERE BookingId =@BookingId AND JobNumber=@JobNumber AND SubJobNumber=@SubJobNumber";
                    const string sql = @" INSERT INTO xCabClientNotificationStatus (BookingId, LoginId, AccountCode,
                                            StateId, JobNumber, SubJobNumber, EmailList, AttachmentFilename, Sent,ReQueue)
                                        VALUES (@BookingId, @LoginId, @AccountCode,
                                            @StateId, @JobNumber, @SubJobNumber, @EmailList, @AttachmentFilename, @Sent,0) ";
                    foreach (var xcabClientNotificationStatus in xcabClientNotificationStatuses)
                        try
                        {
                            var dynamicParams = new DynamicParameters();
                            dynamicParams.Add("BookingId", xcabClientNotificationStatus.BookingId);
                            dynamicParams.Add("JobNumber", xcabClientNotificationStatus.JobNumber);
                            dynamicParams.Add("SubJobNumber", xcabClientNotificationStatus.SubJobNumber);
                            //var rows = connection.Query<int>(sqlIfRowExists, dynamicParams).SingleOrDefault();
                            var rows = await connection.ExecuteScalarAsync<bool>(sqlIfRowExists, dynamicParams);
                            if (rows)
                            {
                                //this is the case when there is a already an existing row
                                await connection.ExecuteAsync(
                                    "UPDATE xCabClientNotificationStatus SET ReQueue=0, Sent = 1, LastUpdated=GETDATE() WHERE BookingId = @BookingID AND JobNumber=@JobNumber AND SubJobNumber=@SubJobNumber",
                                    new
                                    {
                                        xcabClientNotificationStatus.BookingId,
                                        xcabClientNotificationStatus.JobNumber,
                                        xcabClientNotificationStatus.SubJobNumber
                                    }
                                    );

                            }
                            else
                            {
                                await connection.ExecuteAsync(sql,
                                        new
                                        {
                                            xcabClientNotificationStatus.BookingId,
                                            xcabClientNotificationStatus.LoginId,
                                            xcabClientNotificationStatus.AccountCode,
                                            xcabClientNotificationStatus.StateId,
                                            xcabClientNotificationStatus.JobNumber,
                                            xcabClientNotificationStatus.SubJobNumber,
                                            xcabClientNotificationStatus.EmailList,
                                            xcabClientNotificationStatus.AttachmentFileName,
                                            xcabClientNotificationStatus.Sent
                                        }
                                    )
                                    ;
                            }
                        }
                        catch (Exception ex)
                        {
                            await Logger.Log(
                            "Exception Occurred in XCabCLientNotificationStatusRepository: UpdateClientNotificationStatuses, message: " +
                            ex.Message, "XCabClientNotificationStatusRepository");
                        }
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                    "Exception Occurred in XCabCLientNotificationStatusRepository: UpdateClientNotificationStatuses while opening connection, message: " +
                    ex.Message, "XCabClientNotificationStatusRepository");
            }
        }
    }
}
