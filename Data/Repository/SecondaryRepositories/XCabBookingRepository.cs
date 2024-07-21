using Core;
using Dapper;
using Data.Entities;
using Data.Entities.Tplus;
using Data.Repository.SecondaryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;


namespace Data.Repository.SecondaryRepositories
{
    public class XCabBookingRepository : IXCabBookingRepository
    {
        public ICollection<TrackingJob> GetTrackingUpdates(ICollection<XCabBooking> allBookings, EStates stateToConnect)
        {
            throw new NotImplementedException();
        }


        public ICollection<XCabBooking> GetBookingsForClientNotification(string accountCode, string stateId,
            string loginId)
        {
            ICollection<XCabBooking> xCabBookings = null;
            var dbArgs = new DynamicParameters();
            dbArgs.Add("AccountCode", accountCode);
            dbArgs.Add("StateId", stateId);
            dbArgs.Add("LoginId", loginId);
            //get a list of accounts that we need to create notifications
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql =
                        /* @" select StateId,AccountCode,LoginId,BookingId,PickupArriveLocation, PickupCompleteLocation, DeliveryArriveLocation, DeliveryCompleteLocation, 
                                                    PickupArrive, PickupComplete, DeliveryArrive, DeliveryComplete, Ref1, Ref2, FromSuburb, 
                                                    FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToDetail1,ToDetail2, ToDetail3,
                                                    ToDetail4,ToDetail5, ServiceCode, UploadDateTime,TPlus_JobNumber,Completed,FromPostcode,ToPostcode,DriverNumber From xCabBooking
                                                    WHERE LoginId=@LoginId AND StateId=@StateId AND AccountCode=@AccountCode AND convert(date,UploadDateTime)=convert(date,GETDATE())";*/
                        @" select TPLUS_JobAllocationDate,StateId,AccountCode,LoginId,BookingId,PickupArriveLocation, PickupCompleteLocation, DeliveryArriveLocation, DeliveryCompleteLocation, 
                                                                                PickupArrive, PickupComplete, DeliveryArrive, DeliveryComplete, Ref1, Ref2, FromSuburb, 
                                                                                FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToDetail1,ToDetail2, ToDetail3,
                                                                                ToDetail4,ToDetail5, ServiceCode, UploadDateTime,TPlus_JobNumber,Completed,FromPostcode,ToPostcode,DriverNumber From xCabBooking
                                                                                WHERE LoginId=@LoginId AND StateId=@StateId AND AccountCode=@AccountCode AND DATEDIFF(day,UploadDateTime,GETDATE()) <=3";
                    //WHERE LoginId=@LoginId AND StateId=@StateId AND AccountCode=@AccountCode AND DATEDIFF(day,UploadDateTime,GETDATE()) <=1";
                    xCabBookings = connection.Query<XCabBooking>(sql, dbArgs).ToList();
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabBookingRepository(Test): GetBookingsForClientNotification, message: " +
                        e.Message, "XCabBookingRepository");
                }
            }
            return xCabBookings;
        }

        public ICollection<XCabBooking> GetBookingsForClientNotification(IEnumerable<int> bookingId)
        {
            ICollection<XCabBooking> xCabBookings = null;
            var dbArgs = new DynamicParameters();
            dbArgs.Add("BookingID", bookingId);

            //get a list of accounts that we need to create notifications
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql =
                        /* @" select StateId,AccountCode,LoginId,BookingId,PickupArriveLocation, PickupCompleteLocation, DeliveryArriveLocation, DeliveryCompleteLocation, 
                                                    PickupArrive, PickupComplete, DeliveryArrive, DeliveryComplete, Ref1, Ref2, FromSuburb, 
                                                    FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToDetail1,ToDetail2, ToDetail3,
                                                    ToDetail4,ToDetail5, ServiceCode, UploadDateTime,TPlus_JobNumber,Completed,FromPostcode,ToPostcode,DriverNumber From xCabBooking
                                                    WHERE LoginId=@LoginId AND StateId=@StateId AND AccountCode=@AccountCode AND convert(date,UploadDateTime)=convert(date,GETDATE())";*/
                        @" select TPLUS_JobAllocationDate,StateId,AccountCode,LoginId,BookingId,PickupArriveLocation, PickupCompleteLocation, DeliveryArriveLocation, DeliveryCompleteLocation, 
                                                                                PickupArrive, PickupComplete, DeliveryArrive, DeliveryComplete, Ref1, Ref2, FromSuburb, 
                                                                                FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToDetail1,ToDetail2, ToDetail3,
                                                                                ToDetail4,ToDetail5, ServiceCode, UploadDateTime,TPlus_JobNumber,Completed,FromPostcode,ToPostcode,DriverNumber From xCabBooking
                                                                                WHERE BookingId IN @BookingID";
                    //WHERE LoginId=@LoginId AND StateId=@StateId AND AccountCode=@AccountCode AND DATEDIFF(day,UploadDateTime,GETDATE()) <=1";
                    xCabBookings = connection.Query<XCabBooking>(sql, dbArgs).ToList();
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabBookingRepository(Test): GetBookingsForClientNotification, message: " +
                        e.Message, "XCabBookingRepository");
                }
            }
            return xCabBookings;
        }
        [Obsolete("This is not needed anymore as we do npt check how many legs are there in a job")]
        public void UpdateTotalDeliveryLegs(int bookingId, int numberOfDelLegs)
        {
            try
            {
                var dbArgs = new DynamicParameters();

                dbArgs.Add("TotalDeliveryLegs", numberOfDelLegs);
                dbArgs.Add("BookingId", bookingId);
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql =
                        @"UPDATE XCabBooking SET TotalDeliveryLegs = @TotalDeliveryLegs WHERE BookingId=@BookingId";
                    connection.Execute(sql, dbArgs);
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository(Test): UpdateTotalDeliveryLegs, message: " +
                    e.Message, "XCabBookingRepository");
            }
        }

        //public async Task UpdateTrackingJob(TrackingJob job)
        //{
        //    try
        //    {
        //        var dbArgs = new DynamicParameters();
        //        string sqlFieldContent;

        //        // work out the event type 
        //        switch (job.CurrentTrackingEvent)
        //        {
        //            case ETrackingEvent.PICKUP_ARRIVE:

        //                dbArgs.Add("PickupArrive", job.eventDateTime);
        //                dbArgs.Add("PickupArriveLocation", job.eventLocation.Latitude + "," + job.eventLocation.Longitude);
        //                dbArgs.Add("BookingId", job.BookingId);
        //                sqlFieldContent = "PickupArrive = @PickupArrive, PickupArriveLocation= @PickupArriveLocation";
        //                break;
        //            case ETrackingEvent.PICKUP_COMPLETE:

        //                dbArgs.Add("PickupComplete", job.eventDateTime);
        //                dbArgs.Add("PickupCompleteLocation", job.eventLocation.Latitude + "," + job.eventLocation.Longitude);
        //                dbArgs.Add("BookingId", job.BookingId);
        //                sqlFieldContent = "PickupComplete = @PickupComplete, PickupCompleteLocation= @PickupCompleteLocation";

        //                break;
        //            case ETrackingEvent.DELIVERY_ARRIVE:

        //                dbArgs.Add("DeliveryArrive", job.eventDateTime);
        //                dbArgs.Add("DeliveryArriveLocation", job.eventLocation.Latitude + "," + job.eventLocation.Longitude);
        //                dbArgs.Add("BookingId", job.BookingId);
        //                sqlFieldContent = "DeliveryArrive = @DeliveryArrive, DeliveryArriveLocation= @DeliveryArriveLocation";
        //                break;
        //            case ETrackingEvent.DELIVERY_COMPLETE:

        //                dbArgs.Add("DeliveryComplete", job.eventDateTime);
        //                dbArgs.Add("DeliveryCompleteLocation", job.eventLocation.Latitude + "," + job.eventLocation.Longitude);
        //                dbArgs.Add("BookingId", job.BookingId);
        //                sqlFieldContent = "DeliveryComplete = @DeliveryComplete, DeliveryCompleteLocation= @DeliveryCompleteLocation, Completed = 1";
        //                break;
        //            default:
        //                Logger.Log("Update of XCAB, BookingId:" + job.BookingId + Environment.NewLine +
        //                           "Warning:No Event to Update",
        //                    "XCabBookingRepository(Test):UpdateTrackingJob:A");
        //                return;

        //        }


        //        using (var connection = new SqlConnection(DbSettings.Default.SecondaryDbConnectionStringtest))
        //        {
        //            connection.Open();
        //            var sql =
        //                @"UPDATE xCabBooking SET " + sqlFieldContent +
        //                " WHERE BookingId = @BookingId";
        //            connection.Execute(sql, dbArgs);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log("Update of XCAB, BookingId:" + job.BookingId + Environment.NewLine +
        //                   "Problem Updating:Message: " + ex.Message,
        //            "XCabBookingRepository(Test):UpdateTrackingJob:B");
        //    }
        //}

        public void UpdateJobCompleted(int bookingId)
        {
            try
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("BookingId", bookingId);
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql = @"UPDATE XCabBooking SET Completed = 1 WHERE BookingId=@BookingId";
                    connection.Execute(sql, dbArgs);
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository(Test): UpdateJobCompleted, message: " +
                    e.Message, "XCabBookingRepository");
            }
        }

        public ICollection<TplusJobEntity> GetJobsNotInXCab(ICollection<TplusJobEntity> tplusJobs)
        {
            var jobsNotInXcab = new List<TplusJobEntity>();
            if (!tplusJobs.Any())
                return jobsNotInXcab;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql =
                        @"SELECT * FROM XCABBOOKING WHERE AccountCode=@AccountCode AND UploadDateTime=@JobDate AND TPLUS_JobNumber = @TPLUS_JobNumber";
                    foreach (var tplusJob in tplusJobs)
                    {
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("AccountCode", tplusJob.ClientCode);
                        dbArgs.Add("JobDate", tplusJob.JobDate);
                        dbArgs.Add("TPLUS_JobNumber", tplusJob.JobNumber);
                        var job = connection.Query<TplusJobEntity>(sql, dbArgs).FirstOrDefault();
                        if (job == null)
                            jobsNotInXcab.Add(tplusJob);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository(Test): GetJobsNotInXCab, message: " +
                    e.Message, "XCabBookingRepository");
            }
            return jobsNotInXcab;
        }

        public void InsertJobsIntoXCab(ICollection<TplusJobEntity> tplusJobs)
        {
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

                    const string sql = @" INSERT INTO XCabBooking (LoginId, AccountCode,
                                                                                                StateId, TPLUS_JobNumber, FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,
                                                                                                 ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,FromPostcode,FromSuburb,ToPostcode,ToSuburb,UploadDateTime,Ref1, Ref2,ServiceCode,UploadedToTplus,DeliveryEta)
                                                                                        VALUES ( @LoginId, @AccountCode,
                                                                                                @StateId, @TPLUS_JobNumber, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4,
                                                                                                @FromDetail5,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@FromPostcode,@FromSuburb,@ToPostcode,@ToSuburb,@UploadDateTime,@Ref1,@Ref2,@ServiceCode,@UploadedToTplus,@DeliveryEta)";
                    foreach (var tplusJob in tplusJobs)
                        try
                        {
                            // this rule is for JAYPERM as we don't report if no ref1 or ref2
                            if (tplusJob.LoginId == 24 && tplusJob.Ref1.Trim().Length == 0 && tplusJob.Ref2.Trim().Length == 0)
                                continue;
                            if (tplusJob.FromPostcode != null)
                                connection.Execute(sql,
                                    new
                                    {
                                        tplusJob.LoginId,
                                        AccountCode = tplusJob.ClientCode,
                                        tplusJob.StateId,
                                        TPLUS_JobNumber = tplusJob.JobNumber,
                                        tplusJob.FromDetail1,
                                        tplusJob.FromDetail2,
                                        tplusJob.FromDetail3,
                                        tplusJob.FromDetail4,
                                        tplusJob.FromDetail5,
                                        tplusJob.ToDetail1,
                                        tplusJob.ToDetail2,
                                        tplusJob.ToDetail3,
                                        tplusJob.ToDetail4,
                                        tplusJob.ToDetail5,
                                        tplusJob.FromPostcode,
                                        tplusJob.FromSuburb,
                                        tplusJob.ToPostcode,
                                        tplusJob.ToSuburb,
                                        UploadDateTime = tplusJob.JobDate,
                                        tplusJob.Ref1,
                                        tplusJob.Ref2,
                                        tplusJob.ServiceCode,
                                        tplusJob.UploadedToTplus,
                                        tplusJob.DeliveryEta
                                    }
                                );
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository(Test): InsertJobsIntoXCab, JobDetails:" + tplusJob.ToString() + " ,Exception message: " +
                                ex.Message, "XCabBookingRepository");
                        }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "Exception Occurred in XCabCLientNotificationStatusRepository(Test): UpdateClientNotificationStatuses, message: " +
                    ex.Message, "XCabClientNotificationStatusRepository");
            }
        }

        public ICollection<XCabBooking> GetBookingForClientReference(DateTime jobDate, string clientReference1, string accountCode)
        {
            //XCabBooking xCabBookings = null;

            var xCabBookings = new List<XCabBooking>();

            var dbArgs = new DynamicParameters();
            dbArgs.Add("Ref1", "%" + clientReference1 + "%");
            dbArgs.Add("Ref2", "%" + clientReference1 + "%");
            // deduct 1 day as while the upload date is valid the client may have a booking that was uploaded
            // and completed on the same day, this is not always going to be the case. If the upload for example
            // was on the 30th April and despatch was on the 1st May the client will enter the 1st May not the
            // upload date.
            dbArgs.Add("UploadDateTime", jobDate.AddDays(-30));
            // job cannot be completed before it was uploaded so cater for days in the future
            // to collect the job detail which will only work if the REF1 and/or REF2 are unique
            DateTime exDateTime = jobDate.AddDays(30);
            dbArgs.Add("UploadDateTimeExtended", exDateTime);

            //get a list of accounts that we need to create notifications
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql =
                        @"SET ROWCOUNT 20 SELECT distinct b.StateId,b.AccountCode,b.LoginId,b.BookingId,b.PickupArriveLocation, b.PickupCompleteLocation, 
                                                                                        b.DeliveryArriveLocation, b.DeliveryCompleteLocation, b.PickupArrive, b.PickupComplete, 
                                                                                        b.DeliveryArrive, b.DeliveryComplete, c.Reference1 as Ref1, c.Reference2 as Ref2, b.FromSuburb, 
                                                                                b.FromDetail1, b.FromDetail2, b.FromDetail3,b.FromDetail4,b.FromDetail5,b.ToSuburb,b.ToDetail1,b.ToDetail2, b.ToDetail3,
                                                                                b.ToDetail4,b.ToDetail5, b.ServiceCode, b.UploadDateTime,b.TPlus_JobNumber,b.Completed,b.FromPostcode,b.ToPostcode,b.DriverNumber,
                                                                                       b.DateInserted  
                                                                      FROM xCabBooking b
                                                                      INNER JOIN  xCabClientReferences c
                                                                      ON b.BookingId = c.PrimaryJobId
                                                                                WHERE b.AccountCode IN (" + accountCode + ") AND " +
                        " ((c.Reference1 LIKE @Ref1) OR (c.Reference2 LIKE @Ref2)) AND convert(date,UploadDateTime) " +
                        "BETWEEN  @UploadDateTime AND @UploadDateTimeExtended AND b.TPLUS_JobNumber IS NOT NULL " +
                        "ORDER BY b.DateInserted DESC ";

                    xCabBookings = connection.Query<XCabBooking>(sql, dbArgs).ToList();
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabBookingRepository(Test): GetBookingForClientReference, message: " +
                        e.Message, "XCabBookingRepository");
                }
            }
            return xCabBookings;
        }

        public int InsertBooking(XCabBooking booking)
        {
            var insertedId = -1;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"DECLARE @InsertedRows AS TABLE (BookingId int);
                                                     INSERT INTO XCabBooking(LoginId,StateId,AccountCode,ServiceCode,FromSuburb,FromPostcode,FromDetail1,
                                                     FromDetail2,FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,
                                                     Ref1,Ref2,ExtraPuInformation,ExtraDelInformation,PreAllocatedDriverNumber,TotalWeight,TotalVolume,DespatchDateTime,IsQueued) OUTPUT Inserted.BookingId INTO @InsertedRows
                                                     VALUES (@LoginId,@StateId,@AccountCode,@ServiceCode,@FromSuburb,@FromPostcode,@FromDetail1,
                                                     @FromDetail2,@FromDetail3,@FromDetail4,@FromDetail5,@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,
                                                     @Ref1,@Ref2,@ExtraPuInformation,@ExtraDelInformation,@PreAllocatedDriverNumber,@TotalWeight,@TotalVolume,@DespatchDateTime,@IsQueued);
                                                    SELECT BookingId FROM @InsertedRows";
                    insertedId = connection.Query<int>(sql, new
                    {
                        booking.LoginId,
                        booking.StateId,
                        booking.AccountCode,
                        booking.ServiceCode,
                        booking.FromSuburb,
                        booking.FromPostcode,
                        booking.FromDetail1,
                        booking.FromDetail2,
                        booking.FromDetail3,
                        booking.FromDetail4,
                        booking.FromDetail5,
                        booking.ToSuburb,
                        booking.ToPostcode,
                        booking.ToDetail1,
                        booking.ToDetail2,
                        booking.ToDetail3,
                        booking.ToDetail4,
                        booking.ToDetail5,
                        booking.Ref1,
                        booking.Ref2,
                        booking.ExtraPuInformation,
                        booking.ExtraDelInformation,
                        booking.PreAllocatedDriverNumber,
                        // booking.UploadDateTime,
                        booking.TotalWeight,
                        booking.TotalVolume,
                        booking.DespatchDateTime,
                        booking.IsQueued
                        //UploadDateTime = booking.UploadDateTime,
                        //Cancelled = booking.Cancelled,
                        //OkToUpload = booking.OkToUpload
                    }).Single();
                    //also insert the items 
                    if (booking.lstItems != null && booking.lstItems.Count > 0)
                    {
                        try
                        {
                            foreach (var item in booking.lstItems)
                            {
                                var insertItemQuery =
                                    "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode) VALUES (@BookingId, @Description, @Length, @Width, @Height, @Weight,@Cubic, @Barcode)";
                                connection.Execute(insertItemQuery,
                                    new
                                    {
                                        BookingId = insertedId,
                                        item.Description,
                                        item.Length,
                                        item.Width,
                                        item.Height,
                                        item.Weight,
                                        item.Cubic,
                                        item.Barcode
                                    });
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository(Test): InsertBooking while adding barcode items to xCabItems table, message: " +
                                e.Message, "XCabBookingRepository");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabBookingRepository(Test): InsertBooking, message: " +
                        e.Message, "XCabBookingRepository");
                }
                return insertedId;
            }
        }

        public ICollection<XCabBooking> GetBookingsForTmsTracking(int days)
        {
            var xCabBookings = new List<XCabBooking>();

            var dbArgs = new DynamicParameters();
            dbArgs.Add("Days", days);
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"           SELECT B.BookingId,B.AccountCode,B.DriverNumber,B.UploadDateTime,
                                                                                                                  B.StateId,B.TPLUS_JobNumber,B.Ref1,B.Ref2,B.ConsignmentNumber, B.TplusPodTime,L.username, L.password, L.trackingfoldername,L.notifycanceljobs
                                                                                                                  FROM xCabBooking B INNER JOIN xCabFtpLoginDetails L ON B.LoginId = L.id
                                                                                                                  where B.TPLUS_JobNumber IS NOT NULL AND B.Cancelled = 0 AND B.Completed = 0 AND L.usingtmstracking = 0
                                                                                                                  and convert(date,B.uploaddatetime) between  DATEADD(day,-7,GETDATE()) and GETDATE()
                                                                                                                  ORDER BY B.BookingId,B.AccountCode,B.DriverNumber,B.UploadDateTime,
                                                                                                                  B.StateId,B.TPLUS_JobNumber,B.Ref1,B.Ref2,  B.ConsignmentNumber,B.TplusPodTime,L.username, L.password, L.trackingfoldername,L.notifycanceljobs
                                                                                                                  ;";
                    xCabBookings = connection.Query<XCabBooking>(sql).ToList();
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabBookingRepository(Test): GetBookingsForTmsTracking, message: " +
                        e.Message, "XCabBookingRepository");
                }
            }
            return xCabBookings;

        }
    }
}


