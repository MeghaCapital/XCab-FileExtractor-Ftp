using Core;
using Dapper;
using Data.Api.Asn;
using Data.Model;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.Asn
{
    public class AsnRepository : IAsnRepository
    {
        public async Task<bool> UpdateAsnBooking(AsnUpdateRequest asnBookingRequest)
        {
            bool isAsnBookingUpdated = false;
            var dynamicParams = new DynamicParameters();
            var updateSql = "Update xCabBooking SET ";
            if (asnBookingRequest.DeliveryDate != null && asnBookingRequest.DeliveryDate != DateTime.MinValue)
            {
                updateSql += " AdvanceDateTime = @AdvanceDateTime, DespatchDateTime = @DespatchDateTime, ";
                dynamicParams.Add("AdvanceDateTime", asnBookingRequest.DeliveryDate);
                dynamicParams.Add("DespatchDateTime", asnBookingRequest.DeliveryDate);
            }
            if (!string.IsNullOrWhiteSpace(asnBookingRequest.ToDetail1))
            {
                updateSql += " ToDetail1 = @ToDetail1, ";
                dynamicParams.Add("ToDetail1", asnBookingRequest.ToDetail1);
            }
            if (!string.IsNullOrWhiteSpace(asnBookingRequest.ToDetail2))
            {
                updateSql += " ToDetail2 = @ToDetail2, ";
                dynamicParams.Add("ToDetail2", asnBookingRequest.ToDetail2);
            }
            if (!string.IsNullOrWhiteSpace(asnBookingRequest.ToDetail3))
            {
                updateSql += " ToDetail3 = @ToDetail3, ";
                dynamicParams.Add("ToDetail3", asnBookingRequest.ToDetail3);
            }
            if (!string.IsNullOrWhiteSpace(asnBookingRequest.ToSuburb))
            {
                updateSql += " ToSuburb = @ToSuburb, ";
                dynamicParams.Add("ToSuburb", asnBookingRequest.ToSuburb);
            }
            updateSql += " LastModified  = GETDATE()";

            updateSql += " where ConsignmentNumber = @ConsignmentNumber and UploadedToTplus = 0 and OkToUpload = 0 and cancelled = 0 and DATEDIFF(day, DateInserted,GETDATE()) <=30 and AccountCode = @AccountCode and StateId = @StateId";
            dynamicParams.Add("ConsignmentNumber", asnBookingRequest.ConsignmentNumber);
            dynamicParams.Add("AccountCode", asnBookingRequest.AccountCode);
            dynamicParams.Add("StateId", asnBookingRequest.StateId);
            if (asnBookingRequest != null && !string.IsNullOrWhiteSpace(asnBookingRequest.ConsignmentNumber))
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    try
                    {
                        await connection.OpenAsync();
                        var result = await connection.ExecuteAsync(updateSql, dynamicParams);
                        if (result > 0)
                            isAsnBookingUpdated = true;
                    }
                    catch (Exception ex)
                    {
                        await Logger.Log($"Exception Occurred while updating asn booking. Message : {ex.Message}", Name());
                    }
                }
            }
            return isAsnBookingUpdated;
        }

        public async Task<bool> ReleaseAsnBooking(List<XCabAsnBooking> lstXcabAsnBooking)
        {
            bool isAsnBookingUpdated = true;
            var xCabBookings = 0;
            try
            {
                var sql = @"UPDATE [dbo].[xCabBooking] SET Cancelled = 0, OkToUpload = 1, ActionImmediate = 1 WHERE BookingId in @BookingId";

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("BookingId", lstXcabAsnBooking.Select(x => x.BookingId).ToList());           

                    xCabBookings = (connection.ExecuteAsync(sql, dynamicParameters).Result);
                }
            }
            catch (Exception e)
            {
                isAsnBookingUpdated = false;
                await Logger.Log(
                    "Exception Occurred in XCabBookingRepository: GetAsnXCabBookingsForAccount, message: " +
                    e.Message, "XCabBookingRepository");
            }
            return isAsnBookingUpdated;
        }

        private string Name()
        {
            return GetType().Name;
        }
    }
}
