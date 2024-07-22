using Core;
using Core.Logging.SeriLog;
using Core.Models.Slack;
using Data;
using Data.Model.Tracking;
using Data.Repository.EntityRepositories;
using Data.Repository.EntityRepositories.ExtraReferences;
using Data.Repository.EntityRepositories.Job.TimeSlots;
using Data.Repository.V2;
using FluentFTP.Helpers;
using System.Xml.Linq;
using XCabService.FileService;
using XCabService.FutileService;

namespace XCabService.IkeaService
{
    public class IkeaTrackingService : IIkeaTrackingService
    {
        public List<string> GetFileContent(
         string barcode,
         string status,
         string eventDateTime,
         string podName,
         string suburb,
         string shipmentType,
         string consignmentNumber,
         string serviceCode,
         bool isFutile,
         bool isFutileAdditionalJob,
         string futileReason,
         int driverId, string accountCode,
         string type = "PACKAGE")

        {
            List<string> fileContent = new();
            if (string.IsNullOrEmpty(podName))
            {
                podName = "MANUAL";
            }

            // the different flows will be LEGACY, CCD, LCD
            // LCD will be CFB flow
            if (!string.IsNullOrWhiteSpace(status) && status.ToUpper().Equals("PICKUP"))
            {
                if (shipmentType.ToUpper().Equals("LCD"))
                {
                    var eventTime = !string.IsNullOrWhiteSpace(eventDateTime) ? DateTime.Parse(eventDateTime).AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var statusCode = "19";
                    var statusDescription = "Handed over to TSP";
                    // If job is Futile dont create any file.
                    // Else Create 2 files for pickup
                    GetXmlContentForDeliveryStatus(barcode, podName, suburb, type, fileContent, eventTime, statusCode, statusDescription, shipmentType, driverId);
                    eventTime = !string.IsNullOrWhiteSpace(eventDateTime) ? DateTime.Parse(eventDateTime).AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    statusCode = "92";
                    statusDescription = "Loaded on delivery truck";
                    GetXmlContentForDeliveryStatus(barcode, podName, suburb, type, fileContent, eventTime, statusCode, statusDescription, shipmentType, driverId);
                }
                else if (shipmentType.ToUpper().Equals("CCD"))
                {

                    // If account code KMIKCC and IC servcie code then 1 file should be created for click and collect for pickup
                    if (IsClickAndCollect(serviceCode, accountCode, shipmentType))
                    {
                        var eventTime = !string.IsNullOrWhiteSpace(eventDateTime) ? DateTime.Parse(eventDateTime).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var statusCode = "92";
                        var statusDescription = "Loaded on delivery truck";

                        GetXmlContentForDeliveryStatus(barcode, podName, suburb, type, fileContent, eventTime, statusCode, statusDescription, shipmentType, driverId);
                    }
                    else
                    {
                        var eventTime = !string.IsNullOrWhiteSpace(eventDateTime) ? DateTime.Parse(eventDateTime).AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var statusCode = "90";
                        var statusDescription = "Received at hub";
                        // If job is Futile dont create any file.
                        // Create 2 files for pickup
                        GetXmlContentForDeliveryStatus(barcode, podName, suburb, type, fileContent, eventTime, statusCode, statusDescription, shipmentType, driverId);

                        eventTime = !string.IsNullOrWhiteSpace(eventDateTime) ? DateTime.Parse(eventDateTime).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        statusCode = "92";
                        statusDescription = "Loaded on delivery truck";

                        GetXmlContentForDeliveryStatus(barcode, podName, suburb, type, fileContent, eventTime, statusCode, statusDescription, shipmentType, driverId);
                    }
                }
                else if (shipmentType.ToUpper().Equals("LEGACY"))
                {
                    var statusCode = IsReturnServiceCode(serviceCode)
                                         ? "19:2" : "19";

                    var statusDescription = IsReturnServiceCode(serviceCode)
                                     ? "Picked up at customer" : "Handed over to TSP";

                    var eventTime = !string.IsNullOrWhiteSpace(eventDateTime) ? DateTime.Parse(eventDateTime).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    // If job is Futile dont create any file.
                    GetXmlContentForDeliveryStatus(barcode, podName, suburb, type, fileContent, eventTime, statusCode, statusDescription, shipmentType, driverId);
                }
            }
            else if (!string.IsNullOrWhiteSpace(status) && status.ToUpper().Equals("DELIVERY"))
            {
                var eventTime = !string.IsNullOrWhiteSpace(eventDateTime) ? DateTime.Parse(eventDateTime).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Click and collect for IC service code
                if (IsClickAndCollect(serviceCode, accountCode, shipmentType) && !serviceCode.ToUpper().Equals("CPOD"))
                {
                    var statusCode = "90:2";
                    var statusDescription = "Received at PuP";
                    XElement fileContentElement = new("deliveryStatuses");
                    XElement deliveryStatuses = new("deliveryStatus",
                                            new XElement("type", type),
                                            new XElement("identifier", barcode),
                                            new XElement("code", statusCode),
                                            new XElement("description", statusDescription),
                                            new XElement("eventTime", eventTime),
                                            new XElement("location", suburb),
                                            new XElement("signedBy", podName)
                                        );
                    fileContentElement.Add(deliveryStatuses);
                    fileContent.Add(deliveryStatuses.ToString());

                    statusCode = "97";
                    statusDescription = "Ready for pickup";
                    fileContentElement = new("deliveryStatuses");
                    deliveryStatuses = new("deliveryStatus",
                                            new XElement("type", type),
                                            new XElement("identifier", barcode),
                                            new XElement("code", statusCode),
                                            new XElement("description", statusDescription),
                                            new XElement("eventTime", eventTime),
                                            new XElement("location", suburb),
                                            new XElement("signedBy", podName)
                                        );
                    fileContentElement.Add(deliveryStatuses);
                    fileContent.Add(deliveryStatuses.ToString());
                }
                // Click and collect for CPOD service code
                else if (IsClickAndCollect(serviceCode, accountCode, shipmentType) && serviceCode.ToUpper().Equals("CPOD"))
                {
                    var statusCode = isFutile ? "93:1" : "99:4";
                    var statusDescription = isFutile ? "Failed to pickup" : "Picked up by customer";
                    XElement fileContentElement = new("deliveryStatuses");
                    XElement deliveryStatuses = new("deliveryStatus",
                                            new XElement("type", type),
                                            new XElement("identifier", barcode),
                                            new XElement("code", statusCode),
                                            new XElement("description", statusDescription),
                                            new XElement("eventTime", eventTime),
                                            new XElement("location", suburb),
                                            new XElement("signedBy", podName)
                                        );
                    fileContentElement.Add(deliveryStatuses);
                    fileContent.Add(deliveryStatuses.ToString());
                }
                else
                {
                    var statusCode = shipmentType.ToUpper().Equals("LEGACY") && IsReturnServiceCode(serviceCode)
                                    ? "99:2" : "99";

                    var description = shipmentType.ToUpper().Equals("LEGACY") && IsReturnServiceCode(serviceCode)
                                     ? "Returned to Store Pick-Up" : "Order delivered";

                    // If job is Futile check reason code. 
                    // If reason code is Failed delivery -then statusid 93, Customer refusal - 96

                    XElement deliveryStatuses = new("deliveryStatuses");
                    XElement deliveryStatusElement = new("deliveryStatus",
                        new XElement("type", type),
                        new XElement("identifier", barcode),
                        new XElement("code", (isFutile ? (isFutileAdditionalJob ? "90" : (futileReason.ToLower() == "rejected by customer" ? (shipmentType.ToUpper().Equals("LEGACY") ? "93" : "96") : "93")) : statusCode)),
                        new XElement("description", (isFutile ? (isFutileAdditionalJob ? "Received at hub" : (futileReason.ToLower() == "rejected by customer" ? (shipmentType.ToUpper().Equals("LEGACY") ? "Failed delivery" : "Customer refusal") : "Failed delivery")) : description)),
                        new XElement("eventTime", eventTime),
                        new XElement("location", suburb),
                        new XElement("signedBy", podName)
                    );

                    deliveryStatuses.Add(deliveryStatusElement);
                    fileContent.Add(deliveryStatuses.ToString());
                }
            }
            return fileContent;
        }

