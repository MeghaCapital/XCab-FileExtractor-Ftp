using Core.Helpers;
using Core.Models.Slack;
using Serilog;
using System;
using System.IO;

namespace Core.Logging.TextFileLog
{
    /// <summary>
    /// Create Seri log files
    /// <returns></returns>
    /// 
    public class TextFileGenerator : ITextFileGenerator, IRollingLogger
    {
        private bool UseRollingLogger = false;
#if DEBUG
        private const string LogFolderPath = @"C:\Logs\API\";
#else
        private const string LogFolderPath = @"D:\Logs\API\";
#endif
        private string DefaultlogFileName = LogFolderPath + "XCab-log " + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        // private static Serilog.Core.Logger RollingLogger;
        //.WriteTo.File(WebCLientLogFileName, rollingInterval: RollingInterval.Day).CreateLogger();

        /// <summary>
        /// Default constructor for the class TextFileGenerator
        /// </summary>
        /// <returns> </returns>
        /// 
        public TextFileGenerator()
        {
            try
            {
                if (!Directory.Exists(LogFolderPath))
                    Directory.CreateDirectory(LogFolderPath);
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Parameterised constructor for the class TextFileGenerator
        /// </summary>
        /// <param name="logFolderPath"></param>
        /// <param name="logFileName"></param>
        /// <returns>Please Refer to the structure@ShipmentModel.QuoteRequest </returns>
        /// 
        public TextFileGenerator(string logFolderPath, string logFileName)
        {
            try
            {
                if (!Directory.Exists(logFolderPath))
                    Directory.CreateDirectory(logFolderPath);
                if (!UseRollingLogger)
                {
                    DefaultlogFileName = logFileName.Replace(".txt", " " + DateTime.Now.ToString("yyyyMMdd") + ".txt");

                }
                else
                {
                    //if (RollingLogger == null)
                    //{
                    //    CreateLogger(logFileName);
                    //}
                }
            }
            catch (Exception e)
            {
                Logger.LogSlackNotificationFromApp("RESTful Web Service",
                         "Exception occurred while creating TextLogger, Message = " + e.Message,
                         "RESTful Web Service", SlackChannel.WebServiceErrors);
            }
        }

        //public void CreateLogger(string fileName)
        //{
        //    if (UseRollingLogger)
        //        RollingLogger = new LoggerConfiguration().WriteTo.File(fileName, rollingInterval: RollingInterval.Day).CreateLogger();

        //}

        /// <summary>
        /// write a record in a log file
        /// </summary>
        /// <param name="LogPoint"></param>
        /// <param name="Detail"></param>
        /// <param name="LogType"></param>
        /// <returns>Please Refer to the structure@ShipmentModel.QuoteRequest </returns>
        /// 
        public void Write(string LogPoint, string Detail, string LogType)
        {
            try
            {
                if (!UseRollingLogger)
                {
                    string logTextType = "";
                    switch (LogType)
                    {
                        case Constants.ErrorList.Error:
                            logTextType = "ERR";
                            break;
                        case Constants.ErrorList.Information:
                            logTextType = "INF";
                            break;
                        case Constants.ErrorList.Debug:
                            logTextType = "DEB";
                            break;
                        case Constants.ErrorList.Warning:
                            logTextType = "WAR";
                            break;
                        case Constants.ErrorList.Verbose:
                            logTextType = "VER";
                            break;
                        case Constants.ErrorList.Fatal:
                            logTextType = "FATAL";
                            break;
                        default:
                            logTextType = "DEF";
                            break;
                    }

                    File.AppendAllText(DefaultlogFileName, System.Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ss") + " [" + logTextType + "] " + LogPoint + " : " + Detail);
                }
            //    else
            //    {
            //        switch (LogType)
            //        {
            //            //case Constants.ErrorList.Error:                            
            //            //    RollingLogger.Error(Detail);
            //            //    break;
            //            //case Constants.ErrorList.Information:
            //            //    RollingLogger.Information(Detail);
            //            //    break;
            //            //case Constants.ErrorList.Debug:
            //            //    RollingLogger.Debug(Detail);
            //            //    break;
            //            //case Constants.ErrorList.Warning:
            //            //    RollingLogger.Warning(Detail);
            //            //    break;
            //            //case Constants.ErrorList.Verbose:
            //            //    RollingLogger.Verbose(Detail);                            
            //            //    break;
            //            //case Constants.ErrorList.Fatal:
            //            //    RollingLogger.Fatal(Detail);
            //            //    break;
            //            //default:
            //            //    RollingLogger.Information(Detail);
            //            //    break;
            //        }
            //    }
            }
            catch 
            { }

        }

    }
}
