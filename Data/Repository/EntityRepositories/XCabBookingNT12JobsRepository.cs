using Core;
using Dapper;
using Data.Model;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabBookingNT12JobsRepository : IXCabBookingNT12JobsRepository
    {
        public IEnumerable<XCabBookingNT12Jobs> Get(string accountCode, int state, int loginId)
        {
            ICollection<XCabBookingNT12Jobs> xcabBookingNt12Jobs = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = $@"SELECT CB.TPLUS_JobNumber,
                                    CB.AccountCode,
                                    CB.Ref1,
                                    CB.Ref2,
                                    CB.Caller,
                                    '' ServiceSlot,
                                    CB.DriverNumber,
                                    CB.FromSuburb,
                                    CB.FromDetail1,
                                    CB.FromDetail2,
                                    CB.FromDetail3,
                                    CB.FromDetail4,
                                    CB.FromDetail5,
                                    CB.ToSuburb,
                                    CB.ToDetail1,
                                    Cb.ToDetail2,
                                    CB.ToDetail3,
                                    CB.ToDetail4,
                                    CB.ToDetail5,
                                    0 Distance,
                                    CB.UploadDateTime,
                                    CB.PickupArrive,
                                    CB.PickupComplete,
                                    CB.DeliveryArrive,
                                    CB.DeliveryComplete,
                                    (SELECT JB.DEL_POD_NAME 
									FROM [dwh12].[Tplus].[dbo].[Jobs] JB  WHERE CONVERT(Date,JB.JOB_DATE) = CONVERT(Date,CB.UploadDateTime) AND JB.JOB_NUMBER =CB.TPLUS_JobNumber AND JB.State =CB.StateId) PODName
                                    FROM[dbo].[xCabBooking] CB
                                    INNER JOIN [dbo].[XCabTrackingSchedule] TS ON TS.StateId = CB.StateId AND TS.AccountCode = CB.AccountCode AND TS.LoginId = CB.LoginId AND TS.Enabled = 1
                                    WHERE CB.AccountCode = '{accountCode}' AND CB.StateId = {Convert.ToString(state)} AND CB.LoginId = {Convert.ToString(loginId)} AND CB.Cancelled = 0 AND (CB.PickupComplete > DATEADD(Minute,-6,(COALESCE(TS.LastRunTime,CONVERT(date, getdate())))) OR CB.DeliveryComplete > DATEADD(Minute,-6,(COALESCE(TS.LastRunTime,CONVERT(date, getdate())))))";
                    xcabBookingNt12Jobs = connection.Query<XCabBookingNT12Jobs>(sql).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Log("Exception Occurred while extracting trcking details through Tracking Manager for Account : " + accountCode + ". Message :" + ex.Message, "XCabBookingNT12JobsRepository");
                }
            }
            return xcabBookingNt12Jobs;
        }

        public int? Get(string jobNumber, int stateId, string accountCode, string fromSuburb, string fromPostcode, string toSuburb, string toPostcode, DateTime dateInserted)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("StateId", stateId);
                    dbArgs.Add("AccountCode", accountCode);
                    dbArgs.Add("JobNumber", jobNumber);
                    dbArgs.Add("FromSuburb", fromSuburb);
                    dbArgs.Add("FromPostcode", fromPostcode);
                    dbArgs.Add("ToSuburb", toSuburb);
                    dbArgs.Add("ToPostcode", toPostcode);
                    dbArgs.Add("DateInserted", dateInserted);
                    const string sql = @"select top 1
	                                        [BookingId]
                                        from 
	                                        [dbo].[xCabBooking] [b] 
                                        where 
	                                        [b].[TPLUS_JobNumber] = @JobNumber 
	                                        and [b].[StateId] = @StateId
                                            and [b].[AccountCode] = @AccountCode
	                                        and [b].[FromSuburb] = @FromSuburb 
	                                        and [b].[FromPostcode] = @FromPostcode 
	                                        and [b].[ToSuburb] = @ToSuburb
	                                        and [b].[ToPostcode] = @ToPostcode
	                                        order by [b].[DateInserted] desc";
                    return connection.Query<int>(sql, dbArgs).ToList().FirstOrDefault();
                }
                catch (Exception)
                {
                    //Logger.Log("Exception occurred while extracting booking id via booking details : " + accountCode + ". Message :" + ex.Message, "XCabBookingNT12JobsRepository");
                }
            }
            return null;
        }

        public string Get(int stateId, string jobNumber, DateTime dateInserted)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("StateId", stateId);
                    dbArgs.Add("JobNumber", jobNumber);
                    dbArgs.Add("DateInserted", dateInserted.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    const string sql = @"select 
	                                        a.ApiKey
                                        from 
	                                        xCabAuthorizedAccounts a
	                                        left outer join xCabBooking b on a.LoginId = b.LoginId and a.AccountCode = b.AccountCode and a.StateId = b.StateId
                                        where
	                                        b.StateId = @StateId
	                                        and b.TPLUS_JobNumber = @JobNumber
	                                        and b.DateInserted = @DateInserted";
                    return connection.Query<string>(sql, dbArgs).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Logger.Log("Exception occurred while extracting api key via booking : " + jobNumber + ". Message :" + ex.Message, "XCabBookingNT12JobsRepository");
                }
            }
            return null;
        }
    }
}
