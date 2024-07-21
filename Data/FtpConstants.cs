using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Data
{
    /// <summary>
    ///     FtpProcessor
    /// </summary>
    public class FtpProcessor
    {
        /// <summary>
        ///     Names this instance.
        /// </summary>
        /// <returns></returns>
        private static string Name()
        {
            return "FtpProcessor";
        }

        /// <summary>
        ///     Ftpfiles the remove.
        /// </summary>
        /// <param name="ftpUrl">The FTP URL.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="baseFolder">The base folder.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public static bool FtpfileRemove(string ftpUrl, string userName,
            string password, string baseFolder, string filename)
        {
            var deleteSuccess = false;
            var uploadUrl = $"{ftpUrl}/{filename}";

            try
            {
                var r = (FtpWebRequest)WebRequest.Create(uploadUrl);

                r.KeepAlive = true;
                r.UseBinary = true;
                r.Credentials = new NetworkCredential(userName, password);
                r.Method = WebRequestMethods.Ftp.DeleteFile;

                using (var response = (FtpWebResponse)r.GetResponse())
                {
                    //Logger.Log("Delete status:" + response.StatusDescription, Name());
                    deleteSuccess = true;
                }
            }
            catch (Exception exx)
            {
                throw exx;
            }
            return deleteSuccess;
        }

        /// <summary>
        ///     Files the download.
        /// </summary>
        /// <param name="ftpUrl">The FTP URL.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="downloadFolder">The download folder.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public async static Task<bool> FileDownload(string ftpUrl, string userName,
            string password, string directory, string downloadFolder, string filename)
        {
            string uploadUrl = $"{ftpUrl}/{filename}";
            //string uploadUrl = $"{ftpUrl}/{directory}/{filename}";
            string downloadFolderFile = $"{downloadFolder}\\{filename}";
            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);
            try
            {
                //var r = (FtpWebRequest)WebRequest.Create(uploadUrl);

                //r.KeepAlive = true;
                //r.UseBinary = true;
                //r.Credentials = new NetworkCredential(userName, password);
                //r.Method = WebRequestMethods.Ftp.DownloadFile;

                using (var request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(userName, password);
                    var fileData = request.DownloadData(uploadUrl);

                    using (var file = File.Create(downloadFolderFile))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }
                    //Logger.Log("Download Complete", Name());
                }
            }
            
            catch (Exception e)
            {
                await Logger.Log("FileDownload(): Exception occurred when downloading FTP files from remote location. FTP path: " + uploadUrl + ". Message: " +
    e.Message, nameof(FtpProcessor));
            }
            return true;
        }
        public async static Task<bool> FileDownloadFletcher(string ftpUrl, string userName,
    string password, string directory, string downloadFolder, string filename)
        {
            string uploadUrl = $"{ftpUrl}/{filename}";
            //string uploadUrl = $"{ftpUrl}/{directory}/{filename}";
            string downloadFolderFile = $"{downloadFolder}\\{filename}";
            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);
            try
            {
                //var r = (FtpWebRequest)WebRequest.Create(uploadUrl);

                //r.KeepAlive = true;
                //r.UseBinary = true;
                //r.Credentials = new NetworkCredential(userName, password);
                //r.Method = WebRequestMethods.Ftp.DownloadFile;

                //using (var request = new WebClient())
                //{
                //    request.Credentials = new NetworkCredential(userName, password);
                //    request.DownloadFile(uploadUrl, downloadFolderFile);
                //    //var fileData = request.DownloadData(uploadUrl);

                //    //using (var file = File.Create(downloadFolderFile))
                //    //{
                //    //    file.Write(fileData, 0, fileData.Length);
                //    //    file.Close();
                //    //}
                //    //Logger.Log("Download Complete", Name());
                //}
                FtpWebRequest request =
    (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.Credentials = new NetworkCredential(userName, password);
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                using (Stream fileStream = File.Create(downloadFolderFile))
                {
                    ftpStream.CopyTo(fileStream);
                }
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
            }
            catch (Exception e)
            {
                await Logger.Log("FileDownload(): Exception occurred when downloading FTP files from remote location. FTP path: " + uploadUrl + ". Message: " +
    e.Message, nameof(FtpProcessor));
            }
            return true;
        }
        public static string GetFileDownload(string ftpUrl, string userName,
            string password, string directory, string downloadFolder, string filename)
        {
            string uploadUrl = $"{ftpUrl}/{filename}";
            string downloadFolderFile = $"{downloadFolder}\\{filename}";
            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);
            try
            {
                //var r = (FtpWebRequest)WebRequest.Create(uploadUrl);

                //r.KeepAlive = true;
                //r.UseBinary = true;
                //r.Credentials = new NetworkCredential(userName, password);
                //r.Method = WebRequestMethods.Ftp.DownloadFile;

                using (var request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(userName, password);
                    var fileData = request.DownloadData(uploadUrl);

                    using (var file = File.Create(downloadFolderFile))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }
                   // Logger.Log("Download Complete", Name());
                }
            }
            catch (Exception exx)
            {
                throw exx;
            }
            return downloadFolderFile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftpUrl"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="directory"></param>
        /// <param name="downloadFolder"></param>
        /// <param name="filename"></param>
        /// <param name="retrievedFolder"></param>
        /// <returns></returns>
        public static string GetFileDownloadFtpToCsv(string ftpUrl, string userName,
         string password, string directory, string downloadFolder, string filename, string retrievedFolder)
        {
            string uploadUrl = $"{ftpUrl}/{retrievedFolder}/{filename}";
            string downloadFolderFile = $"{downloadFolder}\\{filename}";
            if (!Directory.Exists(downloadFolder))
                Directory.CreateDirectory(downloadFolder);
            try
            {
                using (var request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(userName, password);
                    var fileData = request.DownloadData(uploadUrl);

                    using (var file = File.Create(downloadFolderFile))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }
                }
            }
            catch (Exception exx)
            {
                throw exx;
            }
            return downloadFolderFile;
        }


        /// <summary>
        ///     Files the listing.
        /// </summary>
        /// <param name="ftpUrl">The FTP URL.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="getEveryThing">if set to <c>true</c> [get every thing].</param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static List<string> FileListing(string ftpUrl, string userName,
            string password, string directory, bool getEveryThing, string extension)
        {
            string uploadUrl = $"{ftpUrl}//{directory}";
            // get the list of files into this var
            var l = new List<string>();
            try
            {
                var r = (FtpWebRequest)WebRequest.Create(uploadUrl);

                r.Credentials = new NetworkCredential(userName, password);
                r.KeepAlive = true;
                r.Method = getEveryThing ? WebRequestMethods.Ftp.ListDirectoryDetails : WebRequestMethods.Ftp.ListDirectory;

                using (var response = (FtpWebResponse)r.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, true))
                        {
                            while (!reader.EndOfStream)
                            {
                                //for the new typhon FTP server the ReadLine method does not return the full path name
                                //instead it retuns only the name of the file, for e.g. a.xml instead of Bookings/a.xml                                
                                var relativePathName = reader.ReadLine();
                                if (relativePathName != null)
                                {
                                    var fileName = Path.Combine(directory, relativePathName);
                                    if (extension != null)
                                    {
                                        //check if we have the extension that is provided
                                        if (fileName.EndsWith(extension, true, null))
                                            l.Add(fileName);
                                    }
                                    else
                                        l.Add(fileName);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    $"FileListing Method, FtpUrl:{ftpUrl}, Username{userName}, Directory{directory}, Message {e.Message}",
                    Name());
            }
            return l;
        }
        public static List<string> FileListingRemoteFtpFletcher(string ftpUrl, string userName,
    string password, string directory, bool getEveryThing, string extension)
        {
            string uploadUrl = $"{ftpUrl}//{directory}";
            // get the list of files into this var
            var l = new List<string>();
            try
            {
                var r = (FtpWebRequest)WebRequest.Create(uploadUrl);

                r.Credentials = new NetworkCredential(userName, password);
                r.KeepAlive = true;
                r.Method = getEveryThing ? WebRequestMethods.Ftp.ListDirectoryDetails : WebRequestMethods.Ftp.ListDirectory;

                using (var response = (FtpWebResponse)r.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, true))
                        {
                            while (!reader.EndOfStream)
                            {
                                //for the new typhon FTP server the ReadLine method does not return the full path name
                                //instead it retuns only the name of the file, for e.g. a.xml instead of Bookings/a.xml                                
                                var relativePathName = reader.ReadLine();
                                if (relativePathName != null)
                                {
                                    var fileName = Path.Combine(directory, relativePathName.Substring(relativePathName.LastIndexOf(" ") + 1));
                                    if (extension != null)
                                    {
                                        //check if we have the extension that is provided
                                        if (fileName.EndsWith(extension, true, null))
                                            l.Add(fileName);
                                    }
                                    else
                                        l.Add(fileName);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(
                    $"FileListing Method, FtpUrl:{ftpUrl}, Username{userName}, Directory{directory}, Message {e.Message}",
                    Name());
            }
            return l;
        }
        
        private static string GetDateTimeForFile()
        {
            var uploadTime = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" +
                             DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" +
                             DateTime.Now.Minute + "_" + DateTime.Now.Millisecond;
            return uploadTime;
        }

        /// <summary>
        ///     Uploads the file.
        /// </summary>
        /// <param name="ftpUrl">The FTP URL.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="uploadDirectory">The upload directory.</param>
        /// <param name="addDateTime"></param>
        /// <returns></returns>
        /// Upload File to Specified FTP Url with username and password and Upload Directory
        /// Base FtpUrl of FTP Server
        /// Local Filename to Upload
        /// Username of FTP Server
        /// Password of FTP Server
        public static bool UploadFile(string ftpUrl, string fileName, string userName, string password,
            string uploadDirectory, bool addDateTime)
        {
            var pureFileName = new FileInfo(fileName).Name;
            if (addDateTime)
                pureFileName += "." + GetDateTimeForFile();
            var _uploadDir = @"//" + uploadDirectory + @"/";
            var uploadUrl = $"{ftpUrl}{_uploadDir}/{pureFileName}";
            try
            {
                var req = (FtpWebRequest)WebRequest.Create(uploadUrl);
                req.Proxy = null;
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.Credentials = new NetworkCredential(userName, password);
                req.UseBinary = true;
                req.UsePassive = true;
                req.KeepAlive = true;
                var data = File.ReadAllBytes(fileName);
                req.ContentLength = data.Length;
                var stream = req.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                /* using (var res = (FtpWebResponse)req.GetResponse())
                 {
                     Core.Logger.Log("... Completed " + res.StatusCode + "\r\n" + res.BannerMessage + "\r\n" +
                                       res.WelcomeMessage + "\r\n" + res.StatusDescription, Name());
                     Core.Logger.Log("Successfully Uploaded File:" + fileName, Name());                    
                 }*/
            }
            catch (Exception exx)
            {
                Logger.Log(
                    "Exception Occurred while Uploading File to FTP:" + exx.Message + ", FileName:" + fileName +
                    ",FtpUrl:" + ftpUrl + ",upload directory:" + uploadDirectory, "FtpConstants");
            }
            return true;
        }
    }
}
