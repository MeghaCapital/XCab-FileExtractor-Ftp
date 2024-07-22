using Core;
using Core.Helpers;
using Core.Logging.SeriLog;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using XCabService.FileService;

namespace XCabBookingFileExtractor.Utils
{
    public class FileDownloader : IFileDownloader
    {
        INfsFileService _nfsFileService;
        bool VerboseLoggingEnabled = true;

        public string Name()
        {
            return "FileDownloader";
        }
        public FileDownloader(ILogger<NfsFileService> logger)
        {
            _nfsFileService = new NfsFileService(logger);
        }

        public FileDownloader(INfsFileService nfsFileService)
        {
            _nfsFileService = nfsFileService;
        }

        public void DownloadFtpCsvFiles(List<LoginDetails> lstLoginDetail)
        {
            if (VerboseLoggingEnabled)
                RollingLogger.WriteToFileDownloaderLogs($"DownloadFtpCsvFiles() - Number of logins to check: {lstLoginDetail.Count}", ELogTypes.Information);

            if (lstLoginDetail.Count <= 0) return;            
            //setup folders in the temp folder to download these files into the respective client folders

            foreach (var login in lstLoginDetail)
            {
                //setup local folders for every client; structure is c:\temp\CLIENT NAME\Bookings for all Bookings and c:\temp\CLIENT NAME\Trackings for trackings
                //there are folders for Processed & Error_Files for Bookings biut no sub folders for Tracking
                var ftpUrl = DbSettings.Default.TyphonFtpConnectionString;
                var nfsUrl = DbSettings.Default.NfsUrlString;
                var userName = login.UserName;
                var password = login.Password;
                var remoteFolderToMonitor = login.BookingsFolderName; //this is the remote folder to monitor

                var bookingFileExtension = DbSettings.Default.BookingFileExtension;
                if (login.TrackingSchemaName.ToLower().Equals("capital-csv"))
                    bookingFileExtension = "csv";
                else if (login.TrackingSchemaName.ToLower().Equals("capital-csv-1.1"))
                    bookingFileExtension = "csv";
                else if (login.BookingSchemaName.ToLower().Equals("officeworks"))
                    bookingFileExtension = "txt";
                else if (login.BookingSchemaName.ToLower().Equals("sigma") || login.BookingSchemaName.ToLower().Equals("ignore-bookings"))
                    bookingFileExtension = null;
                var localDownloadsFolder = DbSettings.Default.LocalDownloadFolder; //this is c:\temp\xCab

                var localBookingsProcessedFolder = Path.Combine(localDownloadsFolder, login.UserName,
                    login.BookingsFolderName, DbSettings.Default.ProcessedFolderName);
                var localBookingsErrorFolder = Path.Combine(localDownloadsFolder, login.UserName,
                    login.BookingsFolderName, DbSettings.Default.ErrorFolderName);

                var localTrackingFolder = Path.Combine(localDownloadsFolder, login.UserName, login.TrackingFolderName);
                //check to see if the folder exists
                if (!Directory.Exists(localBookingsProcessedFolder))
                    Directory.CreateDirectory(localBookingsProcessedFolder);
                if (!Directory.Exists(localBookingsErrorFolder))
                    Directory.CreateDirectory(localBookingsErrorFolder);

                if (!Directory.Exists(localTrackingFolder))
                    Directory.CreateDirectory(localTrackingFolder);
				var lstFiles = new List<string>();
#if DEBUG
                lstFiles = (List<string>)_nfsFileService.GetFileListing("C:\\temp\\Bookings", bookingFileExtension);
#endif
#if !DEBUG
                lstFiles = (List<string>)_nfsFileService.GetFileListing(Path.Combine(nfsUrl, userName, remoteFolderToMonitor), bookingFileExtension);
#endif

                if (lstFiles != null && VerboseLoggingEnabled)
                    RollingLogger.WriteToFileDownloaderLogs($"DownloadFtpCsvFiles() - List of files avialble on FTP for username: {login.UserName}. Files count: {lstLoginDetail.Count}", ELogTypes.Information);

                if (login.BookingSchemaName.ToLower().Equals("ignore-bookings"))
                {
                    if (lstFiles.Count > 0)
                    {
                        lstFiles.Remove(Path.Combine(login.BookingsFolderName, DbSettings.Default.ErrorFolderName));
                        lstFiles.Remove(Path.Combine(login.BookingsFolderName, DbSettings.Default.ProcessedFolderName));
                    }
                }

                //this listing could also include folders so we need to filter out only the xml files
                foreach (var file in lstFiles)
                {
                    var downloadSuccess = false;
                    try
                    {
                        if (VerboseLoggingEnabled)
                            RollingLogger.WriteToFileDownloaderLogs($"DownloadFtpCsvFiles() - Downloading file: {file} From: {ftpUrl}. To: {Path.Combine(localDownloadsFolder, userName)}", ELogTypes.Information);
						//download the file to the local download folder for client
						downloadSuccess = _nfsFileService.DownloadFile(file, Path.Combine(localDownloadsFolder, userName, remoteFolderToMonitor));
                    }
                    catch (Exception exx)
                    {
                        Logger.Log("System error " + exx.Message, Name());
                        if (VerboseLoggingEnabled)
                            RollingLogger.WriteToFileDownloaderLogs($"DownloadFtpCsvFiles() - Downloading file: {file} had an error: {exx.Message}", ELogTypes.Error);
                        downloadSuccess = false;
                    }                    
                }
            }
        }