        private static bool IsClickAndCollect(string serviceCode, string accountCode, string shipmentType)
        {
            return !string.IsNullOrWhiteSpace(accountCode) && accountCode.ToUpper().Equals("KMIKCC") && shipmentType.ToUpper().Equals("CCD");
        }

        private static void GetXmlContentForDeliveryStatus(string barcode, string podName, string suburb, string type, List<string> fileContent, string eventTime, string statusCode, string statusDescription, string shipmentType, int driverId)
        {
            XElement fileContentElement = new("deliveryStatuses");
            XElement deliveryStatuses = new("deliveryStatus",
                                    new XElement("type", type),
                                    new XElement("identifier", barcode),
                                    new XElement("code", statusCode),
                                    new XElement("description", statusDescription),
                                    new XElement("eventTime", eventTime),
                                    new XElement("location", suburb),
                                    new XElement("signedBy", podName)
                                );
            fileContentElement.Add(deliveryStatuses);
            if ((shipmentType.ToUpper().Equals("LCD") && statusCode == "92")
                || (shipmentType.ToUpper().Equals("CCD") && statusCode == "92")
                || (shipmentType.ToUpper().Equals("LEGACY") && statusCode == "19"))
                GetXmlContentForVehicleDetails(barcode, type, eventTime, fileContentElement, driverId);
            fileContent.Add(fileContentElement.ToString());
        }

