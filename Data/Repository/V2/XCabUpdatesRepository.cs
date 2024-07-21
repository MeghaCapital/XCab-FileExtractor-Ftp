using Core;
using Dapper;
using Data.Model;
using Microsoft.Data.SqlClient;

namespace Data.Repository.V2
{
	public class XCabUpdatesRepository
	{
		private const string daysFrom = "-12";
		public async Task<ICollection<CcrXCabTrackingJob>> GetXCabBookingUpdates(int xCabBookingIdToTest = 0)
		{
			var xCabBookingUpdates = new List<CcrXCabTrackingJob>();
			try
			{
				using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
				{
					await connection.OpenAsync();
					var sql = @$"DECLARE @CompletedJobs AS TABLE (
										JOB_NUMBER int,
										JOB_DATE datetime,
										State int,
										PICKUP_ARRIVE datetime,
										PICKUP_COMPLETE datetime,
										DELIVERY_ARRIVE datetime,
										DELIVERY_COMPLETE datetime,
										CLIENT_CODE nvarchar(50),
										BOOKED_TIME datetime,
										ALLOCATED datetime,
										FROM_DETAIL1 nvarchar(100),
										FROM_DETAIL2 nvarchar(100),
										FROM_DETAIL3 nvarchar(100), 
										FROM_DETAIL4 nvarchar(100), 
										FROM_DETAIL5 nvarchar(100), 
										FROM_SUBURB nvarchar(50),
										FROM_POSTCODE nvarchar(5),
										TO_DETAIL1 nvarchar(100),
										TO_DETAIL2 nvarchar(100),
										TO_DETAIL3 nvarchar(100),
										TO_DETAIL4 nvarchar(100),
										TO_DETAIL5 nvarchar(100),
										TO_SUBURB nvarchar(50),
										TO_POSTCODE nvarchar(5),
										DEL_POD_NAME nvarchar(100)
										);
									INSERT INTO @CompletedJobs
											SELECT JOB_NUMBER, 
											JOB_DATE, 
											State, 
											CASE WHEN datepart(year,PICKUP_ARRIVE)=1980 THEN PICKUP_COMPLETE ELSE PICKUP_ARRIVE END 
											PICKUP_ARRIVE, 
											PICKUP_COMPLETE, 
											CASE WHEN datepart(year,DELIVERY_ARRIVE)=1980 THEN DELIVERY_COMPLETE ELSE DELIVERY_ARRIVE END 
											DELIVERY_ARRIVE, 
											DELIVERY_COMPLETE, 
											CLIENT_CODE, 
											BOOKED_TIME,
											ALLOCATED,
											FROM_ADD_1,
											FROM_ADD_2,
											FROM_ADD_3,
											FROM_ADD_4,
											FROM_ADD_5,
											FROM_SUB,
											FROM_PC,
											TO_ADD_1,
											TO_ADD_2,
											TO_ADD_3,
											TO_ADD_4,
											TO_ADD_5,
											TO_SUB,
											TO_PC, DEL_POD_NAME
										FROM [dwh12].[TPlus].[dbo].[Jobs] 
										WHERE JOB_DATE > GETDATE() {daysFrom};
									DECLARE @XcabUpdates AS TABLE (
										BookingId int,
										LoginId int,
										StateId int,
										AccountCode varchar(50),
										JobNumber varchar(10),
										ExternalClientIntegrationId int,
										TrackingSchemaName varchar(50),
										CcrPickupArrive datetime,
										CcrPickupComplete datetime,
										CcrDeliveryArrive datetime,
										CcrDeliveryComplete datetime,
										XCabPickupArrive datetime,
										XCabPickupComplete datetime,
										XCabDeliveryArrive datetime,
										XCabDeliveryComplete datetime,
										JobBookingDay datetime,
										JobAllocationDate datetime,
										FromDetail1 varchar(100),
										FromDetail2 varchar(100),
										FromDetail3 varchar(100), 
										FromDetail4 varchar(100), 
										FromDetail5 varchar(100), 
										FromSuburb varchar(50),
										FromPostcode varchar(100),
										ToDetail1 varchar(100),
										ToDetail2 varchar(100),
										ToDetail3 varchar(100),
										ToDetail4 varchar(100),
										ToDetail5 varchar(100),
										ToSuburb varchar(50),
										ToPostcode varchar(100),
										Ref1 varchar(50),
										Ref2 varchar(50),
										ConsignmentNumber varchar(50),
										DriverId int,
										ServiceCode varchar(50),
										UserName varchar(50),
										TrackingFolderName varchar(50), PodName varchar(100), Remoteftphostname varchar(100), RemoteFtpUserName varchar(50), RemoteFtpPassword varchar(50), Remotetrackingfoldername varchar(50)
										);
									INSERT INTO @XcabUpdates 
										SELECT 
											x.BookingId,
											x.LoginId,	
											x.StateId,
											x.AccountCode,
											x.TPLUS_JobNumber,
											f.ExternalClientIntegrationId,
											f.TrackingSchemaName,
											r.PICKUP_ARRIVE CcrPickupArrive,
											r.PICKUP_COMPLETE CcrPickupComplete,
											r.DELIVERY_ARRIVE CcrDeliveryArrive,
											r.DELIVERY_COMPLETE CcrDeliveryComplete,
											x.PickupArrive as XCabPickupArrive,
											x.PickupComplete as XCabPickupComplete,
											x.DeliveryArrive as XCabDeliveryArrive,
											x.DeliveryComplete as XCabDeliveryComplete,
											x.DateInserted as JobBookingDay,
											x.TPLUS_JobAllocationDate as JobAllocationDate,
											x.FromDetail1 as FromDetail1, 
									        x.FromDetail2 as FromDetail2,
									        x.FromDetail3 as FromDetail3, 
									        x.FromDetail4 as FromDetail4, 
									        x.FromDetail5 as FromDetail5, 
									        x.FromSuburb as FromSuburb,
									        x.FromPostcode as FromPostcode,
									        x.ToDetail1 as ToDetail1, 
									        x.ToDetail2 as ToDetail2, 
									        x.ToDetail3 as ToDetail3, 
									        x.ToDetail4 as ToDetail4, 
									        x.ToDetail5 as ToDetail5, 
									        x.ToSuburb as ToSuburb,
									        x.ToPostcode as ToPostcode,
											x.Ref1 as Ref1,
											x.Ref2 as Ref2,
											x.ConsignmentNumber as ConsignmentNumber, 
											x.DriverNumber as DriverId,
											x.ServiceCode as ServiceCode,
											f.username as UserName,
											f.trackingfoldername as TrackingFolderName,
											r.DEL_POD_NAME as PodName,
											f.remoteftphostname as Remoteftphostname,
											f.remoteftpusername as RemoteFtpUserName,
											f.remoteftppassword as RemoteFtpPassword, 
											f.Remotetrackingfoldername as Remotetrackingfoldername
										FROM
											xCabBooking x
											RIGHT OUTER JOIN @CompletedJobs r ON r.JOB_NUMBER = x.TPLUS_JobNumber
											AND (r.JOB_DATE BETWEEN convert(date,dateadd(day,-5, x.TPLUS_JobAllocationDate)) 
											AND convert(date, dateadd(day,5,x.TPLUS_JobAllocationDate))) 
											AND x.stateid = r.State
											Join XCabFtpLoginDetails f ON f.id = x.LoginId
										WHERE
											x.UploadedToTplus = 1	-- Only compare against uploaded jobs
											AND x.TPLUS_JobAllocationDate > GETDATE() {daysFrom}
											AND ( -- At least one of the events is null
											x.PickupArrive IS NULL
											OR x.PickupComplete IS NULL
											OR x.DeliveryArrive IS NULL
											OR x.DeliveryComplete IS NULL
											)
											AND x.LoginId NOT IN (3)
									SELECT DISTINCT
										BookingId,
										LoginId,
										StateId,
										AccountCode,
										JobNumber,
										ExternalClientIntegrationId,
										TrackingSchemaName,
										CcrPickupArrive,
										CcrPickupComplete,
										CcrDeliveryArrive,
										CcrDeliveryComplete,
										XCabPickupArrive,
										XCabPickupComplete,
										XCabDeliveryArrive,
										XCabDeliveryComplete,
										JobBookingDay,
										JobAllocationDate,
										FromDetail1,
										FromDetail2,
										FromDetail3, 
										FromDetail4, 
										FromDetail5, 
										FromSuburb,
										FromPostcode,
										ToDetail1,
										ToDetail2,
										ToDetail3,
										ToDetail4,
										ToDetail5,
										ToSuburb,
										ToPostcode,
										Ref1, 
										Ref2, 
										ConsignmentNumber,
										DriverId,
										ServiceCode,
										UserName,
										TrackingFolderName, PodName, Remoteftphostname, RemoteFtpUserName,RemoteFtpPassword, Remotetrackingfoldername
									FROM @XcabUpdates";
#if DEBUG
					//sql += " WHERE BookingId = " + xCabBookingIdToTest;
					sql += " WHERE BookingId in (8710627,8710772,8710644,8743305,8710690,8710700,8710708,8710725,8710732,8710741,8710749,8710753,8710768,8710784,8710818,8710798,8710800,8710811,8710827,8710855,8710864,8710873,8710913,8710922,8718122,8725210,8710692,8755466,8773858,8710646,8710648,8710657,8710660,8710661,8710681,8710702,8710715,8710728,8710730,8710731,8710787,8710775,8710780,8710785,8710791,8710810,8710816,8710826,8710830,8710837,8710848,8710865,8711051,8710624,8720603)";

#endif

                    xCabBookingUpdates = (List<CcrXCabTrackingJob>)await connection.QueryAsync<CcrXCabTrackingJob>(sql, commandTimeout: 180);
				}
			}
			catch (Exception e)
			{
				await Logger.Log(
					 "Exception Occurred in XCabUpdatesRepository: GetXCabBookingUpdates, message: " +
					 e.Message, "XCabUpdatesRepository");
			}

			return xCabBookingUpdates;
		}
	}
}
