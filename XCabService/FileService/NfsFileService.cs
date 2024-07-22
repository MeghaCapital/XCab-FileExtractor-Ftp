using Microsoft.Extensions.Logging;
using Quartz.Util;

namespace XCabService.FileService;

public class NfsFileService : INfsFileService
{
    private readonly ILogger<NfsFileService> _logger;

    public NfsFileService(ILogger<NfsFileService> logger)
    {
        _logger = logger;
    }

    public NfsFileService()
    {
        
    }

    /// <summary>
    /// Download a file to a remote share folder. Will fail if the file already exists
    /// </summary>
    /// <param name="fileNameToDownload">File name with extension to fetch - excluding path</param>
    /// <param name="sourceShareLocation">Path to remote destination</param>
    /// <param name="destinationLocation">Path to local destination</param>
    /// <returns>Success or failure</returns>
    public bool DownloadFile(string fileNameToDownload, string destinationLocation)
    {
        bool isSuccessful;

        try
        {
			if (!destinationLocation.EndsWith("\\"))
			{
                destinationLocation += "\\";
			}

			isSuccessful = UploadFile(fileNameToDownload, destinationLocation);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred when trying to downloading a file");
            isSuccessful = false;
        }

        return isSuccessful; 
    }

    /// <summary>
    /// Retrieve a list of file names from a remote folder
    /// </summary>
    /// <param name="shareLocation"></param>
    /// <param name="fileExtensionToSearch"></param>
    /// <param name="getEverything"></param>
    /// <returns>ICollection element of file names with paths. Null on failure</returns>
    public ICollection<string>? GetFileListing(string shareLocation, string fileExtensionToSearch)
    {
        var fileList = new List<string>();

        try
        {
            var useExtensionFilter = !fileExtensionToSearch.IsNullOrWhiteSpace();

            if (!Directory.Exists(shareLocation))
            {
                _logger.LogError("The source folder does not exist");
                return null;
            }

            fileList = Directory.GetFiles(shareLocation).ToList();

            if (useExtensionFilter && fileExtensionToSearch != "*")
            {
                fileList = fileList.FindAll(x => x.EndsWith(fileExtensionToSearch));
            }
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex,"An exception occurred while trying to obtain a file listing");
            return null;
        }

        return fileList;
    }

    /// <summary>
    /// Upload a file to a remote share folder. Will fail if the file already exists
    /// </summary>
    /// <param name="fileNameWithLocationToDownload">Name and path of file to transfer</param>
    /// <param name="destinationLocation">Path to remote destination</param>
    /// <returns>Success or failure</returns>
    public bool UploadFile(string fileNameWithLocationToDownload, string destinationLocation)
    {
        bool isSuccessful = true;

        try
        {
            if (!File.Exists(fileNameWithLocationToDownload) || !Directory.Exists(destinationLocation))
            {
                _logger.LogError("Either the source file or destination folder does not exist");
                isSuccessful = false;
            } else
            {
                var fileNameToDownload = Path.GetFileName(fileNameWithLocationToDownload);
                destinationLocation = Path.Combine(destinationLocation, fileNameToDownload);
                File.Move(fileNameWithLocationToDownload, destinationLocation);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred when trying to moving file {Path.GetFileName(fileNameWithLocationToDownload)} to {Path.GetFullPath(destinationLocation)}");
            isSuccessful = false;
        }

        return isSuccessful;
    }

    public bool MoveFileWithTimestamp(string sourceFileName, string destinationFileName)
    {
        bool isSuccessful = true;
		try
		{
            if (File.Exists(sourceFileName) && !string.IsNullOrEmpty(destinationFileName))
            {
                string uploadTime = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" +
                                    DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" +
                                    DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Millisecond.ToString();
                File.Move(sourceFileName, destinationFileName + "." + uploadTime);
            }
        }
		catch (Exception e)
		{
            _logger.LogError("Exception occurred when trying to upload processed file to destination location. Message: " + e.Message);
            isSuccessful = false;
        }
        return isSuccessful;
    }

    public bool CopyFileWithTimestamp(string sourceFileName, string destinationFileName)
    {
        bool isSuccessful = true;

        try
        {
            if (File.Exists(sourceFileName) && !string.IsNullOrEmpty(destinationFileName))
            {
                string uploadTime = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" +
                                    DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" +
                                    DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Millisecond.ToString();
                File.Copy(sourceFileName, destinationFileName + "." + uploadTime);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred when trying to copy file {Path.GetFileName(sourceFileName)} to {Path.GetFullPath(destinationFileName)}");
            isSuccessful = false;
        }

        return isSuccessful;
    }

    public bool MoveFileWithoutTimestamp(string sourceFileName, string destinationFileName)
    {
        bool isSuccessful = true;
        try
        {
            if (File.Exists(sourceFileName) && !string.IsNullOrEmpty(destinationFileName))
            {
                File.Move(sourceFileName, destinationFileName);
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Exception occurred when trying to upload processed file to destination location without timestamp. Message: " + e.Message);
            isSuccessful = false;
        }
        return isSuccessful;
    }

    public bool CopyFileWithoutTimestamp(string sourceFileName, string destinationFileName)
    {
        bool isSuccessful = true;

        try
        {
            if (File.Exists(sourceFileName) && !string.IsNullOrEmpty(destinationFileName))
            {
                File.Copy(sourceFileName, destinationFileName);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred when trying to copy file without timestamp {Path.GetFileName(sourceFileName)} to {Path.GetFullPath(destinationFileName)}");
            isSuccessful = false;
        }

        return isSuccessful;
    }
}
