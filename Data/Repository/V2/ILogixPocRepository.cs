using Core;
using Dapper;
using Data.Model.Poc.V2;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public class ILogixPocRepository : IILogixPocRepository
	{
        public async Task<ICollection<PocImageResponse>> ExtractPocImages(string jobNumber, int legNumber, DateTime jobDate, EStates stateId)
        {
            List<PocImageResponse> pocImageResponse = new List<PocImageResponse>();
            try
            {
                if(jobNumber != null && jobDate != DateTime.MinValue && legNumber > 0)
                {
                    var imageFound = false;
                    var year = jobDate.Year;
                    var month = jobDate.Month;
                    var day = jobDate.Day;
                    var statePrefix = "";

                    switch (stateId)
                    {
                        case Core.EStates.VIC:
                            statePrefix = "M";
                            break;
                        case Core.EStates.NSW:
                            statePrefix = "S";
                            break;
                        case Core.EStates.QLD:
                            statePrefix = "B";
                            break;
                        case Core.EStates.SA:
                            statePrefix = "A";
                            break;
                        case Core.EStates.WA:
                            statePrefix = "P";
                            break;
                    }

                    //get the ilogix job number
                    var ilogixJobNumber = day.ToString().PadLeft(2, '0') + month.ToString().PadLeft(2, '0') + year.ToString().Substring(2) +
                                          statePrefix + jobNumber.PadLeft(8, '0');

                    var sql = "";
                    var century = "_" + DateTime.Now.Year.ToString().Substring(0, 2);
                    var _defaultTableName = "Poc";
                    var isLiveTable = false;
                    year = Convert.ToInt32(year.ToString().Substring(2));
                    var requestedDateTime = new DateTime(2000 + year, month, day);
                    var currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                    if (requestedDateTime > currentDateTime)
                        return pocImageResponse;
                    if (currentDateTime.Subtract(requestedDateTime).Days <= 7)
                    {
                        isLiveTable = true;
                    }
                    if (!isLiveTable)
                    {
                        _defaultTableName = _defaultTableName + century + Convert.ToString(year) + "_" +
                                            Convert.ToString(month).PadLeft(2, '0');
                    }

                    using (var mySqlConnection = new MySqlConnection(DbSettings.Default.ILogixMysqlConnection))
                    {
                        try
                        {
                            mySqlConnection.Open();
                            sql = "SELECT JobNumber, SubJobNumber, Picture FROM "
                  + _defaultTableName + " WHERE JobNumber='" + ilogixJobNumber + "' AND SubJobNumber='" + legNumber.ToString().PadLeft(2, '0') + "'";
                            try
                            {
                                var cmd = new MySqlCommand();
                                cmd = new MySqlCommand(sql, mySqlConnection);

                                var reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                    var PocImage = new PocImageResponse
                                    {
                                        Image = (byte[])reader["Picture"],
                                        Type = "Poc"
                                    };
                                    imageFound = true;
                                    pocImageResponse.Add(PocImage);
                                }
                                //close the reader if open
                                if (!reader.IsClosed)
                                    reader.Close();

                                if (!imageFound)
                                {
                                    for (var i = 1; i <= 14; i++)
                                    {
                                        var requestedDateTimeFix = requestedDateTime.AddDays(i);
                                        if (requestedDateTimeFix > currentDateTime)
                                            break;
                                        var ilogixJobNumberFix = requestedDateTimeFix.Day.ToString().PadLeft(2, '0') + requestedDateTimeFix.Month.ToString().PadLeft(2, '0') + year.ToString() +
                                                 statePrefix + jobNumber.PadLeft(8, '0');
                                        if (currentDateTime.Subtract(requestedDateTimeFix).Days <= 7)
                                        {
                                            isLiveTable = true;
                                            _defaultTableName = "Poc";
                                        }
                                        if (!isLiveTable)
                                        {
                                            // If defaultTableName is poc_2020_11 then because of above line it append one more 2020_11
                                            // and table name becomes – poc_2020_11_2020_11 which is invalid table name and it end up in Catch block. 
                                            if (!_defaultTableName.Contains(century))
                                                _defaultTableName = _defaultTableName + century + Convert.ToString(year) + "_" +
                                                                    Convert.ToString(month).PadLeft(2, '0');
                                            else
                                                _defaultTableName = "Poc" + century + Convert.ToString(year) + "_" +
                                                                    Convert.ToString(month).PadLeft(2, '0');
                                        }

                                        var sqlJobNumberFix = "SELECT JobNumber, SubJobNumber, Picture FROM "
                                              + _defaultTableName + " WHERE JobNumber='" + ilogixJobNumberFix + "' AND SubJobNumber='" + legNumber.ToString().PadLeft(2, '0') + "'";

                                        var cmdJobNumberFix = new MySqlCommand();
                                        cmdJobNumberFix = new MySqlCommand(sqlJobNumberFix, mySqlConnection);

                                        var readerJobNumberFix = cmdJobNumberFix.ExecuteReader();

                                        while (readerJobNumberFix.Read())
                                        {
                                            var PocImage = new PocImageResponse
                                            {
                                                Image = (byte[])readerJobNumberFix["Picture"],
                                                Type = "Poc"
                                            };
                                            imageFound = true;
                                            pocImageResponse.Add(PocImage);
                                        }
                                        //close the reader if open
                                        if (!readerJobNumberFix.IsClosed)
                                            readerJobNumberFix.Close();
                                        if (imageFound)
                                            break;
                                    }
                                }

                                if (!imageFound)
                                {
                                    // Most of the time last day of month POC stores into next month table. 
                                    // Eg - 301120A00254692 is stored in Poc_2020_12 table. So above code does not returns any image. 
                                    // Below code to fix that issue. 
                                    var newTableName = "Poc";
                                    var newYear = year;
                                    var newMonth = month;
                                    if (month == 12)
                                    {
                                        newMonth = 1;
                                        newYear = year + 1;
                                    }
                                    else
                                    {
                                        newMonth = month + 1;
                                    }
                                    newTableName = "Poc" + century + Convert.ToString(newYear) + "_" +
                                                        Convert.ToString(newMonth).PadLeft(2, '0');

                                    var tableExistSql = $"select TABLE_NAME from information_schema.tables where table_name='{newTableName}'";
                                    var tableExistcmd = new MySqlCommand(tableExistSql, mySqlConnection);
                                    var record = tableExistcmd.ExecuteScalar();

                                    if (record != null &&
                                        record.ToString() != String.Empty &&
                                        record.ToString().ToLower().Equals(newTableName.ToLower()))
                                    {

                                        sql = "SELECT JobNumber, SubJobNumber, Picture FROM "
                                              + newTableName + " WHERE JobNumber='" + ilogixJobNumber + "' AND SubJobNumber='" +
                                              legNumber.ToString().PadLeft(2, '0') + "'";

                                        cmd = new MySqlCommand(sql, mySqlConnection);

                                        var newReader = cmd.ExecuteReader();

                                        while (newReader.Read())
                                        {
                                            var PocImage = new PocImageResponse
                                            {                                                
                                                Image = (byte[])newReader["Picture"],
                                                Type = "Poc"
                                            };
                                            imageFound = true;
                                            pocImageResponse.Add(PocImage);
                                        }

                                        //close the reader if open
                                        if (!newReader.IsClosed)
                                            newReader.Close();
                                    }

                                }

                                // This might be redudant code but just for fall back option 
                                if (!imageFound)
                                {
                                    sql = "SELECT JobNumber, SubJobNumber, Picture FROM Poc "
                                       + "WHERE JobNumber='" + ilogixJobNumber + "' AND SubJobNumber='" + legNumber.ToString().PadLeft(2, '0') + "'";

                                    cmd = new MySqlCommand(sql, mySqlConnection);

                                    var mySqlDataReader = cmd.ExecuteReader();

                                    while (mySqlDataReader.Read())
                                    {
                                        var PocImage = new PocImageResponse
                                        {
                                            Image = (byte[])mySqlDataReader["Picture"],
                                            Type = "Poc"
                                        };
                                        imageFound = true;
                                        pocImageResponse.Add(PocImage);
                                    }

                                    //close the reader if open
                                    if (!mySqlDataReader.IsClosed)
                                        mySqlDataReader.Close();
                                }
                            }
                            catch (Exception)
                            {
                            
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
			}
            catch (Exception)
            {

			}
            return pocImageResponse;
        }        
	}
}
