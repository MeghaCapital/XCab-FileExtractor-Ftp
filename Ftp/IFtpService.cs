namespace Ftp
{
	public interface IFtpService
	{
		public Task<List<string>> GetFileListing(string ftpUrl, string userName,
string password, string directory, bool getEveryThing, string extension);

		public Task<bool> DownloadFile(string ftpUrl, string userName,
	string password, string localDirectory, string downloadFolder, string remoteFilename);

		public Task<bool> UploadFile(string ftpUrl, string userName,
	string password, string localDirectory, string downloadFolder, string remoteFilename);

	}
}
