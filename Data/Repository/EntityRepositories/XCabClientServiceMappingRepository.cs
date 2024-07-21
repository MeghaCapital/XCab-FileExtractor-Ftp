using Dapper;
using Data.Entities.Services;
using Data.Repository.EntityRepositories.Interfaces;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories
{
    public class XCabClientServiceMappingRepository : IXCabClientServiceMappingRepository
    {
        public ICollection<XCabClientServiceMapping> GetClientServiceMappings(int LoginId)
        {
            ICollection<XCabClientServiceMapping> xCabClientServiceMapping = null;
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("LoginId", LoginId);
                const string sql = @"
                                SELECT Id, LoginId, StateId, ServiceName, ServiceDescription FROM XCabClientServiceMapping WHERE LoginId=@LoginId
                    ";
                xCabClientServiceMapping = connection.Query<XCabClientServiceMapping>(sql, dynamicParams).ToList();
            }
            return xCabClientServiceMapping;
        }
    }
}
