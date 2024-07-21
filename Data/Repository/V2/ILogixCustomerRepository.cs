using Core;
using Data.Model.Tracking.ILogix;
using MySql.Data.MySqlClient;
using System.Data;

namespace Data.Repository.V2
{
	public class ILogixCustomerRepository : IILogixCustomerRepository
	{
		public async Task<ILogixCustomer> GetCustomerByName(string customerName)
		{
			var customer = new ILogixCustomer();
			var sql = $@"
                        SELECT
	                        c.CustomerCode AS CustomerCode, 
	                        c.ClientCode AS ClientCode 
                        FROM 
	                        customer c 
                        WHERE 
	                        CustomerName = '{customerName}'";

			var connectionString = DbSettings.Default.ILogixMysqlConnection;
			using (var sqlConnection = new MySqlConnection(connectionString))
			{
				try
				{
					await sqlConnection.OpenAsync();
					using var cmd = new MySqlCommand(sql, sqlConnection);
					using var reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						customer.ClientCode = Convert.ToInt32(reader["ClientCode"].ToString());
						customer.CustomerCode = Convert.ToInt32(reader["CustomerCode"].ToString());
						break;
					}
					if (!reader.IsClosed)
						reader.Close();
				}
				catch (Exception ex)
				{
					Logger.Log("Exception Occurred in ILogixCustomerRepository: GetCustomerByName, message: " + ex.Message, nameof(ILogixCustomerRepository));
				}
				finally
				{
					if (sqlConnection.State == ConnectionState.Open)
					{
						sqlConnection.Close();
					}
				}
			}

			return customer;
		}
	}
}
