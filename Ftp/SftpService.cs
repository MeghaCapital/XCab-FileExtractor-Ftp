using Core;
using Core.Logging.SeriLog;
using FluentFTP.Helpers;
using Renci.SshNet;

namespace Ftp
{
    public class SftpService : ISftpService
    {
        public async Task<bool> UploadByteArray(string hostName, int port, string username, string password, byte[] fileContents, string remoteFilePath)
        {
            bool uploaded = true;
            try
            {
                using (var sshClient = new SshClient(hostName, port, username, password))
                {
                    sshClient.Connect();
                    using (var sftpClient = new SftpClient(sshClient.ConnectionInfo))
                    {
                        try
                        {
                            sftpClient.Connect();

                            using (var stream = new System.IO.MemoryStream(fileContents))
                            {
                                sftpClient.UploadFile(stream, remoteFilePath);

                            }
                        }
                        catch (Exception ex)
                        {
                            await Logger.Log($"UploadByteArray(): Exception occurred when connecting to sftpClient. SFTP host:{hostName}, remoteFilePath:{remoteFilePath}. Message: {ex.Message}", nameof(SftpService));
                            RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Failed to connect to sftpClient via SftpService. Message: {ex.Message}.", ELogTypes.Error);
                        }
                        finally
                        {
                            sftpClient.Disconnect();
                        }
                    }
                    sshClient.Disconnect();
                    RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Successfully uploaded file contents to {remoteFilePath} via SftpService.", ELogTypes.Information);
                }
            }
            catch (Exception e)
            {
                uploaded = false;
                await Logger.Log($"UploadByteArray(): Exception occurred when uploading FTP contents to remote location. SFTP host:{hostName}, remoteFilePath:{remoteFilePath}. Message:{e.Message}", nameof(SftpService));
                RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Exception occurred when uploading FTP file from remote location. SFTP host:{hostName}, remoteFilePath:{remoteFilePath}. Message: {e.Message}", ELogTypes.Error);
            }
            return uploaded;
        }



        public async Task<bool> UploadFile(string hostName, int port, string username, string password, string localFilePath, string remoteFilePath)
        {
            bool uploaded = true;
            var fileName = localFilePath.Substring(localFilePath.LastIndexOf('\\') + 1);
            try
            {
                using (var sshClient = new SshClient(hostName, port, username, password))
                {
                    sshClient.Connect();
                    using (var sftpClient = new SftpClient(sshClient.ConnectionInfo))
                    {
                        try
                        {
                            sftpClient.Connect();

                            using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                            {
                                sftpClient.UploadFile(fileStream, remoteFilePath + "/" + fileName);

                            }
                        }
                        catch (Exception ex)
                        {
                            await Logger.Log($"UploadFile(): Exception occurred when connecting to sftpClient. SFTP host:{hostName}, localFilePath:{localFilePath}, remoteFilePath:{remoteFilePath}. Message: {ex.Message}", nameof(SftpService));
                            RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Failed to connect to sftpClient via SftpService. Message: {ex.Message}.", ELogTypes.Error);
                        }
                        finally
                        {
                            sftpClient.Disconnect();
                        }
                    }
                    sshClient.Disconnect();

                    RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Successfully uploaded file {localFilePath} to {remoteFilePath} via SftpService.", ELogTypes.Information);
                }
            }
            catch (Exception e)
            {
                uploaded = false;
                await Logger.Log($"UploadFile(): Exception occurred when uploading FTP file from remote location. SFTP host:{hostName}, localFilePath:{localFilePath}, remoteFilePath:{remoteFilePath}. Message:{e.Message}", nameof(SftpService));
                RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Exception occurred when uploading FTP file from remote location. SFTP host:{hostName}, localFilePath:{localFilePath}, remoteFilePath:{remoteFilePath}. Message: {e.Message}", ELogTypes.Error);
            }

            return uploaded;
        }

        public async Task<bool> UploadMultipleFiles(string hostName, int port, string username, string password, string[] localFiles, string remoteFilePath, string destinationProcessedFolder, string destinationErrorFolder)
        {
            bool uploaded = true;
            try
            {
                using (var sshClient = new SshClient(hostName, port, username, password))
                {
                    sshClient.Connect();
                    using (var sftpClient = new SftpClient(sshClient.ConnectionInfo))
                    {
                        try
                        {
                            sftpClient.Connect();
                            foreach (var localFilePath in localFiles)
                            {
                                var fileName = localFilePath.Substring(localFilePath.LastIndexOf('\\') + 1);
                                try
                                {
                                    using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                                    {
                                        sftpClient.UploadFile(fileStream, remoteFilePath + "/" + fileName);
                                        RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Successfully uploaded file {localFilePath} to {remoteFilePath} via SftpService.", ELogTypes.Information);
                                    }
                                    var destinationProcessedFileName = Path.Combine(destinationProcessedFolder + "\\", localFilePath.GetFtpFileName());
                                    if (File.Exists(localFilePath) && !string.IsNullOrEmpty(destinationProcessedFileName))
                                    {
                                        File.Move(localFilePath, destinationProcessedFileName);
                                    }
                                    else
                                    {
                                        await Logger.Log($"UploadFile(): Exception: File {localFilePath} does not exists.", nameof(SftpService));
                                        RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"File {localFilePath} does not exists", ELogTypes.Error);
                                    }
                                }
                                catch(Exception ex)
                                {
                                    var destinationErrorFileName = Path.Combine(destinationErrorFolder + "\\", localFilePath.GetFtpFileName());
                                    if (File.Exists(localFilePath) && !string.IsNullOrWhiteSpace(destinationErrorFileName))
                                        File.Move(localFilePath, destinationErrorFileName);
                                    await Logger.Log($"UploadFile(): Exception occurred when uploading FTP file from remote location. SFTP host:{hostName}, localFilePath:{localFilePath}, remoteFilePath:{remoteFilePath}. Message:{ex.Message}", nameof(SftpService));
                                    RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Exception occurred when uploading FTP file from remote location. SFTP host:{hostName}, localFilePath:{localFilePath}, remoteFilePath:{remoteFilePath}. Message: {ex.Message}", ELogTypes.Error);

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            await Logger.Log($"UploadFile(): Exception occurred when connecting to sftpClient. SFTP host:{hostName}, remoteFilePath:{remoteFilePath}. Message: {ex.Message}", nameof(SftpService));
                            RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Failed to connect to sftpClient via SftpService. Message: {ex.Message}.", ELogTypes.Error);
                        }
                        finally
                        {
                            sftpClient.Disconnect();
                        }
                    }
                    sshClient.Disconnect();

                }
            }
            catch (Exception e)
            {
                uploaded = false;
            }

            return uploaded;
        }

    }
}