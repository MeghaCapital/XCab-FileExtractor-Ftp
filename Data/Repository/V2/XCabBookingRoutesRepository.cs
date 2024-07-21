using Core;
using Dapper;
using Data.Model.Route;
using Microsoft.Data.SqlClient;

namespace Data.Repository.V2
{
	public class XCabBookingRoutesRepository : IXCabBookingRoutesRepository
	{
		private const string shortLoadBarcodeExceptionReason = "Short Load";
		public async Task<ICollection<RouteBarcodeDetails>> GetBarcodesWithDropSequenceForRoute(RouteBarcodesRequest routeBarcodesRequest)
		{
			var routeBarcodeDetailsList = new List<RouteBarcodeDetails>();
			var dbArgs = new DynamicParameters();
			dbArgs.Add("RouteCode", routeBarcodesRequest.RouteCode);
			dbArgs.Add("StateId", routeBarcodesRequest.StateId);
			dbArgs.Add("AccountCode", routeBarcodesRequest.AccountCode);
			dbArgs.Add("DespatchDate", routeBarcodesRequest.DespatchDate);
			dbArgs.Add("ShortLoad", shortLoadBarcodeExceptionReason);
			using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
			{
				try
				{
					var sqlToFindBarcodesForRoute = $@"SELECT I.Barcode, I.Description, R.DropSequence
														FROM 
														dbo.xCabBookingRoutes R
															INNER JOIN dbo.xCabBooking B ON B.BookingId = R.BookingId
															INNER JOIN dbo.xCabItems I ON I.BookingId = R.BookingId							
														WHERE 
															R.Route = @RouteCode
															AND B.StateId = @StateId
															AND B.AccountCode = @AccountCode
															AND COALESCE(CONVERT(Date,B.DespatchDateTime), CONVERT(Date,B.UploadDateTime)) = CONVERT(Date, @DespatchDate)
															AND B.ServiceCode = 'CPOD'
															AND B.Cancelled = 0
															AND B.TPLUS_JobNumber IS NULL
														UNION
														SELECT I.Barcode, I.Description, R.DropSequence
															FROM 
															dbo.xCabBookingRoutes R
																INNER JOIN dbo.xCabBooking B ON B.BookingId = R.BookingId
																INNER JOIN dbo.xCabItems I ON I.BookingId = R.BookingId
																INNER JOIN bcs.BarcodeScan S ON S.JobNumber = B.TPLUS_JobNumber 
																	AND S.StateId = B.StateId 
																	AND S.AccountCode =  B.AccountCode 
																	AND S.BarCode = I.Barcode
																	AND COALESCE(CONVERT(Date,B.DespatchDateTime), CONVERT(Date,B.UploadDateTime)) = S.JobDate 
																	AND S.SubJobNumber = 1 AND S.Status = 3
																LEFT JOIN bcs.BarcodeException E ON E.BarcodeId = S.id 
															WHERE 
																R.Route = @RouteCode
																AND B.StateId = @StateId
																AND B.AccountCode = @AccountCode
																AND COALESCE(CONVERT(Date,B.DespatchDateTime), CONVERT(Date,B.UploadDateTime)) = CONVERT(Date, @DespatchDate)
																AND B.ServiceCode = 'CPOD'
																AND B.Cancelled = 0
																AND E.ExceptionReason != @ShortLoad
																AND B.TPLUS_JobNumber IS NOT NULL";

					await connection.OpenAsync();
					routeBarcodeDetailsList = (List<RouteBarcodeDetails>)await connection.QueryAsync<RouteBarcodeDetails>(sqlToFindBarcodesForRoute, dbArgs, commandTimeout: 60000);
				}
				catch (Exception ex)
				{
					await Logger.Log(
						$"Exception Occurred in XCabBookingRoutesRepository: GetBarcodesWithDropSequenceForRoute, message: {ex.Message}", "XCabBookingRepository");
				}
			}
			return routeBarcodeDetailsList;
		}
	}
}
