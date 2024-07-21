
using Core;
using FluentFTP;

namespace Ftp
{
    public class FtpService: IFtpService
    {
        public async Task<List<string>> GetFileListing(string ftpUrl, string userName, string password, string directory, bool getEveryThing, string extension)
        {
            var files = new List<string>();
			try
			{
				using (var conn = new FtpClient(ftpUrl, userName,password))
				{
					conn.Connect();
					// get a recursive listing of the files & folders in a specific folder
					foreach (var item in conn.GetListing(directory, FtpListOption.AllFiles))
					{
						switch (item.Type)
						{						
							case FtpObjectType.File:
								if (item.FullName.EndsWith(extension))
								{
									//get the name of the files
									files.Add(item.Name);
								}
								break;
						}
					}
				}
			}
			catch (Exception e)
			{
				await Logger.Log("GetFileListing(): Exception occurred when getting FTP file list from remote location. FTP URL: " + ftpUrl + ". Message: " + e.Message, nameof(FtpService));
			}
			return files;
        }
        public async Task<bool> DownloadFile(string ftpUrl, string userName,
    string password, string localDirectory, string downloadFolder, string remoteFilename)
        {
            bool downloaded = true;
			var localDownloadFileName = Path.Combine(localDirectory, remoteFilename);
			var remoteFilePath = Path.Combine(downloadFolder, remoteFilename);
			try
			{
				using (var ftp = new FtpClient(ftpUrl, userName, password))
				{
					ftp.Connect();
					// download a file and ensure the local directory is created
					ftp.DownloadFile(localDownloadFileName, remoteFilePath, FtpLocalExists.Overwrite);
					//delete remote file
					ftp.DeleteFile(remoteFilePath);
				}
			}
			catch (Exception e)
			{
				downloaded = false;
				await Logger.Log("DownloadFile(): Exception occurred when downloading FTP file list from remote location. Remote path: " + remoteFilePath + ". Message: " + e.Message, nameof(FtpService));
			}
			return downloaded;
        }

		public async Task<bool> UploadFile(string ftpUrl, string userName,
	string password, string localDirectory, string downloadFolder, string fileName)
		{
			bool uploaded = true;
			var localDownloadFileName = fileName;
			var remoteFilePath = string.Empty;
			if (!string.IsNullOrEmpty(localDirectory))
			{
				localDownloadFileName = Path.Combine(localDirectory, fileName);
			}		
			if(string.IsNullOrEmpty(downloadFolder))
			{
				remoteFilePath = fileName;
			}
			else
			{
				remoteFilePath = Path.Combine(downloadFolder, fileName);
			}
			 
			try
			{
				using (var ftp = new FtpClient(ftpUrl, userName, password))
				{
					ftp.Connect();
					// download a file and ensure the local directory is created
					ftp.UploadFile(localDownloadFileName, remoteFilePath);
				}
			}
			catch (Exception e)
			{
				uploaded = false;
				await Logger.Log($"UploadFile(): Exception occurred when uploading FTP file from remote location. FTP URL:{ftpUrl}, LocalDirectory:{localDirectory}, DownloadFolder:{downloadFolder}, Remote path:{remoteFilePath}, FileName:{fileName}. Message: " + e.Message, nameof(FtpService));
			}
			return uploaded;
		}
	}
}