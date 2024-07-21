using Dapper;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Data.Repository.EntityRepositories.Interfaces;
using Core;

namespace Data.Repository.EntityRepositories
{
    public class XCabRemarksRepository : IXCabRemarksRepository
    {
        public List<string> GetXCabRemarks(int bookingId)
        {
            List<string> Remarks = null;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("BookingId", bookingId);
                const string sql = @"SELECT [Remarks] FROM [dbo].[xCabRemarks] WHERE BookingId = @BookingId";
                Remarks = connection.Query<string>(sql, dynamicParams).ToList();
            }
            return Remarks;
        }

        public void Insert(List<string> remarks, int bookingId)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql =
                        @"
                        INSERT INTO [xCabRemarks]([BookingId], [Remarks])
                        VALUES (@BookingId,@Remarks)";
                    foreach (var remark in remarks)
                    {
                        connection.Execute(sql, new
                        {
                            BookingId = bookingId,
                            remarks = remark,

                        });
                    }

                }
                catch (Exception e)
                {
                    Logger.Log(
                       "Exception Occurred in XCabRemarksRepository: Insert, message: " +
                       e.Message, "XCabRemarksRepository");
                }

            }
        }
    }
}
