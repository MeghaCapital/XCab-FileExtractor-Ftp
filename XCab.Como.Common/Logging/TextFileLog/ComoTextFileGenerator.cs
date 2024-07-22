using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xcab.como.common.Logging.TextFileLog
{
    /// <summary>
    /// Create Seri log files
    /// <returns></returns>
    /// 
    public class ComoTextFileGenerator : IComoTextFileGenerator
    {
        private readonly string defaultlogFileName;

#if DEBUG
        private readonly string logFolderPath = @"C:\Logs\API\Como\";
#else
        private readonly string logFolderPath = @"D:\Logs\API\Como\";
#endif

        /// <summary>
        /// Default constructor for the class TextFileGenerator
        /// </summary>
        /// <returns> </returns>
        /// 
        public ComoTextFileGenerator(string projectType)
        {
            try
            {
                this.logFolderPath = @"C:\Logs\" + projectType + "\\";
                this.defaultlogFileName = this.logFolderPath + "XCAB_COMO-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!Directory.Exists(this.logFolderPath))
                    Directory.CreateDirectory(this.logFolderPath);
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
        public ComoTextFileGenerator(string logFolderPath, string logFileName)
        {
            try
            {
                defaultlogFileName = logFileName.Replace(".txt", " " + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                if (!Directory.Exists(logFolderPath))
                    Directory.CreateDirectory(logFolderPath);
            }
            catch (Exception)
            { }
        }

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

                File.AppendAllText(this.defaultlogFileName, System.Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ss") + " [" + logTextType + "] " + LogPoint + " : " + Detail);
            }
            catch (Exception e)
            { }

        }

    }
}
