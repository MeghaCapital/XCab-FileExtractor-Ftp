using Core;
using Dapper;
using Data;
using Microsoft.Data.SqlClient;

namespace xcab.como.common.Data.Repository
{
	public class ComoActiveStatesRepository : IComoActiveStatesRepository
	{
		public ComoActiveStatesRepository()
		{
			SetupDbConnectionSettings();
		}

		public async Task<ICollection<int>> GetAllComoActiveStates()
		{
			var comoActiveStates = new List<int>();
			using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
			{
				try
				{
					await connection.OpenAsync();
					var dbArgs = new DynamicParameters();
					dbArgs.Add("CurrentDate", DateTime.Now);
					const string sql = @"SELECT [StateId] FROM [como].[ComoActiveStates] 
                                        WHERE ActiveInLive <= @CurrentDate 
                                        AND ActiveFrom <= @CurrentDate";
					comoActiveStates = (List<int>)await connection.QueryAsync<int>(sql, dbArgs);
				}
				catch (Exception e)
				{
					await Logger.Log(
					$"Exception occurred in GetAllComoActiveStates method when identifying como active states. Message: " +
					e.Message, nameof(ComoActiveStatesRepository));
				}
			}
			return comoActiveStates;
		}

		public async Task<bool> IsStateActiveForComo(int statieId, DateTime dateTime)
		{

			bool isStateActive = false;
			using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
			{
				try
				{
					await connection.OpenAsync();
					var dbArgs = new DynamicParameters();
					dbArgs.Add("StateId", statieId);
					dbArgs.Add("ActiveFrom", dateTime);
					const string sql = @"SELECT [Id],[StateId] ,[ActiveFrom] FROM [como].[ComoActiveStates] WHERE ActiveFrom<=@ActiveFrom AND ActiveInLive <= @ActiveFrom AND StateId = @StateId";
					var result = await connection.QueryAsync<ComoActiveStates>(sql, dbArgs);
					if (result.Count() > 0)
						isStateActive = true;
				}
				catch (Exception e)
				{
					await Logger.Log(
					$"Exception occurred in IsStateActiveForComo method when identifying como is active for state ID: {statieId}. Message: " +
					e.Message, nameof(ComoActiveStatesRepository));
				}
			}
			return isStateActive;
		}

		public void SetupDbConnectionSettings()
		{
#if DEBUG && UseLiveDbForTest
            DbSettings.Default.ApplicationSqlDatabaseConnectionString = DbSettings.Default.ApplicationSqlDatabaseConnectionString;
#elif DEBUG
			DbSettings.Default.ApplicationSqlDatabaseConnectionString = DbSettings.Default.XCabDevDatabase;
#endif
		}
	}
}