        public ICollection<string> DownloadFtpCsvFiles(LoginDetails login)
        {
            ICollection<string> allFilesDownloaded = new List<string>();
            if (login == null)
                return allFilesDownloaded;

            var ftpUrl = DbSettings.Default.TyphonFtpConnectionString;
            var userName = login.UserName;
            var password = login.Password;
            var remoteFolderToMonitor = login.BookingsFolderName; //this is the remote folder to monitor
            var nfsUrl = DbSettings.Default.NfsUrlString;

            var bookingFileExtension = login.BookingSchemaName.ToUpper().Contains("CSV")
                ? "csv"
                : DbSettings.Default.BookingFileExtension;

            var localDownloadsFolder = DbSettings.Default.LocalDownloadFolder; //this is c:\temp\xCab

            //String localDownloadFolder = Path.Combine(LocalBookingsFolder, DbSettings.Default.FolderNameBookings);
            var localBookingsProcessedFolder = Path.Combine(localDownloadsFolder, login.UserName,
                login.BookingsFolderName, DbSettings.Default.ProcessedFolderName);
            var localBookingsErrorFolder = Path.Combine(localDownloadsFolder, login.UserName,
                login.BookingsFolderName, DbSettings.Default.ErrorFolderName);

            var localTrackingFolder = Path.Combine(localDownloadsFolder, login.UserName, login.TrackingFolderName);
            //check to see if the folder exists
            if (!Directory.Exists(localBookingsProcessedFolder))
                Directory.CreateDirectory(localBookingsProcessedFolder);
            if (!Directory.Exists(localBookingsErrorFolder))
                Directory.CreateDirectory(localBookingsErrorFolder);

            if (!Directory.Exists(localTrackingFolder))
                Directory.CreateDirectory(localTrackingFolder);
            //var lstFiles = FtpProcessor.FileListing(ftpUrl, userName, password, remoteFolderToMonitor, false, bookingFileExtension);
            var lstFiles = new List<string>();
#if DEBUG
            lstFiles = (List<string>)_nfsFileService.GetFileListing("C:\\temp\\Bookings", bookingFileExtension);
#endif
#if !DEBUG
                lstFiles = (List<string>)_nfsFileService.GetFileListing(Path.Combine(nfsUrl, userName, remoteFolderToMonitor), bookingFileExtension);
#endif

            //this listing could also include folders so we need to filter out only the xml files
            foreach (var file in lstFiles)
            {
                try
                {
                    //download the file to the local download folder for client
                    var isFileDownloaded = _nfsFileService.DownloadFile(file, Path.Combine(localDownloadsFolder, userName, remoteFolderToMonitor));
                    if (isFileDownloaded)
                    {
                        var downloadedFile = Path.Combine(localDownloadsFolder, userName, login.BookingsFolderName, Path.GetFileName(file));
                        allFilesDownloaded.Add(downloadedFile);
                    }
                }
                catch (Exception exx)
                {
                    Logger.Log("System error " + exx.Message, nameof(FileDownloader));
                }
            }
            return allFilesDownloaded;
        }
    }
}