
namespace Ftp
{
	public interface ISftpService
	{
		Task<bool> UploadFile(string hostName, int port, string username, string password, string localFilePath, string remoteFilePath);
		Task<bool> UploadByteArray(string hostName, int port, string username, string password, byte[] contents, string remoteFilePath);
	}
}