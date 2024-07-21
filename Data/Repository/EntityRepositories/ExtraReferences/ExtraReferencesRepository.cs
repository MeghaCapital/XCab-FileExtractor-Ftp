using Core;
using Dapper;
using Data.Entities.ExtraReferences;
using Data.Repository.EntityRepositories.ExtraReferences.Interface;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.ExtraReferences
{
    public class ExtraReferencesRepository : IExtraReferencesRepository
    {
        public ICollection<XCabExtraReferences> GetXCabExtraReferenceses(int bookingId)
        {

            ICollection<XCabExtraReferences> xCabExtraReferences = null;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("PrimaryBookingId", bookingId);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql = @"SELECT * FROM eint.xCabExtraReferences WHERE PrimaryBookingId=@PrimaryBookingId";
                    xCabExtraReferences =
                         connection.Query<XCabExtraReferences>(sql, dynamicParameters).ToList(); ;

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: GetXCabExtraReferenceses, exception:" + e.Message, "ExtraReferencesRepository");
            }
            return xCabExtraReferences;
        }

        public void Insert(ICollection<XCabExtraReferences> xCabExtraReferenceses)
        {

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"
                        INSERT INTO Eint.xCabExtraReferences(PrimaryBookingId,Name,Value,UseInUns)
                        VALUES (@PrimaryBookingId,@Name,@Value,@UseInUns)";
                    foreach (var xCabExtraReferencese in xCabExtraReferenceses)
                    {
                        connection.Execute(sql, new
                        {
                            PrimaryBookingId = xCabExtraReferencese.PrimaryBookingId,
                            Name = xCabExtraReferencese.Name,
                            Value = xCabExtraReferencese.Value,
                            xCabExtraReferencese.UseInUns
                        });
                    }

                }
                catch (Exception e)
                {
                    Logger.Log(
                       "Exception Occurred in Insert: Insert, message: " +
                       e.Message, "ExtraReferencesRepository");
                }

            }
        }

        public async Task<ICollection<XCabExtraReferences>> GetXCabExtraReferencesesAsync(int bookingId)
        {
            var extraReferences = new List<XCabExtraReferences>();
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("PrimaryBookingId", bookingId);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    const string sql = @"SELECT * FROM eint.xCabExtraReferences WHERE PrimaryBookingId=@PrimaryBookingId";
                    extraReferences = (List<XCabExtraReferences>)await connection.QueryAsync<XCabExtraReferences>(sql, dynamicParameters);
                }
            }
            catch (Exception e)
            {
                await Logger.Log($"Exception Occurred while retrieving data from table: eint.xCabExtraReferences for bookingId {bookingId}, exception: {e.Message}", "ExtraReferencesRepository");
            }
            return extraReferences;
        }
    }
}
