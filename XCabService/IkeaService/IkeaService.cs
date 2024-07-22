using System.Text;
using System.Xml.Linq;
using Data.Entities;
using Data.Entities.Booking.TimeSlots;
using Data.Entities.ExtraReferences;
using Data.Entities.Items;
using Data.Model.Route;
using Data.Repository.V2;
using Microsoft.Extensions.Logging;
using Data.Repository.EntityRepositories.ExtraReferences.Interface;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using XCabService.SimpleEmailService;
using XCabService.SimpleFtpService;
using Item = Core.Item;
using System.Collections.Generic;
using Ftp;
using Core.Helpers;
using Data.Model;
using Core.Logging.SeriLog;
using Quartz.Util;

namespace XCabService.IkeaService;

public class IkeaService : IIkeaService
{
    private const int IKEA_LOGIN = 160;
    private const string IKEA_DEFAULT_SERVICE = "CPOD";
    private const string softwareDevEmail = "software-development@capitaltransport.com.au";
    private readonly ILogger<IkeaService> _logger;
    private readonly IXCabBookingRepository _dbService;
    private readonly ISimpleFtpService _ftpService;
    private readonly IExtraReferencesRepository _extraReferencesRepository;
    private readonly ISimpleEmailService _simpleEmailService;
    private ISftpService _sftpService;

    private static class IkeaSettings
    {
        public static string SftpHostname { get; set; } = "ft.centiro.ikea.com";
        public static string SftpUsername { get; set; } = "Kingstransport.au";
        public static string SftpPassword { get; set; } = "zritXKbHDAJ664";
        public static string SftpRemotePath { get; set; } = "/in/transport/upload_temp/";
        public static int SftpPort { get; set; } = 22;
        public static string CancelServiceCode { get; set; } = "ICAN";
        public static string ITDeliveryEmailAddress { get; set; } = "ITDelivery@kingsgroup.com.au";
    }

    public IkeaService(
        ILogger<IkeaService> logger,
        IXCabBookingRepository dbService,
        IExtraReferencesRepository extraReferencesRepository,
        ISimpleFtpService ftpService,
        ISimpleEmailService simpleEmailService,
        ISftpService sftpService)
    {
        _dbService = dbService;
        _logger = logger;
        _extraReferencesRepository = extraReferencesRepository;
        _ftpService = ftpService;
        _simpleEmailService = simpleEmailService;
        _sftpService = sftpService;
    }

    public async Task<string> CreateUpdateXml(
        string shipmentNo,
        string status,
        DateTime eventTime,
        string podName,
        string suburb, string shipmentType)
    {
        string description;
        string statusId;
        switch (status)
        {
            case "DELIVERED":
                statusId = "99";
                description = "Order Delivered";
                break;
            case "PICKEDUP":
                statusId = "92";
                description = "Loaded on delivery truck";
                break;
            case "FUTILE":
                statusId = "98:2";
                description = "Returned to store";
                break;
            default:
                throw new Exception($"Unknown status: {status}");
        }

        if (string.IsNullOrEmpty(podName))
        {
            podName = "MANUAL";
        }

        var packageType = shipmentType == "LEGACY" ? "SHIPMENT" : "PACKAGE";
        var eventDateTime = eventTime.ToString("yyyy-MM-dd HH:mm:ss");

        XElement deliveryStatuses = new XElement("deliveryStatuses");
        XElement deliveryStatusElement = new XElement("deliveryStatus",
            new XElement("type", "PACKAGE"),
            new XElement("identifier", shipmentNo),
            new XElement("code", statusId),
            new XElement("description", description),
            new XElement("eventTime", eventTime),
            new XElement("location", suburb),
            new XElement("signedBy", podName)
        );

        deliveryStatuses.Add(deliveryStatusElement);

        return deliveryStatuses.ToString();
    }

    private void CreateCancelledJob(XCabBooking booking)
    {
        var date = DateTime.Now;
        booking.ServiceCode = IkeaSettings.CancelServiceCode;
        booking.AdvanceDateTime = date;
        booking.DespatchDateTime = date;
        booking.OkToUpload = true;
        booking.UploadDateTime = null;
        booking.UploadedToTplus = false;
        _dbService.InsertBooking(booking);
    }

