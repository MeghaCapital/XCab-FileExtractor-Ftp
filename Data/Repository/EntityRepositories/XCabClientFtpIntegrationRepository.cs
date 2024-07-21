using Core;
using Dapper;
using Data.Entities.Ftp;
using Data.Entities.GenericIntegration;
using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Data.Repository.EntityRepositories
{
    public class XCabClientFtpIntegrationRepository : IXCabClientFtpIntegrationRepository
    {
        public ICollection<XCabClientFtpIntegration> GetClientFtpIntegrations()
        {
            IEnumerable<XCabClientFtpIntegration> xCabClientFtpIntegrations = null;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        @"SELECT Id, Username, Password, BookingsFolderName, TrackingFolderName, TrackingSchemaName, Active, StateId, ClientCode FROM xCabClientFtpIntegration WHERE Active=1";
                    xCabClientFtpIntegrations = connection.Query<XCabClientFtpIntegration>(sql);
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while retrieving data from table: XCabClientFtpIntegration, exception:" +
                    e.Message, Name());
            }
            return (ICollection<XCabClientFtpIntegration>)xCabClientFtpIntegrations;
        }

        public ICollection<XCabClientFtpIntegration> GetClientFtpCsvIntegrations()
        {
            IEnumerable<XCabClientFtpIntegration> xCabClientFtpIntegrations = null;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        @"SELECT Id, Username, Password, BookingsFolderName, TrackingFolderName, TrackingSchemaName, Active, StateId, ClientCode 
                        FROM xCabClientFtpIntegration WHERE Active=1 AND LOWER(trackingschemaname) LIKE'csv%'";
                    xCabClientFtpIntegrations = connection.Query<XCabClientFtpIntegration>(sql);
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while retrieving data from table: XCabClientFtpIntegration, exception:" +
                    e.Message, Name());
            }
            return (ICollection<XCabClientFtpIntegration>)xCabClientFtpIntegrations;
        }

        public XCabClientIntegrationCsvColumnMap GetClientIntegrationCsvColumnMaps(int clientId)
        {
            ICollection<XCabClientIntegrationCsvColumnMap> xCabClientIntegrationCsvColumnMap = null;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ClientId", clientId);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        @"SELECT ClientId,  Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9, Column10, Column11,Column12,
                            Column13, Column14, Column15, Column16, column17, column18, column19, column20, column21, column22, column23, column24, column25,
                            column26, column27, column28, column29, column30, column31, column32 FROM xCabClientIntegrationCsvColumnMap WHERE ClientId=@ClientId";
                    xCabClientIntegrationCsvColumnMap =
                        connection.Query<XCabClientIntegrationCsvColumnMap>(sql, dynamicParameters) as
                            ICollection<XCabClientIntegrationCsvColumnMap>;
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientIntegrationCsvColumnMap, exception:" +
                    e.Message, Name());
            }
            return (xCabClientIntegrationCsvColumnMap as List<XCabClientIntegrationCsvColumnMap>)[0];
        }

        private string Name()
        {
            return GetType().Name;
        }
    }
}
