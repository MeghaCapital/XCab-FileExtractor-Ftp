using Core;
using Dapper;
using Data.Entities.Sundries;
using Data.Repository.EntityRepositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories
{
    public class XCabSundryRepository : IXCabSundryRepository
    {
        public void Insert(XCabSundry xcabSundy)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql =
                        @"
                        INSERT INTO xCabSundry(BookingId, Service1, Qty1, Service2, Qty2, Service3, Qty3, Service4, Qty4)
                        VALUES (@BookingId,@Service1,@Qty1,@Service2,@Qty2,@Service3,@Qty3,@Service4,@Qty4)";

                    connection.Execute(sql, new
                    {
                        BookingId = xcabSundy.BookingId,
                        Service1 = xcabSundy.Service1,
                        Qty1 = xcabSundy.Qty1,
                        Service2 = xcabSundy.Service2,
                        Qty2 = xcabSundy.Qty2,
                        Service3 = xcabSundy.Service3,
                        Qty3 = xcabSundy.Qty3,
                        Service4 = xcabSundy.Service4,
                        Qty4 = xcabSundy.Qty4

                    });
                }
                catch (Exception e)
                {
                    Logger.Log(
                       "Exception Occurred in XCabSundryRepository: Insert, message: " +
                       e.Message, "XCabSundryRepository");
                }

            }
        }
    }
}
