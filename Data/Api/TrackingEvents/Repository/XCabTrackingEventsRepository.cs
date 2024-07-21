using Core;
using Dapper;
using Data.Api.TrackingEvents.Model;
using Microsoft.Data.SqlClient;

namespace Data.Api.TrackingEvents.Repository
{
	public class XCabTrackingEventsRepository : IXCabTrackingEventsRepository
	{

		public async Task<XCabTrackingEvent> GetTrackingEventsForReference(DateTime fromDate, DateTime toDate, string accountCode, string reference)
		{
			XCabTrackingEvent xCabTrackingEvent = null;
			var sql = "";
			try
			{
				if (!string.IsNullOrEmpty(accountCode))
				{
					sql =
						$@"select B.DriverNumber,B.Completed,
                    SUBSTRING(B.DeliveryCompleteLocation,1,CHARINDEX(',', B.DeliveryCompleteLocation)-1) As DeliveryCompleteLatitude,
                    SUBSTRING(B.DeliveryCompleteLocation,CHARINDEX(',', B.DeliveryCompleteLocation)+1, LEN(B.DeliveryCompleteLocation)) As DeliveryCompleteLongitude,
                    B.DeliveryComplete As DeliveryCompleteDateTime from 
                        xCabBooking B
                        inner join eint.xCabExtraReferences r
                        on r.PrimaryBookingId = B.BookingId
                    WHERE                        
                        b.DespatchDateTime between '{fromDate.ToString("yyyy - MM - dd")}' AND '{toDate.ToString("yyyy - MM - dd")}' 
                        AND r.Name = 'DeliveryId' 
                        AND r.Value = '{reference}' 
                        AND b.accountcode = '{accountCode}'";
				}
				else
				{
					sql =
						$@"select B.DriverNumber,B.Completed,
                        SUBSTRING(B.DeliveryCompleteLocation,1,CHARINDEX(',', B.DeliveryCompleteLocation)-1) As DeliveryCompleteLatitude,
                        SUBSTRING(B.DeliveryCompleteLocation,CHARINDEX(',', B.DeliveryCompleteLocation)+1, LEN(B.DeliveryCompleteLocation)) As DeliveryCompleteLongitude,
                        B.DeliveryComplete As DeliveryCompleteDateTime from 
                        xCabBooking B
                        inner join eint.xCabExtraReferences r
                        on r.PrimaryBookingId = B.BookingId
                    WHERE                        
                        b.DespatchDateTime between '{fromDate.ToString("yyyy - MM - dd")}' AND '{toDate.ToString("yyyy - MM - dd")}'
                        AND r.Name = 'DeliveryId' 
                        AND r.Value = '{reference}";
				}
				using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
				{
					await connection.OpenAsync();
					xCabTrackingEvent = await connection.QueryFirstOrDefaultAsync<XCabTrackingEvent>(sql);
					//check if the reference is not found in the extra references table
					if (xCabTrackingEvent == null)
					{
						var sqlCheckRefInxCabBookings = $@"select B.DriverNumber,B.Completed,
                        SUBSTRING(B.DeliveryCompleteLocation,1,CHARINDEX(',', B.DeliveryCompleteLocation)-1) As DeliveryCompleteLatitude,
                        SUBSTRING(B.DeliveryCompleteLocation,CHARINDEX(',', B.DeliveryCompleteLocation)+1, LEN(B.DeliveryCompleteLocation)) As DeliveryCompleteLongitude
                        ,B.DeliveryComplete As DeliveryCompleteDateTime from 
                        xCabBooking B
                        WHERE        
                            b.DespatchDateTime between '{fromDate.ToString("yyyy - MM - dd")}' AND '{toDate.ToString("yyyy - MM - dd")}' 
                            AND Ref1 = '{reference}'";

						xCabTrackingEvent = await connection.QueryFirstOrDefaultAsync<XCabTrackingEvent>(sqlCheckRefInxCabBookings);
						if (xCabTrackingEvent == null)
						{
							var sqlCheckRefInxCabClientReferences =
								$@"select B.DriverNumber,B.Completed,
                        SUBSTRING(B.DeliveryCompleteLocation,1,CHARINDEX(',', B.DeliveryCompleteLocation)-1) As DeliveryCompleteLatitude,
                        SUBSTRING(B.DeliveryCompleteLocation,CHARINDEX(',', B.DeliveryCompleteLocation)+1, LEN(B.DeliveryCompleteLocation)) As DeliveryCompleteLongitude
                        ,B.DeliveryComplete As DeliveryCompleteDateTime from 
                        xCabBooking B
                        inner JOIN xCabClientReferences xr on B.BookingId = xr.PrimaryJobId 
                        WHERE                        
							b.DespatchDateTime between '{fromDate.ToString("yyyy - MM - dd")}' AND '{toDate.ToString("yyyy - MM - dd")}'  
                            AND (Ref1 ='{reference}' or xr.Reference1 ='{reference}')";

							xCabTrackingEvent = await connection.QueryFirstOrDefaultAsync<XCabTrackingEvent>(sqlCheckRefInxCabClientReferences);

						}
					}
				}
			}
			catch (Exception e)
			{
				await Logger.Log("Exception occurred in GetTrackingEventsForReference, message: " + e.Message, nameof(XCabTrackingEventsRepository));

			}
			return xCabTrackingEvent;
		}

	public bool UpdateXCabTrackingEvents(ICollection<XCabTrackingEvent> trackingEvents)
	{
		throw new NotImplementedException();
	}
}
}
