using Core;
using Dapper;
using Data.Model;
using Data.Properties;
using Microsoft.Data.SqlClient;

namespace Data.Repository.V2
{
    public class XCabTrackingRepository : IXCabTrackingRepository
    {
        public async Task<ICollection<ToshibaTrackingDetail>> GetToshibaEventsToUpdate()
        {
            var toshibaTrackingEvents = new List<ToshibaTrackingDetail>();
            try
            {
				;
				using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
				{
                    await connection.OpenAsync();

					toshibaTrackingEvents = (List<ToshibaTrackingDetail>)(connection.QueryAsync<ToshibaTrackingDetail>(Resources.ToshibaTrackingEvents).Result);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Exception Occurred while extracting tracking events from xCab for Toshiba, Details: {ex.Message.ToString()}", GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return toshibaTrackingEvents;
        }
    }
}
