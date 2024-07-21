using Data.Entities.Ftp;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Core;

namespace Data.Repository.V2;

public class RemoteFtpConfiguration : IRemoteFtpConfiguration
{
 
    public async Task<List<XcabRemoteConfiguration>> GetConfigurationsAsync()
    {
        List<XcabRemoteConfiguration> remoteConfigurations;

        try
        {
			using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString)) {
				await connection.OpenAsync();

				var result = await connection.QueryAsync <XcabRemoteConfiguration>(_SQL_GETCONFIGURATIONS);
				remoteConfigurations = result.ToList();
			}
        } catch (Exception e)
		{			
			remoteConfigurations = new List<XcabRemoteConfiguration>();
			await Logger.Log("Exception occurred when extracting remote FTP configurations, message: " +
								   e.Message, nameof(RemoteFtpConfiguration));
		}

		return remoteConfigurations;
    }   
	
	const string _SQL_GETCONFIGURATIONS = @"
		SELECT 
			c.loginid,
			c.ProdHostname,
			c.ProdRootPath,
			c.ProdUsername,
			c.ProdPassword,
			c.testhostname,
			c.TestRootPath,
			c.TestUsername,
			c.TestPassword,
			c.UseTest,
			a.SchemaType,
			a.RemoteFtpActionType,
			a.SubFtpPath
		FROM 
			xCabRemoteFtpConfig c
			JOIN xCabRemoteFtpActions a on a.LoginId = c.LoginId
			JOIN xCabFtpLoginDetails ld ON ld.id = c.LoginId
		WHERE
			a.IsActive = 1";

}
