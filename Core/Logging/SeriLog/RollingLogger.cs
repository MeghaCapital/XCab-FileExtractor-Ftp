using Core.Models.Slack;
using Serilog;

namespace Core.Logging.SeriLog
{
	public class RollingLogger
	{
		private const string logFolderPath = @"c:\Temp\XCab\Logs\";

#if DEBUG
		private const string liveBookingApiLogFolderPath = @"c:\Logs\BookingApi\Live\";
		private const string testBookingApiLogFolderPath = @"c:\Logs\BookingApi\Test\";
		private const string liveQuoteApiLogFolderPath = @"c:\Logs\QuoteApi\Live\";
		private const string testQuoteApiLogFolderPath = @"c:\Logs\QuoteApi\Test\";
		private const string liveServiceQuoteApiLogFolderPath = @"c:\Logs\ServiceQuoteApi\Live\";
		private const string testServiceQuoteApiLogFolderPath = @"c:\Logs\ServiceQuoteApi\Test\";
		private const string liveConsolidatedBookingApiLogFolderPath = @"c:\Logs\ConsolidatedBookingApi\Live\";
		private const string liveRouteApiLogFolderPath = @"c:\Logs\RouteApi\Live\";
		private const string testRouteApiLogFolderPath = @"c:\Logs\RouteApi\Test\";
		private const string liveTrackingStatusApiLogFolderPath = @"c:\Logs\TrackingApi\Live\";
		private const string liveTrackAndTraceApiLogFolderPath = @"c:\Logs\TrackingApi\Live\";
		private const string liveDriverLocationApiLogFolderPath = @"c:\Logs\TrackingApi\Live\";
		private const string testTrackingStatusApiLogFolderPath = @"c:\Logs\TrackingApi\Test\";
		private const string testDriverLocationApiLogFolderPath = @"c:\Logs\TrackingApi\Test\";
		private const string toshibaTrackingLogFolderPath = @"c:\Logs\ToshibaTracking\";
		private const string liveAsnApiLogFolderPath = @"c:\Logs\AsnApi\Live\";
		private const string testAsnApiLogFolderPath = @"c:\Logs\AsnApi\Test\";
		private const string liveBasfConsignmentApiLogFolderPath = @"c:\Logs\BasfConsignmentApi\Live\";
		private const string testBasfConsignmentApiLogFolderPath = @"c:\Logs\BasfConsignmentApi\Test\";
		private const string barcodesForRouteApiLogFolderPath = @"c:\Logs\BarcodesForRouteApi\";
		private const string jobTrackerLogFolderPath = @"c:\Logs\JobTracker\";
		private const string wooCommerceOrderExtractorLogFolderPath = @"c:\Logs\ECommerceApiScheduler\";
		private const string xCabBookingFileExtractorLogFolderPath = @"c:\Logs\XCabBookingFileExtractor\";
		private const string fileDownloaderLogFolderPath = @"c:\Logs\XCab-Service\Live\";
		private const string activateBookingsApiLogFolderPath = @"c:\Logs\ActivateBookingsApi\";
		private const string ikeaTrackingFileCreatorLogFolderPath = @"c:\Logs\JobTracker\Ikea\";
#else
		private const string liveBookingApiLogFolderPath = @"d:\Logs\BookingApi\Live\";
        private const string testBookingApiLogFolderPath = @"d:\Logs\BookingApi\Test\";
        private const string liveQuoteApiLogFolderPath = @"d:\Logs\QuoteApi\Live\";
        private const string testQuoteApiLogFolderPath = @"d:\Logs\QuoteApi\Test\";
        private const string liveServiceQuoteApiLogFolderPath = @"d:\Logs\ServiceQuoteApi\Live\";
        private const string testServiceQuoteApiLogFolderPath = @"d:\Logs\ServiceQuoteApi\Test\";
        private const string liveConsolidatedBookingApiLogFolderPath = @"d:\Logs\ConsolidatedBookingApi\Live\";
        private const string liveRouteApiLogFolderPath = @"d:\Logs\RouteApi\Live\";
		private const string testRouteApiLogFolderPath = @"d:\Logs\RouteApi\Test\";
		private const string liveTrackingStatusApiLogFolderPath = @"d:\Logs\TrackingStatusApi\Live\";
		private const string liveTrackAndTraceApiLogFolderPath = @"d:\Logs\TrackingApi\Live\";
		private const string liveDriverLocationApiLogFolderPath = @"d:\Logs\TrackingApi\Live\";
		private const string testTrackingStatusApiLogFolderPath = @"d:\Logs\TrackingApi\Test\";
		private const string testDriverLocationApiLogFolderPath = @"d:\Logs\TrackingApi\Test\";
		private const string toshibaTrackingLogFolderPath = @"d:\Logs\ToshibaTracking\";
		private const string liveAsnApiLogFolderPath = @"d:\Logs\AsnApi\Live\";
		private const string testAsnApiLogFolderPath = @"d:\Logs\AsnApi\Test\";
		private const string liveBasfConsignmentApiLogFolderPath = @"d:\Logs\BasfConsignmentApi\Live\";
		private const string testBasfConsignmentApiLogFolderPath = @"d:\Logs\BasfConsignmentApi\Test\";
		private const string barcodesForRouteApiLogFolderPath = @"d:\Logs\BarcodesForRouteApi\";
		private const string jobTrackerLogFolderPath = @"d:\Logs\XCab-service\JobTracker\";
		private const string wooCommerceOrderExtractorLogFolderPath = @"d:\Logs\ECommerceApiScheduler\";
		private const string xCabBookingFileExtractorLogFolderPath = @"d:\Logs\XCabBookingFileExtractor\";
		private const string fileDownloaderLogFolderPath = @"d:\Logs\XCab-Service\Live\";
		private const string activateBookingsApiLogFolderPath = @"d:\Logs\ActivateBookingsApi\";
		private const string ikeaTrackingFileCreatorLogFolderPath = @"d:\Logs\JobTracker\Ikea\";
#endif

