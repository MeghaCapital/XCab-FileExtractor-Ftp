using Core;
using Core.Models.Slack;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Data.Repository.EntityRepositories;
using Data.Utils;

namespace Data
{
    /**
     * Db Connector manages all DB related operations
     * Date             Revision        Author      Summary
     * 29-Feb-2016      1.1             Rahul       Added LoginState table to associate loginid with a list of states that client can use for Booking
     * 23-Aug-2016      1.2             Rahul       Added Dapper
     */

    public class DbConnector
    {
        private const int _maxItemDescriptionLength = 50;
        public List<LoginDetails> GetLoginDetails()
        {
            var lstLoginDetails = new List<LoginDetails>();
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var cmd = new SqlCommand())
                    {
                        var sql = string.Format(@"SELECT s.*,l.* FROM xCabFtpLoginDetails l " +
                                                @"LEFT JOIN XCABLOGINSTATE S " +
                                                @"ON l.ID = s.loginid " +
                                                @"where l.active = 1 and l.trackingschemaname<>'csv' AND l.usesrestfulwebservice = 0 AND l.SkipFtpAccess = 0");
                        cmd.Connection = connection;
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                if ((reader["username"].ToString() != string.Empty) &&
                                    (reader["password"].ToString() != string.Empty))
                                {
                                    var loginDetail = new LoginDetails
                                    {
                                        UserName = reader["username"].ToString(),
                                        Password = reader["password"].ToString(),
                                        Id = reader["id"].ToString()
                                    };
                                    //check for configurable folder names
                                    if (reader["bookingsfoldername"].ToString() != string.Empty)
                                        loginDetail.BookingsFolderName = reader["bookingsfoldername"].ToString();

                                    if (reader["processedfoldername"].ToString() != string.Empty)
                                        loginDetail.ProcessedFolderName = reader["processedfoldername"].ToString();

                                    if (reader["errorfoldername"].ToString() != string.Empty)
                                        loginDetail.ErrorFolderName = reader["errorfoldername"].ToString();

                                    if (reader["trackingfoldername"].ToString() != string.Empty)
                                        loginDetail.TrackingFolderName = reader["trackingfoldername"].ToString();
                                    if (!string.IsNullOrEmpty(reader["trackingschemaname"].ToString()))
                                        loginDetail.TrackingSchemaName =
                                            reader["trackingschemaname"].ToString().ToLower();

                                    if (!string.IsNullOrEmpty(reader["bookingschemaname"].ToString()))
                                        loginDetail.BookingSchemaName = reader["bookingschemaname"].ToString().ToLower();

                                    if (!string.IsNullOrEmpty(reader["AccountCode"].ToString()))
                                        loginDetail.lstAccountCodes.Add(reader["AccountCode"].ToString());
                                    if (!string.IsNullOrEmpty(reader["DefaultServiceCode"].ToString()))
                                        loginDetail.lstServiceCodes.Add(reader["DefaultServiceCode"].ToString());
                                    if (!string.IsNullOrEmpty(reader["StateId"].ToString()))
                                        loginDetail.lstStateIds.Add(Convert.ToInt16(reader["StateId"].ToString()));
                                    //v1.1 populate list of states that a client can connect to
                                    if (lstLoginDetails.Count > 0)
                                    {
                                        var ld =
                                            lstLoginDetails.Find(x => x.UserName == reader["username"].ToString());
                                        if (ld != null)
                                        {
                                            if (!string.IsNullOrEmpty(reader["StateId"].ToString()))
                                                ld.lstStateIds.Add(Convert.ToInt16(reader["StateId"].ToString()));
                                            if (!string.IsNullOrEmpty(reader["AccountCode"].ToString()))
                                                ld.lstAccountCodes.Add(reader["AccountCode"].ToString());
                                            if (!string.IsNullOrEmpty(reader["DefaultServiceCode"].ToString()))
                                                ld.lstServiceCodes.Add(reader["DefaultServiceCode"].ToString());
                                        }
                                        else
                                        {
                                            lstLoginDetails.Add(loginDetail);
                                        }
                                    }
                                    else
                                    {
                                        lstLoginDetails.Add(loginDetail);
                                    }
                                }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("Exception Occurred while retrieving Ftp Login Details from DB: " + e.Message, Name());
                }
            }
            return lstLoginDetails;
        }

        public List<LoginDetails> GetLoginDetailsForCsvIntegration(int loginId)
        {
            var lstLoginDetails = new List<LoginDetails>();
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var cmd = new SqlCommand())
                    {
                        var sql = string.Format(@"SELECT s.*,l.* FROM xCabFtpLoginDetails l " +
                                                @"LEFT JOIN XCABLOGINSTATE S " +
                                                @"ON l.ID = s.loginid " +
                                                @"where l.active = 1 and l.trackingschemaname='csv' and l.loginid='" +
                                                loginId + "')");
                        cmd.Connection = connection;
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                if ((reader["username"].ToString() != string.Empty) &&
                                    (reader["password"].ToString() != string.Empty))
                                {
                                    var loginDetail = new LoginDetails
                                    {
                                        UserName = reader["username"].ToString(),
                                        Password = reader["password"].ToString(),
                                        Id = reader["id"].ToString()
                                    };
                                    //check for configurable folder names
                                    if (reader["bookingsfoldername"].ToString() != string.Empty)
                                        loginDetail.BookingsFolderName = reader["bookingsfoldername"].ToString();

                                    if (reader["processedfoldername"].ToString() != string.Empty)
                                        loginDetail.ProcessedFolderName = reader["processedfoldername"].ToString();

                                    if (reader["errorfoldername"].ToString() != string.Empty)
                                        loginDetail.ErrorFolderName = reader["errorfoldername"].ToString();

                                    if (reader["trackingfoldername"].ToString() != string.Empty)
                                        loginDetail.TrackingFolderName = reader["trackingfoldername"].ToString();
                                    if (!string.IsNullOrEmpty(reader["trackingschemaname"].ToString()))
                                        loginDetail.TrackingSchemaName =
                                            reader["trackingschemaname"].ToString().ToLower();
                                    if (!string.IsNullOrEmpty(reader["AccountCode"].ToString()))
                                        loginDetail.lstAccountCodes.Add(reader["AccountCode"].ToString());
                                    if (!string.IsNullOrEmpty(reader["DefaultServiceCode"].ToString()))
                                        loginDetail.lstServiceCodes.Add(reader["DefaultServiceCode"].ToString());
                                    if (!string.IsNullOrEmpty(reader["StateId"].ToString()))
                                        loginDetail.lstStateIds.Add(Convert.ToInt16(reader["StateId"].ToString()));
                                    //v1.1 populate list of states that a client can connect to
                                    if (lstLoginDetails.Count > 0)
                                    {
                                        var ld =
                                            lstLoginDetails.Find(x => x.UserName == reader["username"].ToString());
                                        if (ld != null)
                                        {
                                            if (!string.IsNullOrEmpty(reader["StateId"].ToString()))
                                                ld.lstStateIds.Add(Convert.ToInt16(reader["StateId"].ToString()));
                                            if (!string.IsNullOrEmpty(reader["AccountCode"].ToString()))
                                                ld.lstAccountCodes.Add(reader["AccountCode"].ToString());
                                            if (!string.IsNullOrEmpty(reader["DefaultServiceCode"].ToString()))
                                                ld.lstServiceCodes.Add(reader["DefaultServiceCode"].ToString());
                                        }
                                        else
                                        {
                                            lstLoginDetails.Add(loginDetail);
                                        }
                                    }
                                    else
                                    {
                                        lstLoginDetails.Add(loginDetail);
                                    }
                                }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("Exception Occurred while retrieving Ftp Login Details from DB: " + e.Message, Name());
                }
            }
            return lstLoginDetails;
        }

        public bool StoreMigratedJobsToDb(Booking booking)
        {
            var bookingStored = false;

            if (booking == null)
                return false;

            if (!string.IsNullOrWhiteSpace(booking.AccountCode))
                booking.AccountCode = booking.AccountCode.ToUpper();

            //check if Account Code & Service Codes are correctly configured
            if (string.IsNullOrEmpty(booking.AccountCode) || string.IsNullOrEmpty(booking.ServiceCode))
            {
                var errorMsg =
                    "Received file with Missing AccountCode and/or Service Code. As per the schema these fields are mandatory. This file will be moved into the Error_Files Folder. Booking Information:" + booking;
                Logger.Log(errorMsg, Name(), true);
                return false;
            }
            if (string.IsNullOrEmpty(booking.FromSuburb) || string.IsNullOrEmpty(booking.ToSuburb))
            {
                var errorMsg =
                    "Received file with Missing From/To Suburb Names. As per the schema these fields are mandatory.This file will be moved into the Error_Files Folder.";
                Logger.Log(errorMsg, Name(), true);
                return false;
            }
            if (string.IsNullOrEmpty(booking.FromPostcode) || string.IsNullOrEmpty(booking.ToPostcode))
            {
                const string errorMsg =
                    "Received file with Missing From/To Postcodes. As per the schema these fields are mandatory. This file will be moved into the Error_Files Folder.";
                Logger.Log(errorMsg, Name(), true);
                return false;
            }

            var itemSql =
                "INSERT INTO xCabItems (BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode) " +
                "VALUES (@BookingId,@Description,@Length,@Width,@Height,@Weight,@Cubic,@Barcode)";
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;

                        #region Raw text for SQL Inserts

                        var sql =
                            "INSERT INTO xCabBooking(LoginId,StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                            "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,DespatchDateTime,OkToUpload,UploadedToTplus,TPLUS_JobNumber,UploadDateTime) OUTPUT INSERTED.BookingId " +
                            " VALUES(@LoginId,@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                            "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@DespatchDateTime,@OkToUpload,@UploadedToTplus,@TPLUS_JobNumber,@UploadDateTime)" +
                            "";

                        #endregion

                        command.Parameters.Add("@LoginId", SqlDbType.Int);
                        command.Parameters.Add("@StateId", SqlDbType.Int);
                        command.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@ServiceCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@FromSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@Ref1", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@Ref2", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@DespatchDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@OkToUpload", SqlDbType.Bit);
                        command.Parameters.Add("@UploadedToTplus", SqlDbType.Bit);
                        command.Parameters.Add("@TPLUS_JobNumber", SqlDbType.NVarChar);
                        command.Parameters.Add("@UploadDateTime", SqlDbType.DateTime);
                        //now setup values based on the parameters defined aboved
                        command.Parameters["@LoginId"].Value = Convert.ToString(booking.LoginDetails.Id);
                        command.Parameters["@StateId"].Value = Convert.ToString(booking.StateId);
                        command.Parameters["@AccountCode"].Value = booking.AccountCode;
                        command.Parameters["@ServiceCode"].Value = booking.ServiceCode;
                        command.Parameters["@FromSuburb"].Value = string.IsNullOrEmpty(booking.FromSuburb)
                            ? ""
                            : booking.FromSuburb;
                        command.Parameters["@FromPostcode"].Value = string.IsNullOrEmpty(booking.FromPostcode)
                            ? ""
                            : booking.FromPostcode;
                        //due to issues with Except 
                        command.Parameters["@FromDetail1"].Value = string.IsNullOrEmpty(booking.FromDetail1)
                            ? ""
                            : booking.FromDetail1;
                        command.Parameters["@FromDetail2"].Value = string.IsNullOrEmpty(booking.FromDetail2)
                            ? ""
                            : booking.FromDetail2;
                        command.Parameters["@FromDetail3"].Value = string.IsNullOrEmpty(booking.FromDetail3)
                            ? ""
                            : booking.FromDetail3;
                        command.Parameters["@FromDetail4"].Value = string.IsNullOrEmpty(booking.FromDetail4)
                            ? ""
                            : booking.FromDetail4;
                        command.Parameters["@FromDetail5"].Value = string.IsNullOrEmpty(booking.FromDetail5)
                            ? ""
                            : booking.FromDetail5;
                        command.Parameters["@ToSuburb"].Value = string.IsNullOrEmpty(booking.ToSuburb)
                            ? ""
                            : booking.ToSuburb;
                        command.Parameters["@ToPostcode"].Value = string.IsNullOrEmpty(booking.ToPostcode)
                            ? ""
                            : booking.ToPostcode;
                        command.Parameters["@ToDetail1"].Value = string.IsNullOrEmpty(booking.ToDetail1)
                            ? ""
                            : booking.ToDetail1;
                        command.Parameters["@ToDetail2"].Value = string.IsNullOrEmpty(booking.ToDetail2)
                            ? ""
                            : booking.ToDetail2;
                        command.Parameters["@ToDetail3"].Value = string.IsNullOrEmpty(booking.ToDetail3)
                            ? ""
                            : booking.ToDetail3;
                        command.Parameters["@ToDetail4"].Value = string.IsNullOrEmpty(booking.ToDetail4)
                            ? ""
                            : booking.ToDetail4;
                        command.Parameters["@ToDetail5"].Value = string.IsNullOrEmpty(booking.ToDetail5)
                            ? ""
                            : booking.ToDetail5;
                        command.Parameters["@Ref1"].Value = string.IsNullOrEmpty(booking.Ref1) ? "" : booking.Ref1;
                        command.Parameters["@Ref2"].Value = string.IsNullOrEmpty(booking.Ref2) ? "" : booking.Ref2;
                        if (booking.DespatchDateTime != null)
                            command.Parameters["@DespatchDateTime"].Value = booking.DespatchDateTime;
                        else
                            command.Parameters["@DespatchDateTime"].Value = DBNull.Value;
                        command.Parameters["@OkToUpload"].Value = booking.OkToUpload;
                        command.Parameters["@UploadedToTplus"].Value = booking.UploadedToTplus;
                        command.Parameters["@TPLUS_JobNumber"].Value = booking.TPLUS_JobNumber;
                        if (booking.UploadDateTime != null)
                            command.Parameters["@UploadDateTime"].Value = booking.UploadDateTime;
                        else
                            command.Parameters["@UploadDateTime"].Value = DBNull.Value;
                        command.CommandText = sql;
                        var id = (int)command.ExecuteScalar();
                        command.Parameters.Clear();
                        //after updating the Booking table ; we also need to update the Items table with any barcodes that are present in the booking
                        if ((booking.lstItems != null) && (booking.lstItems.Count > 0))
                        {
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@Description", SqlDbType.NVarChar);
                            command.Parameters.Add("@Length", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Width", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Height", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Weight", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Cubic", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Barcode", SqlDbType.NVarChar);
                            foreach (var item in booking.lstItems)
                            {
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@Description"].Value = string.IsNullOrEmpty(item.Description)
                                    ? ""
                                    : item.Description;
                                command.Parameters["@Length"].Precision = 18;
                                command.Parameters["@Length"].Scale = 2;
                                command.Parameters["@Length"].Value = item.Length;
                                command.Parameters["@Width"].Precision = 18;
                                command.Parameters["@Width"].Scale = 2;
                                command.Parameters["@Width"].Value = item.Width;
                                command.Parameters["@Height"].Precision = 18;
                                command.Parameters["@Height"].Scale = 2;
                                command.Parameters["@Height"].Value = item.Height;
                                command.Parameters["@Weight"].Precision = 18;
                                command.Parameters["@Weight"].Scale = 2;
                                command.Parameters["@Weight"].Value = item.Weight;
                                command.Parameters["@Cubic"].Precision = 18;
                                command.Parameters["@Cubic"].Scale = 2;
                                command.Parameters["@Cubic"].Value = item.Cubic;
                                command.Parameters["@Barcode"].Value = string.IsNullOrEmpty(item.Barcode)
                                    ? ""
                                    : item.Barcode;
                                command.CommandText = itemSql;
                                command.ExecuteNonQuery();
                            }
                        }
                        //update contact details if any
                        if ((booking.lstContactDetail != null) && (booking.lstContactDetail.Count > 0))
                        {
                            //clear the parameters
                            command.Parameters.Clear();
                            //setup data types
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@AreaCode", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50);
                            var detailSql =
                                "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                            foreach (var detail in booking.lstContactDetail)
                            {
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@AreaCode"].Value = string.IsNullOrEmpty(detail.AreaCode)
                                    ? ""
                                    : detail.AreaCode;
                                command.Parameters["@PhoneNumber"].Value = string.IsNullOrEmpty(detail.PhoneNumber)
                                    ? ""
                                    : detail.PhoneNumber;
                                command.CommandText = detailSql;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    bookingStored = true;
                }
                catch (SqlException ex)
                {
                    Logger.Log("Exception Occurred: " + ex.Message, Name());
                    bookingStored = false;
                }
            }
            return bookingStored;
        }

        public int GetDriverForRoute(string routeName, int stateId, int loginId)
        {
            var drivernumber = -1;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        string.Format(@"SELECT DriverNumber FROM xCabDriverRoutes WHERE StateId=" + stateId +
                                      " AND LoginId=" + loginId + " AND routeName = '" + routeName.Trim() +
                                      "' AND Active = 1");
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        var dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                            if (!dataReader.IsDBNull(0))
                            {
                                var dNum = dataReader["DriverNumber"].ToString();
                                if (dNum != null)
                                    drivernumber = Convert.ToInt16(dNum);
                            }
                            else
                            {
                                drivernumber = -1;
                            }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while mapping route to driver. Input Route: " + routeName + ", StateId=" +
                    stateId + ", LoginId=" + loginId + ". Exception:" + e.Message, Name());
            }
            return drivernumber;
        }
        public bool IsQueueAllocateAllowed(string routeName, int stateId, int loginId)
        {
            var queueAllocateAllowed = false;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        string.Format(@"SELECT IsQueueAllocateAllowed FROM xCabDriverRoutes WHERE StateId=" + stateId +
                                      " AND LoginId=" + loginId + " AND routeName = '" + routeName.Trim() +
                                      "' AND Active = 1");
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        var dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                            if (!dataReader.IsDBNull(0))
                            {
                                var dNum = dataReader["IsQueueAllocateAllowed"].ToString();
                                //if (dNum != null)
                                queueAllocateAllowed = Convert.ToBoolean(dNum);
                            }

                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while checking for Queue Allocate Flag for a route. Input Route: " + routeName + ", StateId=" +
                    stateId + ", LoginId=" + loginId + ". Exception:" + e.Message, Name());
            }
            return queueAllocateAllowed;
        }
        public bool IsRouteInactive(string routeName, int stateId, int loginId)
        {
            var routeInactive = false;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        string.Format(@"SELECT DriverNumber FROM xCabDriverRoutes WHERE StateId=" + stateId +
                                      " AND LoginId=" + loginId + " AND routeName = '" + routeName.Trim() +
                                      "' AND Active = 0");
                    var routes = connection.Query<string>(sql);
                    //check if there are any routes returned
                    if (routes.Any())
                        routeInactive = true;
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while checking for inactive routes. Input Route: " + routeName + ", StateId=" +
                    stateId + ", LoginId=" + loginId + ". Exception:" + e.Message, Name());
            }
            return routeInactive;
        }

        public bool IsRouteNew(string routeName, int stateId, int loginId)
        {
            var routeNew = true;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        string.Format(@"SELECT * FROM xCabDriverRoutes WHERE StateId=" + stateId +
                                      " AND LoginId=" + loginId + " AND routeName = '" + routeName.Trim() +
                                      "'");
                    var present = connection.Query<string>(sql);
                    //check if there are any routes returned
                    if (present.Any())
                        routeNew = false;
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while checking for inactive routes. Input Route: " + routeName + ", StateId=" +
                    stateId + ", LoginId=" + loginId + ". Exception:" + e.Message, Name());
            }
            return routeNew;
        }

        public bool StoreToDb(Booking booking, bool okToUpload = true)
        {
            var bookingStored = false;
            if (booking == null)
                return false;

            if (!string.IsNullOrWhiteSpace(booking.AccountCode))
                booking.AccountCode = booking.AccountCode.ToUpper();

            //check if Account Code & Service Codes are correctly configured
            if (string.IsNullOrEmpty(booking.AccountCode) || string.IsNullOrEmpty(booking.ServiceCode))
            {
                string errorMsg =
                    "Received file with Missing AccountCode and/or Service Code. As per the schema these fields are mandatory. This file will be moved into the Error_Files Folder.";
                Logger.Log(errorMsg, Name(), true);
                return false;
            }
            if (string.IsNullOrEmpty(booking.FromSuburb) || string.IsNullOrEmpty(booking.FromPostcode))
            {
                var errorMsg =
                    "Received file with Missing From Suburb Names/ Postcodes. As per the schema these fields are mandatory.This file will be moved into the Error_Files Folder.";
                Logger.Log(errorMsg, Name(), true);
                return false;
            }
            if (string.IsNullOrEmpty(booking.ToSuburb) && string.IsNullOrEmpty(booking.ToPostcode))
            {
                var errorMsg =
                    "Received file with Missing To Postcodes and Suburb Names. As per the schema either one of the fields is mandatory. This file will be moved into the Error_Files Folder.";
                Logger.Log(errorMsg, Name(), true);
                return false;
            }

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        var sql = string.Empty;

                        #region Raw text for SQL Inserts
                        // TO DO: okToUpload - false new sql 
                        // Staged booking
                        if (!okToUpload)
                        {
                            sql =
                                "INSERT INTO xCabBooking(LoginId,StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                                "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,DespatchDateTime,ConsignmentNumber,TotalWeight, TotalVolume,PreAllocatedDriverNumber,Caller,ExtraDelInformation,ExtraPuInformation, OkToUpload, ATL, IsQueued, UsingComo) OUTPUT INSERTED.BookingId " +
                                " VALUES(@LoginId,@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                                "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@DespatchDateTime,@ConsignmentNumber, @TotalWeight,@TotalVolume,@PreAllocatedDriverNumber,@Caller,@ExtraDelInformation,@ExtraPuInformation, @OkToUpload, @ATL, @IsQueued, @UsingComo)" +
                                "";
                        }

                        else
                        {
                            sql =
                                "INSERT INTO xCabBooking(LoginId,StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                                "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,DespatchDateTime,ConsignmentNumber,TotalWeight, TotalVolume,PreAllocatedDriverNumber,Caller,ExtraDelInformation,ExtraPuInformation, ATL, IsQueued, UsingComo) OUTPUT INSERTED.BookingId " +
                                " VALUES(@LoginId,@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                                "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@DespatchDateTime,@ConsignmentNumber, @TotalWeight,@TotalVolume,@PreAllocatedDriverNumber,@Caller,@ExtraDelInformation,@ExtraPuInformation, @ATL, @IsQueued, @UsingComo)" +
                                "";
                        }

                        #endregion

                        command.Parameters.Add("@LoginId", SqlDbType.Int);
                        command.Parameters.Add("@StateId", SqlDbType.Int);
                        command.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@ServiceCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@FromSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@Ref1", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@Ref2", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@DespatchDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@ConsignmentNumber", SqlDbType.NVarChar);
                        command.Parameters.Add("@TotalWeight", SqlDbType.Decimal);
                        command.Parameters.Add("@TotalVolume", SqlDbType.Decimal);
                        command.Parameters.Add("@PreAllocatedDriverNumber", SqlDbType.Int);
                        command.Parameters.Add("@Caller", SqlDbType.NVarChar);
                        command.Parameters.Add("@ExtraDelInformation", SqlDbType.NVarChar);
                        command.Parameters.Add("@ExtraPuInformation", SqlDbType.NVarChar);
                        command.Parameters.Add("@ATL", SqlDbType.Bit);
                        command.Parameters.Add("@IsQueued", SqlDbType.Bit);
                        command.Parameters.Add("@UsingComo", SqlDbType.Bit);
                        if (!okToUpload)
                            command.Parameters.Add("@OkToUpload", SqlDbType.Bit);
                        command.Parameters["@LoginId"].Value = Convert.ToString(booking.LoginDetails.Id);
                        command.Parameters["@StateId"].Value = Convert.ToString(booking.StateId);
                        command.Parameters["@AccountCode"].Value = booking.AccountCode;
                        command.Parameters["@ServiceCode"].Value = booking.ServiceCode;
                        command.Parameters["@FromSuburb"].Value = string.IsNullOrEmpty(booking.FromSuburb)
                            ? ""
                            : booking.FromSuburb;
                        command.Parameters["@FromPostcode"].Value = string.IsNullOrEmpty(booking.FromPostcode)
                            ? ""
                            : booking.FromPostcode;
                        command.Parameters["@FromDetail1"].Value = string.IsNullOrEmpty(booking.FromDetail1)
                            ? ""
                            : booking.FromDetail1;
                        command.Parameters["@FromDetail2"].Value = string.IsNullOrEmpty(booking.FromDetail2)
                            ? ""
                            : booking.FromDetail2;
                        command.Parameters["@FromDetail3"].Value = string.IsNullOrEmpty(booking.FromDetail3)
                            ? ""
                            : booking.FromDetail3;
                        command.Parameters["@FromDetail4"].Value = string.IsNullOrEmpty(booking.FromDetail4)
                            ? ""
                            : booking.FromDetail4;
                        command.Parameters["@FromDetail5"].Value = string.IsNullOrEmpty(booking.FromDetail5)
                            ? ""
                            : booking.FromDetail5;

                        command.Parameters["@ToSuburb"].Value = string.IsNullOrEmpty(booking.ToSuburb)
                            ? ""
                            : booking.ToSuburb;
                        command.Parameters["@ToPostcode"].Value = string.IsNullOrEmpty(booking.ToPostcode)
                            ? ""
                            : booking.ToPostcode;
                        command.Parameters["@ToDetail1"].Value = string.IsNullOrEmpty(booking.ToDetail1)
                            ? ""
                            : booking.ToDetail1;
                        command.Parameters["@ToDetail2"].Value = string.IsNullOrEmpty(booking.ToDetail2)
                            ? ""
                            : booking.ToDetail2;
                        command.Parameters["@ToDetail3"].Value = string.IsNullOrEmpty(booking.ToDetail3)
                            ? ""
                            : booking.ToDetail3;
                        command.Parameters["@ToDetail4"].Value = string.IsNullOrEmpty(booking.ToDetail4)
                            ? ""
                            : booking.ToDetail4;
                        command.Parameters["@ToDetail5"].Value = string.IsNullOrEmpty(booking.ToDetail5)
                            ? ""
                            : booking.ToDetail5;
                        command.Parameters["@Ref1"].Value = string.IsNullOrEmpty(booking.Ref1) ? "" : booking.Ref1;
                        command.Parameters["@Ref2"].Value = string.IsNullOrEmpty(booking.Ref2) ? "" : booking.Ref2;
                        //if (booking.DespatchDateTime != null)
                        //    command.Parameters["@DespatchDateTime"].Value = booking.DespatchDateTime;
                        //else
                        //    command.Parameters["@DespatchDateTime"].Value = DBNull.Value;
                        if (booking.DespatchDateTime != DateTime.MinValue)
                            command.Parameters["@DespatchDateTime"].Value = booking.DespatchDateTime;
                        else
                            command.Parameters["@DespatchDateTime"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ConsignmentNumber))
                            command.Parameters["@ConsignmentNumber"].Value = booking.ConsignmentNumber;
                        else
                            command.Parameters["@ConsignmentNumber"].Value = DBNull.Value;

                        if (!string.IsNullOrEmpty(booking.TotalWeight))
                            command.Parameters["@TotalWeight"].Value = booking.TotalWeight;
                        else
                            command.Parameters["@TotalWeight"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.TotalVolume))
                            command.Parameters["@TotalVolume"].Value = booking.TotalVolume;
                        else
                            command.Parameters["@TotalVolume"].Value = DBNull.Value;
                        if (booking.DriverNumber != 0)
                            command.Parameters["@PreAllocatedDriverNumber"].Value = booking.DriverNumber;
                        else
                            command.Parameters["@PreAllocatedDriverNumber"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.Caller))
                            command.Parameters["@Caller"].Value = booking.Caller;
                        else
                            command.Parameters["@Caller"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ExtraDelInformation))
                            command.Parameters["@ExtraDelInformation"].Value = booking.ExtraDelInformation;
                        else
                            command.Parameters["@ExtraDelInformation"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ExtraPuInformation))
                            command.Parameters["@ExtraPuInformation"].Value = booking.ExtraPuInformation;
                        else
                            command.Parameters["@ExtraPuInformation"].Value = DBNull.Value;

                        command.Parameters["@ATL"].Value = booking.ATL;
                        command.Parameters["@IsQueued"].Value = booking.IsQueued;
                        command.Parameters["@UsingComo"].Value = booking.UsingComo;

                        // Staged Booking 
                        if (!okToUpload)
                            command.Parameters["@OkToUpload"].Value = 0;
                        command.CommandText = sql;
                        var id = (int)command.ExecuteScalar();
                        command.Parameters.Clear();

                        //after updating the Booking table ; we also need to update the Items table with any barcodes that are present in the booking
                        if ((booking.lstItems != null) && (booking.lstItems.Count > 0))
                        {
                            var itemSql =
                                "INSERT INTO xCabItems (BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode, Qantity) " +
                                "VALUES (@BookingId,@Description,@Length,@Width,@Height,@Weight,@Cubic,@Barcode,@Quantity)";
                            //setup data types
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@Description", SqlDbType.NVarChar);
                            command.Parameters.Add("@Length", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Width", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Height", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Weight", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Cubic", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Barcode", SqlDbType.NVarChar);
                            command.Parameters.Add("@Quantity", SqlDbType.Int);
                            foreach (var item in booking.lstItems)
                            {
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@Description"].Value = string.IsNullOrEmpty(item.Description)
                                    ? ""
                                    : item.Description.Length > 50 ? item.Description.Substring(0, 50) : item.Description;

                                command.Parameters["@Length"].Precision = 18;
                                command.Parameters["@Length"].Scale = 2;
                                command.Parameters["@Length"].Value = item.Length;

                                command.Parameters["@Width"].Precision = 18;
                                command.Parameters["@Width"].Scale = 2;
                                command.Parameters["@Width"].Value = item.Width;

                                command.Parameters["@Height"].Precision = 18;
                                command.Parameters["@Height"].Scale = 2;
                                command.Parameters["@Height"].Value = item.Height;

                                command.Parameters["@Weight"].Precision = 18;
                                command.Parameters["@Weight"].Scale = 2;
                                command.Parameters["@Weight"].Value = item.Weight;

                                command.Parameters["@Cubic"].Precision = 18;
                                command.Parameters["@Cubic"].Scale = 2;
                                command.Parameters["@Cubic"].Value = item.Cubic;

                                command.Parameters["@Barcode"].Value = string.IsNullOrEmpty(item.Barcode)
                                    ? ""
                                    : item.Barcode;
                                command.Parameters["@Quantity"].Value = item.Quantity;
                                // execute the command
                                command.CommandText = itemSql;
                                command.ExecuteNonQuery();
                            }
                        }

                        //update contact details if any
                        if ((booking.lstContactDetail != null) && (booking.lstContactDetail.Count > 0))
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@AreaCode", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50);
                            foreach (var detail in booking.lstContactDetail)
                            {
                                var detailSql =
                                    "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@AreaCode"].Value = string.IsNullOrEmpty(detail.AreaCode)
                                    ? ""
                                    : detail.AreaCode;
                                command.Parameters["@PhoneNumber"].Value = string.IsNullOrEmpty(detail.PhoneNumber)
                                    ? ""
                                    : detail.PhoneNumber;
                                command.CommandText = detailSql;
                                command.ExecuteNonQuery();
                            }
                        }

                        // Update Trace and Track notification details 
                        var smsNumber = string.Empty;
                        var emailAddress = string.Empty;
                        if (!string.IsNullOrWhiteSpace(booking.TrackAndTraceSmsNumber))
                        {
                            smsNumber = booking.TrackAndTraceSmsNumber.Trim().Replace(" ", "");
                        }
                        if (!string.IsNullOrWhiteSpace(booking.TrackAndTraceEmailAddress))
                        {
                            emailAddress = booking.TrackAndTraceEmailAddress.Trim().Replace(" ", "");
                        }

                        if (smsNumber.Length > 0 || emailAddress.Length > 0)
                        {
                            var insertTraceAndTrack =
                                string.Format(@"INSERT INTO [dbo].[xCabNotifications] ([BookingId],[SmsNumber],[EmailAddress]) 
                                                    VALUES (@BookingId, @SmsNumber, @EmailAddress)");
                            try
                            {
                                connection.Execute(insertTraceAndTrack, new
                                {
                                    BookingId = id,
                                    SmsNumber = smsNumber,
                                    EmailAddress = emailAddress
                                });
                            }
                            catch (Exception e)
                            {
                                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector, error while inserting UNS SMS/Email fields,details",
                                    e.Message,
                                    "RESTful Web Service:DbConnector", SlackChannel.GeneralErrors);
                            }
                        }

                        // Update extra references
                        try
                        {
                            if ((booking.lstExtraFields != null) && (booking.lstExtraFields.Count > 0))
                            {
                                var extraRefSql =
                                    "INSERT INTO Eint.xCabExtraReferences (PrimaryBookingId, Name, Value, UseInUNS) " +
                                    "VALUES (@PrimaryBookingId, @Name, @Value, @UseInUNS)";

                                //setup data types
                                command.Parameters.Add("@PrimaryBookingId", SqlDbType.Int);
                                command.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
                                command.Parameters.Add("@Value", SqlDbType.NVarChar);
                                command.Parameters.Add("@UseInUNS", SqlDbType.Bit);

                                foreach (var extraReferences in booking.lstExtraFields)
                                {
                                    command.Parameters["@PrimaryBookingId"].Value = id;
                                    command.Parameters["@Name"].Value = string.IsNullOrEmpty(extraReferences.Key) ? "" : extraReferences.Key;
                                    command.Parameters["@Value"].Value = string.IsNullOrEmpty(extraReferences.Value) ? "" : extraReferences.Value;
                                    command.Parameters["@UseInUNS"].Value = 0;

                                    // execute the command
                                    command.CommandText = extraRefSql;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector, error while inserting extra references to Eint.xCabExtraReferences. Details : ",
                                        e.Message, "RESTful Web Service:DbConnector", SlackChannel.GeneralErrors);
                        }

                    }
                    bookingStored = true;
                }
                catch (SqlException ex)
                {
                    Logger.Log("Exception Occurred: " + ex.Message, Name());
                    bookingStored = false;
                }
            }

            return bookingStored;
        }

        public bool StoreToDbTempForNHP(Booking booking, bool okToUpload = true)
        {
            var bookingStored = false;
            if (booking == null)
                return false;

            if (!string.IsNullOrWhiteSpace(booking.AccountCode))
                booking.AccountCode = booking.AccountCode.ToUpper();

            //check if Account Code & Service Codes are correctly configured
            if (string.IsNullOrEmpty(booking.AccountCode) || string.IsNullOrEmpty(booking.ServiceCode))
            {
                string errorMsg =
                    "Received file with Missing AccountCode and/or Service Code. As per the schema these fields are mandatory. This file will be moved into the Error_Files Folder.";
                Logger.Log(errorMsg, Name(), true);
                return false;
            }
            if (string.IsNullOrEmpty(booking.FromSuburb) || string.IsNullOrEmpty(booking.FromPostcode))
            {
                var errorMsg =
                    "Received file with Missing From Suburb Names/ Postcodes. As per the schema these fields are mandatory.This file will be moved into the Error_Files Folder.";
                Logger.Log(errorMsg, Name(), true);
                return false;
            }
            if (string.IsNullOrEmpty(booking.ToSuburb) && string.IsNullOrEmpty(booking.ToPostcode))
            {
                var errorMsg =
                    "Received file with Missing To Postcodes and Suburb Names. As per the schema either one of the fields is mandatory. This file will be moved into the Error_Files Folder.";
                Logger.Log(errorMsg, Name(), true);
                return false;
            }

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        var sql = string.Empty;

                        #region Raw text for SQL Inserts
                        // TO DO: okToUpload - false new sql 
                        // Staged booking
                        if (!okToUpload)
                        {
                            sql =
                                "INSERT INTO xCabBooking(LoginId,StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                                "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,DespatchDateTime,ConsignmentNumber,TotalWeight, TotalVolume,PreAllocatedDriverNumber,Caller,ExtraDelInformation,ExtraPuInformation, OkToUpload,IsQueued) OUTPUT INSERTED.BookingId " +
                                " VALUES(@LoginId,@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                                "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@DespatchDateTime,@ConsignmentNumber, @TotalWeight,@TotalVolume,@PreAllocatedDriverNumber,@Caller,@ExtraDelInformation,@ExtraPuInformation, @OkToUpload,@IsQueued)" +
                                "";
                        }

                        else
                        {
                            sql =
                                "INSERT INTO xCabBooking(LoginId,StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                                "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,DespatchDateTime,ConsignmentNumber,TotalWeight, TotalVolume,PreAllocatedDriverNumber,Caller,ExtraDelInformation,ExtraPuInformation,IsQueued) OUTPUT INSERTED.BookingId " +
                                " VALUES(@LoginId,@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                                "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@DespatchDateTime,@ConsignmentNumber, @TotalWeight,@TotalVolume,@PreAllocatedDriverNumber,@Caller,@ExtraDelInformation,@ExtraPuInformation,@IsQueued)" +
                                "";
                        }

                        #endregion

                        command.Parameters.Add("@LoginId", SqlDbType.Int);
                        command.Parameters.Add("@StateId", SqlDbType.Int);
                        command.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@ServiceCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@FromSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@Ref1", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@Ref2", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@DespatchDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@ConsignmentNumber", SqlDbType.NVarChar);
                        command.Parameters.Add("@TotalWeight", SqlDbType.Decimal);
                        command.Parameters.Add("@TotalVolume", SqlDbType.Decimal);
                        command.Parameters.Add("@PreAllocatedDriverNumber", SqlDbType.Int);
                        command.Parameters.Add("@Caller", SqlDbType.NVarChar);
                        command.Parameters.Add("@ExtraDelInformation", SqlDbType.NVarChar);
                        command.Parameters.Add("@ExtraPuInformation", SqlDbType.NVarChar);
                        command.Parameters.Add("@IsQueued", SqlDbType.Bit);
                        if (!okToUpload)
                            command.Parameters.Add("@OkToUpload", SqlDbType.Bit);
                        command.Parameters["@LoginId"].Value = Convert.ToString(booking.LoginDetails.Id);
                        command.Parameters["@StateId"].Value = Convert.ToString(booking.StateId);
                        command.Parameters["@AccountCode"].Value = booking.AccountCode;
                        command.Parameters["@ServiceCode"].Value = booking.ServiceCode;
                        command.Parameters["@FromSuburb"].Value = string.IsNullOrEmpty(booking.FromSuburb)
                            ? ""
                            : booking.FromSuburb;
                        command.Parameters["@FromPostcode"].Value = string.IsNullOrEmpty(booking.FromPostcode)
                            ? ""
                            : booking.FromPostcode;
                        command.Parameters["@FromDetail1"].Value = string.IsNullOrEmpty(booking.FromDetail1)
                            ? ""
                            : booking.FromDetail1;
                        command.Parameters["@FromDetail2"].Value = string.IsNullOrEmpty(booking.FromDetail2)
                            ? ""
                            : booking.FromDetail2;
                        command.Parameters["@FromDetail3"].Value = string.IsNullOrEmpty(booking.FromDetail3)
                            ? ""
                            : booking.FromDetail3;
                        command.Parameters["@FromDetail4"].Value = string.IsNullOrEmpty(booking.FromDetail4)
                            ? ""
                            : booking.FromDetail4;
                        command.Parameters["@FromDetail5"].Value = string.IsNullOrEmpty(booking.FromDetail5)
                            ? ""
                            : booking.FromDetail5;

                        command.Parameters["@ToSuburb"].Value = string.IsNullOrEmpty(booking.ToSuburb)
                            ? ""
                            : booking.ToSuburb;
                        command.Parameters["@ToPostcode"].Value = string.IsNullOrEmpty(booking.ToPostcode)
                            ? ""
                            : booking.ToPostcode;
                        command.Parameters["@ToDetail1"].Value = string.IsNullOrEmpty(booking.ToDetail1)
                            ? ""
                            : booking.ToDetail1;
                        command.Parameters["@ToDetail2"].Value = string.IsNullOrEmpty(booking.ToDetail2)
                            ? ""
                            : booking.ToDetail2;
                        command.Parameters["@ToDetail3"].Value = string.IsNullOrEmpty(booking.ToDetail3)
                            ? ""
                            : booking.ToDetail3;
                        command.Parameters["@ToDetail4"].Value = string.IsNullOrEmpty(booking.ToDetail4)
                            ? ""
                            : booking.ToDetail4;
                        command.Parameters["@ToDetail5"].Value = string.IsNullOrEmpty(booking.ToDetail5)
                            ? ""
                            : booking.ToDetail5;
                        command.Parameters["@Ref1"].Value = string.IsNullOrEmpty(booking.Ref1) ? "" : booking.Ref1;
                        command.Parameters["@Ref2"].Value = string.IsNullOrEmpty(booking.Ref2) ? "" : booking.Ref2;
                        //if (booking.DespatchDateTime != null)
                        //    command.Parameters["@DespatchDateTime"].Value = booking.DespatchDateTime;
                        //else
                        //    command.Parameters["@DespatchDateTime"].Value = DBNull.Value;
                        if (booking.DespatchDateTime != DateTime.MinValue)
                            command.Parameters["@DespatchDateTime"].Value = booking.DespatchDateTime;
                        else
                            command.Parameters["@DespatchDateTime"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ConsignmentNumber))
                            command.Parameters["@ConsignmentNumber"].Value = booking.ConsignmentNumber;
                        else
                            command.Parameters["@ConsignmentNumber"].Value = DBNull.Value;

                        if (!string.IsNullOrEmpty(booking.TotalWeight))
                            command.Parameters["@TotalWeight"].Value = booking.TotalWeight;
                        else
                            command.Parameters["@TotalWeight"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.TotalVolume))
                            command.Parameters["@TotalVolume"].Value = booking.TotalVolume;
                        else
                            command.Parameters["@TotalVolume"].Value = DBNull.Value;
                        if (booking.DriverNumber != 0)
                            command.Parameters["@PreAllocatedDriverNumber"].Value = booking.DriverNumber;
                        else
                            command.Parameters["@PreAllocatedDriverNumber"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.Caller))
                            command.Parameters["@Caller"].Value = booking.Caller;
                        else
                            command.Parameters["@Caller"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ExtraDelInformation))
                            command.Parameters["@ExtraDelInformation"].Value = booking.ExtraDelInformation;
                        else
                            command.Parameters["@ExtraDelInformation"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ExtraPuInformation))
                            command.Parameters["@ExtraPuInformation"].Value = booking.ExtraPuInformation;
                        else
                            command.Parameters["@ExtraPuInformation"].Value = DBNull.Value;
                        // Staged Booking 
                        if (!okToUpload)
                            command.Parameters["@OkToUpload"].Value = 0;
                        command.Parameters["@IsQueued"].Value = booking.IsQueued;
                        command.CommandText = sql;
                        var id = (int)command.ExecuteScalar();
                        command.Parameters.Clear();
                        //after updating the Booking table ; we also need to update the Items table with any barcodes that are present in the booking
                        if ((booking.lstItems != null) && (booking.lstItems.Count > 0))
                        {
                            var itemSql =
                                "INSERT INTO xCabItems (BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode) " +
                                "VALUES (@BookingId,@Description,@Length,@Width,@Height,@Weight,@Cubic,@Barcode)";
                            //setup data types
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@Description", SqlDbType.NVarChar);
                            command.Parameters.Add("@Length", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Width", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Height", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Weight", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Cubic", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Barcode", SqlDbType.NVarChar);
                            foreach (var item in booking.lstItems)
                            {
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@Description"].Value = string.IsNullOrEmpty(item.Description)
                                    ? ""
                                    : item.Description.Length > 50 ? item.Description.Substring(0, 50) : item.Description;

                                command.Parameters["@Length"].Precision = 18;
                                command.Parameters["@Length"].Scale = 2;
                                command.Parameters["@Length"].Value = item.Length;

                                command.Parameters["@Width"].Precision = 18;
                                command.Parameters["@Width"].Scale = 2;
                                command.Parameters["@Width"].Value = item.Width;

                                command.Parameters["@Height"].Precision = 18;
                                command.Parameters["@Height"].Scale = 2;
                                command.Parameters["@Height"].Value = item.Height;

                                command.Parameters["@Weight"].Precision = 18;
                                command.Parameters["@Weight"].Scale = 2;
                                command.Parameters["@Weight"].Value = item.Weight;

                                command.Parameters["@Cubic"].Precision = 18;
                                command.Parameters["@Cubic"].Scale = 2;
                                command.Parameters["@Cubic"].Value = item.Cubic;

                                command.Parameters["@Barcode"].Value = string.IsNullOrEmpty(item.Barcode)
                                    ? ""
                                    : item.Barcode;
                                // execute the command
                                command.CommandText = itemSql;
                                command.ExecuteNonQuery();
                            }
                        }
                        //update contact details if any
                        if ((booking.lstContactDetail != null) && (booking.lstContactDetail.Count > 0))
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@AreaCode", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50);
                            foreach (var detail in booking.lstContactDetail)
                            {
                                var detailSql =
                                    "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@AreaCode"].Value = string.IsNullOrEmpty(detail.AreaCode)
                                    ? ""
                                    : detail.AreaCode;
                                command.Parameters["@PhoneNumber"].Value = string.IsNullOrEmpty(detail.PhoneNumber)
                                    ? ""
                                    : detail.PhoneNumber;
                                command.CommandText = detailSql;
                                command.ExecuteNonQuery();
                            }
                        }

                        // Update Trace and Track notification details 
                        var smsNumber = string.Empty;
                        var emailAddress = string.Empty;
                        if (!string.IsNullOrWhiteSpace(booking.TrackAndTraceSmsNumber))
                        {
                            smsNumber = booking.TrackAndTraceSmsNumber.Trim().Replace(" ", "");
                        }
                        if (!string.IsNullOrWhiteSpace(booking.TrackAndTraceEmailAddress))
                        {
                            emailAddress = booking.TrackAndTraceEmailAddress.Trim().Replace(" ", "");
                        }

                        if (smsNumber.Length > 0 || emailAddress.Length > 0)
                        {
                            var insertTraceAndTrack =
                                string.Format(@"INSERT INTO [dbo].[xCabNotifications] ([BookingId],[SmsNumber],[EmailAddress]) 
                                                    VALUES (@BookingId, @SmsNumber, @EmailAddress)");
                            try
                            {
                                connection.Execute(insertTraceAndTrack, new
                                {
                                    BookingId = id,
                                    SmsNumber = smsNumber,
                                    EmailAddress = emailAddress
                                });
                            }
                            catch (Exception e)
                            {
                                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector, error while inserting UNS SMS/Email fields,details",
                                    e.Message,
                                    "RESTful Web Service:DbConnector", SlackChannel.GeneralErrors);
                            }
                        }
                    }
                    bookingStored = true;
                }
                catch (SqlException ex)
                {
                    Logger.Log("Exception Occurred: " + ex.Message, Name());
                    bookingStored = false;
                }
            }

            return bookingStored;
        }
        public bool StoreToDbFromRestfulWs(Booking booking)
        {
            var bookingStored = false;
            if (booking == null)
                return false;

            if (!string.IsNullOrWhiteSpace(booking.AccountCode))
                booking.AccountCode = booking.AccountCode.ToUpper();

            //check if Account Code & Service Codes are correctly configured
            if (string.IsNullOrEmpty(booking.AccountCode) || string.IsNullOrEmpty(booking.ServiceCode))
            {
                string errorMsg = "Received file with Missing AccountCode and/or Service Code. As per the schema these fields are mandatory. This file will be moved into the Error_Files Folder. Booking Information:" + booking;
                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector",
   errorMsg,
    "RESTful Web Service:DbConnector", SlackChannel.GeneralErrors);
                return false;
            }
            if (string.IsNullOrEmpty(booking.FromSuburb) || string.IsNullOrEmpty(booking.ToSuburb))
            {
                var errorMsg =
                    "Received file with Missing From/To Suburb Names. As per the schema these fields are mandatory.This file will be moved into the Error_Files Folder. Booking Information:" + booking;
                // Logger.Log(errorMsg, Name(), true);
                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector",
errorMsg,
"RESTful Web Service:DbConnector", SlackChannel.GeneralErrors);
                return false;
            }
            if (string.IsNullOrEmpty(booking.FromPostcode) || string.IsNullOrEmpty(booking.ToPostcode))
            {
                var errorMsg =
                    "Received file with Missing From/To Postcodes. As per the schema these fields are mandatory. This file will be moved into the Error_Files Folder. Booking Information:" + booking;
                // Logger.Log(errorMsg, Name(), true);
                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector",
errorMsg,
"RESTful Web Service:DbConnector", SlackChannel.GeneralErrors);
                return false;
            }

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        var sql = string.Empty;

                        #region Raw text for SQL Inserts

                        sql =
                            "INSERT INTO xCabBooking(TPLUS_JobNumber,UploadedToTplus,UploadDateTime,LoginId,StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                            "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,DespatchDateTime,AdvanceDateTime,ConsignmentNumber,TotalWeight, TotalVolume,PreAllocatedDriverNumber,Caller,ExtraDelInformation,ExtraPuInformation,IsQueued, OkToUpload, ATL, UsingComo, ComoJobId) OUTPUT INSERTED.BookingId " +
                            " VALUES(@TPLUS_JobNumber,@UploadedToTplus,@UploadDateTime,@LoginId,@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                            "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@DespatchDateTime,@AdvanceDateTime,@ConsignmentNumber, @TotalWeight,@TotalVolume,@PreAllocatedDriverNumber,@Caller,@ExtraDelInformation, @ExtraPuInformation,@IsQueued, @OkToUpload, @ATL, @UsingComo, @ComoJobId)" +
                            "";

                        #endregion
                        command.Parameters.Add("@TPLUS_JobNumber", SqlDbType.Int);
                        command.Parameters.Add("@UploadedToTplus", SqlDbType.Bit);
                        command.Parameters.Add("@UploadDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@LoginId", SqlDbType.Int);
                        command.Parameters.Add("@StateId", SqlDbType.Int);
                        command.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@ServiceCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@FromSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@Ref1", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@Ref2", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@DespatchDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@AdvanceDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@ConsignmentNumber", SqlDbType.NVarChar);
                        command.Parameters.Add("@TotalWeight", SqlDbType.Decimal);
                        command.Parameters.Add("@TotalVolume", SqlDbType.Decimal);
                        command.Parameters.Add("@PreAllocatedDriverNumber", SqlDbType.Int);
                        command.Parameters.Add("@Caller", SqlDbType.NVarChar);
                        command.Parameters.Add("@ExtraDelInformation", SqlDbType.NVarChar);
                        command.Parameters.Add("@ExtraPuInformation", SqlDbType.NVarChar);
                        command.Parameters.Add("@IsQueued", SqlDbType.Bit);
                        command.Parameters.Add("@OkToUpload", SqlDbType.Bit);
                        command.Parameters.Add("@ATL", SqlDbType.Bit);
                        command.Parameters.Add("@UsingComo", SqlDbType.Bit);
                        command.Parameters.Add("@ComoJobId", SqlDbType.NVarChar);
                        //command.Parameters["@TPLUS_JobNumber"].Value = booking.TPLUS_JobNumber;
                        if (!string.IsNullOrEmpty(booking.TPLUS_JobNumber))
                            command.Parameters["@TPLUS_JobNumber"].Value = booking.TPLUS_JobNumber;
                        else
                            command.Parameters["@TPLUS_JobNumber"].Value = DBNull.Value;
                        command.Parameters["@UploadedToTplus"].Value = booking.UploadedToTplus;
                        // UploadDateTime should be null for Staged booking
                        if (booking.UploadDateTime != DateTime.MinValue)
                            command.Parameters["@UploadDateTime"].Value = booking.UploadDateTime;
                        else
                            command.Parameters["@UploadDateTime"].Value = DBNull.Value;

                        command.Parameters["@LoginId"].Value = Convert.ToString(booking.LoginDetails.Id);
                        command.Parameters["@StateId"].Value = Convert.ToString(booking.StateId);
                        command.Parameters["@AccountCode"].Value = booking.AccountCode.ToUpper();
                        command.Parameters["@ServiceCode"].Value = booking.ServiceCode.ToUpper();
                        command.Parameters["@FromSuburb"].Value = string.IsNullOrEmpty(booking.FromSuburb.ToUpper())
                            ? ""
                            : booking.FromSuburb;
                        command.Parameters["@FromPostcode"].Value = string.IsNullOrEmpty(booking.FromPostcode)
                            ? ""
                            : booking.FromPostcode;
                        command.Parameters["@FromDetail1"].Value = string.IsNullOrEmpty(booking.FromDetail1)
                            ? ""
                            : booking.FromDetail1;
                        command.Parameters["@FromDetail2"].Value = string.IsNullOrEmpty(booking.FromDetail2)
                            ? ""
                            : booking.FromDetail2;
                        command.Parameters["@FromDetail3"].Value = string.IsNullOrEmpty(booking.FromDetail3)
                            ? ""
                            : booking.FromDetail3;
                        command.Parameters["@FromDetail4"].Value = string.IsNullOrEmpty(booking.FromDetail4)
                            ? ""
                            : booking.FromDetail4;
                        command.Parameters["@FromDetail5"].Value = string.IsNullOrEmpty(booking.FromDetail5)
                            ? ""
                            : booking.FromDetail5;

                        command.Parameters["@ToSuburb"].Value = string.IsNullOrEmpty(booking.ToSuburb.ToUpper())
                            ? ""
                            : booking.ToSuburb;
                        command.Parameters["@ToPostcode"].Value = string.IsNullOrEmpty(booking.ToPostcode)
                            ? ""
                            : booking.ToPostcode;
                        command.Parameters["@ToDetail1"].Value = string.IsNullOrEmpty(booking.ToDetail1)
                            ? ""
                            : booking.ToDetail1;
                        command.Parameters["@ToDetail2"].Value = string.IsNullOrEmpty(booking.ToDetail2)
                            ? ""
                            : booking.ToDetail2;
                        command.Parameters["@ToDetail3"].Value = string.IsNullOrEmpty(booking.ToDetail3)
                            ? ""
                            : booking.ToDetail3;
                        command.Parameters["@ToDetail4"].Value = string.IsNullOrEmpty(booking.ToDetail4)
                            ? ""
                            : booking.ToDetail4;
                        command.Parameters["@ToDetail5"].Value = string.IsNullOrEmpty(booking.ToDetail5)
                            ? ""
                            : booking.ToDetail5;
                        command.Parameters["@Ref1"].Value = string.IsNullOrEmpty(booking.Ref1) ? "" : booking.Ref1;
                        command.Parameters["@Ref2"].Value = string.IsNullOrEmpty(booking.Ref2) ? "" : booking.Ref2;
                        if (booking.AdvanceDateTime == DateTime.MinValue)
                            command.Parameters["@AdvanceDateTime"].Value = DBNull.Value;
                        else
                            command.Parameters["@AdvanceDateTime"].Value = booking.AdvanceDateTime;
                        if (booking.DespatchDateTime == DateTime.MinValue)
                            command.Parameters["@DespatchDateTime"].Value = DBNull.Value;
                        else
                            command.Parameters["@DespatchDateTime"].Value = booking.DespatchDateTime;
                        if (!string.IsNullOrEmpty(booking.ConsignmentNumber))
                            command.Parameters["@ConsignmentNumber"].Value = booking.ConsignmentNumber;
                        else
                            command.Parameters["@ConsignmentNumber"].Value = DBNull.Value;

                        if (!string.IsNullOrEmpty(booking.TotalWeight))
                            command.Parameters["@TotalWeight"].Value = booking.TotalWeight;
                        else
                            command.Parameters["@TotalWeight"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.TotalVolume))
                            command.Parameters["@TotalVolume"].Value = booking.TotalVolume;
                        else
                            command.Parameters["@TotalVolume"].Value = DBNull.Value;
                        if (booking.DriverNumber != 0)
                            command.Parameters["@PreAllocatedDriverNumber"].Value = booking.DriverNumber;
                        else
                            command.Parameters["@PreAllocatedDriverNumber"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.Caller))
                            command.Parameters["@Caller"].Value = booking.Caller;
                        else
                            command.Parameters["@Caller"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ExtraDelInformation))
                            command.Parameters["@ExtraDelInformation"].Value = booking.ExtraDelInformation;
                        else
                            command.Parameters["@ExtraDelInformation"].Value = DBNull.Value;

                        if (!string.IsNullOrEmpty(booking.ExtraPuInformation))
                            command.Parameters["@ExtraPuInformation"].Value = booking.ExtraPuInformation;
                        else
                            command.Parameters["@ExtraPuInformation"].Value = DBNull.Value;
                        if (booking.IsQueued)
                        {
                            command.Parameters["@IsQueued"].Value = 1;
                        }
                        else
                        {
                            command.Parameters["@IsQueued"].Value = 0;
                        }
                        command.Parameters["@OkToUpload"].Value = booking.OkToUpload;
                        command.Parameters["@ATL"].Value = booking.ATL;
                        command.Parameters["@UsingComo"].Value = booking.UsingComo;
                        command.Parameters["@ComoJobId"].Value = booking.ComoJobId;


                        command.CommandText = sql;
                        var id = (int)command.ExecuteScalar();
                        command.Parameters.Clear();
                        //after updating the Booking table ; we also need to update the Items table with any barcodes that are present in the booking
                        if ((booking.lstItems != null) && (booking.lstItems.Count > 0))
                        {
                            var itemSql =
                                "INSERT INTO xCabItems (BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode,Qantity) " +
                                "VALUES (@BookingId,@Description,@Length,@Width,@Height,@Weight,@Cubic,@Barcode,@Qantity)";
                            //setup data types
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@Description", SqlDbType.NVarChar);
                            command.Parameters.Add("@Length", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Width", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Height", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Weight", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Cubic", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Barcode", SqlDbType.NVarChar);
                            command.Parameters.Add("@Qantity", SqlDbType.Int);
                            foreach (var item in booking.lstItems)
                            {
                                if (!string.IsNullOrEmpty(item.Description) && item.Description.Length > _maxItemDescriptionLength)
                                {
                                    item.Description = item.Description.Substring(0, _maxItemDescriptionLength);
                                }

                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@Description"].Value = string.IsNullOrEmpty(item.Description)
                                    ? ""
                                    : item.Description;

                                command.Parameters["@Length"].Precision = 18;
                                command.Parameters["@Length"].Scale = 2;
                                command.Parameters["@Length"].Value = item.Length;

                                command.Parameters["@Width"].Precision = 18;
                                command.Parameters["@Width"].Scale = 2;
                                command.Parameters["@Width"].Value = item.Width;

                                command.Parameters["@Height"].Precision = 18;
                                command.Parameters["@Height"].Scale = 2;
                                command.Parameters["@Height"].Value = item.Height;

                                command.Parameters["@Weight"].Precision = 18;
                                command.Parameters["@Weight"].Scale = 2;
                                command.Parameters["@Weight"].Value = item.Weight;

                                command.Parameters["@Cubic"].Precision = 18;
                                command.Parameters["@Cubic"].Scale = 2;
                                command.Parameters["@Cubic"].Value = item.Cubic;

                                command.Parameters["@Barcode"].Value = string.IsNullOrEmpty(item.Barcode)
                                    ? ""
                                    : item.Barcode;
                                if (item.Quantity != 0)
                                {
                                    command.Parameters["@Qantity"].Value = item.Quantity;
                                }
                                else
                                {
                                    command.Parameters["@Qantity"].Value = DBNull.Value;
                                }
                                // execute the command
                                command.CommandText = itemSql;
                                command.ExecuteNonQuery();
                                // Logger.Log("Executed SQL: " + itemSql, Name());
                            }
                        }
                        //update contact details if any
                        if ((booking.lstContactDetail != null) && (booking.lstContactDetail.Count > 0))
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@AreaCode", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50);
                            foreach (var detail in booking.lstContactDetail)
                            {
                                var detailSql =
                                    "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@AreaCode"].Value = string.IsNullOrEmpty(detail.AreaCode)
                                    ? ""
                                    : detail.AreaCode;
                                command.Parameters["@PhoneNumber"].Value = string.IsNullOrEmpty(detail.PhoneNumber)
                                    ? ""
                                    : detail.PhoneNumber;
                                command.CommandText = detailSql;
                                command.ExecuteNonQuery();
                                // Logger.Log("Executed SQL: " + detailSql, Name());
                            }
                        }
                        // Update Trace and Track notification details 
                        var smsNumber = string.Empty;
                        var emailAddress = string.Empty;
                        if (!string.IsNullOrWhiteSpace(booking.TrackAndTraceSmsNumber))
                        {
                            smsNumber = booking.TrackAndTraceSmsNumber.Trim().Replace(" ", "");
                        }
                        if (!string.IsNullOrWhiteSpace(booking.TrackAndTraceEmailAddress))
                        {
                            emailAddress = booking.TrackAndTraceEmailAddress.Trim().Replace(" ", "");
                        }

                        if (smsNumber.Length > 0 || emailAddress.Length > 0)
                        {
                            var insertTraceAndTrack =
                                string.Format(@"INSERT INTO [dbo].[xCabNotifications] ([BookingId],[SmsNumber],[EmailAddress]) 
                                                    VALUES (@BookingId, @SmsNumber, @EmailAddress)");
                            try
                            {
                                connection.Execute(insertTraceAndTrack, new
                                {
                                    BookingId = id,
                                    SmsNumber = smsNumber,
                                    EmailAddress = emailAddress
                                });
                            }
                            catch (Exception e)
                            {
                                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector, error while inserting UNS SMS/Email fields,details",
                                    e.Message,
                                    "RESTful Web Service:DbConnector", SlackChannel.GeneralErrors);
                            }
                        }

                        // Update XCabTimeSlots if any
                        if (booking.PickUpTimeSlot != null || booking.DeliveryTimeSlot != null)
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@ClientRequiredPickupTime", SqlDbType.DateTime);
                            command.Parameters.Add("@ClientRequiredDeliveryTime", SqlDbType.DateTime);

                            var timeslotSql = "INSERT INTO xCabTimeSlots (BookingId, ClientRequiredPickupTime, ClientRequiredDeliveryTime) values (@BookingId, @ClientRequiredPickupTime, @ClientRequiredDeliveryTime)";
                            command.Parameters["@BookingId"].Value = id;
                            if (booking.PickUpTimeSlot != null && booking.PickUpTimeSlot.ClientRequiredPickupTime > DateTime.MinValue)
                                command.Parameters["@ClientRequiredPickupTime"].Value = booking.PickUpTimeSlot.ClientRequiredPickupTime;
                            else
                                command.Parameters["@ClientRequiredPickupTime"].Value = DBNull.Value;

                            if (booking.DeliveryTimeSlot != null && booking.DeliveryTimeSlot.ClientRequiredDeliveryTime > DateTime.MinValue)
                                command.Parameters["@ClientRequiredDeliveryTime"].Value = booking.DeliveryTimeSlot.ClientRequiredDeliveryTime;
                            else
                                command.Parameters["@ClientRequiredDeliveryTime"].Value = DBNull.Value;

                            command.CommandText = timeslotSql;
                            command.ExecuteNonQuery();
                        }
                        //update remarks if any
                        if (booking.Remarks != null && booking.Remarks.Count > 0)
                        {
                            try
                            {
                                foreach (var remark in booking.Remarks)
                                {
                                    var insertRemarksQuery = "INSERT INTO [dbo].[xCabRemarks] (BookingId, Remarks) VALUES (@BookingId,@Remarks)";
                                    connection.Execute(insertRemarksQuery, new
                                    {
                                        BookingId = id,
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
                        //update sundries if any - use dapper instead of direct SQL
                        if (booking.Sundries != null && booking.Sundries.Any())
                        {
                            try
                            {
                                var xCabSundryRepository = new XCabSundryRepository();
                                var xCabSundry = ConversionUtils.GetXCabSundry(booking.Sundries, id);
                                if (xCabSundry != null)
                                    xCabSundryRepository.Insert(xCabSundry);
                            }
                            catch (Exception ex)
                            {
                                Logger.Log(
                                    "Exception Occurred in XCabBookingRepository: InsertBooking while adding sundries to xCabSundry table, message: " +
                                    ex.Message, "XCabBookingRepository");
                            }
                        }


                    }
                    bookingStored = true;
                }
                catch (SqlException ex)
                {
                    Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector",
ex.Message,
"RESTful Web Service:DbConnector", SlackChannel.GeneralErrors);
                    //Logger.Log("Exception Occurred: " + ex.Message, Name());
                    bookingStored = false;
                }
            }

            return bookingStored;
        }
        public bool StoreToSecondaryDbFromRestfulWs(Booking booking)
        {
            var bookingStored = false;
            if (booking == null)
                return false;

            if (!string.IsNullOrWhiteSpace(booking.AccountCode))
                booking.AccountCode = booking.AccountCode.ToUpper();

            //check if Account Code & Service Codes are correctly configured
            if (string.IsNullOrEmpty(booking.AccountCode) || string.IsNullOrEmpty(booking.ServiceCode))
            {
                string errorMsg = "Received file with Missing AccountCode and/or Service Code. As per the schema these fields are mandatory. This file will be moved into the Error_Files Folder. Booking Information:" + booking;
                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector(Test)",
   errorMsg,
    "RESTful Web Service:DbConnector(Test)", SlackChannel.GeneralErrors);
                return false;
            }
            if (string.IsNullOrEmpty(booking.FromSuburb) || string.IsNullOrEmpty(booking.ToSuburb))
            {
                var errorMsg =
                    "Received file with Missing From/To Suburb Names. As per the schema these fields are mandatory.This file will be moved into the Error_Files Folder. Booking Information:" + booking;
                // Logger.Log(errorMsg, Name(), true);
                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector(Test)",
errorMsg,
"RESTful Web Service:DbConnector(Test)", SlackChannel.GeneralErrors);
                return false;
            }
            if (string.IsNullOrEmpty(booking.FromPostcode) || string.IsNullOrEmpty(booking.ToPostcode))
            {
                var errorMsg =
                    "Received file with Missing From/To Postcodes. As per the schema these fields are mandatory. This file will be moved into the Error_Files Folder. Booking Information:" + booking;
                // Logger.Log(errorMsg, Name(), true);
                Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector(Test)",
errorMsg,
"RESTful Web Service:DbConnector(Test)", SlackChannel.GeneralErrors);
                return false;
            }

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        var sql = string.Empty;

                        #region Raw text for SQL Inserts

                        sql =
                            "INSERT INTO tst.xCabBooking(TPLUS_JobNumber,UploadedToTplus,UploadDateTime,LoginId,StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                            "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,DespatchDateTime,ConsignmentNumber,TotalWeight, TotalVolume,PreAllocatedDriverNumber,Caller,ExtraDelInformation,ExtraPuInformation,IsQueued, OkToUpload) OUTPUT INSERTED.BookingId " +
                            " VALUES(@TPLUS_JobNumber,@UploadedToTplus,@UploadDateTime,@LoginId,@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                            "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@DespatchDateTime,@ConsignmentNumber, @TotalWeight,@TotalVolume,@PreAllocatedDriverNumber,@Caller,@ExtraDelInformation, @ExtraPuInformation,@IsQueued, @OkToUpload)" +
                            "";

                        #endregion
                        command.Parameters.Add("@TPLUS_JobNumber", SqlDbType.Int);
                        command.Parameters.Add("@UploadedToTplus", SqlDbType.Bit);
                        command.Parameters.Add("@UploadDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@LoginId", SqlDbType.Int);
                        command.Parameters.Add("@StateId", SqlDbType.Int);
                        command.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@ServiceCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@FromSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@Ref1", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@Ref2", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@DespatchDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@ConsignmentNumber", SqlDbType.NVarChar);
                        command.Parameters.Add("@TotalWeight", SqlDbType.Decimal);
                        command.Parameters.Add("@TotalVolume", SqlDbType.Decimal);
                        command.Parameters.Add("@PreAllocatedDriverNumber", SqlDbType.Int);
                        command.Parameters.Add("@Caller", SqlDbType.NVarChar);
                        command.Parameters.Add("@ExtraDelInformation", SqlDbType.NVarChar);
                        command.Parameters.Add("@ExtraPuInformation", SqlDbType.NVarChar);
                        command.Parameters.Add("@IsQueued", SqlDbType.Bit);
                        command.Parameters.Add("@OkToUpload", SqlDbType.Bit);
                        //command.Parameters["@TPLUS_JobNumber"].Value = booking.TPLUS_JobNumber;
                        if (!string.IsNullOrEmpty(booking.TPLUS_JobNumber))
                            command.Parameters["@TPLUS_JobNumber"].Value = booking.TPLUS_JobNumber;
                        else
                            command.Parameters["@TPLUS_JobNumber"].Value = DBNull.Value;
                        command.Parameters["@UploadedToTplus"].Value = booking.UploadedToTplus;

                        // UploadDateTime should be null for Staged booking
                        if (booking.UploadDateTime != DateTime.MinValue)
                            command.Parameters["@UploadDateTime"].Value = booking.UploadDateTime;
                        else
                            command.Parameters["@UploadDateTime"].Value = DBNull.Value;

                        command.Parameters["@LoginId"].Value = Convert.ToString(booking.LoginDetails.Id);
                        command.Parameters["@StateId"].Value = Convert.ToString(booking.StateId);
                        command.Parameters["@AccountCode"].Value = booking.AccountCode.ToUpper();
                        command.Parameters["@ServiceCode"].Value = booking.ServiceCode.ToUpper();
                        command.Parameters["@FromSuburb"].Value = string.IsNullOrEmpty(booking.FromSuburb.ToUpper())
                            ? ""
                            : booking.FromSuburb;
                        command.Parameters["@FromPostcode"].Value = string.IsNullOrEmpty(booking.FromPostcode)
                            ? ""
                            : booking.FromPostcode;
                        command.Parameters["@FromDetail1"].Value = string.IsNullOrEmpty(booking.FromDetail1)
                            ? ""
                            : booking.FromDetail1;
                        command.Parameters["@FromDetail2"].Value = string.IsNullOrEmpty(booking.FromDetail2)
                            ? ""
                            : booking.FromDetail2;
                        command.Parameters["@FromDetail3"].Value = string.IsNullOrEmpty(booking.FromDetail3)
                            ? ""
                            : booking.FromDetail3;
                        command.Parameters["@FromDetail4"].Value = string.IsNullOrEmpty(booking.FromDetail4)
                            ? ""
                            : booking.FromDetail4;
                        command.Parameters["@FromDetail5"].Value = string.IsNullOrEmpty(booking.FromDetail5)
                            ? ""
                            : booking.FromDetail5;

                        command.Parameters["@ToSuburb"].Value = string.IsNullOrEmpty(booking.ToSuburb.ToUpper())
                            ? ""
                            : booking.ToSuburb;
                        command.Parameters["@ToPostcode"].Value = string.IsNullOrEmpty(booking.ToPostcode)
                            ? ""
                            : booking.ToPostcode;
                        command.Parameters["@ToDetail1"].Value = string.IsNullOrEmpty(booking.ToDetail1)
                            ? ""
                            : booking.ToDetail1;
                        command.Parameters["@ToDetail2"].Value = string.IsNullOrEmpty(booking.ToDetail2)
                            ? ""
                            : booking.ToDetail2;
                        command.Parameters["@ToDetail3"].Value = string.IsNullOrEmpty(booking.ToDetail3)
                            ? ""
                            : booking.ToDetail3;
                        command.Parameters["@ToDetail4"].Value = string.IsNullOrEmpty(booking.ToDetail4)
                            ? ""
                            : booking.ToDetail4;
                        command.Parameters["@ToDetail5"].Value = string.IsNullOrEmpty(booking.ToDetail5)
                            ? ""
                            : booking.ToDetail5;
                        command.Parameters["@Ref1"].Value = string.IsNullOrEmpty(booking.Ref1) ? "" : booking.Ref1;
                        command.Parameters["@Ref2"].Value = string.IsNullOrEmpty(booking.Ref2) ? "" : booking.Ref2;
                        if (booking.DespatchDateTime != null)
                            command.Parameters["@DespatchDateTime"].Value = booking.DespatchDateTime;
                        else
                            command.Parameters["@DespatchDateTime"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ConsignmentNumber))
                            command.Parameters["@ConsignmentNumber"].Value = booking.ConsignmentNumber;
                        else
                            command.Parameters["@ConsignmentNumber"].Value = DBNull.Value;

                        if (!string.IsNullOrEmpty(booking.TotalWeight))
                            command.Parameters["@TotalWeight"].Value = booking.TotalWeight;
                        else
                            command.Parameters["@TotalWeight"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.TotalVolume))
                            command.Parameters["@TotalVolume"].Value = booking.TotalVolume;
                        else
                            command.Parameters["@TotalVolume"].Value = DBNull.Value;
                        if (booking.DriverNumber != 0)
                            command.Parameters["@PreAllocatedDriverNumber"].Value = booking.DriverNumber;
                        else
                            command.Parameters["@PreAllocatedDriverNumber"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.Caller))
                            command.Parameters["@Caller"].Value = booking.Caller;
                        else
                            command.Parameters["@Caller"].Value = DBNull.Value;
                        if (!string.IsNullOrEmpty(booking.ExtraDelInformation))
                            command.Parameters["@ExtraDelInformation"].Value = booking.ExtraDelInformation;
                        else
                            command.Parameters["@ExtraDelInformation"].Value = DBNull.Value;

                        if (!string.IsNullOrEmpty(booking.ExtraPuInformation))
                            command.Parameters["@ExtraPuInformation"].Value = booking.ExtraPuInformation;
                        else
                            command.Parameters["@ExtraPuInformation"].Value = DBNull.Value;
                        if (booking.IsQueued)
                        {
                            command.Parameters["@IsQueued"].Value = 1;
                        }
                        else
                        {
                            command.Parameters["@IsQueued"].Value = 0;
                        }

                        command.Parameters["@OkToUpload"].Value = booking.OkToUpload;

                        command.CommandText = sql;
                        var id = (int)command.ExecuteScalar();
                        command.Parameters.Clear();
                        //after updating the Booking table ; we also need to update the Items table with any barcodes that are present in the booking
                        if ((booking.lstItems != null) && (booking.lstItems.Count > 0))
                        {
                            var itemSql =
                                "INSERT INTO tst.xCabItems (BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode,Qantity) " +
                                "VALUES (@BookingId,@Description,@Length,@Width,@Height,@Weight,@Cubic,@Barcode,@Qantity)";
                            //setup data types
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@Description", SqlDbType.NVarChar);
                            command.Parameters.Add("@Length", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Width", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Height", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Weight", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Cubic", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Barcode", SqlDbType.NVarChar);
                            command.Parameters.Add("@Qantity", SqlDbType.Int);
                            foreach (var item in booking.lstItems)
                            {
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@Description"].Value = string.IsNullOrEmpty(item.Description)
                                    ? ""
                                    : item.Description;

                                command.Parameters["@Length"].Precision = 18;
                                command.Parameters["@Length"].Scale = 2;
                                command.Parameters["@Length"].Value = item.Length;

                                command.Parameters["@Width"].Precision = 18;
                                command.Parameters["@Width"].Scale = 2;
                                command.Parameters["@Width"].Value = item.Width;

                                command.Parameters["@Height"].Precision = 18;
                                command.Parameters["@Height"].Scale = 2;
                                command.Parameters["@Height"].Value = item.Height;

                                command.Parameters["@Weight"].Precision = 18;
                                command.Parameters["@Weight"].Scale = 2;
                                command.Parameters["@Weight"].Value = item.Weight;

                                command.Parameters["@Cubic"].Precision = 18;
                                command.Parameters["@Cubic"].Scale = 2;
                                command.Parameters["@Cubic"].Value = item.Cubic;

                                command.Parameters["@Barcode"].Value = string.IsNullOrEmpty(item.Barcode)
                                    ? ""
                                    : item.Barcode;
                                if (item.Quantity != 0)
                                {
                                    command.Parameters["@Qantity"].Value = item.Quantity;
                                }
                                else
                                {
                                    command.Parameters["@Qantity"].Value = DBNull.Value;
                                }
                                // execute the command
                                command.CommandText = itemSql;
                                command.ExecuteNonQuery();
                                // Logger.Log("Executed SQL: " + itemSql, Name());
                            }
                        }
                        //update contact details if any
                        if ((booking.lstContactDetail != null) && (booking.lstContactDetail.Count > 0))
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@AreaCode", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50);
                            foreach (var detail in booking.lstContactDetail)
                            {
                                var detailSql =
                                    "INSERT INTO tst.xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@AreaCode"].Value = string.IsNullOrEmpty(detail.AreaCode)
                                    ? ""
                                    : detail.AreaCode;
                                command.Parameters["@PhoneNumber"].Value = string.IsNullOrEmpty(detail.PhoneNumber)
                                    ? ""
                                    : detail.PhoneNumber;
                                command.CommandText = detailSql;
                                command.ExecuteNonQuery();
                                // Logger.Log("Executed SQL: " + detailSql, Name());
                            }
                        }
                        // Update Trace and Track notification details 
                        if (!string.IsNullOrWhiteSpace(booking.TrackAndTraceSmsNumber))
                        {
                            var insertTraceAndTrack =
                                string.Format(@"INSERT INTO [dbo].[tst.xCabNotifications] ([BookingId],[SmsNumber],[EmailAddress]) 
                                                    VALUES (@BookingId, @SmsNumber, @EmailAddress)");

                            connection.Execute(insertTraceAndTrack, new
                            {
                                BookingId = id,
                                SmsNumber = booking.TrackAndTraceSmsNumber.Trim().Replace(" ", ""),
                                EmailAddress = string.Empty
                            });
                        }
                    }
                    bookingStored = true;
                }
                catch (SqlException ex)
                {
                    Logger.LogSlackErrorFromApp("RESTful Web Service:DbConnector",
ex.Message,
"RESTful Web Service:DbConnector(Test)", SlackChannel.GeneralErrors);
                    //Logger.Log("Exception Occurred: " + ex.Message, Name());
                    bookingStored = false;
                }
            }

            return bookingStored;
        }
        public bool StoreToDb(ModifiedBooking booking)
        {
            var bookingStored = false;

            if (booking == null)
                return false;

            if (!string.IsNullOrWhiteSpace(booking.AccountCode))
                booking.AccountCode = booking.AccountCode.ToUpper();

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        var sql = string.Empty;

                        #region Raw text for SQL Inserts

                        sql =
                            "INSERT INTO xCabBooking(LoginId,StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                            "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,TPLUS_JobNumber,UploadedToTplus,UploadDateTime,ConsignmentNumber) OUTPUT INSERTED.BookingId " +
                            " VALUES(@LoginId,@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                            "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@TPLUS_JobNumber,@UploadedToTplus,@UploadDateTime,@ConsignmentNumber)" +
                            "";

                        #endregion

                        #region initialize parameters
                        command.Parameters.Add("@LoginId", SqlDbType.Int);
                        command.Parameters.Add("@StateId", SqlDbType.Int);
                        command.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@ServiceCode", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@FromSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@FromDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToSuburb", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToPostcode", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail1", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail2", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail3", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail4", SqlDbType.NVarChar);
                        command.Parameters.Add("@ToDetail5", SqlDbType.NVarChar);
                        command.Parameters.Add("@Ref1", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@Ref2", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@TPLUS_JobNumber", SqlDbType.NVarChar, 50);
                        command.Parameters.Add("@UploadedToTplus", SqlDbType.Bit);
                        command.Parameters.Add("@UploadDateTime", SqlDbType.DateTime);
                        command.Parameters.Add("@ConsignmentNumber", SqlDbType.NVarChar, 25);
                        #endregion

                        command.Parameters["@LoginId"].Value = Convert.ToString(booking.LoginDetails.Id);
                        command.Parameters["@StateId"].Value = Convert.ToString(booking.StateId);
                        command.Parameters["@AccountCode"].Value = booking.AccountCode;
                        command.Parameters["@ServiceCode"].Value = booking.ServiceCode;
                        command.Parameters["@FromSuburb"].Value = string.IsNullOrEmpty(booking.FromSuburb)
                            ? ""
                            : booking.FromSuburb;
                        command.Parameters["@FromPostcode"].Value = string.IsNullOrEmpty(booking.FromPostcode)
                            ? ""
                            : booking.FromPostcode;
                        command.Parameters["@FromDetail1"].Value = string.IsNullOrEmpty(booking.FromDetail1)
                            ? ""
                            : booking.FromDetail1;
                        command.Parameters["@FromDetail2"].Value = string.IsNullOrEmpty(booking.FromDetail2)
                            ? ""
                            : booking.FromDetail2;
                        command.Parameters["@FromDetail3"].Value = string.IsNullOrEmpty(booking.FromDetail3)
                            ? ""
                            : booking.FromDetail3;
                        command.Parameters["@FromDetail4"].Value = string.IsNullOrEmpty(booking.FromDetail4)
                            ? ""
                            : booking.FromDetail4;
                        command.Parameters["@FromDetail5"].Value = string.IsNullOrEmpty(booking.FromDetail5)
                            ? ""
                            : booking.FromDetail5;

                        command.Parameters["@ToSuburb"].Value = string.IsNullOrEmpty(booking.ToSuburb)
                            ? ""
                            : booking.ToSuburb;
                        command.Parameters["@ToPostcode"].Value = string.IsNullOrEmpty(booking.ToPostcode)
                            ? ""
                            : booking.ToPostcode;
                        command.Parameters["@ToDetail1"].Value = string.IsNullOrEmpty(booking.ToDetail1)
                            ? ""
                            : booking.ToDetail1;
                        command.Parameters["@ToDetail2"].Value = string.IsNullOrEmpty(booking.ToDetail2)
                            ? ""
                            : booking.ToDetail2;
                        command.Parameters["@ToDetail3"].Value = string.IsNullOrEmpty(booking.ToDetail3)
                            ? ""
                            : booking.ToDetail3;
                        command.Parameters["@ToDetail4"].Value = string.IsNullOrEmpty(booking.ToDetail4)
                            ? ""
                            : booking.ToDetail4;
                        command.Parameters["@ToDetail5"].Value = string.IsNullOrEmpty(booking.ToDetail5)
                            ? ""
                            : booking.ToDetail5;
                        command.Parameters["@Ref1"].Value = string.IsNullOrEmpty(booking.Ref1) ? "" : booking.Ref1;
                        command.Parameters["@Ref2"].Value = string.IsNullOrEmpty(booking.Ref2) ? "" : booking.Ref2;

                        command.Parameters["@TPLUS_JobNumber"].Value = string.IsNullOrEmpty(booking.TPLUS_JobNumber)
                            ? ""
                            : booking.TPLUS_JobNumber;
                        command.Parameters["@ConsignmentNumber"].Value = string.IsNullOrEmpty(booking.ConsignmentNumber) ? "" : booking.ConsignmentNumber;

                        switch (booking.UploadedToTplus)
                        {
                            case false:
                                command.Parameters["@UploadedToTplus"].Value = 0;
                                break;
                            case true:
                                command.Parameters["@UploadedToTplus"].Value = 1;
                                break;
                        }

                        command.Parameters["@UploadDateTime"].Value = booking.UploadDateTime;
                        command.CommandText = sql;
                        var id = (int)command.ExecuteScalar();
                        command.Parameters.Clear();
                        //after updating the Booking table ; we also need to update the Items table with any barcodes that are present in the booking
                        if ((booking.lstItems != null) && (booking.lstItems.Count > 0))
                        {
                            var itemSql =
                                "INSERT INTO xCabItems (BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode) " +
                                "VALUES (@BookingId,@Description,@Length,@Width,@Height,@Weight,@Cubic,@Barcode)";
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@Description", SqlDbType.NVarChar);
                            command.Parameters.Add("@Length", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Width", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Height", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Weight", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Cubic", SqlDbType.Decimal, 18);
                            command.Parameters.Add("@Barcode", SqlDbType.NVarChar);
                            foreach (var item in booking.lstItems)
                            {
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@Description"].Value = string.IsNullOrEmpty(item.Description)
                                    ? ""
                                    : item.Description;

                                command.Parameters["@Length"].Precision = 18;
                                command.Parameters["@Length"].Scale = 2;
                                command.Parameters["@Length"].Value = item.Length;

                                command.Parameters["@Width"].Precision = 18;
                                command.Parameters["@Width"].Scale = 2;
                                command.Parameters["@Width"].Value = item.Width;

                                command.Parameters["@Height"].Precision = 18;
                                command.Parameters["@Height"].Scale = 2;
                                command.Parameters["@Height"].Value = item.Height;

                                command.Parameters["@Weight"].Precision = 18;
                                command.Parameters["@Weight"].Scale = 2;
                                command.Parameters["@Weight"].Value = item.Weight;

                                command.Parameters["@Cubic"].Precision = 18;
                                command.Parameters["@Cubic"].Scale = 2;
                                command.Parameters["@Cubic"].Value = item.Cubic;

                                command.Parameters["@Barcode"].Value = string.IsNullOrEmpty(item.Barcode)
                                    ? ""
                                    : item.Barcode;
                                // execute the command
                                command.CommandText = itemSql;
                                command.ExecuteNonQuery();
                            }
                        }
                        //update contact details if any
                        if ((booking.lstContactDetail != null) && (booking.lstContactDetail.Count > 0))
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@BookingId", SqlDbType.Int);
                            command.Parameters.Add("@AreaCode", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50);
                            foreach (var detail in booking.lstContactDetail)
                            {
                                var detailSql =
                                    "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                                command.Parameters["@BookingId"].Value = id;
                                command.Parameters["@AreaCode"].Value = string.IsNullOrEmpty(detail.AreaCode)
                                    ? ""
                                    : detail.AreaCode;
                                command.Parameters["@PhoneNumber"].Value = string.IsNullOrEmpty(detail.PhoneNumber)
                                    ? ""
                                    : detail.PhoneNumber;
                                command.CommandText = detailSql;
                                command.ExecuteNonQuery();
                                Logger.Log("Executed SQL: " + detailSql, Name());
                            }
                        }
                    }
                    bookingStored = true;
                }
                catch (SqlException ex)
                {
                    Logger.Log("Exception Occurred: " + ex.Message, Name());
                    bookingStored = false;
                }
            }
            return bookingStored;
        }

        public bool StoreToDb(List<ModifiedBooking> lstBooking)
        {
            var bookingStored = false;
            if (lstBooking.Count == 0)
                return false;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        string sql;

                        #region Raw text for SQL Inserts

                        sql =
                            "INSERT INTO xCabBooking(StateId, AccountCode, ServiceCode, FromSuburb, FromPostcode, FromDetail1, FromDetail2, FromDetail3, FromDetail4, FromDetail5," +
                            "ToSuburb,ToPostcode,ToDetail1,ToDetail2,ToDetail3,ToDetail4,ToDetail5,Ref1,Ref2,TPLUS_JobNumber) OUTPUT INSERTED.BookingId " +
                            " VALUES(@StateId, @AccountCode, @ServiceCode, @FromSuburb, @FromPostcode, @FromDetail1, @FromDetail2, @FromDetail3, @FromDetail4, @FromDetail5," +
                            "@ToSuburb,@ToPostcode,@ToDetail1,@ToDetail2,@ToDetail3,@ToDetail4,@ToDetail5,@Ref1,@Ref2,@TPLUS_JobNumber)" +
                            "";

                        #endregion

                        foreach (var booking in lstBooking)
                        {
                            if (!string.IsNullOrWhiteSpace(booking.AccountCode))
                                booking.AccountCode = booking.AccountCode.ToUpper();

                            command.Parameters.Add("@LoginId", SqlDbType.Int);
                            command.Parameters.Add("@StateId", SqlDbType.Int);
                            command.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@ServiceCode", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@FromSuburb", SqlDbType.NVarChar);
                            command.Parameters.Add("@FromPostcode", SqlDbType.NVarChar);
                            command.Parameters.Add("@FromDetail1", SqlDbType.NVarChar);
                            command.Parameters.Add("@FromDetail2", SqlDbType.NVarChar);
                            command.Parameters.Add("@FromDetail3", SqlDbType.NVarChar);
                            command.Parameters.Add("@FromDetail4", SqlDbType.NVarChar);
                            command.Parameters.Add("@FromDetail5", SqlDbType.NVarChar);
                            command.Parameters.Add("@ToSuburb", SqlDbType.NVarChar);
                            command.Parameters.Add("@ToPostcode", SqlDbType.NVarChar);
                            command.Parameters.Add("@ToDetail1", SqlDbType.NVarChar);
                            command.Parameters.Add("@ToDetail2", SqlDbType.NVarChar);
                            command.Parameters.Add("@ToDetail3", SqlDbType.NVarChar);
                            command.Parameters.Add("@ToDetail4", SqlDbType.NVarChar);
                            command.Parameters.Add("@ToDetail5", SqlDbType.NVarChar);
                            command.Parameters.Add("@Ref1", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@Ref2", SqlDbType.NVarChar, 50);
                            command.Parameters.Add("@TPLUS_JobNumber", SqlDbType.NVarChar, 50);
                            command.Parameters["@LoginId"].Value = Convert.ToString(booking.LoginDetails.Id);
                            command.Parameters["@StateId"].Value = Convert.ToString(booking.StateId);
                            command.Parameters["@AccountCode"].Value = booking.AccountCode;
                            command.Parameters["@ServiceCode"].Value = booking.ServiceCode;
                            command.Parameters["@FromSuburb"].Value = string.IsNullOrEmpty(booking.FromSuburb)
                                ? ""
                                : booking.FromSuburb;
                            command.Parameters["@FromPostcode"].Value = string.IsNullOrEmpty(booking.FromPostcode)
                                ? ""
                                : booking.FromPostcode;
                            command.Parameters["@FromDetail1"].Value = string.IsNullOrEmpty(booking.FromDetail1)
                                ? ""
                                : booking.FromDetail1;
                            command.Parameters["@FromDetail2"].Value = string.IsNullOrEmpty(booking.FromDetail2)
                                ? ""
                                : booking.FromDetail2;
                            command.Parameters["@FromDetail3"].Value = string.IsNullOrEmpty(booking.FromDetail3)
                                ? ""
                                : booking.FromDetail3;
                            command.Parameters["@FromDetail4"].Value = string.IsNullOrEmpty(booking.FromDetail4)
                                ? ""
                                : booking.FromDetail4;
                            command.Parameters["@FromDetail5"].Value = string.IsNullOrEmpty(booking.FromDetail5)
                                ? ""
                                : booking.FromDetail5;

                            command.Parameters["@ToSuburb"].Value = string.IsNullOrEmpty(booking.ToSuburb)
                                ? ""
                                : booking.ToSuburb;
                            command.Parameters["@ToPostcode"].Value = string.IsNullOrEmpty(booking.ToPostcode)
                                ? ""
                                : booking.ToPostcode;
                            command.Parameters["@ToDetail1"].Value = string.IsNullOrEmpty(booking.ToDetail1)
                                ? ""
                                : booking.ToDetail1;
                            command.Parameters["@ToDetail2"].Value = string.IsNullOrEmpty(booking.ToDetail2)
                                ? ""
                                : booking.ToDetail2;
                            command.Parameters["@ToDetail3"].Value = string.IsNullOrEmpty(booking.ToDetail3)
                                ? ""
                                : booking.ToDetail3;
                            command.Parameters["@ToDetail4"].Value = string.IsNullOrEmpty(booking.ToDetail4)
                                ? ""
                                : booking.ToDetail4;
                            command.Parameters["@ToDetail5"].Value = string.IsNullOrEmpty(booking.ToDetail5)
                                ? ""
                                : booking.ToDetail5;
                            command.Parameters["@Ref1"].Value = string.IsNullOrEmpty(booking.Ref1) ? "" : booking.Ref1;
                            command.Parameters["@Ref2"].Value = string.IsNullOrEmpty(booking.Ref2) ? "" : booking.Ref2;
                            command.Parameters["@TPLUS_JobNumber"].Value = string.IsNullOrEmpty(booking.TPLUS_JobNumber)
                                ? ""
                                : booking.TPLUS_JobNumber;
                            command.CommandText = sql;
                            var id = (int)command.ExecuteScalar();
                            command.Parameters.Clear();
                            //after updating the Booking table ; we also need to update the Items table with any barcodes that are present in the booking
                            if ((booking.lstItems != null) && (booking.lstItems.Count > 0))
                            {
                                var itemSql =
                                    "INSERT INTO xCabItems (BookingId, Description, Length, Width, Height, Weight, Cubic, Barcode) " +
                                    "VALUES (@BookingId,@Description,@Length,@Width,@Height,@Weight,@Cubic,@Barcode)";
                                command.Parameters.Add("@BookingId", SqlDbType.Int);
                                command.Parameters.Add("@Description", SqlDbType.NVarChar);
                                command.Parameters.Add("@Length", SqlDbType.Decimal, 18);
                                command.Parameters.Add("@Width", SqlDbType.Decimal, 18);
                                command.Parameters.Add("@Height", SqlDbType.Decimal, 18);
                                command.Parameters.Add("@Weight", SqlDbType.Decimal, 18);
                                command.Parameters.Add("@Cubic", SqlDbType.Decimal, 18);
                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar);
                                foreach (var item in booking.lstItems)
                                {
                                    command.Parameters["@BookingId"].Value = id;
                                    command.Parameters["@Description"].Value = string.IsNullOrEmpty(item.Description)
                                        ? ""
                                        : item.Description;

                                    command.Parameters["@Length"].Precision = 18;
                                    command.Parameters["@Length"].Scale = 2;
                                    command.Parameters["@Length"].Value = item.Length;

                                    command.Parameters["@Width"].Precision = 18;
                                    command.Parameters["@Width"].Scale = 2;
                                    command.Parameters["@Width"].Value = item.Width;

                                    command.Parameters["@Height"].Precision = 18;
                                    command.Parameters["@Height"].Scale = 2;
                                    command.Parameters["@Height"].Value = item.Height;

                                    command.Parameters["@Weight"].Precision = 18;
                                    command.Parameters["@Weight"].Scale = 2;
                                    command.Parameters["@Weight"].Value = item.Weight;

                                    command.Parameters["@Cubic"].Precision = 18;
                                    command.Parameters["@Cubic"].Scale = 2;
                                    command.Parameters["@Cubic"].Value = item.Cubic;

                                    command.Parameters["@Barcode"].Value = string.IsNullOrEmpty(item.Barcode)
                                        ? ""
                                        : item.Barcode;
                                    command.CommandText = itemSql;
                                    command.ExecuteNonQuery();
                                }
                            }
                            //update contact details if any
                            if ((booking.lstContactDetail != null) && (booking.lstContactDetail.Count > 0))
                            {
                                command.Parameters.Clear();
                                //setup data types
                                command.Parameters.Add("@BookingId", SqlDbType.Int);
                                command.Parameters.Add("@AreaCode", SqlDbType.NVarChar, 50);
                                command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50);
                                foreach (var detail in booking.lstContactDetail)
                                {
                                    var detailSql =
                                        "INSERT INTO xCabContactDetails (BookingId, AreaCode, PhoneNumber) VALUES (@BookingId,@AreaCode,@PhoneNumber)";
                                    command.Parameters["@BookingId"].Value = id;
                                    command.Parameters["@AreaCode"].Value = string.IsNullOrEmpty(detail.AreaCode)
                                        ? ""
                                        : detail.AreaCode;
                                    command.Parameters["@PhoneNumber"].Value = string.IsNullOrEmpty(detail.PhoneNumber)
                                        ? ""
                                        : detail.PhoneNumber;
                                    command.CommandText = detailSql;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    bookingStored = true;
                }
                catch (SqlException ex)
                {
                    Logger.Log("Exception Occurred: " + ex.Message, Name());
                    bookingStored = false;
                }
            }
            return bookingStored;
        }

        private static string Name()
        {
            return "DbConnector";
        }
    }
}
