using FluentFTP;
using Microsoft.Extensions.Logging;

namespace XCabService.SimpleFtpService;

public class SimpleFtpService : ISimpleFtpService
{
    private ILogger<SimpleFtpService> _logger;

    public SimpleFtpService(ILogger<SimpleFtpService> logger)
    {
        _logger = logger;
    }
    
    public async Task<bool> UploadFile(
        string host,
        string userName,
        string password,
        string remotePath,
        string fileName,
        byte[] fileContent)
    {
        bool success = false;

        using var ftpClient = new FtpClient(host, userName, password);

        try
        {
            ftpClient.UploadBytes(fileContent, Path.Combine(remotePath, fileName));
            success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload file content to FTP server");
        }

        return success;
    }
}