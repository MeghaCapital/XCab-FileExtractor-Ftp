using Core;
using Core.Helpers;
using Core.Models.Slack;
using Data.Model;

namespace XCabService.FileService
{
    public class TrackingFileCreator
    {
        private const string FileLocation = @"\\challengelogistics.com.au\National\FTP\Home";
        private const string FileExtension = "xml";
        public static bool CreateTrackingFile(List<XCabTrackingFileContentResponse> xCabTrackingFileContentResponses)
        {
            var isSuccessful = true;

            foreach (var xCabTrackingFileContentResponse in xCabTrackingFileContentResponses)
            {
                try
                {
                    if (xCabTrackingFileContentResponse != null && xCabTrackingFileContentResponse.TrackingResponse != null && xCabTrackingFileContentResponse.LoginDetails != null)
                    {
                        var loginDetails = xCabTrackingFileContentResponse.LoginDetails;
                        var accountCode = xCabTrackingFileContentResponse.TrackingResponse.AccountCode;
                        if (xCabTrackingFileContentResponse.LoginDetails.IsRemotePushEnabled)
                        {
                            var remoteFtpHostName = loginDetails.RemoteFtpHostName;
                            var remoteFtpUserName = loginDetails.RemoteFtpUserName;
                            var remoteFtpPassword = loginDetails.RemoteFtpPassword;
                            var remoteTrackingFolderName = loginDetails.RemoteTrackingFolderName;
                            var outputFileName = @$"{accountCode}_{FileNameHelper.GetDateTimeForFile()}.{FileExtension}";
                            // To Do: Currently Logger.Log logs only exceptions or soap request to tplus in release mode. Adding logs at LogSlackNotificationFromApp which may remove in future.
                            Logger.Log($"Created tracking file: {outputFileName} for username: {remoteFtpUserName} at remote Ftp location: {remoteFtpHostName}, tracking folder name {remoteTrackingFolderName}", Name());
                            Logger.LogSlackNotificationFromApp("XCAB",
                              $"Created tracking file: {outputFileName} for username: {remoteFtpUserName} at remote Ftp location: {remoteFtpHostName}, tracking folder name {remoteTrackingFolderName}",
                              Name(), SlackChannel.WebServiceLogs);

                            // TO DO: Need to refactor / relocate FtpManager to avoid circular references. 
                            //FtpManager.UploadFile(remoteFtpHostName, outputFileName,
                            //        remoteFtpUserName, remoteFtpPassword,
                            //        remoteTrackingFolderName, false);
                        }
                        else
                        {
                            var username = loginDetails.UserName;
                            var trackingFilePath = loginDetails.TrackingFolderName;
#if DEBUG
                            var filePath = Path.Combine(@"c:\temp\", username, trackingFilePath);
#else
                            var filePath = Path.Combine(FileLocation, username, trackingFilePath);
#endif
                            var fileName = @$"{filePath}\{accountCode}_{FileNameHelper.GetDateTimeForFile()}.xml";
                            xCabTrackingFileContentResponse.TrackingResponse.SaveToFile(fileName);
                            // To Do: Currently Logger.Log logs only exceptions or soap request to tplus in release mode. Adding logs at LogSlackNotificationFromApp which may remove in future.
                            Logger.Log($"Created tracking file: {fileName} for username: {username}", Name());
                            Logger.LogSlackNotificationFromApp("XCAB",
                              $"Created tracking file: {fileName} for username: {username}",
                              Name(), SlackChannel.WebServiceLogs);
                        }
                    }
                    else if (xCabTrackingFileContentResponse != null && xCabTrackingFileContentResponse.LoginDetails != null && xCabTrackingFileContentResponse.TrackingResponse == null)
                    {
                        Logger.LogSlackNotificationFromApp("XCAB",
                             $"No tracking file created for username: {xCabTrackingFileContentResponse.LoginDetails.UserName} as response contents are missing.",
                             Name(), SlackChannel.GeneralErrors);
                    }
                }
                catch (Exception ex)
                {
                    isSuccessful = false;
                    Logger.LogSlackNotificationFromApp("XCAB",
                              $"Error while creating tracking file. Details are {ex.Message}",
                              Name(), SlackChannel.GeneralErrors);
                }
            }
            return isSuccessful;
        }

        private static string Name()
        {
            return nameof(TrackingFileCreator);
        }
    }
}
