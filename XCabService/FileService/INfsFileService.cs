namespace XCabService.FileService;

public interface INfsFileService
{
    bool UploadFile(string fileName, string shareLocation);
    ICollection<string>? GetFileListing(string shareLocation, string fileExtensionToSearch);
    bool DownloadFile(string fileNameToDownload, string destinationLocation);
    bool MoveFileWithTimestamp(string sourceFileName, string destinationFileName);
    bool CopyFileWithTimestamp(string sourceFileName, string destinationFileName);
    bool MoveFileWithoutTimestamp(string sourceFileName, string destinationFileName);
    bool CopyFileWithoutTimestamp(string sourceFileName, string destinationFileName);
}
