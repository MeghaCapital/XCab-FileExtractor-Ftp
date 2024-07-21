using Dapper;
using Data.Entities.Futile;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.V2
{
	public class ILogixFutileJobRepository : IILogixFutileJobRepository
	{
		public async Task<ICollection<JobFutile>> GetFutileJobDetails(string consignmentNumber)
		{
			var futileJobDetails = new List<JobFutile>();
			try
			{
				using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))				
				{
					await connection.OpenAsync();
					var sql = $@"SELECT [Id] 
                                    ,[AccountCode]
                                    ,[JobNumber]
                                    ,[SubJobNumber]
                                    ,[StateId]
                                    ,[FutileType]
                                    ,[DriverNumber]
                                    ,[GeneratedFutileJobNumber]
                                    ,[Ref1]
                                    ,[Ref2]
                                    ,[ConsignmentNumber]
                                    ,[IsUltimateOfBatch]
                                FROM [dbo].[JobFutile]
                                WHERE ConsignmentNumber LIKE '%{consignmentNumber}%'
								ORDER BY LastUpdated ASC";
					
					futileJobDetails = (List<JobFutile>)await connection.QueryAsync<JobFutile>(sql);

				}
			}
			catch (Exception)
			{ 
				//log
			}
			return futileJobDetails;
		}
	}
}
