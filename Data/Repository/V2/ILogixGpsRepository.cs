using Core;
using Dapper;
using Data.Model;
using Microsoft.Data.SqlClient;

namespace Data.Repository.V2
{
	public class ILogixGpsRepository : IILogixGpsRepository
	{
        public async Task<ICollection<Gps>> GetLastKnownGpsDetails(IEnumerable<int> driverNumbers, EStates state)
        {
            using (var connection = new SqlConnection(DbSettings.Default.SecondaryDbConnectionString))
            {
                var lastKnownGpsDetails = new List<Gps>();
                try
                {
                    var tableName = "";
                    switch (state)
                    {
                        case EStates.VIC:
                            tableName = "gpsDataMelbourne";
                            break;
                        case EStates.NSW:
                            tableName = "gpsDataSydney";
                            break;
                        case EStates.QLD:
                            tableName = "gpsDataBrisbane";
                            break;
                        case EStates.SA:
                            tableName = "gpsDataAdelaide";
                            break;
                        case EStates.WA:
                            tableName = "gpsDataPerth";
                            break;
                    }

                    if (state != EStates.NSW)
                    {
                        await connection.OpenAsync();
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("DriverNumbers", string.Join(",", driverNumbers));
                        var sql = @"SELECT id,
                                            DriverNumber,
                                            GpsDateTime,
                                            Latitude,
                                            Longitude,
                                            Speed,
                                            DriveDate,
                                            distance 
                                        FROM [apps].[gccd].[" + tableName + "] " +
										"WHERE id IN(SELECT " + "max(id) " +
										"FROM [apps].[gccd].[" + tableName + "] WHERE DriverNumber IN (@DriverNumbers) Group by DriverNumber)";
                        lastKnownGpsDetails = (List<Gps>)connection.QueryAsync<Gps>(sql, dbArgs).Result;
                        return lastKnownGpsDetails;
                    }
                    else
                    {
                        await connection.OpenAsync();
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("DriverNumbers", string.Join(",", driverNumbers));
                        var sql = @"SELECT id,
                                            DriverNumber,
                                            GpsDateTime,
                                            Latitude,
                                            Longitude,
                                            Speed
                                        FROM [apps].[gccd].[" + tableName + "] " +
                                        "WHERE id IN(SELECT " + "max(id) " +
                                        "FROM [apps].[gccd].[" + tableName + "] WHERE DriverNumber IN (@DriverNumbers) Group by DriverNumber)";
                        lastKnownGpsDetails = (List<Gps>)connection.QueryAsync<Gps>(sql, dbArgs).Result;
                        return lastKnownGpsDetails;
                    }
                }
                catch (Exception)
                {
                    //Log
                }
            }
            return null;
        }

        public async Task<ICollection<Gps>> GetGpsDetailsBetweenDates(string startDateTime, string endDateTime, ICollection<int> driverNumbers, EStates state)
        {
            using (var connection = new SqlConnection(DbSettings.Default.SecondaryDbConnectionString))
            {
                var gpsDetails = new List<Gps>();
                try
                {
                    var tableName = "";
                    switch (state)
                    {
                        case EStates.VIC:
                            tableName = "gpsDataMelbourne";
                            break;
                        case EStates.NSW:
                            tableName = "gpsDataSydney";
                            break;
                        case EStates.QLD:
                            tableName = "gpsDataBrisbane";
                            break;
                        case EStates.SA:
                            tableName = "gpsDataAdelaide";
                            break;
                        case EStates.WA:
                            tableName = "gpsDataPerth";
                            break;
                    }

                    if (state != EStates.NSW)
                    {
                        await connection.OpenAsync();
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("DriverNumbers", string.Join(",", driverNumbers));
                        var sql = @"SELECT id,
                                            DriverNumber,
                                            GpsDateTime,
                                            Latitude,
                                            Longitude,
                                            Speed,
                                            DriveDate,
                                            distance
                                        FROM [apps].[gccd].[" + tableName + "] WHERE DriverNumber IN (@DriverNumbers) " +
										"AND GpsDateTime BETWEEN '" + startDateTime + "' AND '" + endDateTime + "'";
                        gpsDetails = (List<Gps>)connection.QueryAsync<Gps>(sql, dbArgs).Result;
                        return gpsDetails;
                    }
                    else
                    {
                        await connection.OpenAsync();
                        var dbArgs = new DynamicParameters();
                        dbArgs.Add("DriverNumbers", string.Join(",", driverNumbers));
                        var sql = @"SELECT id,
                                            DriverNumber,
                                            GpsDateTime,
                                            Latitude,
                                            Longitude,
                                            Speed
                                        FROM [apps].[gccd].[" + tableName + "] WHERE DriverNumber IN (@DriverNumbers) " +
                                        "AND GpsDateTime BETWEEN '" + startDateTime + "' AND '" + endDateTime + "'";
                        gpsDetails = (List<Gps>)connection.QueryAsync<Gps>(sql, dbArgs).Result;
                        return gpsDetails;
                    }
                }
                catch (Exception)
                {
                    //Log
                }
            }
            return null;
        }
    }
}
