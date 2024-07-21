using Core;
using Core.Helpers;
using Dapper;
using Data.Model.Address;
using Data.Entities.Ftp;

using Data.Repository.EntityRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Data.Entities.GenericIntegration;

namespace Data.Repository.EntityRepositories
{

    public class XCabClientIntegrationRepository : IXCabClientIntegrationRepository, IBaseLogging
    {     // lsAccountsToMigrate = (List<AccountsToMigrate>)connection.Query<AccountsToMigrate>(sqlAcc);
        public ICollection<XCabClientIntegration> GetClientIntegrations(int clientIntId = 0)
        {
            IEnumerable<XCabClientIntegration> clientIntegrations = null;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

#if DEBUG

                    var sql =
                       string.Format(@"SELECT Id, Name, ClientCode,StateId,EmailList,AllowConsolidation,IntegrationType,FtpLoginId FROM xCabClientIntegration WHERE Id in ("+ clientIntId + ")");
#else
                    var sql =
                        string.Format(@"SELECT Id, Name, ClientCode,StateId,EmailList,AllowConsolidation,IntegrationType,FtpLoginId FROM xCabClientIntegration WHERE Active=1");
#endif


                    clientIntegrations = (IEnumerable<XCabClientIntegration>)connection.Query<XCabClientIntegration>(sql);

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientIntegration, exception:" + e.Message, Name());
            }
            return clientIntegrations as ICollection<XCabClientIntegration>;
        }

