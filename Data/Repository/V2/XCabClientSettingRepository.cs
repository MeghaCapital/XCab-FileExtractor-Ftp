using Core;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public class XCabClientSettingRepository : IXCabClientSettingRepository
	{
        public async Task<bool> IsStagedBooking(int ftpLoginId, int state, string accountCode, string serviceCode = null)
        {
            var stageBookings = false;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("FtpLoginId", ftpLoginId);
                    dynamicParams.Add("State", state);
                    dynamicParams.Add("AccountCode", accountCode);
                    dynamicParams.Add("ServiceCode", serviceCode);
                    const string sql = @"SELECT StageBookingAPIJobs FROM [dbo].[xCabClientSetting] 
                                            WHERE FtpLoginId = @FtpLoginId 
                                            AND StateId = @State  
                                            AND AccountCode = @AccountCode 
                                            AND Active = 1 
                                            AND StageBookingOnServiceCodes = 0
                                                UNION 
                                                SELECT StageBookingAPIJobs FROM [dbo].[xCabClientSetting] cs 
                                                       INNER JOIN [dbo].[xCabClientSettingOnServices] co 
                                                       ON co.ClientSettingId = cs.Id
                                                       WHERE cs.FtpLoginId = @FtpLoginId 
                                                       AND cs.StateId = @State  
                                                       AND cs.AccountCode = @AccountCode 
                                                       AND COALESCE(co.ServiceCode,'N/A') = @ServiceCode 
                                                       AND cs.Active = 1 
                                                       AND co.Active = 1 
                                                       AND cs.StageBookingOnServiceCodes = 1";

                    stageBookings = connection.Query<bool>(sql, dynamicParams).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                stageBookings = false;
                await Logger.Log($"Exception occurred when identifying whether a staged booking client. Message: {e.Message}", nameof(XCabClientSettingRepository));
            }
            return stageBookings;
        }
    }
}
