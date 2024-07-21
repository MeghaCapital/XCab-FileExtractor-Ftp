namespace Core
{
    [Serializable]
    public class LoginDetails
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BookingsFolderName { get; set; }
        public string ProcessedFolderName { get; set; }
        public string ErrorFolderName { get; set; }
        public string TrackingFolderName { get; set; }
        public bool sendPodUrl { get; set; }
        public bool sendBinaryPod { get; set; }
        public bool SendBase64Image { get; set; }
        public string Id { get; set; }
        public string TrackingSchemaName { get; set; }
        public string BookingSchemaName { get; set; }
        public bool NotifyCancelJobs { get; set; }
        public List<int> lstStateIds { get; set; }
        public List<string> lstAccountCodes { get; set; }
        public List<string> lstServiceCodes { get; set; }
        public bool IsRemotePushEnabled { get; set; }
        public string RemoteFtpHostName { get; set; }

        public string RemoteFtpPassword { get; set; }
        public string RemoteTrackingFolderName { get; set; }
        //      public bool remoteftpUsesActive { get; set; }

        // added to support secure ftp processing (SFTP)
        public string Sshkeyprivate { get; set; }
 
        public bool UsesSftpPush { get; set; }
        public string RemoteFtpUserName { get; set; }

        public bool SkipFtpAccess { get; set; }

        public int? ExternalClientIntegrationId { get; set; }

        public LoginDetails()
        {
            //assign default vales here
            BookingsFolderName = "";
            ProcessedFolderName = "Processed";
            ErrorFolderName = "Error_Files";
            TrackingFolderName = "Tracking";
            sendPodUrl = false;
            SendBase64Image = false;
            NotifyCancelJobs = false;
            lstStateIds = new List<int>();
            lstAccountCodes = new List<string>();
            lstServiceCodes = new List<string>();
            sendBinaryPod = false;
            UsesSftpPush = false;
            ExternalClientIntegrationId = 0;
        }
    }
}