        private static void GetXmlContentForVehicleDetails(string barcode, string type, string eventTime, XElement fileContentElement, int driverId)
        {
            var xCabDriverRepository = new XCabDriverRepository();
            string driverClass = xCabDriverRepository.GetDriverClass(driverId).Result;
            // Database call to get driver class
            string fuelType = !string.IsNullOrEmpty(driverClass) && driverClass.ToUpper().Contains("ZE") ? "ELECTRIC" : "DIESEL";
            XElement vehicleDetail = new("vehicleDetail",
                                    new XElement("type", type),
                                    new XElement("identifier", barcode),
                                    new XElement("vehicle", "TRUCK"),
                                    new XElement("fuelType", fuelType),
                                    new XElement("eventTime", eventTime)
                                );
            fileContentElement.Add(vehicleDetail);
        }

        public async Task<List<ExtraFields>> GetTypeDetails(int bookingId)
        {
            var lstIkeaExtraFields = new List<ExtraFields>();
            var extraReferencesRepository = new ExtraReferencesRepository();

            var extraReferences = await extraReferencesRepository.GetXCabExtraReferencesesAsync(bookingId);

            if (extraReferences != null && extraReferences.Any())
            {
                foreach (var extraReference in extraReferences)
                {
                    if (extraReference.Name.ToUpper() == "SHIPMENTTYPE" ||
                       extraReference.Name.ToUpper() == "FUTILEREASON" ||
                       extraReference.Name.ToUpper() == "CASENUMBER")
                    {
                        lstIkeaExtraFields.Add(new ExtraFields() { Key = extraReference.Name.ToUpper(), Value = extraReference.Value });

                    }
                }
            }

            return lstIkeaExtraFields;
        }

        private static bool IsReturnServiceCode(string serviceCode)
        {
            return !string.IsNullOrWhiteSpace(serviceCode) &&
                ((serviceCode.ToUpper().StartsWith("X") || serviceCode.ToUpper().StartsWith("R")));
        }