		private const string webCLientLogFileName = logFolderPath + "webclient-log.txt";
		private const string csrLoggingFile = logFolderPath + "csr-log.txt";
		private const string xCabEventMaintenanceServiceLoggingFile = logFolderPath + "XCabEventMaintenanceService-log.txt";
		private const string jobCreatorServiceLoggingFile = logFolderPath + "JobCreatorService-log.txt";
		private const string asnJobReleaseServiceLoggingFile = logFolderPath + "AsnJobReleaseService-log.txt";
		private const string soapRequestoreLoggingFile = logFolderPath + "SoapRequestor-log.txt";
		private const string genericWebhookClientsLoggingFile = logFolderPath + "webhookclients-log.txt";
		private const string liveBookingApiLogFileName = liveBookingApiLogFolderPath + @"Booking-log.txt";
		private const string testBookingApiLogFileName = testBookingApiLogFolderPath + @"Booking-log.txt";
		private const string liveCreateWithLabelApiLogFileName = liveBookingApiLogFolderPath + @"CreateWithLabel-log.txt";
		private const string testCreateWithLabelApiLogFileName = testBookingApiLogFolderPath + @"CreateWithLabel-log.txt";
		private const string liveDGBookingApiLogFileName = liveBookingApiLogFolderPath + @"DGBooking-log.txt";
		private const string testDGBookingApiLogFileName = testBookingApiLogFolderPath + @"DGBooking-log.txt";
		private const string liveQuoteApiLogFileName = liveQuoteApiLogFolderPath + @"Quote-log.txt";
		private const string testQuoteApiLogFileName = testQuoteApiLogFolderPath + @"Quote-log.txt";
		private const string liveServiceQuoteApiLogFileName = liveServiceQuoteApiLogFolderPath + @"ServiceQuote-log.txt";
		private const string testServiceQuoteApiLogFileName = testServiceQuoteApiLogFolderPath + @"ServiceQuote-log.txt";
		private const string liveConsolidatedBookingApiLogFileName = liveConsolidatedBookingApiLogFolderPath + @"ConsolidatedBooking-log.txt";
		private const string liveRouteApiLogFileName = liveRouteApiLogFolderPath + @"RouteStagedBookings-log.txt";
		private const string testRouteApiLogFileName = testRouteApiLogFolderPath + @"RouteStagedBookings-log .txt";
		private const string liveTrackingStatusApiLogFileName = liveTrackingStatusApiLogFolderPath + @"TrackingStatus-log.txt";
		private const string liveTrackAndTraceApiLogFileName = liveTrackAndTraceApiLogFolderPath + @"TrackAndTrace-log.txt";
		private const string liveDriverLocationApiLogFileName = liveDriverLocationApiLogFolderPath + @"DriverLocation-log.txt";
		private const string testTrackingStatusApiLogFileName = testTrackingStatusApiLogFolderPath + @"TrackingStatus-log.txt";
		private const string testDriverLocationApiLogFileName = testDriverLocationApiLogFolderPath + @"DriverLocation-log.txt";
		private const string toshibaTrackingLogFileName = toshibaTrackingLogFolderPath + @"ToshibaTracking-log.txt";
		private const string liveAsnApiLogFileName = liveAsnApiLogFolderPath + @"Asn-log.txt";
		private const string testAsnApiLogFileName = testAsnApiLogFolderPath + @"Asn-log.txt";
		private const string liveBasfConsignmentApiLogFileName = liveBasfConsignmentApiLogFolderPath + @"BasfConsignment-log.txt";
		private const string testBasfConsignmentApiLogFileName = testBasfConsignmentApiLogFolderPath + @"BasfConsignment-log.txt";
		private const string barcodesForRouteApiLogFileName = barcodesForRouteApiLogFolderPath + @"BarcodesForRouteApi-log.txt";
		private const string trackingViaEmailLogFileName = jobTrackerLogFolderPath + @"TrackingViaEmail-log.txt";
		private const string wooCommerceOrderExtractorLogFileName = wooCommerceOrderExtractorLogFolderPath + @"WooCommerce-log.txt";
		private const string rabbitMqConsumerLogFileName = jobTrackerLogFolderPath + @"FTPTracking-log.txt";
		private const string nfsBookingFileExtractorLogFileName = xCabBookingFileExtractorLogFolderPath + @"NfsBookingExtractor-log.txt";
		private const string shredderProcessorLogFileName = xCabBookingFileExtractorLogFolderPath + @"ShredderProcessor-log.txt";
		private const string fileDownloaderLogFileName = fileDownloaderLogFolderPath + @"FileDownloader-log.txt";
		private const string activateBookingsApiLogFileName = activateBookingsApiLogFolderPath + @"ActivateBookingsApi-log.txt";
        private const string rabbitMqPublisherLogFileName = jobTrackerLogFolderPath + @"FTPTracking_RabbitMqPublisher-log.txt";
        private const string rabbitMqConsumerTrackingLogFileName = jobTrackerLogFolderPath + @"FTPTracking-_RabbitMqConsumerlog.txt";
		private const string ikeaTrackingFileCreatorLogFileName = ikeaTrackingFileCreatorLogFolderPath + @"IkeaTrackingFileCreator-log.txt";
		private const string sftpUploadServiceLogFile = logFolderPath + "SftpUploadService-log.txt";

