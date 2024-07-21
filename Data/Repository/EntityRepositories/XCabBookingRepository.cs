using Core;
using Dapper;
using Data.Entities;
using Data.Entities.ConsolidatedReferences;
using Data.Entities.Tplus;
using Data.Model;
using Data.Model.ConsolidatedReferences;
using Data.Repository.EntityRepositories.FileInfo;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabBookingRepository : IXCabBookingRepository
    {
        public ICollection<TrackingJob> GetTrackingUpdates(ICollection<XCabBooking> allBookings, EStates stateToConnect)
        {
            throw new NotImplementedException();
        }


        public async Task<ICollection<XCabBooking>> GetBookingsForClientNotification(string accountCode, string stateId,
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
                    await connection.OpenAsync();
#if DEBUG
                    const string sql =

                        @" select TPLUS_JobAllocationDate,StateId,AccountCode,LoginId,BookingId,PickupArriveLocation, PickupCompleteLocation, DeliveryArriveLocation, DeliveryCompleteLocation, 
                                                                                PickupArrive, PickupComplete, DeliveryArrive, DeliveryComplete, Ref1, Ref2, FromSuburb, 
                                                                                FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToDetail1,ToDetail2, ToDetail3,
                                                                                ToDetail4,ToDetail5, ServiceCode, UploadDateTime,TPlus_JobNumber,Completed,FromPostcode,ToPostcode,DriverNumber From xCabBooking
                                                                                WHERE StateId=@StateId  AND AccountCode=@AccountCode AND Cancelled =0 AND DATEDIFF(day,UploadDateTime,GETDATE()) <=3";
#else
                                        const string sql =
                      
                        @" select TPLUS_JobAllocationDate,StateId,AccountCode,LoginId,BookingId,PickupArriveLocation, PickupCompleteLocation, DeliveryArriveLocation, DeliveryCompleteLocation, 
                                                                                PickupArrive, PickupComplete, DeliveryArrive, DeliveryComplete, Ref1, Ref2, FromSuburb, 
                                                                                FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToDetail1,ToDetail2, ToDetail3,
                                                                                ToDetail4,ToDetail5, ServiceCode, UploadDateTime,TPlus_JobNumber,Completed,FromPostcode,ToPostcode,DriverNumber From xCabBooking
                                                                                WHERE StateId=@StateId  AND AccountCode=@AccountCode AND Cancelled =0 AND DATEDIFF(day,UploadDateTime,GETDATE()) <=3";

#endif
                    //WHERE LoginId=@LoginId AND StateId=@StateId AND AccountCode=@AccountCode AND DATEDIFF(day,UploadDateTime,GETDATE()) <=1";
                    xCabBookings = (List<XCabBooking>)await connection.QueryAsync<XCabBooking>(sql, dbArgs);
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabBookingRepository: GetBookingsForClientNotification, message: " +
                        e.Message, "XCabBookingRepository");
                }
            }
            return xCabBookings;
        }

        public async Task<ICollection<XCabBooking>> GetBookingsForClientNotification(IEnumerable<int> bookingId)
        {
            ICollection<XCabBooking> xCabBookings = null;
            var dbArgs = new DynamicParameters();
            dbArgs.Add("BookingID", bookingId);

            //get a list of accounts that we need to create notifications
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    const string sql =
                        @" select TPLUS_JobAllocationDate,StateId,AccountCode,LoginId,BookingId,PickupArriveLocation, PickupCompleteLocation, DeliveryArriveLocation, DeliveryCompleteLocation, 
                                                                                PickupArrive, PickupComplete, DeliveryArrive, DeliveryComplete, Ref1, Ref2, FromSuburb, 
                                                                                FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToDetail1,ToDetail2, ToDetail3,
                                                                                ToDetail4,ToDetail5, ServiceCode, UploadDateTime,TPlus_JobNumber,Completed,FromPostcode,ToPostcode,DriverNumber From xCabBooking
                                                                                WHERE BookingId IN @BookingID";
                    //WHERE LoginId=@LoginId AND StateId=@StateId AND AccountCode=@AccountCode AND DATEDIFF(day,UploadDateTime,GETDATE()) <=1";
                    xCabBookings = (List<XCabBooking>)await connection.QueryAsync<XCabBooking>(sql, dbArgs);
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabBookingRepository: GetBookingsForClientNotification, message: " +
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
                    "Exception Occurred in XCabBookingRepository: UpdateTotalDeliveryLegs, message: " +
                    e.Message, "XCabBookingRepository");
            }
        }

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
                    "Exception Occurred in XCabBookingRepository: UpdateJobCompleted, message: " +
                    e.Message, "XCabBookingRepository");
            }
        }

        public ICollection<TplusMultiLegModel> GetJobsNotInXCab(ICollection<TplusMultiLegModel> tplusJobs)
        {
            var jobsNotInXcab = new List<TplusMultiLegModel>();
            if (!tplusJobs.Any())
                return jobsNotInXcab;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql =
                        @"SELECT TOP 1 1 FROM XCABBOOKING WHERE AccountCode=@AccountCode AND CONVERT(date,UploadDateTime) = CONVERT(date,@JobDate) AND TPLUS_JobNumber = @TPLUS_JobNumber";
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
                    "Exception Occurred in XCabBookingRepository: GetJobsNotInXCab, message: " +
                    e.Message, "XCabBookingRepository");
            }
            return jobsNotInXcab;
        }

        public ICollection<TplusMultiLegModel> GetJobsNotInXCabMultiLegs(ICollection<TplusMultiLegModel> tplusJobs)
        {
            var jobsNotInXcab = new List<TplusMultiLegModel>();
            if (!tplusJobs.Any())
                return jobsNotInXcab;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql =
                        @"SELECT TOP 1 1 FROM XCabMultipleDeliveries WHERE AccountCode=@AccountCode AND JobDate=@JobDate AND PrimaryJobNumber = @PrimaryJobNumber";
                    foreach (var tplusJob in tplusJobs)
                    {
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("AccountCode", tplusJob.ClientCode);
                        dbArgs.Add("JobDate", tplusJob.JobDate);
                        dbArgs.Add("PrimaryJobNumber", tplusJob.BaseJobNumber);
                        var job = connection.Query<TplusMultiLegModel>(sql, dbArgs).FirstOrDefault();
                        if (job == null)
                            jobsNotInXcab.Add(tplusJob);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: GetJobsNotInXCabMultiLegs, message: " +
                    e.Message, "XCabBookingRepository");
            }
            return jobsNotInXcab;
        }

        public ICollection<TplusMultiLegModel> InsertJobsIntoXCab(ICollection<TplusMultiLegModel> tplusJobs)
        {
            ICollection<TplusMultiLegModel> output = new List<TplusMultiLegModel>();
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

                    const string sql = @" INSERT INTO XCabBooking (LoginId, AccountCode, StateId, TPLUS_JobNumber, FromDetail1, FromDetail2, FromDetail3,FromDetail4,FromDetail5,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,FromPostcode,FromSuburb,ToPostcode,ToSuburb,UploadDateTime,Ref1, Ref2,ServiceCode,UploadedToTplus,DeliveryEta,AdvanceDateTime,DespatchDateTime)
                                         VALUES ( @LoginId, @AccountCode, @StateId, @TPLUS_JobNumber, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4,@FromDetail5,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@FromPostcode,@FromSuburb,@ToPostcode,@ToSuburb,@UploadDateTime,@Ref1,@Ref2,@ServiceCode,@UploadedToTplus,@DeliveryEta,@AdvanceDateTime,@DespatchDateTime);
                                        SELECT CAST(SCOPE_IDENTITY() As Int)";
                    foreach (var tplusJob in tplusJobs)
                        try
                        {
                            // this rule is for JAYPERM as we don't report if no ref1 or ref2
                            if (tplusJob.LoginId == 24 && tplusJob.Ref1.Trim().Length == 0 && tplusJob.Ref2.Trim().Length == 0)
                                continue;
                            if (tplusJob.BaseJobNumber != tplusJob.JobNumber)
                            {
                                //this is the case of multi legs and we do not add them in the XCabBookings table
                                output.Add(tplusJob);
                                continue;
                            }
                            if (tplusJob.FromPostcode != null)
                            {
                                var id = connection.Query<int>(sql,
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
                                        Ref1 = tplusJob.Ref1.Trim().Replace("\0", ""),
                                        Ref2 = tplusJob.Ref2.Trim().Replace("\0", ""),
                                        tplusJob.ServiceCode,
                                        tplusJob.UploadedToTplus,
                                        tplusJob.DeliveryEta,
                                        tplusJob.AdvanceDateTime,
                                        tplusJob.DespatchDateTime
                                    }

                                );
                                tplusJob.BookingId = id.FirstOrDefault();
                                output.Add(tplusJob);

                            }
                            else
                            {
                                var id = connection.Query<int>(sql,
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
                                        FromPostcode = "0",
                                        FromSuburb = "Unavailable",
                                        tplusJob.ToPostcode,
                                        tplusJob.ToSuburb,
                                        UploadDateTime = tplusJob.JobDate,
                                        Ref1 = tplusJob.Ref1.Trim().Replace("\0", ""),
                                        Ref2 = tplusJob.Ref2.Trim().Replace("\0", ""),
                                        tplusJob.ServiceCode,
                                        tplusJob.UploadedToTplus,
                                        tplusJob.DeliveryEta,
                                        tplusJob.AdvanceDateTime,
                                        tplusJob.DespatchDateTime
                                    }

                                );
                                tplusJob.BookingId = id.FirstOrDefault();
                                output.Add(tplusJob);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository: InsertJobsIntoXCab, JobDetails:" + tplusJob.ToString() + " ,Exception message: " +
                                ex.Message, "XCabBookingRepository");
                        }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: InsertJobsIntoXCab, message: " +
                    ex.Message, "XCabBookingRepository");
            }
            return output;
        }

        public void InsertJobsIntoXCabMultiLegs(ICollection<TplusMultiLegModel> tplusJobs)
        {
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

                    const string sql = @" INSERT INTO XCabMultipleDeliveries(PrimaryBookingId, PrimaryJobNumber, DriverNumber, AccountCode, DeliverySuburb, Ref1, Ref2, LegNumber, JobDate, Eta, TotalLegs) 
                                                                                        VALUES ( @PrimaryBookingId, @PrimaryJobNumber,
                                                                                                @DriverNumber, @AccountCode, @DeliverySuburb, @Ref1, @Ref2, @LegNumber,
                                                                                                @JobDate,@Eta,@TotalLegs)";
                    foreach (var tplusJob in tplusJobs)
                        try
                        {
                            // this rule is for JAYPERM as we don't report if no ref1 or ref2

                            /* if (tplusJob.LoginId == 24 && tplusJob.Ref1.Trim().Length == 0 && tplusJob.Ref2.Trim().Length == 0)
                                 continue;*/
                            if (tplusJob.BaseJobNumber != null && !string.IsNullOrEmpty(tplusJob.SuburbName))
                                connection.Execute(sql,
                                    new
                                    {
                                        PrimaryBookingId = tplusJob.BookingId,
                                        PrimaryJobNumber = tplusJob.BaseJobNumber,
                                        DriverNumber = tplusJob.Driver,
                                        AccountCode = tplusJob.ClientCode,
                                        DeliverySuburb = tplusJob.SuburbName,
                                        Ref1 = tplusJob.Ref1.Trim(),
                                        Ref2 = tplusJob.Ref2.Trim(),
                                        LegNumber = (Convert.ToInt32(tplusJob.LegNumber) + 1).ToString().PadLeft(2, '0'),
                                        tplusJob.JobDate,
                                        Eta = tplusJob.DeliveryEta,
                                        tplusJob.TotalLegs
                                    }
                                );
                            else if (tplusJob.BaseJobNumber != null && !string.IsNullOrEmpty(tplusJob.ToSuburb))
                                connection.Execute(sql,
                                    new
                                    {
                                        PrimaryBookingId = tplusJob.BookingId,
                                        PrimaryJobNumber = tplusJob.BaseJobNumber,
                                        DriverNumber = tplusJob.Driver,
                                        AccountCode = tplusJob.ClientCode,
                                        DeliverySuburb = tplusJob.ToSuburb,
                                        Ref1 = tplusJob.Ref1.Trim(),
                                        Ref2 = tplusJob.Ref2.Trim(),
                                        LegNumber = (Convert.ToInt32(tplusJob.LegNumber) + 1).ToString().PadLeft(2, '0'),
                                        tplusJob.JobDate,
                                        Eta = tplusJob.DeliveryEta,
                                        tplusJob.TotalLegs
                                    }
                                );
                            else if (tplusJob.BaseJobNumber != null)
                                connection.Execute(sql,
                                    new
                                    {
                                        PrimaryBookingId = tplusJob.BookingId,
                                        PrimaryJobNumber = tplusJob.BaseJobNumber,
                                        DriverNumber = tplusJob.Driver,
                                        AccountCode = tplusJob.ClientCode,
                                        DeliverySuburb = tplusJob.SuburbName,
                                        Ref1 = tplusJob.Ref1.Trim(),
                                        Ref2 = tplusJob.Ref2.Trim(),
                                        LegNumber = (Convert.ToInt32(tplusJob.LegNumber) + 1).ToString().PadLeft(2, '0'),
                                        tplusJob.JobDate,
                                        Eta = tplusJob.DeliveryEta,
                                        tplusJob.TotalLegs
                                    }
                                );
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository: InsertJobsIntoXCabMultiLegs, JobDetails:" + tplusJob.ToString() + " ,Exception message: " +
                                ex.Message, "XCabBookingRepository");
                        }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: InsertJobsIntoXCabMultiLegs, message: " +
                    ex.Message, "XCabBookingRepository");
            }
        }

        public List<ClientReferenceIdList> GetReferenceValuesJobNumberDate(string tPlusJobNumber, DateTime dateInserted)
        {
            // job number may be too long take last 8 chars   100418S00195015
            tPlusJobNumber = tPlusJobNumber.Length > 8 ? tPlusJobNumber.Substring(7) : tPlusJobNumber;

            var clientLists = new List<ClientReferenceIdList>();

            var dbArgs = new DynamicParameters();
            dbArgs.Add("tPlusJobNumber", tPlusJobNumber);
            //dbArgs.Add("UploadDateTime", dateInserted);
            dbArgs.Add("DateInsertedFrom", dateInserted.AddDays(-3));
            dbArgs.Add("DateInsertedTo", dateInserted.AddDays(1));
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql =
                        "SELECT b.BookingId , c.Reference1 AS Ref1Ids, c.Reference2 AS Ref2Ids " +
                        "FROM xCabBooking b " +
                        "INNER JOIN " +
                        "xCabClientReferences c " +
                        "ON " +
                        "b.BookingId = c.PrimaryJobId " +
                        "WHERE (CONVERT(DATE, DateInserted) BETWEEN @DateInsertedFrom AND  @DateInsertedTo) " +
                        "AND TPLUS_JobNumber = @tplusJobNumber";


                    clientLists = connection.Query<ClientReferenceIdList>(sql, dbArgs).ToList();

                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabBookingRepository: GetReferenceValuesJobNumberDate, message: " +
                        e.Message, "XCabBookingRepository");
                }
            }

            return clientLists;
        }


        public List<ClientReferenceIdList> GetReference2Values(int primaryJobId)
        {
            var clientLists = new List<ClientReferenceIdList>();

            var dbArgs = new DynamicParameters();
            dbArgs.Add("PrimaryJobId", primaryJobId);

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql = "SELECT Reference1 AS Ref1Ids, Reference2 as Ref2Ids FROM xCabClientReferences WHERE PrimaryJobId = @PrimaryJobId";
                    clientLists = connection.Query<ClientReferenceIdList>(sql, dbArgs).ToList();

                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabBookingRepository: GetReference2Values, message: " +
                        e.Message, "XCabBookingRepository");
                }
            }

            return clientLists;
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
                        "Exception Occurred in XCabBookingRepository: GetBookingForClientReference, message: " +
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
                    booking.AccountCode = booking.AccountCode.ToUpper();
                    DateTime? DespatchDateTime = null;

                    if (booking.DespatchDateTime != DateTime.MinValue)
                        DespatchDateTime = booking.DespatchDateTime;                          

                    connection.Open();
                    const string sql = @"DECLARE @InsertedRows AS TABLE (BookingId int);
                                                     INSERT INTO XCabBooking(LoginId,StateId,AccountCode,ServiceCode,FromSuburb,FromPostcode,FromDetail1,
                                                    FromDetail2,FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,
                                                    Ref1,Ref2,ExtraPuInformation,ExtraDelInformation,PreAllocatedDriverNumber,TotalWeight,TotalVolume,DespatchDateTime,IsQueued,Caller,ConsignmentNumber,OkToUpload,ATL, UsingComo, IsNtJob) OUTPUT Inserted.BookingId INTO @InsertedRows
                                                     VALUES (@LoginId,@StateId,@AccountCode,@ServiceCode,@FromSuburb,@FromPostcode,@FromDetail1,
                                                     @FromDetail2,@FromDetail3,@FromDetail4,@FromDetail5,@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,
                                                     @Ref1,@Ref2,@ExtraPuInformation,@ExtraDelInformation,@PreAllocatedDriverNumber,@TotalWeight,@TotalVolume,@DespatchDateTime,@IsQueued,@Caller,@ConsignmentNumber,@OkToUpload,@ATL, @UsingComo, @IsNtJob);
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
                        DespatchDateTime,
                        booking.IsQueued,
                        booking.Caller,
                        //UploadDateTime = booking.UploadDateTime,
                        //Cancelled = booking.Cancelled,                        
                        booking.ConsignmentNumber,
                        booking.OkToUpload,
                        booking.ATL,
                        booking.UsingComo,
                        booking.IsNtJob
                    }).Single();
                    //also insert the items 
                    if (booking.lstItems != null && booking.lstItems.Count > 0)
                    {
                        try
                        {
                            foreach (var item in booking.lstItems)
                            {
                                var insertItemQuery =
                                    "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity) VALUES (@BookingId, @Description, @Length, @Width, @Height, @Weight,@Cubic, @Barcode, @Quantity)";
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
                                        item.Barcode,
                                        item.Quantity
                                    });
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository: InsertBooking while adding barcode items to xCabItems table, message: " +
                                e.Message, "XCabBookingRepository");
                        }
                    }

                    if (booking.lstContactDetail != null && booking.lstContactDetail.Count > 0)
                    {
                        try
                        {
                            foreach (var detail in booking.lstContactDetail)
                            {
                                var insertContactQuery = "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                                connection.Execute(insertContactQuery, new
                                {
                                    BookingId = insertedId,
                                    detail.AreaCode,
                                    detail.PhoneNumber
                                });
                            }

                        }
                        catch (Exception ex)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository: InsertBooking while adding contact details to xCabContactDetails table, message: " +
                                ex.Message, "XCabBookingRepository");
                        }
                    }

                    if (booking.Remarks != null && booking.Remarks.Count > 0)
                    {
                        try
                        {
                            foreach (var remark in booking.Remarks)
                            {
                                var insertRemarksQuery = "INSERT INTO [dbo].[xCabRemarks] (BookingId, Remarks) VALUES (@BookingId,@Remarks)";
                                connection.Execute(insertRemarksQuery, new
                                {
                                    BookingId = insertedId,
                                    Remarks = remark
                                });
                            }

                        }
                        catch (Exception ex)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository: InsertBooking while adding remarks to xCabRemarks table, message: " +
                                ex.Message, "XCabBookingRepository");
                        }
                    }

                    if (booking.XCabBookingRoute != null)
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
                            connection.Execute(bookingRoutesInsertQuery,
                                    new
                                    {
                                        BookingId = insertedId,
                                        booking.XCabBookingRoute.Route,
                                        booking.XCabBookingRoute.DropSequence
                                    });
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository: InsertBooking while adding booking routes to XCabBookingRoutes table, message: " +
                                ex.Message, "XCabBookingRepository");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(
                        $"Exception Occurred in XCabBookingRepository(Data\\Repository\\EntityRepositories) when inserting booking details for account {booking.AccountCode}, Ref1: {booking.Ref1}, JobNumber: {booking.Tplus_JobNumber} : InsertBooking, message: " +
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
                        $"Exception Occurred in XCabBookingRepository: GetBookingsForTmsTracking (Number of days: {days}), message: " +
                        e.Message, "XCabBookingRepository");
                }
            }
            return xCabBookings;

        }

        public int GetBookingIdForMultiLeg(TplusMultiLegModel multiLeg)
        {
            int primaryBookingId = 0;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql =
                        @"SELECT * FROM XCabBooking WHERE AccountCode=@AccountCode AND TPLUS_JobNumber=@TPLUS_JobNumber AND UploadDateTime = @JobDate";
                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("AccountCode", multiLeg.ClientCode);
                    dbArgs.Add("JobDate", multiLeg.JobDate);
                    dbArgs.Add("TPLUS_JobNumber", multiLeg.BaseJobNumber);
                    var job = connection.Query<TplusMultiLegModel>(sql, dbArgs).FirstOrDefault();
                    if (job != null)
                        primaryBookingId = job.BookingId;
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: GetBookingIdForMultiLeg, message: " +
                    e.Message, "XCabBookingRepository");
            }
            return primaryBookingId;
        }

        public void UpdateStagedBookings(ICollection<int> BookingIds)
        {
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql =
                        @"UPDATE XCabBooking SET OkToUpload = 1,ActionImmediate = 1 WHERE BookingId=@BookingId AND UploadedToTplus=0 AND TPLUS_JobNumber is NULL and cancelled <> 1";


                    foreach (var bookingId in BookingIds)
                    {
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("BookingId", bookingId);
                        connection.Execute(sql, dbArgs);
                    }
                }

            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: UpdateStagedBookings, message: " +
                    e.Message, "XCabBookingRepository");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="ref1"></param>
        /// <param name="loginId"></param>
        /// <param name="previousWorkingday"></param>
        /// <returns></returns>
        public ICollection<XCabBooking> GetJobsInXCab(string accountCode, string ref1, int loginId, string previousWorkingday)
        {
            var xCabBookings = new List<XCabBooking>();

            var dbArgs = new DynamicParameters();
            dbArgs.Add("LoginId", loginId);
            dbArgs.Add("AccountCode", accountCode);
            dbArgs.Add("Ref1", ref1);
            dbArgs.Add("DateInsertedFrom", previousWorkingday + " 00:00:000");
            dbArgs.Add("DateInsertedTo", DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 00:00:000");
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"SELECT B.BookingId,B.AccountCode,B.DriverNumber,B.UploadDateTime, B.StateId,B.TPLUS_JobNumber,B.Ref1,B.Ref2,B.ConsignmentNumber, B.TplusPodTime,L.username, L.password, L.trackingfoldername,L.notifycanceljobs,B.OkToUpload
                                            FROM xCabBooking B INNER JOIN xCabFtpLoginDetails L ON B.LoginId = L.id
                                            WHERE B.Cancelled= 0 AND B.LoginId = @LoginId AND B.AccountCode = @AccountCode AND B.Ref1 = @Ref1 AND CONVERT(datetime, DateInserted) > CONVERT(datetime,@DateInsertedFrom) AND CONVERT(datetime, DateInserted) < CONVERT(datetime,@DateInsertedTo) ORDER BY B.BookingId DESC;";
                    xCabBookings = connection.Query<XCabBooking>(sql, dbArgs).ToList();
                }
                catch (Exception e)
                {
                    Logger.Log(
                        $"Exception Occurred in XCabBookingRepository: GetJobsInXCab (AccountCode:{accountCode}, Ref1: {ref1}, LoginId: {loginId}), message: " +
                        e.Message, "XCabBookingRepository");
                }
            }
            return xCabBookings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="ExistingBookinId"></param>
        /// <param name="previousWorkingday"></param>
        public void UpdateJobdetailsAndAddNewItems(Booking booking, int ExistingBookinId, string previousWorkingday)
        {
            try
            {
                var dbArgs = new DynamicParameters();
                dbArgs.Add("BookingId", ExistingBookinId);
                dbArgs.Add("Ref2", booking.Ref2);
                dbArgs.Add("FromDetail1", booking.FromDetail1);
                dbArgs.Add("FromDetail2", booking.FromDetail2);
                dbArgs.Add("FromDetail3", booking.FromDetail3);
                dbArgs.Add("FromSuburb", booking.FromSuburb);
                dbArgs.Add("FromPostcode", booking.FromPostcode);
                dbArgs.Add("ToDetail1", booking.ToDetail1);
                dbArgs.Add("ToDetail2", booking.ToDetail2);
                dbArgs.Add("ToDetail3", booking.ToDetail3);
                dbArgs.Add("ToSuburb", booking.ToSuburb);
                dbArgs.Add("ToPostcode", booking.ToPostcode);
                dbArgs.Add("TotalWeight", booking.TotalWeight);
                dbArgs.Add("ExtraDelInformation", booking.ExtraDelInformation);
                dbArgs.Add("ExtraPuInformation", booking.ExtraPuInformation);
                dbArgs.Add("DespatchDateTime", booking.DespatchDateTime);
                dbArgs.Add("ATL", booking.ATL);
                dbArgs.Add("ServiceCode", booking.ServiceCode.Trim());

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    const string sql = @"UPDATE XCabBooking SET Ref2 = @Ref2, 
                                            FromDetail1 =@FromDetail1,FromDetail2=@FromDetail2,FromDetail3=@FromDetail3,FromSuburb=@FromSuburb,FromPostcode =@FromPostcode,
                                            ToDetail1=@ToDetail1,ToDetail2=@ToDetail2,ToDetail3=@ToDetail3,ToSuburb=@ToSuburb,ToPostcode=@ToPostcode,
                                            TotalWeight=@TotalWeight,ExtraDelInformation=@ExtraDelInformation,ExtraPuInformation=@ExtraPuInformation, ATL = @ATL,DespatchDateTime=@DespatchDateTime, ServiceCode = @ServiceCode
                                            WHERE BookingId=@BookingId";
                    connection.Execute(sql, dbArgs);

                    try
                    {
                        const string sqlSelectItem =
                                         @"SELECT I.BookingId,I.Description, I.Length, I.Width, I.Height, I.Weight, I.Cubic, I.Barcode, I.Qantity FROM xCabItems I INNER JOIN xCabbooking B On I.BookingId = B.BookingId WHERE B.Cancelled = 0 AND B.LoginId = @LoginId AND B.AccountCode = @AccountCode AND B.Ref1 = @Ref1 AND CONVERT(datetime, DateInserted) > CONVERT(datetime,@DateInsertedFrom) AND CONVERT(datetime, DateInserted) < CONVERT(datetime,@DateInsertedTo)";

                        var dbArgsItems = new DynamicParameters();
                        dbArgsItems.Add("LoginId", booking.LoginDetails.Id);
                        dbArgsItems.Add("AccountCode", booking.AccountCode);
                        dbArgsItems.Add("Ref1", booking.Ref1);
                        dbArgsItems.Add("DateInsertedFrom", previousWorkingday + " 00:00:000");
                        dbArgsItems.Add("DateInsertedTo", DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 00:00:000");

                        var existingItems = connection.Query<Item>(sqlSelectItem, dbArgsItems).ToList();

                        foreach (var item in booking.lstItems)
                        {
                            if (existingItems == null || existingItems.Where(x => x.Barcode == item.Barcode).ToList().Count == 0)
                            {
                                var insertItemQuery =
                                    "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity) VALUES (@BookingId, @Description, @Length, @Width, @Height, @Weight,@Cubic, @Barcode, @Quantity)";
                                connection.Execute(insertItemQuery,
                                    new
                                    {
                                        BookingId = ExistingBookinId,
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
                    }
                    catch (Exception e)
                    {
                        Logger.Log(
                            "Exception Occurred in XCabBookingRepository: InsertBooking while adding items to xCabItems table, message: " +
                            e.Message, "XCabBookingRepository");
                    }


                    try
                    {
                        if (booking.Remarks != null)
                        {
                            var deleteRemarksQuery = "DELETE FROM [dbo].[xCabRemarks] WHERE BookingId = @BookingId";
                            connection.Execute(deleteRemarksQuery, new
                            {
                                BookingId = ExistingBookinId
                            });

                            foreach (var remark in booking.Remarks)
                            {
                                var insertRemarksQuery = "INSERT INTO [dbo].[xCabRemarks] (BookingId, Remarks) VALUES (@BookingId,@Remarks);";
                                connection.Execute(insertRemarksQuery, new
                                {
                                    BookingId = ExistingBookinId,
                                    Remarks = remark
                                });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Log(
                            "Exception Occurred in XCabBookingRepository: InsertBooking while adding Remaks to xCabRemarks table, message: " +
                            e.Message, "XCabBookingRepository");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: UpdateJobdetailsAndAddNewItems, message: " +
                    e.Message, "XCabBookingRepository");
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="fileName"></param>
        /// <param name="storeName"></param>
        /// <param name="jobType"></param>
        /// <param name="previousWorkingday"></param>
        public void InsertJobdetailsAndAddNewItems(Booking booking, string fileName, string storeName, string jobType, string previousWorkingday, int fileStateId)
        {
            try
            {
                var insertedId = -1;

                booking.AccountCode = booking.AccountCode.ToUpper();

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    string sqlSelectExistItem =
                       @"SELECT COUNT(distinct I.Barcode) FROM xCabItems I INNER JOIN xCabbooking B On I.BookingId = B.BookingId WHERE B.Cancelled = 0 
                            AND B.LoginId = " + booking.LoginDetails.Id + " AND B.AccountCode = '" + booking.AccountCode + "' AND B.Ref1 = '" + booking.Ref1 + "' " +
                            " AND CONVERT(datetime, DateInserted) > CONVERT(datetime,'" + previousWorkingday + " 00:00:000" + "') AND CONVERT(datetime, DateInserted) < CONVERT(datetime,'" + DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 00:00:000" + "') " +
                            " AND CONVERT(date, DateInserted) = CONVERT(date,'" + DateTime.Now.ToString("yyyy/MM/dd") + "') " +
                            " AND I.Barcode IN('" + (string.Join("','", booking.lstItems.Select(x => x.Barcode).ToList())) + "')";

                    var conutExistItems = connection.ExecuteScalar<int>(sqlSelectExistItem);

                    if (conutExistItems != booking.lstItems.Count)
                    {
                        connection.Open();
                        var LoginId = booking.LoginDetails.Id;
                        const string sql = @"DECLARE @InsertedRows AS TABLE (BookingId int);
                                                     INSERT INTO XCabBooking(LoginId,StateId,AccountCode,ServiceCode,FromSuburb,FromPostcode,FromDetail1,
                                                    FromDetail2,FromDetail3,FromDetail4,FromDetail5,ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,
                                                    Ref1,Ref2,ExtraPuInformation,ExtraDelInformation,PreAllocatedDriverNumber,TotalWeight,TotalVolume,DespatchDateTime,IsQueued,Caller,ConsignmentNumber,OkToUpload,ATL) OUTPUT Inserted.BookingId INTO @InsertedRows
                                                     VALUES (@LoginId,@StateId,@AccountCode,@ServiceCode,@FromSuburb,@FromPostcode,@FromDetail1,
                                                     @FromDetail2,@FromDetail3,@FromDetail4,@FromDetail5,@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,
                                                     @Ref1,@Ref2,@ExtraPuInformation,@ExtraDelInformation,@PreAllocatedDriverNumber,@TotalWeight,@TotalVolume,@DespatchDateTime,@IsQueued,@Caller,@ConsignmentNumber,@OkToUpload,@ATL);
                                                    SELECT BookingId FROM @InsertedRows";
                        insertedId = connection.Query<int>(sql, new
                        {
                            LoginId,
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
                            booking.TotalWeight,
                            booking.TotalVolume,
                            booking.DespatchDateTime,
                            booking.IsQueued,
                            booking.Caller,
                            booking.ConsignmentNumber,
                            booking.OkToUpload,
                            booking.ATL
                        }).Single();

                        if (insertedId > 0)
                            new XCabBookingFileInformationRepository().Insert(new XCabBookingFileInformation
                            {
                                LoginId = Convert.ToInt32(LoginId),
                                StateId = fileStateId,
                                BookingId = insertedId,
                                FileName = fileName,
                                StoreNameFromFile = storeName,
                                JobType = jobType
                            });

                        try
                        {
                            const string sqlSelectItem =
                                                    @"SELECT I.BookingId,I.Description, I.Length, I.Width, I.Height, I.Weight, I.Cubic, I.Barcode, I.Qantity FROM xCabItems I INNER JOIN xCabbooking B On I.BookingId = B.BookingId WHERE B.Cancelled = 0 AND B.LoginId = @LoginId AND B.AccountCode = @AccountCode AND B.Ref1 = @Ref1 AND CONVERT(datetime, DateInserted) > CONVERT(datetime,@DateInsertedFrom) AND CONVERT(datetime, DateInserted) < CONVERT(datetime,@DateInsertedTo)";

                            var dbArgsItems = new DynamicParameters();
                            dbArgsItems.Add("LoginId", booking.LoginDetails.Id);
                            dbArgsItems.Add("AccountCode", booking.AccountCode);
                            dbArgsItems.Add("Ref1", booking.Ref1);
                            dbArgsItems.Add("DateInsertedFrom", previousWorkingday + " 00:00:000");
                            dbArgsItems.Add("DateInsertedTo", DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 00:00:000");

                            var existingItems = connection.Query<Item>(sqlSelectItem, dbArgsItems).ToList();

                            foreach (var item in booking.lstItems)
                            {
                                if (existingItems == null || existingItems.Where(x => x.Barcode == item.Barcode).ToList().Count == 0)
                                {
                                    var insertItemQuery =
                                        "INSERT INTO xCabItems(BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity) VALUES (@BookingId, @Description, @Length, @Width, @Height, @Weight,@Cubic, @Barcode, @Quantity)";
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
                                            item.Barcode,
                                            item.Quantity
                                        });
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository: InsertBooking while adding items to xCabItems table, message: " +
                                e.Message, "XCabBookingRepository");
                        }

                        try
                        {
                            if (booking.Remarks != null)
                            {
                                var deleteRemarksQuery = "DELETE FROM [dbo].[xCabRemarks] WHERE BookingId = @BookingId";
                                connection.Execute(deleteRemarksQuery, new
                                {
                                    BookingId = insertedId
                                });

                                foreach (var remark in booking.Remarks)
                                {
                                    var insertRemarksQuery = "INSERT INTO [dbo].[xCabRemarks] (BookingId, Remarks) VALUES (@BookingId,@Remarks);";
                                    connection.Execute(insertRemarksQuery, new
                                    {
                                        BookingId = insertedId,
                                        Remarks = remark
                                    });
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception Occurred in XCabBookingRepository: InsertBooking while adding Remaks to xCabRemarks table, message: " +
                                e.Message, "XCabBookingRepository");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: UpdateJobdetailsAndAddNewItems, message: " +
                    e.Message, "XCabBookingRepository");
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public List<XCabBookingDetails> GetPedningTplusJobs(int? testBookingId = null)
        {
            List<XCabBookingDetails> xCabBookings = new List<XCabBookingDetails>();
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

                    var sql = @"SELECT TOP(300) 
								L.UsesSoap, 
								CD.NumContacts, 								
								L.username AS Username, 
								L.password AS Password, 
								L.trackingfoldername As TrackingFolderName,
								L.trackingschemaname As TrackingSchemaName,
								L.isbookingonlyclient As IsbookingOnlyClient, 
								l.BookingsFolderName,
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
								X.ConsignmentNumber,
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
                                L.SkipFtpAccess
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
									on X.BookingId = R.BookingId
							";


#if DEBUG
                    var where = @"WHERE X.BookingId in (" + testBookingId + ")";
#else
				var where = @"WHERE
								X.OkToUpload = 1 
								AND X.Cancelled = 0 
								AND X.UploadedToTplus = 0 
								AND X.uploadDateTime is null 
								AND L.Active = 1 
								AND (CAST( X.DateInserted AS DATE) = CAST( GETDATE() AS DATE) OR X.ActionImmediate = 1)
								ORDER BY X.BookingId";
#endif

                    sql = sql + where;

                    xCabBookings = connection.Query<XCabBookingDetails>(sql).ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred in XCabBookingRepository: GetPedningTplusJobs, message: " +
                    e.Message, "XCabBookingRepository");
            }
            return xCabBookings;
        }

    }
}


