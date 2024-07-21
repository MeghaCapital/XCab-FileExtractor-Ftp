using Data.Model.ServiceCodes;
using Data.Repository.EntityRepositories.Service.Interface;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Core;
using Core.Models.Slack;

namespace Data.Repository.EntityRepositories.Service
{
    public class HandlingTypeRepository : IHandlingTypeRepository
    {
        public ICollection<ManualLiftRestrictions> GetManualLiftRestrictions()
        {
            var manualLiftRestrictions = new List<ManualLiftRestrictions>();
            try
            {
                string sql = "SELECT ItemDescription, COALESCE(MaximumDimension,0) MaximumDimension , COALESCE(MedianDimension,0) MedianDimension ,COALESCE(MinimumDimension,0) MinimumDimension ,COALESCE(ItemWeight,0) ItemWeight FROM [dbo].[xcabManualLiftRestrictions]";

                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    manualLiftRestrictions = connection.Query<ManualLiftRestrictions>(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogSlackErrorFromApp("RESTful Web Service: ServiceCodesRepository",
                           $"Error while retrieving manual lift restrictions. Message: {ex.Message}, Service request:",
                           "RESTful Web Service:ServiceCodesRepository", SlackChannel.WebServiceErrors);
            }
            return manualLiftRestrictions;
        }
    }
}
