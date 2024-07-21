using Core;
using Dapper;
using Data.Entities.ExternalClientIntegration;
using Data.Repository.EntityRepositories.Interface.ExternalClientIntegration;
using System;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.ExternalClientIntegrations
{
    public class MessageResponseRepository : IMessageResponseRepository
    {
        public async void Insert(MessageResponse messageResponse)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    const string sql = @"
                        INSERT INTO Eint.MessageResponse(MessagePayloadId, PayloadStatusId, StatusCode, ProcessedDateTime, ResponseId)
                        VALUES (@MessagePayloadId,@PayloadStatusId,@StatusCode,@ProcessedDateTime,@ResponseId)";

                    await connection.ExecuteAsync(sql, new
                    {
                        messageResponse.MessagePayloadId,
                        messageResponse.PayloadStatusId,
                        messageResponse.StatusCode,
                        messageResponse.ProcessedDateTime,
                        messageResponse.ResponseId
                    });
                }
                catch (Exception e)
                {
                    await Logger.Log(
                       "Exception Occurred while inserting rows into MessageResponse, message: " +
                       e.Message, "MessageResponseRepository");
                }

            }
        }
    }
}
