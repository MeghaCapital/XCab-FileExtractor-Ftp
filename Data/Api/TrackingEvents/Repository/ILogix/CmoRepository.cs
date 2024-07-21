using Core;
using Data.Api.TrackingEvents.Model.ILogix;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.TrackingEvents.Repository.ILogix
{
    public class CmoRepository: ICmoRepository
    {
        public CmoDriverRow GetCmoDriverRow(int driverNumber)
        {
            var cmoDriverRow = new CmoDriverRow();
            
            if (driverNumber == 0 || driverNumber == -1)
                return cmoDriverRow;
            var sql = "SELECT Latitude, Longitude,GPSTime FROM CMO WHERE MobileId=" + driverNumber + " and Modified>=" + (int)DateTime.Now.ToOADate();
            var connectionString = DbSettings.Default.ILogixMysqlConnection;
            var sqlConnection = new MySqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                using (var cmd = new MySqlCommand(sql, sqlConnection))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cmoDriverRow.Latitude = reader["Latitude"].ToString();
                        cmoDriverRow.Longitude = reader["Longitude"].ToString();
                        cmoDriverRow.GpsDateTime = DateTime.FromOADate(Convert.ToDouble(reader["GPSTime"].ToString()));
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log("Exception Occurred in CmoRepository: GetCmoDriverRow, message: " + ex.Message, nameof(CmoRepository));
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return cmoDriverRow;
        }
    }
}
