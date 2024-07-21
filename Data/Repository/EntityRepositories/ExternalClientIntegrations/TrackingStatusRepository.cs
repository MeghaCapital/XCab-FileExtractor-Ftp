using Core;
using Dapper;
using Data.Repository.EntityRepositories.ExternalClientIntegrations.Interface;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.ExternalClientIntegrations
{
    public class TrackingStatusRepository : ITrackingStatusRepository
    {
        public async Task UpdateXCabTableWithTrackingUpdate(Model.Tracking.ETrackingEvent eTrackingEvent, string location, string eventDateTime, int bookingId)
        {
            var sql = "UPDATE XCabBooking SET ";
            switch (eTrackingEvent)
            {
                case Model.Tracking.ETrackingEvent.PickupArrive:
                    sql += "PickupArrive = @eventDateTime, PickupArriveLocation = @location, LastModified = getdate() where BookingId = @bookingId";
                    break;
                case Model.Tracking.ETrackingEvent.PickupComplete:
                    sql += "PickupComplete = @eventDateTime, PickupCompleteLocation = @location, LastModified = getdate() where BookingId = @bookingId";
                    break;
                case Model.Tracking.ETrackingEvent.DeliveryArrive:
                    sql += "DeliveryArrive = @eventDateTime, DeliveryArriveLocation = @location, LastModified = getdate() where BookingId = @bookingId";
                    break;
                case Model.Tracking.ETrackingEvent.DeliveryComplete:
                    sql += "DeliveryComplete = @eventDateTime, DeliveryCompleteLocation = @location, Completed=1, LastModified = getdate() where BookingId = @bookingId";
                    break;
            }
            using var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString);
            try
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, new
                {
                    eventDateTime,
                    location,
                    bookingId
                });
            }
            catch (Exception ex)
            {
                await Logger.Log($"Exception Occurred while updating tracking events for Transvirtual Transport.Exception Message {ex.Message}", Name());
            }
        }

        private string Name()
        {
            return GetType().Name;
        }
    }
}
