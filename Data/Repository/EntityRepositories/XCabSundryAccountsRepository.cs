using Dapper;
using Data.Entities.Sundries;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories
{
    public class XCabSundryAccountsRepository : IXCabSundryAccountsRepository
    {
        public bool? Exists(string accountCode, int stateId, string sundryCode)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    const string sql = @"
                                        SELECT 
	                                        [Id], 
	                                        [ChargePerSundry] 
                                        FROM 
	                                        [xCabSundryAccounts] 
                                        WHERE 
	                                        [Active] = 1
	                                        AND [AccountCode] = @AccountCode
	                                        AND [StateId] = @StateId
	                                        AND [SundryCode] = @SundryCode
                                        ";
                    return ((ICollection<XCabSundry>)connection.Query<XCabSundry>(sql, new { AccountCode = accountCode, StateId = stateId, SundryCode = sundryCode })).Count == 1;
                    // Check 
                    // return ((ICollection<Sundry>)connection.Query<Sundry>(sql, new { AccountCode = accountCode, StateId = stateId, SundryCode = sundryCode })).Count == 1;
                }
                catch (Exception e)
                {
                    Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabSundryAccounts, method: Exists, exception:" + e.Message, "XCabSundryAccountsRepository");
                }
            }
            return null;
        }
    }
}
