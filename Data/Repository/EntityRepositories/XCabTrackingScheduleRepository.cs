using Core;
using Dapper;
using Data.Entities;
using Data.Repository.EntityRepositories.Interfaces;
using Data.Utils;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabTrackingScheduleRepository : IXCabTrackingScheduleRepository
    {
        private CalculateDates _dateUtil;

        public XCabTrackingScheduleRepository()
        {
            _dateUtil = new CalculateDates();
        }

        public IEnumerable<XCabTracking> Get(bool enabled)
        {
            ICollection<XCabTracking> xcabTrackings = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = $@"SELECT [Id]
                                ,[LoginId]
                                ,[AccountCode]
                                ,[StateId]
                                ,[EmailAddress]
                                ,[Enabled]
                                ,[LastRunTime]
                                ,[EmailSubject]
                                ,[EmailBody]
                                FROM [dbo].[XCabTrackingSchedule]
                                where Enabled = {(enabled ? 1 : 0)}";
                    xcabTrackings = connection.Query<XCabTracking>(sql).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Log("Exception Occurred while selecting data from XCabTrackingSchedule. Message : " + ex.Message, "XCabTrackingScheduleJobsRepository");
                }
            }
            return xcabTrackings;
        }

        public void Update(XCabTracking tracking)
        {
            DateTime lastRunTime = _dateUtil.GetLocalDateByState(tracking.StateId);
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = $@"UPDATE
	                                    [dbo].[XCabTrackingSchedule]
                                    SET
	                                    [LastRunTime] = '{lastRunTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}'
                                    WHERE
	                                    [Id] = {Convert.ToString(tracking.Id)}";
                    connection.Execute(sql);
                }
                catch (Exception ex)
                {
                    Logger.Log("Exception Occurred while updating data in XCabTrackingSchedule for id : " + tracking.Id + ". Message : " + ex.Message, "XCabTrackingScheduleJobsRepository");
                }
            }
        }
    }
}
