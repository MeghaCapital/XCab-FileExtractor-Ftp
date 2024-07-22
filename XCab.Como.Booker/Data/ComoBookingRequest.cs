using System;
using System.Collections.Generic;
using System.Text;

namespace xcab.como.booker.Data
{
    public class ComoBookingRequest
    {
        public ComoBookingRequest()
        {
            lstItems = new List<Item>();
            lstContactDetail = new List<ContactDetail>();
            Notification = new Notification();
            RouteLeg = new RouteLeg();
            LoginDetails = new LoginDetails();
            DeliveryTimeSlot = new TimeSlot();
            lstExtraFields = new List<ExtraFields>();
            Remarks = new List<string>();
            BookingContactInformation = new BookingContactInformation();
        }

        public string AccountCode { get; set; }

        public string FromSuburb { get; set; }

        public string FromPostcode { get; set; }

        public string FromDetail1 { get; set; }

        public string FromDetail2 { get; set; }

        public virtual string FromDetail3 { get; set; }

        public virtual string FromDetail4 { get; set; }

        public virtual string FromDetail5 { get; set; }

        public string ToSuburb { get; set; }

        public string ToPostcode { get; set; }

        public string ToDetail1 { get; set; }

        public string ToDetail2 { get; set; }

        public virtual string ToDetail3 { get; set; }

        public virtual string ToDetail4 { get; set; }

        public virtual string ToDetail5 { get; set; }

        public string State { get; set; }

        public string Ref1 { get; set; }

        public string Ref2 { get; set; }

        public virtual string ConsignmentNumber { get; set; }

        public List<Item> lstItems { get; set; }

        public virtual List<ContactDetail> lstContactDetail { get; set; }

        public virtual Notification Notification { get; set; }

        public string ServiceCode { get; set; }

        public virtual string TotalItems { get; set; }

        public virtual string TotalWeight { get; set; }

        public virtual string TotalVolume { get; set; }

        public virtual bool UsesSOAP { get; set; }

        public virtual RouteLeg RouteLeg { get; set; }

        public virtual bool ATL { get; set; }

        public virtual string Id { get; set; }

        public virtual LoginDetails LoginDetails { get; set; }

        public DateTime DespatchDateTime { get; set; }

        public virtual DateTime UploadDateTime { get; set; }

        public virtual bool IsBookingOnlyClient { get; set; }

        public virtual bool OkToUpload { get; set; }

        public virtual bool UploadedToTplus { get; set; }

        public virtual string TPLUS_JobNumber { get; set; }

        public virtual DateTime AdvanceDateTime { get; set; }

        public virtual int DriverNumber { get; set; }

        public virtual int PreAllocatedDriverNumber { get; set; }

        public string Caller { get; set; }

        public virtual string ExtraPuInformation { get; set; }

        public virtual string ExtraDelInformation { get; set; }

        public virtual bool IsQueued { get; set; }

        public virtual bool BarcodesAllowed { get; set; }

        public virtual TimeSlot DeliveryTimeSlot { get; set; }

        public virtual string TrackAndTraceSmsNumber { get; set; }

        public virtual string TrackAndTraceEmailAddress { get; set; }

        public virtual List<ExtraFields> lstExtraFields { get; set; }

        public virtual List<string> Remarks { get; set; }

        public bool OnHoldBooking { get; set; }

        public bool TransportDocumentWillAccompanyLoad { get; set; }

        public bool PackagedInAccordanceWithAdg7_4 { get; set; }

        public bool IsWeight { get; set; }

        /// <summary>
        /// ATL instructions
        /// </summary>
        public virtual string ATLInstructions { get; set; }

        public virtual BookingContactInformation BookingContactInformation { get; set; }
    }

    public class Item
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public String Description { get; set; }
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public double Length { get; set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height { get; set; }
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the cubic.
        /// </summary>
        /// <value>
        /// The cubic.
        /// </value>
        public double Cubic { get; set; }
        /// <summary>
        /// Gets or sets the barcode.
        /// </summary>
        /// <value>
        /// The barcode.
        /// </value>
        public String Barcode { get; set; }

        /// <summary>
        /// Quantity for the item
        /// </summary>
        public int Quantity { get; set; }

    }

    public class ContactDetail
    {
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class Notification
    {
        public int BookingId { get; set; }
        public string SMSNumber { get; set; }
        public string EmailAddress { get; set; }
    }

    public class RouteLeg
    {
        public int RouteId { get; set; }

        public int LegNumber { get; set; }

        public bool RouteToCustomer { get; set; }
    }

    public class LoginDetails
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual string BookingsFolderName { get; set; }
        public virtual string ProcessedFolderName { get; set; }
        public virtual string ErrorFolderName { get; set; }
        public virtual string TrackingFolderName { get; set; }
        public virtual bool sendPodUrl { get; set; }
        public virtual bool SendBinaryPod { get; set; }
        public virtual bool SendBase64Image { get; set; }
        public virtual string Id { get; set; }
        public virtual string TrackingSchemaName { get; set; }
        public virtual string BookingSchemaName { get; set; }
        public virtual bool NotifyCancelJobs { get; set; }
        public virtual List<int> LstStateIds { get; set; }
        public virtual List<string> LstAccountCodes { get; set; }
        public virtual List<string> IstServiceCodes { get; set; }
        public virtual bool IsRemotePushEnabled { get; set; }
        public virtual string RemoteFtpHostname { get; set; }
        public virtual string RemoteFtpUsername { get; set; }
        public virtual string RemoteFtpPassword { get; set; }
        public virtual string RemoteTrackingFolderName { get; set; }
        //      public bool remoteftpUsesActive { get; set; }

        // added to support secure ftp processing (SFTP)
        public virtual string Sshkeyprivate { get; set; }
        public virtual string Remoteftphostname { get; set; }
        public virtual string Remotetrackingfoldername { get; set; }
        public virtual bool UsesSftpPush { get; set; }
        public virtual string RemoteFtpUserName { get; set; }


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
            LstStateIds = new List<int>();
            LstAccountCodes = new List<string>();
            IstServiceCodes = new List<string>();
            SendBinaryPod = false;
            UsesSftpPush = false;
        }
    }

    public class TimeSlot
    {
        /// <summary>
        /// Gets or sets the account code.
        /// </summary>
        /// <value>
        /// The account code.
        /// </value>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// Gets or sets from suburb.
        /// </summary>
        /// <value>
        /// From suburb.
        /// </value>
        public int Duration { get; set; }
    }

    public class ExtraFields
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class BookingContactInformation
    {
        public BookingContactInformation()
        {
            PhoneNumbers = new List<string>();
        }

        public string Name { get; set; }

        public List<string> PhoneNumbers { get; set; }
    }
}
