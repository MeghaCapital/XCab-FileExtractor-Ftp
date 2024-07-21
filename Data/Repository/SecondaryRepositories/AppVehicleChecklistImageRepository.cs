using Core;
using Dapper;
using Data.Entities.Ilogix;
using Data.Model;
using Data.Repository.SecondaryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Data.Model.Poc;
using PocImage = Data.Entities.Ilogix.PocImage;

namespace Data.Repository.SecondaryRepositories
{
    public class AppVehicleChecklistImageRepository : IAppVehicleChecklistImageRepository
    {
        public TplusWebApi GetImage(int imageId)
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                try
                {
                    connection.Open();
                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("ImageId", imageId);
                    const string sql = @"SELECT 
	                                        [i].[Image] AS Image,
											[r].[JobNumber] AS JobNumber,
											[r].[SubJobNumber] AS SubJobNumber,
											[g].[StateId] AS State,
											[r].[DateTimeSelected] As JobDate
                                        FROM 
	                                        [images].[driver].[VehicleChecklistImage] [i]
	                                        LEFT OUTER JOIN [driver].[VehcileChecklistResponse] [r] on [i].[ChecklistResponseId] = [r].[id]
	                                        LEFT OUTER JOIN [driver].[VehicleChecklist] [c] on [r].[ChecklistId] = [c].[id]
	                                        LEFT OUTER JOIN [driver].[VehicleGroup] [g] on [c].[VehicleGroupId] = [g].[id]	
                                        WHERE 
	                                        [i].[id] = @ImageId";
                    return connection.Query<TplusWebApi>(sql, dbArgs).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Logger.Log("Exception occurred while extracting checklist images via booking details : " + "Image " + imageId.ToString() + ". Message :" + ex.Message, "AppVehicleChecklistImageRepository");
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="jobnumber"></param>
        /// <param name="responseDate"></param>
        /// <param name="subJobFilter"></param>
        /// <returns></returns>
        public ICollection<PocImage> GetCheckListImage(int stateId, string jobnumber, DateTime responseDate, checkListSubJobFilter subJobFilter = checkListSubJobFilter.All)
        {
            var checkListImages = new List<PocImage>();

            using (var Nt12sqlConnection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString + "; Connection Timeout = 60"))
            {
                try
                {
                    Nt12sqlConnection.Open();

                    var dbArgs = new DynamicParameters();
                    dbArgs.Add("stateId", stateId);
                    dbArgs.Add("jobnumber", jobnumber);
                    dbArgs.Add("responseDate", responseDate.ToString("yyyy-MM-dd"));

                    var sql = @"SELECT 
                                            [i].[Image] AS pocImage,
                                            [r].[JobNumber] AS TPLUS_JobNumber,
                                            [r].[SubJobNumber] AS SubJobNumber,
                                            [g].[StateId] AS State,
                                            [r].[DateTimeSelected] As JobDateTime
                                        FROM
                                            [Images].[driver].[VehicleChecklistImage][i]
                                            LEFT OUTER JOIN [driver].[VehcileChecklistResponse][r] on[i].[ChecklistResponseId] = [r].[id]
                                            LEFT OUTER JOIN [driver].[VehicleChecklist][c] on[r].[ChecklistId] = [c].[id]
                                            LEFT OUTER JOIN [driver].[VehicleGroup][g] on[c].[VehicleGroupId] = [g].[id]
                                        WHERE
                                            [g].StateId = @stateId AND[r].JobNumber = @jobnumber AND CONVERT(date, [r].[ResponseDate]) = @responseDate";

                    if (subJobFilter == checkListSubJobFilter.PickUp)
                        sql += " AND[r].SubJobNumber = 1 ";
                    else if (subJobFilter == checkListSubJobFilter.Delivery)
                        sql += " AND[r].SubJobNumber = 2 ";

                    checkListImages = Nt12sqlConnection.Query<PocImage>(sql, dbArgs).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Log("Exception Occurred in ILogixImagesRepository: GetCheckListImage, message: " + ex.Message, "ILogixImagesRepository");
                }
            }
            return checkListImages;
        }
    }
}