    private async Task CreateIkeaBooking(IkeaBookingWrapper packet)
    {
        int bookingId = await _dbService.InsertBooking(packet.Booking);

        var newRefs = new List<XCabExtraReferences>
        {
            new()
            {
                PrimaryBookingId = bookingId,
                Name = "ShipmentType",
                Value = packet.Workflow
            }
        };

        _extraReferencesRepository.Insert(newRefs);
    }

    public async Task<bool> ProcessBookings(List<IkeaBookingWrapper> bookingWrapper)
    {
        bool success = false;
        try
        {
            foreach (var packet in bookingWrapper)
            {
                var bookingId = 0;
                switch (packet.ModifyType)
                {
                    case IkeaModifyType.Create:
                        await CreateIkeaBooking(packet);

                        break;
                    case IkeaModifyType.Cancel:
                        //var res = await _dbService.GetBookingByReferenceAndLogin(packet.Booking.Ref2, IKEA_LOGIN);
                        var futile = await _dbService.IsBookingFutiledForReference(packet.Booking.Ref2, IKEA_LOGIN);
                        if (futile) //This needs to be checking for FUTILE, if this booking was futiled
                        {
                            //if futiled send an XML
                            string xml = await CreateUpdateXml(packet.Booking.Ref1, "FUTILE", DateTime.Now, "MANUAL",
                                "MANUAL", packet.Workflow);
                            byte[] fileContent = Encoding.UTF8.GetBytes(xml);
                            var guid = Guid.NewGuid().ToString() + ".xml";
                            //has to be uploaded : /in/transport to remote SFTP                            
                            var uploadSuccess = await _sftpService.UploadByteArray(IkeaSettings.SftpHostname,
                                IkeaSettings.SftpPort,
                                IkeaSettings.SftpUsername, IkeaSettings.SftpPassword, fileContent,
                                Path.Combine(IkeaSettings.SftpRemotePath, guid));
                            //var uplaodSuccess = await _ftpService.UploadFile(
                            //    "t1-lx-dev01",
                            //    "ikea",
                            //    "ikeapass",
                            //    "tracking",
                            //    guid,
                            //    fileContent
                            //);

                            if (packet.Booking != null)
                            {
                                CreateCancelledJob(packet.Booking);
                            }
                        }
                        else
                        {
                            //if its not futiled we check if its still in ASN, if yes cancel the booking
                            var res = await _dbService.GetBookingByReferenceLoginAndCode(packet.Booking.Ref2,
                                IKEA_LOGIN, packet.Booking.ServiceCode);
                            //else if its been released send an email to: IKEAVIC,WA and IKEAQLD-CHECK iVAN'S EMAIL
                            if (res != null)
                            {
                                _ = await _dbService.CancelXCabBooking(res.BookingId);
                            }
                            else
                            {
                                var emailBody =
                                    @"A Cancellation request has been raised by Ikea for a job with a status of allocated.
                                                Please check whether the job can be manually cancelled, otherwise advise Ikea that the cancellation request will need to be sent after a failed delivery.
                                                Account: " + packet.Booking.AccountCode + @"
                                                Shipment Number: " + packet.Booking.Ref1 + @"
                                                Work Order Number: " + packet.Booking.Ref2 +
                                    @"                                               
                                                ";
                                var recipientEmailAddress =
                                    IkeaHelper.GetIkeaEmailAddressGroups(packet.Booking.StateId);
#if DEBUG
								recipientEmailAddress = "software-development@capitaltransport.com.au";
								IkeaSettings.ITDeliveryEmailAddress = recipientEmailAddress;
#endif
                                await _simpleEmailService.SendHtmlEmail(
                                    new List<string>() { recipientEmailAddress, IkeaSettings.ITDeliveryEmailAddress },
                                    $"IKEA Job Cancellation Request",
                                    emailBody);
                            }
                        }

                        break;
                    case IkeaModifyType.Update:
                        var updateResult = await _dbService.UpdateBooking(packet.Booking);

                        string message = string.Empty;
                        string subject = string.Empty;
                        List<string> recieverEmail = new List<string>()
                            { IkeaHelper.GetIkeaEmailAddress(packet.Booking.StateId) };


                        if (updateResult == XCabBookingRepository.UpdateStatus.FailedUnknown)
                        {
                            subject = $"Ikea file update failed for order: {packet.Booking.Ref1}";
                            message = subject + $"\nResult: {updateResult.ToString()}";
                            await _simpleEmailService.SendHtmlEmail(recieverEmail, subject, message);
                            continue;
                        }

                        if (updateResult == XCabBookingRepository.UpdateStatus.FailedNotFound)
                        {
                            if (packet.Booking.XCabTimeSlots.StartDateTime.Date >= DateTime.Now.Date)
                            {
                                await CreateIkeaBooking(packet);
                            }
                            else
                            {
                                subject = $"Ikea file Update failed for order: {packet.Booking.Ref1}";
                                message = subject + $"\nResult: {updateResult.ToString()}";

                                await _simpleEmailService.SendHtmlEmail(recieverEmail, subject, message);
                            }
                        }
                        else if (updateResult != XCabBookingRepository.UpdateStatus.Success)
                        {
                            if (packet.Booking.XCabTimeSlots.StartDateTime.Date > DateTime.Now.Date)
                            {
                                await CreateIkeaBooking(packet);
                            }
                            else
                            {
                                subject = $"Ikea file Update failed for order: {packet.Booking.Ref1}";
                                message = subject + $"\nResult: {updateResult.ToString()}";

                                await _simpleEmailService.SendHtmlEmail(recieverEmail, subject, message);
                            }
                        }

                        break;
                }
            }

            success = true;
        }
        catch (Exception ex)
        {
            RollingLogger.WriteToShredderProcessorLogs(
                $"Execute Method - Exception occurred when extracting bookings for Ikea, details:{ex.Message}",
                ELogTypes.Error);

            _logger.LogError(ex, "Failed to process booking");
        }

        return success;
    }

