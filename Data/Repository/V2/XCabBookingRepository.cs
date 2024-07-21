using Core;
using Dapper;
using Data.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Api.TrackingEvents.Model;
using Data.Api.TrackingEvents;
using Data.Utils.Helpers;
using Data.Model;
using Core.Helpers;
using Data.Model.Tracking;
using System.Data;
using Data.Entities.Items;
using Data.Utils;
using Newtonsoft.Json;
using Quartz.Util;
using static Data.Api.Bookings.BookingModel;
using Data.Api.Bookings;

namespace Data.Repository.V2
{
    public class XCabBookingRepository : IXCabBookingRepository
    {
        private const int MaxRows = 300;
        private readonly int DaysUpto = -150;
        private readonly int DaysTill = 1;
        private const string consolidatedConsignmentValue = "CONSOLIDATED";
        private const int maxCharacterLength = 50;

        public async Task<bool> CancelXCabBooking(int bookingId)
        {
            bool result = true;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("BookingId", bookingId);
                    string sqlUpdate = "";
                    sqlUpdate = "UPDATE xCabBooking " +
                                " SET Cancelled = 1, OkToUpload=0, " +
                                " LastModified=GETDATE() " +
                                " WHERE " +
                                " BookingId = @BookingId";
                    await connection.ExecuteAsync(sqlUpdate, dbArgs);
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }

        public async Task<ICollection<XCabBookingDetails>> GetPendingXCabBookings(int? testBookingId)
        {
            var xCabBookings = new List<XCabBookingDetails>();
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    string where = "";
                    var sql = @"SELECT * FROM (SELECT TOP(" + MaxRows + @") 
								L.UsesSoap, 
								CD.NumContacts, 								
								L.username AS Username, 
								L.password AS Password, 
								L.Id As LoginId,
								L.trackingfoldername As TrackingFolderName,
								L.trackingschemaname As TrackingSchemaName,
								L.isbookingonlyclient As IsbookingOnlyClient, 
								X.BookingId, 
								X.StateId, 
								X.AccountCode, 
								X.ServiceCode, 
								X.FromSuburb, 
								X.FromPostcode, 
								X.FromDetail1, 
								X.FromDetail2,
								X.FromDetail3,
								X.FromDetail4,
								X.FromDetail5, 
								X.ToSuburb, 
								X.ToPostcode, 
								X.ToDetail1, 
								X.ToDetail2, 
								X.ToDetail3, 
								X.ToDetail4, 
								X.ToDetail5,
								X.Ref1, 
								X.Ref2,
								X.DespatchDateTime,
								COALESCE((CASE X.TotalWeight WHEN 0 THEN NULL ELSE X.TotalWeight END),I.tWeight) As TotalWeight,
								COALESCE((CASE X.TotalVolume WHEN 0 THEN NULL ELSE X.TotalVolume END),I.tCubic) As TotalVolume,
								(CASE WHEN Coalesce(I.tQty, I.tCount) = 0 THEN 1 ELSE Coalesce(I.tQty, I.tCount) END) AS TotalItems ,
								COALESCE(X.ConsignmentNumber,CONCAT('X', X.BookingId)) As ConsignmentNumber,
								X.PreAllocatedDriverNumber,
								X.Caller,
								X.ExtraDelInformation,
								X.IsQueued,
								X.ATL, 
								l.BookingsFolderName,
								COALESCE(S.BarcodesAllowed,0) BarcodesAllowed, 
								X.ExtraPuInformation, 
								T.StartDateTime As TimeSlotStart,
								T.Duration As TimeSlotDuration,
								U.RouteId,
								U.LegNumber ,
								U.RouteToCustomer,
								V.EmailAddress as UNSEmail,
								V.SmsNumber as UNSSMSNumber,
								R.NumRemarks as NumOfRemarks,
								I.tCount as NumOfItems,
								X.UsingComo,
                                COALESCE(S.TimeSlotsAllowed,0) TimeSlotsAllowed,
								BR.Route, 
								BR.DropSequence
							FROM 
								xCabBooking X 
								INNER JOIN xCabFtpLoginDetails L ON X.LoginId = L.id 
								LEFT JOIN (SELECT Count(ContactId) NumContacts, BookingId FROM  xCabContactDetails GROUP BY BookingId) CD
									ON X.BookingId = CD.BookingId
								LEFT JOIN xCabClientSetting S on S.FtpLoginId = L.Id and S.AccountCode = X.AccountCode and s.stateid = x.stateid 
								LEFT JOIN xCabTimeSlots T on T.BookingId = X.BookingId 
								LEFT JOIN xCabRouteLeg U on X.BookingId = U.BookingId
								LEFT JOIN xCabNotifications V on X.BookingId = v.BookingId
								LEFT JOIN (SELECT sum([weight]) tWeight,sum([Cubic]) tCubic, sum(Qantity) tQty, count(itemid) tCount, BookingId FROM xCabItems GROUP BY BookingId) I
									ON X.BookingId = I.BookingId
								LEFT JOIN (SELECT count(RemarkId) NumRemarks, BookingId FROM xCabRemarks GROUP BY BookingId) R
									ON X.BookingId = R.BookingId
                                LEFT JOIN xCabBookingRoutes BR 
								    ON BR.BookingId = X.BookingId
							";
#if DEBUG
                    if (testBookingId != null)
                        where = @"WHERE X.BookingId in (" + testBookingId + "))  AS Main";
                    else
                        return xCabBookings; //return nothing as in DEBUG mode a test id is needed
#else
                    where = @"WHERE
								X.OkToUpload = 1 
								AND X.Cancelled = 0 
								AND X.UploadedToTplus = 0 
								AND X.uploadDateTime is null 
								AND L.Active = 1 
								AND (CAST( X.DateInserted AS DATE) = CAST( GETDATE() AS DATE) OR X.ActionImmediate = 1)
								)  AS Main
							 ORDER BY CONCAT(COALESCE(Main.Route, 'ZZZZ'), right(replicate('0',4) + Main.DropSequence,4)),  CONVERT(nvarchar,Main.BookingId)";
#endif

