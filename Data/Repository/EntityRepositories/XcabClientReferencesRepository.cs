using Core;
using Dapper;
using Data.Entities.ConsolidatedReferences;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabClientReferencesRepository : IXCabClientReferenecesRepository
    {
        public void Insert(ICollection<XCabClientReferences> xCabClientReferences)
        {

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql =
                        @"
                        INSERT INTO XCabClientReferences(Reference1, Reference2, JobDate, PrimaryJobId)
                        VALUES (@Reference1,@Reference2,@JobDate,@PrimaryJobId)";
                    foreach (var xCabClientReference in xCabClientReferences)
                    {
                        connection.Execute(sql, new
                        {
                            Reference1 = xCabClientReference.Reference1,
                            Reference2 = xCabClientReference.Reference2,
                            JobDate = xCabClientReference.JobDate,
                            PrimaryJobId = xCabClientReference.PrimaryJobId

                        });
                    }

                }
                catch (Exception e)
                {
                    Logger.Log(
                       "Exception Occurred in XCabClientReferences: Insert, message: " +
                       e.Message, "XCabClientReferencesRepository");
                }

            }
        }

        public ICollection<XCabClientReferences> GetXCabClientReferences(int bookingId)
        {
            ICollection<XCabClientReferences> xcabClientReferences = null;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("PrimaryJobId", bookingId);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql = @"SELECT [Id], [Reference1],[Reference2],[JobDate],[PrimaryJobId] FROM xCabClientReferences WHERE PrimaryJobId=@PrimaryJobId";
                    xcabClientReferences =
                         connection.Query<XCabClientReferences>(sql, dynamicParameters).ToList(); ;

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientReferences, exception:" + e.Message, "XCabClientReferencesRepository");
            }
            return xcabClientReferences;
        }
    }
}
