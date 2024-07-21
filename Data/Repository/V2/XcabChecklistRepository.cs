using Core;
using Data.Entities.Ftp;
using Data.Model.Checklist;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Data.Repository.V2;

public class XcabChecklistRepository : IXcabChecklistRepository
{
    const string _IMAGE_TYPE = "CHECKLIST";
    const string _SQL_CHECKLISTIMAGE = @"
		SELECT
			[vci].[Image] 'Image',
			@TypeText 'Type'
		FROM 
			xcab.driver.VehcileChecklistResponse vcr
			LEFT JOIN [Xcab].[driver].[VehicleChecklist] vcl ON vcl.id = vcr.ChecklistId
			LEFT JOIN [Xcab].[Driver].[VehicleGroup] vcg ON vcg.id = vcl.VehicleGroupId	
			LEFT JOIN [Images].[driver].[VehicleChecklistImage] vci ON vci.ChecklistResponseId = vcr.Id

		WHERE
			vcr.jobnumber = @JobNumber
			AND vcr.SubJobNumber = @SubJobNumber
			AND vcr.ResponseDate = @JobDate
			AND vcg.StateId = @JobState
        ";

    public async Task<ICollection<ChecklistImageResponse>> ExtractChecklistImagesAsync(string jobNumber, int legNumber, DateTime jobDate, EStates stateId)
    {
        List<ChecklistImageResponse> checklistImages = new();

        int jobNumberInt;
        int.TryParse(jobNumber,out jobNumberInt);

        try
        {
            using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
            {
                await connection.OpenAsync();

                var queryArgs = new DynamicParameters();

                queryArgs.Add("TypeText",_IMAGE_TYPE);
                queryArgs.Add("JobNumber", jobNumberInt);
                queryArgs.Add("SubJobNumber",legNumber);
                queryArgs.Add("JobDate",jobDate);
                queryArgs.Add("JobState",(int)stateId);

                checklistImages = (await connection.QueryAsync<ChecklistImageResponse>(_SQL_CHECKLISTIMAGE,queryArgs)).ToList();
            }
        }
        catch (Exception e)
        {
            _ = Logger.Log("Exception occurred when extracting remote FTP configurations, message: " +
                                    e.Message, nameof(RemoteFtpConfiguration));
        }

        return checklistImages;
      
    }
}
