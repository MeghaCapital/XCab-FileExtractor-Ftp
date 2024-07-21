using Core;
using Dapper;
using Data.Entities.Booking.TimeSlots;
using Data.Repository.EntityRepositories.Job.TimeSlots.Interface;
using System;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.Job.TimeSlots
{
    public class XCabTimeSlotsRepository : IXCabTimeSlotsRepository
    {
        public bool Insert(XCabTimeSlots xCabTimeSlots)
        {
            bool inserted = true;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"
                        INSERT INTO dbo.XCabTimeSlots(BookingId,StartDateTime,Duration)
                        VALUES (@BookingId,@StartDateTime,@Duration)";

                    connection.Execute(sql, new
                    {
                        BookingId = xCabTimeSlots.BookingId,
                        StartDateTime = xCabTimeSlots.StartDateTime,
                        Duration = xCabTimeSlots.Duration
                    });
                }
                catch (Exception e)
                {
                    Logger.Log(
                       "Exception Occurred in Insert: Insert, message: " +
                       e.Message, "XCabTimeSlotsRepository");
                    inserted = false;
                }

            }
            return inserted;
        }

        public async Task<XCabTimeSlots> GetTimeSlot(int bookingId)
        {
            XCabTimeSlots xCabTimeSlots = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    const string sql = @"
                        SELECT StartDateTime, Duration FROM dbo.XCabTimeSlots WHERE BookingId = @BookingId";

                    xCabTimeSlots = await connection.QueryFirstOrDefaultAsync<XCabTimeSlots>(sql, new
                    {
                        BookingId = bookingId
                    });
                }
                catch (Exception e)
                {
                    await Logger.Log($"Exception Occurred in extracting time slot. Details: {e.Message}", "XCabTimeSlotsRepository");
                }
            }
            return xCabTimeSlots;
        }
    }
}
