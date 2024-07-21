using Core;
using Core.Models.Slack;
using Dapper;
using Data.Model.Address;
using Data.Repository.EntityRepositories.DeliverySuburb.Interface;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.DeliverySuburb
{
    public class XCabDeliverySuburbRepository : IXCabDeliverySuburbRepository
    {
        private const int MaximumSuburbLength = 19;
        public bool IsDeliverySuburbAllowed(string suburb, string postcode, int stateId)
        {
            bool Valid = true;

            if (stateId == 1 || stateId == 2 || stateId == 4 || stateId == 5)
                return Valid;

            int count = suburb.Length;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                string suburbTextToCompare = suburb;
                if (count > MaximumSuburbLength)
                {
                    suburbTextToCompare = suburb.Substring(0, MaximumSuburbLength);
                }
                try
                {
                    connection.Open();
                    string sql = "";
                    var dbArgs = new DynamicParameters();
                    if (!string.IsNullOrEmpty(suburbTextToCompare) && !string.IsNullOrEmpty(postcode))
                    {
                        dbArgs.Add("Suburb", suburbTextToCompare.Trim().ToUpper());
                        dbArgs.Add("Postcode", postcode);
                        sql = @"SELECT COUNT(*) FROM xCabDeliverySuburb where
                                    Suburb = @Suburb
									AND Postcode = @Postcode
                                    AND StateId=@StateId";
                    }
                    else if (!string.IsNullOrEmpty(suburbTextToCompare))
                    {
                        dbArgs.Add("Suburb", suburbTextToCompare.Trim().ToUpper());
                        sql = @"SELECT COUNT(*) FROM xCabDeliverySuburb where
                                    Suburb = @Suburb									
                                    AND StateId=@StateId";
                    }
                    else if (!string.IsNullOrEmpty(postcode))
                    {
                        dbArgs.Add("Postcode", postcode);
                        sql = @"SELECT COUNT(*) FROM xCabDeliverySuburb where                                    
									 Postcode = @Postcode
                                    AND StateId=@StateId";
                    }
                    dbArgs.Add("StateId", stateId);
                    int rows = connection.ExecuteScalar<int>(sql, dbArgs);
                    if (rows == 0)
                        Valid = false;
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabDeliverySuburbRepository: isDeliverySuburbAllowed, message: " +
                        e.Message, nameof(XCabDeliverySuburbRepository));
                }

            }
            return Valid;
        }

        public async Task<bool> IsPickupOrDeliverySuburbAllowed(string suburb, string postcode, int stateId)
        {
            bool Valid = true;
            var isMetro = "Y";

            int count = suburb.Length;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                string suburbTextToCompare = suburb;
                if (count > MaximumSuburbLength)
                {
                    suburbTextToCompare = suburb.Substring(0, MaximumSuburbLength);
                }
                try
                {
                    await connection.OpenAsync();
                    string sql = "";
                    var dbArgs = new DynamicParameters();
                    if (!string.IsNullOrEmpty(suburbTextToCompare) && !string.IsNullOrEmpty(postcode))
                    {
                        dbArgs.Add("Suburb", suburbTextToCompare.Trim().ToUpper());
                        dbArgs.Add("Postcode", postcode);
                        dbArgs.Add("IsMetro", isMetro);
                        sql = @"SELECT COUNT(*) FROM xCabDeliverySuburb where
                                    Suburb = @Suburb
									AND Postcode = @Postcode
                                    AND IsMetro = isMetro
                                    AND StateId = @StateId";
                    }
                    else if (!string.IsNullOrEmpty(suburbTextToCompare))
                    {
                        dbArgs.Add("Suburb", suburbTextToCompare.Trim().ToUpper());
                        dbArgs.Add("IsMetro", isMetro);
                        sql = @"SELECT COUNT(*) FROM xCabDeliverySuburb where
                                    Suburb = @Suburb
                                    AND IsMetro = isMetro
                                    AND StateId = @StateId";
                    }
                    else if (!string.IsNullOrEmpty(postcode))
                    {
                        dbArgs.Add("Postcode", postcode);
                        dbArgs.Add("IsMetro", isMetro);
                        sql = @"SELECT COUNT(*) FROM xCabDeliverySuburb where
									 Postcode = @Postcode
                                    AND IsMetro = isMetro
                                    AND StateId=@StateId";
                    }
                    else
                    {
                        return false;
                    }

                    dbArgs.Add("StateId", stateId);
                    int rows = await connection.ExecuteScalarAsync<int>(sql, dbArgs);
                    if (rows == 0)
                        Valid = false;
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabDeliverySuburbRepository: IsPickupOrDeliverySuburbAllowed, message: " +
                        e.Message, nameof(XCabDeliverySuburbRepository));
                }

            }
            return Valid;
        }

        public async Task<bool> IsNonMetroSuburb(string serviceCode, int stateId)
        {
            bool valid = true;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = "";
                    var dbArgs = new DynamicParameters();
                    if (!string.IsNullOrEmpty(serviceCode) && (stateId != -1))
                    {
                        dbArgs.Add("ServiceCode", serviceCode.ToUpper());
                        sql = @"SELECT COUNT(*) FROM XcabRegionalServiceableCodes where
                                    ServiceCode = @ServiceCode
                                    AND StateId = @StateId";
                    }

                    dbArgs.Add("StateId", stateId);
                    int rows = await connection.ExecuteScalarAsync<int>(sql, dbArgs);
                    if (rows == 0)
                        valid = false;
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabDeliverySuburbRepository: IsNonMetroSuburb, message: " +
                        e.Message, nameof(XCabDeliverySuburbRepository));
                }

                return valid;
            }
        }

        public bool IsZoneServiceAllowed(string FromSuburb, string FromPostcode, string ToSuburb, string ToPostcode, int stateId)
        {
            bool Valid = true;

            if (stateId == 1 || stateId == 2 || stateId == 4 || stateId == 5)
                return Valid;

            string fromSuburbTextToCompare = FromSuburb;
            string toSuburbTextToCompare = ToSuburb;
            if (FromSuburb.Length > MaximumSuburbLength)
            {
                fromSuburbTextToCompare = FromSuburb.Substring(0, MaximumSuburbLength);
            }
            else if (ToSuburb.Length > MaximumSuburbLength)
            {
                toSuburbTextToCompare = ToSuburb.Substring(0, MaximumSuburbLength);
            }

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = @"SELECT COUNT(*)
                                FROM [dbo].[xCabZoneGroupsServiceMap] D
                                WHERE D.FromZoneGroupId IN
                                (SELECT TOP 1 C.Id
                                 FROM [dbo].[xCabDeliverySuburb]
                                        A
                                INNER JOIN [dbo].[xCabZones] B ON A.ZoneNumber	= B.ZoneNumber
                                INNER JOIN [dbo].[xCabZoneGroups] C ON B.ZoneGroupId = C.Id";

                    sql += " WHERE A.STATEID = '" + stateId + "'";
                    sql += string.IsNullOrEmpty(FromSuburb) ? "" : " AND A.SUBURB = '" + fromSuburbTextToCompare + "' ";
                    sql += string.IsNullOrEmpty(FromPostcode) ? "" : " AND A.POSTCODE = " + FromPostcode;
                    sql += @") AND D.ToZoneGroupId  IN
                                (SELECT TOP 1 C.Id
                                  FROM [dbo].[xCabDeliverySuburb] A
                                  INNER JOIN [dbo].[xCabZones] B ON A.ZoneNumber  = B.ZoneNumber
                                  INNER JOIN [dbo].[xCabZoneGroups] C ON B.ZoneGroupId = C.Id";

                    sql += " WHERE A.STATEID = '" + stateId + "'";
                    sql += string.IsNullOrEmpty(ToSuburb) ? "" : " AND A.SUBURB = '" + toSuburbTextToCompare + "' ";
                    sql += string.IsNullOrEmpty(ToPostcode) ? "" : " AND A.POSTCODE = " + ToPostcode;
                    sql += ")";

                    int rows = connection.ExecuteScalar<int>(sql);
                    if (rows == 0)
                        Valid = false;
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabDeliverySuburbRepository: IsZoneServiceAllowed, message: " +
                        e.Message, nameof(XCabDeliverySuburbRepository));
                }
            }
            return Valid;
        }

        public async Task<bool> IsZoneServiceAllowedHubSystem(string FromSuburb, string ToSuburb, int stateId)
        {
            bool Valid = true;

            if (stateId == 1 || stateId == 2 || stateId == 4 || stateId == 5)
                return Valid;

            string fromSuburbTextToCompare = FromSuburb;
            string toSuburbTextToCompare = ToSuburb;
            if (FromSuburb.Length > MaximumSuburbLength)
            {
                fromSuburbTextToCompare = FromSuburb.Substring(0, MaximumSuburbLength);
            }
            else if (ToSuburb.Length > MaximumSuburbLength)
            {
                toSuburbTextToCompare = ToSuburb.Substring(0, MaximumSuburbLength);
            }

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = @" SELECT COUNT(AB.ID) FROM (
                                    SELECT  B.ID
                                 FROM [dbo].[xCabDeliverySuburb]   A
                                INNER JOIN [dbo].[xCabZones] B ON A.ZoneNumber	= B.ZoneNumber
								WHERE B.ZoneGroupId = 3 AND A.STATEID = 3 AND A.SUBURB ='" + fromSuburbTextToCompare + "' ";
                    sql += @"UNION ALL SELECT  B.ID
                                 FROM [dbo].[xCabDeliverySuburb]   A
                                INNER JOIN [dbo].[xCabZones] B ON A.ZoneNumber	= B.ZoneNumber
								WHERE B.ZoneGroupId = 3 AND A.STATEID = 3 AND A.SUBURB = '" + toSuburbTextToCompare + "') AB";


                    int rows = await connection.ExecuteScalarAsync<int>(sql);
                    if (rows != 2)
                        Valid = false;
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabDeliverySuburbRepository: IsZoneServiceAllowedHubSystem, message: " +
                        e.Message, nameof(XCabDeliverySuburbRepository));
                }
            }
            return Valid;
        }

        public async Task<bool> IsZoneServiceAllowed(string FromSuburb, string ToSuburb, int stateId)
        {
            bool Valid = true;

            if (stateId == 1 || stateId == 2 || stateId == 4 || stateId == 5)
                return Valid;

            string fromSuburbTextToCompare = FromSuburb;
            string toSuburbTextToCompare = ToSuburb;
            if (FromSuburb.Length > MaximumSuburbLength)
            {
                fromSuburbTextToCompare = FromSuburb.Substring(0, MaximumSuburbLength);
            }
            else if (ToSuburb.Length > MaximumSuburbLength)
            {
                toSuburbTextToCompare = ToSuburb.Substring(0, MaximumSuburbLength);
            }

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = @"SELECT COUNT(*)
                                FROM [dbo].[xCabZoneGroupsServiceMap] D
                                WHERE D.FromZoneGroupId IN
                                (SELECT TOP 1 C.Id
                                 FROM [dbo].[xCabDeliverySuburb]
                                        A
                                INNER JOIN [dbo].[xCabZones] B ON A.ZoneNumber	= B.ZoneNumber
                                INNER JOIN [dbo].[xCabZoneGroups] C ON B.ZoneGroupId = C.Id";

                    sql += " WHERE A.STATEID = '" + stateId + "'";
                    sql += string.IsNullOrEmpty(FromSuburb) ? "" : " AND A.SUBURB = '" + fromSuburbTextToCompare + "' ";
                    sql += @") AND D.ToZoneGroupId  IN
                                (SELECT TOP 1 C.Id
                                  FROM [dbo].[xCabDeliverySuburb] A
                                  INNER JOIN [dbo].[xCabZones] B ON A.ZoneNumber  = B.ZoneNumber
                                  INNER JOIN [dbo].[xCabZoneGroups] C ON B.ZoneGroupId = C.Id";

                    sql += " WHERE A.STATEID = '" + stateId + "'";
                    sql += string.IsNullOrEmpty(ToSuburb) ? "" : " AND A.SUBURB = '" + toSuburbTextToCompare + "' ";
                    sql += ")";

                    int rows = await connection.ExecuteScalarAsync<int>(sql);
                    if (rows == 0)
                        Valid = false;
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabDeliverySuburbRepository: IsZoneServiceAllowed, message: " +
                        e.Message, nameof(XCabDeliverySuburbRepository));
                }
            }
            return Valid;
        }

        public async Task<bool> IsInMyerQLDZoneDelivery(string FromSuburb, string ToSuburb, int stateId)
        {
            bool Valid = true;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string sql = @" SELECT count(*)  FROM [dbo].[xCabDeliverySuburb] WHERE SUBURB = '" + FromSuburb + "' AND stateid = " + stateId + " AND ZoneNumber in (1,2,3,4,5,6,7,8,9,11,12,13)";

                    int fromSuburbInZone = await connection.ExecuteScalarAsync<int>(sql);
                    if (fromSuburbInZone > 0)
                    {
                        sql = @"SELECT count(*)  FROM [dbo].[xCabDeliverySuburb] WHERE SUBURB = '" + ToSuburb + "' AND stateid = " + stateId + " AND ZoneNumber in (1,2,3,4,5,6,7,8,9,11,12,13)";

                        int toSuburbInZone = await connection.ExecuteScalarAsync<int>(sql);
                        if (toSuburbInZone == 0)
                        {
                            Valid = false;
                        }
                    }

                }
                catch (Exception e)
                {
                    await Logger.Log(
                        "Exception Occurred in XCabDeliverySuburbRepository: IsZoneServiceAllowed, message: " +
                        e.Message, nameof(XCabDeliverySuburbRepository));
                }
            }
            return Valid;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromSuburb"></param>
        /// <param name="FromPostcode"></param>
        /// <param name="ToSuburb"></param>
        /// <param name="ToPostcode"></param>
        /// <param name="StateId"></param>
        /// <param name="ServiceCode"></param>
        /// <returns></returns>
        public bool IsStandardServiceAllowed(string FromSuburb, string FromPostcode, string ToSuburb, string ToPostcode, int StateId, string ServiceCode)
        {
            bool Valid = true;

            if (StateId == 2 || StateId == 4 || StateId == 5)
                return Valid;

            if (IsStandardService(ServiceCode))
            {
                Valid = CheckStandardAllowedForZone(StateId, FromSuburb, FromPostcode, ToSuburb, ToPostcode);
            }

            return Valid;
        }

        public async Task<bool> IsStandardServiceAllowed(string FromSuburb, string ToSuburb, int StateId, string ServiceCode)
        {
            bool Valid = true;

            if (StateId == 2 || StateId == 4 || StateId == 5)
                return Valid;

            if (IsStandardService(ServiceCode))
            {
                Valid = await CheckStandardAllowedForZone(StateId, FromSuburb, ToSuburb);
            }

            return Valid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServiceCode"></param>
        /// <returns></returns>
        public bool IsStandardService(string ServiceCode)
        {
            bool StandardTypeCode = true;

            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT SUM(found) found From(" +
                                      " SELECT Count(*) found FROM [Quote].[ServiceCode] WHERE ServiceSubType = 'STN' and Code = '" + ServiceCode + "'" +
                                      " UNION" +
                                      " SELECT Count(*) found FROM [Quote].[ExtraStandardServiceCodes] WHERE Active = 1 and Code = '" + ServiceCode + "') StandardServices";

                    int rows = connection.ExecuteScalar<int>(sql);
                    if (rows == 0)
                        StandardTypeCode = false;
                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in XCabDeliverySuburbRepository: IsZoneServiceAllowed, message: " +
                        e.Message, nameof(XCabDeliverySuburbRepository));
                }
            }
            return StandardTypeCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StateId"></param>
        /// <param name="fromSuburb"></param>
        /// <param name="fromPostcode"></param>
        /// <param name="toSuburb"></param>
        /// <param name="toPostcode"></param>
        /// <returns></returns>
        public bool CheckStandardAllowedForZone(int StateId, string fromSuburb, string fromPostcode, string toSuburb, string toPostcode)
        {
            var standrdsAllowed = false;

            if (StateId == 1 || StateId == 2 || StateId == 4 || StateId == 5)
                return !standrdsAllowed;

            string fromSuburbTextToCompare = fromSuburb;
            string toSuburbTextToCompare = toSuburb;
            if (fromSuburb.Length > MaximumSuburbLength)
            {
                fromSuburbTextToCompare = fromSuburb.Substring(0, MaximumSuburbLength);
            }
            else if (toSuburb.Length > MaximumSuburbLength)
            {
                toSuburbTextToCompare = toSuburb.Substring(0, MaximumSuburbLength);
            }

            try
            {
                string sqlServiceCode = @"SELECT Count(*) StandardsAllowed FROM   [dbo].[xCabDeliverySuburb] DS
                INNER JOIN [dbo].[xCabCentralZoneMapping] CZ on CZ.FromZone = DS.ZoneNumber AND DS.STATEID = " + StateId +
                " WHERE DS.SUBURB = '" + fromSuburbTextToCompare + "' ";

                sqlServiceCode += string.IsNullOrEmpty(fromPostcode) ? "" : " AND DS.POSTCODE = " + fromPostcode.Trim();

                sqlServiceCode += @"  AND CZ.State = " + StateId + " AND   CZ.Allowed = 'Y' AND CZ.ToZone IN " +
                " (SELECT DST.ZoneNumber FROM  [dbo].[xCabDeliverySuburb] DST WHERE DST.SUBURB = '" + toSuburbTextToCompare + "' ";

                sqlServiceCode += string.IsNullOrEmpty(toPostcode) ? "" : " AND DST.POSTCODE = " + toPostcode.Trim();

                sqlServiceCode += @" AND DST.STATEID = " + StateId + ")";

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    if (connection.ExecuteScalar<int>(sqlServiceCode) > 0)
                        standrdsAllowed = true;
                }

                return standrdsAllowed;
            }
            catch (Exception e)
            {
                Logger.LogSlackErrorFromApp("RESTful Web Service: SuburbRepository",
                            "Error while retrieving details from database for Standard type Serviceable zones. Message:" + e.Message + ", Service request:",
                            "RESTful Web Service:SuburbRepository", SlackChannel.WebServiceErrors);
                return standrdsAllowed;
            }
        }

        public async Task<bool> CheckStandardAllowedForZone(int StateId, string fromSuburb, string toSuburb)
        {
            var standrdsAllowed = false;

            if (StateId == 1 || StateId == 2 || StateId == 4 || StateId == 5)
                return !standrdsAllowed;

            string fromSuburbTextToCompare = fromSuburb;
            string toSuburbTextToCompare = toSuburb;
            if (fromSuburb.Length > MaximumSuburbLength)
            {
                fromSuburbTextToCompare = fromSuburb.Substring(0, MaximumSuburbLength);
            }
            else if (toSuburb.Length > MaximumSuburbLength)
            {
                toSuburbTextToCompare = toSuburb.Substring(0, MaximumSuburbLength);
            }

            try
            {
                string sqlServiceCode = @"SELECT Count(*) StandardsAllowed FROM   [dbo].[xCabDeliverySuburb] DS
                INNER JOIN [dbo].[xCabCentralZoneMapping] CZ on CZ.FromZone = DS.ZoneNumber AND DS.STATEID = " + StateId +
                " WHERE DS.SUBURB = '" + fromSuburbTextToCompare + "' ";

                sqlServiceCode += @"  AND CZ.State = " + StateId + " AND   CZ.Allowed = 'Y' AND CZ.ToZone IN " +
                " (SELECT DST.ZoneNumber FROM  [dbo].[xCabDeliverySuburb] DST WHERE DST.SUBURB = '" + toSuburbTextToCompare + "' ";

                sqlServiceCode += @" AND DST.STATEID = " + StateId + ")";

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    await connection.OpenAsync();
                    if (await connection.ExecuteScalarAsync<int>(sqlServiceCode) > 0)
                        standrdsAllowed = true;
                }

                return standrdsAllowed;
            }
            catch (Exception e)
            {
                await Logger.LogSlackErrorFromApp("RESTful Web Service: SuburbRepository",
                            "Error while retrieving details from database for Standard type Serviceable zones. Message:" + e.Message + ", Service request:",
                            "RESTful Web Service:SuburbRepository", SlackChannel.WebServiceErrors);
                return standrdsAllowed;
            }
        }

        public ICollection<Suburb> GetMetroSuburbs(string stateId = null)
        {
            try
            {
                string sqlServiceCode = " SELECT SUBURB as name,POSTCODE as PostCode,STATEID as State  FROM [dbo].[xCabDeliverySuburb] WHERE ISMETRO = 'Y'" + (string.IsNullOrEmpty(stateId) ? "" : " and STATEID =" + stateId);

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var metroList = connection.Query<Suburb>(sqlServiceCode).ToList();
                    return metroList;
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception Occurred while extracting Metro suburb list. Message:" +
                       e.Message, nameof(XCabDeliverySuburbRepository));
                return null;
            }
        }

        public async Task<bool> IsValidInterstateBooking(string fromPostcode, string toPostcode)
        {
            var validBooking = false;
            var fromStateId = 0;
            var toStateId = 0;

            try
            {
                if (toPostcode != null && fromPostcode != null)
                {
                    var fromIndexChar = fromPostcode.Substring(0, 1);
                    switch (fromIndexChar)
                    {
                        case "3":
                            fromStateId = 1;
                            break;
                        case "2":
                            fromStateId = 2;
                            break;
                        case "4":
                            fromStateId = 3;
                            break;
                        case "5":
                            fromStateId = 4;
                            break;
                        case "6":
                            fromStateId = 5;
                            break;
                        default:
                            fromStateId = 0;
                            break;
                    }

                    var toIndexChar = toPostcode.Substring(0, 1);
                    switch (toIndexChar)
                    {
                        case "3":
                            toStateId = 1;
                            break;
                        case "2":
                            toStateId = 2;
                            break;
                        case "4":
                            toStateId = 3;
                            break;
                        case "5":
                            toStateId = 4;
                            break;
                        case "6":
                            toStateId = 5;
                            break;

                        default:
                            toStateId = 0;
                            break;
                    }

                    if (fromStateId != 0 && toStateId != 0)
                    {
                        if (fromStateId == toStateId)
                        {
                            validBooking = true;
                        }

                        if (!validBooking && (fromStateId == 3 || toStateId == 3))
                        {
                            if (fromIndexChar == "2" || toIndexChar == "2")
                            {
                                try
                                {
                                    string sql = "SELECT COUNT(*) FROM xCabDeliverySuburb WHERE Postcode = " + (fromIndexChar == "2" ? fromPostcode : toPostcode) + " AND ZoneNumber >= 50 AND StateId = 3";

                                    using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                                    {
                                        await connection.OpenAsync();
                                        int rows = await connection.ExecuteScalarAsync<int>(sql);

                                        if (rows == 0)
                                        {
                                            validBooking = false;
                                        }

                                        validBooking = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.Log("Exception occurred when checking if an allowed interstate booking in xCabDeliverySuburb. Message:" +
                                        ex.Message, nameof(XCabDeliverySuburbRepository));
                                }
                            }

                        }
                    }
                    return validBooking;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception occurred when validating for stop interstate bookings. Message:" +
                           ex.Message, nameof(XCabDeliverySuburbRepository));
            }

            return validBooking;
        }

    }

}