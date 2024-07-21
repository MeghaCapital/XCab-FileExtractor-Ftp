using Dapper;
using Data.Model;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabClientSettingRepository : IXCabClientSettingRepository
    {
        public ICollection<XCabClientSetting> GetXCabClientSetting()
        {
            ICollection<XCabClientSetting> xCabClientSetting = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"
                                SELECT [Id], [FtpLginId], [AccountCode], [StateId], [BarcodesAllowed],[Active] FROM [xCabClientSetting] WHERE Active =1";
                    xCabClientSetting = connection.Query<XCabClientSetting>(sql).ToList();
                }
                catch (Exception e)
                {
                    Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientSetting, method: GetXCabClientSetting, exception:" + e.Message, "XCabClientSettingRepository");
                }
            }
            return xCabClientSetting;
        }

        public XCabClientSetting GetXCabClientSetting(int ftpLoginId, int state, string accountCode, string serviceCode = null)
        {
            XCabClientSetting xCabClientSetting = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("FtpLoginId", ftpLoginId);
                    dynamicParameters.Add("AccountCode", accountCode);
                    dynamicParameters.Add("ServiceCode", serviceCode);
                    dynamicParameters.Add("State", state);
                    const string sql = @"SELECT [Id], 
                                   [FtpLoginId]
                                  ,[AccountCode]
                                  ,[StateId]
                                  ,[BarcodesAllowed]
                                  ,[StageBookingAPIJobs]
                                  ,[StageBookingOnServiceCodes]
                                  ,[Active]
                                  ,[MapRef3ToRef2]   
                                    FROM [dbo].[xCabClientSetting] 
                                    WHERE FtpLoginId = @FtpLoginId AND StateId = @State  AND AccountCode = @AccountCode AND Active = 1 and StageBookingOnServiceCodes = 0
                                    Union 
                                    SELECT 
                                   cs.[Id], 
                                   cs.[FtpLoginId]
                                  ,cs.[AccountCode]
                                  ,cs.[StateId]
                                  ,cs.[BarcodesAllowed]
                                  ,cs.[StageBookingAPIJobs]
                                  ,cs.[StageBookingOnServiceCodes]
                                  ,cs.[Active]
                                  ,cs.[MapRef3ToRef2]  
                                    FROM [dbo].[xCabClientSetting] cs 
                                           INNER JOIN[dbo].[xCabClientSettingOnServices] co On co.ClientSettingId = cs.Id
                                            WHERE cs.FtpLoginId = @FtpLoginId AND cs.StateId = @State  AND cs.AccountCode = @AccountCode AND COALESCE(co.ServiceCode,'N/A') = @ServiceCode AND cs.Active = 1 AND co.Active = 1 and cs.StageBookingOnServiceCodes = 1";


                    xCabClientSetting = connection.Query<XCabClientSetting>(sql, dynamicParameters).FirstOrDefault();

                }
                catch (Exception e)
                {
                    Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientSetting, method: GetXCabClientSetting, exception:" + e.Message, "XCabClientSettingRepository");
                }
            }
            return xCabClientSetting;
        }

        public bool IsBarcodeAllowed(int loginId, string accountCode, int stateId)
        {
            bool barcodeAllowed = false;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("LoginId", loginId);
                    dynamicParams.Add("StateId", stateId);
                    dynamicParams.Add("AccountCode", accountCode);
                    const string sql = @"
                                SELECT COUNT(*) FROM [xCabClientSetting] WHERE Active =1 AND FtpLoginId = @LoginId AND StateId=@StateId AND AccountCode = @AccountCode";
                    var rows = connection.ExecuteScalar<int>(sql, dynamicParams);
                    if (rows > 0)
                        barcodeAllowed = true;
                }
                catch (Exception e)
                {
                    Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientSetting, method:IsBarcodeAllowed, exception:" + e.Message, "XCabClientSettingRepository");
                }
            }
            return barcodeAllowed;
        }
    }
}
