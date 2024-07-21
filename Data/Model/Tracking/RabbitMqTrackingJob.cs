namespace Data.Model.Tracking
{
    public class RabbitMqTrackingJob
    {
        public string JobNumber { get; set; }
        public string UploadDateTime { get; set; }
        public string JobAllocationDate { get; set; }

        public int StateId { get; set; }
        public int DriverId { get; set; }
        public string AccountCode { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string ConsignmentNumber { get; set; }
        public Location eventLocation { get; set; }
        public LoginDetails LoginDetails { get; set; }
        public string eventDateTime { get; set; }
        public ETrackingEvent CurrentTrackingEvent { get; set; }
        public int BookingId { get; set; }
        public PodUrl podUrl { get; set; }
        public string FromDetail1 { get; set; }
        public string FromDetail2 { get; set; }
        public string FromDetail3 { get; set; }
        public string FromDetail4 { get; set; }
        public string FromDetail5 { get; set; }
        public string FromSuburb { get; set; }
        public string FromPostcode { get; set; }
        public string ToDetail1 { get; set; }
        public string ToDetail2 { get; set; }
        public string ToDetail3 { get; set; }
        public string ToDetail4 { get; set; }
        public string ToDetail5 { get; set; }
        public string ToSuburb { get; set; }
        public string ToPostcode { get; set; }
        public bool SkipFtpAccess { get; set; }
        public int ExternalClientIntegrationId { get; set; }
        public double TotalWeight { get; set; }
        public double TotalVolume { get; set; }
        public DateTime? DeliveryEta { get; set; }
        public string LegNumber { get; set; }
        public string TotalLegs { get; set; }
        public string PickupArriveLocation { get; set; }
        public DateTime? PickupArrive { get; set; }
        public string PickupCompleteLocation { get; set; }
        public DateTime? PickupComplete { get; set; }
        public string DeliveryArriveLocation { get; set; }
        public DateTime? DeliveryArrive { get; set; }
        public string DeliveryCompleteLocation { get; set; }
        public DateTime? DeliveryComplete { get; set; }

        public string ServiceCode { get; set; }

        public string PodName { get; set; }

        public string MultipleDeliveryId { get; set; }
        public class PodUrl
        {
            public string PodJobNumber { get; set; }
            public string PodSubJobNumber { get; set; }
        }

        public string TplusPodTime { get; set; }


        public override string ToString()
        {
            return "Job:" + JobNumber + ",JobBookingDay:" + UploadDateTime + ",TrackingEvent:" + CurrentTrackingEvent;
        }
    }

    public class Location
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public enum ETrackingEvent
    {
        JobBooked,
        PickupArrive,
        PickupComplete,
        DeliveryArrive,
        DeliveryComplete,
        Cancelled
    }

    public class LoginDetails
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BookingsFolderName { get; set; }
        public string ProcessedFolderName { get; set; }
        public string ErrorFolderName { get; set; }
        public string TrackingFolderName { get; set; }
        public bool sendPodUrl { get; set; }
        public bool SendBase64Image { get; set; }

        public bool sendBinaryPod { get; set; }
        public string Id { get; set; }
        public string TrackingSchemaName { get; set; }
        public bool NotifyCancelJobs { get; set; }

        // added to support secure ftp processing (SFTP)
        public string Sshkeyprivate { get; set; }
        public string Remoteftphostname { get; set; }
        public string Remotetrackingfoldername { get; set; }
        public bool UsesSftpPush { get; set; }
        public string RemoteFtpUserName { get; set; }

        public string RemoteFtpPassword { get; set; }

        public bool SkipFtpAccess { get; set; }

        public LoginDetails()
        {
            //assign default vales here
            BookingsFolderName = "Bookings";
            ProcessedFolderName = "Processed";
            ErrorFolderName = "Error_Files";
            TrackingFolderName = "Tracking";
            sendPodUrl = false;
            SendBase64Image = false;
            NotifyCancelJobs = false;
            UsesSftpPush = false;
        }
    }
}