    public async Task<string> GenerateAccountCode(string senderBu)
    {
        string code;

        switch (senderBu)
        {
            case "006":
                code = "KMIKS";
                break;
            case "034":
                code = "KMIKC";
                break;
            case "CC":
                code = "KMIKCC";
                break;
            case "384":
                code = "KMIKR";
                break;
            case "556":
                code = "KPIKP";
                break;
            case "385":
                code = "KSIKR";
                break;
            default:
                code = String.Empty;
                break;
        }

        return code;
    }

    public async Task<string> GenerateServiceCode(string storeRef, double weight, bool isExchange = false,
        bool isReturn = false)
    {
        var mid = storeRef switch
        {
            "SGR90000897" => "12",
            _ => "6"
        };

        string prefix = string.Empty;
        if (isReturn && isExchange)
        {
            prefix = "X";
        }
        else if (isReturn && !isExchange)
        {
            prefix = "R";
        }
        else // (isExchange && !isReturn) || (!isExchange && !isReturn)
        {
            prefix = "I";
        }

        var postfix = weight switch
        {
            <= 26 => "XS",
            <= 51 => "S",
            <= 151 => "M",
            > 151 => "L",
            _ => null
        };

        if (postfix is null || mid is null)
        {
            return IKEA_DEFAULT_SERVICE;
        }

        return $"{prefix}{mid}{postfix}";
    }

    public async Task<int> GenerateAccountStateId(string accountCode)
    {
        int stateId;

        switch (accountCode)
        {
            case "006":
                stateId = 1;
                break;
            case "034":
                stateId = 1;
                break;
            case "CC":
                stateId = 1;
                break;
            case "384":
                stateId = 1;
                break;
            case "556":
                stateId = 5;
                break;
            case "385":
                stateId = 2;
                break;
            default:
                stateId = 0;
                break;
        }

        return stateId;
    }

    public async Task<TimeOnly> ParseTime(string timeString)
    {
        TimeOnly time;
        try
        {
            time = new TimeOnly(int.Parse(timeString.Substring(0, 2)), int.Parse(timeString.Substring(2)), 0);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, @"Failed to parse time component from string: {Time}", timeString);
            time = new TimeOnly(7, 0, 0);
        }

