using Core;
using Data.Entities.Ilogix;
using Data.Repository.EntityRepositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Data.Repository.EntityRepositories
{
    public class ILogixJobsRepository : IIlogixJobsRepository
    {
        const string connectionString =
            "Server=ilogixdbserver.challengelogistics.com.au;Pooling=true;Uid=iLogixWebProApp;Pwd=ilogix256;Database=ilogixdb;Connection Timeout=30;";
        public string GetiLogixCustomerCodes(string clientcode)
        {
            var sql = "SELECT CustomerCode FROM customer WHERE ClientCode = " + clientcode;
            var ClientCode = string.Empty;


            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    //var cmd = new MySqlCommand(sql, sqlConnection);
                    using (var cmd = new MySqlCommand(sql, sqlConnection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            //var reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                ClientCode += reader["CustomerCode"] + ",";
                            }
                            reader.Close();
                            cmd.Dispose();
                        }
                    }
                    // remove last comma
                    if (ClientCode.Length > 0)
                    {
                        ClientCode = ClientCode.TrimEnd(',');
                    }

                }
                catch (Exception)
                {
                    //Logger.SendHtmlEmail(
                    //    "Exception Occurred in GetILogixCustomerCode, message: " + ex.Message, "ILogixJobsRepository");
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }

                return ClientCode;
            }
        }

        public string GetILogixClientCode(string clientcode)
        {
            var sql = "SELECT CustomerName FROM customer WHERE ClientCode = " + clientcode;
            var ClientCode = string.Empty;



            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    //var cmd = new MySqlCommand(sql, sqlConnection);
                    using (var cmd = new MySqlCommand(sql, sqlConnection))
                    {
                        //       var reader = cmd.ExecuteReader();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientCode += "'" + reader["CustomerName"].ToString() + "',";
                                //ClientCode += reader["CustomerCode"].ToString() + ",";
                            }
                            reader.Close();
                            cmd.Dispose();
                        }
                    }
                    // remove last comma
                    if (ClientCode.Length > 0)
                    {
                        ClientCode = ClientCode.TrimEnd(',');
                    }

                }
                catch (Exception)
                {
                    //Logger.SendHtmlEmail(
                    //    "Exception Occurred in GetILogixClientCode, message: " + ex.Message, "ILogixJobsRepository");
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
            return ClientCode;

        }

        /// <summary>
        /// Will return the customer code based on the passed in customername 
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public int GetILogixCustomerCode(string customerName)
        {
            var sql = "SELECT CustomerCode FROM customer WHERE CustomerName = '" + customerName.Trim() + "'";
            var customerCode = -1;


            //   var sqlConnection = new MySqlConnection(connectionString);

            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    //var cmd = new MySqlCommand(sql, sqlConnection);
                    using (var cmd = new MySqlCommand(sql, sqlConnection))
                    {
                        customerCode = (int)cmd.ExecuteScalar();
                        cmd.Dispose();
                    }
                }
                catch (Exception)
                {
                    //Logger.SendHtmlEmail(
                    //    "Exception Occurred in GetILogixCustomerCode, message: " + ex.Message, "ILogixJobsRepository");
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
            return customerCode;

        }

        public async Task<ICollection<IlogixJobLeg>> GetIlogixJobLegs(string jobnumber, string statePrefix, DateTime jobDate)
        {
            var ilogixJobLegs = new List<IlogixJobLeg>();
            var year = jobDate.Year;
            var month = jobDate.Month;
            var day = jobDate.Day;
            //get the ilogix job number
            var ilogixJobNumber = day.ToString().PadLeft(2, '0') + month.ToString().PadLeft(2, '0') +
                                  year.ToString().Substring(2) +
                                  statePrefix + jobnumber.PadLeft(8, '0');

            var sql = "";
            var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
            var defaultTableName = "Jobs";
            var isLiveTable = false;
            year = Convert.ToInt32(year.ToString().Substring(2));
            var requestedDateTime = new DateTime(2000 + year, month, day);
            var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            /* if ((month == DateTime.Now.Month) && (currentDateTime.Subtract(requestedDateTime).Days <= 7))
                 isLiveTable = true;*/
            if (currentDateTime.Subtract(requestedDateTime).Days <= 7)
                isLiveTable = true;
            if (!isLiveTable)
                defaultTableName = defaultTableName + century + Convert.ToString(year) + "_" +
                                   Convert.ToString(month).PadLeft(2, '0');
            sql = "SELECT JobNumber, SubJobNumber, Details,StatusId,MobileId,DateTimePickup,DateTimeDelivered,Comments,Demurruge, OriginalAddressPickup As DetentionReasonCode,ConsignmentNote As Reference,Latitude,Longitude, Destination As DeliveryAddress FROM "
                  + defaultTableName + " WHERE JobNumber='" + ilogixJobNumber + "' AND (statusId = 11 or StatusId=7 or StatusId = 8)";

            //var sqlConnection = new MySqlConnection(connectionString);
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    await sqlConnection.OpenAsync();
                    using (var cmd = new MySqlCommand(sql, sqlConnection))
                    {
                        //var reader = cmd.ExecuteReader();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var ilogixJobLeg = new IlogixJobLeg
                                {
                                    JobNumber = reader["jobnumber"].ToString(),
                                    SubJobNumber = reader["subjobnumber"].ToString(),
                                    StatusId = Convert.ToInt32(reader["StatusId"].ToString()),
                                    MobileId = reader["MobileId"].ToString(),
                                    DateTimePickup = Convert.ToDouble(reader["DateTimePickup"].ToString()),
                                    DateTimeDelivered = Convert.ToDouble(reader["DateTimeDelivered"].ToString()),
                                    Comments = reader["comments"].ToString(),
                                    Demurruge = reader["Demurruge"].ToString(),
                                    Details = reader["details"].ToString(),
                                    DetentionReasonCode = reader["DetentionReasonCode"].ToString(),
                                    Reference = reader["Reference"].ToString(),
                                    Latitude = Convert.ToDouble(reader["latitude"].ToString()),
                                    Longitude = Convert.ToDouble(reader["Longitude"].ToString()),
                                    DeliveryAddress = reader["DeliveryAddress"].ToString(),
                                    TPLUS_JobNumber = jobnumber
                                };
                                ilogixJobLegs.Add(ilogixJobLeg);
                            }
                            reader.Close();
                            cmd.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Logger.Log(
                   "Exception Occurred in ILogixJobsRepository: GetIlogixJobLegs, message: " +
                   ex.Message, "ILogixJobsRepository");
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
            return ilogixJobLegs;
        }

        /// <summary>
        /// Designed to search ilogix jobs by the ref1 and ref2, will return any matched job numbers
        /// </summary>
        /// <param name="jobDate">A date from today to any day in the past</param>
        /// <param name="clientCode">required field</param>
        /// <param name="searchRefId">required field any format accepted.  ref1 = ConsignmentNote, ref2 = RFID</param>
        /// <param name="customerCode">optional field, useful when used with a national account</param>
        /// <returns></returns>
        public ICollection<IlogixJobLegLookups> SearchILogixJobNumber(DateTime jobDate, string clientCode,
            string searchRefId, string customerCode = "")
        {
            var ilogixJobLegs = new List<IlogixJobLegLookups>();

            var year = jobDate.Year;
            var month = jobDate.Month;
            var day = jobDate.Day;

            var inputdateStr = day.ToString().PadLeft(2, '0') + "/" + month.ToString().PadLeft(2, '0') + "/" + year;
            var extendDate = DateTime.ParseExact(inputdateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var sql = "";
            var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
            var defaultTableName = "Jobs";
            var isLiveTable = false;
            year = Convert.ToInt32(year.ToString().Substring(2));
            // var requestedDateTime = new DateTime(2000 + year, month, day);
            //var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //if ((currentDateTime.Subtract(requestedDateTime).Days < 7))
            if (month == DateTime.Now.Month)
                isLiveTable = true;

            if (!isLiveTable)
                // last month
                defaultTableName += century + Convert.ToString(year) + "_" +
                                    Convert.ToString(month).PadLeft(2, '0');

            var sqlLookupTable = "SELECT EXISTS(" +
                                 "SELECT " +
                                 "    TABLE_NAME " +
                                 "FROM " +
                                 "    INFORMATION_SCHEMA.TABLES " +
                                 "WHERE " +
                                 "   (TABLE_NAME = '" + defaultTableName + "') " +
                                 "   AND " +
                                 "   (TABLE_SCHEMA = 'ilogixdb')" +
                                 ") as 'is-exists'";



            //  var sqlConnection = new MySqlConnection(connectionString);
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();
                //var cmd = new MySqlCommand();

                // gather results from 3 tables maximum
                for (var i = 0; i < 3; i++)
                {
                    // MySqlDataReader reader = null;
                    try
                    {
                        // need to avoid lookup of table that does not exist 
                        //cmd = new MySqlCommand(sqlLookupTable, sqlConnection);

                        using (var cmd = new MySqlCommand(sqlLookupTable, sqlConnection))
                        {
                            //reader = cmd.ExecuteReader();
                            using (var reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                if (Convert.ToUInt32(reader["is-exists"].ToString()) > 0)
                                {
                                    // just get the jobnumber - nothing else needed
                                    sql +=
                                        "SELECT DISTINCT JobNumber, ConsignmentNote as Ref1, RFID as Ref2, fnDblDate(Modified) as UploadDateTime, Details as FromDetail4 FROM "
                                        + defaultTableName + " WHERE (ConsignmentNote LIKE '%" + searchRefId + "%' " +
                                        " OR RFID LIKE '%" + searchRefId + "%')" +
                                        " AND ClientCode = " + clientCode;
                                    // need to cater for customer code as well 
                                    if (customerCode != "-1") sql += " AND CustomerCode = " + customerCode;

                                    // do not add union for last iteration
                                    if (i != 2)
                                        sql += " UNION ";
                                }
                                reader.Close();
                            }
                        }


                        // get the next table name but fix for it using current month
                        if (isLiveTable)
                        {
                            defaultTableName = "Jobs" + century + Convert.ToString(year) + "_" +
                                               Convert.ToString(month).PadLeft(2, '0');
                            isLiveTable = false;
                        }
                        else
                        {
                            year = month == 01 ? year - 1 : year;
                            month = month == 1 ? 12 : month - 1;
                            defaultTableName = "Jobs" + century + Convert.ToString(year) + "_" +
                                               Convert.ToString(month).PadLeft(2, '0');
                        }
                        sqlLookupTable = "SELECT EXISTS(" +
                                         "SELECT " +
                                         "    TABLE_NAME " +
                                         "FROM " +
                                         "    INFORMATION_SCHEMA.TABLES " +
                                         "WHERE " +
                                         "   (TABLE_NAME = '" + defaultTableName + "') " +
                                         "   AND " +
                                         "   (TABLE_SCHEMA = 'ilogixdb')" +
                                         ") as 'is-exists'";
                    }
                    catch (Exception)
                    {
                        //Logger.SendHtmlEmail(
                        //    "Exception Occurred in SearchILogixJobNumber I, message: " + e.Message, "ILogixJobsRepository");
                    }
                    /* finally
                     {
                         //   reader?.Close();
                     }*/
                }



                //forward month
                var extendedMonth = extendDate.AddMonths(1);
                var jobTableFwd = "Jobs_" + Convert.ToString(extendedMonth.Year) + "_" +
                                  Convert.ToString(extendedMonth.Month).PadLeft(2, '0');
                var sqlForwardMonthLookupTable = "SELECT EXISTS(" +
                                                 "SELECT " +
                                                 "    TABLE_NAME " +
                                                 "FROM " +
                                                 "    INFORMATION_SCHEMA.TABLES " +
                                                 "WHERE " +
                                                 "   (TABLE_NAME = '" + jobTableFwd + "') " +
                                                 "   AND " +
                                                 "   (TABLE_SCHEMA = 'ilogixdb')" +
                                                 ") as 'is-exists'";

                //var cmd2 = new MySqlCommand(sqlForwardMonthLookupTable, sqlConnection);

                using (var cmd2 = new MySqlCommand(sqlForwardMonthLookupTable, sqlConnection))
                {
                    //   var reader2 = cmd2.ExecuteReader();
                    using (var reader2 = cmd2.ExecuteReader())
                    {
                        reader2.Read();
                        if (Convert.ToUInt32(reader2["is-exists"].ToString()) > 0)
                        {
                            // just get the jobnumber - nothing else needed
                            sql +=
                                " UNION SELECT DISTINCT JobNumber, ConsignmentNote as Ref1, RFID as Ref2, fnDblDate(Modified) as UploadDateTime, Details as FromDetail4 FROM "
                                + jobTableFwd + " WHERE (ConsignmentNote LIKE '%" + searchRefId + "%' " +
                                " OR RFID LIKE '%" + searchRefId + "%')" +
                                " AND ClientCode = " + clientCode;
                            // need to cater for customer code as well 
                            if (customerCode != "-1") sql += " AND CustomerCode = " + customerCode;
                        }
                        reader2.Close();
                        cmd2.Dispose();
                    }
                }

                try
                {
                    //cmd = new MySqlCommand(sql, sqlConnection);
                    using (var cmd = new MySqlCommand(sql, sqlConnection))
                    {
                        //var reader = cmd.ExecuteReader();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ilogixJobLeg = new IlogixJobLegLookups
                                {
                                    JobNumber = reader["JobNumber"].ToString(),
                                    UploadDateTime = Convert.ToDateTime(reader["UploadDateTime"]),
                                    FromDetail4 = reader["FromDetail4"].ToString(),
                                    Ref1 = reader["Ref1"].ToString(),
                                    Ref2 = reader["Ref2"].ToString(),
                                    JobMonthId = Convert.ToDateTime(reader["UploadDateTime"]).Month.ToString()
                                };
                                ilogixJobLegs.Add(ilogixJobLeg);
                            }
                            reader.Close();
                            cmd.Dispose();
                        }

                    }
                }
                catch (Exception)
                {
                    //Logger.SendHtmlEmail(
                    //    "Exception Occurred in SearchILogixJobNumber II, message: " + ex.Message, "ILogixJobsRepository");
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }

            return ilogixJobLegs;
        }


        /// <summary>
        /// Designed to search ilogix jobs by the ref1 and ref2, will return any matched job numbers
        /// </summary>
        /// <param name="jobDate">A date from today to any day in the past</param>
        /// <param name="searchRefId">required field any format accepted.  ref1 = ConsignmentNote, ref2 = RFID</param>
        /// <param name="customerCode">optional field, useful when used with a national account</param>
        /// <returns></returns>
        public ICollection<IlogixJobLegLookups> SearchILogixJobNumberByCustomerCode(DateTime jobDate, string searchRefId, string customerCode)
        {
            if (customerCode.Length == 0)
            {
                return null;
            }

            var ilogixJobLegs = new List<IlogixJobLegLookups>();

            var year = jobDate.Year;
            var month = jobDate.Month;

            var day = jobDate.Day;

            var inputdateStr = day.ToString().PadLeft(2, '0') + "/" + month.ToString().PadLeft(2, '0') + "/" + year;
            var extendDate = DateTime.ParseExact(inputdateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var sql = "";
            var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
            var defaultTableName = "Jobs";
            var isLiveTable = false;
            year = Convert.ToInt32(year.ToString().Substring(2));

            if (month == DateTime.Now.Month)
                isLiveTable = true;

            if (!isLiveTable)
                // last month
                defaultTableName += century + Convert.ToString(year) + "_" +
                                    Convert.ToString(month).PadLeft(2, '0');

            var sqlLookupTable = "SELECT EXISTS(" +
                                 "SELECT " +
                                 "    TABLE_NAME " +
                                 "FROM " +
                                 "    INFORMATION_SCHEMA.TABLES " +
                                 "WHERE " +
                                 "   (TABLE_NAME = '" + defaultTableName + "') " +
                                 "   AND " +
                                 "   (TABLE_SCHEMA = 'ilogixdb')" +
                                 ") as 'is-exists'";



            //var sqlConnection = new MySqlConnection(connectionString);
            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();

                // gather results from 1 year old tables maximum
                // Live table + same month table + 12 months back data = 14 
                // e.g. - On 15th Nov 2019 - Jobs + Jobs_2019_11 + Jobs_2019_10 +... + Jobs_2018_11
                for (var i = 0; i < 8; i++)
                {
                    //  MySqlDataReader reader = null;
                    try
                    {
                        // need to avoid lookup of table that does not exist 
                        //var cmd = new MySqlCommand(sqlLookupTable, sqlConnection);

                        using (var cmd = new MySqlCommand(sqlLookupTable, sqlConnection))
                        {
                            //reader = cmd.ExecuteReader();
                            using (var reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                if (Convert.ToUInt32(reader["is-exists"].ToString()) > 0)
                                {
                                    // just get the jobnumber - nothing else needed
                                    sql +=
                                        "SELECT DISTINCT JobNumber, ConsignmentNote as Ref1, RFID as Ref2, fnDblDate(Modified) as UploadDateTime, Details as FromDetail4 FROM "
                                        + defaultTableName + " WHERE (ConsignmentNote LIKE '%" + searchRefId + "%' " +
                                        " OR RFID LIKE '%" + searchRefId + "%')";
                                    //" AND ClientCode = " + clientCode;
                                    // need to cater for customer code as well 
                                    sql += " AND CustomerCode IN (" + customerCode + ")";

                                    // do not add union for last iteration
                                    if (i != 7)
                                        sql += " UNION ";
                                }
                                reader.Close();
                                cmd.Dispose();
                            }
                        }

                        // get the next table name but fix for it using current month
                        if (isLiveTable)
                        {
                            defaultTableName = "Jobs" + century + Convert.ToString(year) + "_" +
                                               Convert.ToString(month).PadLeft(2, '0');
                            isLiveTable = false;
                        }
                        else
                        {
                            year = month == 01 ? year - 1 : year;
                            month = month == 1 ? 12 : month - 1;
                            defaultTableName = "Jobs" + century + Convert.ToString(year) + "_" +
                                               Convert.ToString(month).PadLeft(2, '0');
                        }
                        sqlLookupTable = "SELECT EXISTS(" +
                                         "SELECT " +
                                         "    TABLE_NAME " +
                                         "FROM " +
                                         "    INFORMATION_SCHEMA.TABLES " +
                                         "WHERE " +
                                         "   (TABLE_NAME = '" + defaultTableName + "') " +
                                         "   AND " +
                                         "   (TABLE_SCHEMA = 'ilogixdb')" +
                                         ") as 'is-exists'";
                    }
                    catch (Exception)
                    {
                        //Logger.SendHtmlEmail(
                        //    "Exception Occurred in SearchILogixJobNumberByCustomerCode I, message: " + e.Message, "ILogixJobsRepository");
                    }
                    /*finally
                    {
                        // reader?.Close();
                    }*/
                }


                //forward month
                var extendedMonth = extendDate.AddMonths(1);
                var jobTableFwd = "Jobs_" + Convert.ToString(extendedMonth.Year) + "_" +
                                  Convert.ToString(extendedMonth.Month).PadLeft(2, '0');
                var sqlForwardMonthLookupTable = "SELECT EXISTS(" +
                                                 "SELECT " +
                                                 "    TABLE_NAME " +
                                                 "FROM " +
                                                 "    INFORMATION_SCHEMA.TABLES " +
                                                 "WHERE " +
                                                 "   (TABLE_NAME = '" + jobTableFwd + "') " +
                                                 "   AND " +
                                                 "   (TABLE_SCHEMA = 'ilogixdb')" +
                                                 ") as 'is-exists'";
                //                var cmd2 = new MySqlCommand(sqlForwardMonthLookupTable, sqlConnection);

                using (var cmd2 = new MySqlCommand(sqlForwardMonthLookupTable, sqlConnection))
                {
                    //var reader2 = cmd2.ExecuteReader();
                    using (var reader2 = cmd2.ExecuteReader())
                    {
                        reader2.Read();
                        if (Convert.ToUInt32(reader2["is-exists"].ToString()) > 0)
                        {
                            // just get the jobnumber - nothing else needed
                            sql +=
                                " UNION SELECT DISTINCT JobNumber, ConsignmentNote as Ref1, RFID as Ref2, fnDblDate(Modified) as UploadDateTime, Details as FromDetail4 FROM "
                                + jobTableFwd + " WHERE (ConsignmentNote LIKE '%" + searchRefId + "%' " +
                                " OR RFID LIKE '%" + searchRefId + "%')";
                            // " AND ClientCode = " + clientCode;
                            // need to cater for customer code as well 
                            sql += " AND CustomerCode IN (" + customerCode + ")";

                        }
                        reader2.Close();
                        cmd2.Dispose();
                    }
                }
                try
                {
                    sql += " limit 100";
                    //var cmd3 = new MySqlCommand(sql, sqlConnection);
                    using (var cmd3 = new MySqlCommand(sql, sqlConnection))
                    {
                        cmd3.CommandTimeout = 120;
                        //var reader3 = cmd3.ExecuteReader();
                        using (var reader3 = cmd3.ExecuteReader())
                        {
                            while (reader3.Read())
                            {
                                var ilogixJobLeg = new IlogixJobLegLookups
                                {
                                    JobNumber = reader3["JobNumber"].ToString(),
                                    UploadDateTime = Convert.ToDateTime(reader3["UploadDateTime"]),
                                    FromDetail4 = reader3["FromDetail4"].ToString(),
                                    Ref1 = reader3["Ref1"].ToString(),
                                    Ref2 = reader3["Ref2"].ToString(),
                                    JobMonthId = Convert.ToDateTime(reader3["UploadDateTime"]).Month.ToString()
                                };
                                ilogixJobLegs.Add(ilogixJobLeg);
                            }
                            reader3.Close();
                            cmd3.Dispose();
                        }
                    }
                }
                catch (Exception)
                {
                    //Logger.SendHtmlEmail(
                    //    "Exception Occurred in SearchILogixJobNumberByCustomerCode II, message: " + ex.Message, "ILogixJobsRepository");
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }

            return ilogixJobLegs;
        }


        /// <summary>
        /// Will lookup the MySQL table, JOBS and return one JobNumber
        /// </summary>
        /// <param name="shortJobnumber">Will contain just the job number extracted from XCab</param>
        /// <param name="jobDate">The date client has entered, we need to move back in some cases to capture data</param>
        /// <param name="clientCode">The client code that requesting client belongs to</param>
        /// <param name="customerCode">The customer code that requesting client belongs to, OPTIONAL</param>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public ICollection<IlogixJobLeg> GetILogixJobNumber(string shortJobnumber, DateTime jobDate, string clientCode,
            string customerCode = "", string stateId = "")
        {
            var ilogixJobLegs = new List<IlogixJobLeg>();

            var year = jobDate.Year;
            var month = jobDate.Month;
            var originalMonth = jobDate.Month;

            var statePrefix = "";
            // check state code if passed and setup the State
            if (stateId.Length > 0)
            {
                switch (stateId)
                {
                    case "1":
                        statePrefix = "M";
                        break;
                    case "2":
                        statePrefix = "S";
                        break;
                    case "3":
                        statePrefix = "B";
                        break;
                    case "4":
                        statePrefix = "A";
                        break;
                    case "5":
                        statePrefix = "P";
                        break;
                }
            }

            var ilogixJobNumber = "%" + statePrefix + shortJobnumber.PadLeft(8, '0');

            var sql = "";
            var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
            var defaultTableName = "Jobs";
            var isLiveTable = false;
            year = Convert.ToInt32(year.ToString().Substring(2));
            // var requestedDateTime = new DateTime(2000 + year, month, day);
            //var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (month == DateTime.Now.Month)
                isLiveTable = true;

            if (!isLiveTable)
                defaultTableName = defaultTableName + century + Convert.ToString(year) + "_" +
                                   Convert.ToString(month).PadLeft(2, '0');

            // just get the jobnumber - nothing else needed
            // sql = "SELECT DISTINCT JobNumber, SubJobNumber," + month + " AS mthTable, Rfid, ConsignmentNote, Modified FROM "
            //       + defaultTableName + " WHERE JobNumber LIKE '" + ilogixJobNumber + "' ";

            var sqlLookupTable = "SELECT EXISTS(" +
                                 "SELECT " +
                                 "    TABLE_NAME " +
                                 "FROM " +
                                 "    INFORMATION_SCHEMA.TABLES " +
                                 "WHERE " +
                                 "   (TABLE_NAME = '" + defaultTableName + "') " +
                                 "   AND " +
                                 "   (TABLE_SCHEMA = 'ilogixdb')" +
                                 ") as 'is-exists'";



            //var sqlConnection = new MySqlConnection(connectionString);

            using (var sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();
                //   var cmd = new MySqlCommand();

                // gather results from 3 tables maximum
                for (var i = 0; i < 3; i++)
                {
                    //MySqlDataReader reader = null;
                    try
                    {
                        // need to avoid lookup of table that does not exist 
                        //var cmd = new MySqlCommand(sqlLookupTable, sqlConnection);
                        using (var cmd = new MySqlCommand(sqlLookupTable, sqlConnection))
                        {
                            //reader = cmd.ExecuteReader();
                            using (var reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                if (Convert.ToUInt32(reader["is-exists"].ToString()) > 0)
                                {
                                    // just get the jobnumber - nothing else needed
                                    sql += "SELECT DISTINCT JobNumber, SubJobNumber," + month +
                                           " AS mthTable, Rfid, ConsignmentNote, Modified FROM "
                                           + defaultTableName + " WHERE JobNumber LIKE '" + ilogixJobNumber + "' ";
                                    // need to cater for customer code as well - this is critical since iLogix esp. with GWA
                                    // munts data so we expect the client code to be 481 but not all sites have that value.
                                    // so the customer code is specific to customers as they login so is still a valid query item
                                    if (customerCode != "-1")
                                    {
                                        sql += " AND CustomerCode = " + customerCode;
                                    }

                                    // do not add union for last iteration
                                    if (i != 2)
                                        sql += " UNION ";
                                }
                                reader.Close();
                                cmd.Dispose();
                            }
                        }

                        // get the next table name but fix for it using current month
                        if (isLiveTable)
                        {
                            defaultTableName = "Jobs" + century + Convert.ToString(year) + "_" +
                                               Convert.ToString(month).PadLeft(2, '0');
                            isLiveTable = false;
                        }
                        else
                        {
                            year = month == 01 ? year - 1 : year;
                            month = month == 1 ? 12 : month - 1;
                            defaultTableName = "Jobs" + century + Convert.ToString(year) + "_" +
                                               Convert.ToString(month).PadLeft(2, '0');
                        }
                        sqlLookupTable = "SELECT EXISTS(" +
                                         "SELECT " +
                                         "    TABLE_NAME " +
                                         "FROM " +
                                         "    INFORMATION_SCHEMA.TABLES " +
                                         "WHERE " +
                                         "   (TABLE_NAME = '" + defaultTableName + "') " +
                                         "   AND " +
                                         "   (TABLE_SCHEMA = 'ilogixdb')" +
                                         ") as 'is-exists'";
                    }
                    catch (Exception)
                    {
                        //Logger.SendHtmlEmail(
                        //    "Exception Occurred in GetILogixJobNumber I, message: " + e.Message, "ILogixJobsRepository");
                    }
                    /*finally
                    {
                        //  reader?.Close();
                    }*/
                }

                // reset back to original date set
                var inputdateStr = jobDate.Day.ToString().PadLeft(2, '0') + "/" +
                                   jobDate.Month.ToString().PadLeft(2, '0') + "/" + jobDate.Year;

                var extendDate = DateTime.ParseExact(inputdateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //forward month
                var extendedMonth = extendDate.AddMonths(1);
                var jobTableFwd = "Jobs_" + Convert.ToString(extendedMonth.Year) + "_" +
                                  Convert.ToString(extendedMonth.Month).PadLeft(2, '0');
                var sqlForwardMonthLookupTable = "SELECT EXISTS(" +
                                                 "SELECT " +
                                                 "    TABLE_NAME " +
                                                 "FROM " +
                                                 "    INFORMATION_SCHEMA.TABLES " +
                                                 "WHERE " +
                                                 "   (TABLE_NAME = '" + jobTableFwd + "') " +
                                                 "   AND " +
                                                 "   (TABLE_SCHEMA = 'ilogixdb')" +
                                                 ") as 'is-exists'";
                //var cmd2 = new MySqlCommand(sqlForwardMonthLookupTable, sqlConnection);

                using (var cmd2 = new MySqlCommand(sqlForwardMonthLookupTable, sqlConnection))
                {
                    //var reader2 = cmd2.ExecuteReader();
                    using (var reader2 = cmd2.ExecuteReader())
                    {
                        reader2.Read();
                        if (Convert.ToUInt32(reader2["is-exists"].ToString()) > 0)
                        {
                            // just get the jobnumber - nothing else needed
                            sql += " UNION SELECT DISTINCT JobNumber, SubJobNumber," + month +
                                   " AS mthTable, Rfid, ConsignmentNote, Modified FROM "
                                   + jobTableFwd + " WHERE JobNumber LIKE '" + ilogixJobNumber + "' ";
                            if (customerCode != "-1")
                            {
                                sql += " AND CustomerCode = " + customerCode;
                            }
                        }
                        reader2.Close();
                        cmd2.Dispose();
                    }
                }

                // add back the current month if not used above - this is needed to cater in the event of
                // a job created towards the end of a month still being located in the live JOB table. The job
                // will be moved to an archive table during the next week and this is treated as an edge case.
                if (originalMonth != DateTime.Now.Month)
                {
                    // just get the jobnumber - nothing else needed
                    sql += " UNION SELECT DISTINCT JobNumber, SubJobNumber," + month +
                           " AS mthTable, Rfid, ConsignmentNote, Modified FROM JOBS"
                           + " WHERE JobNumber LIKE '" + ilogixJobNumber + "' ";
                    if (customerCode != "-1")
                    {
                        sql += " AND CustomerCode = " + customerCode;
                    }
                }

                try
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    //   var cmd = new MySqlCommand();
                    //var cmd = new MySqlCommand(sql, sqlConnection);
                    using (var cmd = new MySqlCommand(sql, sqlConnection))
                    {
                        //   var reader = cmd.ExecuteReader();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ilogixJobLeg = new IlogixJobLeg
                                {
                                    JobNumber = reader["JobNumber"].ToString(),
                                    SubJobNumber = reader["SubJobNumber"].ToString(),
                                    JobMonthId = reader["mthTable"].ToString(),
                                    Rfid = reader["Rfid"].ToString(),
                                    ConsignmentNote = reader["consignmentnote"].ToString(),
                                    Modified = Convert.ToDouble(reader["Modified"].ToString())

                                };
                                ilogixJobLegs.Add(ilogixJobLeg);
                            }
                            reader.Close();
                            cmd.Dispose();
                        }
                    }
                }
                catch (Exception)
                {
                    //Logger.SendHtmlEmail(
                    //    "Exception Occurred in GetILogixJobNumber II, message: " + ex.Message, "ILogixJobsRepository");
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
            return ilogixJobLegs;
        }
    }
}

