namespace XCabService.SimpleFtpService;

public interface ISimpleFtpService
{
    Task<bool> UploadFile(
        string host,
        string userName,
        string password,
        string remotePath,
        string fileName,
        byte[] fileContent);
}