        public static Serilog.Core.Logger webClientlogger = new LoggerConfiguration().WriteTo.File(webCLientLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger csrLogger = new LoggerConfiguration().WriteTo.File(csrLoggingFile, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger xCabEventMaintenanceServiceLogger = new LoggerConfiguration().WriteTo.File(xCabEventMaintenanceServiceLoggingFile, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger jobCreatorServiceLogger = new LoggerConfiguration().WriteTo.File(jobCreatorServiceLoggingFile, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger asnJobReleaseServiceLogger = new LoggerConfiguration().WriteTo.File(asnJobReleaseServiceLoggingFile, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger soapRequestoreLogger = new LoggerConfiguration().WriteTo.File(soapRequestoreLoggingFile, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger genericWebhookClientsLogger = new LoggerConfiguration().WriteTo.File(genericWebhookClientsLoggingFile, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveBookingApiLogger = new LoggerConfiguration().WriteTo.File(liveBookingApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testBookingApiLogger = new LoggerConfiguration().WriteTo.File(testBookingApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveCreateWithLabelApiLogger = new LoggerConfiguration().WriteTo.File(liveCreateWithLabelApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testCreateWithLabelApiLogger = new LoggerConfiguration().WriteTo.File(testCreateWithLabelApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveDGBookingApiLogger = new LoggerConfiguration().WriteTo.File(liveDGBookingApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testDGBookingApiLogger = new LoggerConfiguration().WriteTo.File(testDGBookingApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveQuoteApiLogger = new LoggerConfiguration().WriteTo.File(liveQuoteApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testQuoteApiLogger = new LoggerConfiguration().WriteTo.File(testQuoteApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveServiceQuoteApiLogger = new LoggerConfiguration().WriteTo.File(liveServiceQuoteApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testServiceQuoteApiLogger = new LoggerConfiguration().WriteTo.File(testServiceQuoteApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveConsolidatedBookingApiLogger = new LoggerConfiguration().WriteTo.File(liveConsolidatedBookingApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveRouteApiLogger = new LoggerConfiguration().WriteTo.File(liveRouteApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testRouteApiLogger = new LoggerConfiguration().WriteTo.File(testRouteApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveTrackingStatusApiLogger = new LoggerConfiguration().WriteTo.File(liveTrackingStatusApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveTrackAndTraceApiLogger = new LoggerConfiguration().WriteTo.File(liveTrackAndTraceApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveDriverLocationApiLogger = new LoggerConfiguration().WriteTo.File(liveDriverLocationApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testTrackingStatusApiLogger = new LoggerConfiguration().WriteTo.File(testTrackingStatusApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testDriverLocationApiLogger = new LoggerConfiguration().WriteTo.File(testDriverLocationApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveAsnApiLogger = new LoggerConfiguration().WriteTo.File(liveAsnApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testAsnApiLogger = new LoggerConfiguration().WriteTo.File(testAsnApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger toshibaTrackingLogger = new LoggerConfiguration().WriteTo.File(toshibaTrackingLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger liveBasfConsignmentApiLogger = new LoggerConfiguration().WriteTo.File(liveBasfConsignmentApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger testBasfConsignmentApiLogger = new LoggerConfiguration().WriteTo.File(testBasfConsignmentApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger barcodesForRouteApiLogger = new LoggerConfiguration().WriteTo.File(barcodesForRouteApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger trackingViaEmailLogger = new LoggerConfiguration().WriteTo.File(trackingViaEmailLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger wooCommerceOrderExtractorLogger = new LoggerConfiguration().WriteTo.File(wooCommerceOrderExtractorLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger rabbitMqConsumerLogger = new LoggerConfiguration().WriteTo.File(rabbitMqConsumerLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger nfsBookingFileExtractorLogger = new LoggerConfiguration().WriteTo.File(nfsBookingFileExtractorLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger shredderProcessorLogger = new LoggerConfiguration().WriteTo.File(shredderProcessorLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger fileDownloaderLogger = new LoggerConfiguration().WriteTo.File(fileDownloaderLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger activateBookingsApiLogger = new LoggerConfiguration().WriteTo.File(activateBookingsApiLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
        public static Serilog.Core.Logger rabbitMqPublisherLogger = new LoggerConfiguration().WriteTo.File(rabbitMqPublisherLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
        public static Serilog.Core.Logger rabbitMqConsumerTrackingLogger = new LoggerConfiguration().WriteTo.File(rabbitMqConsumerTrackingLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
		public static Serilog.Core.Logger ikeaTrackingFileCreatorLogger = new LoggerConfiguration().WriteTo.File(ikeaTrackingFileCreatorLogFileName, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
        public static Serilog.Core.Logger sftpUploadServiceLogger = new LoggerConfiguration().WriteTo.File(sftpUploadServiceLogFile, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30).CreateLogger();
        static RollingLogger()
		{
			try
			{
				//check if the log folder exists
				if (!Directory.Exists(logFolderPath))
					Directory.CreateDirectory(logFolderPath);
			}
			catch (Exception e)
			{
				Logger.Log("Exception Occurred while creating Log Folder, Path:" + logFolderPath + ", Exception:" + e.Message, nameof(RollingLogger), true);
			}
		}
		public static void Write(SlackChannel channel, string log)
		{
			switch (channel)
			{
				case SlackChannel.WebClientLogs:
					webClientlogger?.Information(log);
					break;
			}

		}
		public static void WriteToCsrLogs(string log)
		{
			csrLogger?.Information(log);
		}
		public static void WriteToXCabEventMaintenanceServiceLogs(string log)
		{
			xCabEventMaintenanceServiceLogger?.Information(log);
		}
		public static void WriteToJobCreatorServiceLogs(string log)
		{
			jobCreatorServiceLogger?.Information(log);
		}
		public static void WriteToAsnJobReleaseServiceLogs(string log)
		{
			asnJobReleaseServiceLogger?.Information(log);
		}
		public static void WriteToSoapRequestorLogs(string log)
		{
			soapRequestoreLogger?.Information(log);
		}
		public static void WriteToGenericWebhookClientsLogs(string log)
		{
			genericWebhookClientsLogger?.Information(log);
		}
		public static void WriteToLiveBookingApiLogs(string log)
		{
			liveBookingApiLogger?.Information(log);
		}
		public static void WriteToTestBookingApiLogs(string log)
		{
			testBookingApiLogger?.Information(log);
		}
		public static void WriteToLiveCreateWithLabelApiLogs(string log)
		{
			liveCreateWithLabelApiLogger?.Information(log);
		}
		public static void WriteToTestCreateWithLabelApiLogs(string log)
		{
			testCreateWithLabelApiLogger?.Information(log);
		}
		public static void WriteToLiveDGBookingApiLogs(string log)
		{
			liveDGBookingApiLogger?.Information(log);
		}
		public static void WriteToTestDGBookingApiLogs(string log)
		{
			liveDGBookingApiLogger?.Information(log);
		}
		public static void WriteToLiveQuoteApiLogs(string log)
		{
			liveQuoteApiLogger?.Information(log);
		}
		public static void WriteToTestQuoteApiLogs(string log)
		{
			testQuoteApiLogger?.Information(log);
		}
		public static void WriteToLiveServiceQuoteApiLogs(string log)
		{
			liveServiceQuoteApiLogger?.Information(log);
		}
		public static void WriteToTestServiceQuoteApiLogs(string log)
		{
			testServiceQuoteApiLogger?.Information(log);
		}
		public static void WriteToConsolidatedeBookingApiLogs(string log)
		{
			liveConsolidatedBookingApiLogger?.Information(log);
		}
		public static void WriteToLiveRoutetBookingApiLogs(string log)
		{
			liveRouteApiLogger?.Information(log);
		}
		public static void WriteToTestRoutetBookingApiLogs(string log)
		{
			liveRouteApiLogger?.Information(log);
		}
		public static void WriteToTrackingStatusApiLogs(string log)
		{
			liveTrackingStatusApiLogger?.Information(log);
		}
		public static void WriteToTrackAndTraceApiLogs(string log)
		{
			liveTrackAndTraceApiLogger?.Information(log);
		}
		public static void WriteToDriverLocationApiLogs(string log)
		{
			liveDriverLocationApiLogger?.Information(log);
		}
		public static void WriteToTestTrackingStatusApiLogs(string log)
		{
			testTrackingStatusApiLogger?.Information(log);
		}
		public static void WriteToTestDriverLocationApiLogs(string log)
		{
			testDriverLocationApiLogger?.Information(log);
		}
		public static void WriteToToshibaLogs(string log)
		{
			toshibaTrackingLogger?.Information(log);
		}

		public static void WriteToLiveAsnApiLogs(string log)
		{
			liveAsnApiLogger?.Information(log);
		}

		public static void WriteToTestAsnApiLogs(string log)
		{
			testAsnApiLogger?.Information(log);
		}
		public static void WriteToLiveBasfConsignmentApiLogs(string log)
		{
			liveBasfConsignmentApiLogger?.Information(log);
		}
		public static void WriteToTestBasfConsignmentApiLogs(string log)
		{
			testBasfConsignmentApiLogger?.Information(log);
		}
		public static void WriteToBarcodesForRoutesApiLogs(string log)
		{
			barcodesForRouteApiLogger?.Information(log);
		}
        
        public static void WriteToTrackingViaEmailApiLogs(string log, ELogTypes logType)
		{
			if (logType == ELogTypes.Error)
			{
				trackingViaEmailLogger?.Error(log);
			}
			else if (logType == ELogTypes.Information)
			{
				trackingViaEmailLogger?.Information(log);
			}
		}
		public static void WriteToWooCommerceOrderExtractorApiLogs(string log, ELogTypes logType)
		{
			if (logType == ELogTypes.Error)
			{
				wooCommerceOrderExtractorLogger?.Error(log);
			}
			else if (logType == ELogTypes.Information)
			{
				wooCommerceOrderExtractorLogger?.Information(log);
			}
		}
		public static void WriteToRabbitMqConsumerApiLogs(string log, ELogTypes logType)
		{
			if (logType == ELogTypes.Error)
			{
				rabbitMqConsumerLogger?.Error(log);
			}
			else if (logType == ELogTypes.Information)
			{
				rabbitMqConsumerLogger?.Information(log);
			}
		}
		public static void WriteToNfsBookingFileExtractorLogs(string log, ELogTypes logType)
		{
			if (logType == ELogTypes.Error)
			{
				nfsBookingFileExtractorLogger?.Error(log);
			}
			else if (logType == ELogTypes.Information)
			{
				nfsBookingFileExtractorLogger?.Information(log);
			}
		}

        public static void WriteToRabbitMqPublisherLogs(string log, ELogTypes logType)
        {
            if (logType == ELogTypes.Error)
            {
                rabbitMqPublisherLogger?.Error(log);
            }
            else if (logType == ELogTypes.Information)
            {
                rabbitMqPublisherLogger?.Information(log);
            }
        }

        public static void WriteToRabbitMqConsumerTrackingLogs(string log, ELogTypes logType)
        {
            if (logType == ELogTypes.Error)
            {
                rabbitMqConsumerTrackingLogger?.Error(log);
            }
            else if (logType == ELogTypes.Information)
            {
                rabbitMqConsumerTrackingLogger?.Information(log);
            }
        }

        public static void WriteToShredderProcessorLogs(string log, ELogTypes logType)
		{
			if (logType == ELogTypes.Error)
			{
				nfsBookingFileExtractorLogger?.Error(log);
			}
			else if (logType == ELogTypes.Information)
			{
				nfsBookingFileExtractorLogger?.Information(log);
			}
		}
		public static void WriteToFileDownloaderLogs(string log, ELogTypes logType)
		{
			if (logType == ELogTypes.Error)
			{
				fileDownloaderLogger?.Error(log);
			}
			else if (logType == ELogTypes.Information)
			{
				fileDownloaderLogger?.Information(log);
			}
		}
		public static void WriteToActivateBookingsApiLogs(string log, ELogTypes logType)
		{
			if (logType == ELogTypes.Error)
			{
				activateBookingsApiLogger?.Error(log);
			}
			else if (logType == ELogTypes.Information)
			{
				activateBookingsApiLogger?.Information(log);
			}
		}
		public static void WriteToIkeaTrackingFileCreatorLogs(string log, ELogTypes logType)
		{
			if (logType == ELogTypes.Error)
			{
				ikeaTrackingFileCreatorLogger?.Error(log);
			}
			else if (logType == ELogTypes.Information)
			{
				ikeaTrackingFileCreatorLogger?.Information(log);
			}
		}

        public static void WriteToSftpUploadServiceLogs(string log, ELogTypes logType)
        {
			if (logType == ELogTypes.Error)
				sftpUploadServiceLogger?.Error(log);
			else if (logType == ELogTypes.Information)
				sftpUploadServiceLogger?.Information(log);
        }
    }
}