        /* public ICollection<XCabClientIntegration> GetCsvClientIntegration()
         {
             IEnumerable<XCabClientIntegration> csvClientIntegrations = null;
             try
             {
                 using (var connection = new SqlConnection(DbSettings.Default.CcrDatabaseConnectionString))
                 {
                     connection.Open();
                     var sql =
                         string.Format(@"SELECT Id, Name, ClientCode,StateId FROM xCabClientIntegration WHERE Active=1 AND IntegrationType=1");
                     csvClientIntegrations = (IEnumerable<XCabClientIntegration>)connection.Query<XCabClientIntegration>(sql);

                 }
             }
             catch (Exception e)
             {
                 Core.Logger.Log(
                     "Exception Occurred while retrieving data from table: xCabClientIntegration, exception:" + e.Message, Name());
             }
             return csvClientIntegrations as ICollection<XCabClientIntegration>;
         }
 */

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
                        string.Format(@"SELECT ClientId,  Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9, Column10 FROM xCabClientIntegrationCsvColumnMap WHERE ClientId=@ClientId");
                    xCabClientIntegrationCsvColumnMap = (IEnumerable<XCabClientIntegrationCsvColumnMap>)connection.Query<XCabClientIntegrationCsvColumnMap>(sql, dynamicParameters) as ICollection<XCabClientIntegrationCsvColumnMap>;

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientIntegrationCsvColumnMap, exception:" + e.Message, Name());
            }
            return xCabClientIntegrationCsvColumnMap.Any() ? (xCabClientIntegrationCsvColumnMap as List<XCabClientIntegrationCsvColumnMap>)[0] : null;
        }

        public XCabClientIntegrationNfsDetail GetCabClientIntegrationNfsDetails(int clientId)
        {
            ICollection<XCabClientIntegrationNfsDetail> xCabClientIntegrationNfsDetail = null;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ClientId", clientId);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        string.Format(@"SELECT ClientId, UNCPathBookings, UNCPathProcessed,UNCPathError FROM xCabClientIntegrationNfsDetail WHERE ClientId=@ClientId");
                    xCabClientIntegrationNfsDetail =
                        (ICollection<XCabClientIntegrationNfsDetail>)connection.Query<XCabClientIntegrationNfsDetail>(sql, dynamicParameters);

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientIntegrationNfsDetail, exception:" + e.Message, Name());
            }
            return xCabClientIntegrationNfsDetail.Any() ? (xCabClientIntegrationNfsDetail as List<XCabClientIntegrationNfsDetail>)[0] : null;
        }

        public XCabClientIntegration GetDefaultAddressDetails(string clientCode, int stateId)
        {
            XCabClientIntegration xCabClientIntegration = null;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ClientCode", clientCode);
            dynamicParameters.Add("StateId", stateId);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

                    var sql =
                        string.Format(@"SELECT * FROM xCabClientIntegration WHERE ClientCode=@ClientCode AND StateId=@StateId and Active =1");

                    xCabClientIntegration =
                       connection.Query<XCabClientIntegration>(sql, dynamicParameters).FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientIntegrationNfsDetail, exception:" + e.Message, Name());
            }
            return xCabClientIntegration;
        }

        public ICollection<XCabClientIntegration> GetDefaultAddressDetails(List<string> CLientCodes)
        {
            ICollection<XCabClientIntegration> xCabClientIntegrations = null;


            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

                    var sql =
                         $@" SELECT * FROM (SELECT CCI.id , COALESCE(GCI.Name, CCI.Name) Name,  COALESCE(GCI.ClientCode,CCI.ClientCode) ClientCode, CCI.IntegrationType,CCI.StateId, 
                            COALESCE((CASE WHEN CCI.Active = 'False' THEN CCI.Active ELSE GCI.Active END),CCI.Active)  Active,  
                            COALESCE(GCI.DefaultPickupAddressLine1, CCI.DefaultPickupAddressLine1) DefaultPickupAddressLine1,
                            COALESCE(GCI.DefaultPickupAddressLine2, CCI.DefaultPickupAddressLine2) DefaultPickupAddressLine2,
                            COALESCE(GCI.DefaultPickupAddressLine3, CCI.DefaultPickupAddressLine3) DefaultPickupAddressLine3 ,  
                            COALESCE(GCI.DefaultPickupAddressLine4, CCI.DefaultPickupAddressLine4) DefaultPickupAddressLine4,
                            COALESCE(GCI.DefaultPickupAddressLine5, CCI.DefaultPickupAddressLine5) DefaultPickupAddressLine5,
                            COALESCE(GCI.DefaultPickupSuburb, CCI.DefaultPickupSuburb) DefaultPickupSuburb,  
                            COALESCE(GCI.DefaultPickupPostcode, CCI.DefaultPickupPostcode) DefaultPickupPostcode,  
                            CCI.Ref1,CCI.Ref2,CCI.EmailList, 
                            COALESCE(GCI.AllowConsolidation, CCI.AllowConsolidation) AllowConsolidation,  
                            COALESCE(CCi.FtpLoginId, GCI.FtpLoginId) AS  FtpLoginId,GCI.Active SubAccountActive, COALESCE(CCI.ServiceCode,GCI.ServiceCode) ServiceCode,
                            GCI.IsUniqueJobsPerDay IsUniqueJobsPerDay,
                            GCI.NumOfDaysJobsUnique NumOfDaysJobsUnique
                            FROM xCabClientIntegration CCI LEFT JOIN xCabGenericClientIntegration GCI ON CCI.Id = GCI.PrimaryId AND GCI.Active = 1 AND CCI.Active = 1) MQR {DapperHelper.GenerateWhereListForSqlServer(
                             CLientCodes, "clientCode", true)}";

                    xCabClientIntegrations =
                       (ICollection<XCabClientIntegration>)connection.Query<XCabClientIntegration>(sql);

                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientIntegrationNfsDetail, exception:" + e.Message, Name());
            }
            return xCabClientIntegrations;
        }

        public Suburb ValidateSuburb(string SuburbName, string State)
        {
            Suburb Suburb = null;

            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.SecondaryDbConnectionString))
                {
                    connection.Open();

                    var sql = $@" SELECT Name, PostCode, State, Latitude, Longitude, IsMetro
                                FROM TPlus.dbo.Suburbs sb
                                WHERE sb.State = '" + State + "' AND UPPER(sb.Name) = '" + (SuburbName.Length > 19 ? SuburbName.Substring(0, 19).ToUpper() : SuburbName.ToUpper()).ToUpper() + "'";

                    Suburb = (Suburb)connection.QueryFirst<Suburb>(sql);
                }
            }
            catch (Exception)
            {
                //Core.Logger.Log(
                //    "Exception Occurred while retrieving data from table: Suburbs, exception:" + e.Message, Name());
            }
            return Suburb;
        }

        public Suburb ValidatePostcode(string Postcode, string State)
        {
            Suburb Suburb = null;

            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.SecondaryDbConnectionString))
                {
                    connection.Open();

                    var sql = $@" SELECT Name, PostCode, State, Latitude, Longitude, IsMetro
                                FROM TPlus.dbo.Suburbs sb
                                WHERE sb.State = '" + State + "' AND sb.PostCode = " + Postcode.Trim().Substring(0, 4);

                    Suburb = (Suburb)connection.QueryFirst<Suburb>(sql);
                }
            }
            catch (Exception)
            {
                //Core.Logger.Log(
                //    "Exception Occurred while retrieving data from table: Suburbs, exception:" + e.Message, Name());
            }
            return Suburb;
        }

        /// <summary>
        /// Get details of CSV files which comes through FTP
        /// </summary>
        /// <returns></returns>
        public ICollection<XCabClientFtpIntegration> GetFTPToCSVClients()
        {
            IEnumerable<XCabClientFtpIntegration> clientIntegrations = null;
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

#if DEBUG
                    var sql =
                                            string.Format(@"SELECT a.id, a.username, a.password,a.bookingsfoldername,a.processedfoldername,a.errorfoldername,a.bookingschemaname,a.trackingschemaname,a.trackingfoldername, b.StateId
                                      FROM [dbo].[xCabFtpLoginDetails] a
                                      INNER JOIN [dbo].[xCabClientIntegration] b ON a.id = b.FtpLoginId
                                      INNER JOIN [dbo].[xCabClientIntegrationNfsDetail] c ON b.id = c.ClientId
                                      where username = '3powerp'");
#endif
#if !DEBUG
                    var sql =
                       string.Format(@"SELECT a.id, a.username, a.password,a.bookingsfoldername,a.processedfoldername,a.errorfoldername,a.bookingschemaname,a.trackingschemaname,a.trackingfoldername, b.StateId
                                      FROM [dbo].[xCabFtpLoginDetails] a
                                      INNER JOIN  [dbo].[xCabClientIntegration] b ON a.id = b.FtpLoginId
                                      INNER JOIN [dbo].[xCabClientIntegrationNfsDetail] c ON b.id = c.ClientId
                                      WHERE b.FtpToCSV = 1 AND a.active = 1 AND b.Active = 1");
#endif

                    clientIntegrations = (IEnumerable<XCabClientFtpIntegration>)connection.Query<XCabClientFtpIntegration>(sql);
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: xCabClientIntegration, exception:" + e.Message, Name());
            }
            return clientIntegrations as ICollection<XCabClientFtpIntegration>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetFTPToCSVClientsFolder(int clientId, int state)
        {
            var folderPath = "";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("clientId", clientId);
            dynamicParameters.Add("state", state);
            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var sql =
                        string.Format(@"SELECT c.UNCPathBookings 
                                          FROM [dbo].[xCabFtpLoginDetails] a
                                          INNER JOIN  [dbo].[xCabClientIntegration] b ON a.id = b.FtpLoginId
                                          INNER JOIN [dbo].[xCabClientIntegrationNfsDetail] c ON b.id = c.ClientId
                                          WHERE b.FtpToCSV = 1 AND b.StateId = @state AND a.id = @clientId");
                    folderPath = connection.ExecuteScalar<string>(sql, dynamicParameters);
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving FTP to CSV client folder details from table: xCabClientIntegrationNfsDetail, exception:" + e.Message, Name());
            }
            return folderPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool JobAlreadyExists(Booking booking, DateTime date)
        {
            const bool jobExists = false;
            int numOfJobs = 0;

            try
            {
                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();

                    var sql = @"SELECT COUNT(*) FROM [dbo].[xCabBooking] A " +
                                " WHERE A.Cancelled = 0 " +
                                " AND CONVERT(Date,A.DateInserted) >= '" + date.ToString("yyyy-MM-dd") + "'" +
                                " AND A.AccountCode = '" + booking.AccountCode + "'" +
                                " AND A.StateId = " + booking.StateId +
                                " AND A.LoginId = " + booking.LoginDetails.Id +
                                " AND A.Ref1 = '" + booking.Ref1 + "'";
                    if (!string.IsNullOrEmpty(booking.Ref2))
                        sql += " AND A.Ref2 = '" + booking.Ref2 + "'";

                    numOfJobs = connection.QueryFirst<int>(sql);
                    if (numOfJobs > 0)
                        return true;
                }
            }
            catch (Exception e)
            {
                Core.Logger.Log(
                    "Exception Occurred while retrieving data from table: job deatils, exception:" + e.Message, Name());
            }
            return jobExists;
        }

        public string Name()
        {
            return this.GetType().Name;
        }
    }
}