        public static async Task IKEATracking(RabbitMqTrackingJob job, string eventDateTime, string podName)
        {
            var ikeaService = new IkeaTrackingService();
            var xCabItemsRepository = new XCabItemsRepository();
            var futileServiceManager = new FutileServiceManager(new ILogixFutileJobRepository());
            var shipmentType = string.Empty;
            var futileReason = string.Empty;
            var skipFileCreation = false;
            var isFutileJob = false;
            var isFutileAdditionalJob = false;
            //foreach (var job in jobs)
            //{

            try
            {
                var barcodes = await xCabItemsRepository.GetBarcodes(job.BookingId);
                var extraReferences = await ikeaService.GetTypeDetails(job.BookingId);
                if (extraReferences != null && extraReferences.Any())
                {
                    shipmentType = extraReferences.Where(x => x.Key.ToUpper() == "SHIPMENTTYPE") != null &&
                        extraReferences.Where(x => x.Key.ToUpper() == "SHIPMENTTYPE").Count() > 0 ? extraReferences.Where(x => x.Key.ToUpper() == "SHIPMENTTYPE").FirstOrDefault().Value : "";
                    futileReason = extraReferences.Where(x => x.Key.ToUpper() == "FUTILEREASON") != null
                        && extraReferences.Where(x => x.Key.ToUpper() == "FUTILEREASON").Count() > 0 ? extraReferences.Where(x => x.Key.ToUpper() == "FUTILEREASON").FirstOrDefault().Value : "";
                }
                // Assign the default shipment type if it is not found in the database
                if (string.IsNullOrWhiteSpace(shipmentType))
                {
                    shipmentType = !string.IsNullOrWhiteSpace(job.AccountCode)
                    && job.AccountCode.ToUpper().Trim().Contains("KMIKC") ? "CCD" : "LCD";
                }

                // Assign the default futile reason if it is not found in the database
                if (string.IsNullOrWhiteSpace(futileReason))
                {
                    futileReason = "Failed delivery";
                }

                if (!string.IsNullOrWhiteSpace(job.ServiceCode) && job.ServiceCode.ToUpper().Equals("ICAN"))
                {
                    skipFileCreation = true;
                }
                else if (IsClickAndCollect(job.ServiceCode, job.AccountCode, shipmentType) && job.ServiceCode.ToUpper().Equals("CPOD") && job.CurrentTrackingEvent == Data.Model.Tracking.ETrackingEvent.PickupComplete)
                {
                    skipFileCreation = true;
                }
                else if (IsClickAndCollect(job.ServiceCode, job.AccountCode, shipmentType) && job.ServiceCode.ToUpper().Equals("FUT"))
                {
                    skipFileCreation = true;
                }
                else
                {
                    var futileJobDetails = await futileServiceManager.GetFutileDetails(job.ConsignmentNumber);
                    if (futileJobDetails != null && futileJobDetails.CurrentLeg != null)
                    {
                        if (futileJobDetails.CurrentLeg.IsUltimateOfBatch && futileJobDetails.CurrentLeg.IsUltimateOfJob)
                        {
                            isFutileAdditionalJob = true;
                            isFutileJob = true;

                            if (job.CurrentTrackingEvent == Data.Model.Tracking.ETrackingEvent.PickupComplete)
                                skipFileCreation = true;
                        }
                        else if (!futileJobDetails.CurrentLeg.IsUltimateOfBatch && !futileJobDetails.CurrentLeg.IsUltimateOfJob)
                        {
                            skipFileCreation = true;
                            isFutileAdditionalJob = true;
                            isFutileJob = true;
                        }
                        else if (futileJobDetails.CurrentLeg.IsUltimateOfBatch && !futileJobDetails.CurrentLeg.IsUltimateOfJob)
                        {
                            isFutileJob = true;
                        }
                    }
                }

                if (job.CurrentTrackingEvent == Data.Model.Tracking.ETrackingEvent.PickupComplete && !skipFileCreation)
                {
                    if (barcodes.Count > 0)
                    {
                        foreach (var barcode in barcodes)
                        {
                            var fileContents = ikeaService.GetFileContent(barcode, "Pickup", eventDateTime, podName, job.FromSuburb, shipmentType, job.ConsignmentNumber, job.ServiceCode, isFutileJob, isFutileAdditionalJob, futileReason, job.DriverId, job.AccountCode);
                            foreach (var fileContent in fileContents)
                            {
                                await CreateFtpFileAndUploadViaSftp(job, fileContent, barcode);
                            }
                        }
                    }
                    else
                    {
                        var fileContents = ikeaService.GetFileContent(job.Ref1, "Pickup", eventDateTime, podName, job.FromSuburb, shipmentType, job.ConsignmentNumber, job.ServiceCode, isFutileJob, isFutileAdditionalJob, futileReason, job.DriverId, job.AccountCode, "SHIPMENT");
                        foreach (var fileContent in fileContents)
                        {
                            await CreateFtpFileAndUploadViaSftp(job, fileContent, job.Ref1);
                        }
                    }
                }
                if (job.CurrentTrackingEvent == Data.Model.Tracking.ETrackingEvent.DeliveryArrive && !skipFileCreation)
                {
                    DeliveryArriveDateTime ??= new Dictionary<int, DateTime>();
                    if (DeliveryArriveDateTime.ContainsKey(job.BookingId))
                        DeliveryArriveDateTime[job.BookingId] = DateTime.Parse(eventDateTime);
                    else
                        DeliveryArriveDateTime.Add(job.BookingId, DateTime.Parse(eventDateTime));
                }
                else if (job.CurrentTrackingEvent == Data.Model.Tracking.ETrackingEvent.DeliveryComplete && !skipFileCreation)
                {
                    eventDateTime = await GetDeliveryTime(eventDateTime, job.BookingId);
                    if (barcodes.Count > 0)
                    {
                        foreach (var barcode in barcodes)
                        {
                            var fileContents = ikeaService.GetFileContent(barcode, "Delivery", eventDateTime, podName, job.FromSuburb, shipmentType, job.ConsignmentNumber, job.ServiceCode, isFutileJob, isFutileAdditionalJob, futileReason, job.DriverId, job.AccountCode);
                            foreach (var fileContent in fileContents)
                            {
                                await CreateFtpFileAndUploadViaSftp(job, fileContent, barcode);
                            }
                        }
                    }
                    else
                    {
                        var fileContents = ikeaService.GetFileContent(job.Ref1, "Delivery", eventDateTime, podName, job.FromSuburb, shipmentType, job.ConsignmentNumber, job.ServiceCode, isFutileJob, isFutileAdditionalJob, futileReason, job.DriverId, job.AccountCode, "SHIPMENT");
                        foreach (var fileContent in fileContents)
                        {
                            await CreateFtpFileAndUploadViaSftp(job, fileContent, job.Ref1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                    $"Error in IKEATracking for jobnumber {job.JobNumber}. Deatails: {ex.Message}", "IkeaTrackingService: IKEATracking");
            }
            //}
        }

        private static async Task<string> GetDeliveryTime(string deliveryCompleteTime, int bookingId)
        {
            var eventDateTime = string.Empty;
            var xCabTimeSlotsRepository = new XCabTimeSlotsRepository();
            try
            {
                var xCabTimeSlots = await xCabTimeSlotsRepository.GetTimeSlot(bookingId);

                if (xCabTimeSlots != null && xCabTimeSlots.StartDateTime != DateTime.MinValue)
                {
                    var endTime = xCabTimeSlots.StartDateTime.AddMinutes(xCabTimeSlots.Duration);
                    var deliveryArriveTime = DeliveryArriveDateTime != null && DeliveryArriveDateTime.ContainsKey(bookingId) ? DeliveryArriveDateTime[bookingId] : Convert.ToDateTime(deliveryCompleteTime);
                    if (DateTime.Compare(xCabTimeSlots.StartDateTime, deliveryArriveTime) <= 0 && DateTime.Compare(deliveryArriveTime, endTime) <= 0)
                    {
                        eventDateTime = DeliveryArriveDateTime[bookingId].ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        eventDateTime = deliveryCompleteTime;
                    }
                }
                else
                {
                    eventDateTime = deliveryCompleteTime;
                }
            }
            catch (Exception ex)
            {
                await Logger.Log(
                    $"Error in IKEATracking while extarcting Delivery Time for {bookingId}. Deatails: {ex.Message}", "IkeaTrackingService: IKEATracking");
            }
            return eventDateTime;
        }

        private static async Task CreateFtpFileAndUploadViaSftp(RabbitMqTrackingJob job, string fileContent, string identifier)
        {
            if (!job.SkipFtpAccess)
            {
                var sourceFileName = string.Empty;
                var nfsFileService = new NfsFileService();
                try
                {
                    try
                    {
                        var trackingFolderPath = Path.Combine(DbSettings.Default.LocalDownloadFolder, job.LoginDetails.UserName, job.LoginDetails.TrackingFolderName);

#if DEBUG
                        trackingFolderPath = "C:\\Test\\IKEA";
#endif

                        var fileName = $"{job.AccountCode}_{GetDateTimeForFile()}_{identifier}.xml";
                        sourceFileName = Path.Combine(trackingFolderPath, fileName);
                        File.WriteAllText(sourceFileName, fileContent);
                    }
                    catch (Exception e)
                    {
                        await Logger.Log(
                           "Error occurred while storing the Tracking File Locally on the Server.FileName:" + sourceFileName + ", AccountCode: " +
                           job.AccountCode + ", XCab Username:" + job.LoginDetails.UserName + ", Exception Details:" +
                           e.Message, "IkeaTrackingService");
                    }
                }
                catch (Exception e)
                {
                    var destinationErrorFileName = Path.Combine(DbSettings.Default.NfsUrlString, job.LoginDetails.UserName, job.LoginDetails.TrackingFolderName, DbSettings.Default.ErrorFolderName + "\\", sourceFileName.GetFtpFileName());
                    var moveFileStatus = nfsFileService.MoveFileWithoutTimestamp(sourceFileName, destinationErrorFileName);
                    if (!moveFileStatus)
                    {
                        await Logger.LogSlackNotificationFromApp("TrackingFileCreator", $"Error occurred when extracting data to remoteFtpPush. Failed to move tracking file {sourceFileName} back to local FTP location {destinationErrorFileName}.Message: {e.Message}.", "XCab Tracking Engine:CreateFtpFileAndUploadViaSftp", SlackChannel.GeneralErrors);
                        RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Error occurred when extracting data to remoteFtpPush. Failed to move tracking file {sourceFileName} back to local FTP location {destinationErrorFileName}.Message: {e.Message}.", ELogTypes.Error);
                    }
                    await Logger.Log(
                        $"Error occurred while creating a tracking file job:{job.JobNumber}, AccountCode:{job.AccountCode}, XCab Username:{job.LoginDetails.UserName}, Details:{e.Message}", "IkeaTrackingService");
                    RollingLogger.WriteToIkeaTrackingFileCreatorLogs($"Error occurred while creating a tracking file job:{job.JobNumber}, AccountCode:{job.AccountCode}, XCab Username:{job.LoginDetails.UserName}, Details:{e.Message}", ELogTypes.Error);
                }
            }
        }
        private static string GetDateTimeForFile()
        {
            var uploadTime = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" +
                             DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" +
                             DateTime.Now.Minute + "_" + DateTime.Now.Millisecond;
            return uploadTime;
        }

        public static Dictionary<int, DateTime>? DeliveryArriveDateTime { get; set; }

    }
}
