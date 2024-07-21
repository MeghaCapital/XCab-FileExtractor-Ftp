using Core;
using Dapper;
using Data.Model.Driver.DriverImages;
using Data.Repository.EntityRepositories.Driver.Interface;
using System;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data.Repository.EntityRepositories.Driver
{
    public class DriverPayloadsRepository : IDriverPayloadsRepository
    {
        public bool ProcessPayload(PodModel pod)
        {
            var result = false;

            #region Setup the parameters
            // if the record exists, update otherwise insert 
            var dbArgs = new DynamicParameters();
            dbArgs.Add("JobNumber", pod.JobNumber);
            dbArgs.Add("SubJobNumber", pod.SubJobNumber);
            dbArgs.Add("AccountCode", pod.AccountCode);
            dbArgs.Add("PODDate", pod.PODDate);
            dbArgs.Add("DomicileState", pod.DomicileState);
            #endregion Setup the parameters

            //get a list of accounts that we need to create notifications
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();

                    #region Check for any rows in table
                    var sql = "SELECT COUNT(*) FROM " +
                                 "emp.DriverImages d " +
                                 "  WHERE " +
                                 " d.JobNumber = @JobNumber " +
                                 "  AND d.SubJobNumber = @SubJobNumber " +
                                 "  AND d.AccountCode = @AccountCode " +
                                 "  AND d.PODDate = @PODDate " +
                                 "  AND d.DomicileState = @DomicileState";

                    var itemcnt = connection.ExecuteScalar<int>(sql, dbArgs);

                    #endregion Check for any rows in table

                    #region additional args
                    // setup args for either update or insert
                    dbArgs.Add("PODName", pod.PodName);
                    dbArgs.Add("BASE64Image", pod.Base64Image);
                    dbArgs.Add("DriverNumber", pod.DriverNumber);
                    dbArgs.Add("ImageType", pod.ImageType);
                    #endregion additional args

                    // if more than one record, update the row otherwise insert the new record 
                    if (itemcnt > 0)
                    {
                        #region Update an existing row
                        // update 
                        sql = "UPDATE emp.DriverImages " +
                              " SET PodName = @PODName, " +
                              " Base64Image = @BASE64Image " +
                              "  WHERE " +
                              "  JobNumber = @JobNumber " +
                              "     AND SubJobNumber = @SubJobNumber " +
                              "     AND AccountCode = @AccountCode " +
                              "     AND PODDate = @PODDate " +
                              "     AND DomicileState = @DomicileState";
                        try
                        {
                            // call the query to update rows.
                            result = connection.Execute(sql, dbArgs) > 0;
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception Occurred in DriverPayloadsRepository: Update Existing Row, message: " +
                                e.Message, nameof(DriverPayloadsRepository));
                        }
                        #endregion Update an existing row
                    }
                    else
                    {
                        #region Insert a new row
                        // insert new row
                        sql = "INSERT INTO emp.DriverImages (JobNumber, SubJobNumber, " +
                                "DriverNumber, DomicileState, AccountCode, PODDate, ImageType, PodName, BASE64Image) " +
                              " VALUES ( " +
                              " @JobNumber " +
                              " ,@SubJobNumber " +
                              " ,@DriverNumber" +
                              " ,@DomicileState" +
                              " ,@AccountCode " +
                              " ,@PODDate " +
                              " ,@ImageType " +
                              " ,@PODName " +
                              " ,@BASE64Image )";

                        try
                        {
                            // call the query to update rows.
                            result = connection.Execute(sql, dbArgs) > 0;
                        }
                        catch (Exception e)
                        {
                            Logger.Log(
                                "Exception Occurred in DriverPayloadsRepository: Insert New Row, message: " +
                                e.Message, "DriverPayloadsRepository");

                        }
                        #endregion End Insert new Row

                    }

                }
                catch (Exception e)
                {
                    Logger.Log(
                        "Exception Occurred in DriverPayloadsRepository: ProcessPayload, message: " +
                        e.Message, nameof(DriverPayloadsRepository));

                }
            }

            return result;
        }


        public DriverPayload GetDriverPayload(string accountCode, string domicileState, string jobNumber, string subJobNumber, DateTime jobDateTime)
        {
            DriverPayload driverPayloads = null;
            //get a list of accounts that we need to create notifications
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                connection.Open();
                var dbArgs = new DynamicParameters();
                dbArgs.Add("JobNumber", jobNumber.PadLeft(8, '0'));
                dbArgs.Add("SubJobNumber", subJobNumber);
                dbArgs.Add("AccountCode", accountCode);
                dbArgs.Add("DomicileState", domicileState);
                dbArgs.Add("JobDateTime", jobDateTime);
                const string sql = @"SELECT Base64Image, PodName,PodDate, DriverNumber FROM
									emp.DriverImages WHERE JobNumber = @JobNumber
									AND SubJobNumber = @SubJobNumber AND AccountCode=@AccountCode
									AND ImageType='S' AND convert(date,PODDate) >= DATEADD(DAY,-2,convert(Date,@jobDateTime))
									AND convert(date,PODDate) <= DATEADD(DAY,5,convert(Date,@jobDateTime))";

                driverPayloads = connection.Query<DriverPayload>(sql, dbArgs).FirstOrDefault();
            }
            return driverPayloads;
        }
    }
}
