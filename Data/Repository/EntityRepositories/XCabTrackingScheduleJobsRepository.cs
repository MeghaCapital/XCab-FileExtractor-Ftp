using Core;
using Dapper;
using Data.Entities;
using Data.Model;
using Data.Repository.EntityRepositories.Interfaces;
using Data.Utils;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.EntityRepositories
{
	public class XCabTrackingScheduleJobsRepository : IXCabTrackingScheduleJobsRepository
	{
		private CalculateDates _dateUtil;

		public XCabTrackingScheduleJobsRepository()
		{
			_dateUtil = new CalculateDates();
		}

		public void Insert(XCabTracking tracking, XCabBookingNT12Jobs booking)
		{
			DateTime lastTrackTime = DateTime.MinValue;
			char trackType = 'U';

			try
			{
				DateTime? pickupCompletionTime = booking.PickupComplete;
				DateTime? deliveryCompletionTime = null;
				if (!string.IsNullOrWhiteSpace(booking.DeliveryComplete.ToString()))
					deliveryCompletionTime = booking.DeliveryComplete;

				if (!deliveryCompletionTime.HasValue)
				{
					if (pickupCompletionTime > tracking.LastRunTime)
					{
						trackType = 'P';
					}
				}
				else
				{
					if (deliveryCompletionTime > tracking.LastRunTime)
					{
						if (pickupCompletionTime <= tracking.LastRunTime)
						{
							trackType = 'D';
						}
						else
						{
							trackType = 'B';
						}
					}
				}
				lastTrackTime = _dateUtil.GetLocalDateByState(tracking.StateId);
			}
			catch (Exception ex)
			{
				Logger.Log("Exception Occurred while determining the trackType. Message : " + ex.Message, "XCabTrackingScheduleJobsRepository");
			}

			using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
			{
				try
				{
					connection.Open();
					string sql = $@"INSERT INTO
	                                   [dbo].[XCabTrackingScheduleJobs]
	                                    (
		                                    TrackScheduleId, 
		                                    TPLUSJobNumber, 
		                                    LastTrackTime, 
		                                    TrackType
	                                    )
                                    VALUES
	                                    (
		                                    {Convert.ToString(tracking.Id)}, 
		                                    {Convert.ToString(booking.Tplus_JobNumber)}, 
		                                    '{lastTrackTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}', 
		                                    '{trackType}'
	                                    )";
					connection.Execute(sql);
				}
				catch (Exception ex)
				{
					Logger.Log("Exception Occurred while inserting into XCabTrackingScheduleJobs. Message : " + ex.Message, "XCabTrackingScheduleJobsRepository");
				}
			}
		}
	}
}