                    sql += where;
                    xCabBookings = (List<XCabBookingDetails>)(connection.QueryAsync<XCabBookingDetails>(sql).Result);
				}
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred in XCabBookingRepository: GetPedningTplusJobs, message: " +
                    e.Message, "XCabBookingRepository");
            }

            return xCabBookings;
        }

        public async Task<ICollection<XCabTrackingEvent>> GetXCabTrackingEvents(XCabTrackingRequest xCabTrackingRequest)
        {
            var xCabTrackingEvents = new List<XCabTrackingEvent>();
            DateTime fromDate = DateTime.Now.AddDays((double)this.DaysUpto);
            DateTime toDate = DateTime.Now.AddDays((double)this.DaysTill);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    var accountCode = xCabTrackingRequest.AccountCode;
                    var stateId = StateHelpers.GetStateId(xCabTrackingRequest.State.ToString());
                    var uniqueReferenceValues = string.Join("','", xCabTrackingRequest.UniqueReferenceValue.ToArray());
                    var uniqueReferenceType =
                        !string.IsNullOrEmpty(
                            UniqueReferenceTypeHelper.GetUniqueReferenceTypeForDatabaseMapping(xCabTrackingRequest
                                .UniqueReferenceType))
                            ? UniqueReferenceTypeHelper.GetUniqueReferenceTypeForDatabaseMapping(xCabTrackingRequest
                                .UniqueReferenceType)
                            : null;
                    await connection.OpenAsync();
                    string sql = "";
                    if (uniqueReferenceType.ToUpper() != "ANY")
                    {
                        sql = $@"
								SELECT B.TPLUS_JobNumber,
									COALESCE(B.TPLUS_JobAllocationDate, B.DespatchDateTime, B.UploadDateTime)  AS JobBookingDateTime,
									B.TPLUS_JobAllocationDate AS AllocationDateTime,
									B.AccountCode,
									B.StateId,
									B.{uniqueReferenceType} As UniqueReferenceValue,
									B.Ref1,
									COALESCE(B.ConsignmentNumber, ('X' + CAST(B.BookingID AS varchar))) AS  ConsignmentNumber,
									B.Completed,
									B.DriverNumber,
									B.Cancelled,	
									B.OkToUpload,
								    B.UploadedToTplus,
									FORMAT(B.UploadDateTime,'yyyy-MM-dd HH:mm:ss')  UploadDateTime,
									B.PickupArrive As PickupArriveDateTime, 
									Substring(B.PickupArriveLocation,1,CharIndex(',', B.PickupArriveLocation)-1) As PickupArriveLatitude,
									Substring(B.PickupArriveLocation,CharIndex(',', B.PickupArriveLocation)+1, LEN(B.PickupArriveLocation)) As PickupArriveLongitude,
									B.PickupComplete As PickupCompleteDateTime,
									Substring(B.PickupCompleteLocation,1,CharIndex(',', B.PickupCompleteLocation)-1) As PickupCompleteLatitude,
									Substring(B.PickupCompleteLocation,CharIndex(',', B.PickupCompleteLocation)+1, LEN(B.PickupArriveLocation)) As PickupCompleteLongitude,
									B.DeliveryArrive As DeliveryArriveDateTime,
									Substring(B.DeliveryArriveLocation,1,CharIndex(',', B.DeliveryArriveLocation)-1) As DeliveryArriveLatitude,
									Substring(B.DeliveryArriveLocation,CharIndex(',', B.DeliveryArriveLocation)+1, LEN(B.DeliveryArriveLocation)) As DeliveryArriveLongitude,                  
									B.DeliveryComplete As DeliveryCompleteDateTime,
									Substring(B.DeliveryCompleteLocation,1,CharIndex(',', B.DeliveryCompleteLocation)-1) As DeliveryCompleteLatitude,
									Substring(B.DeliveryCompleteLocation,CharIndex(',', B.DeliveryCompleteLocation)+1, LEN(B.DeliveryCompleteLocation)) As DeliveryCompleteLongitude,
									B.ComoJobId
								FROM xCabBooking B LEFT JOIN xCabClientReferences R ON B.BookingId = R.PrimaryJobId 
								LEFT OUTER JOIN JobFutile F ON F.ConsignmentNumber = B.ConsignmentNumber
								WHERE 
									COALESCE(B.DateInserted,B.TPLUS_JobAllocationDate,B.DespatchDateTime) BETWEEN '{fromDate.ToString("yyyy - MM - dd")}' AND '{toDate.ToString("yyyy - MM - dd")}'
									AND B.AccountCode = '{accountCode}'
									AND B.StateId = {stateId} 
									AND B.{uniqueReferenceType} IN ('{uniqueReferenceValues}') OR B.ConsignmentNumber IN ('{uniqueReferenceValues}')
									AND F.Id IS NULL 
									ORDER BY FORMAT(B.UploadDateTime,'yyyy-MM-dd HH:mm:ss') DESC, B.BookingId DESC";
                    }
                    else
                    {
                        sql = $@"
								SELECT B.TPLUS_JobNumber,
									COALESCE(B.TPLUS_JobAllocationDate, B.DespatchDateTime, B.UploadDateTime)  AS JobBookingDateTime,
									B.TPLUS_JobAllocationDate AS AllocationDateTime,
									B.AccountCode,
									B.StateId,
									COALESCE(B.Ref1, B.ConsignmentNumber) As UniqueReferenceValue,
									B.Ref1,
									COALESCE(B.ConsignmentNumber, ('X' + CAST(B.BookingID AS varchar))) AS  ConsignmentNumber,
									B.Completed,
									B.DriverNumber,
									B.Cancelled,	
									B.OkToUpload,
								    B.UploadedToTplus,
									FORMAT(B.UploadDateTime,'yyyy-MM-dd HH:mm:ss')  UploadDateTime,
									B.PickupArrive As PickupArriveDateTime, 
									Substring(B.PickupArriveLocation,1,CharIndex(',', B.PickupArriveLocation)-1) As PickupArriveLatitude,
									Substring(B.PickupArriveLocation,CharIndex(',', B.PickupArriveLocation)+1, LEN(B.PickupArriveLocation)) As PickupArriveLongitude,
									B.PickupComplete As PickupCompleteDateTime,
									Substring(B.PickupCompleteLocation,1,CharIndex(',', B.PickupCompleteLocation)-1) As PickupCompleteLatitude,
									Substring(B.PickupCompleteLocation,CharIndex(',', B.PickupCompleteLocation)+1, LEN(B.PickupArriveLocation)) As PickupCompleteLongitude,
									B.DeliveryArrive As DeliveryArriveDateTime,
									Substring(B.DeliveryArriveLocation,1,CharIndex(',', B.DeliveryArriveLocation)-1) As DeliveryArriveLatitude,
									Substring(B.DeliveryArriveLocation,CharIndex(',', B.DeliveryArriveLocation)+1, LEN(B.DeliveryArriveLocation)) As DeliveryArriveLongitude,                  
									B.DeliveryComplete As DeliveryCompleteDateTime,
									Substring(B.DeliveryCompleteLocation,1,CharIndex(',', B.DeliveryCompleteLocation)-1) As DeliveryCompleteLatitude,
									Substring(B.DeliveryCompleteLocation,CharIndex(',', B.DeliveryCompleteLocation)+1, LEN(B.DeliveryCompleteLocation)) As DeliveryCompleteLongitude,
									B.ComoJobId
								FROM xCabBooking B LEFT JOIN xCabClientReferences R ON B.BookingId = R.PrimaryJobId 
								LEFT OUTER JOIN JobFutile F ON F.ConsignmentNumber = B.ConsignmentNumber
								WHERE 
									COALESCE(B.DateInserted,B.TPLUS_JobAllocationDate,B.DespatchDateTime) BETWEEN '{fromDate.ToString("yyyy - MM - dd")}' AND '{toDate.ToString("yyyy - MM - dd")}'
									AND B.AccountCode = '{accountCode}'
									AND B.StateId = {stateId} 
									AND (B.Ref1 IN ('{uniqueReferenceValues}') OR B.ConsignmentNumber IN ('{uniqueReferenceValues}'))
									AND F.Id IS NULL 
									ORDER BY FORMAT(B.UploadDateTime,'yyyy-MM-dd HH:mm:ss') DESC, B.BookingId DESC";
                    }

                    xCabTrackingEvents = (List<XCabTrackingEvent>)await connection.QueryAsync<XCabTrackingEvent>(sql);

                    //Identify consolidated consignments
                    var consolidatedReferences = xCabTrackingEvents.Where(x =>
                        x.Cancelled == true && x.ConsignmentNumber.ToUpper() == consolidatedConsignmentValue).ToList();
                    if (consolidatedReferences.Count > 0)
                    {
                        sql = $@"
							SELECT B.TPLUS_JobNumber,
								COALESCE(B.TPLUS_JobAllocationDate, B.DespatchDateTime, B.UploadDateTime)  AS JobBookingDateTime,
								B.TPLUS_JobAllocationDate AS AllocationDateTime,
								B.AccountCode,
								B.StateId,
								R.Reference1 As UniqueReferenceValue,
								R.Reference1,
								COALESCE(B.ConsignmentNumber, ('X' + CAST(B.BookingID AS varchar))) AS  ConsignmentNumber,
								B.Completed,
								B.DriverNumber,
								B.Cancelled,	
								B.OkToUpload,
								B.UploadedToTplus,
								FORMAT(B.UploadDateTime,'yyyy-MM-dd HH:mm:ss') UploadDateTime,
								B.PickupArrive As PickupArriveDateTime, 
								Substring(B.PickupArriveLocation,1,CharIndex(',', B.PickupArriveLocation)-1) As PickupArriveLatitude,
								Substring(B.PickupArriveLocation,CharIndex(',', B.PickupArriveLocation)+1, LEN(B.PickupArriveLocation)) As PickupArriveLongitude,
								B.PickupComplete As PickupCompleteDateTime,
								Substring(B.PickupCompleteLocation,1,CharIndex(',', B.PickupCompleteLocation)-1) As PickupCompleteLatitude,
								Substring(B.PickupCompleteLocation,CharIndex(',', B.PickupCompleteLocation)+1, LEN(B.PickupArriveLocation)) As PickupCompleteLongitude,
								B.DeliveryArrive As DeliveryArriveDateTime,
								Substring(B.DeliveryArriveLocation,1,CharIndex(',', B.DeliveryArriveLocation)-1) As DeliveryArriveLatitude,
								Substring(B.DeliveryArriveLocation,CharIndex(',', B.DeliveryArriveLocation)+1, LEN(B.DeliveryArriveLocation)) As DeliveryArriveLongitude,
								B.DeliveryComplete As DeliveryCompleteDateTime,
								Substring(B.DeliveryCompleteLocation,1,CharIndex(',', B.DeliveryCompleteLocation)-1) As DeliveryCompleteLatitude,
								Substring(B.DeliveryCompleteLocation,CharIndex(',', B.DeliveryCompleteLocation)+1, LEN(B.DeliveryCompleteLocation)) As DeliveryCompleteLongitude,
								B.ComoJobId
							FROM xCabBooking B LEFT JOIN xCabClientReferences R ON B.BookingId = R.PrimaryJobId 
							LEFT OUTER JOIN JobFutile F ON F.ConsignmentNumber = B.ConsignmentNumber
							WHERE 
								COALESCE(B.DateInserted,B.TPLUS_JobAllocationDate,B.DespatchDateTime) BETWEEN '{fromDate.ToString("yyyy - MM - dd")}' AND '{toDate.ToString("yyyy - MM - dd")}'
								AND B.AccountCode = '{accountCode}'
								AND B.StateId = {stateId} 
								AND R.Reference1 IN ('{uniqueReferenceValues}')
								AND B.BookingId = R.PrimaryJobId
								AND F.Id IS NULL 
								ORDER BY FORMAT(B.UploadDateTime,'yyyy-MM-dd HH:mm:ss') DESC, B.BookingId DESC";

                        var consolidatedJobsTrackingEvents = new List<XCabTrackingEvent>();
                        consolidatedJobsTrackingEvents =
                            (List<XCabTrackingEvent>)await connection.QueryAsync<XCabTrackingEvent>(sql);
                        xCabTrackingEvents.AddRange(consolidatedJobsTrackingEvents);
                        xCabTrackingEvents = xCabTrackingEvents
                            .Where(x => x.ConsignmentNumber != consolidatedConsignmentValue).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred in " + nameof(XCabBookingRepository) +
                    " while extracting tracking event details for tracking API request in " +
                    nameof(GetXCabTrackingEvents) + ", message: " +
                    e.Message, nameof(XCabBookingRepository));
            }

            return xCabTrackingEvents;
        }

        public async Task<int> InsertBooking(XCabBooking xCabBooking)
        {
            var insertedId = -1;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    if (xCabBooking != null)
                    {
                        xCabBooking = CleanupExtraCharatersHelper.GetCleansedXCabBooking(xCabBooking);
                    }

                    xCabBooking.AccountCode = xCabBooking.AccountCode.ToUpper();
                    xCabBooking.UploadDateTime = xCabBooking.UploadDateTime == DateTime.MinValue
                        ? null
                        : xCabBooking.UploadDateTime;
                    xCabBooking.AdvanceDateTime = xCabBooking.AdvanceDateTime == DateTime.MinValue
                        ? null
                        : xCabBooking.AdvanceDateTime;
                    int? jobNumber = null;
                    if (xCabBooking.Tplus_JobNumber > 0)
                    {
                        jobNumber = xCabBooking.Tplus_JobNumber;
                    }

                    await connection.OpenAsync();
                    const string sql = @"DECLARE @InsertedRows AS TABLE (BookingId int);
                                                     INSERT INTO XCabBooking(LoginId,StateId,AccountCode,ServiceCode,FromSuburb,FromPostcode,FromDetail1,
                                                    FromDetail2,FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,
                                                    Ref1,Ref2,ExtraPuInformation,ExtraDelInformation,PreAllocatedDriverNumber,TotalWeight,TotalVolume,DespatchDateTime,AdvanceDateTime,IsQueued,Caller,ConsignmentNumber,OkToUpload,ATL,TPLUS_JobNumber,
                                                    UploadedToTplus, UploadDateTime, Cancelled, UsingComo, ComoJobId,IsNtJob) OUTPUT Inserted.BookingId INTO @InsertedRows
                                                     VALUES (@LoginId,@StateId,@AccountCode,@ServiceCode,@FromSuburb,@FromPostcode,@FromDetail1,
                                                     @FromDetail2,@FromDetail3,@FromDetail4,@FromDetail5,@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,
                                                     @Ref1,@Ref2,@ExtraPuInformation,@ExtraDelInformation,@PreAllocatedDriverNumber,@TotalWeight,@TotalVolume,@DespatchDateTime,@AdvanceDateTime,@IsQueued,@Caller,@ConsignmentNumber,@OkToUpload,@ATL,@jobNumber,
                                                     @UploadedToTplus,@UploadDateTime,@Cancelled, @UsingComo, @ComoJobId, @IsNtJob);
                                                    SELECT BookingId FROM @InsertedRows";

                    insertedId = await connection.QuerySingleAsync<int>(sql, new
                    {
                        xCabBooking.LoginId,
                        xCabBooking.StateId,
                        xCabBooking.AccountCode,
                        xCabBooking.ServiceCode,
                        xCabBooking.FromSuburb,
                        xCabBooking.FromPostcode,
                        xCabBooking.FromDetail1,
                        xCabBooking.FromDetail2,
                        xCabBooking.FromDetail3,
                        xCabBooking.FromDetail4,
                        xCabBooking.FromDetail5,
                        xCabBooking.ToSuburb,
                        xCabBooking.ToPostcode,
                        xCabBooking.ToDetail1,
                        xCabBooking.ToDetail2,
                        xCabBooking.ToDetail3,
                        xCabBooking.ToDetail4,
                        xCabBooking.ToDetail5,
                        xCabBooking.Ref1,
                        xCabBooking.Ref2,
                        xCabBooking.ExtraPuInformation,
                        xCabBooking.ExtraDelInformation,
                        xCabBooking.PreAllocatedDriverNumber,
                        xCabBooking.TotalWeight,
                        xCabBooking.TotalVolume,
                        xCabBooking.DespatchDateTime,
                        xCabBooking.AdvanceDateTime,
                        xCabBooking.IsQueued,
                        xCabBooking.Caller,
                        xCabBooking.ConsignmentNumber,
                        xCabBooking.OkToUpload,
                        xCabBooking.ATL,
                        jobNumber,
                        xCabBooking.UploadedToTplus,
                        xCabBooking.UploadDateTime,
                        xCabBooking.Cancelled,
                        xCabBooking.ComoJobId,
                        xCabBooking.UsingComo,
                        xCabBooking.IsNtJob
                    });
                    if (insertedId > 0)
                    {
                        if (xCabBooking.lstItems != null && xCabBooking.lstItems.Count > 0)
                        {
                            try
                            {
                                foreach (var item in xCabBooking.lstItems)
                                {
                                    var insertItemQuery =
                                        "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity) VALUES (@BookingId, @Description, @Length, @Width, @Height, @Weight,@Cubic, @Barcode, @Quantity)";
                                    await connection.ExecuteAsync(insertItemQuery,
                                        new
                                        {
                                            BookingId = insertedId,
                                            item.Description,
                                            item.Length,
                                            item.Width,
                                            item.Height,
                                            item.Weight,
                                            item.Cubic,
                                            item.Barcode,
                                            item.Quantity
                                        });
                                }
                            }
                            catch (Exception e)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding barcode items to xCabItems table. Booking ID: " +
                                    insertedId + ", message: " +
                                    e.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabBooking.lstContactDetail != null && xCabBooking.lstContactDetail.Count > 0)
                        {
                            try
                            {
                                foreach (var detail in xCabBooking.lstContactDetail)
                                {
                                    var insertContactQuery =
                                        "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                                    await connection.ExecuteAsync(insertContactQuery, new
                                    {
                                        BookingId = insertedId,
                                        detail.AreaCode,
                                        detail.PhoneNumber
                                    });
                                }
                            }
                            catch (Exception ex)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding contact details to xCabContactDetails table.Booking ID: " +
                                    insertedId + ", message: " +
                                    ex.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabBooking.Remarks != null && xCabBooking.Remarks.Count > 0)
                        {
                            try
                            {
                                foreach (var remark in xCabBooking.Remarks)
                                {
                                    var insertRemarksQuery =
                                        "INSERT INTO [dbo].[xCabRemarks] (BookingId, Remarks) VALUES (@BookingId,@Remarks)";
                                    await connection.ExecuteAsync(insertRemarksQuery, new
                                    {
                                        BookingId = insertedId,
                                        Remarks = remark
                                    });
                                }
                            }
                            catch (Exception ex)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding remarks to xCabRemarks table. Booking ID: " +
                                    insertedId + ", message: " +
                                    ex.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabBooking.XCabTimeSlots != null)
                        {
                            try
                            {
                                const string insertTimeslots =
                                    @"INSERT INTO dbo.XCabTimeSlots(BookingId,StartDateTime,Duration)
                                                                VALUES (@BookingId,@StartDateTime,@Duration)";

                                await connection.ExecuteAsync(insertTimeslots, new
                                {
                                    BookingId = insertedId,
                                    StartDateTime = xCabBooking.XCabTimeSlots.StartDateTime,
                                    Duration = xCabBooking.XCabTimeSlots.Duration
                                });
                            }
                            catch (Exception e)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding timeslots to XCabTimeSlots table. Booking ID: " +
                                    insertedId + ", message: " +
                                    e.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabBooking.Notification != null)
                        {
                            try
                            {
                                var insertNotificationSql =
                                    @"INSERT INTO xCabNotifications(BookingId, SmsNumber, EmailAddress)
                        VALUES (@BookingId,@SmsNumber,@EmailAddress)";

                                await connection.ExecuteAsync(insertNotificationSql, new
                                {
                                    BookingId = insertedId,
                                    SmsNumber = xCabBooking.Notification.SMSNumber,
                                    EmailAddress = xCabBooking.Notification.EmailAddress
                                });
                            }
                            catch (Exception e)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding notifications to xCabNotifications table. Booking ID: " +
                                    insertedId + ", message: " +
                                    e.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabBooking.XCabClientReferences != null && xCabBooking.XCabClientReferences.Count() > 0)
                        {
                            try
                            {
                                foreach (var item in xCabBooking.XCabClientReferences)
                                {
                                    var insertSql = @$"INSERT INTO xCabClientReferences
			                                            (Reference1
                                                       ,Reference2
                                                       ,JobDate
                                                       ,PrimaryJobId)
                                                 VALUES
                                                       (@Reference1 
                                                       ,@Reference2
                                                       ,@JobDate 
                                                       ,@BookingId)";
                                    await connection.ExecuteAsync(insertSql,
                                        new
                                        {
                                            Reference1 = item.Reference1, Reference2 = item.Reference2,
                                            JobDate = item.JobDate, BookingId = insertedId
                                        });
                                }
                            }
                            catch (Exception e)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding consolidated references to xCabClientReferences table. Booking ID: " +
                                    insertedId + ", message: " +
                                    e.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabBooking.XCabBookingRoute != null)
                        {
                            try
                            {
                                var bookingRoutesInsertQuery = @"INSERT INTO [dbo].[XCabBookingRoutes]
                                                                ([BookingId]
                                                                ,[Route]
                                                                ,[DropSequence])
                                                            VALUES
                                                                (@BookingId,
                                                                 @Route,
                                                                 @DropSequence)";
                                await connection.ExecuteAsync(bookingRoutesInsertQuery,
                                    new
                                    {
                                        BookingId = insertedId,
                                        xCabBooking.XCabBookingRoute.Route,
                                        xCabBooking.XCabBookingRoute.DropSequence
                                    });
                            }
                            catch (Exception ex)
                            {
                                await Logger.Log(
                                    $"Exception Occurred in XCabBookingRepository V2: InsertBooking while adding booking routes to XCabBookingRoutes table for bookingID {insertedId}, message: " +
                                    ex.Message, "XCabBookingRepository");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        $"Exception Occurred in XCabBookingRepository V2 when inserting booking details for account {xCabBooking.AccountCode}, Ref1: {xCabBooking.Ref1}, JobNumber: {xCabBooking.Tplus_JobNumber} : InsertBooking, message: " +
                        e.Message, "XCabBookingRepository");
                }

                return insertedId;
            }
        }

        public async Task<bool> UpdateBookingRoute(XCabBooking booking, bool updateCaller = false)
        {
            var isUpdateSuccessful = false;
            if (booking.XCabBookingRoute == null)
                return isUpdateSuccessful;

            try
            {
                await using var connection =
                    new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString);
                await connection.OpenAsync();

                var id = await connection.QueryFirstOrDefaultAsync<int>(
                    "SELECT TOP 1 bookingid FROM XCabBookingRoutes WHERE bookingid = @BookingId",
                    new { BookingId = booking.BookingId });
                if (id != 0)
                {
                    await connection.ExecuteAsync(@"DELETE FROM XCabBookingRoutes WHERE bookingid = @BookingId",
                        new { BookingId = booking.BookingId });
                }

                var bookingRoutesInsertQuery = @"INSERT INTO [dbo].[XCabBookingRoutes]
                ([BookingId],[Route],[DropSequence])
                VALUES
                (@BookingId, @Route,@DropSequence)";

                var result = await connection.ExecuteAsync(bookingRoutesInsertQuery,
                    new
                    {
                        BookingId = booking.BookingId,
                        booking.XCabBookingRoute.Route,
                        booking.XCabBookingRoute.DropSequence
                    });
                if (result > 0)
                {
                    isUpdateSuccessful = true;
                }

                if (updateCaller)
                {
                    var bookingUpdateQuery =
                        @"UPDATE [dbo].[XCabBooking] set caller = @Caller WHERE bookingid = @BookingId";
                    await connection.ExecuteAsync(bookingUpdateQuery,
                        new
                        {
                            Caller = booking.Caller,
                            BookingId = booking.BookingId
                        });
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                    $"Exception Occurred in XCabBookingRepository V2 when updating booking route for account {booking.AccountCode} Id:{booking.BookingId}: UpdateRoute, message: " +
                    ex.Message, "XCabBookingRepository");
            }

            return isUpdateSuccessful;
        }

        public async Task<bool> UpdateBookingETA(int bookingId, string ETA)
        {
			var isUpdateSuccessful = false;
			if (bookingId > 0 && string.IsNullOrEmpty(ETA))
				return isUpdateSuccessful;
			try
			{
				if (!string.IsNullOrEmpty(ETA))
				{
					await using var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString);
					await connection.OpenAsync();
					var id = await connection.QueryFirstOrDefaultAsync<int>("SELECT TOP 1 bookingid FROM dbo.XCabETA WHERE bookingid = @BookingId",	new { bookingId });
					if (id != 0)
					{
						await connection.ExecuteAsync(@"DELETE FROM dbo.XCabETA WHERE bookingid = @BookingId", new { bookingId });
					}

					var bookingTimeSlotInsertQuery = @"INSERT INTO [dbo].[xCabETA] ([BookingId],[ETA]) VALUES (@BookingId, @ETA)";
					var result = await connection.ExecuteAsync(bookingTimeSlotInsertQuery, new { bookingId, ETA });
					if (result > 0)
					{
						isUpdateSuccessful = true;
					}
				}
			}
			catch (Exception ex)
			{
				await Logger.Log($"Exception Occurred in XCabBookingRepository V2 when updating booking ETA for booking Id:{bookingId}: message: {ex.Message}", nameof(XCabBookingRepository));
			}
			return isUpdateSuccessful;
		}
		private class BookingStatus
        {
            public int BookingId { get; set; }
            public bool Cancelled { get; set; }
            public bool Uploaded { get; set; }
        }

        // public async Task<int> UpdateBooking(XCabBooking xCabBooking)
        // {
        //     return -1;
        // }

        public enum UpdateStatus
        {
            FailedUnknown,
            FailedExists,
            FailedNoId,
            FailedNotFound,
            FailedHasBeenCancelled,
            FailedHasBeenBooked,
            Success
        }

        public async Task<UpdateStatus> UpdateBooking(XCabBooking xCabBooking)
        {
            await using var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString);
            await connection.OpenAsync();

            var searchRes = await this.GetBookingByReferenceLoginAndCode(xCabBooking.Ref1, xCabBooking.LoginId, xCabBooking.ServiceCode);

            xCabBooking.BookingId = searchRes?.BookingId ?? 0;

            if (xCabBooking.BookingId == 0) return UpdateStatus.FailedNotFound;

            BookingStatus checkExisting = await connection.QueryFirstOrDefaultAsync<BookingStatus>(
                @"select bookingid,	Cancelled, UploadedToTplus as Uploaded from xcabbooking where bookingid = @BookingId",
                new { BookingId = xCabBooking.BookingId });

            if (checkExisting == null) return UpdateStatus.FailedNotFound;
            if (checkExisting.Uploaded) return UpdateStatus.FailedHasBeenBooked;
            if (checkExisting.Cancelled) return UpdateStatus.FailedHasBeenCancelled;

            xCabBooking = CleanupExtraCharatersHelper.GetCleansedXCabBooking(xCabBooking);

            xCabBooking.AccountCode = xCabBooking.AccountCode.ToUpper();
            xCabBooking.UploadDateTime =
                xCabBooking.UploadDateTime == DateTime.MinValue ? null : xCabBooking.UploadDateTime;
            xCabBooking.AdvanceDateTime =
                xCabBooking.AdvanceDateTime == DateTime.MinValue ? null : xCabBooking.AdvanceDateTime;
            int? jobNumber = null;
            if (xCabBooking.Tplus_JobNumber > 0)
            {
                jobNumber = xCabBooking.Tplus_JobNumber;
            }

            if (!searchRes.Caller.IsNullOrWhiteSpace())
            {
                xCabBooking.Caller = searchRes.Caller;
            }

            try
            {
                const string sql = @"
                    UPDATE XCabBooking set 
                        FromSuburb = @FromSuburb,
                        FromPostcode = @FromPostcode,
                        FromDetail1 = @FromDetail1,
                        FromDetail2 = @FromDetail2,
                        FromDetail3 = @FromDetail3, 
                        FromDetail4 = @FromDetail4,
                        FromDetail5 = @FromDetail5,
                        ToSuburb = @ToSuburb,
                        ToPostcode = @ToPostcode,
                        ToDetail1 = @ToDetail1,
                        ToDetail2 = @ToDetail2,
                        ToDetail3 = @ToDetail3,
                        ToDetail4 = @ToDetail4,
                        ToDetail5 = @ToDetail5,

                        ExtraPuInformation = @ExtraPuInformation,
                        ExtraDelInformation = @ExtraDelInformation,
                        PreAllocatedDriverNumber = @PreAllocatedDriverNumber,
                        TotalWeight = @TotalWeight,
                        TotalVolume = @TotalVolume,
                        DespatchDateTime = @DespatchDateTime,
                        AdvanceDateTime = @AdvanceDateTime,
                        IsQueued = @IsQueued,
                        Caller = @Caller,
                        ConsignmentNumber = @ConsignmentNumber,
                        OkToUpload = @OkToUpload,
                        ATL = @ATL
                        where bookingid = @BookingId
                    ";

                await connection.ExecuteAsync(sql, new
                {
                    xCabBooking.BookingId,
                    xCabBooking.FromSuburb,
                    xCabBooking.FromPostcode,
                    xCabBooking.FromDetail1,
                    xCabBooking.FromDetail2,
                    xCabBooking.FromDetail3,
                    xCabBooking.FromDetail4,
                    xCabBooking.FromDetail5,
                    xCabBooking.ToSuburb,
                    xCabBooking.ToPostcode,
                    xCabBooking.ToDetail1,
                    xCabBooking.ToDetail2,
                    xCabBooking.ToDetail3,
                    xCabBooking.ToDetail4,
                    xCabBooking.ToDetail5,

                    xCabBooking.ExtraPuInformation,
                    xCabBooking.ExtraDelInformation,
                    xCabBooking.PreAllocatedDriverNumber,
                    xCabBooking.TotalWeight,
                    xCabBooking.TotalVolume,
                    xCabBooking.DespatchDateTime,
                    xCabBooking.AdvanceDateTime,
                    xCabBooking.IsQueued,
                    xCabBooking.Caller,
                    xCabBooking.ConsignmentNumber,
                    xCabBooking.OkToUpload,
                    xCabBooking.ATL
                });

                await connection.ExecuteAsync("DELETE FROM xcabitems WHERE bookingid = @BookingId",
                    new { BookingId = xCabBooking.BookingId });


                if (xCabBooking.lstItems != null && xCabBooking.lstItems.Count > 0)
                {
                    try
                    {
                        foreach (var item in xCabBooking.lstItems)
                        {
                            var insertItemQuery =
                                "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity) VALUES (@BookingId, @Description, @Length, @Width, @Height, @Weight,@Cubic, @Barcode, @Quantity)";
                            await connection.ExecuteAsync(insertItemQuery,
                                new
                                {
                                    BookingId = xCabBooking.BookingId,
                                    item.Description,
                                    item.Length,
                                    item.Width,
                                    item.Height,
                                    item.Weight,
                                    item.Cubic,
                                    item.Barcode,
                                    item.Quantity
                                });
                        }
                    }
                    catch (Exception e)
                    {
                        await Logger.Log(
                            "Exception Occurred in XCabBookingRepository while adding barcode items to xCabItems table. Booking ID: " +
                            xCabBooking.BookingId + ", message: " +
                            e.Message, "XCabBookingRepository");
                        return UpdateStatus.FailedUnknown;
                    }
                }


				try
				{
					var insertRemarksQuery =
						"INSERT INTO [dbo].[xCabRemarks] (BookingId, Remarks) VALUES (@BookingId,@Remarks)";
					await connection.ExecuteAsync(insertRemarksQuery, new
					{
						BookingId = xCabBooking.BookingId,
						Remarks = "Booking recieved UPDATE from file."
					});
				}
				catch (Exception ex)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabBookingRepository while adding remarks to xCabRemarks table. Booking ID: " +
                        xCabBooking.BookingId + ", message: " +
                        ex.Message, "XCabBookingRepository");
                    return UpdateStatus.FailedUnknown;
                }

                if (xCabBooking.XCabTimeSlots != null && xCabBooking.XCabTimeSlots.StartDateTime > DateTime.MinValue && xCabBooking.XCabTimeSlots.Duration > 0)
                {
                    try
                    {
                        await connection.ExecuteAsync("DELETE FROM XCabTimeSlots WHERE bookingid = @BookingId",
                                new { BookingId = xCabBooking.BookingId });

                        const string insertTimeslots =
                            @"INSERT INTO dbo.XCabTimeSlots(BookingId,StartDateTime,Duration)
                                                                VALUES (@BookingId,@StartDateTime,@Duration)";

                        await connection.ExecuteAsync(insertTimeslots, new
                        {
                            BookingId = xCabBooking.BookingId,
                            StartDateTime = xCabBooking.XCabTimeSlots.StartDateTime,
                            Duration = xCabBooking.XCabTimeSlots.Duration
                        });
                    }
                    catch (Exception e)
                    {
                        await Logger.Log(
                            "Exception Occurred in XCabBookingRepository while adding timeslots to XCabTimeSlots table. Booking ID: " +
                            xCabBooking.BookingId + ", message: " +
                            e.Message, "XCabBookingRepository");
                    }
                }

                if (xCabBooking.Notification != null)
                {
                    try
                    {
                        await connection.ExecuteAsync("DELETE FROM xCabNotifications WHERE bookingid = @BookingId",
                                new { BookingId = xCabBooking.BookingId });

                        var insertNotificationSql =
                            @"INSERT INTO xCabNotifications(BookingId, SmsNumber, EmailAddress)
                        VALUES (@BookingId,@SmsNumber,@EmailAddress)";

                        await connection.ExecuteAsync(insertNotificationSql, new
                        {
                            BookingId = xCabBooking.BookingId,
                            SmsNumber = xCabBooking.Notification.SMSNumber,
                            EmailAddress = xCabBooking.Notification.EmailAddress
                        });
                    }
                    catch (Exception e)
                    {
                        await Logger.Log(
                            "Exception Occurred in XCabBookingRepository while adding notifications to xCabNotifications table. Booking ID: " +
                            xCabBooking.BookingId + ", message: " +
                            e.Message, "XCabBookingRepository");
                    }
                }

            }
            catch (Exception ex)
            {
                await Logger.Log(
                    "Exception Occurred in XCabBookingRepository while updating. Booking ID: " +
                    xCabBooking.BookingId + ", message: " +
                    ex.Message, "XCabBookingRepository");
                return UpdateStatus.FailedUnknown;
            }

            return UpdateStatus.Success;
        }

        public async Task<int> InsertBookingForConsolidation(XCabAsnBooking xCabAsnBooking)
        {
            var insertedId = -1;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    if (xCabAsnBooking != null)
                    {
                        xCabAsnBooking = CleanupExtraCharatersHelper.GetCleansedXCabAsnBooking(xCabAsnBooking);
                    }

                    xCabAsnBooking.AccountCode = xCabAsnBooking.AccountCode.ToUpper();
                    xCabAsnBooking.UploadDateTime = xCabAsnBooking.UploadDateTime == DateTime.MinValue
                        ? null
                        : xCabAsnBooking.UploadDateTime;
                    xCabAsnBooking.AdvanceDateTime = xCabAsnBooking.AdvanceDateTime == DateTime.MinValue
                        ? null
                        : xCabAsnBooking.AdvanceDateTime;
                    int? jobNumber = null;
                    if (xCabAsnBooking.Tplus_JobNumber > 0)
                    {
                        jobNumber = xCabAsnBooking.Tplus_JobNumber;
                    }

                    await connection.OpenAsync();
                    const string sql = @"DECLARE @InsertedRows AS TABLE (BookingId int);
                                                     INSERT INTO XCabBooking(LoginId,StateId,AccountCode,ServiceCode,FromSuburb,FromPostcode,FromDetail1,
                                                    FromDetail2,FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,
                                                    Ref1,Ref2,ExtraPuInformation,ExtraDelInformation,PreAllocatedDriverNumber,TotalWeight,TotalVolume,DespatchDateTime,AdvanceDateTime,IsQueued,Caller,ConsignmentNumber,OkToUpload,ATL,TPLUS_JobNumber,
                                                    UploadedToTplus, UploadDateTime, Cancelled, UsingComo, ComoJobId,IsNtJob) OUTPUT Inserted.BookingId INTO @InsertedRows
                                                     VALUES (@LoginId,@StateId,@AccountCode,@ServiceCode,@FromSuburb,@FromPostcode,@FromDetail1,
                                                     @FromDetail2,@FromDetail3,@FromDetail4,@FromDetail5,@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,
                                                     @Ref1,@Ref2,@ExtraPuInformation,@ExtraDelInformation,@PreAllocatedDriverNumber,@TotalWeight,@TotalVolume,@DespatchDateTime,@AdvanceDateTime,@IsQueued,@Caller,@ConsignmentNumber,@OkToUpload,@ATL,@jobNumber,
                                                     @UploadedToTplus,@UploadDateTime,@Cancelled, @UsingComo, @ComoJobId, @IsNtJob);
                                                    UPDATE XCabBooking SET ConsignmentNumber = CONCAT('X',(SELECT BookingId FROM @InsertedRows)) WHERE BookingId = (SELECT BookingId FROM @InsertedRows);
                                                    SELECT BookingId FROM @InsertedRows;";

                    insertedId = await connection.QuerySingleAsync<int>(sql, new
                    {
                        xCabAsnBooking.LoginId,
                        xCabAsnBooking.StateId,
                        xCabAsnBooking.AccountCode,
                        xCabAsnBooking.ServiceCode,
                        xCabAsnBooking.FromSuburb,
                        xCabAsnBooking.FromPostcode,
                        xCabAsnBooking.FromDetail1,
                        xCabAsnBooking.FromDetail2,
                        xCabAsnBooking.FromDetail3,
                        xCabAsnBooking.FromDetail4,
                        xCabAsnBooking.FromDetail5,
                        xCabAsnBooking.ToSuburb,
                        xCabAsnBooking.ToPostcode,
                        xCabAsnBooking.ToDetail1,
                        xCabAsnBooking.ToDetail2,
                        xCabAsnBooking.ToDetail3,
                        xCabAsnBooking.ToDetail4,
                        xCabAsnBooking.ToDetail5,
                        xCabAsnBooking.Ref1,
                        xCabAsnBooking.Ref2,
                        xCabAsnBooking.ExtraPuInformation,
                        xCabAsnBooking.ExtraDelInformation,
                        xCabAsnBooking.PreAllocatedDriverNumber,
                        xCabAsnBooking.TotalWeight,
                        xCabAsnBooking.TotalVolume,
                        xCabAsnBooking.DespatchDateTime,
                        xCabAsnBooking.AdvanceDateTime,
                        xCabAsnBooking.IsQueued,
                        xCabAsnBooking.Caller,
                        xCabAsnBooking.ConsignmentNumber,
                        xCabAsnBooking.OkToUpload,
                        xCabAsnBooking.ATL,
                        jobNumber,
                        xCabAsnBooking.UploadedToTplus,
                        xCabAsnBooking.UploadDateTime,
                        xCabAsnBooking.Cancelled,
                        xCabAsnBooking.ComoJobId,
                        xCabAsnBooking.UsingComo,
                        xCabAsnBooking.IsNtJob
                    });
                    if (insertedId > 0 && xCabAsnBooking.LstConsolidatedReferences != null &&
                        xCabAsnBooking.LstConsolidatedReferences.Any())
                    {
                        try
                        {
                            var insertItemQuery =
                                "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity) (SELECT @BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity FROM xCabItems WHERE BookingId IN @OldBookingIds)";
                            await connection.ExecuteAsync(insertItemQuery,
                                new
                                {
                                    BookingId = insertedId,
                                    OldBookingIds = xCabAsnBooking.LstConsolidatedReferences.Select(x => x.PrimaryJobId)
                                        .ToList()
                                });
                        }
                        catch (Exception e)
                        {
                            await Logger.Log(
                                "Exception Occurred in XCabBookingRepository while adding barcode items to xCabItems table. Booking ID: " +
                                insertedId + ", message: " +
                                e.Message, "XCabBookingRepository");
                        }

                        try
                        {
                            var insertRemarksQuery =
                                "INSERT INTO xCabRemarks (BookingId, Remarks) (SELECT @BookingId,Remarks FROM xCabRemarks WHERE BookingId IN @OldBookingIds)";
                            await connection.ExecuteAsync(insertRemarksQuery, new
                            {
                                BookingId = insertedId,
                                OldBookingIds = xCabAsnBooking.LstConsolidatedReferences.Select(x => x.PrimaryJobId)
                                    .ToList()
                            });
                        }
                        catch (Exception ex)
                        {
                            await Logger.Log(
                                "Exception Occurred in XCabBookingRepository while adding Remarks to xCabContactDetails table.Booking ID: " +
                                insertedId + ", message: " +
                                ex.Message, "XCabBookingRepository");
                        }

                        if (xCabAsnBooking.ContactDetailsCount > 0)
                        {
                            try
                            {
                                var insertContactQuery =
                                    "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) (SELECT @BookingId,AreaCode, PhoneNumber FROM xCabContactDetails WHERE BookingId = @OldBookingIds)";
                                await connection.ExecuteAsync(insertContactQuery, new
                                {
                                    BookingId = insertedId,
                                    OldBookingIds = xCabAsnBooking.LstConsolidatedReferences.Select(x => x.PrimaryJobId)
                                        .ToList().FirstOrDefault()
                                });
                            }
                            catch (Exception ex)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding contact details to xCabContactDetails table.Booking ID: " +
                                    insertedId + ", message: " +
                                    ex.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabAsnBooking.NotificationCount > 0)
                        {
                            try
                            {
                                var insertNotificationQuery =
                                    @"INSERT INTO xCabNotifications(BookingId, SmsNumber, EmailAddress) (SELECT @BookingId,SmsNumber, EmailAddress FROM xCabNotifications WHERE BookingId = @OldBookingIds)";

                                await connection.ExecuteAsync(insertNotificationQuery, new
                                {
                                    BookingId = insertedId,
                                    OldBookingIds = xCabAsnBooking.LstConsolidatedReferences.Select(x => x.PrimaryJobId)
                                        .ToList().FirstOrDefault()
                                });
                            }
                            catch (Exception e)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding notifications to xCabNotifications table. Booking ID: " +
                                    insertedId + ", message: " +
                                    e.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabAsnBooking.XCabClientReferences != null &&
                            xCabAsnBooking.XCabClientReferences.Count() > 0)
                        {
                            try
                            {
                                foreach (var item in xCabAsnBooking.XCabClientReferences)
                                {
                                    var insertClientReferenceQuery = @$"INSERT INTO xCabClientReferences
			                                            (Reference1
                                                       ,Reference2
                                                       ,JobDate
                                                       ,PrimaryJobId)
                                                 VALUES
                                                       (@Reference1 
                                                       ,@Reference2
                                                       ,@JobDate 
                                                       ,@BookingId)";
                                    await connection.ExecuteAsync(insertClientReferenceQuery,
                                        new
                                        {
                                            Reference1 = item.Reference1,
                                            Reference2 = item.Reference2,
                                            JobDate = DateTime.Now,
                                            BookingId = insertedId
                                        });
                                }
                            }
                            catch (Exception e)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding consolidated references to xCabClientReferences table. Booking ID: " +
                                    insertedId + ", message: " +
                                    e.Message, "XCabBookingRepository");
                            }
                        }

                        if (xCabAsnBooking.LstConsolidatedReferences != null &&
                            xCabAsnBooking.LstConsolidatedReferences.Count() > 0)
                        {
                            try
                            {
                                foreach (var item in xCabAsnBooking.LstConsolidatedReferences)
                                {
                                    var insertConReferenceQuery = @$"INSERT INTO [dbo].[xCabConsolidatedReferences]
			                                            (Reference1
                                                          ,Reference2
                                                          ,ConsolidateJobId
                                                          ,PrimaryJobId)
                                                 VALUES
                                                       (@Reference1 
                                                       ,@Reference2
                                                       ,@ConsolidateJobId
                                                       ,@PrimaryJobId)";
                                    await connection.ExecuteAsync(insertConReferenceQuery,
                                        new
                                        {
                                            Reference1 = item.Reference1,
                                            Reference2 = item.Reference2,
                                            ConsolidateJobId = insertedId,
                                            PrimaryJobId = item.PrimaryJobId
                                        });
                                }

                                var updateXcabbookingQuery =
                                    @"UPDATE [dbo].[xCabBooking] SET ConsignmentNumber = 'CONSOLIDATED', Cancelled = 1 
                                                                     WHERE BookingId IN @BookingId";
                                await connection.ExecuteAsync(updateXcabbookingQuery,
                                    new
                                    {
                                        BookingId = xCabAsnBooking.LstConsolidatedReferences.Select(x => x.PrimaryJobId)
                                            .ToList()
                                    });
                            }
                            catch (Exception e)
                            {
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository while adding consolidated references to xCabClientReferences table. Booking ID: " +
                                    insertedId + ", message: " +
                                    e.Message, "XCabBookingRepository");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        $"Exception Occurred in XCabBookingRepository V2 when inserting booking details for account {xCabAsnBooking.AccountCode}, Ref1: {xCabAsnBooking.Ref1}, JobNumber: {xCabAsnBooking.Tplus_JobNumber} : InsertBooking, message: " +
                        e.Message, "XCabBookingRepository");
                }

                return insertedId;
            }
        }

        public async Task<bool> UpdateXCabBookingWithBookingDetailsCreatedInTms(XCabBooking xCabBooking)
        {
            bool result = true;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sqlUpdate = "";
                    var comoJobId = xCabBooking.ComoJobId > 0 ? xCabBooking.ComoJobId : 0;
                    int? jobNumber = null;
                    if (xCabBooking.Tplus_JobNumber > 0)
                    {
                        jobNumber = xCabBooking.Tplus_JobNumber;
                    }

                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("BookingId", xCabBooking.BookingId);
                    dbArgs.Add("UploadedToTPlus", xCabBooking.UploadedToTplus);
                    dbArgs.Add("UploadDateTime", xCabBooking.UploadDateTime);
                    dbArgs.Add("OkToUpload", xCabBooking.OkToUpload);
                    dbArgs.Add("Tplus_JobNumber", jobNumber);
                    dbArgs.Add("ComoJobId", comoJobId);
                    dbArgs.Add("UsingComo", xCabBooking.UsingComo);
                    dbArgs.Add("ConsignmentNumber", xCabBooking.ConsignmentNumber);
                    dbArgs.Add("Cancelled", xCabBooking.Cancelled);

                    sqlUpdate = @"UPDATE xCabBooking 
                                    SET UploadedToTplus = @UploadedToTPlus, 
                                        UploadDateTime = @UploadDateTime,
                                        OkToUpload = @OkToUpload,
                                        LastModified=GETDATE(), 
                                        Tplus_JobNumber=@Tplus_JobNumber, 
                                        ComoJobId = @ComoJobId,
                                        UsingComo = @UsingComo,
                                        ConsignmentNumber = @ConsignmentNumber,
                                        Cancelled = @Cancelled
                                    WHERE 
                                    BookingId = @BookingId";
                    await connection.ExecuteAsync(sqlUpdate, dbArgs);
                }
                catch (Exception e)
                {
                    result = false;
                    await Logger.Log(
                        $"Exception Occurred in XCabBookingRepository V2 when updating booking details for account {xCabBooking.AccountCode}, Ref1: {xCabBooking.Ref1}, JobNumber: {xCabBooking.Tplus_JobNumber} : UpdateBooking, message: " +
                        e.Message, "XCabBookingRepository");
                }
            }

            return result;
        }

        public async Task<bool> UpdateXCabTrackingEvents(ICollection<XCabTrackingEvent> xCabTrackingEvents,
            ICollection<TrackingJob> trackingJobs)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    foreach (var xCabTrackingEvent in xCabTrackingEvents)
                    {
                        //find the matching in the second list
                        var trackingJob = trackingJobs.Where(x => x.BookingId == xCabTrackingEvent.BookingId)
                            .FirstOrDefault();
                        //find if the two lists have same tracking events
                        var newTrackingEvent = xCabTrackingEvent.PickupArriveDateTime.ToString()
                                               + xCabTrackingEvent.PickupArriveLatitude + ","
                                               + xCabTrackingEvent.PickupArriveLongitude
                                               + xCabTrackingEvent.PickupCompleteDateTime.ToString()
                                               + xCabTrackingEvent.PickupCompleteLatitude + ","
                                               + xCabTrackingEvent.PickupCompleteLongitude
                                               + xCabTrackingEvent.DeliveryArriveDateTime.ToString()
                                               + xCabTrackingEvent.DeliveryArriveLatitude + ","
                                               + xCabTrackingEvent.DeliveryArriveLongitude
                                               + xCabTrackingEvent.DeliveryCompleteDateTime.ToString()
                                               + xCabTrackingEvent.DeliveryCompleteLatitude + ","
                                               + xCabTrackingEvent.DeliveryCompleteLongitude;
                        var stringBuilder = new StringBuilder();
                        stringBuilder.Append(trackingJob.PickupArrive != null
                            ? trackingJob.PickupArrive.ToString()
                            : DateTime.MinValue.ToString());
                        stringBuilder.Append(
                            (trackingJob.PickupArriveLocation == null || trackingJob.PickupArriveLocation.Length == 0)
                                ? ","
                                : trackingJob.PickupArriveLocation);
                        stringBuilder.Append(trackingJob.PickupComplete != null
                            ? trackingJob.PickupComplete.ToString()
                            : DateTime.MinValue.ToString());
                        stringBuilder.Append(
                            (trackingJob.PickupCompleteLocation == null ||
                             trackingJob.PickupCompleteLocation.Length == 0)
                                ? ","
                                : trackingJob.PickupCompleteLocation);
                        stringBuilder.Append(trackingJob.DeliveryArrive != null
                            ? trackingJob.DeliveryArrive.ToString()
                            : DateTime.MinValue.ToString());
                        stringBuilder.Append(
                            (trackingJob.DeliveryArriveLocation == null ||
                             trackingJob.DeliveryArriveLocation.Length == 0)
                                ? ","
                                : trackingJob.DeliveryArriveLocation);
                        stringBuilder.Append(trackingJob.DeliveryComplete != null
                            ? trackingJob.DeliveryComplete.ToString()
                            : DateTime.MinValue.ToString());
                        stringBuilder.Append(
                            (trackingJob.DeliveryCompleteLocation == null ||
                             trackingJob.DeliveryCompleteLocation.Length == 0)
                                ? ","
                                : trackingJob.DeliveryCompleteLocation);


                        var currentTrackingEvent = stringBuilder.ToString();
                        var hashSame =
                            Core.Helpers.HashUtils.HashAndCheckIfSame(newTrackingEvent, currentTrackingEvent);
                        if (!hashSame)
                        {
                            var dbArgs = new DynamicParameters();
                            dbArgs.Add("BookingId", xCabTrackingEvent.BookingId);
                            dbArgs.Add("PickupArrive",
                                xCabTrackingEvent.PickupArriveDateTime != DateTime.MinValue
                                    ? xCabTrackingEvent.PickupArriveDateTime
                                    : null);
                            dbArgs.Add("PickupArriveLocation",
                                (!string.IsNullOrEmpty(xCabTrackingEvent.PickupArriveLatitude)
                                    ? xCabTrackingEvent.PickupArriveLatitude
                                    : "0.0") + "," + (!string.IsNullOrEmpty(xCabTrackingEvent.PickupArriveLongitude)
                                    ? xCabTrackingEvent.PickupArriveLongitude
                                    : "0.0"));
                            dbArgs.Add("PickupComplete",
                                xCabTrackingEvent.PickupCompleteDateTime != DateTime.MinValue
                                    ? xCabTrackingEvent.PickupCompleteDateTime
                                    : null);
                            dbArgs.Add("PickupCompleteLocation",
                                (!string.IsNullOrEmpty(xCabTrackingEvent.PickupCompleteLatitude)
                                    ? xCabTrackingEvent.PickupCompleteLatitude
                                    : "0.0") + "," + (!string.IsNullOrEmpty(xCabTrackingEvent.PickupCompleteLongitude)
                                    ? xCabTrackingEvent.PickupCompleteLongitude
                                    : "0.0"));
                            dbArgs.Add("DeliveryArrive",
                                xCabTrackingEvent.DeliveryArriveDateTime != DateTime.MinValue
                                    ? xCabTrackingEvent.DeliveryArriveDateTime
                                    : null);
                            dbArgs.Add("DeliveryArriveLocation",
                                (!string.IsNullOrEmpty(xCabTrackingEvent.DeliveryArriveLatitude)
                                    ? xCabTrackingEvent.DeliveryArriveLatitude
                                    : "0.0") + "," + (!string.IsNullOrEmpty(xCabTrackingEvent.DeliveryArriveLongitude)
                                    ? xCabTrackingEvent.DeliveryArriveLongitude
                                    : "0.0"));
                            dbArgs.Add("DeliveryComplete",
                                xCabTrackingEvent.DeliveryCompleteDateTime != DateTime.MinValue
                                    ? xCabTrackingEvent.DeliveryCompleteDateTime
                                    : null);
                            dbArgs.Add("DeliveryCompleteLocation",
                                (!string.IsNullOrEmpty(xCabTrackingEvent.DeliveryCompleteLatitude)
                                    ? xCabTrackingEvent.DeliveryCompleteLatitude
                                    : "0.0") + "," + (!string.IsNullOrEmpty(xCabTrackingEvent.DeliveryCompleteLongitude)
                                    ? xCabTrackingEvent.DeliveryCompleteLongitude
                                    : "0.0"));

                            string sqlUpdate = "";
                            if (xCabTrackingEvent.DeliveryCompleteDateTime != DateTime.MinValue)
                            {
                                sqlUpdate = "UPDATE xCabBooking " +
                                            " SET PickupArrive = @PickupArrive, " +
                                            " PickupArriveLocation = @PickupArriveLocation, " +
                                            " PickupComplete = @PickupComplete, " +
                                            " PickupCompleteLocation = @PickupCompleteLocation, " +
                                            " DeliveryArrive = @DeliveryArrive, " +
                                            " DeliveryArriveLocation = @DeliveryArriveLocation, " +
                                            " DeliveryComplete = @DeliveryComplete, " +
                                            " DeliveryCompleteLocation = @DeliveryCompleteLocation," +
                                            " Completed = 1, " +
                                            " LastModified=GETDATE() " +
                                            " WHERE " +
                                            " BookingId = @BookingId";
                            }
                            else
                            {
                                sqlUpdate = "UPDATE xCabBooking " +
                                            " SET PickupArrive = @PickupArrive, " +
                                            " PickupArriveLocation = @PickupArriveLocation, " +
                                            " PickupComplete = @PickupComplete, " +
                                            " PickupCompleteLocation = @PickupCompleteLocation, " +
                                            " DeliveryArrive = @DeliveryArrive, " +
                                            " DeliveryArriveLocation = @DeliveryArriveLocation, " +
                                            " DeliveryComplete = @DeliveryComplete, " +
                                            " DeliveryCompleteLocation = @DeliveryCompleteLocation, " +
                                            " LastModified=GETDATE() " +
                                            " WHERE " +
                                            " BookingId = @BookingId";
                            }


                            var result = await connection.ExecuteAsync(sqlUpdate, dbArgs);
                        }
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> UpdateXCabTrackingEventsForCancellationAndDriverAllocation(
            ICollection<XCabTrackingEvent> xCabTrackingEvents)
        {
            bool result = true;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    foreach (var xCabTrackingEvent in xCabTrackingEvents)
                    {
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("BookingId", xCabTrackingEvent.BookingId);
                        dbArgs.Add("Cancelled", xCabTrackingEvent.Cancelled);
                        string sqlUpdate = "";

                        if (xCabTrackingEvent.AllocationDateTime != DateTime.MinValue &&
                            xCabTrackingEvent.DriverNumber > 0)
                        {
                            dbArgs.Add("DriverNumber", xCabTrackingEvent.DriverNumber);
                            dbArgs.Add("TPLUS_JobAllocationDate", xCabTrackingEvent.AllocationDateTime);
                            sqlUpdate = "UPDATE xCabBooking " +
                                        " SET DriverNumber = @DriverNumber, " +
                                        " TPLUS_JobAllocationDate = @TPLUS_JobAllocationDate, " +
                                        " LastModified=GETDATE() " +
                                        " WHERE " +
                                        " BookingId = @BookingId";
                        }
                        else
                        {
                            sqlUpdate = "UPDATE xCabBooking " +
                                        " SET Cancelled = @Cancelled, " +
                                        " LastModified=GETDATE() " +
                                        " WHERE " +
                                        " BookingId = @BookingId";
                        }

                        await connection.ExecuteAsync(sqlUpdate, dbArgs);
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }

        public async Task<XCabTrackingEvent> GetUltimateLegDetailsOfFutileJob(string ultimateLegConsignmentNumber)
        {
            var xCabTrackingEvent = new XCabTrackingEvent();
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    string sql = $@"SELECT B.TPLUS_JobNumber,
										COALESCE(B.DespatchDateTime, B.UploadDateTime, B.DateInserted)  AS JobBookingDateTime,
										B.TPLUS_JobAllocationDate AS AllocationDateTime,
										B.AccountCode,
										B.StateId,
										COALESCE(B.Ref1, B.ConsignmentNumber) As UniqueReferenceValue,
										COALESCE(B.ConsignmentNumber, ('X' + CAST(B.BookingID AS varchar))) AS  ConsignmentNumber,
										B.Completed,
										B.DriverNumber,
										B.Cancelled,	
										B.OkToUpload,
										B.UploadedToTplus,
										B.UploadDateTime,
										B.PickupArrive As PickupArriveDateTime, 
										Substring(B.PickupArriveLocation,1,CharIndex(',', B.PickupArriveLocation)-1) As PickupArriveLatitude,
										Substring(B.PickupArriveLocation,CharIndex(',', B.PickupArriveLocation)+1, LEN(B.PickupArriveLocation)) As PickupArriveLongitude,
										B.PickupComplete As PickupCompleteDateTime,
										Substring(B.PickupCompleteLocation,1,CharIndex(',', B.PickupCompleteLocation)-1) As PickupCompleteLatitude,
										Substring(B.PickupCompleteLocation,CharIndex(',', B.PickupCompleteLocation)+1, LEN(B.PickupArriveLocation)) As PickupCompleteLongitude,
										B.DeliveryArrive As DeliveryArriveDateTime,
										Substring(B.DeliveryArriveLocation,1,CharIndex(',', B.DeliveryArriveLocation)-1) As DeliveryArriveLatitude,
										Substring(B.DeliveryArriveLocation,CharIndex(',', B.DeliveryArriveLocation)+1, LEN(B.DeliveryArriveLocation)) As DeliveryArriveLongitude,                  
										B.DeliveryComplete As DeliveryCompleteDateTime,
										Substring(B.DeliveryCompleteLocation,1,CharIndex(',', B.DeliveryCompleteLocation)-1) As DeliveryCompleteLatitude,
										Substring(B.DeliveryCompleteLocation,CharIndex(',', B.DeliveryCompleteLocation)+1, LEN(B.DeliveryCompleteLocation)) As DeliveryCompleteLongitude,
										B.ComoJobId
                                    FROM xCabBooking B
                                    WHERE
                                    B.ConsignmentNumber = '{ultimateLegConsignmentNumber}'";
                    xCabTrackingEvent =
                        (XCabTrackingEvent)(connection.QueryAsync<XCabTrackingEvent>(sql).Result.FirstOrDefault());
                }
            }
            catch (Exception e)
            {
				await Logger.Log(
					 $"Exception Occurred in XCabBookingRepository: GetUltimateLegDetailsOfFutileJob when extracting details of futiled job with ConsignmentNumber {ultimateLegConsignmentNumber}. Message: " +
	                 e.Message, nameof(XCabBookingRepository));
			}

            return xCabTrackingEvent;
        }

        // Does not actually return the full booking model
        public async Task<XCabBooking> GetBookingByReferenceLoginAndCode(string reference, int login, string serviceCode)
        {
            XCabBooking booking = null;
            var selectSql = $@"
                            SELECT 
	                            b.BookingId , 
	                            b.TPLUS_JobNumber, 
	                            b.TPLUS_JobAllocationDate,
	                            b.AccountCode,
                                b.StateId,
                                b.Cancelled,
                                b.UploadedToTplus
                            FROM 
	                            xCabBooking b
                            WHERE 
	                            b.LoginId = {Convert.ToString(login)} 
	                            AND (b.Ref1 = '{reference}' OR b.Ref2 = '{reference}')	
                                AND b.ServiceCode = '{serviceCode}'							
								AND b.Cancelled = 0
                                AND b.UploadedToTplus = 0";
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    booking = await connection.QueryFirstOrDefaultAsync<XCabBooking>(selectSql);
                }
                catch (Exception ex)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabBookingRepository: GetBookingByReferenceLoginAndCode, message: " +
                        ex.Message, "XCabBookingRepository");
                }
            }

            return booking;
        }
        
        public async Task<XCabBooking> GetBookingByReferenceAndLogin(string reference, int login)
        {
            XCabBooking booking = null;
            var selectSql = $@"
                            SELECT 
	                            b.BookingId , 
	                            b.TPLUS_JobNumber, 
	                            b.TPLUS_JobAllocationDate,
	                            b.AccountCode,
                                b.StateId,
                                b.Cancelled,
                                b.UploadedToTplus
                            FROM 
	                            xCabBooking b
                            WHERE 
	                            b.LoginId = {Convert.ToString(login)} 
	                            AND (b.Ref1 = '{reference}' OR b.Ref2 = '{reference}')	
								AND b.Cancelled = 0
                                AND b.UploadedToTplus = 0";
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    booking = await connection.QueryFirstOrDefaultAsync<XCabBooking>(selectSql);
                }
                catch (Exception ex)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabBookingRepository: GetBookingByReferenceAndLogin, message: " +
                        ex.Message, "XCabBookingRepository");
                }
            }

            return booking;
        }

        public async Task<List<XCabBooking>> GetBookingsByBookingIdAndLogin(string bookingId, int login)
        {            
            List<XCabBooking> booking = null;
            
            var selectSql = $@"
                            SELECT 
	                            b.BookingId , 
	                            b.TPLUS_JobNumber, 
	                            b.TPLUS_JobAllocationDate,
	                            b.AccountCode,
                                b.StateId,
                                b.Cancelled,
                                b.UploadedToTplus,
                                b.Ref1
                            FROM 
	                            xCabBooking b
                            WHERE 
	                            b.LoginId = {Convert.ToString(login)} 
	                            AND bookingid = {bookingId}							
								AND b.Cancelled = 0";
            await using var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString);

            try
            {
                await connection.OpenAsync();
                var res = await connection.QueryAsync<XCabBooking>(selectSql);
                booking = res.ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: GetBookingByReferenceAndLogin, message: " +
                    ex.Message, "XCabBookingRepository");
            }

            return booking;
        }

        public async Task<ICollection<Reference>> GetReferencesByPrimaryJob(int primaryJob)
        {
            ICollection<Reference> references = null;
            var selectSql = $@"
                            SELECT 
	                            Reference1, 
	                            Reference2 
                            FROM 
	                            xCabClientReferences 
                            WHERE 
	                            PrimaryJobId = {Convert.ToString(primaryJob)}
                            UNION
                            SELECT 
	                            ref1, 
								ref2 
                            FROM 
	                            xCabBooking
                            WHERE 
	                            BookingId = {Convert.ToString(primaryJob)}";
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    references = new List<Reference>();
                    var cmd = new SqlCommand(selectSql, connection)
                    {
                        CommandType = CommandType.Text
                    };
                    using (var cursor = cmd.ExecuteReader())
                    {
                        while (cursor.Read())
                        {
                            Reference reference = new Reference();
                            if (cursor["Reference1"] != null)
                            {
                                reference.Reference1 = cursor.GetString(cursor.GetOrdinal("Reference1"));
                            }

                            if (cursor["Reference2"] != null)
                            {
                                reference.Reference2 = cursor.GetString(cursor.GetOrdinal("Reference2"));
                            }

                            references.Add(reference);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(
                        "Exception Occurred in XCabBookingRepository: GetReferencesByPrimaryJob, message: " +
                        ex.Message, "XCabBookingRepository");
                }
            }

            return references;
        }

        public async Task<bool> UpdateTrackingEventsDetails(ICollection<TrackingJob> trackingJobs)
        {
            var insertedSuccessfully = true;
            try
            {
                if (trackingJobs.Count > 0)
                {
                    var lstSql = new List<string>();
                    foreach (var trackingJob in trackingJobs)
                    {
                        var trackingEvent = "";
                        switch (trackingJob.CurrentTrackingEvent)
                        {
                            case ETrackingEvent.PICKUP_ARRIVE:
                                trackingEvent = "PickupArrive";
                                break;
                            case ETrackingEvent.PICKUP_COMPLETE:
                                trackingEvent = "PickupComplete";
                                break;
                            case ETrackingEvent.DELIVERY_ARRIVE:
                                trackingEvent = "DeliveryArrive";
                                break;
                            case ETrackingEvent.DELIVERY_COMPLETE:
                                trackingEvent = "DeliveryComplete";
                                break;
                            default:
                                break;
                        }

                        if (trackingEvent != "DeliveryComplete")
                        {
                            lstSql.Add(new string("UPDATE XCabBooking SET " + trackingEvent + " = '" +
                                                  trackingJob.eventDateTime +
                                                  "', " + trackingEvent + "Location='" + "0.0,0.0" +
                                                  "', LastModified = GETDATE() " + "WHERE BookingId=" +
                                                  trackingJob.BookingId + ";"));
                        }
                        else
                        {
                            lstSql.Add(new string("UPDATE XCabBooking SET " + trackingEvent + " = '" +
                                                  trackingJob.eventDateTime +
                                                  "', " + trackingEvent + "Location='" + "0.0,0.0" +
                                                  "', LastModified = GETDATE(), Completed = 1 " + "WHERE BookingId=" +
                                                  trackingJob.BookingId + ";"));
                        }
                    }

                    if (lstSql.Count > 0)
                    {
                        using (var connection =
                               new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                        {
                            var sql = string.Join(" ", lstSql);
                            await connection.OpenAsync();
                            var result = await connection.ExecuteAsync(sql);
                            if (lstSql.Count != result)
                            {
                                insertedSuccessfully = false;
                                await Logger.Log(
                                    "Exception Occurred in XCabBookingRepository: UpdateTrackingEventsDetails, message: " +
                                    "Updated " + result + " records out of " + lstSql.Count, "XCabBookingRepository");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred in XCabBookingRepository: UpdateTrackingEventsDetails, message: " +
                    e.Message, "XCabBookingRepository");
                insertedSuccessfully = false;
            }

            return insertedSuccessfully;
        }

        public async Task<XCabTrackingEvent> GetTrackingStatusForOw(DateTime fromDate, DateTime toDate,
            string accountCode, int stateId, string reference1)
        {
            XCabTrackingEvent xCabTrackingEvent = new XCabTrackingEvent();
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "";
                    if (!string.IsNullOrEmpty(accountCode))
                        sql = $@"SELECT B.TPLUS_JobNumber AS JobNumber,
									B.DespatchDateTime AS JobDate, 
									B.DriverNumber,
									B.Completed,
									B.Cancelled,
									B.PickupArrive, 
									B.PickupArriveLocation,
									B.PickupComplete,
									B.PickupCompleteLocation,
									B.DeliveryArrive, 
									B.DeliveryArriveLocation,
									B.DeliveryCompleteLocation,
									B.DeliveryComplete
									FROM xCabBooking B
								WHERE
									B.DespatchDateTime BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}'
									AND B.AccountCode = '{accountCode}'
									AND LoginId = 80
									AND B.StateId = '{(object)stateId}' AND (B.Ref1 = '{reference1}' OR B.ConsignmentNumber = '{reference1}')";
                    XCabTrackingEvent bookingTrackingStatus = connection
                        .Query<XCabTrackingEvent>(sql, (object)null, (IDbTransaction)null, true, new int?(),
                            new CommandType?()).FirstOrDefault<XCabTrackingEvent>();
                    if (bookingTrackingStatus != null)
                    {
                        xCabTrackingEvent.Tplus_JobNumber = bookingTrackingStatus.Tplus_JobNumber;
                        xCabTrackingEvent.DriverNumber = bookingTrackingStatus.DriverNumber;
                        xCabTrackingEvent.Cancelled = bookingTrackingStatus.Cancelled;
                        xCabTrackingEvent.Completed = bookingTrackingStatus.Completed;
                        xCabTrackingEvent.DateInserted = bookingTrackingStatus.DateInserted;

                        if (!string.IsNullOrEmpty(bookingTrackingStatus.PickupArriveLatitude) &&
                            !string.IsNullOrEmpty(bookingTrackingStatus.PickupArriveLongitude))
                        {
                            xCabTrackingEvent.PickupArriveLatitude = bookingTrackingStatus.PickupArriveLatitude;
                            xCabTrackingEvent.PickupArriveLongitude = bookingTrackingStatus.PickupArriveLongitude;
                            xCabTrackingEvent.PickupArriveDateTime = bookingTrackingStatus.PickupArriveDateTime;
                        }

                        if (!string.IsNullOrEmpty(bookingTrackingStatus.PickupCompleteLatitude) &&
                            !string.IsNullOrEmpty(bookingTrackingStatus.PickupCompleteLongitude))
                        {
                            xCabTrackingEvent.PickupCompleteLatitude = bookingTrackingStatus.PickupCompleteLatitude;
                            xCabTrackingEvent.PickupCompleteLongitude = bookingTrackingStatus.PickupCompleteLongitude;
                            xCabTrackingEvent.PickupCompleteDateTime = bookingTrackingStatus.PickupCompleteDateTime;
                        }

                        if (!string.IsNullOrEmpty(bookingTrackingStatus.DeliveryArriveLatitude) &&
                            !string.IsNullOrEmpty(bookingTrackingStatus.DeliveryArriveLongitude))
                        {
                            xCabTrackingEvent.DeliveryArriveLatitude = bookingTrackingStatus.DeliveryArriveLatitude;
                            xCabTrackingEvent.DeliveryArriveLongitude = bookingTrackingStatus.DeliveryArriveLongitude;
                            xCabTrackingEvent.DeliveryArriveDateTime = bookingTrackingStatus.DeliveryArriveDateTime;
                        }

                        if (!string.IsNullOrEmpty(bookingTrackingStatus.DeliveryCompleteLatitude) &&
                            !string.IsNullOrEmpty(bookingTrackingStatus.DeliveryCompleteLongitude))
                        {
                            xCabTrackingEvent.DeliveryCompleteLatitude = bookingTrackingStatus.DeliveryCompleteLatitude;
                            xCabTrackingEvent.DeliveryCompleteLongitude =
                                bookingTrackingStatus.DeliveryCompleteLongitude;
                            xCabTrackingEvent.DeliveryCompleteDateTime = bookingTrackingStatus.DeliveryCompleteDateTime;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabBookingRepository: GetTrackingStatus, message: " + ex.Message,
                        "XCabBookingRepository");
                }
            }

            return xCabTrackingEvent;
        }

        public async Task<bool> AddItems(int loginId, EReferenceType referenceType, string referenceValue,
            ICollection<XCabItems> xCabItems)
        {
            bool updatedSuccessfully = true;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    try
                    {
                        await connection.OpenAsync();
                        var dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add(XCabDatabaseColumnHelper.GetReferenceColumnName(referenceType),
                            referenceValue);
                        dynamicParameters.Add("DateInserted", DateTime.Now.AddDays(-30));
                        dynamicParameters.Add("LoginId", loginId);
                        dynamicParameters.Add("Ref1", referenceValue);
                        var sql = "";
                        switch (referenceType)
                        {
                            case EReferenceType.Reference1:
                                sql = "SELECT Top(1) BookingId from XCabBooking WHERE LoginId = @LoginId   " +
                                      " AND Ref1=@Ref1 AND UploadedToTplus = 0 " +
                                      "AND Cancelled = 0 AND DateInserted>= @DateInserted ORDER BY BookingID Desc";
                                break;
                            case EReferenceType.Reference2:
                                sql = "SELECT Top(1) BookingId from XCabBooking WHERE LoginId = @LoginId  " +
                                      " AND Ref2=@Ref2 AND UploadedToTplus = 0 " +
                                      "AND Cancelled = 0 AND DateInserted>= @DateInserted ORDER BY BookingID Desc";
                                break;
                            case EReferenceType.ConsignmentNumber:
                                sql = "SELECT Top(1) BookingId from XCabBooking WHERE LoginId = @LoginId  " +
                                      " AND ConsignmentNumber = @ConsignmentNumber AND UploadedToTplus = 0 " +
                                      "AND Cancelled = 0 AND DateInserted>= @DateInserted ORDER BY BookingID Desc";
                                break;
                        }

                        var bookingId = await connection.QueryFirstOrDefaultAsync<string>(sql, dynamicParameters);
                        if (!string.IsNullOrEmpty(bookingId))
                        {
                            try
                            {
                                foreach (var xCabItem in xCabItems)
                                {
                                    try
                                    {
                                        var insertItemQuery =
                                            "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity,Status) VALUES (@BookingId, @Description, @Length, @Width, @Height, @Weight,@Cubic, @Barcode, @Quantity,@Status)";
                                        await connection.ExecuteAsync(insertItemQuery,
                                            new
                                            {
                                                BookingId = bookingId,
                                                Description = xCabItem.Description,
                                                Length = xCabItem.Length,
                                                Width = xCabItem.Width,
                                                Height = xCabItem.Height,
                                                Weight = xCabItem.Weight,
                                                Cubic = xCabItem.Cubic,
                                                Barcode = xCabItem.Barcode,
                                                Quantity = xCabItem.Qantity,
                                                Status = xCabItem.Status
                                            });
                                    }
                                    catch (Exception e)
                                    {
                                        await Logger.Log(
                                            "Exception Occurred in AddBarcodes while adding barcode items to xCabItems table, message: " +
                                            e.Message, nameof(XCabBookingRepository));
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                await Logger.Log(
                                    "Exception Occurred in AddBarcodes while adding barcode items to xCabItems table, message: " +
                                    e.Message, nameof(XCabBookingRepository));
                                updatedSuccessfully = false;
                            }
                        }
                        else
                        {
                            updatedSuccessfully = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        await Logger.Log(
                            "Exception Occurred in FindingBooking & AddBarcodes while adding barcode items to xCabItems table, message: " +
                            ex.Message, nameof(XCabBookingRepository));
                        updatedSuccessfully = false;
                    }
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred in AddBarcodes while adding barcode items to xCabItems table for loginId: " +
                    loginId + ", message: " +
                    e.Message, nameof(XCabBookingRepository));
                updatedSuccessfully = false;
            }

            return updatedSuccessfully;
        }

        public async Task<bool> AddItems(int loginId, string accountCode, int stateId, EReferenceType referenceType,
            string referenceValue, ICollection<XCabItems> xCabItems)
        {
            bool updatedSuccessfully = true;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(XCabDatabaseColumnHelper.GetReferenceColumnName(referenceType),
                        referenceValue);
                    dynamicParameters.Add("AccountCode", accountCode);
                    dynamicParameters.Add("StateId", stateId);
                    dynamicParameters.Add("DateInserted", DateTime.Now.AddDays(-30));
                    dynamicParameters.Add("LoginId", loginId);
                    var sql = "";
                    switch (referenceType)
                    {
                        case EReferenceType.Reference1:
                            sql =
                                "SELECT Top(1) BookingId from XCabBooking WHERE LoginId = @LoginId AND AccountCode = @AccountCode " +
                                "AND StateId = @StateId AND Ref1=@Ref1 AND UploadedToTplus = 0 " +
                                "AND Cancelled = 0 AND DateInserted>= @DateInserted ORDER BY BookingID Desc";
                            break;
                        case EReferenceType.Reference2:
                            sql =
                                "SELECT Top(1) BookingId from XCabBooking WHERE LoginId = @LoginId AND AccountCode = @AccountCode " +
                                "AND StateId = @StateId AND Ref2=@Ref2 AND UploadedToTplus = 0 " +
                                "AND Cancelled = 0 AND DateInserted>= @DateInserted ORDER BY BookingID Desc";
                            break;
                        case EReferenceType.ConsignmentNumber:
                            sql =
                                "SELECT Top(1) BookingId from XCabBooking WHERE LoginId = @LoginId AND AccountCode = @AccountCode " +
                                "AND StateId = @StateId AND ConsignmentNumber = @ConsignmentNumber AND UploadedToTplus = 0 " +
                                "AND Cancelled = 0 AND DateInserted>= @DateInserted ORDER BY BookingID Desc";
                            break;
                    }

                    var bookingId = await connection.QueryFirstOrDefaultAsync<string>(sql, dynamicParameters);
                    if (!string.IsNullOrEmpty(bookingId))
                    {
                        try
                        {
                            foreach (var xCabItem in xCabItems)
                            {
                                var insertItemQuery =
                                    "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity,Status) VALUES (@BookingId, @Description, @Length, @Width, @Height, @Weight,@Cubic, @Barcode, @Quantity,@Status)";
                                await connection.ExecuteAsync(insertItemQuery,
                                    new
                                    {
                                        BookingId = bookingId,
                                        Description = xCabItem.Description,
                                        Length = xCabItem.Length,
                                        Width = xCabItem.Width,
                                        Height = xCabItem.Height,
                                        Weight = xCabItem.Weight,
                                        Cubic = xCabItem.Cubic,
                                        Barcode = xCabItem.Barcode,
                                        Quantity = xCabItem.Qantity,
                                        Status = xCabItem.Status
                                    });
                            }
                        }
                        catch (Exception e)
                        {
                            await Logger.Log(
                                "Exception Occurred in AddBarcodes while adding barcode items to xCabItems table, message: " +
                                e.Message, nameof(XCabBookingRepository));
                            updatedSuccessfully = false;
                        }
                    }
                    else
                    {
                        updatedSuccessfully = false;
                    }
                }
                catch (Exception ex)
                {
                    await Logger.Log(
                        "Exception Occurred in FindingBooking & AddBarcodes while adding barcode items to xCabItems table, message: " +
                        ex.Message, nameof(XCabBookingRepository));
                    updatedSuccessfully = false;
                }
            }

            return updatedSuccessfully;
        }

        public async Task<List<XCabAsnBooking>> GetAsnXCabBookingsForAccount(string accountCode, int stateId,
            DateTime fromDateTime, DateTime toDateTime)
        {
            var xCabBookings = new List<XCabAsnBooking>();
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("AccountCode", accountCode);
                    dynamicParameters.Add("StateId", stateId);
                    dynamicParameters.Add("FromDateTime", fromDateTime);
                    dynamicParameters.Add("ToDateTime", toDateTime);
                    var sql = @"SELECT 
	                              [BookingId]
                                  ,[StateId]
                                  ,[LoginId]
                                  ,[accountcode]
                                  ,[ServiceCode]
                                  ,[FromSuburb]
                                  ,[FromPostcode] 
                                  ,[FromDetail1]
                                  ,[FromDetail2]
                                  ,[FromDetail3]
                                  ,[ExtraPuInformation]
                                  ,[ExtraDelInformation]
                                  ,[ToSuburb]
                                  ,[ToPostcode]
                                  ,[ToDetail1]
                                  ,[ToDetail2]
                                  ,[ToDetail3]
                                  ,[Ref1]
                                  ,[Ref2]
                                  ,[Caller]
	                              ,[DespatchDateTime]    
	                              ,[TPLUS_JobNumber]
                                  ,[TotalWeight]
                                  ,[TotalVolume]
                                  ,[Atl]
                                  ,[PreAllocatedDriverNumber]
                                  ,[IsQueued]
                                  , CAST((SELECT Count(*) from [dbo].[xCabNotifications] n WHERE n.BookingId = a.BookingId) as int) as NotificationCount
								  , CAST((SELECT Count(*) from [dbo].[xCabContactDetails] c WHERE c.BookingId = a.BookingId)as int) as ContactDetailsCount
                              FROM [xcab].[dbo].[xCabBooking] a
                              WHERE a.Cancelled = 0
                              AND a.UploadedToTplus = 0
                              AND a.OkToUpload = 0
                              AND a.UploadDateTime IS NULL
                              AND a.AccountCode = @AccountCode
                              AND a.StateId = @StateId
                              AND a.DespatchDateTime Between @FromDateTime AND @ToDateTime";

                    xCabBookings =
                        (List<XCabAsnBooking>)(connection.QueryAsync<XCabAsnBooking>(sql, dynamicParameters).Result);
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred in XCabBookingRepository: GetAsnXCabBookingsForAccount, message: " +
                    e.Message, "XCabBookingRepository");
            }

            return xCabBookings;
        }

        public async Task<ICollection<int>> ActivateBookings(ActivateBookingsRequest activateBookingsRequest)
        {
            List<int> activatedBookingIds = null;
            if (activateBookingsRequest != null)
            {
                try
                {
                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("AccountCode", activateBookingsRequest.AccountCode);
                    dbArgs.Add("StateId", activateBookingsRequest.StateId);
                    dbArgs.Add("RouteCode", activateBookingsRequest.RouteCode);
                    dbArgs.Add("DriverNumber", activateBookingsRequest.DriverNumber);
                    var updateSql = string.Empty;
                    var selectSql = @"SELECT B.BookingId, R.DropSequence, B.ServiceCode, E.ETA FROM dbo.xCabBooking B
                                    INNER JOIN dbo.xCabBookingRoutes R ON B.BookingId = R.BookingId	
                                    LEFT JOIN dbo.xCabETA E ON B.BookingId = E.BookingId
	                            WHERE 
                                    B.AccountCode = @AccountCode 
	                                AND B.StateId = @StateId 
	                                AND CONVERT(DATE, COALESCE(B.DespatchDateTime,B.AdvanceDateTime)) = CONVERT(Date, getdate())
	                                AND B.Cancelled = 0
	                                AND B.OkToUpload = 0
	                                AND B.UploadedToTplus = 0
                                    AND R.Route = @RouteCode";

                    using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                    {
                        await connection.OpenAsync();
                        var identifiedBookings = await connection.QueryAsync<XCabBookingToUpload>(selectSql, dbArgs);
                        if (identifiedBookings != null && identifiedBookings.Count() > 0)
                        {
                            activatedBookingIds = new List<int>();
                            foreach (var booking in identifiedBookings)
                            {
                                updateSql = @$"UPDATE dbo.xCabBooking SET 
									                        PreAllocatedDriverNumber = @DriverNumber, 
	                                                        LastModified = GETDATE(), 
	                                                        DespatchDateTime = null, 
	                                                        Cancelled = 0, 
	                                                        OkToUpload=1, 
	                                                        IsQueued = 1, 
	                                                        ActionImmediate = 1,
	                                                        ConsignmentNumber = IsNull( ConsignmentNumber,CONCAT('X',BookingId))";
                                var dropSequence = string.IsNullOrEmpty(booking.ETA) ? "'SEQ-" + booking.DropSequence.TrimStart('0') + "'" : "'SEQ-" + booking.DropSequence.TrimStart('0') + $" ETA {booking.ETA}'";
                                if (booking.ServiceCode.ToUpper().StartsWith("X") || booking.ServiceCode.ToUpper().StartsWith("R"))
                                {
                                    updateSql += $", FromDetail3 = {dropSequence}";
                                }
                                else
                                {
                                    updateSql += $", ToDetail3 = {dropSequence}";
                                }
                                updateSql += $" WHERE BookingId = {booking.BookingId}";

                                var result = await connection.ExecuteAsync(updateSql, dbArgs);
                                if (result > 0)
                                {
                                    activatedBookingIds.Add(booking.BookingId);
                                }
                                else
                                {
                                    await Logger.Log($"Exception Occurred in GetBookingsToActivate when activating booking ID {booking.BookingId} for Ikea", nameof(XCabBookingRepository));
                                }
                                updateSql = string.Empty;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    await Logger.Log($"Exception Occurred in GetBookingsToActivate for activateBookingsRequest : {JsonConvert.SerializeObject(activateBookingsRequest)}, message: {e.Message}", nameof(XCabBookingRepository));
                }
            }
            return activatedBookingIds;
        }

        public async Task<bool> IsBookingFutiledForReference(string reference, int loginId)
        {
            var sql = @"SELECT 
	                            b.BookingId , 
	                            b.TPLUS_JobNumber, 
	                            b.TPLUS_JobAllocationDate,
	                            b.AccountCode,
                                b.StateId,
                                b.Cancelled,
                                b.UploadedToTplus
                            FROM 
	                            xCabBooking b
								INNER JOIN JobFutile j 
                                ON J.JobNumber = b.TPLUS_JobNumber AND j.StateId = b.StateId 
                                AND CONVERT(date,j.LastUpdated) = CONVERT(date,UploadDateTime)
                            WHERE   
                                b.LoginId = @LoginId
                                AND (b.Ref1 = @Reference OR b.Ref2 = @Reference)								
								AND b.Cancelled = 0
                                AND b.UploadedToTplus = 1;
                            ";
            bool futile = false;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("LoginId", loginId);
                    dynamicParameters.Add("Reference", reference);
                    var result = await connection.QueryAsync<XCabBooking>(sql, dynamicParameters);
                    if (result != null && result.Count() > 0)
                    {
                        futile = true;
                    }
                }
                catch (Exception ex)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabBookingRepository: IsBookingFutiledForReference, message: " +
                        ex.Message, "XCabBookingRepository");
                }
            }
            return futile;

        }
    }
}