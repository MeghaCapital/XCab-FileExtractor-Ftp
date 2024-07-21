using Core;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Data.Utils
{
    public class CalculateDates
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="LookDate"> From date which looks for the next working date</param>
        /// <param name="NumberOfDays">number of next date to loook for from start date</param>
        /// <param name="State">state to consider fro holidays</param>
        public DateTime GetNextWorkingDay(DateTime LookDate, int NumberOfDays, string State)
        {
            DateTime NextWorkigDay = DateTime.MinValue;
            try
            {
                using (var sqlCon = new SqlConnection(DbSettings.Default.SecondaryDbConnectionString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("[TPlus].[dbo].[sp_NextNthWorkingDay_GivenDate]", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@state", SqlDbType.NVarChar).Value = State;
                    sql_cmnd.Parameters.AddWithValue("@NextNthday", SqlDbType.Int).Value = NumberOfDays;
                    sql_cmnd.Parameters.AddWithValue("@PassDate", SqlDbType.DateTime).Value = new DateTime(LookDate.Year, LookDate.Month, LookDate.Day, 0, 0, 0);
                    sql_cmnd.Parameters.AddWithValue("@NextWorkingDay", SqlDbType.DateTime).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;
                    sql_cmnd.Parameters["@NextWorkingDay"].Direction = ParameterDirection.Output;
                    sql_cmnd.ExecuteNonQuery();
                    NextWorkigDay = DateTime.Parse(sql_cmnd.Parameters["@NextWorkingDay"].Value.ToString());
                    sqlCon.Close();
                }
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }

            return NextWorkigDay;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LookDate"></param>
        /// <param name="NumberOfDays"></param>
        /// <param name="State"></param>
        /// <param name="isSatudayWorking"></param>
        /// <returns></returns>
        public DateTime GetNextWorkingDayInclusiveSaturday(DateTime LookDate, int NumberOfDays, string State, bool isSatudayWorking)
        {
            DateTime NextWorkigDay = DateTime.MinValue;
            try
            {
                using (var sqlCon = new SqlConnection(DbSettings.Default.SecondaryDbConnectionString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("[TPlus].[dbo].[sp_NextWorkingDay_GivenDate]", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.Add("@state", SqlDbType.NVarChar).Value = State;
                    sql_cmnd.Parameters.Add("@isSaturdayWorking", SqlDbType.Bit).Value = isSatudayWorking ? 1 : 0;
                    sql_cmnd.Parameters.Add("@PassDate", SqlDbType.DateTime).Value = new DateTime(LookDate.Year, LookDate.Month, LookDate.Day, 0, 0, 0);
                    sql_cmnd.Parameters.Add("@NextWorkingDay", SqlDbType.DateTime).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;
                    sql_cmnd.Parameters["@NextWorkingDay"].Direction = ParameterDirection.Output;
                    sql_cmnd.ExecuteNonQuery();
                    NextWorkigDay = DateTime.Parse(sql_cmnd.Parameters["@NextWorkingDay"].Value.ToString());
                    sqlCon.Close();
                }
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }

            return NextWorkigDay;
        }

        public DateTime GetPreviousWorkingDayInclusiveSaturday(DateTime LookDate, int NumberOfDays, string State, bool isSatudayWorking)
        {
            DateTime PreviousWorkigDay = DateTime.MinValue;
            try
            {
                using (var sqlCon = new SqlConnection(DbSettings.Default.SecondaryDbConnectionString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("[TPlus].[dbo].[sp_PreviousWorkingDay_GivenDate]", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@state", SqlDbType.NVarChar).Value = State;
                    sql_cmnd.Parameters.AddWithValue("@isSaturdayWorking", SqlDbType.Bit).Value = isSatudayWorking ? 1 : 0;
                    sql_cmnd.Parameters.AddWithValue("@PassDate", SqlDbType.DateTime).Value = new DateTime(LookDate.Year, LookDate.Month, LookDate.Day, 0, 0, 0);
                    sql_cmnd.Parameters.AddWithValue("@PreviousWorkingDay", SqlDbType.DateTime).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;
                    sql_cmnd.Parameters["@PreviousWorkingDay"].Direction = ParameterDirection.Output;
                    sql_cmnd.ExecuteNonQuery();
                    PreviousWorkigDay = DateTime.Parse(sql_cmnd.Parameters["@PreviousWorkingDay"].Value.ToString());
                    sqlCon.Close();
                }
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }

            return PreviousWorkigDay;
        }

        /// <summary>
        /// Gets the current local time for a given state
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>Localised DateTime</returns>
        public DateTime GetLocalDateByState(int stateId)
        {
            var timeZoneInfo = GetStateTimeZoneInfo(stateId);
            var dateTime = GetLocalDateTime(DateTime.UtcNow, timeZoneInfo);

            return dateTime;
        }


        /// <summary>
        /// Finds the TimeZoneInfo for the given state id
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns>TimeZoneInfo</returns>
        /// <exception cref="ArgumentOutOfRangeException">Exception raised when stateId is invalid</exception>
        public TimeZoneInfo GetStateTimeZoneInfo(int stateId) => stateId switch
        {
            1 => TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time"),
            2 => TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time"),
            3 => TimeZoneInfo.FindSystemTimeZoneById("E. Australia Standard Time"),
            4 => TimeZoneInfo.FindSystemTimeZoneById("Cen. Australia Standard Time"),
            5 => TimeZoneInfo.FindSystemTimeZoneById("W. Australia Standard Time"),
            _ => throw new ArgumentOutOfRangeException(nameof(stateId), $"State id provided is invalid: Id provided {stateId}"),
        };


        private DateTime GetLocalDateTime(DateTime utcDateTime, TimeZoneInfo timeZone)
        {
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
            DateTime time = TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
            return time;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobDate"></param>
        /// <param date="jobDate"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsPublicHoliday(DateTime jobDate, string state)
        {
            var isPublicHoliday = false;

            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ReportSqlDatabaseConnectionString))
                {
                    connection.Open();

                    string sql = @"SELECT COUNT(*) FROM  [TPlus].[dbo].[PublicHolidays] WHERE Day = '" + jobDate.ToString("yyyy-MM-dd") + "'";

                    switch (state)
                    {
                        case "VIC":
                            sql += " AND VIC = 'Y'";
                            break;

                        case "NSW":
                            sql += " AND NSW = 'Y'";
                            break;

                        case "QLD":
                            sql += " AND QLD = 'Y'";
                            break;

                        case "SA":
                            sql += " AND SA = 'Y'";
                            break;

                        case "WA":
                            sql += " AND WA = 'Y'";
                            break;
                        case "NT":
                            sql += " AND NT = 'Y'";
                            break;
                    }

                    var rows = connection.ExecuteScalar<int>(sql);

                    isPublicHoliday = (rows >= 1 ? true : false);
                }
            }
            catch (Exception e)
            {
                _ = Logger.Log(
                    "Exception Occurred while checking if the given job date is a public holiday in : " + state + ". Exception: " + e.Message, "CalculateDates", false);
            }

            return isPublicHoliday;
        }
    }
}
