using Core;
using Dapper;
using Data.Entities.ExternalClientIntegration;
using Data.Repository.EntityRepositories.Interface.ExternalClientIntegration;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.ExternalClientIntegrations
{
    public class MessagePayloadRepository : IMessagePayloadRepository
    {
		public async Task<int> Insert(MessagePayload messagePayload)
		{
            var insertedId = -1;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    const string sql = @"                       
                        INSERT INTO Eint.MessagePayload(PrimaryJobId, Processed, ExternalIntegrationId, GeocodedLocationId, PayloadId, TrackingEvent)
                        VALUES (@PrimaryJobId,@Processed,@ExternalIntegrationId,@GeocodedLocationId,@PayloadId,@TrackingEvent);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                    insertedId = await connection.QueryFirstAsync<int>(sql, new
                    {
                        messagePayload.PrimaryJobId,
                        messagePayload.Processed,
                        ExternalIntegrationId = messagePayload.ExternalClientIntegrationId,
                        messagePayload.GeocodedLocationId,
                        messagePayload.PayloadId,
                        messagePayload.TrackingEvent
                    });
                }
                catch (Exception e)
                {
                    await Logger.Log(
                       "Exception Occurred while inserting rows into MessagePayload, message: " +
                       e.Message, "MessagePayloadRepository");
                }

            }
            return insertedId;
        }
    }
}
