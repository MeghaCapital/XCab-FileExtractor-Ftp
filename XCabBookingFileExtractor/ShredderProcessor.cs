using Core;
using Core.Logging.SeriLog;
using Dapper;
using Data;
using Data.Entities;
using Data.Entities.ConsolidatedReferences;
using Data.Entities.EmailAlerts;
using Data.Entities.ExtraReferences;
using Data.Entities.GenericIntegration;
using Data.Model.Route;
using Data.Repository.EntityRepositories;
using Data.Repository.EntityRepositories.ExtraReferences;
using Data.Repository.EntityRepositories.FileInfo;
using Data.Utils;
using Ftp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XCabBookingFileExtractor.Capital_CSV_1_1;
using XCabBookingFileExtractor.CapitalSchemaV2;
using XCabBookingFileExtractor.GasMotors;
using XCabBookingFileExtractor.Officeworks;
using XCabBookingFileExtractor.Sigma;
using XCabBookingFileExtractor.Utils;
using XCabBookingFileExtractor.Utils.Consolidation;
using XCabService.ComoActiveStatesService;
using XCabService.EmailRecipientsService;
using XCabService.FileService;
using XCabService.IkeaService;
using XCabService.RequestValidationService;
using XCabService.SimpleEmailService;
using XCabService.SimpleFtpService;
using XCabService.XCabDatabaseService;

namespace XCabBookingFileExtractor
{
    [DisallowConcurrentExecution]
    public class ShredderProcessor : IccrProcess, IEmailAlert
    {
        private readonly IRequestValidationProvider _requestValidationProvider;
        private readonly INfsFileService _nfsFileService;
        private readonly IFileDownloader _fileDownloader;
        private readonly IComoActiveStatesProvider _comoActiveStatesProvider;
        private readonly IXCabDatabaseProvider? _xCabDatabaseProvider;
        private readonly IXCabEmailRecipientsServiceProvider _xCabEmailRecipientsProvider;
        private readonly IIkeaService _ikeaService;

        #region Public Members

        public class LoginState
        {
            public string AccountCode { get; set; }
            public string DefaultServiceCode { get; set; }
        }

        public class ClientServiceMapping
        {
            public string Id { get; set; }
            public string LoginId { get; set; }
            public string StateId { get; set; }
            public string ServiceName { get; set; }
            public string ServiceDescription { get; set; }
        }

        public class AccountCodeMapping
        {
            // cater for client Name Client codes
            public string Id { get; set; }
            public string LoginId { get; set; }
            public string StateId { get; set; }
            public string ClientName { get; set; }
            public string ClientMappingCode { get; set; }
        }

        public void Start()
        {
            if (VerboseLoggingEnabled)
                RollingLogger.WriteToShredderProcessorLogs($"Start Method - QuartzScheduler Started",
                    ELogTypes.Information);
        }

        public void Stop()
        {
            stopped = true;
            if (VerboseLoggingEnabled)
                RollingLogger.WriteToShredderProcessorLogs($"Start Method - QuartzScheduler Stopped",
                    ELogTypes.Information);
        }

        #endregion

        #region Initialization

        private LoginDetails testLoginDetails = null;
        private string testFilePath = "";
        private const string DatabaseInsertFailesMessageTitle = "XCab:File Receieved with Errors while Insertion";
        private string nfsUrl = DbSettings.Default.NfsUrlString;
        private string sourceFile;
        private readonly Core.Utils utils;
        private bool stopped;
        private bool VerboseLoggingEnabled = true;
        private bool isComoActiveState = false;


        public ShredderProcessor()
        {
            utils = new Core.Utils();
            stopped = false;
            _requestValidationProvider = new RequestValidationProvider();
            _nfsFileService = new NfsFileService(LogFactory.GetFileLogger<NfsFileService>());
            _fileDownloader = new FileDownloader(_nfsFileService);
            _comoActiveStatesProvider = new ComoActiveStatesProvider();
            _xCabDatabaseProvider = new XCabDatabaseProvider();
            _xCabEmailRecipientsProvider = new XCabEmailRecipientsServiceProvider();

            _ikeaService = new IkeaService(
                LogFactory.GetFileLogger<IkeaService>(),
                new Data.Repository.V2.XCabBookingRepository(),
                new ExtraReferencesRepository(),
                new SimpleFtpService(LogFactory.GetFileLogger<SimpleFtpService>()),
                new SimpleEmailService(LogFactory.GetFileLogger<SimpleEmailService>()),
                new SftpService()
            );
        }

        public ShredderProcessor(LoginDetails testLoginDetails, string testFilePath, ILogger<NfsFileService> logger)
        {
            utils = new Core.Utils();
            stopped = false;
            this.testLoginDetails = testLoginDetails;
            this.testFilePath = testFilePath;
            _requestValidationProvider = new RequestValidationProvider();
            _nfsFileService = new NfsFileService(logger);
            _fileDownloader = new FileDownloader(_nfsFileService);
            _comoActiveStatesProvider = new ComoActiveStatesProvider();
            _xCabDatabaseProvider = new XCabDatabaseProvider();
            _xCabEmailRecipientsProvider = new XCabEmailRecipientsServiceProvider();

            _ikeaService = new IkeaService(
                LogFactory.GetFileLogger<IkeaService>(),
                new Data.Repository.V2.XCabBookingRepository(),
                new ExtraReferencesRepository(),
                new SimpleFtpService(LogFactory.GetFileLogger<SimpleFtpService>()),
                new SimpleEmailService(LogFactory.GetFileLogger<SimpleEmailService>()),
                new SftpService()
            );
        }

        #endregion

        #region Utility Classes/Methods

        public class FtpLogin
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string FolderToMonitor { get; set; }
        }

        public static async Task<XmlDocument> RemoveXmlns(XmlDocument doc)
        {
            var xmlDocument = new XmlDocument();
            try
            {
                XDocument d;
                using (var nodeReader = new XmlNodeReader(doc))
                {
                    d = XDocument.Load(nodeReader);
                }

                d.Root.Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

                foreach (var elem in d.Descendants())
                {
                    elem.Name = elem.Name.LocalName;
                }

                using (var xmlReader = d.CreateReader())
                {
                    xmlDocument.Load(xmlReader);
                }
            }
            catch (Exception e)
            {
                await Logger.Log("Exception Occurred while Removing Toll Namespaces" + e.Message, "ShredderProcessor");
            }

            return xmlDocument;
        }

        public class XCabClientCustomSettings
        {
            public bool isRef3MappedToRef2 { get; set; }
            public bool isStagedOnlyBooking { get; set; }
        }

        private async Task<XCabClientCustomSettings> SetupClientSettingsForAsnBarcodeRef3(string LoginId,
            string userName, int stateId, string accountCode, string serviceCode)
        {
            XCabClientCustomSettings xCabClientCustomSettings = new XCabClientCustomSettings();
            xCabClientCustomSettings.isStagedOnlyBooking = false;
            xCabClientCustomSettings.isRef3MappedToRef2 = false;
            try
            {
                XCabClientSettingRepository xCabClientSettingRepository = new XCabClientSettingRepository();

#if DEBUG
                var loginId = !string.IsNullOrWhiteSpace(LoginId) ? Convert.ToInt32(LoginId) : 0;
#endif
#if !DEBUG
                var loginId = !string.IsNullOrWhiteSpace(LoginId) ? Convert.ToInt32(LoginId) : 0;
#endif

                if (VerboseLoggingEnabled)
                    RollingLogger.WriteToShredderProcessorLogs(
                        $"Execute Method - Retreiving client settings for account: {accountCode}, StateId: {stateId}, ServiceCode: {serviceCode}",
                        ELogTypes.Information);
                var xcabClientSetting =
                    xCabClientSettingRepository.GetXCabClientSetting(loginId, stateId, accountCode, serviceCode);
                if (VerboseLoggingEnabled)
                    RollingLogger.WriteToShredderProcessorLogs(
                        $"Execute Method - Retreived client settings for account: {accountCode}, StateId: {stateId}, ServiceCode: {serviceCode}",
                        ELogTypes.Information);

                if (xcabClientSetting != null)
                {
                    if (xcabClientSetting.StageBookingAPIJobs || xcabClientSetting.StageBookingOnServiceCodes)
                    {
                        xCabClientCustomSettings.isStagedOnlyBooking = true;
                    }

                    if (xcabClientSetting.MapRef3ToRef2)
                    {
                        xCabClientCustomSettings.isRef3MappedToRef2 = true;
                    }
                }
            }
            catch (Exception e)
            {
                if (VerboseLoggingEnabled)
                    RollingLogger.WriteToShredderProcessorLogs(
                        $"Execute Method - Exception occurred when extracting data from XCabClientSetting. Error : {e.Message}",
                        ELogTypes.Error);
                await Logger.Log("Exception occurred when extracting data from XCabClientSetting. Error : " + e.Message,
                    Name());
            }

            if (VerboseLoggingEnabled)
                RollingLogger.WriteToShredderProcessorLogs(
                    $"Execute Method - Resultant client settings. StagedBooking: {xCabClientCustomSettings.isStagedOnlyBooking.ToString()}, MapRef3ToRef2:{xCabClientCustomSettings.isRef3MappedToRef2}",
                    ELogTypes.Information);
            return xCabClientCustomSettings;
        }

        #endregion

        #region IccrProcess Members: Main method for Data Extraction

        public async Task Execute(IJobExecutionContext context)
        {
            if (VerboseLoggingEnabled)
                RollingLogger.WriteToShredderProcessorLogs(
                    $"Execute Method - Quartz Scheduler started with stopped flag set as {stopped}",
                    ELogTypes.Information);

            #region Download Ftp Files for Extraction

            var _dbConnector = new DbConnector();
            if (!stopped)
            {
                //connect to FTP and see if there are files that are needed to be downloaded
                //get a list of files that are available in the ftp folder
#if !DEBUG
                if (VerboseLoggingEnabled)
                    RollingLogger.WriteToShredderProcessorLogs($"Execute Method - Retrieving FTP Details from database",
                        ELogTypes.Information);
                var lstLoginDetail = _dbConnector.GetLoginDetails();
#else
                var lstLoginDetailsTmp = new List<LoginDetails>();
                lstLoginDetailsTmp.Add(testLoginDetails);

#if !DEBUG
                _fileDownloader.DownloadFtpCsvFiles(lstLoginDetailsTmp);
#endif
                var lstLoginDetail = lstLoginDetailsTmp;

#endif
                if (lstLoginDetail.Count > 0)
                {
                    if (VerboseLoggingEnabled)
                        RollingLogger.WriteToShredderProcessorLogs(
                            $"Execute Method - Downloading FTP Files to local folder", ELogTypes.Information);

                    //download files for the FTP users, setup user folders locally etc
#if !DEBUG
                    _fileDownloader.DownloadFtpCsvFiles(lstLoginDetail);
#endif
                    if (VerboseLoggingEnabled)
                        RollingLogger.WriteToShredderProcessorLogs(
                            $"Execute Method - Downloading FTP Files to local folder Completed", ELogTypes.Information);

                    //once the files have been downloaded we begin processing/parsing xml files
                    foreach (var login in lstLoginDetail)
                    {
                        try
                        {
                            //clear the list of booking
                            //lstBooking.Clear();
                            //get a list of files that are available in the ftp folder
                            var ftpUrl = DbSettings.Default.TyphonFtpConnectionString;
                            var ftpUserName = login.UserName;
                            var ftpPassword = login.Password;

                            var localBookingsFolder = DbSettings.Default.LocalDownloadFolder;
                            ICollection<string> nfsFiles;
                            string[] lstSourceFiles;
#if !DEBUG
                            if (login.TrackingSchemaName.ToUpper().Equals("CAPITAL-CSV"))
                            {
                                nfsFiles = _nfsFileService.GetFileListing(
                                 Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName, login.BookingsFolderName), ".csv");
                            }
                            else if (login.BookingSchemaName.ToUpper().Equals("CAPITAL-CSV-1.1"))
                            {
                                nfsFiles = _nfsFileService.GetFileListing(
                                 Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName, login.BookingsFolderName), ".csv");
                            }
                            else if (login.BookingSchemaName.ToUpper().Equals("OFFICEWORKS"))
                            {
                                nfsFiles = _nfsFileService.GetFileListing(
                            Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName, login.BookingsFolderName), ".txt");
                            }
                            else if (login.BookingSchemaName.ToUpper().Equals("SIGMA") || login.BookingSchemaName.ToUpper().Equals("IGNORE-BOOKINGS"))
                            {
                                nfsFiles = _nfsFileService.GetFileListing(
                            Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName, login.BookingsFolderName), "*");
                            }
                            else if (login.BookingSchemaName.ToUpper().Equals("IKEA"))
                            {
                                nfsFiles = _nfsFileService.GetFileListing(
                            Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName, login.BookingsFolderName), ".xml");
                            }
                            else
                            {
                                nfsFiles = _nfsFileService.GetFileListing(
                            Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName, login.BookingsFolderName), ".xml");
                            }

                            if (nfsFiles == null || !nfsFiles.Any())
                            {
                                RollingLogger.WriteToShredderProcessorLogs($"Execute Method - No files found in the FTP folder for user: {login.UserName}", ELogTypes.Information);
                                continue;
                            }
                            else
                            {
                                lstSourceFiles = nfsFiles.ToArray<string>();
                            }
#endif

#if DEBUG
                            lstSourceFiles = new string[] { testFilePath };
