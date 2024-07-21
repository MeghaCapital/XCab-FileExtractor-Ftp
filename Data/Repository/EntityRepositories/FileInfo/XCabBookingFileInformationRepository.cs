using Dapper;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.FileInfo
{
    public class XCabBookingFileInformationRepository : IXCabBookingFileInformationRepository
    {
        public async Task<ICollection<XCabBookingFileInformation>> GetXCabBookingFileInformationForStore(int loginId, int fileStateId, string storeName, DateTime fileDatetime, string jobType, string previousWorkingday)
        {
            ICollection<XCabBookingFileInformation> XCabBookingFileInformation = null;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("LoginId", loginId);
            dynamicParameters.Add("StateId", fileStateId);
            dynamicParameters.Add("StoreNameFromFile", storeName);
            dynamicParameters.Add("DateInsertedFrom", previousWorkingday + " 00:00:000");
            dynamicParameters.Add("DateInsertedTo", DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 00:00:000");
            dynamicParameters.Add("JobType", jobType);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();

                    const string sql = @"SELECT * FROM [xCabBookingFileInformation] A INNER JOIN [dbo].[xCabBooking] B ON A.BookingId = B.BookingId
                                        WHERE B.Cancelled <> 1 AND B.TPLUS_JobNumber IS NULL AND B.UploadedToTplus=0 AND A.LoginId=@LoginId AND A.StateId=@StateId AND A.StoreNameFromFile=@StoreNameFromFile AND CONVERT(datetime, A.FileDateTime) > CONVERT(datetime,@DateInsertedFrom) AND CONVERT(datetime, A.FileDateTime) < CONVERT(datetime,@DateInsertedTo) AND A.JobType = @JobType";
                    XCabBookingFileInformation =
                        (List<XCabBookingFileInformation>) await(connection.QueryAsync<XCabBookingFileInformation>(sql, dynamicParameters));

                }
            }
            catch (Exception e)
            {
                await Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: GetXCabBookingFileInformation, exception:" + e.Message, "XCabBookingFileInformationRepository");
            }
            return XCabBookingFileInformation;
        }
        public ICollection<XCabBookingFileInformation> GetXCabBookingFileInformationForState(int loginId, int stateId, DateTime fileDatetime)
        {
            ICollection<XCabBookingFileInformation> XCabBookingFileInformation = null;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("LoginId", loginId);
            dynamicParameters.Add("StateId", stateId);
            dynamicParameters.Add("FileDateTime", fileDatetime);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql = @"SELECT * FROM [xCabBookingFileInformation] WHERE LoginId=@LoginId
                    AND StateId=@StateId AND convert(date,FileDateTime)=convert(date,@FileDateTime)";
                    XCabBookingFileInformation =
                         connection.Query<XCabBookingFileInformation>(sql, dynamicParameters).ToList(); ;

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: GetXCabBookingFileInformation, exception:" + e.Message, "XCabBookingFileInformationRepository");
            }
            return XCabBookingFileInformation;
        }

        public void Insert(XCabBookingFileInformation XCabBookingFileInformation)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"
                        INSERT INTO [xCabBookingFileInformation](LoginId,StateId, BookingId,FileName,RouteNameFromFile,StoreNameFromFile,JobType)
                        VALUES (@LoginId,@StateId,@BookingId,@FileName,@RouteNameFromFile,@StoreNameFromFile,@JobType)";

                    connection.Execute(sql, new
                    {
                        XCabBookingFileInformation.LoginId,
                        XCabBookingFileInformation.StateId,
                        XCabBookingFileInformation.BookingId,
                        XCabBookingFileInformation.FileName,
                        XCabBookingFileInformation.RouteNameFromFile,
                        XCabBookingFileInformation.StoreNameFromFile,
                        XCabBookingFileInformation.JobType
                    });


                }
                catch (Exception e)
                {
                    Core.Logger.Log(
                       "Exception Occurred in Insert: Insert, message: " +
                       e.Message, "XCabBookingFileInformationRepository");
                }

            }
        }
    }
}
