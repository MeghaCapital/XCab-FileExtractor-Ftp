using System.Net;

namespace Core.Models.FTP
{
    public class FTPRespone
    {
        public FtpStatusCode FTPStatusCode { get; set; }
        public string FTPStatusDescription { get; set; }
    }
}