#endif
                            IDictionary<int, bool> comoActiveStatusForStates = null;
                            if (lstSourceFiles != null)
                            {
                                comoActiveStatusForStates = new Dictionary<int, bool>();
                            }
                            else
                            {
                                continue;
                            }

                            foreach (var _sourceFile in lstSourceFiles)
                            {
                                sourceFile = Path.Combine(
                                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                    _sourceFile);

                                #region Alert Zero Byte files

                                if (new FileInfo(sourceFile).Length == 0)
                                {
                                    //this is the case when we have received a file with 0 Bytes
                                    //we move it to the errors folder and also Slack it
                                    await Logger.Log(
                                        "File Exception: Received a file with no contents, Source File name:" +
                                        _sourceFile
                                        + ", Login Username:" + login.UserName
                                        +
                                        ". This file will be moved into the Errors folder and not processed again.",
                                        Name());

                                    try
                                    {
                                        //For EzyStrut the filename does not contain the state code and as a result we cannot dynamically send out emails to the respective state
                                        //Below is a temp scenario and should be removed once the FTP is stable for EZYSTRUT
                                        try
                                        {
                                            if (login.UserName.ToLower() == "ezystrut")
                                            {
                                                AlertZeroByteFile(
                                                    "Received file: " + _sourceFile +
                                                    " with no contents. This file will not be processed. Please liaise with IT Department/Client so that the booking can be sent again.",
                                                    "File Uploaded with No Contents", Convert.ToInt16(login.Id), -1,
                                                    true, _sourceFile);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            await Logger.Log(
                                                "Error sending email alert for 0 Byte file, exception:" + e.Message,
                                                Name());
                                        }

                                        //there has been an error - so we move to the Error Folder
                                        var path = "";
                                        if (!string.IsNullOrEmpty(login.BookingsFolderName))
                                        {
                                            path = Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName,
                                                login.BookingsFolderName, DbSettings.Default.ErrorFolderName);
                                        }
                                        else
                                        {
                                            path = Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName,
                                                DbSettings.Default.ErrorFolderName);
                                        }

                                        //check if the file exists
                                        if (File.Exists(sourceFile))
                                        {
                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        if (VerboseLoggingEnabled)
                                            RollingLogger.WriteToShredderProcessorLogs(
                                                $"Execute Method - Exception Occurred while moving the ftp files: {e.Message}",
                                                ELogTypes.Error);
                                        await Logger.Log("Exception Occurred while moving the ftp files:" + e.Message,
                                            Name());
                                    }

                                    continue;
                                }

                                #endregion

                                if (!comoActiveStatusForStates.Any())
                                {
                                    comoActiveStatusForStates =
                                        await _comoActiveStatesProvider.GetComoActiveStatusForStates();
                                }

                                #region Capital Default Schema V1 (Used By MyerOnl, general clients)

                                if ((login.TrackingSchemaName.ToUpper() == "CAPITAL") &&
                                    (login.BookingSchemaName.ToUpper() != "CAPITALV2"))
                                {
                                    XCabBookingFileExtractor.CapitalSchemaV1.Request request = null;
                                    var fileCleansed = false;
                                    var originalSourceFile = sourceFile;
                                    var successfullyInserted = true;
                                    try
                                    {
                                        request =
                                            XCabBookingFileExtractor.CapitalSchemaV1.Request.LoadFromFile(sourceFile);
                                        if (fileCleansed)
                                        {
                                            try
                                            {
                                                File.Delete(sourceFile);
                                                sourceFile = originalSourceFile;
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Error occurred while deleting the temporary file after cleansing. Temp File name= " +
                                                    sourceFile +
                                                    "ErrorMessage:" + e.Message, Name());
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        await Logger.Log(
                                            "Parsing Exception - Issue in Parsing File " + sourceFile +
                                            " from Local Temp Folder. It does not match with the Booking schema. This file will be moved into the Errors folder & processing of other files will continue as per normal. Please check this file as there may have been invalid data. ErrorMessage:" +
                                            e.Message, Name());
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        if (fileCleansed)
                                        {
                                            MoveAndCopyFileToErrorsFolder(login,
                                                sourceFile.Remove(sourceFile.IndexOf(".tmp") - 1));
                                        }

                                        continue;
                                    }

                                    if (request != null)
                                    {
                                        var stateId = utils.GetStateId(request.CapitalState.ToString());
                                        if (comoActiveStatusForStates != null && comoActiveStatusForStates.Count > 0)
                                        {
                                            isComoActiveState = comoActiveStatusForStates[stateId];
                                        }

                                        if (_requestValidationProvider.IsRequestValid(request.AccountCode,
                                                request.ServiceCode, request.FromDetail.locSuburb,
                                                request.FromDetail.locPostcode, request.ToLegs.Leg.ToDetail.locSuburb,
                                                request.ToLegs.Leg.ToDetail.locPostcode,
                                                request.ToLegs.Leg.ToDetail.locDetail1,
                                                request.FromDetail.locDetail1) == false)
                                        {
                                            successfullyInserted = false;
                                        }
                                        else
                                        {
                                            sourceFile =
                                                originalSourceFile; //change it back to the original source file
                                            //read through the file grabbing contents as needed
                                            var booking = new Booking();

                                            //check if the Account Code or Service Codes are empty
                                            if (string.IsNullOrEmpty(request.AccountCode))
                                            {
                                                await Logger.Log(
                                                    "Error:Received an empty Account Code. Will try using default AccountCode setup in the system. File=" +
                                                    sourceFile, Name());
                                                if ((login.lstAccountCodes != null) &&
                                                    (login.lstAccountCodes.Count > 0))
                                                {
                                                    booking.AccountCode = login.lstAccountCodes[0];
                                                }
                                                else
                                                {
                                                    await Logger.Log(
                                                        "Error:Cannot use any predefined Account Code. This file will not be processed untill the Account Codes are sent with the file. File=" +
                                                        sourceFile, Name());
                                                    MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                booking.AccountCode = request.AccountCode;
                                            }

                                            //check if the Account Code or Service Codes are empty
                                            if (string.IsNullOrEmpty(request.ServiceCode))
                                            {
                                                await Logger.Log(
                                                    "Error:Received an empty Service Code. Will try using default Service setup in the system. File=" +
                                                    sourceFile, Name());
                                                if ((login.lstAccountCodes != null) &&
                                                    (login.lstServiceCodes.Count > 0))
                                                {
                                                    //booking.AccountCode = login.lstAccountCodes[0];
                                                    booking.ServiceCode = login.lstServiceCodes[0];
                                                }
                                                else
                                                {
                                                    await Logger.Log(
                                                        "Error:Cannot use any predefined Service Code. This file will not be processed untill the Service Codes are sent with the file. File=" +
                                                        sourceFile, Name());
                                                    MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                booking.ServiceCode = request.ServiceCode;
                                            }
                                            //booking.DespatchDateTime = DateTime.Now;

                                            var ld = new LoginDetails { Id = login.Id };
                                            booking.LoginDetails = ld;


                                            if (stateId != -1)
                                            {
                                                booking.StateId = stateId.ToString();
                                            }

                                            //for all CPOD bookings for NHP we need to change the state for al linehaul bookings
                                            if (!string.IsNullOrEmpty(request.ServiceCode) &&
                                                request.ServiceCode == "CPOD")
                                            {
                                                if (!string.IsNullOrEmpty(request.Runcode))
                                                {
                                                    if (booking.AccountCode == "NHPWA")
                                                    {
                                                        stateId = 5;
                                                    }

                                                    if (booking.AccountCode == "NHP")
                                                    {
                                                        stateId = 2;
                                                    }

                                                    if (booking.AccountCode == "3NHPQLD")
                                                    {
                                                        stateId = 3;
                                                    }

                                                    if (booking.AccountCode == "NHPSA")
                                                    {
                                                        stateId = 4;
                                                    }

                                                    if (booking.AccountCode == "3NHPNT")
                                                    {
                                                        stateId = 3;
                                                    }
                                                }
                                            }

                                            var xCabClientCustomSettings = new XCabClientCustomSettings();
                                            try
                                            {
                                                //setup xcab client custom settings
                                                if (!string.IsNullOrEmpty(login.Id) &&
                                                    !string.IsNullOrEmpty(login.UserName) &&
                                                    !string.IsNullOrEmpty(request.AccountCode) &&
                                                    !string.IsNullOrEmpty(request.ServiceCode) && stateId > 0)
                                                    xCabClientCustomSettings =
                                                        await SetupClientSettingsForAsnBarcodeRef3(login.Id,
                                                            login.UserName, stateId, request.AccountCode,
                                                            request.ServiceCode);
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Error occurred while retrieving client custom settings for ASN," +
                                                    "ErrorMessage:" + e.Message, Name());
                                            }

                                            booking.FromDetail1 = request.FromDetail.locDetail1;
                                            booking.FromDetail2 = request.FromDetail.locDetail2;
                                            booking.FromDetail3 = request.FromDetail.locDetail3;
                                            booking.FromDetail4 = request.FromDetail.locDetail4;
                                            booking.FromDetail5 = request.FromDetail.locDetail5;
                                            booking.FromPostcode = request.FromDetail.locPostcode;
                                            booking.FromSuburb = request.FromDetail.locSuburb;
                                            booking.ToDetail1 = request.ToLegs.Leg.ToDetail.locDetail1;
                                            booking.ToDetail2 = request.ToLegs.Leg.ToDetail.locDetail2;
                                            booking.ToDetail3 = request.ToLegs.Leg.ToDetail.locDetail3;
                                            booking.ToDetail4 = request.ToLegs.Leg.ToDetail.locDetail4;
                                            booking.ToDetail5 = request.ToLegs.Leg.ToDetail.locDetail5;
                                            booking.ToSuburb = request.ToLegs.Leg.ToDetail.locSuburb;
                                            booking.ToPostcode = request.ToLegs.Leg.ToDetail.locPostcode;
                                            booking.TrackAndTraceSmsNumber = request.ToLegs.Leg.TrackAndTraceSmsNumber;
                                            booking.TrackAndTraceEmailAddress =
                                                request.ToLegs.Leg.TrackAndTraceEmailAddress;
                                            try
                                            {
                                                if (!string.IsNullOrEmpty(request.ToLegs.Leg.Atl))
                                                {
                                                    booking.ATL = Boolean.Parse(request.ToLegs.Leg.Atl.Trim());
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception Occurred while extracting ATL information in CapitalSchemaV1, exception message:" +
                                                    e.Message, Name());
                                            }

                                            if (!string.IsNullOrEmpty(request.ToLegs.Leg.TrackAndTraceSmsNumber))
                                            {
                                                booking.TrackAndTraceSmsNumber =
                                                    request.ToLegs.Leg.TrackAndTraceSmsNumber;
                                            }

                                            if (!string.IsNullOrEmpty(request.ToLegs.Leg.TrackAndTraceEmailAddress))
                                            {
                                                booking.TrackAndTraceEmailAddress =
                                                    request.ToLegs.Leg.TrackAndTraceEmailAddress;
                                            }

                                            var okToUpload = true;

                                            if (xCabClientCustomSettings.isStagedOnlyBooking)
                                            {
                                                okToUpload = false;
                                                booking.OkToUpload = false;
                                            }

                                            try
                                            {
                                                if (!string.IsNullOrEmpty(request.ServiceCode) &&
                                                    request.ServiceCode == "CPOD")
                                                {
                                                    if (!string.IsNullOrEmpty(request.Runcode))
                                                    {
                                                        if (xCabClientCustomSettings.isStagedOnlyBooking == false)
                                                        {
                                                            var xcabDriverRouteRepository =
                                                                new XCabDriverRouteRepository();
                                                            var xcabDriverRoutes =
                                                                xcabDriverRouteRepository
                                                                    .GetXCabDriverRoutesForRouteName(
                                                                        request.Runcode.ToUpper(), login.Id, stateId,
                                                                        request.AccountCode);

                                                            if (xcabDriverRoutes != null &&
                                                                !string.IsNullOrEmpty(xcabDriverRoutes.DriverNumber))
                                                            {
                                                                int.TryParse(xcabDriverRoutes.DriverNumber,
                                                                    out int driver);
                                                                booking.DriverNumber = driver;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception Occurred while extracting Driver information linked to a Route, exception message:" +
                                                    e.Message, Name());
                                            }

                                            if (!string.IsNullOrEmpty(request.SpecialInstructions))
                                            {
                                                booking.ExtraPuInformation = request.SpecialInstructions;
                                                booking.ExtraDelInformation = request.SpecialInstructions;
                                            }

                                            if (request.ToLegs.Leg.Ref1 != null)
                                            {
                                                booking.Ref1 = request.ToLegs.Leg.Ref1;
                                            }
                                            else
                                            {
                                                booking.Ref1 = string.Empty;
                                            }

                                            try
                                            {
                                                if ((request.ToLegs.Leg.Ref3 != null) &&
                                                    xCabClientCustomSettings.isRef3MappedToRef2)
                                                {
                                                    booking.Ref2 = request.ToLegs.Leg.Ref3;
                                                    request.ToLegs.Leg.Ref3 = null;
                                                }

                                                if ((request.ToLegs.Leg.Ref2 != null) &&
                                                    xCabClientCustomSettings.isRef3MappedToRef2)
                                                {
                                                    request.ToLegs.Leg.Ref3 = request.ToLegs.Leg.Ref2;
                                                }
                                                else if ((request.ToLegs.Leg.Ref2 != null) &&
                                                         !xCabClientCustomSettings.isRef3MappedToRef2)
                                                {
                                                    booking.Ref2 = request.ToLegs.Leg.Ref2;
                                                }
                                                else if (!xCabClientCustomSettings.isRef3MappedToRef2)
                                                {
                                                    booking.Ref2 = string.Empty;
                                                }

                                                if (request.ToLegs.Leg.Ref3 != null)
                                                {
                                                    try
                                                    {
                                                        var extraField = new ExtraFields
                                                        {
                                                            Key = "Ref3",
                                                            Value = request.ToLegs.Leg.Ref3
                                                        };

                                                        if (booking.lstExtraFields == null)
                                                        {
                                                            var extraFields = new List<ExtraFields>
                                                            {
                                                                extraField
                                                            };
                                                            booking.lstExtraFields = extraFields;
                                                        }
                                                        else
                                                        {
                                                            booking.lstExtraFields.Add(extraField);
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        await Logger.Log(
                                                            "Exception Occurred when mapping extra references in capital default schema. Message: " +
                                                            e.Message, Name());
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception Occurred while mapping Ref2 and Ref3 in CAPITAL default schema. Message: " +
                                                    e.Message, Name());
                                            }

                                            try
                                            {
                                                if (login.UserName.ToLower() == "briggatemedical")
                                                {
                                                    var nextWorkingDayForYesterday =
                                                        new CalculateDates().GetNextWorkingDayInclusiveSaturday(
                                                            DateTime.Now.AddDays(-1), 1,
                                                            request.CapitalState.ToString(), false);
                                                    if (nextWorkingDayForYesterday.Date != DateTime.Now.Date)
                                                    {
                                                        request.Planned =
                                                            DateTime.Parse(
                                                                nextWorkingDayForYesterday.ToString("yyyy/MM/dd ") +
                                                                "08:30 AM");
                                                    }
                                                    else if (DateTime.Now <=
                                                             DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd ") +
                                                                            "10:00 AM")) /// bookings are accepted till 10AM for tha same day
                                                    {
                                                        request.Planned =
                                                            DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd ") +
                                                                           "08:30 AM");
                                                    }
                                                    else
                                                    {
                                                        request.Planned =
                                                            DateTime.Parse(new CalculateDates()
                                                                .GetNextWorkingDay(DateTime.Now, 1,
                                                                    request.CapitalState.ToString())
                                                                .ToString("yyyy/MM/dd ") + "08:30 AM");
                                                    }

                                                    request.PlannedSpecified = true;
                                                }

                                                if (request.PlannedSpecified)
                                                {
                                                    booking.DespatchDateTime = request.Planned;
                                                    //we realize that TPLUS does not put adv date time on line 5, so we force it here:
                                                    booking.FromDetail5 = "Earliest PU " +
                                                                          booking.DespatchDateTime.ToString(
                                                                              "dd MMM HH:mm");
                                                }
                                                else if (request.EarliestSpecified)
                                                {
                                                    booking.DespatchDateTime = request.Earliest;
                                                    //we realize that TPLUS does not put adv date time on line 5, so we force it here:
                                                    booking.FromDetail5 = "Earliest PU " +
                                                                          booking.DespatchDateTime.ToString(
                                                                              "dd MMM HH:mm");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception Occurred while extracting Despatch Date time from Planned/Earliest Xml field, exception message:" +
                                                    e.Message, Name());
                                            }

                                            try
                                            {
                                                if ((request.ToLegs.Leg.Items != null) &&
                                                    (request.ToLegs.Leg.Items.Count != 0))
                                                {
                                                    var lstItems = request.ToLegs.Leg.Items;
                                                    var lstBarcodes = new List<Item>();
                                                    foreach (var item in lstItems)
                                                    {
                                                        var _item = new Item { Description = item.itmDescription };
                                                        if (item.itmLengthSpecified)
                                                        {
                                                            _item.Length = (double)item.itmLength;
                                                        }

                                                        if (item.itmWidthSpecified)
                                                        {
                                                            _item.Width = (double)item.itmWidth;
                                                        }

                                                        if (item.itmHeightSpecified)
                                                        {
                                                            _item.Height = (double)item.itmHeight;
                                                        }

                                                        if (item.itmWeightSpecified)
                                                        {
                                                            _item.Weight = (double)item.itmWeight;
                                                        }

                                                        if (item.itmCubicSpecified)
                                                        {
                                                            _item.Cubic = (double)item.itmCubic;
                                                        }

                                                        _item.Barcode = item.itmBarcode;
                                                        _item.Quantity = 1;
                                                        lstBarcodes.Add(_item);
                                                    }

                                                    //assign the list of items back to the bookings
                                                    booking.lstItems = lstBarcodes;
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log("Exception Occurred in parsing items:" + e.Message,
                                                    Name());
                                            }

                                            try
                                            {
                                                if ((request.ToLegs.Leg.PhoneNumbers != null) &&
                                                    (request.ToLegs.Leg.PhoneNumbers.Count != 0))
                                                {
                                                    var lstPhoneNumber = request.ToLegs.Leg.PhoneNumbers;
                                                    var lstContactDetail = lstPhoneNumber.Select(pnt =>
                                                        new ContactDetail
                                                        {
                                                            AreaCode = pnt.AreaCode,
                                                            PhoneNumber = pnt.PhoneNumber
                                                        }).ToList();
                                                    //assign the list of contact details back to the booking structure
                                                    booking.lstContactDetail = lstContactDetail;
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception Occurred in parsing phone number:" + e.Message, Name());
                                            }

                                            try
                                            {
                                                //only update the adv date time and Pu location for jobs that are sent on a linehaul from VIC
                                                if (booking.AccountCode == "NHPWA")
                                                {
                                                    if (booking.StateId == "1")
                                                    {
                                                        //override the PU details
                                                        var nhpBooking = Core.AddressMapping.NhpAddressMapping
                                                            .GetNhpPickupAddress(EStates.WA);
                                                        booking.FromDetail1 = nhpBooking.FromDetail1;
                                                        booking.FromDetail2 = nhpBooking.FromDetail2;
                                                        booking.FromDetail3 = nhpBooking.FromDetail3;
                                                        booking.FromSuburb = nhpBooking.FromSuburb;
                                                        booking.FromPostcode = nhpBooking.FromPostcode;
                                                        booking.StateId = Core.Helpers.StateHelpers
                                                            .GetStateId(EStates.WA.ToString()).ToString();

                                                        var addDays = 0;
                                                        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                                                        {
                                                            addDays = 3;
                                                        }
                                                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                                                        {
                                                            addDays = 3;
                                                        }
                                                        //for anything that is manifested on Wed - Fri put it all for Monday
                                                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                                                        {
                                                            addDays = 5;
                                                        }
                                                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                                                        {
                                                            addDays = 4;
                                                        }
                                                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                        {
                                                            addDays = 3;
                                                        }
                                                        else
                                                        {
                                                            addDays = 0;
                                                        }

                                                        var dt = DateTime.Now.AddDays(addDays);
                                                        booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                            dt.Day, 6, 0, 0);
                                                        booking.AdvanceDateTime = booking.DespatchDateTime;
                                                    }
                                                    else
                                                    {
                                                        //this is the local freight
                                                        //we need to make bookings as advance by a day of they are received after 2 PM WA Time
                                                        //otherwise they go as ready now
                                                        if (DateTime.Now.TimeOfDay >=
                                                            new TimeSpan(16, 0,
                                                                0)) //2 PM is 4 PM MEL Time (without DST)
                                                        {
                                                            var addDays = 1;
                                                            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                            {
                                                                addDays = 3;
                                                            }

                                                            var dt = DateTime.Now.AddDays(addDays);
                                                            booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                                dt.Day, 6, 0, 0);
                                                            booking.AdvanceDateTime = booking.DespatchDateTime;
                                                        }
                                                    }
                                                }

                                                //only update the adv date time and Pu location for jobs that are sent on a linehaul from VIC
                                                if (booking.AccountCode == "NHP")
                                                {
                                                    //override the PU details
                                                    var nhpBooking =
                                                        Core.AddressMapping.NhpAddressMapping.GetNhpPickupAddress(
                                                            EStates.NSW);
                                                    booking.FromDetail1 = nhpBooking.FromDetail1;
                                                    booking.FromDetail2 = nhpBooking.FromDetail2;
                                                    booking.FromDetail3 = nhpBooking.FromDetail3;
                                                    booking.FromSuburb = nhpBooking.FromSuburb;
                                                    booking.FromPostcode = nhpBooking.FromPostcode;
                                                    booking.StateId = Core.Helpers.StateHelpers
                                                        .GetStateId(EStates.NSW.ToString()).ToString();

                                                    var CalculateDates = new Data.Utils.CalculateDates();
                                                    var despatchDate =
                                                        CalculateDates.GetNextWorkingDay(DateTime.Now, 1, "NSW");
                                                    if (despatchDate == DateTime.MinValue)
                                                    {
                                                        var addDays = 0;
                                                        if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                        {
                                                            addDays = 3;
                                                        }

                                                        else
                                                        {
                                                            addDays = 1;
                                                        }

                                                        var dt = DateTime.Now.AddDays(addDays);
                                                        booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                            dt.Day, 6, 0, 0);
                                                    }
                                                    else
                                                    {
                                                        booking.DespatchDateTime = new DateTime(despatchDate.Year,
                                                            despatchDate.Month, despatchDate.Day, 6, 0, 0);
                                                    }

                                                    booking.AdvanceDateTime = booking.DespatchDateTime;
                                                }

                                                //only update the adv date time and Pu location for jobs that are sent on a linehaul from VIC
                                                if (booking.AccountCode == "3NHPQLD")
                                                {
                                                    if (booking.StateId == "1")
                                                    {
                                                        //override the PU details - old address
                                                        var nhpBooking =
                                                            Core.AddressMapping.NhpAddressMapping.GetNhpPickupAddress(
                                                                EStates.QLD);
                                                        booking.FromDetail1 = nhpBooking.FromDetail1;
                                                        booking.FromDetail2 = nhpBooking.FromDetail2;
                                                        booking.FromDetail3 = nhpBooking.FromDetail3;
                                                        booking.FromSuburb = nhpBooking.FromSuburb;
                                                        booking.FromPostcode = nhpBooking.FromPostcode;
                                                        booking.StateId = Core.Helpers.StateHelpers
                                                            .GetStateId(EStates.QLD.ToString()).ToString();

                                                        var CalculateDates = new Data.Utils.CalculateDates();
                                                        var despatchDate =
                                                            CalculateDates.GetNextWorkingDay(DateTime.Now, 2, "QLD");
                                                        if (despatchDate == DateTime.MinValue)
                                                        {
                                                            var addDays = 0;
                                                            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                                                            {
                                                                addDays = 2;
                                                            }
                                                            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                                                            {
                                                                addDays = 2;
                                                            }
                                                            //for anything that is manifested on Wed - Fri put it all for Monday
                                                            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                                                            {
                                                                addDays = 2;
                                                            }
                                                            else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                                                            {
                                                                addDays = 4;
                                                            }
                                                            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                            {
                                                                addDays = 4;
                                                            }
                                                            else
                                                            {
                                                                addDays = 0;
                                                            }

                                                            var dt = DateTime.Now.AddDays(addDays);
                                                            booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                                dt.Day, 6, 0, 0);
                                                        }
                                                        else
                                                        {
                                                            booking.DespatchDateTime = new DateTime(despatchDate.Year,
                                                                despatchDate.Month, despatchDate.Day, 6, 0, 0);
                                                        }

                                                        booking.AdvanceDateTime = booking.DespatchDateTime;
                                                    }
                                                    else
                                                    {
                                                        //this is the local freight and the bookings will need to be advanced by a day

                                                        var CalculateDates = new Data.Utils.CalculateDates();
                                                        var despatchDate =
                                                            CalculateDates.GetNextWorkingDay(DateTime.Now, 1, "QLD");
                                                        if (despatchDate == DateTime.MinValue)
                                                        {
                                                            var addDays = 1;
                                                            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                            {
                                                                addDays = 3;
                                                            }

                                                            var dt = DateTime.Now.AddDays(addDays);
                                                            booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                                dt.Day, 6, 0, 0);
                                                        }
                                                        else
                                                        {
                                                            booking.DespatchDateTime = new DateTime(despatchDate.Year,
                                                                despatchDate.Month, despatchDate.Day, 6, 0, 0);
                                                        }


                                                        booking.AdvanceDateTime = booking.DespatchDateTime;
                                                    }
                                                }

                                                if (booking.AccountCode == "NHPSA")
                                                {
                                                    if (booking.StateId == "1")
                                                    {
                                                        //override the PU details
                                                        var nhpBooking = Core.AddressMapping.NhpAddressMapping
                                                            .GetNhpPickupAddress(EStates.SA);
                                                        booking.FromDetail1 = nhpBooking.FromDetail1;
                                                        booking.FromDetail2 = nhpBooking.FromDetail2;
                                                        booking.FromDetail3 = nhpBooking.FromDetail3;
                                                        booking.FromSuburb = nhpBooking.FromSuburb;
                                                        booking.FromPostcode = nhpBooking.FromPostcode;
                                                        booking.StateId = Core.Helpers.StateHelpers
                                                            .GetStateId(EStates.SA.ToString()).ToString();

                                                        var CalculateDates = new Data.Utils.CalculateDates();
                                                        var despatchDate =
                                                            CalculateDates.GetNextWorkingDay(DateTime.Now, 1, "SA");
                                                        if (despatchDate == DateTime.MinValue)
                                                        {
                                                            var addDays = 0;
                                                            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                            {
                                                                addDays = 3;
                                                            }

                                                            else
                                                            {
                                                                addDays = 1;
                                                            }

                                                            var dt = DateTime.Now.AddDays(addDays);
                                                            booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                                dt.Day, 6, 0, 0);
                                                        }
                                                        else
                                                        {
                                                            booking.DespatchDateTime = new DateTime(despatchDate.Year,
                                                                despatchDate.Month, despatchDate.Day, 6, 0, 0);
                                                        }

                                                        booking.AdvanceDateTime = booking.DespatchDateTime;
                                                    }
                                                    else
                                                    {
                                                        //this is the local freight and the bookings will need to be advanced by a day
                                                        //if (DateTime.Now.TimeOfDay >= new TimeSpan(16, 0, 0)) //2 PM is 4 PM MEL Time (without DST)
                                                        // {
                                                        var CalculateDates = new Data.Utils.CalculateDates();
                                                        var despatchDate =
                                                            CalculateDates.GetNextWorkingDay(DateTime.Now, 1, "SA");
                                                        if (despatchDate == DateTime.MinValue)
                                                        {
                                                            var addDays = 1;
                                                            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                            {
                                                                addDays = 3;
                                                            }

                                                            var dt = DateTime.Now.AddDays(addDays);
                                                            booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                                dt.Day, 6, 0, 0);
                                                        }
                                                        else
                                                        {
                                                            booking.DespatchDateTime = new DateTime(despatchDate.Year,
                                                                despatchDate.Month, despatchDate.Day, 6, 0, 0);
                                                        }

                                                        booking.AdvanceDateTime = booking.DespatchDateTime;
                                                        //  }
                                                    }
                                                }

                                                if (booking.AccountCode == "3NHPNT")
                                                {
                                                    if (booking.StateId == "1")
                                                    {
                                                        //override the PU details
                                                        var nhpBooking = Core.AddressMapping.NhpAddressMapping
                                                            .GetNhpPickupAddress(EStates.NT);
                                                        booking.FromDetail1 = nhpBooking.FromDetail1;
                                                        booking.FromDetail2 = nhpBooking.FromDetail2;
                                                        booking.FromDetail3 = nhpBooking.FromDetail3;
                                                        booking.FromSuburb = nhpBooking.FromSuburb;
                                                        booking.FromPostcode = nhpBooking.FromPostcode;
                                                        booking.StateId = Core.Helpers.StateHelpers
                                                            .GetStateId(EStates.QLD.ToString()).ToString();

                                                        /**
                                                         * Despatch Melbourne	Arrival Darwin	Advise Shipping Days
                                                            Monday	Monday	5
                                                            Tuesday	Tuesday	5
                                                            Wednesday	Wednesday	5
                                                            Thursday	Thursday	5
                                                            Friday	Friday	5

                                                         **/

                                                        var addDays = 7;

                                                        var dt = DateTime.Now.AddDays(addDays);
                                                        booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                            dt.Day, 6, 0, 0);

                                                        booking.AdvanceDateTime = booking.DespatchDateTime;
                                                    }
                                                    else
                                                    {
                                                        //this is the local freight and the bookings will need to be advanced by a day                                            
                                                        //make the stateid the same as QLD
                                                        booking.StateId = "3";
                                                        var addDays = 1;
                                                        if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                        {
                                                            addDays = 3;
                                                        }

                                                        var dt = DateTime.Now.AddDays(addDays);
                                                        booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                            dt.Day, 6, 0, 0);
                                                        booking.AdvanceDateTime = booking.DespatchDateTime;
                                                    }
                                                }

                                                if (booking.AccountCode == "3NHPELEC")
                                                {
                                                    if ((booking.ServiceCode.ToUpper() == "CPOD") ||
                                                        (booking.ServiceCode.ToUpper() == "CPOR"))
                                                    {
                                                        if (booking.StateId != "1")
                                                        {
                                                            switch (booking.StateId)
                                                            {
                                                                case "2":
                                                                    var nswBooking =
                                                                        Core.AddressMapping.NhpAddressMapping
                                                                            .GetNhpDeliveryAddress(EStates.NSW);
                                                                    booking.AccountCode = nswBooking.AccountCode;
                                                                    booking.ToDetail1 = nswBooking.ToDetail1;
                                                                    booking.ToDetail2 = nswBooking.ToDetail2;
                                                                    booking.ToSuburb = nswBooking.ToSuburb;
                                                                    booking.ToPostcode = nswBooking.ToPostcode;
                                                                    break;
                                                                case "3":
                                                                    var qldBooking =
                                                                        Core.AddressMapping.NhpAddressMapping
                                                                            .GetNhpDeliveryAddress(EStates.QLD);
                                                                    booking.AccountCode = qldBooking.AccountCode;
                                                                    booking.ToDetail1 = qldBooking.ToDetail1;
                                                                    booking.ToDetail2 = qldBooking.ToDetail2;
                                                                    booking.ToSuburb = qldBooking.ToSuburb;
                                                                    booking.ToPostcode = qldBooking.ToPostcode;
                                                                    break;
                                                                case "4":
                                                                    var saBooking =
                                                                        Core.AddressMapping.NhpAddressMapping
                                                                            .GetNhpDeliveryAddress(EStates.SA);
                                                                    booking.AccountCode = saBooking.AccountCode;
                                                                    booking.ToDetail1 = saBooking.ToDetail1;
                                                                    booking.ToDetail2 = saBooking.ToDetail2;
                                                                    booking.ToSuburb = saBooking.ToSuburb;
                                                                    booking.ToPostcode = saBooking.ToPostcode;
                                                                    break;
                                                                case "5":
                                                                    var waBooking =
                                                                        Core.AddressMapping.NhpAddressMapping
                                                                            .GetNhpDeliveryAddress(EStates.WA);
                                                                    booking.AccountCode = waBooking.AccountCode;
                                                                    booking.ToDetail1 = waBooking.ToDetail1;
                                                                    booking.ToDetail2 = waBooking.ToDetail2;
                                                                    booking.ToSuburb = waBooking.ToSuburb;
                                                                    booking.ToPostcode = waBooking.ToPostcode;
                                                                    break;
                                                            }
                                                        }

                                                        var CalculateDates = new Data.Utils.CalculateDates();
                                                        var despatchDate =
                                                            CalculateDates.GetNextWorkingDay(DateTime.Now, 1, "VIC");
                                                        if (despatchDate == DateTime.MinValue)
                                                        {
                                                            var addDays = 0;
                                                            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                                                            {
                                                                addDays = 3;
                                                            }

                                                            else
                                                            {
                                                                addDays = 1;
                                                            }

                                                            var dt = DateTime.Now.AddDays(addDays);
                                                            booking.DespatchDateTime = new DateTime(dt.Year, dt.Month,
                                                                dt.Day, 6, 0, 0);
                                                        }
                                                        else
                                                        {
                                                            booking.DespatchDateTime = new DateTime(despatchDate.Year,
                                                                despatchDate.Month, despatchDate.Day, 6, 0, 0);
                                                        }

                                                        booking.AdvanceDateTime = booking.DespatchDateTime;
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "3NHPNT Exception Occurred while remapping details, exception:" +
                                                    e.Message, Name());
                                            }

                                            try
                                            {
                                                if (booking.AccountCode.ToUpper().Trim() == "MYFBBQGQ")
                                                {
                                                    if (booking.StateId == "3" && booking.FromDetail2.ToLower()
                                                                                   .Trim() == "15 curlew st"
                                                                               && !string.IsNullOrWhiteSpace(
                                                                                   booking.FromPostcode) &&
                                                                               booking.FromPostcode.Trim() == "4178")
                                                    {
                                                        //override the PU details
                                                        booking.FromDetail1 = "MOXY LOGISTICS";
                                                        booking.FromDetail2 = "48 PARADISE RD";
                                                        booking.FromDetail3 = "";
                                                        booking.FromSuburb = "ACACIA RIDGE";
                                                        booking.FromPostcode = "4110";
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "MYFBBQGQ Exception Occurred while remapping address details, exception:" +
                                                    e.Message, Name());
                                            }

                                            if (!string.IsNullOrEmpty(request.Caller))
                                            {
                                                booking.Caller = request.Caller.Trim();
                                            }
                                            else if (!string.IsNullOrWhiteSpace(request.Runcode))
                                            {
                                                //this is mostly required for all NHP bookings however we should do it for all bookings where the runcode is used
                                                booking.Caller = request.Runcode.Trim();
                                            }

                                            //send the list of bookings to the Data Layer                                    
                                            try
                                            {
                                                if (booking != null)
                                                {
                                                    booking.UsingComo = isComoActiveState;
                                                    var dbConnector = new DbConnector();
                                                    if (booking.AccountCode == "3NHPELEC" ||
                                                        booking.AccountCode == "NHPWA" ||
                                                        booking.AccountCode == "NHP" ||
                                                        booking.AccountCode == "3NHPQLD" ||
                                                        booking.AccountCode == "NHPSA" ||
                                                        booking.AccountCode == "3NHPNT")
                                                    {
                                                        if (booking.DriverNumber > 0)
                                                            booking.IsQueued = true;
                                                        successfullyInserted =
                                                            dbConnector.StoreToDbTempForNHP(booking, okToUpload);
                                                    }
                                                    else
                                                    {
                                                        successfullyInserted =
                                                            dbConnector.StoreToDb(booking, okToUpload);
                                                    }

                                                    if (VerboseLoggingEnabled)
                                                        RollingLogger.WriteToShredderProcessorLogs(
                                                            $"Execute Method - Insert booking for {login.BookingSchemaName} schema to XCab DB. Successfull = {successfullyInserted}. File: {sourceFile}",
                                                            ELogTypes.Information);
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception Occurred while writing to the db:" + e.Message, Name());
                                                successfullyInserted = false;
                                            }
                                        }
                                    }

                                    //once this file has been processed move it to the processed folder
                                    //string destinationFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), processedFolderName);
                                    if (successfullyInserted)
                                    {
                                        var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                            login.UserName,
                                            login.BookingsFolderName, DbSettings.Default.ProcessedFolderName);
                                        var destinationFile = Path.Combine(_path,
                                            _sourceFile.Substring(_sourceFile.LastIndexOf('\\') + 1));

                                        //also move the file across the ftp folder
                                        MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                    }
                                    else
                                    {
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                    }
                                }

                                #endregion

                                #region Capital V2 Schema Used by FreightConcepts

                                else if (login.BookingSchemaName.ToUpper() == "CAPITALV2")
                                {
                                    try
                                    {
                                        Requests requests = null;

                                        //XMlShredder.CapitalSchema.v2.Request request = null;
                                        var fileCleansed = false;
                                        var originalSourceFile = sourceFile;
                                        var fulcrumRuns = new List<string>() { "FLCRME", "FLCRMN", "FLCRMS", "FLCRMW" };

                                        try
                                        {
                                            //load the requests 
                                            requests = Requests.LoadFromFile(sourceFile);
                                        }
                                        catch (Exception e)
                                        {
                                            await Logger.Log(
                                                "Parsing Exception - Issue in Parsing File " + sourceFile +
                                                " from Local Temp Folder. It does not match with the Booking schema. This file will be moved into the Errors folder & processing of other files will continue as per normal. Please check this file as there may have been invalid data. ErrorMessage:" +
                                                e.Message, Name());
                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                            if (fileCleansed)
                                            {
                                                MoveAndCopyFileToErrorsFolder(login,
                                                    sourceFile.Remove(sourceFile.IndexOf(".tmp") - 1));
                                            }

                                            continue;
                                        }

                                        sourceFile = originalSourceFile;
                                        var allRecordsInsertedSucessfully = true;
                                        try
                                        {
                                            //now iterate through all the request tags
                                            foreach (var request in requests.Request)
                                            {
                                                if (request != null)
                                                {
                                                    var stateId = utils.GetStateId(request.CapitalState.ToString());
                                                    if (comoActiveStatusForStates != null &&
                                                        comoActiveStatusForStates.Count > 0)
                                                    {
                                                        isComoActiveState = comoActiveStatusForStates[stateId];
                                                    }

                                                    if (_requestValidationProvider.IsRequestValid(request.AccountCode,
                                                            request.ServiceCode, request.FromDetail.locSuburb,
                                                            request.FromDetail.locPostcode,
                                                            request.ToLegs.Leg.ToDetail.locSuburb,
                                                            request.ToLegs.Leg.ToDetail.locPostcode,
                                                            request.ToLegs.Leg.ToDetail.locDetail1,
                                                            request.FromDetail.locDetail1) == false)
                                                    {
                                                        allRecordsInsertedSucessfully = false;
                                                    }
                                                    else
                                                    {
                                                        var booking = new Booking();

                                                        //check if the Account Code or Service Codes are empty
                                                        if (string.IsNullOrEmpty(request.AccountCode))
                                                        {
                                                            if ((login.lstAccountCodes != null) &&
                                                                (login.lstAccountCodes.Count > 0))
                                                            {
                                                                booking.AccountCode = login.lstAccountCodes[0];
                                                            }
                                                            else
                                                            {
                                                                if (File.Exists(sourceFile))
                                                                {
                                                                    //  Core.await Logger.Log("Error:Cannot use any predefined Account Code. This file will not be processed untill the Account Codes are sent with the file. File=" + sourceFile, Name());								
                                                                    MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                                }

                                                                continue;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            booking.AccountCode = request.AccountCode;
                                                        }

                                                        //check if the Account Code or Service Codes are empty
                                                        if (string.IsNullOrEmpty(request.ServiceCode))
                                                        {
                                                            if ((login.lstAccountCodes != null) &&
                                                                (login.lstServiceCodes.Count > 0))
                                                            {
                                                                //booking.AccountCode = login.lstAccountCodes[0];
                                                                booking.ServiceCode = login.lstServiceCodes[0];
                                                            }
                                                            else
                                                            {
                                                                if (File.Exists(sourceFile))
                                                                {
                                                                    // Core.await Logger.Log("Error:Cannot use any predefined Service Code. This file will not be processed untill the Service Codes are sent with the file. File=" + sourceFile, Name());
                                                                    MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                                }

                                                                continue;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (request.AccountCode == "KBEFMLOG" &&
                                                                fulcrumRuns.Contains(request.ServiceCode.Trim()))
                                                            {
                                                                booking.ServiceCode = "CPOD";
                                                                request.RunCode = request.ServiceCode.Trim();
                                                            }
                                                            else
                                                            {
                                                                booking.ServiceCode = request.ServiceCode;
                                                            }
                                                        }

                                                        //booking.DespatchDateTime = DateTime.Now;

                                                        var ld = new LoginDetails { Id = login.Id };
                                                        booking.LoginDetails = ld;
                                                        var xCabClientCustomSettings = new XCabClientCustomSettings();
                                                        try
                                                        {
                                                            //setup xcab client custom settings
                                                            if (!string.IsNullOrEmpty(login.Id) &&
                                                                !string.IsNullOrEmpty(login.UserName) &&
                                                                !string.IsNullOrEmpty(request.AccountCode) &&
                                                                !string.IsNullOrEmpty(request.ServiceCode) &&
                                                                stateId > 0)
                                                                xCabClientCustomSettings =
                                                                    await SetupClientSettingsForAsnBarcodeRef3(login.Id,
                                                                        login.UserName, stateId, request.AccountCode,
                                                                        request.ServiceCode);
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            if (VerboseLoggingEnabled)
                                                                RollingLogger.WriteToShredderProcessorLogs(
                                                                    $"Execute Method - Error occurred while retrieving client custom settings for ASN, ErrorMessage: {e.Message}",
                                                                    ELogTypes.Error);
                                                            await Logger.Log(
                                                                "Error occurred while retrieving client custom settings for ASN," +
                                                                "ErrorMessage:" + e.Message, Name());
                                                        }

                                                        if (stateId != -1)
                                                        {
                                                            booking.StateId = stateId.ToString();
                                                        }

                                                        booking.FromDetail1 = request.FromDetail.locDetail1;
                                                        booking.FromDetail2 = request.FromDetail.locDetail2;
                                                        booking.FromDetail3 = request.FromDetail.locDetail3;
                                                        booking.FromDetail4 = request.FromDetail.locDetail4;
                                                        booking.FromDetail5 = request.FromDetail.locDetail5;
                                                        booking.FromPostcode = request.FromDetail.locPostcode;
                                                        booking.FromSuburb = request.FromDetail.locSuburb;
                                                        booking.ToDetail1 = request.ToLegs.Leg.ToDetail.locDetail1;
                                                        booking.ToDetail2 = request.ToLegs.Leg.ToDetail.locDetail2;
                                                        booking.ToDetail3 = request.ToLegs.Leg.ToDetail.locDetail3;
                                                        booking.ToDetail4 = request.ToLegs.Leg.ToDetail.locDetail4;
                                                        booking.ToDetail5 = request.ToLegs.Leg.ToDetail.locDetail5;
                                                        booking.ToSuburb = request.ToLegs.Leg.ToDetail.locSuburb;
                                                        booking.ToPostcode = request.ToLegs.Leg.ToDetail.locPostcode;
                                                        booking.UsingComo = isComoActiveState;
                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(request.ToLegs.Leg.ATL))
                                                            {
                                                                booking.ATL =
                                                                    Boolean.Parse(request.ToLegs.Leg.ATL.Trim());
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred while extracting ATL information in CapitalSchemaV1.1, exception message:" +
                                                                e.Message, Name());
                                                        }

                                                        if (!string.IsNullOrEmpty(request.ToLegs.Leg
                                                                .TrackAndTraceSmsNumber))
                                                        {
                                                            booking.TrackAndTraceSmsNumber =
                                                                request.ToLegs.Leg.TrackAndTraceSmsNumber;
                                                        }

                                                        if (!string.IsNullOrEmpty(request.ToLegs.Leg
                                                                .TrackAndTraceEmailAddress))
                                                        {
                                                            booking.TrackAndTraceEmailAddress =
                                                                request.ToLegs.Leg.TrackAndTraceEmailAddress;
                                                        }

                                                        if (!string.IsNullOrEmpty(
                                                                request.FromDetail.SpecialInstructions))
                                                        {
                                                            booking.ExtraPuInformation =
                                                                request.FromDetail.SpecialInstructions;
                                                        }

                                                        if (!string.IsNullOrEmpty(request.ToLegs.Leg.ToDetail
                                                                .SpecialInstructions))
                                                        {
                                                            booking.ExtraDelInformation = request.ToLegs.Leg.ToDetail
                                                                .SpecialInstructions;
                                                        }

                                                        if (request.ToLegs.Leg.Ref1 != null)
                                                        {
                                                            booking.Ref1 = request.ToLegs.Leg.Ref1;
                                                        }
                                                        else
                                                        {
                                                            booking.Ref1 = string.Empty;
                                                        }

                                                        try
                                                        {
                                                            if ((request.ToLegs.Leg.Ref3 != null) &&
                                                                xCabClientCustomSettings.isRef3MappedToRef2)
                                                            {
                                                                booking.Ref2 = request.ToLegs.Leg.Ref3;
                                                                request.ToLegs.Leg.Ref3 = null;
                                                            }

                                                            if ((request.ToLegs.Leg.Ref2 != null) &&
                                                                xCabClientCustomSettings.isRef3MappedToRef2)
                                                            {
                                                                request.ToLegs.Leg.Ref3 = request.ToLegs.Leg.Ref2;
                                                            }
                                                            else if ((request.ToLegs.Leg.Ref2 != null) &&
                                                                     !xCabClientCustomSettings.isRef3MappedToRef2)
                                                            {
                                                                booking.Ref2 = request.ToLegs.Leg.Ref2;
                                                            }
                                                            else if (!xCabClientCustomSettings.isRef3MappedToRef2)
                                                            {
                                                                booking.Ref2 = string.Empty;
                                                            }

                                                            if (request.ToLegs.Leg.Ref3 != null)
                                                            {
                                                                try
                                                                {
                                                                    var extraField = new ExtraFields
                                                                    {
                                                                        Key = "Ref3",
                                                                        Value = request.ToLegs.Leg.Ref3
                                                                    };

                                                                    if (booking.lstExtraFields == null)
                                                                    {
                                                                        var extraFields = new List<ExtraFields>
                                                                        {
                                                                            extraField
                                                                        };
                                                                        booking.lstExtraFields = extraFields;
                                                                    }
                                                                    else
                                                                    {
                                                                        booking.lstExtraFields.Add(extraField);
                                                                    }
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    await Logger.Log(
                                                                        "Exception Occurred when mapping extra references in CAPITALV2 default schema. Message: " +
                                                                        e.Message, Name());
                                                                }
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred while mapping Ref2 and Ref3 in CAPITALV2 default schema. Message: " +
                                                                e.Message, Name());
                                                        }

                                                        try
                                                        {
                                                            if (request.PlannedSpecified)
                                                            {
                                                                booking.DespatchDateTime = request.Planned;
                                                                //we realize that TPLUS does not put adv date time on line 5, so we force it here:
                                                                booking.FromDetail5 = "Earliest PU " +
                                                                    booking.DespatchDateTime.ToString("dd MMM HH:mm");
                                                            }
                                                            else if (request.EarliestSpecified)
                                                            {
                                                                booking.DespatchDateTime = request.Earliest;
                                                                //we realize that TPLUS does not put adv date time on line 5, so we force it here:
                                                                booking.FromDetail5 = "Earliest PU " +
                                                                    booking.DespatchDateTime.ToString("dd MMM HH:mm");
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred while extracting Despatch Date time from Planned/Earliest Xml field, exception message:" +
                                                                e.Message, Name());
                                                        }

                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(request.DriverNumber))
                                                            {
                                                                if (Int32.TryParse(request.DriverNumber,
                                                                        out int driver))
                                                                {
                                                                    booking.DriverNumber = driver;
                                                                }
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred while extracting Driver number Xml field, exception message:" +
                                                                ex.Message, Name());
                                                        }

                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(request.Caller))
                                                            {
                                                                booking.Caller = request.Caller.Trim();
                                                            }
                                                            else if (!string.IsNullOrWhiteSpace(request.RunCode))
                                                            {
                                                                //this is mostly required for all NHP bookings however we should do it for all bookings where the runcode is used
                                                                booking.Caller = request.RunCode.Trim();
                                                            }

                                                            if (xCabClientCustomSettings.isStagedOnlyBooking == false &&
                                                                !string.IsNullOrWhiteSpace(request.RunCode))
                                                            {
                                                                var xcabDriverRouteRepository =
                                                                    new XCabDriverRouteRepository();
                                                                var xcabDriverRoutes =
                                                                    xcabDriverRouteRepository
                                                                        .GetXCabDriverRoutesForRouteName(
                                                                            request.RunCode.ToUpper(), login.Id,
                                                                            stateId, request.AccountCode);

                                                                if (xcabDriverRoutes != null &&
                                                                    !string.IsNullOrEmpty(xcabDriverRoutes
                                                                        .DriverNumber))
                                                                {
                                                                    int.TryParse(xcabDriverRoutes.DriverNumber,
                                                                        out int driver);
                                                                    booking.DriverNumber = driver;
                                                                    if (request.AccountCode == "KBEFMLOG" &&
                                                                        fulcrumRuns.Contains(request.ServiceCode
                                                                            .Trim()))
                                                                    {
                                                                        booking.IsQueued = true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred while extracting call / runcode Xml field, exception message:" +
                                                                e.Message, Name());
                                                        }

                                                        try
                                                        {
                                                            if ((request.ToLegs.Leg.Items != null) &&
                                                                (request.ToLegs.Leg.Items.Count != 0))
                                                            {
                                                                var lstItems = request.ToLegs.Leg.Items;
                                                                var lstBarcodes = new List<Item>();
                                                                foreach (var item in lstItems)
                                                                {
                                                                    var _item = new Item
                                                                    { Description = item.itmDescription };
                                                                    if (item.itmLengthSpecified)
                                                                    {
                                                                        _item.Length = (double)item.itmLength;
                                                                    }

                                                                    if (item.itmWidthSpecified)
                                                                    {
                                                                        _item.Width = (double)item.itmWidth;
                                                                    }

                                                                    if (item.itmHeightSpecified)
                                                                    {
                                                                        _item.Height = (double)item.itmHeight;
                                                                    }

                                                                    if (item.itmWeightSpecified)
                                                                    {
                                                                        _item.Weight = (double)item.itmWeight;
                                                                    }

                                                                    if (item.itmCubicSpecified)
                                                                    {
                                                                        _item.Cubic = (double)item.itmCubic;
                                                                    }

                                                                    _item.Barcode = item.itmBarcode;
                                                                    _item.Quantity = 1;
                                                                    lstBarcodes.Add(_item);
                                                                }

                                                                //assign the list of items back to the bookings
                                                                booking.lstItems = lstBarcodes;
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred in parsing items:" + e.Message,
                                                                Name());
                                                        }

                                                        try
                                                        {
                                                            if ((request.ToLegs.Leg.PhoneNumbers != null) &&
                                                                (request.ToLegs.Leg.PhoneNumbers.Count != 0))
                                                            {
                                                                var lstContactDetail = new List<ContactDetail>();
                                                                var lstPhoneNumber = request.ToLegs.Leg.PhoneNumbers;
                                                                foreach (var pnt in lstPhoneNumber)
                                                                {
                                                                    var detail = new ContactDetail
                                                                    {
                                                                        AreaCode = pnt.AreaCode,
                                                                        PhoneNumber = pnt.PhoneNumber
                                                                    };
                                                                    // Core.await Logger.Log("Phone Number:" + pnt.PhoneNumber, Name());
                                                                    lstContactDetail.Add(detail);
                                                                }

                                                                //assign the list of contact details back to the booking structure
                                                                booking.lstContactDetail = lstContactDetail;
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred in parsing phone number:" +
                                                                e.Message, Name());
                                                        }

                                                        //send the list of bookings to the Data Layer
                                                        var successfullyInserted = true;
                                                        try
                                                        {
                                                            if (booking != null)
                                                            {
                                                                var dbConnector = new DbConnector();
                                                                successfullyInserted = dbConnector.StoreToDb(booking);
                                                                if (!successfullyInserted)
                                                                    allRecordsInsertedSucessfully = false;
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred while writing to the db:" +
                                                                e.Message, Name());
                                                            allRecordsInsertedSucessfully = false;
                                                        }
                                                    }
                                                }
                                            }

                                            // Move the file to Processed folder if all bookings are successfullyInserted
                                            if (allRecordsInsertedSucessfully)
                                            {
                                                var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                    login.UserName, login.BookingsFolderName,
                                                    DbSettings.Default.ProcessedFolderName);

                                                //also move the file across the ftp folder
                                                MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                            }
                                            else
                                            {
                                                //there has been an error - so we move to the Error Folder
                                                var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                    login.UserName,
                                                    login.BookingsFolderName + "\\" +
                                                    DbSettings.Default.ErrorFolderName);

                                                //also move the file across the ftp folder
                                                MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Logger.Log("Exception Occurred while creating job " + ex.Message,
                                                Name());

                                            var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                login.UserName, login.BookingsFolderName,
                                                DbSettings.Default.ErrorFolderName);
                                            if (File.Exists(sourceFile))
                                            {
                                                MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        await Logger.Log(
                                            "Exception occured while proccessing the File " + sourceFile +
                                            " ErrorMessage:" +
                                            e.Message, Name());
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        continue;
                                    }
                                }

                                #endregion

                                #region Capital 1.1 Schema

                                else if (login.BookingSchemaName.ToUpper() == "CAPITAL-1.1")
                                {
                                    try
                                    {
                                        XCabBookingFileExtractor.CapitalSchemaV1_1.Request request = null;
                                        var originalSourceFile = sourceFile;
                                        var successfullyInserted = true;
                                        var booking = new Booking();
                                        const string NoAccountCodeMessage =
                                            "File Received with No Account Code, Received file is attached with this email. Please review that the file has Account Code as most likely this value is missing and has caused the booking to fail. This booking will not appear in TPLUS.";
                                        const string NoAccountCodeMessageTitle =
                                            "XCab:File Receieved with Missing Account Code";
                                        const string DatabaseInsertFailesMessage =
                                            "File Received but Database Insertion Failed, Received file is attached with this email. Please review that the file has Suburb, Postcode, AccountCode & ServiceCode as one or more of these missing values have caused the booking to fail. This booking will not appear in TPLUS.";
                                        const string NoServiceCodeMessage =
                                            "File Received with No Service Code, Received file is attached with this email. Please review that the file has Service Code as most likely this value is missing and has caused the booking to fail. This booking will not appear in TPLUS.";
                                        const string NoServiceCodeMessageTitle =
                                            "XCab:File Receieved with Missing Service Code";
                                        try
                                        {
                                            request =
                                                XCabBookingFileExtractor.CapitalSchemaV1_1.Request.LoadFromFile(
                                                    sourceFile);
                                        }
                                        catch (Exception e)
                                        {
                                            await Logger.Log(
                                                "Parsing Error: Could not parse the received file. File=" +
                                                sourceFile +
                                                ", This file will be moved into the Errors Folder and will not be created as a booking in TPLUS. Exception Details:" +
                                                e.Message, Name(), true);
                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                            continue;
                                        }

                                        if (request != null)
                                        {
                                            if (_requestValidationProvider.IsRequestValid(request.AccountCode,
                                                    request.ServiceCode, request.FromDetail.locSuburb,
                                                    request.FromDetail.locPostcode,
                                                    request.ToLegs.Leg.ToDetail.locSuburb,
                                                    request.ToLegs.Leg.ToDetail.locPostcode,
                                                    request.ToLegs.Leg.ToDetail.locDetail1,
                                                    request.FromDetail.locDetail1) == false)
                                            {
                                                successfullyInserted = false;
                                            }
                                            else
                                            {
                                                sourceFile =
                                                    originalSourceFile; //change it back to the original source file
                                                //read through the file grabbing contents as needed
                                                //check if the Account Code or Service Codes are empty

                                                var stateId = utils.GetStateId(request.CapitalState.ToString());
                                                if (comoActiveStatusForStates != null &&
                                                    comoActiveStatusForStates.Count > 0)
                                                {
                                                    isComoActiveState = comoActiveStatusForStates[stateId];
                                                }

                                                if (string.IsNullOrEmpty(request.AccountCode))
                                                {
                                                    await Logger.Log(
                                                        "Error:Received an empty Account Code. Will try using default AccountCode setup in the system. File=" +
                                                        sourceFile, Name());

                                                    Alert(NoAccountCodeMessage, NoAccountCodeMessageTitle,
                                                        Convert.ToInt32(booking.LoginDetails.Id),
                                                        Convert.ToInt16(booking.StateId), true, sourceFile);

                                                    if ((login.lstAccountCodes != null) &&
                                                        (login.lstAccountCodes.Count > 0))
                                                    {
                                                        booking.AccountCode = login.lstAccountCodes[0];
                                                    }
                                                    else
                                                    {
                                                        await Logger.Log(
                                                            "Error:Cannot use any predefined Account Code. This file will not be processed untill the Account Codes are sent with the file. File=" +
                                                            sourceFile, Name());
                                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    booking.AccountCode = request.AccountCode;
                                                }

                                                //check if the Account Code or Service Codes are empty
                                                if (string.IsNullOrEmpty(request.ServiceCode))
                                                {
                                                    await Logger.Log(
                                                        "Error:Received an empty Service Code. Will try using default Service setup in the system. File=" +
                                                        sourceFile, Name());
                                                    Alert(NoServiceCodeMessage, NoServiceCodeMessageTitle,
                                                        Convert.ToInt32(booking.LoginDetails.Id),
                                                        Convert.ToInt16(booking.StateId), true, sourceFile);
                                                    if ((login.lstAccountCodes != null) &&
                                                        (login.lstServiceCodes.Count > 0))
                                                    {
                                                        //booking.AccountCode = login.lstAccountCodes[0];
                                                        booking.ServiceCode = login.lstServiceCodes[0];
                                                    }
                                                    else
                                                    {
                                                        await Logger.Log(
                                                            "Error:Cannot use any predefined Service Code. This file will not be processed untill the Service Codes are sent with the file. File=" +
                                                            sourceFile, Name());
                                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    booking.ServiceCode = request.ServiceCode;
                                                }

                                                if (!string.IsNullOrEmpty(request.DriverNumber))
                                                {
                                                    if (Int32.TryParse(request.DriverNumber, out int driver))
                                                    {
                                                        booking.DriverNumber = driver;
                                                        booking.IsQueued = true;
                                                    }
                                                }

                                                var ld = new LoginDetails { Id = login.Id };
                                                booking.LoginDetails = ld;
                                                var xCabClientCustomSettings = new XCabClientCustomSettings();
                                                try
                                                {
                                                    //setup xcab client custom settings
                                                    if (!string.IsNullOrEmpty(login.Id) &&
                                                        !string.IsNullOrEmpty(login.UserName) &&
                                                        !string.IsNullOrEmpty(request.AccountCode) &&
                                                        !string.IsNullOrEmpty(request.ServiceCode) && stateId > 0)
                                                        xCabClientCustomSettings =
                                                            await SetupClientSettingsForAsnBarcodeRef3(login.Id,
                                                                login.UserName, stateId, request.AccountCode,
                                                                request.ServiceCode);
                                                }
                                                catch (Exception e)
                                                {
                                                    if (VerboseLoggingEnabled)
                                                        RollingLogger.WriteToShredderProcessorLogs(
                                                            $"Execute Method - Error occurred while retrieving client custom settings for ASN. Username: {login.UserName}, ErrorMessage: {e.Message}",
                                                            ELogTypes.Error);
                                                    await Logger.Log(
                                                        $"Error occurred while retrieving client custom settings for ASN, ErrorMessage: {e.Message}",
                                                        Name());
                                                }

                                                if (stateId != -1)
                                                {
                                                    booking.StateId = stateId.ToString();
                                                }

                                                booking.FromDetail1 = request.FromDetail.locDetail1;
                                                booking.FromDetail2 = request.FromDetail.locDetail2;
                                                booking.FromDetail3 = request.FromDetail.locDetail3;
                                                booking.FromDetail4 = request.FromDetail.locDetail4;
                                                booking.FromDetail5 = request.FromDetail.locDetail5;
                                                booking.FromPostcode = request.FromDetail.locPostcode;
                                                booking.FromSuburb = request.FromDetail.locSuburb;
                                                booking.ToDetail1 = request.ToLegs.Leg.ToDetail.locDetail1;
                                                booking.ToDetail2 = request.ToLegs.Leg.ToDetail.locDetail2;
                                                booking.ToDetail3 = request.ToLegs.Leg.ToDetail.locDetail3;
                                                booking.ToDetail4 = request.ToLegs.Leg.ToDetail.locDetail4;
                                                booking.ToDetail5 = request.ToLegs.Leg.ToDetail.locDetail5;
                                                booking.ToSuburb = request.ToLegs.Leg.ToDetail.locSuburb;
                                                booking.ToPostcode = request.ToLegs.Leg.ToDetail.locPostcode;
                                                booking.TrackAndTraceSmsNumber =
                                                    request.ToLegs.Leg.TrackAndTraceSmsNumber;
                                                booking.TrackAndTraceEmailAddress =
                                                    request.ToLegs.Leg.TrackAndTraceEmailAddress;
                                                booking.UsingComo = isComoActiveState;
                                                try
                                                {
                                                    if (!string.IsNullOrEmpty(request.ToLegs.Leg.Atl))
                                                    {
                                                        booking.ATL = Boolean.Parse(request.ToLegs.Leg.Atl.Trim());
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    await Logger.Log(
                                                        "Exception Occurred while extracting ATL information in CapitalSchemaV1.1, exception message:" +
                                                        e.Message, Name());
                                                }

                                                if (!string.IsNullOrEmpty(request.ToLegs.Leg.TrackAndTraceSmsNumber))
                                                {
                                                    booking.TrackAndTraceSmsNumber =
                                                        request.ToLegs.Leg.TrackAndTraceSmsNumber;
                                                }

                                                if (!string.IsNullOrEmpty(request.ToLegs.Leg.TrackAndTraceEmailAddress))
                                                {
                                                    booking.TrackAndTraceEmailAddress =
                                                        request.ToLegs.Leg.TrackAndTraceEmailAddress;
                                                }

                                                if (!string.IsNullOrEmpty(request.Caller))
                                                {
                                                    booking.Caller = request.Caller.Trim();
                                                }
                                                else
                                                {
                                                    //since there is no run code field in TPLUS, if this value is provided we overwrite onto the caller field
                                                    if (!string.IsNullOrEmpty(request.RunCode))
                                                    {
                                                        booking.Caller = request.RunCode.Trim();
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(request.RunCode))
                                                {
                                                    var xcabDriverRouteRepository = new XCabDriverRouteRepository();
                                                    var xCabDriverRoute =
                                                        xcabDriverRouteRepository.GetXCabDriverRoutesForRouteName(
                                                            request.RunCode, login.Id, stateId, request.AccountCode);
                                                    if (xCabDriverRoute != null &&
                                                        string.IsNullOrEmpty(xCabDriverRoute.DriverNumber))
                                                    {
                                                        int.TryParse(request.RunCode, out int driver);
                                                        if (driver > 0)
                                                        {
                                                            booking.DriverNumber = driver;
                                                            booking.IsQueued = true;
                                                        }
                                                    }
                                                }


                                                if (!string.IsNullOrEmpty(request.SpecialInstructions))
                                                {
                                                    booking.ExtraPuInformation = request.SpecialInstructions;
                                                    booking.ExtraDelInformation = request.SpecialInstructions;
                                                }

                                                if (request.ToLegs.Leg.Ref1 != null)
                                                {
                                                    booking.Ref1 = request.ToLegs.Leg.Ref1;
                                                }
                                                else
                                                {
                                                    booking.Ref1 = string.Empty;
                                                }

                                                try
                                                {
                                                    if ((request.ToLegs.Leg.Ref3 != null) &&
                                                        xCabClientCustomSettings.isRef3MappedToRef2)
                                                    {
                                                        booking.Ref2 = request.ToLegs.Leg.Ref3;
                                                        request.ToLegs.Leg.Ref3 = null;
                                                    }

                                                    if ((request.ToLegs.Leg.Ref2 != null) &&
                                                        xCabClientCustomSettings.isRef3MappedToRef2)
                                                    {
                                                        request.ToLegs.Leg.Ref3 = request.ToLegs.Leg.Ref2;
                                                    }
                                                    else if ((request.ToLegs.Leg.Ref2 != null) &&
                                                             !xCabClientCustomSettings.isRef3MappedToRef2)
                                                    {
                                                        booking.Ref2 = request.ToLegs.Leg.Ref2;
                                                    }
                                                    else if (!xCabClientCustomSettings.isRef3MappedToRef2)
                                                    {
                                                        booking.Ref2 = string.Empty;
                                                    }

                                                    if (request.ToLegs.Leg.Ref3 != null)
                                                    {
                                                        try
                                                        {
                                                            var extraField = new ExtraFields
                                                            {
                                                                Key = "Ref3",
                                                                Value = request.ToLegs.Leg.Ref3
                                                            };

                                                            if (booking.lstExtraFields == null)
                                                            {
                                                                var extraFields = new List<ExtraFields>
                                                                {
                                                                    extraField
                                                                };
                                                                booking.lstExtraFields = extraFields;
                                                            }
                                                            else
                                                            {
                                                                booking.lstExtraFields.Add(extraField);
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            await Logger.Log(
                                                                "Exception Occurred when mapping extra references in CAPITAL-1.1 default schema. Message: " +
                                                                e.Message, Name());
                                                        }
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    await Logger.Log(
                                                        "Exception Occurred while mapping Ref2 and Ref3 in CAPITAL-1.1 default schema. Message: " +
                                                        e.Message, Name());
                                                }

                                                try
                                                {
                                                    if (request.Planned != DateTime.MinValue)
                                                    {
                                                        booking.DespatchDateTime = request.Planned;
                                                        //we realize that TPLUS does not put adv date time on line 5, so we force it here:
                                                        booking.FromDetail5 = "Earliest PU " +
                                                                              booking.DespatchDateTime.ToString(
                                                                                  "dd MMM HH:mm");
                                                    }
                                                    //else if (request.EarliestSpecified)
                                                    else if (request.Earliest != DateTime.MinValue)
                                                    {
                                                        booking.DespatchDateTime = request.Earliest;
                                                        //we realize that TPLUS does not put adv date time on line 5, so we force it here:
                                                        booking.FromDetail5 = "Earliest PU " +
                                                                              booking.DespatchDateTime.ToString(
                                                                                  "dd MMM HH:mm");
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    await Logger.Log(
                                                        "Exception Occurred while extracting Despatch Date time from Planned/Earliest Xml field, exception message:" +
                                                        e.Message, Name());
                                                }

                                                try
                                                {
                                                    if ((request.ToLegs.Leg.Items != null) &&
                                                        (request.ToLegs.Leg.Items.Count != 0))
                                                    {
                                                        var lstItems = request.ToLegs.Leg.Items;
                                                        var lstBarcodes = new List<Item>();
                                                        foreach (var item in lstItems)
                                                        {
                                                            var _item = new Item { Description = item.itmDescription };
                                                            if (item.itmLengthSpecified)
                                                            {
                                                                //if (item.itmLength!=0)
                                                                _item.Length = (double)item.itmLength;
                                                            }

                                                            if (item.itmWidthSpecified)
                                                            {
                                                                //if (item.itmWidth!=0)
                                                                _item.Width = (double)item.itmWidth;
                                                            }

                                                            if (item.itmHeightSpecified)
                                                            {
                                                                //if (item.itmHeight!=0)
                                                                _item.Height = (double)item.itmHeight;
                                                            }

                                                            if (item.itmWeightSpecified)
                                                            {
                                                                //if (item.itmWeight!=0)
                                                                _item.Weight = string.IsNullOrWhiteSpace(item.itmWeight)
                                                                    ? 0
                                                                    : double.Parse(item.itmWeight.Replace(",", ""));
                                                            }

                                                            if (item.itmCubicSpecified)
                                                            {
                                                                //if (item.itmCubic!=0)
                                                                _item.Cubic = (double)item.itmCubic;
                                                            }

                                                            _item.Barcode = item.itmBarcode;
                                                            _item.Quantity = 1;
                                                            lstBarcodes.Add(_item);
                                                        }

                                                        //assign the list of items back to the bookings
                                                        booking.lstItems = lstBarcodes;
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    await Logger.Log("Exception Occurred in parsing items:" + e.Message,
                                                        Name());
                                                }

                                                try
                                                {
                                                    if ((request.ToLegs.Leg.PhoneNumbers != null) &&
                                                        (request.ToLegs.Leg.PhoneNumbers.Count != 0))
                                                    {
                                                        var lstPhoneNumber = request.ToLegs.Leg.PhoneNumbers;
                                                        var lstContactDetail = lstPhoneNumber.Select(pnt =>
                                                            new ContactDetail
                                                            {
                                                                AreaCode = pnt.AreaCode,
                                                                PhoneNumber = pnt.PhoneNumber
                                                            }).ToList();
                                                        //assign the list of contact details back to the booking structure
                                                        booking.lstContactDetail = lstContactDetail;
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    await Logger.Log(
                                                        "Exception Occurred in parsing phone number:" + e.Message,
                                                        Name());
                                                }

                                                //check if ToDetail Line 3 is empty, if so we can add the first item description here
                                                if (string.IsNullOrEmpty(booking.ExtraPuInformation))
                                                {
                                                    if (booking.lstItems.Count > 0)
                                                    {
                                                        if (!string.IsNullOrEmpty(booking.lstItems[0].Description))
                                                        {
                                                            booking.ExtraPuInformation =
                                                                booking.lstItems[0].Description;
                                                            if (string.IsNullOrEmpty(booking.ExtraDelInformation))
                                                            {
                                                                booking.ExtraDelInformation =
                                                                    booking.ExtraPuInformation;
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (string.IsNullOrEmpty(booking.ToDetail3))
                                                {
                                                    if (booking.lstItems.Count > 0)
                                                    {
                                                        if (!string.IsNullOrEmpty(booking.lstItems[0].Description))
                                                        {
                                                            booking.ToDetail3 = booking.lstItems[0].Description;
                                                        }
                                                    }
                                                }

                                                //send the list of bookings to the Data Layer
                                                try
                                                {
                                                    if (booking != null)
                                                    {
                                                        var dbConnector = new DbConnector();
                                                        successfullyInserted = dbConnector.StoreToDb(booking);
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    await Logger.Log(
                                                        "Exception Occurred while writing to the db:" + e.Message,
                                                        Name());
                                                    successfullyInserted = false;
                                                }
                                            }
                                        }

                                        //once this file has been processed move it to the processed folder
                                        //string destinationFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), processedFolderName);
                                        if (successfullyInserted)
                                        {
                                            var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                login.UserName, login.BookingsFolderName,
                                                DbSettings.Default.ProcessedFolderName);
                                            //also move the file across the ftp folder
                                            MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                        }
                                        else
                                        {
                                            Alert(DatabaseInsertFailesMessage, DatabaseInsertFailesMessageTitle,
                                                Convert.ToInt32(booking.LoginDetails.Id),
                                                Convert.ToInt16(booking.StateId), true, sourceFile);
                                            //there has been an error - so we move to the Error Folder
                                            var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                login.UserName,
                                                login.BookingsFolderName + "\\" + DbSettings.Default.ErrorFolderName);

                                            //also move the file across the ftp folder
                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        await Logger.Log(
                                            "Exception occured while proccessing the File " + sourceFile +
                                            " ErrorMessage:" +
                                            e.Message, Name());
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        continue;
                                    }
                                }

                                #endregion

                                #region CAPITAL-CSV

                                else if (login.BookingSchemaName.ToUpper() == "CAPITAL-CSV")
                                {
                                    try
                                    {
                                        var mappedStateId = -1;
                                        var accountCode = "";
                                        try
                                        {
                                            //syntax is: StateId_AccountCode_YYYY-MM-DD_HH-MM-SS.csv   e.g: - VIC_3GASVIC_2019-12-02_13-28-13
                                            var relativePath = Path.GetFileName(sourceFile);
                                            string[] stateAccountFromFile = relativePath?.Split('_');
                                            if (stateAccountFromFile?.Length > 0)
                                            {
                                                switch (stateAccountFromFile[0].ToUpper())
                                                {
                                                    case "VIC":
                                                        mappedStateId = 1;
                                                        break;
                                                    case "NSW":
                                                        mappedStateId = 2;
                                                        break;
                                                    case "QLD":
                                                        mappedStateId = 3;
                                                        break;
                                                    case "SA":
                                                        mappedStateId = 4;
                                                        break;
                                                    case "WA":
                                                        mappedStateId = 5;
                                                        break;
                                                    default:
                                                        mappedStateId = -1;
                                                        break;
                                                }
                                            }

                                            if (stateAccountFromFile?.Length > 1)
                                            {
                                                accountCode = stateAccountFromFile[1].ToUpper();
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            await Logger.Log
                                            ($"Error while setting state and account code for FTP CSV extraction, filename: {sourceFile}. Since there is no state information it will be moved to the Errors folder ,Error= {e.Message}",
                                                Name()
                                            );
                                        }

                                        //if we are unable to find a mapping state we need to flag this as an error and move the file to the errors folder
                                        if (mappedStateId == -1)
                                        {
                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);

                                            return;
                                        }

                                        //if we are unable to find a account code we need to flag this as an error and move the file to the errors folder
                                        if (string.IsNullOrWhiteSpace(accountCode))
                                        {
                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);

                                            return;
                                        }

                                        var lstBookings = new List<Booking>();
                                        var gasMotorsHelper = new GasMotorsHelper();
                                        var csvFileHelper = new CsvFileHelper();
                                        var xcabClientIntegrationRepository = new XCabClientIntegrationRepository();
                                        ICollection<XCabClientIntegration> xcabClientIntegrations = null;

                                        try
                                        {
                                            xcabClientIntegrations =
                                                xcabClientIntegrationRepository.GetDefaultAddressDetails(
                                                    new List<string>
                                                    {
                                                        accountCode,
                                                    });
                                        }
                                        catch (Exception ex)
                                        {
                                            await Logger.Log
                                            ($"Error while extracting client integration information from xCabClientIntegration for account {accountCode} for state {mappedStateId}. Error is {ex.Message}",
                                                Name()
                                            );
                                        }

                                        try
                                        {
                                            var csvRows = csvFileHelper.GetFilecontents(sourceFile);

                                            if (csvRows != null && csvRows.Count > 1)
                                            {
                                                lstBookings =
                                                    gasMotorsHelper.ExtractBooking(accountCode, mappedStateId, csvRows,
                                                        xcabClientIntegrations);
                                            }
                                            // Delete the file if file contains only headers and no data.
                                            else if (csvRows != null && csvRows.Count == 1)
                                            {
                                                File.Delete(sourceFile);
                                                return;
                                            }

                                            // Below condition will be true when file will contain all invalid records. 
                                            // e.g. - 308923, ,Parts Distribution Centre,,,,,, ,1.00, , ,14-jun-20220806,GM, , 
                                            // Delivery suburb and postcode are missing. 
                                            if (lstBookings.Count == 0)
                                            {
                                                MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                                return;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Logger.Log
                                            ($"Error while extracting bookings for FTP CSV extraction, filename: {sourceFile}. Error message {ex.Message}",
                                                Name()
                                            );
                                        }


                                        if ((lstBookings != null) && (lstBookings.Count > 0) &&
                                            xcabClientIntegrations != null && xcabClientIntegrations.FirstOrDefault()
                                                .AllowConsolidation)
                                        {
                                            var successfullyInserted = true;
                                            //try to consolidate the jobs 
                                            var xCabBookingRepository = new XCabBookingRepository();

                                            var consolidatedJobs = JobConsolidation.ByToDetail2(lstBookings);
                                            if (consolidatedJobs?.ConsolidatedBookings != null &&
                                                (consolidatedJobs.ConsolidatedBookings.Count > 0))
                                            {
                                                var xCabClientReferencesRepository =
                                                    new XCabClientReferencesRepository();
                                                foreach (var bookingToCreate in consolidatedJobs.ConsolidatedBookings)
                                                {
                                                    try
                                                    {
                                                        if (bookingToCreate == null)
                                                        {
                                                            continue;
                                                        }

                                                        if (_requestValidationProvider.IsRequestValid(
                                                                bookingToCreate.AccountCode,
                                                                bookingToCreate.ServiceCode, bookingToCreate.FromSuburb,
                                                                bookingToCreate.FromPostcode, bookingToCreate.ToSuburb,
                                                                bookingToCreate.ToPostcode, bookingToCreate.ToDetail1,
                                                                bookingToCreate.FromDetail1) == false)
                                                        {
                                                            successfullyInserted = false;
                                                        }
                                                        else
                                                        {
                                                            if (comoActiveStatusForStates != null &&
                                                                comoActiveStatusForStates.Count > 0)
                                                            {
                                                                isComoActiveState =
                                                                    comoActiveStatusForStates[
                                                                        int.Parse(bookingToCreate.StateId)];
                                                            }

                                                            //perform any data truncations for references
                                                            if (!string.IsNullOrWhiteSpace(bookingToCreate.Ref1) &&
                                                                bookingToCreate.Ref1.Length > 16)
                                                            {
                                                                bookingToCreate.Ref1 =
                                                                    bookingToCreate.Ref1.Substring(0, 15);
                                                            }

                                                            if (!string.IsNullOrWhiteSpace(bookingToCreate.Ref2) &&
                                                                bookingToCreate.Ref2.Length > 16)
                                                            {
                                                                bookingToCreate.Ref2 =
                                                                    bookingToCreate.Ref2.Substring(0, 15);
                                                            }

                                                            //check if we need to queue for allocate jobs
                                                            if (bookingToCreate.PreAllocatedDriverNumber > 0)
                                                            {
                                                                bookingToCreate.IsQueued = true;
                                                            }

                                                            var idInserted = xCabBookingRepository.InsertBooking(
                                                                new XCabBooking
                                                                {
                                                                    AccountCode = bookingToCreate.AccountCode,
                                                                    FromDetail1 = bookingToCreate.FromDetail1,
                                                                    FromDetail2 = bookingToCreate.FromDetail2,
                                                                    FromDetail3 = bookingToCreate.FromDetail3,
                                                                    FromDetail4 = bookingToCreate.FromDetail4,
                                                                    FromDetail5 = bookingToCreate.FromDetail5,
                                                                    FromSuburb = bookingToCreate.FromSuburb,
                                                                    FromPostcode = bookingToCreate.FromPostcode,
                                                                    Ref1 = bookingToCreate.Ref1,
                                                                    Ref2 = bookingToCreate.Ref2,
                                                                    ToDetail1 = bookingToCreate.ToDetail1,
                                                                    ToDetail2 = bookingToCreate.ToDetail2,
                                                                    ToDetail3 = bookingToCreate.ToDetail3,
                                                                    ToDetail4 = bookingToCreate.ToDetail4,
                                                                    ToDetail5 = bookingToCreate.ToDetail5,
                                                                    ToSuburb = bookingToCreate.ToSuburb,
                                                                    ToPostcode = bookingToCreate.ToPostcode,
                                                                    LoginId = Convert.ToInt32(bookingToCreate
                                                                        .LoginDetails.Id),
                                                                    StateId = Convert.ToInt32(bookingToCreate.StateId),
                                                                    ServiceCode = bookingToCreate.ServiceCode,
                                                                    DriverNumber =
                                                                        bookingToCreate.PreAllocatedDriverNumber
                                                                            .ToString(),
                                                                    TotalItems = bookingToCreate.TotalItems,
                                                                    TotalWeight = bookingToCreate.TotalWeight,
                                                                    TotalVolume = bookingToCreate.TotalVolume,
                                                                    ExtraPuInformation =
                                                                        bookingToCreate.ExtraPuInformation,
                                                                    ExtraDelInformation =
                                                                        bookingToCreate.ExtraDelInformation,
                                                                    PreAllocatedDriverNumber =
                                                                        bookingToCreate.PreAllocatedDriverNumber
                                                                            .ToString(),
                                                                    DespatchDateTime = bookingToCreate.DespatchDateTime,
                                                                    IsQueued = bookingToCreate.IsQueued,
                                                                    lstItems = bookingToCreate.lstItems,
                                                                    lstContactDetail = bookingToCreate.lstContactDetail,
                                                                    UsingComo = isComoActiveState
                                                                }
                                                            );
                                                            if (idInserted > 0)
                                                            {
                                                                successfullyInserted = true;
                                                            }

                                                            if ((idInserted > 0) &&
                                                                (consolidatedJobs.GroupedJobs != null) &&
                                                                consolidatedJobs.GroupedJobs.Any())
                                                            {
                                                                successfullyInserted = true;

                                                                var allJobsForSameCustomer =
                                                                    consolidatedJobs.GroupedJobs.Where(
                                                                        group => group.Key ==
                                                                            bookingToCreate.ToDetail2);
                                                                ICollection<XCabClientReferences> references =
                                                                    (from jobs in allJobsForSameCustomer
                                                                     from job in jobs
                                                                     select new XCabClientReferences
                                                                     {
                                                                         JobDate = job.AdvanceDateTime,
                                                                         Reference1 = job.Ref1,
                                                                         Reference2 = job.Ref2,
                                                                         PrimaryJobId = idInserted
                                                                     }).ToList();
                                                                xCabClientReferencesRepository.Insert(references);
                                                            }
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        await Logger.Log(
                                                            "Exception Occurred while writing to the db an EDIFACT Booking:" +
                                                            e.Message, Name());
                                                        successfullyInserted = false;
                                                    }
                                                }

                                                //once this file has been processed move it to the processed folder
                                                if (successfullyInserted)
                                                {
                                                    var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                        login.UserName, login.BookingsFolderName,
                                                        DbSettings.Default.ProcessedFolderName);

                                                    //also move the file across the ftp folder
                                                    MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                                }
                                                else
                                                {
                                                    //there has been an error - so we move to the Error Folder
                                                    var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                        login.UserName,
                                                        login.BookingsFolderName + "\\" +
                                                        DbSettings.Default.ErrorFolderName);

                                                    //also move the file across the ftp folder
                                                    MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        await Logger.Log(
                                            $"Exception occured while proccessing the File {sourceFile} ErrorMessage: {ex.Message}",
                                            Name());
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        continue;
                                    }
                                }

                                #endregion

                                #region CAPITAL-CSV-1.1

                                else if (login.BookingSchemaName.ToUpper() == "CAPITAL-CSV-1.1")
                                {
                                    try
                                    {
                                        var accountCode = "";
                                        try
                                        {
                                            //syntax is: AccountCode_YYYY-MM-DD_HH-MM-SS.csv   e.g: - 3GASVIC.2019.2.02.csv
                                            var fileName = Path.GetFileName(sourceFile);
                                            string[] fileNameElements = fileName.Split('.');

                                            if (fileNameElements.Length <= 0)
                                            {
                                                await Logger.Log(
                                                    $"Error while extracting account details from the file name. File name is not in proper format as expected. File is transfered to Error folder. Filename: {sourceFile}",
                                                    Name());

                                                //there has been an error - so we move to the Error Folder
                                                MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                continue;
                                            }
                                            else
                                            {
                                                accountCode = fileNameElements[1].ToString();
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            await Logger.Log
                                            ($"Error while extracting account code for FTP CSV extraction, filename: {sourceFile} ,Message = {e.Message}",
                                                Name()
                                            );
                                        }

                                        var lstBookings = new List<Booking>();
                                        var xcabClientIntegrationRepository = new XCabClientIntegrationRepository();
                                        ICollection<XCabClientIntegration> xcabClientIntegrations = null;

                                        try
                                        {
                                            xcabClientIntegrations =
                                                xcabClientIntegrationRepository.GetDefaultAddressDetails(
                                                    new List<string>
                                                    {
                                                        accountCode,
                                                    });

                                            if (!xcabClientIntegrations.Any())
                                            {
                                                await Logger.Log(
                                                    $"Error while extracting account details from the XCabClientIntegration. Account details not configured. Filename: {sourceFile}",
                                                    Name());

                                                //there has been an error - so we move to the Error Folder
                                                MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                continue;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Logger.Log
                                            ($"Error while extracting client integration information from xCabClientIntegration for account {accountCode}. Error is {ex.Message}",
                                                Name()
                                            );
                                        }

                                        try
                                        {
                                            var capitalCsvRows = new CapitalCsvHelper().GetCSVFilecontents(sourceFile);

                                            if (capitalCsvRows != null && capitalCsvRows.Count > 0)
                                            {
                                                lstBookings = new CapitalCsvHelper().ExtractBooking(accountCode,
                                                    int.Parse(xcabClientIntegrations.First().StateId), capitalCsvRows,
                                                    xcabClientIntegrations);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await Logger.Log
                                            ($"Error while extracting bookings for FTP Capital CSV-1.1 extraction, filename: {sourceFile}. Error message {ex.Message}",
                                                Name()
                                            );

                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                            continue;
                                        }


                                        if ((lstBookings != null) && (lstBookings.Count > 0) &&
                                            xcabClientIntegrations != null && xcabClientIntegrations.FirstOrDefault()
                                                .AllowConsolidation)
                                        {
                                            var successfullyInserted = true;
                                            //try to consolidate the jobs 
                                            var xCabBookingRepository = new XCabBookingRepository();

                                            var consolidatedJobs = JobConsolidation.ByToDetail2(lstBookings);
                                            if (consolidatedJobs?.ConsolidatedBookings != null &&
                                                (consolidatedJobs.ConsolidatedBookings.Count > 0))
                                            {
                                                var xCabClientReferencesRepository =
                                                    new XCabClientReferencesRepository();
                                                foreach (var bookingToCreate in consolidatedJobs.ConsolidatedBookings)
                                                {
                                                    try
                                                    {
                                                        if (bookingToCreate == null)
                                                        {
                                                            continue;
                                                        }

                                                        if (_requestValidationProvider.IsRequestValid(
                                                                bookingToCreate.AccountCode,
                                                                bookingToCreate.ServiceCode, bookingToCreate.FromSuburb,
                                                                bookingToCreate.FromPostcode, bookingToCreate.ToSuburb,
                                                                bookingToCreate.ToPostcode, bookingToCreate.ToDetail1,
                                                                bookingToCreate.FromDetail1) == false)
                                                        {
                                                            successfullyInserted = false;
                                                        }
                                                        else
                                                        {
                                                            if (comoActiveStatusForStates != null &&
                                                                comoActiveStatusForStates.Count > 0)
                                                            {
                                                                isComoActiveState =
                                                                    comoActiveStatusForStates[
                                                                        int.Parse(bookingToCreate.StateId)];
                                                            }

                                                            //perform any data truncations for references
                                                            if (!string.IsNullOrWhiteSpace(bookingToCreate.Ref1) &&
                                                                bookingToCreate.Ref1.Length > 16)
                                                            {
                                                                bookingToCreate.Ref1 =
                                                                    bookingToCreate.Ref1.Substring(0, 15);
                                                            }

                                                            if (!string.IsNullOrWhiteSpace(bookingToCreate.Ref2) &&
                                                                bookingToCreate.Ref2.Length > 16)
                                                            {
                                                                bookingToCreate.Ref2 =
                                                                    bookingToCreate.Ref2.Substring(0, 15);
                                                            }

                                                            //check if we need to queue for allocate jobs
                                                            if (bookingToCreate.PreAllocatedDriverNumber > 0)
                                                            {
                                                                bookingToCreate.IsQueued = true;
                                                            }

                                                            var idInserted = xCabBookingRepository.InsertBooking(
                                                                new XCabBooking
                                                                {
                                                                    AccountCode = bookingToCreate.AccountCode,
                                                                    FromDetail1 = bookingToCreate.FromDetail1,
                                                                    FromDetail2 = bookingToCreate.FromDetail2,
                                                                    FromDetail3 = bookingToCreate.FromDetail3,
                                                                    FromDetail4 = bookingToCreate.FromDetail4,
                                                                    FromDetail5 = bookingToCreate.FromDetail5,
                                                                    FromSuburb = bookingToCreate.FromSuburb,
                                                                    FromPostcode = bookingToCreate.FromPostcode,
                                                                    Ref1 = bookingToCreate.Ref1,
                                                                    Ref2 = bookingToCreate.Ref2,
                                                                    ToDetail1 = bookingToCreate.ToDetail1,
                                                                    ToDetail2 = bookingToCreate.ToDetail2,
                                                                    ToDetail3 = bookingToCreate.ToDetail3,
                                                                    ToDetail4 = bookingToCreate.ToDetail4,
                                                                    ToDetail5 = bookingToCreate.ToDetail5,
                                                                    ToSuburb = bookingToCreate.ToSuburb,
                                                                    ToPostcode = bookingToCreate.ToPostcode,
                                                                    LoginId = Convert.ToInt32(bookingToCreate
                                                                        .LoginDetails.Id),
                                                                    StateId = Convert.ToInt32(bookingToCreate.StateId),
                                                                    ServiceCode = bookingToCreate.ServiceCode,
                                                                    TotalItems = bookingToCreate.TotalItems,
                                                                    TotalWeight = bookingToCreate.TotalWeight,
                                                                    TotalVolume = bookingToCreate.TotalVolume,
                                                                    ExtraPuInformation =
                                                                        bookingToCreate.ExtraPuInformation,
                                                                    ExtraDelInformation =
                                                                        bookingToCreate.ExtraDelInformation,
                                                                    PreAllocatedDriverNumber =
                                                                        bookingToCreate.PreAllocatedDriverNumber
                                                                            .ToString(),
                                                                    DespatchDateTime = bookingToCreate.DespatchDateTime,
                                                                    IsQueued = bookingToCreate.IsQueued,
                                                                    Caller = bookingToCreate.Caller,
                                                                    lstItems = bookingToCreate.lstItems,
                                                                    lstContactDetail = bookingToCreate.lstContactDetail,
                                                                    UsingComo = isComoActiveState
                                                                }
                                                            );
                                                            if (idInserted > 0)
                                                            {
                                                                successfullyInserted = true;
                                                            }

                                                            if ((idInserted > 0) &&
                                                                (consolidatedJobs.GroupedJobs != null) &&
                                                                consolidatedJobs.GroupedJobs.Any())
                                                            {
                                                                successfullyInserted = true;

                                                                var allJobsForSameCustomer =
                                                                    consolidatedJobs.GroupedJobs.Where(
                                                                        group => group.Key ==
                                                                            bookingToCreate.ToDetail2);
                                                                ICollection<XCabClientReferences> references =
                                                                    (from jobs in allJobsForSameCustomer
                                                                     from job in jobs
                                                                     select new XCabClientReferences
                                                                     {
                                                                         JobDate = (job.DespatchDateTime ==
                                                                             DateTime.MinValue
                                                                                 ? DateTime.Now
                                                                                 : job.DespatchDateTime),
                                                                         Reference1 = job.Ref1,
                                                                         Reference2 = job.Ref2,
                                                                         PrimaryJobId = idInserted
                                                                     }).ToList();
                                                                xCabClientReferencesRepository.Insert(references);

                                                                if (bookingToCreate.Notification != null)
                                                                {
                                                                    var notifications = new Notification()
                                                                    {
                                                                        BookingId = idInserted,
                                                                        SMSNumber = bookingToCreate.Notification
                                                                            .SMSNumber,
                                                                        EmailAddress = bookingToCreate.Notification
                                                                            .EmailAddress
                                                                    };

                                                                    new XCabNotificationsRepository().Insert(
                                                                        notifications);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        await Logger.Log(
                                                            "Exception Occurred while writing to the db in CAPITAL-CSV-1.1 in consolidated bookngs:" +
                                                            e.Message, Name());
                                                        successfullyInserted = false;
                                                    }
                                                }

                                                //once this file has been processed move it to the processed folder
                                                if (successfullyInserted)
                                                {
                                                    MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                                }
                                                else
                                                {
                                                    MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                                }
                                            }
                                        }
                                        else if (lstBookings != null && lstBookings.Any() &&
                                                 !xcabClientIntegrations.FirstOrDefault().AllowConsolidation)
                                        {
                                            var successfullyInserted = true;
                                            //try to consolidate the jobs 
                                            var xCabBookingRepository = new XCabBookingRepository();
                                            var xCabClientReferencesRepository = new XCabClientReferencesRepository();
                                            foreach (var bookingToCreate in lstBookings)
                                            {
                                                try
                                                {
                                                    if (bookingToCreate == null)
                                                        continue;
                                                    if (_requestValidationProvider.IsRequestValid(
                                                            bookingToCreate.AccountCode, bookingToCreate.ServiceCode,
                                                            bookingToCreate.FromSuburb, bookingToCreate.FromPostcode,
                                                            bookingToCreate.ToSuburb, bookingToCreate.ToPostcode,
                                                            bookingToCreate.ToDetail1, bookingToCreate.FromDetail1) ==
                                                        false)
                                                    {
                                                        successfullyInserted = false;
                                                    }
                                                    else
                                                    {
                                                        if (!string.IsNullOrEmpty(bookingToCreate.Ref1) &&
                                                            bookingToCreate.Ref1.Length > 16)
                                                            bookingToCreate.Ref1 =
                                                                bookingToCreate.Ref1.Substring(0, 15);
                                                        if (!string.IsNullOrEmpty(bookingToCreate.Ref2) &&
                                                            bookingToCreate.Ref2.Length > 16)
                                                            bookingToCreate.Ref2 =
                                                                bookingToCreate.Ref2.Substring(0, 15);

                                                        if (bookingToCreate.PreAllocatedDriverNumber > 0)
                                                            bookingToCreate.IsQueued = true;

                                                        if (comoActiveStatusForStates != null &&
                                                            comoActiveStatusForStates.Count > 0)
                                                        {
                                                            isComoActiveState =
                                                                comoActiveStatusForStates[
                                                                    int.Parse(bookingToCreate.StateId)];
                                                        }

                                                        var xcabBooking = new XCabBooking
                                                        {
                                                            AccountCode = bookingToCreate.AccountCode,
                                                            FromDetail1 = bookingToCreate.FromDetail1,
                                                            FromDetail2 = bookingToCreate.FromDetail2,
                                                            FromDetail3 = bookingToCreate.FromDetail3,
                                                            FromDetail4 = bookingToCreate.FromDetail4,
                                                            FromDetail5 = bookingToCreate.FromDetail5,
                                                            FromSuburb = bookingToCreate.FromSuburb,
                                                            FromPostcode = bookingToCreate.FromPostcode,
                                                            Ref1 = bookingToCreate.Ref1,
                                                            Ref2 = bookingToCreate.Ref2,
                                                            ToDetail1 = bookingToCreate.ToDetail1,
                                                            ToDetail2 = bookingToCreate.ToDetail2,
                                                            ToDetail3 = bookingToCreate.ToDetail3,
                                                            ToDetail4 = bookingToCreate.ToDetail4,
                                                            ToDetail5 = bookingToCreate.ToDetail5,
                                                            ToSuburb = bookingToCreate.ToSuburb,
                                                            ToPostcode = bookingToCreate.ToPostcode,
                                                            LoginId = Convert.ToInt32(bookingToCreate.LoginDetails.Id),
                                                            StateId = Convert.ToInt32(bookingToCreate.StateId),
                                                            ServiceCode = bookingToCreate.ServiceCode,
                                                            TotalItems = bookingToCreate.TotalItems,
                                                            TotalWeight = bookingToCreate.TotalWeight,
                                                            TotalVolume = bookingToCreate.TotalVolume,
                                                            ExtraPuInformation = bookingToCreate.ExtraPuInformation,
                                                            ExtraDelInformation = bookingToCreate.ExtraDelInformation,
                                                            PreAllocatedDriverNumber = bookingToCreate
                                                                .PreAllocatedDriverNumber.ToString(),
                                                            DespatchDateTime = bookingToCreate.DespatchDateTime,
                                                            IsQueued = bookingToCreate.IsQueued,
                                                            Caller = bookingToCreate.Caller,
                                                            lstItems = bookingToCreate.lstItems,
                                                            lstContactDetail = bookingToCreate.lstContactDetail,
                                                            UsingComo = isComoActiveState
                                                        };

                                                        var idInserted =
                                                            xCabBookingRepository.InsertBooking(xcabBooking);

                                                        if (idInserted > 0)
                                                        {
                                                            successfullyInserted = true;

                                                            if (bookingToCreate.Notification != null)
                                                            {
                                                                var notifications = new Notification()
                                                                {
                                                                    BookingId = idInserted,
                                                                    SMSNumber = bookingToCreate.Notification.SMSNumber,
                                                                    EmailAddress = bookingToCreate.Notification
                                                                        .EmailAddress
                                                                };

                                                                new XCabNotificationsRepository().Insert(notifications);
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    await Logger.Log(
                                                        "Exception Occurred while writing to the db in CAPITAL-CSV-1.1 in consolidated bookngs:" +
                                                        e.Message, Name());
                                                    successfullyInserted = false;
                                                }
                                            }

                                            if (successfullyInserted)
                                            {
                                                MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                            }
                                            else
                                            {
                                                MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                            }
                                        }
                                        else
                                        {
                                            MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        await Logger.Log(
                                            $"Exception occured while proccessing the File {sourceFile} ErrorMessage: {ex.Message}",
                                            Name());
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        continue;
                                    }
                                }

                                #endregion

                                #region OFFICEWORKS

                                else if (login.BookingSchemaName.ToUpper() == "OFFICEWORKS")
                                {
                                    try
                                    {
                                        var successfullyInserted = false;
                                        var stateAbbr = _sourceFile.Replace(Path.Combine(
                                            DbSettings.Default.LocalDownloadFolder, login.UserName,
                                            login.BookingsFolderName), string.Empty).Split('_')[1].ToString().ToUpper();
                                        var storeName = _sourceFile.Replace(Path.Combine(
                                            DbSettings.Default.LocalDownloadFolder, login.UserName,
                                            login.BookingsFolderName), string.Empty).Split('_')[4].ToString().ToUpper();
                                        var fileType = _sourceFile.Replace(Path.Combine(
                                            DbSettings.Default.LocalDownloadFolder, login.UserName,
                                            login.BookingsFolderName), string.Empty).Split('_')[2].ToString().ToUpper();
                                        var jobType = _sourceFile.Replace(Path.Combine(
                                            DbSettings.Default.LocalDownloadFolder, login.UserName,
                                            login.BookingsFolderName), string.Empty).Split('_')[3].ToString().ToUpper();
#if DEBUG
                                        var fileName = Path.GetFileName(_sourceFile);
#else

                                        var fileName =
                                            _sourceFile.Replace(login.BookingsFolderName + "\\", "$").Split('$')[1]
                                                .ToString();
#endif
                                        var fileStateId = -1;
                                        switch (stateAbbr)
                                        {
                                            case "VIC":
                                                fileStateId = 1;
                                                break;
                                            case "NSW":
                                                fileStateId = 2;
                                                break;
                                            case "QLD":
                                                fileStateId = 3;
                                                break;
                                            case "SA":
                                                fileStateId = 4;
                                                break;
                                            case "WA":
                                                fileStateId = 5;
                                                break;
                                            default:
                                                break;
                                        }

                                        var loginState = AssignDefaultAccountServiceCodes(login.Id, fileStateId);
                                        if (loginState == null || string.IsNullOrEmpty(loginState.AccountCode))
                                        {
                                            await Logger.Log(
                                                "Account details not found for the Officeworks booking. loginId : " +
                                                login.Id + " State : " + fileStateId, Name());
                                            continue;
                                        }

                                        var extractedBookings =
                                            new OfficeworksTextFileHelper().ConvertTextFile(_sourceFile, stateAbbr,
                                                jobType);
                                        var previousWorkingDay = new CalculateDates()
                                            .GetPreviousWorkingDayInclusiveSaturday(DateTime.Now, 1, stateAbbr, true)
                                            .ToString("yyyy/MM/dd");
                                        if (comoActiveStatusForStates != null && comoActiveStatusForStates.Count > 0)
                                        {
                                            isComoActiveState = comoActiveStatusForStates[fileStateId];
                                        }

                                        foreach (var extractedBooking in extractedBookings)
                                        {
                                            extractedBooking.AccountCode = loginState.AccountCode;
                                            extractedBooking.StateId = fileStateId.ToString();

                                            if (loginState.AccountCode == "3OWVIC" && fileStateId == 1 &&
                                                int.Parse(extractedBooking.ToPostcode) >= 2000 &&
                                                int.Parse(extractedBooking.ToPostcode) <= 2999)
                                            {
                                                var warehouseAddress =
                                                    OfficeworksAddressesIdentifier.GetNswPickupAddress();
                                                extractedBooking.FromDetail1 = warehouseAddress.FromDetail1;
                                                extractedBooking.FromDetail2 = warehouseAddress.FromDetail2;
                                                extractedBooking.FromPostcode = warehouseAddress.FromPostcode;
                                                extractedBooking.FromSuburb = warehouseAddress.FromSuburb;
                                                extractedBooking.AccountCode = warehouseAddress.AccountCode;
                                                extractedBooking.StateId = warehouseAddress.StateId;
                                            }

                                            var existingBookings =
                                                new XCabBookingRepository().GetJobsInXCab(extractedBooking.AccountCode,
                                                    extractedBooking.Ref1, int.Parse(login.Id), previousWorkingDay);

                                            if (!string.IsNullOrWhiteSpace(extractedBooking.Ref1) &&
                                                extractedBooking.Ref1.Length > 16)
                                            {
                                                extractedBooking.Ref1 = extractedBooking.Ref1.Substring(0, 15);
                                            }

                                            if (!string.IsNullOrWhiteSpace(extractedBooking.Ref2) &&
                                                extractedBooking.Ref2.Length > 16)
                                            {
                                                extractedBooking.Ref2 = extractedBooking.Ref2.Substring(0, 15);
                                            }

                                            if (extractedBooking.PreAllocatedDriverNumber > 0)
                                            {
                                                extractedBooking.IsQueued = true;
                                            }

                                            extractedBooking.LoginDetails = login;

                                            extractedBooking.ServiceCode = jobType == "RET"
                                                ? "CPOR"
                                                : (extractedBooking.Remarks != null
                                                    ? "CPODG"
                                                    : loginState.DefaultServiceCode);

                                            if (_requestValidationProvider.IsRequestValid(extractedBooking.AccountCode,
                                                    extractedBooking.ServiceCode, extractedBooking.FromSuburb,
                                                    extractedBooking.FromPostcode, extractedBooking.ToSuburb,
                                                    extractedBooking.ToPostcode, extractedBooking.ToDetail1,
                                                    extractedBooking.FromDetail1))
                                            {
                                                if (existingBookings.ToList().Count == 0)
                                                {
                                                    var xCabBookingRepository = new XCabBookingRepository();
                                                    var idInserted = xCabBookingRepository.InsertBooking(new XCabBooking
                                                    {
                                                        AccountCode = extractedBooking.AccountCode,
                                                        FromDetail1 = extractedBooking.FromDetail1,
                                                        FromDetail2 = extractedBooking.FromDetail2,
                                                        FromDetail3 = extractedBooking.FromDetail3,
                                                        FromSuburb = extractedBooking.FromSuburb,
                                                        FromPostcode = extractedBooking.FromPostcode,
                                                        Ref1 = extractedBooking.Ref1,
                                                        Ref2 = extractedBooking.Ref2,
                                                        ToDetail1 = extractedBooking.ToDetail1,
                                                        ToDetail2 = extractedBooking.ToDetail2,
                                                        ToDetail3 = extractedBooking.ToDetail3,
                                                        ToSuburb = extractedBooking.ToSuburb,
                                                        ToPostcode = extractedBooking.ToPostcode,
                                                        LoginId = Convert.ToInt32(login.Id),
                                                        StateId = Convert.ToInt32(extractedBooking.StateId),
                                                        ServiceCode = extractedBooking.ServiceCode,
                                                        TotalItems = extractedBooking.lstItems.Count.ToString(),
                                                        TotalWeight = extractedBooking.TotalWeight,
                                                        ExtraDelInformation = extractedBooking.ExtraDelInformation,
                                                        PreAllocatedDriverNumber = extractedBooking
                                                                .PreAllocatedDriverNumber.ToString(),
                                                        DespatchDateTime = extractedBooking.DespatchDateTime,
                                                        IsQueued = extractedBooking.IsQueued,
                                                        lstItems = extractedBooking.lstItems,
                                                        lstContactDetail = extractedBooking.lstContactDetail,
                                                        ATL = extractedBooking.ATL,
                                                        OkToUpload = false,
                                                        Caller = (string.IsNullOrEmpty(extractedBooking.Caller)
                                                                ? "XCAB"
                                                                : extractedBooking.Caller),
                                                        Remarks = extractedBooking.Remarks,
                                                        UsingComo = isComoActiveState
                                                    }
                                                    );
                                                    if (idInserted > 0)
                                                    {
                                                        successfullyInserted = true;

                                                        new XCabBookingFileInformationRepository().Insert(
                                                            new XCabBookingFileInformation
                                                            {
                                                                LoginId = Convert.ToInt32(login.Id),
                                                                StateId = Convert.ToInt32(fileStateId),
                                                                BookingId = idInserted,
                                                                FileName = fileName,
                                                                StoreNameFromFile = storeName,
                                                                JobType = jobType
                                                            });

                                                        if (extractedBooking.lstExtraFields != null &&
                                                            extractedBooking.lstExtraFields.Count > 0)
                                                        {
                                                            var lstXCabExtraReferences =
                                                                new List<XCabExtraReferences>();

                                                            foreach (var extraFields in extractedBooking.lstExtraFields)
                                                            {
                                                                var xCabExtraReference = new XCabExtraReferences()
                                                                {
                                                                    PrimaryBookingId = idInserted,
                                                                    Name = extraFields.Key,
                                                                    Value = extraFields.Value
                                                                };

                                                                lstXCabExtraReferences.Add(xCabExtraReference);
                                                            }

                                                            new ExtraReferencesRepository().Insert(
                                                                lstXCabExtraReferences);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (existingBookings.Where(x => x.OkToUpload == false).ToList()
                                                            .Count > 0)
                                                    {
                                                        new XCabBookingRepository().UpdateJobdetailsAndAddNewItems(
                                                            extractedBooking, existingBookings.ToList()[0].BookingId,
                                                            previousWorkingDay);
                                                    }
                                                    else
                                                    {
                                                        new XCabBookingRepository().InsertJobdetailsAndAddNewItems(
                                                            extractedBooking, fileName, storeName, jobType,
                                                            previousWorkingDay, fileStateId);
                                                    }

                                                    successfullyInserted = true;
                                                }
                                            }
                                        }

                                        if (successfullyInserted)
                                        {
                                            var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                login.UserName,
                                                login.BookingsFolderName, DbSettings.Default.ProcessedFolderName);
                                            var destinationFile = Path.Combine(_path,
                                                _sourceFile.Substring(_sourceFile.LastIndexOf('\\') + 1));

                                            //also move the file across the ftp folder
                                            MoveAndCopyFileToProcessedFolder(login, sourceFile);

                                            try
                                            {
                                                if (jobType == "RET" && (fileType.ToUpper() == "FIN" ||
                                                                         fileType.ToUpper() == "FINAL"))
                                                {
                                                    var bookingsPending =
                                                        await new XCabBookingFileInformationRepository()
                                                            .GetXCabBookingFileInformationForStore(
                                                                Convert.ToInt32(login.Id), fileStateId, storeName,
                                                                DateTime.Now, jobType, previousWorkingDay);
                                                    if (bookingsPending.Count > 0)
                                                    {
                                                        List<int> bookingIds = bookingsPending.Select(x => x.BookingId)
                                                            .ToList();
                                                        new XCabBookingRepository().UpdateStagedBookings(bookingIds);
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception occurred while creating return bookings in Tplus. File : " +
                                                    fileName + ". Message:" + e.Message, Name());
                                            }
                                        }
                                        else
                                        {
                                            var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                login.UserName,
                                                login.BookingsFolderName + "\\" + DbSettings.Default.ErrorFolderName);
                                            var destinationFile = Path.Combine(_path,
                                                _sourceFile.Substring(_sourceFile.LastIndexOf('\\') + 1));

                                            //also move the file across the ftp folder
                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        await Logger.Log(
                                            "Exception occured while proccessing the officeworks File " + sourceFile +
                                            " jobs inthe file are not processed. ErrorMessage:" +
                                            e.Message, Name());

                                        var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                            login.UserName,
                                            login.BookingsFolderName + "\\" + DbSettings.Default.ErrorFolderName);
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        continue;
                                    }
                                }

                                #endregion

                                #region Whites Group Xml

                                else if (login.BookingSchemaName.ToUpper() == "WHITESGROUP-XML")
                                {
                                    xmlshredder.whitesgroup.Message request;
                                    var originalSourceFile = sourceFile;
                                    try
                                    {
                                        XmlSerializer mySerializer =
                                            new(typeof(xmlshredder.whitesgroup.Message));
                                        FileStream myFileStream =
                                            new FileStream(sourceFile, FileMode.Open);
                                        // Call the Deserialize method and cast to the object type.  
                                        request = (xmlshredder.whitesgroup.Message)
                                            mySerializer.Deserialize(myFileStream);
                                        myFileStream.Close();
                                    }
                                    catch (Exception e)
                                    {
                                        await Logger.Log(
                                            "Parsing Error: Could not parse the received file. File=" +
                                            sourceFile +
                                            ", This file will be moved into the Errors Folder and will not be created as a booking in TPLUS. Exception Details:" +
                                            e.Message, Name(), true);

                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        continue;
                                    }

                                    try
                                    {
                                        var successfullyInserted = true;
                                        var consignment = request.MessageBody.Consignments.Consignment;
                                        var booking = new Booking();
                                        //booking.AccountCode = consig.CodeSender;
                                        if (consignment.Party != null && consignment.Party.Count > 0)
                                        {
                                            booking.AccountCode = consignment.Party[0].Code;
                                        }

                                        if (request.MessageHeader != null && request.MessageHeader.RecipientID != null)
                                        {
                                            booking.ServiceCode = request.MessageHeader.RecipientID;
                                        }

                                        booking.LoginDetails = new LoginDetails
                                        {
                                            Id = login.Id,
                                            UserName = login.UserName,
                                            BookingsFolderName = login.BookingsFolderName,
                                            ProcessedFolderName = login.ProcessedFolderName,
                                            TrackingSchemaName = login.TrackingSchemaName,
                                            ErrorFolderName = login.ErrorFolderName
                                        };
                                        try
                                        {
                                            booking.Ref1 = consignment.Reference1;
                                            booking.Ref2 = consignment.Reference2;
                                        }
                                        catch (Exception e)
                                        {
                                            await Logger.Log(
                                                "Exception Occurred while extracting references for Whites Group, Exception Message:" +
                                                e.Message, Name());
                                        }

                                        if (consignment.Party != null)
                                        {
                                            var sender =
                                                consignment.Party.Where(x => x.Role.ToUpper().Equals("SENDER"));
                                            var fromLine1 = sender.Select(x => x.Name).FirstOrDefault();
                                            if (!string.IsNullOrEmpty(fromLine1))
                                            {
                                                booking.FromDetail1 = fromLine1.Trim();
                                            }

                                            var fromLine2 = sender.Select(x => x.Address1).FirstOrDefault();
                                            if (!string.IsNullOrEmpty(fromLine2))
                                            {
                                                booking.FromDetail2 = fromLine2;
                                            }

                                            var fromSub = sender.Select(x => x.Suburb).FirstOrDefault();
                                            if (!string.IsNullOrEmpty(fromSub))
                                            {
                                                booking.FromSuburb = fromSub;
                                            }

                                            var state = sender.Select(x => x.State).FirstOrDefault();
                                            switch (state.Trim().ToUpper())
                                            {
                                                case "VIC":
                                                    booking.StateId = "1";
                                                    break;
                                                case "NSW":
                                                    booking.StateId = "2";
                                                    break;
                                                case "WA":
                                                    booking.StateId = "5";
                                                    break;
                                                case "QLD":
                                                    booking.StateId = "3";
                                                    break;
                                                case "SA":
                                                    booking.StateId = "4";
                                                    break;
                                                default:
                                                    throw new Exception("Unexpected Case");
                                            }

                                            var fromPostcode = sender.Select(x => x.PostCode).FirstOrDefault();
                                            if (!string.IsNullOrEmpty(fromPostcode))
                                            {
                                                booking.FromPostcode = fromPostcode;
                                            }

                                            var receiver = consignment.Party.Where(x =>
                                                x.Role.ToUpper().Equals("RECEIVER"));
                                            var ToLine1 = receiver.Select(x => x.Name).FirstOrDefault();
                                            if (!string.IsNullOrEmpty(ToLine1))
                                            {
                                                booking.ToDetail1 = ToLine1;
                                            }

                                            var ToLine2 = receiver.Select(x => x.Address1).FirstOrDefault();
                                            if (!string.IsNullOrEmpty(ToLine2))
                                            {
                                                booking.ToDetail2 = ToLine2;
                                            }

                                            var ToSub = receiver.Select(x => x.Suburb).FirstOrDefault();
                                            if (!string.IsNullOrEmpty(ToSub))
                                            {
                                                booking.ToSuburb = ToSub;
                                            }

                                            var ToPostcode = receiver.Select(x => x.PostCode).FirstOrDefault();
                                            if (!string.IsNullOrEmpty(ToPostcode))
                                            {
                                                booking.ToPostcode = ToPostcode;
                                            }
                                        }

                                        var pieces = consignment.Total.Where(x => x.Units.ToUpper().Equals("ITEMS"))
                                            .Select(x => x.Value).FirstOrDefault();
                                        var cubic = consignment.Line.Measurement
                                            .Where(x => x.Property.ToUpper().Equals("CUBIC")).Select(x => x.Value)
                                            .FirstOrDefault();
                                        var weight = consignment.Line.Measurement
                                            .Where(x => x.Property.ToUpper().Equals("WEIGHT")).Select(x => x.Value)
                                            .FirstOrDefault();

                                        var length = consignment.Line.Dimensions != null
                                            ? consignment.Line.Dimensions.Length
                                            : 0;
                                        var height = consignment.Line.Dimensions != null
                                            ? consignment.Line.Dimensions.Height
                                            : 0;
                                        var width = consignment.Line.Dimensions != null
                                            ? consignment.Line.Dimensions.Width
                                            : 0;
                                        var quantity = consignment.Line.Dimensions != null
                                            ? consignment.Line.Dimensions.Quantity
                                            : 0;
                                        var item = new Item
                                        {
                                            Weight = Convert.ToDouble(weight),
                                            Quantity = Convert.ToInt32(pieces),
                                            Cubic = Convert.ToDouble(cubic),
                                            Length = Convert.ToDouble(length),
                                            Height = Convert.ToDouble(height),
                                            Width = Convert.ToDouble(width)
                                        };
                                        booking.lstItems = new List<Item>
                                        {
                                            item
                                        };
                                        if (comoActiveStatusForStates != null && comoActiveStatusForStates.Count > 0)
                                        {
                                            isComoActiveState = comoActiveStatusForStates[int.Parse(booking.StateId)];
                                            booking.UsingComo = isComoActiveState;
                                        }

                                        try
                                        {
                                            if (booking != null)
                                            {
                                                var dbConnector = new DbConnector();
                                                successfullyInserted = dbConnector.StoreToDb(booking);
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            await Logger.Log(
                                                "Exception Occurred while writing to the db for Whites Group:" +
                                                e.Message, Name());
                                            successfullyInserted = false;
                                        }

                                        //once this file has been processed move it to the processed folder
                                        if (successfullyInserted)
                                        {
                                            var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                login.UserName,
                                                login.BookingsFolderName, DbSettings.Default.ProcessedFolderName);
                                            var destinationFile = Path.Combine(_path,
                                                _sourceFile.Substring(_sourceFile.LastIndexOf('\\') + 1));
                                            //also move the file across the ftp folder
                                            try
                                            {
                                                FtpProcessor.UploadFile(ftpUrl, sourceFile, ftpUserName, ftpPassword,
                                                    login.BookingsFolderName + "\\" + login.ProcessedFolderName,
                                                    true);
                                                File.Move(sourceFile, destinationFile + "." + GetDateTimeForFile());
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception Occurred in Whites Group Extraction:" + e.Message,
                                                    Name());
                                            }
                                        }
                                        else
                                        {
                                            //there has been an error - so we move to the Error Folder
                                            var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                                login.UserName,
                                                login.BookingsFolderName + "\\" + login.ErrorFolderName);
                                            var destinationFile = Path.Combine(_path,
                                                _sourceFile.Substring(_sourceFile.LastIndexOf('\\') + 1));

                                            //also move the file across the ftp folder
                                            try
                                            {
                                                FtpProcessor.UploadFile(ftpUrl, sourceFile, ftpUserName, ftpPassword,
                                                    login.BookingsFolderName + "\\" + login.ErrorFolderName,
                                                    true);
                                                File.Move(sourceFile, destinationFile + "." + GetDateTimeForFile());
                                            }
                                            catch (Exception e)
                                            {
                                                await Logger.Log(
                                                    "Exception Occurred while moving the ftp files:" + e.Message,
                                                    Name());
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                        await Logger.Log(
                                            "Exception Occurred while extracting Whites Group XML files, details:" +
                                            e.Message,
                                            Name());
                                        continue;
                                    }
                                }

                                #endregion

                                #region Ikea Xml

                                else if (login.BookingSchemaName.ToUpper() == "IKEA")
                                {
                                    try
                                    {
                                        _ = int.TryParse(login.Id, out var loginId);
                                        var successfullyProcessed = false;
                                        var extractedBookings = await _ikeaService.ParseExportXml(sourceFile, loginId);

                                        if (extractedBookings?.Count >= 1)
                                        {
                                            if ((comoActiveStatusForStates?.Count ?? 0) > 0)
                                                isComoActiveState =
                                                    comoActiveStatusForStates[extractedBookings[0].Booking.StateId];
                                            extractedBookings[0].Booking.UsingComo = isComoActiveState;

                                            successfullyProcessed = await _ikeaService.ProcessBookings(extractedBookings);
                                        }

                                        if (successfullyProcessed)
                                            MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                        else
                                            MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                    }
                                    catch (Exception e)
                                    {
                                        RollingLogger.WriteToShredderProcessorLogs($"Execute Method - Exception occurred when extracting bookings for Ikea, details:{e.Message}", ELogTypes.Error);
                                        await Logger.Log($"{GetType().Name}: Execute Method - Exception occurred when extracting bookings for Ikea, details:{e.Message}", Name());
                                        //as this is a critical error, we will move the file to the errors folder
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                    }
                                }

                                #endregion

                                #region SIGMA

                                else if (login.BookingSchemaName.ToUpper() == "SIGMA")
                                {
                                    var extractedBookings = new SigmaFileHelper().ConvertTextFile(_sourceFile);
                                    var successfullyInserted = false;
                                    if (extractedBookings != null && extractedBookings.Count > 0)
                                    {
                                        foreach (var extractedBooking in extractedBookings)
                                        {
                                            try
                                            {
                                                var xCabBookingRepository = new XCabBookingRepository();
                                                var xCabBookingRoute = new XCabBookingRoute();
                                                var stateId = Convert.ToInt32(extractedBooking.StateId == "9"
                                                    ? "3"
                                                    : extractedBooking.StateId);
                                                if (!string.IsNullOrWhiteSpace(extractedBooking.Ref2) &&
                                                    extractedBooking.Ref2.Length >= 8)
                                                {
                                                    xCabBookingRoute.Route = extractedBooking.Ref2.Substring(0, 4);
                                                    xCabBookingRoute.DropSequence =
                                                        extractedBooking.Ref2.Substring(
                                                            extractedBooking.Ref2.Length - 4, 4);
                                                }

                                                if (comoActiveStatusForStates != null &&
                                                    comoActiveStatusForStates.Count > 0)
                                                {
                                                    isComoActiveState = comoActiveStatusForStates[stateId];
                                                }

                                                if (!string.IsNullOrEmpty(extractedBooking.ServiceCode) &&
                                                    extractedBooking.ServiceCode.ToUpper() == "CPOD" &&
                                                    !string.IsNullOrEmpty(extractedBooking.AccountCode) &&
                                                    !string.IsNullOrEmpty(extractedBooking.Ref1) &&
                                                    !string.IsNullOrEmpty(extractedBooking.StateId) &&
                                                    !string.IsNullOrEmpty(login.Id))
                                                {
                                                    var trackAndTraceEmailAddress =
                                                        await _xCabEmailRecipientsProvider.GetStoreEmailAddress(
                                                            extractedBooking.Ref1, Convert.ToInt32(login.Id),
                                                            extractedBooking.StateId, extractedBooking.AccountCode);
                                                    if (!string.IsNullOrEmpty(trackAndTraceEmailAddress))
                                                    {
                                                        var notification = new Notification
                                                        {
                                                            EmailAddress = trackAndTraceEmailAddress.Trim()
                                                                .Replace(" ", "")
                                                        };

                                                        extractedBooking.Notification = notification;
                                                    }
                                                }

                                                var idInserted = await _xCabDatabaseProvider.InsertXCabBooking(
                                                    new XCabBooking
                                                    {
                                                        AccountCode = extractedBooking.AccountCode,
                                                        FromDetail1 = extractedBooking.FromDetail1,
                                                        FromDetail2 = extractedBooking.FromDetail2,
                                                        FromDetail3 = extractedBooking.FromDetail3,
                                                        FromSuburb = extractedBooking.FromSuburb,
                                                        FromPostcode = extractedBooking.FromPostcode,
                                                        Ref1 = extractedBooking.Ref1,
                                                        Ref2 = extractedBooking.Ref2,
                                                        ToDetail1 = extractedBooking.ToDetail1,
                                                        ToDetail2 = extractedBooking.ToDetail2,
                                                        ToDetail3 = extractedBooking.ToDetail3,
                                                        ToSuburb = extractedBooking.ToSuburb,
                                                        ToPostcode = extractedBooking.ToPostcode,
                                                        LoginId = Convert.ToInt32(login.Id),
                                                        StateId = stateId,
                                                        ServiceCode = extractedBooking.ServiceCode,
                                                        TotalItems = extractedBooking.lstItems.Count.ToString(),
                                                        TotalWeight = extractedBooking.TotalWeight,
                                                        ExtraDelInformation = extractedBooking.ExtraDelInformation,
                                                        PreAllocatedDriverNumber =
                                                            extractedBooking.PreAllocatedDriverNumber.ToString(),
                                                        DespatchDateTime = extractedBooking.DespatchDateTime,
                                                        IsQueued = extractedBooking.IsQueued,
                                                        lstItems = extractedBooking.lstItems.GroupBy(x => x.Barcode)
                                                            .Select(B => B.First()).ToList(),
                                                        lstContactDetail = extractedBooking.lstContactDetail,
                                                        ATL = extractedBooking.ATL,
                                                        OkToUpload = false,
                                                        Caller = (string.IsNullOrEmpty(extractedBooking.Caller)
                                                            ? "XCAB"
                                                            : extractedBooking.Caller),
                                                        Remarks = extractedBooking.Remarks,
                                                        IsNtJob = extractedBooking.StateId == "9" ? true : false,
                                                        XCabBookingRoute = xCabBookingRoute ?? null,
                                                        UsingComo = isComoActiveState,
                                                        Notification = extractedBooking.Notification
                                                    }
                                                );
                                                if (idInserted > 0)
                                                {
                                                    successfullyInserted = true;
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                successfullyInserted = false;
                                                await Logger.Log(
                                                    $"Error occurred while inserting the extracted Sigma bookings to XCab database. File name= {sourceFile}.ErrorMessage: {e.Message}",
                                                    Name());
                                                RollingLogger.WriteToShredderProcessorLogs(
                                                    $"Execute Method - Error occurred while inserting the extracted Sigma bookings to XCab database. File name= {sourceFile}.ErrorMessage: {e.Message}. File moving to errors folder.",
                                                    ELogTypes.Error);
                                            }
                                        }
                                    }

                                    if (successfullyInserted)
                                    {
                                        MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                    }
                                    else
                                    {
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                    }
                                }

                                #endregion

                                #region Ignore

                                else if (login.BookingSchemaName.ToUpper() == "IGNORE-BOOKINGS")
                                {
                                    //ignore bookings under this schema
                                    var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder,
                                        login.UserName,
                                        login.BookingsFolderName, DbSettings.Default.ProcessedFolderName);
                                    var destinationFile = Path.Combine(_path,
                                        _sourceFile.Substring(_sourceFile.LastIndexOf('\\') + 1));

                                    //also move the file across the ftp folder
                                    MoveAndCopyFileToProcessedFolder(login, sourceFile);
                                }

                                #endregion

                                #region For Non-Supported Mappings

                                else
                                {
                                    //this is the case when the file failed to be parsed 
                                    try
                                    {
                                        var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName,
                                            login.BookingsFolderName + "\\" + DbSettings.Default.ErrorFolderName);
                                        MoveAndCopyFileToErrorsFolder(login, sourceFile);
                                    }
                                    catch (Exception e)
                                    {
                                        await Logger.Log(
                                            "Exception Occurred while moving errored files to the FTP folder, Exception Message:" +
                                            e.Message, Name());
                                    }
                                }

                                #endregion
                            }
                        }
                        catch (Exception e)
                        {
                            RollingLogger.WriteToShredderProcessorLogs(
                                $"Execute Method - Error occurred for Integration: {login}, details:{e.Message}",
                                ELogTypes.Information);
                            await Logger.Log(
                                $"{GetType().Name} Execute Method - Error occurred for Integration: {login}, details:{e.Message}",
                                Name());
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Names this instance.
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "XmlShredder";
        }

        #endregion


        /// <summary>
        ///     Gets the date time for file.
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeForFile()
        {
            var uploadTime = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" +
                             DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" +
                             DateTime.Now.Minute + "_" + DateTime.Now.Millisecond;
            return uploadTime;
        }

        #endregion

        #region Private Members

        private async void MoveAndCopyFileToErrorsFolder(LoginDetails login, string fileName)
        {
            if (File.Exists(fileName))
            {
                var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName,
                    login.BookingsFolderName + "\\" + DbSettings.Default.ErrorFolderName);
                try
                {
                    var destinationFile = Path.Combine(nfsUrl, login.UserName, login.BookingsFolderName,
                        DbSettings.Default.ErrorFolderName, fileName.Substring(fileName.LastIndexOf('\\') + 1));
                    var isCopySuccessful = _nfsFileService.CopyFileWithTimestamp(sourceFile, destinationFile);
                    if (VerboseLoggingEnabled)
                        RollingLogger.WriteToShredderProcessorLogs(
                            $"Execute Method - Move and copy file to Errors folder: CopyFileWithTimestamp.Username:{login.UserName}, DestinationFile: {destinationFile}, IsCopySuccessful: {isCopySuccessful}",
                            ELogTypes.Information);
                    if (!isCopySuccessful)
                    {
                        await Logger.Log(
                            $"Exception occurred when copying the file to remote error folder. Source:{sourceFile}, destination:{destinationFile}",
                            Name());
                    }

                    destinationFile = Path.Combine(_path, fileName.Substring(fileName.LastIndexOf('\\') + 1));
                    var isMoveSuccessful = _nfsFileService.MoveFileWithTimestamp(sourceFile, destinationFile);
                    if (VerboseLoggingEnabled)
                        RollingLogger.WriteToShredderProcessorLogs(
                            $"Execute Method - Move and copy file to Errors folder : MoveFileWithTimestamp. Username:{login.UserName}, DestinationFile:{destinationFile}, IsCopySuccessful:{isCopySuccessful}",
                            ELogTypes.Information);
                    if (!isMoveSuccessful)
                    {
                        await Logger.Log(
                            $"Exception occurred when moving the file to local error folder. Source:{sourceFile}, destination:{destinationFile}",
                            Name());
                    }
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        $"Exception Occurred in MoveFileToErrorsFolder. FileName:{fileName}, Login:{login}. Error: {e.Message}",
                        Name());
                }
            }
        }

        private async void MoveAndCopyFileToProcessedFolder(LoginDetails login, string fileName)
        {
            if (File.Exists(fileName))
            {
                var _path = Path.Combine(DbSettings.Default.LocalDownloadFolder, login.UserName,
                    login.BookingsFolderName, DbSettings.Default.ProcessedFolderName);
                try
                {
                    var destinationFile = Path.Combine(nfsUrl, login.UserName, login.BookingsFolderName,
                        DbSettings.Default.ProcessedFolderName, fileName.Substring(fileName.LastIndexOf('\\') + 1));
                    var isCopySuccessful = _nfsFileService.CopyFileWithTimestamp(sourceFile, destinationFile);
                    if (VerboseLoggingEnabled)
                        RollingLogger.WriteToShredderProcessorLogs(
                            $"Execute Method - Move and copy file to processed folder : CopyFileWithTimestamp. Username:{login.UserName}, DestinationFile:{destinationFile}, IsCopySuccessful:{isCopySuccessful}",
                            ELogTypes.Information);
                    if (!isCopySuccessful)
                    {
                        await Logger.Log(
                            $"Exception occurred when copying the file to remote processed folder. Source:{sourceFile}, destination:{destinationFile}",
                            Name());
                    }

                    destinationFile = Path.Combine(_path, fileName.Substring(fileName.LastIndexOf('\\') + 1));
                    var isMoveSuccessful = _nfsFileService.MoveFileWithTimestamp(sourceFile, destinationFile);
                    if (VerboseLoggingEnabled)
                        RollingLogger.WriteToShredderProcessorLogs(
                            $"Execute Method - Move and copy file to Processed folder : MoveFileWithTimestamp. Username:{login.UserName}, DestinationFile:{destinationFile}, IsCopySuccessful:{isCopySuccessful}",
                            ELogTypes.Information);
                    if (!isMoveSuccessful)
                    {
                        await Logger.Log(
                            $"Exception occurred when moving the file to local processed folder. Source:{sourceFile}, destination:{destinationFile}",
                            Name());
                    }
                }
                catch (Exception e)
                {
                    await Logger.Log(
                        $"Exception Occurred in MoveFileToProcessedFolder. FileName:{fileName}, Login:{login}. Error: {e.Message}",
                        Name());
                }
            }
        }

        private LoginState AssignDefaultAccountServiceCodes(string loginId, int stateId)
        {
            var loginState = new LoginState();
            try
            {
                var sql = "SELECT TOP 1 AccountCode, DefaultServiceCode FROM xCabLoginState WHERE LoginID='" +
                          loginId + "' AND StateId=" + stateId;


                using (var connection = new SqlConnection(DbSettings.Default.ApplicationSqlDatabaseConnectionString))
                {
                    connection.Open();
                    var lstLoginState = (List<LoginState>)connection.Query<LoginState>(sql);
                    //there has to be only one row returned
                    if ((lstLoginState != null) && (lstLoginState.Count > 0) &&
                        (lstLoginState[0].AccountCode.Length > 0) &&
                        (lstLoginState[0].DefaultServiceCode.Length > 0))
                    {
                        loginState.AccountCode = lstLoginState[0].AccountCode;
                        loginState.DefaultServiceCode = lstLoginState[0].DefaultServiceCode;
                    }
                    else
                    {
                        loginState.DefaultServiceCode = "CPOD";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return loginState;
        }

        public XCabEmailAlerts GetXCabEmailAlert(int LoginId, int StateId)
        {
            var xcabEmailAlertRepository = new XCabEmailAlertsRepository();
            XCabEmailAlerts xCabEmailAlert = xcabEmailAlertRepository.GetXCabEmailAlert(LoginId, StateId);
            return xCabEmailAlert;
        }

        public void SendEmailAlert(string MessageText, string Title, string emailAddress,
            bool ccSoftwareDevteam = false, string fileName = "")
        {
            var cc = string.Empty;
            if (ccSoftwareDevteam)
            {
                cc = "software-development@capitaltransport.com.au";
            }

            if (fileName.Any())
            {
                SendMail.SendHtmlEMailWithAttachment(MessageText, fileName, Title, emailAddress, cc);
            }
            else
            {
                Logger.SendHtmlEmailNotification(MessageText, Title, emailAddress, cc);
            }
        }

        public async void Alert(string Message, string Title, int LoginId, int StateId, bool ccSoftwareDevteam = false,
            string filename = "")
        {
            try
            {
                if (Title != null && Title != DatabaseInsertFailesMessageTitle)
                {
#if DEBUG
                    ccSoftwareDevteam = true;
#else
                    ccSoftwareDevteam = false;
#endif
                }

                var xCabEmailAlert = GetXCabEmailAlert(LoginId, StateId);
                if (xCabEmailAlert != null)
                {
                    SendEmailAlert(Message, string.Concat(xCabEmailAlert.ClientName, " - ", Title),
                        xCabEmailAlert.EmailAddress, ccSoftwareDevteam, filename);
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred while sending email alert for failed FTP Parsing, details:" +
                    e.Message + ", LogiId=" + LoginId + ",StateId=" + StateId + ",Message=" + Message,
                    Name());
            }
        }

        public async void AlertZeroByteFile(string Message, string Title, int LoginId, int StateId,
            bool ccSoftwareDevteam = false, string filename = "")
        {
            try
            {
                if (StateId == -1)
                {
#if DEBUG
                    Logger.SendHtmlEmailNotification(Message, string.Concat("EzyStrut ", " - ", Title), "asinha@capitaltransport.com.au", "software-development@capitaltransport.com.au");
#else
                    Logger.SendHtmlEmailNotification(Message, string.Concat("EzyStrut ", " - ", Title),
                        "contractssyd@capitaltransport.com.au");
#endif
                }
            }
            catch (Exception e)
            {
                await Logger.Log(
                    "Exception Occurred while sending email alert for failed FTP Parsing, details:" +
                    e.Message + ", LogiId=" + LoginId + ",StateId=" + StateId + ",Message=" + Message,
                    Name());
            }
        }

        #endregion
    }
}