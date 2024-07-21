using Dapper;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories.CustomerDelSuburbs
{
	public class XCabCustomerDelSuburbsRepository : IXCabCustomerDelSuburbsRepository
	{

		public async Task<bool> IsDelSuburbValid(int LoginId, string fromSuburb, string toSuburb, string fromPostcode, string toPostcode, int distance, string storeName)
		{
			bool Valid = true;
			var dynamicParameters = new DynamicParameters();
			if (!string.IsNullOrEmpty(fromSuburb) && !string.IsNullOrEmpty(toSuburb) && !string.IsNullOrEmpty(fromPostcode) && !string.IsNullOrEmpty(toPostcode))
			{
				dynamicParameters.Add("LoginId", LoginId);
				dynamicParameters.Add("FromSuburb", fromSuburb);
				dynamicParameters.Add("ToSuburb", toSuburb);
				dynamicParameters.Add("FromPostcode", fromPostcode);
				dynamicParameters.Add("ToPostcode", toPostcode);
				dynamicParameters.Add("Distance", distance);

			}
			try
			{
				using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
				{
					await connection.OpenAsync();
					const string sql = @"SELECT * FROM xCabCustomerDelSuburbs WHERE LoginId=@LoginId
                    AND FromSuburb=@FromSuburb AND FromPostcode = @FromPostcode AND ToSuburb = @ToSuburb AND ToPostcode = @ToPostcode AND Distance<=@Distance";
					int rows = await connection.ExecuteScalarAsync<int>(sql, dynamicParameters);
					if (rows == 0)
						Valid = false;

				}
			}
			catch (Exception e)
			{
				Core.Logger.Log(
					"Exception Occurred while retrieving data from table: IsDelSuburbValid, exception:" + e.Message, "XCabCustomerDelSuburbsRepository");
			}
			return Valid;
		}
	}
}