        return time;
    }

    public async Task ProcessPinroutes(List<IkeaPinrouteModifier> pinroutes, string fileName)
    {
        try
        {
            if (pinroutes != null & pinroutes.Count > 0)
            {
                var failedReferencesToUpload = new List<IkeaBookingUploadReference>();
                foreach (var route in pinroutes)
                {
                    var bookings = await _dbService.GetBookingsByBookingIdAndLogin(route.Reference, IKEA_LOGIN);
                    if (bookings?.Count == 0) continue;

                    foreach (var booking in bookings)
                    {
                        booking.Caller = route.RouteName;
                        booking.XCabBookingRoute = new XCabBookingRoute()
                            { DropSequence = route.Sequence, Route = route.RouteName };
						booking.ETA = route.Eta.ToString();

						var updated = await _dbService.UpdateBookingRoute(booking, true);
                        if (!updated)
                        {
                            var ikeaBookingUploadReference = new IkeaBookingUploadReference()
                            {
                                Reference1 = booking.Ref1,
                                StateId = booking.StateId,
                            };
                            failedReferencesToUpload.Add(ikeaBookingUploadReference);
                        }
						var updatedEta = await _dbService.UpdateBookingETA(booking.BookingId, booking.ETA);
						if (!updatedEta)
						{
							_logger.LogError($"Failed to update ETA from pinroutes file {fileName} for booking ID {booking.BookingId}");
						}
					}
                }

                if (failedReferencesToUpload != null && failedReferencesToUpload.Count > 0)
                {
                    foreach (var reference in failedReferencesToUpload)
                    {
                        if (reference.StateId == 1)
                        {
                            var receiverEmail = IkeaHelper.GetIkeaEmailAddress(1);
                            await _simpleEmailService.SendHtmlEmail(
                                new List<string>() { receiverEmail, softwareDevEmail },
                                $"Ikea VIC Pinroutes file update failed using the booking file {fileName}",
                                $"Pinroutes update failed for Ikea orders: {string.Join(", ", reference.Reference1)}");
                        }

                        if (reference.StateId == 2)
                        {
                            var receiverEmail = IkeaHelper.GetIkeaEmailAddress(2);
                            await _simpleEmailService.SendHtmlEmail(
                                new List<string>() { receiverEmail, softwareDevEmail },
                                $"Ikea NSW Pinroutes file update failed using the booking file {fileName}",
                                $"Pinroutes update failed for Ikea orders: {string.Join(", ", reference.Reference1)}");
                        }

                        if (reference.StateId == 5)
                        {
                            var receiverEmail = IkeaHelper.GetIkeaEmailAddress(5);
                            await _simpleEmailService.SendHtmlEmail(
                                new List<string>() { receiverEmail, softwareDevEmail },
                                $"Ikea WA Pinroutes file update failed using the booking file {fileName}",
                                $"Pinroutes update failed for Ikea orders: {string.Join(", ", reference.Reference1)}");
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to process pinroutes file {fileName}");
        }
    }

    public async Task<List<IkeaPinrouteModifier>> ParsePinrouteXls(string fileName)
    {
        List<Dictionary<string, string>> parsedData = new List<Dictionary<string, string>>();
        var modifierList = new List<IkeaPinrouteModifier>();

        try
        {
            using SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false);
            WorkbookPart? workbookPart = document.WorkbookPart;
            WorksheetPart? worksheetPart = workbookPart.WorksheetParts.FirstOrDefault();
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
            Row headerRow = sheetData.Elements<Row>().First();
            Dictionary<int, string> columnIndexToHeader = new Dictionary<int, string>();
            int columnIndex = 0;
			
			foreach (Cell cell in headerRow.Elements<Cell>())
			{
				string headerValue = GetCellValue(cell, workbookPart);
				if (!string.IsNullOrEmpty(headerValue))
				{
					columnIndexToHeader.Add(columnIndex, headerValue);
				}
				columnIndex++;
			}
			
			foreach (Row row in sheetData.Elements<Row>().Skip(1))
            {
                try
                {
                    Dictionary<string, string> rowData = new Dictionary<string, string>();
                    columnIndex = 0;
					foreach (Cell cell in row.Elements<Cell>())
					{
						string header = columnIndexToHeader[columnIndex];
						string rowDataValue = GetCellValue(cell, workbookPart);
						if (!string.IsNullOrEmpty(rowDataValue))
						{
							rowData.Add(header, rowDataValue);							
						}
						columnIndex++;
					}
					parsedData.Add(rowData);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "An error was detected in a single row from pinroute file {FileName}",
                        fileName);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cannot process the pinroute file {FileName}", fileName);
        }

        foreach (var fragment in parsedData)
        {
            try
            {
                if (fragment != null && fragment.Count > 0)
                {
                    var routeModifier = new IkeaPinrouteModifier();
                    routeModifier.RouteStart = await ParseTime(fragment["Route Start"]);
                    routeModifier.Eta = await ParseTime(fragment["ETA"]);
                    routeModifier.Reference = fragment["Comment 3"];
                    routeModifier.DriverNumber = fragment["Driver Number"];
                    routeModifier.RouteName = fragment["Driver Number"];
                    routeModifier.Sequence = fragment["Visit Sequence"];

                    modifierList.Add(routeModifier);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Skipping what appears to be a blank row in the pinroute file:  {FileName}", fileName);
            }
        }

        return modifierList;
    }

	private string GetCellValue(Cell cell, WorkbookPart workbookPart)
	{
		if (cell.DataType != null && cell.DataType.Value == CellValues.InlineString)
		{
			InlineString inlineString = cell.Elements<InlineString>().FirstOrDefault();
			if (inlineString != null && inlineString.Text != null)
			{
				return inlineString.Text.Text;
			}
		}
        else if (!string.IsNullOrEmpty(cell.CellValue.Text) && cell.CellValue.Text.Length > 2)
        {
			var cellValue = cell.Elements<CellValue>().FirstOrDefault();
			if (cellValue != null && cellValue.Text != null)
			{
				return cellValue.Text;
			}
		}
        else
        {
			SharedStringTablePart stringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
			string value = cell.CellValue.Text;
			if (stringTablePart!= null && cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
				return stringTablePart.SharedStringTable.ElementAt(int.Parse(value)).InnerText;				 
			}
            else if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
		}		
		return null;
	}

	public async Task<List<IkeaBookingWrapper>> ParseExportXml(string fileNameWithPath, int ftpLoginId)
    {
        var doc = XDocument.Load(fileNameWithPath);

        doc.Root?.Attributes().Where(x => x.IsNamespaceDeclaration).Remove();
        foreach (var elem in doc.Descendants()) elem.Name = elem.Name.LocalName;

        var nodes = from el in doc.Root?.Elements() select el;
        var bookings = new List<IkeaBookingWrapper>();

        foreach (var node in nodes)
        {
            string
                workflow = node.Element("shipmentType")
                    ?.Value; //TODO: Check this property shipment cannot be null and we are not having it nullable at this stage          
            var isExchange = node.Element("exchange")?.Value?.ToUpper() == "TRUE";
            var isReturn = node.Element("deliveryCode")?.Value?.ToUpper() == "RET";
            var isEdiReady = node.Element("edi2")?.Value?.ToUpper() == "TRUE";
            string senderBu = node.Element("senderBUCode")?.Value ?? string.Empty;

            var delMethod = node.Element("deliveryMethod")?.Value ?? string.Empty;
            var totalWeight = node.Element("totalWeight")?.Value ?? string.Empty;
            IEnumerable<XElement>? attribs = null;
            IEnumerable<XElement>? delInstructions = null;
            XElement? addSend = null;
            XElement? addRec = null;
            XElement? addCollection = null;
            string recieverName = "";
            if (!delMethod.ToUpper().StartsWith("SGR"))
            {
                workflow = "LEGACY";
            }

            try
            {
                addSend = (
                    from el in node.Elements("addresses").Elements("address")
                    where el.Element("code")?.Value == "PICKUP"
                    select el
                ).FirstOrDefault();
            }
            catch (Exception e)
            {
                RollingLogger.WriteToShredderProcessorLogs(
                    $"Execute Method - Exception occurred when extracting addsend for Ikea, details:{e.Message}",
                    ELogTypes.Error);
                _logger.LogError(e, "Failed to process booking for addSend");
            }

            try
            {
                addRec = (
                    from el in node.Elements("addresses").Elements("address")
                    where el.Element("code")?.Value == "RECEIVER"
                    select el
                ).FirstOrDefault();
            }
            catch (Exception e)
            {
                RollingLogger.WriteToShredderProcessorLogs(
                    $"Execute Method - Exception occurred when extracting addRec for Ikea, details:{e.Message}",
                    ELogTypes.Error);
                _logger.LogError(e, "Failed to process booking for addRec");
            }

            try
            {
                addCollection = (
                    from el in node.Elements("addresses").Elements("address")
                    where el.Element("code")?.Value == "COLLECTIONPOINT"
                    select el
                ).FirstOrDefault();
                if (addCollection is not null) senderBu = "CC";
            }
            catch (Exception e)
            {
                RollingLogger.WriteToShredderProcessorLogs(
                    $"Execute Method - Exception occurred when extracting addcollection for Ikea, details:{e.Message}",
                    ELogTypes.Error);
                _logger.LogError(e, "Failed to process booking for addCollection");
            }

            try
            {
                attribs = (
                    from el in node.Elements("attributes").Elements("attribute")
                    select el);
            }
            catch (Exception e)
            {
                RollingLogger.WriteToShredderProcessorLogs(
                    $"Execute Method - Exception occurred when extracting attribs for Ikea, details:{e.Message}",
                    ELogTypes.Error);
                _logger.LogError(e, "Failed to process booking for addCollection");
            }

            var booking = new XCabBooking
            {
                Remarks = new List<string>(),

                lstItems = new List<Item>(),
                OkToUpload = false
            };


            var wrapper = new IkeaBookingWrapper
            {
                Workflow = workflow
            };

            booking.ATL = false;
            if (attribs != null)
            {
                foreach (var el in attribs)
                {
                    if (el.Element("code")?.Value == "AUTH_TO_LEAVE")
                    {
                        if (el.Element("data")?.Value == "TRUE")
                        {
                            booking.ATL = true;
                            break;
                        }
                    }
                }
            }

            if (addRec != null)
            {
                recieverName = addRec.Element("name")?.Value ?? string.Empty;
            }

            string operation = "";
            try
            {
                operation = node.Element("operation")?.Value.ToUpper() ?? string.Empty;
            }
            catch (Exception e)
            {
                RollingLogger.WriteToShredderProcessorLogs(
                    $"Execute Method - Exception occurred when extracting operation for Ikea, details:{e.Message}",
                    ELogTypes.Error);
                _logger.LogError(e, "Failed to process booking for addCollection");
            }

            switch (operation)
            {
                case "CREATE":
                    wrapper.ModifyType = IkeaModifyType.Create;
                    break;
                case "CANCEL":
                    wrapper.ModifyType = IkeaModifyType.Cancel;
                    break;
                case "UPDATE":
                    wrapper.ModifyType = IkeaModifyType.Update;
                    break;
                default:
                    throw new Exception($"Operation not supported: {operation}");
            }


            booking.AccountCode = await this.GenerateAccountCode(senderBu);
            booking.ServiceCode =
                await this.GenerateServiceCode(delMethod, double.Parse(totalWeight), isExchange, isReturn);
            booking.StateId = await this.GenerateAccountStateId(node.Element("senderBUCode")?.Value ?? string.Empty);

            booking.LoginId = ftpLoginId;

            booking.TotalItems = node.Element("totalPackages")?.Value ?? string.Empty;
            booking.Ref1 = node.Element("shipmentNumber")?.Value ?? string.Empty;
            booking.Ref2 = node.Element("wmsOrderNumber")?.Value ?? string.Empty;
            if (booking.Ref2 == "")
            {
                booking.Ref2 = node.Element("orderNumber")?.Value ?? string.Empty;
            }

            booking.TotalWeight = node.Element("totalWeight")?.Value ?? "1";
            booking.TotalVolume = node.Element("totalVolume")?.Value ?? "1";

            booking.FromSuburb = addSend?.Element("city")?.Value ?? string.Empty;
            booking.FromPostcode = addSend?.Element("postalCode")?.Value ?? string.Empty;
            booking.FromDetail1 = addSend?.Element("name")?.Value ?? string.Empty;
            booking.FromDetail2 = addSend?.Element("address1")?.Value ?? string.Empty;
            booking.FromDetail3 = addSend?.Element("address2")?.Value ?? string.Empty;
            booking.FromDetail4 = addSend?.Element("address3")?.Value ?? string.Empty;

            booking.ToSuburb = (string?)addRec?.Element("city") ?? string.Empty;
            booking.ToPostcode = (string?)addRec?.Element("postalCode") ?? string.Empty;
            booking.ToDetail1 = recieverName;
            booking.ToDetail2 = (string?)addRec?.Element("address1") ?? string.Empty;
            booking.ToDetail3 = (string?)addRec?.Element("address2") ?? string.Empty;
            booking.ToDetail4 = (string?)addRec?.Element("address3") ?? string.Empty;

            var contactNumber = (string?)addRec?.Element("cellPhone") ?? string.Empty;
            if (!isExchange && !isReturn && addRec != null)
            {
                booking.Notification = new Core.Notification();
                booking.Notification.SMSNumber = contactNumber;
            }

            booking.ToDetail4 = $"Mob: {contactNumber}";

            int totalItems = 0;
            /*
             * If LEGACY -> take <totalPackages> value
             * If CDC/CFB but not EDI ready -> take <orderLine><quantity> sum
             * If EDI ready -> Add additional barcodes with qty 0
             */
            if (workflow == "LEGACY")
            {
                try
                {
                    _ = int.TryParse((node?.Element("totalPackages")?.Value ?? "1"), out var qty);
                    booking.lstItems.Add(new() { Quantity = qty, Barcode = string.Empty });
                    totalItems = qty;
                }
                catch (Exception e)
                {
                    RollingLogger.WriteToShredderProcessorLogs(
                        $"Execute Method - Exception occurred when extracting Item for legacy for Ikea, details:{e.Message}",
                        ELogTypes.Error);
                    _logger.LogError(e, "Failed to process booking for addCollection");
                }
            }
            else
            {
                int totalQty = 0;
                try
                {
                    foreach (var order in node.Elements("orderLines").Elements("orderLine"))
                    {
                        _ = int.TryParse((order?.Element("quantity")?.Value ?? "1"), out var qty);
                        totalQty += qty;
                    }

                    booking.lstItems.Add(new() { Quantity = totalQty, Barcode = string.Empty });
                    totalItems = totalQty;

                    if (isEdiReady)
                    {
                        try
                        {
                            foreach (var order in node.Elements("packages").Elements("package"))
                            {
                                Item item = new Item
                                {
                                    Description = order.Element("pickupLocation")?.Value ?? string.Empty,
                                    Barcode = order.Element("packageNumber")?.Value ??
                                              string.Empty, // CDU Identifier - used in tracking updates
                                    Quantity = 0
                                };

                                booking.lstItems.Add(item);
                            }
                        }
                        catch (Exception e)
                        {
                            RollingLogger.WriteToShredderProcessorLogs(
                                $"Execute Method - Exception occurred when extracting item for non legacy for Ikea, details:{e.Message}",
                                ELogTypes.Error);
                            _logger.LogError(e,
                                $"Failed to read package details for EDI ready booking {fileNameWithPath}",
                                fileNameWithPath);
                        }
                    }
                }
                catch (Exception e)
                {
                    RollingLogger.WriteToShredderProcessorLogs(
                        $"Execute Method - Exception occurred when extracting Qunatity for Ikea, details:{e.Message}",
                        ELogTypes.Error);
                    _logger.LogError(e, $"Failed to process any items for booking {fileNameWithPath}",
                        fileNameWithPath);
                }
            }

            int externalQty = 0;
            int storeQty = 0;           

            if (workflow == "LEGACY")
            {
                try
                {
                    foreach (var package in node?.Elements("packages").Elements("package"))
                    {
                        string location = package.Element("pickupLocation")?.Value?.ToUpper() ?? string.Empty;
                        switch (location)
                        {
                            case "STORE":
                                storeQty += 1;
                                break;
                            case "FULLSERVE":
                                externalQty += 1;
                                break;
                        }

                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not parse package locations for Legacy for {FileName}", fileNameWithPath);
                }
            }
            else
            {
                try
                {
                    foreach (var packageLocation in node?.Elements("PackageLocations").Elements("packageLocation"))
                    {
                        string location = packageLocation.Element("Location")?.Value?.ToUpper() ?? string.Empty;
                        switch (location)
                        {
                            case "STORE":
                                int.TryParse(packageLocation.Element("NumberOfBoxes").Value, out storeQty);
                                break;
                            case "EXTERNAL_FULLSERVE":
                                int.TryParse(packageLocation.Element("NumberOfBoxes").Value, out externalQty);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not parse package locations for {FileName}", fileNameWithPath);
                }
            }

            booking.ExtraPuInformation = $"Self Serve: {storeQty} / Full Serve: {externalQty}";

            delInstructions = (
                from el in node.Elements("deliveryInstructions").Elements("deliveryInstruction")
                select el
            );
            if (delInstructions != null)
            {
                try
                {
                    foreach (var instruction in delInstructions)
                    {
                        var instructionCode = instruction.Element("code")?.Value ?? string.Empty;
                        var instructionValue = instruction.Element("text")?.Value ?? string.Empty;
                        // var instructionSelector = instruction.Element("selector")?.Value ?? string.Empty; // Unused currently
                        booking.Remarks.Add($"{instructionCode}:{instructionValue}");

                        if (instructionCode.Contains("TSP") && !instructionValue.IsNullOrWhiteSpace() &&
                            instructionValue != "NA")
                        {
                            booking.ExtraDelInformation = instructionValue;
                        }
                    }
                }
                catch (Exception e)
                {
                    RollingLogger.WriteToShredderProcessorLogs(
                        $"Execute Method - Exception occurred when extracting delivery instruction for Ikea, details:{e.Message}",
                        ELogTypes.Error);
                    _logger.LogError(e, "Failed to process booking for addCollection");
                }
            }

            try
            {
                DateTime.TryParse(node.Element("timeWindowDateFrom")?.Value, out DateTime slotFrom);
                DateTime.TryParse(node.Element("timeWindowDateTo")?.Value, out DateTime slotTo);
                booking.DespatchDateTime = slotFrom > DateTime.MinValue ? slotFrom : DateTime.Today;
                booking.AdvanceDateTime = booking.DespatchDateTime;

                booking.XCabTimeSlots = new XCabTimeSlots
                {
                    ClientRequiredPickupTime = slotFrom,
                    ClientRequiredDeliveryTime = slotTo,
                    StartDateTime = slotFrom
                };

                if (slotFrom > DateTime.MinValue && slotTo > DateTime.MinValue)
                    booking.XCabTimeSlots.Duration = (int)((slotTo - slotFrom).TotalMinutes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to process booking for addCollection");
            }

            if (addCollection is not null) // override with collection address
            {
                booking.FromSuburb = addSend?.Element("city")?.Value ?? string.Empty;
                booking.FromPostcode = addSend?.Element("postalCode")?.Value ?? string.Empty;
                booking.FromDetail1 = addSend?.Element("name")?.Value ?? string.Empty;
                booking.FromDetail2 = addSend?.Element("address1")?.Value ?? string.Empty;
                booking.FromDetail3 = addSend?.Element("address2")?.Value ?? string.Empty;
                booking.FromDetail4 = addSend?.Element("address3")?.Value ?? string.Empty;

                booking.ToSuburb = (string?)addCollection?.Element("city") ?? string.Empty;
                booking.ToPostcode = (string?)addCollection?.Element("postalCode") ?? string.Empty;
                booking.ToDetail1 = recieverName;
                booking.ToDetail2 = (string?)addCollection?.Element("address1") ?? string.Empty;
                booking.ToDetail3 = (string?)addCollection?.Element("address2") ?? string.Empty;
                booking.ToDetail4 = (string?)addCollection?.Element("address3") ?? string.Empty;

                booking.ServiceCode = "ICMF";
            }

            wrapper.Booking = booking;
            bookings.Add(wrapper);

            if (addCollection is not null)
            {
                var rtnBooking = new XCabBooking
                {
                    Remarks = booking.Remarks,
                    lstItems = booking.lstItems,
                    OkToUpload = false,

                    FromSuburb = addCollection?.Element("city")?.Value ?? string.Empty,
                    FromPostcode = addCollection?.Element("postalCode")?.Value ?? string.Empty,
                    FromDetail1 = addCollection?.Element("name")?.Value ?? string.Empty,
                    FromDetail2 = addCollection?.Element("address1")?.Value ?? string.Empty,
                    FromDetail3 = addCollection?.Element("address2")?.Value ?? string.Empty,
                    FromDetail4 = addCollection?.Element("address3")?.Value ?? string.Empty,

                    ToSuburb = (string?)addRec?.Element("city") ?? string.Empty,
                    ToPostcode = (string?)addRec?.Element("postalCode") ?? string.Empty,
                    ToDetail1 = recieverName,
                    ToDetail2 = (string?)addRec?.Element("address1") ?? string.Empty,
                    ToDetail3 = (string?)addRec?.Element("address2") ?? string.Empty,
                    ToDetail4 = (string?)addRec?.Element("address3") ?? string.Empty,

                    ExtraPuInformation = booking.ExtraPuInformation,
                    ExtraDelInformation = booking.ExtraDelInformation,
                    AdvanceDateTime = booking.AdvanceDateTime,
                    DespatchDateTime = booking.DespatchDateTime,
                    XCabTimeSlots = booking.XCabTimeSlots,
                    StateId = booking.StateId,
                    LoginId = booking.LoginId,
                    Caller = booking.Caller,
                    Ref1 = booking.Ref1,
                    Ref2 = booking.Ref2,
                    TotalVolume = booking.TotalVolume,
                    TotalWeight = booking.TotalWeight,
                    AccountCode = booking.AccountCode,

                    ServiceCode = "CPOD"
                };

                IkeaBookingWrapper rtnWrapper = new()
                {
                    ModifyType = wrapper.ModifyType,
                    Booking = rtnBooking,
                    Workflow = workflow
                };

                bookings.Add(rtnWrapper);
            }
        }

        return bookings;
    }
}