using Core;
using Dapper;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.XCabMultipleDeliveries
{
    public class XCabMultipleDeliveriesRepository : IXCabMultipleDeliveriesRepository
    {
        public ICollection<Entities.XCabMultipleDeliveries.XCabMultipleDeliveries> GetXCabMultipleDeliveries(int BookingId)
        {
            var xCabBookings = new List<Entities.XCabMultipleDeliveries.XCabMultipleDeliveries>();

            var dbArgs = new DynamicParameters();
            dbArgs.Add("PrimaryBookingId", BookingId);
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"SELECT Id,PrimaryJobNumber, DriverNumber, JobDate ,AccountCode, DeliverySuburb, Ref1,Ref2, LegNumber, DeliveryArrive, DeliveryComplete,Eta, TotalLegs FROM 
                                        xCabMultipleDeliveries WHERE PrimaryBookingId = @PrimaryBookingId AND Completed = 0 ORDER BY LegNumber";
                    xCabBookings = connection.Query<Entities.XCabMultipleDeliveries.XCabMultipleDeliveries>(sql, dbArgs).ToList();
                }
                catch (Exception e)
                {
                    Logger.Log(
                        $"Exception Occurred in XCabBookingRepository: GetBookingsForTmsTracking for PrimaryBookingId {BookingId}, message: " +
                        e.Message, "XCabMultipleDeliveriesRepository");
                }
            }
            return xCabBookings;
        }
    }
}
