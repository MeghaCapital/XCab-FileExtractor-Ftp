//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains EDIMessage Class
//			- encapsules the parsing and translation of EDI messages into XML
// </CreatedBy>

// Copyright (C) 2005 Anthony Yates a.yates@iosolutionsinc.com
//
// This software is provided AS IS. No warranty is granted, 
// neither expressed nor implied. USE THIS SOFTWARE AT YOUR OWN RISK.
// NO REPRESENTATION OF MERCHANTABILITY or FITNESS FOR ANY 
// PURPOSE is given.
//
// License to use this software is limited by the following terms:
// 1) This code may be used in any program, including programs developed
//    for commercial purposes, provided that this notice is included verbatim.
//    
// Also, in return for using this code, please attempt to make your fixes and
// updates available in some way, such as by sending your updates to the
// author.
//
//--------------------------------------------------------------

using Core;
using Core.Models.Slack;
using Edifact_Library.parser.utils;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace EDIFACT
{
    /// <summary>
    /// EDIMessage encapsules the parsing and translation of EDI messages into XML.
    /// </summary>
    public class EDIMessage : IDisposable
    {
        #region Properties

        private string _rawMessage;
        public string RawMessage
        {
            get { return _rawMessage; }
            set { _rawMessage = value; }
        }

        /// <summary>
        /// EDI Message properties. 
        /// </summary>
        public MessageProperties MsgProperties
        {
            get { return this.parser.MessageProperties; }
            //set{this._msgProperties = value;}
        }

        private bool _verbose;
        /// <summary>
        /// Determines if output is verbose.
        /// </summary>
        public bool Verbose
        {
            get { return _verbose; }
            set { _verbose = value; }
        }

        bool _useFileIncrement;
        int _incrementValue;
        /// <summary>
        /// Increment returns the next incremental value. Unless IncrementSeed set to a value, 
        /// Increments default value returns 1. If IncrementStep set, next increment value returned
        /// is Increment * IncrementStep. For example, if IncrementStep = 5, initial call to
        /// Increment returns 5, next call to Increment returns 10, etc.
        /// </summary>
        public Int32 Increment
        {
            get
            {
                if (_useIncrementSeed)
                {
                    _useIncrementSeed = false;
                    return _incrementValue * IncrementStep;
                }
                _useFileIncrement = true;
                return ++_incrementValue * IncrementStep;
            }
        }

        bool _useIncrementSeed;
        int _fileIncrementSeed;
        /// <summary>
        /// IncrementSeed is the initial value the %Increment% macro will use when saving output to file.
        /// </summary>
        public Int32 IncrementSeed
        {
            get
            {
                return _fileIncrementSeed;
            }
            set
            {
                if (value >= 0)
                {
                    _useFileIncrement = true;
                    _useIncrementSeed = true;
                    _fileIncrementSeed = value;
                    _incrementValue = _fileIncrementSeed;
                }
                else throw new ArgumentOutOfRangeException(null, "IncrementSeed must be equal to or greater than 0");
            }
        }


        int _fileIncrementStep;
        /// <summary>
        /// IncrementStep is the multiplication value the %Increment% macro will use when saving output to file.
        /// </summary>
        /// <remarks>
        /// IncrementStep is the multiplication value the %Increment% macro will use when saving output to file.
        /// </remarks>
        public Int32 IncrementStep
        {
            get
            {
                return _fileIncrementStep;
            }
            set
            {
                if (value > 0)
                {
                    _useFileIncrement = true;
                    _fileIncrementStep = value;
                }
                else throw new ArgumentOutOfRangeException(null, "IncrementStep must be equal to or greater than 1");
            }
        }


        #endregion

        #region Constructor and Deconstructor

        /// <summary>
        /// Instantiates the EDIFACT message interface for processing EDI files, and converting them to XML.
        /// </summary>
        /// <param name="filename">Path of the edifact file.</param>
        public EDIMessage()
        {

        }

        private bool PreProcessFile = true;
        public SegmentCollection[] GetAllSegmentCollections(string filename)
        {
            SegmentCollection[] segments = null;
            if (File.Exists(filename))
            {
                _incrementValue = 0;
                IncrementSeed = 0;
                IncrementStep = 1;
                if (PreProcessFile)
                {
                    var modifiedFileName = filename + "_tmp";
                    try
                    {
                        //create a new temporary file
                        using (var outputStream = new StreamWriter(modifiedFileName))
                        {
                            //read from the source file line by line
                            foreach (var line in File.ReadLines(filename))
                            {
                                //only check for apostrophes for lines that includes NAD+UD & NAD+ST 
                                if (line.Contains("NAD+UD") || line.Contains("NAD+ST"))
                                {
                                    //check if there is more than one apostrophes in the line, if more than one it means that the delivery address includes an apostrophe
                                    if (line.Count(x => x == '\'') > 1)
                                    {
                                        //we need to replace all apostrophes but the last (as thats treated as the delimeter)
                                        var modifiedLine = "";
                                        var lastIndex = line.LastIndexOf('\'');
                                        if (lastIndex > 0)
                                        {
                                            //replace all apostrophes but the last
                                            modifiedLine = line.Substring(0, lastIndex).Replace("'", "")
                                                           + line.Substring(lastIndex);
                                        }
                                        //write the line to the temporary file
                                        outputStream.WriteLine(modifiedLine);
                                    }
                                    else
                                    {
                                        //this would indicate that NAD+UD (used for address detail lines) only have one trailing apostrophe as the delimeter
                                        outputStream.WriteLine(line);
                                    }
                                }
                                else
                                {
                                    //this would indicate all other tags and we do not change the content of the line
                                    outputStream.WriteLine(line);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Log(
                                "Exception occurred while creating temp file:" + modifiedFileName + " Exception:" +
                                e.Message, "EDIMessage");

                    }
                    _rawMessage = FileUtility.Read(modifiedFileName);
                    if (_rawMessage.Length == 0)
                    {
                        Logger.LogSlackNotificationFromApp("XCab Edifact Library:EDIMessage",
                                                       "XCab Edifact Library ClassName:EDIMessage had an issue in creating temporary file, file with 0 Messages created, filename:" + modifiedFileName,
                                                       "XCab EdifactLibrary: MethodName:GetAllSegmentCollections", SlackChannel.GeneralErrors);
                    }
                    try
                    {
                        if (File.Exists(modifiedFileName))
                            File.Delete(modifiedFileName);
                    }
                    catch (Exception e)
                    {
                        Logger.Log(
                                "Exception occurred while deleting temp file:" + modifiedFileName + " Exception:" +
                                e.Message, "EDIMessage");
                    }
                }
                else
                {
                    _rawMessage = FileUtility.Read(filename);
                }

                //_rawMessage = _rawMessage.Replace("'", "");
                if (!_rawMessage.StartsWith("UN"))
                    throw new ArgumentException("File is not a valid EDI message", filename);
                parser = new Parser(ref _rawMessage);
                segments = parser.GetAllSegments(ref _rawMessage);
                // messageArray = parser.GetMessages();
            }
            else
                throw new FileNotFoundException("EDI message file was not found", filename);
            return segments;
        }

        /// <summary>
        /// Instantiates the EDIFACT message interface for processing EDI files, and converting them to XML.
        /// </summary>
        /// <param name="rawmessage">A string representation of the EDI message. Use this if the EDIFACT message has already been read from a file and is available as a string.</param>
        /// <param name="flag"></param>
        public EDIMessage(string rawmessage, bool flag)
        {
            if (rawmessage != null && rawmessage.Length > 0)
            {
                _incrementValue = 0;
                IncrementSeed = 0;
                IncrementStep = 1;
                _rawMessage = rawmessage;

                if (!_rawMessage.StartsWith("UN"))
                    throw new ArgumentException("String is not a valid EDI message", rawmessage);
                parser = new Parser(ref _rawMessage);
                messageArray = parser.GetMessages();
            }
            else
                throw new NullReferenceException("No EDI message string passed in.");
        }

        ~EDIMessage()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            //Don't dispose more than once
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {//Free managed resources here:
                parser.Dispose();
                if (messageArray != null)
                {
                    for (int i = 0; i < messageArray.Length; i++)
                        messageArray[i] = null;
                }
                if (xDoc != null)
                {
                    for (int i = 0; i < xDoc.Length; i++)
                        xDoc[i] = null;
                }
                _rawMessage = null;
            }
            //Free unmanaged resources here:
            //Set disposed flag:
            _alreadyDisposed = true;
        }


        #endregion

        #region Private Variables
        private bool _alreadyDisposed = false;
        private Parser parser;
        private IMessage[] messageArray;
        private XmlDocument[] xDoc;
        #endregion

        /// <summary>
        /// This function converts the inner representation of an EDI message into XML.
        /// </summary>
        /// <returns>An array of XmlDocument that may consist of zero or many EDI messages in an XML representation.</returns>
        public XmlDocument[] SerializeToXml()
        {
            XmlSerializer sr;
            StringBuilder sb;
            XmlTextWriter tr;
            xDoc = new XmlDocument[messageArray.Length];

            try
            {
                for (Int32 i = 0; i < messageArray.Length; i++)
                {
                    System.Type type = messageArray[i].GetType();
                    sr = new XmlSerializer(type);
                    sb = new StringBuilder();
                    tr = new XmlTextWriter(new StringWriterWithEncoding(sb, System.Text.Encoding.UTF8)); //Environment.CurrentDirectory + "\\" + ord.Number + "Serialized.xml");
                    sr.Serialize(tr, messageArray[i]);
                    tr.Close();
                    if (_verbose)
                        Console.WriteLine(sb.ToString());
                    xDoc[i] = new XmlDocument();
                    xDoc[i].LoadXml(sb.ToString());
                }
                return xDoc;
            }
            catch (Exception)
            {
                //Console.WriteLine(string.Format("Exception: {0}. InnerException: {1}.", ex.Message, ex.InnerException));
                return null;
            }
            finally
            {
                sr = null;
                sb = null;
                tr = null;
            }
        }

        /// <summary>
        /// This method saves the XML representation of the EDIFACT message to disk.
        /// </summary>
        /// <param name="filename">The path and name of the file to save.</param>
        public void SerializeToFile(string filename)
        {
            if (xDoc == null)
                this.SerializeToXml();
            //Allocate strings for message names
            parser.mp.fileNames = new string[xDoc.Length];
            for (Int32 i = 0; i < xDoc.Length; i++)
            {
                string newfilename = AnalyseFileName(filename, i);
                xDoc[i].Save(newfilename);
                if (Verbose)
                {
                    Console.Write(xDoc[i].ToString());
                }
                parser.mp.fileNames[i] = newfilename;
            }

            if (Increment > 0)
                IncrementSeed = 0;
        }

        /// <summary>
        /// This function is used to gain access to an in-memory XML representation of the EDIFACT message.
        /// </summary>
        /// <returns>Zero or many XmlDocuments that are the XML representation of an EDIFACT message.</returns>
        public XmlDocument[] GetSerializedXml()
        {
            if (xDoc != null)
                return xDoc;
            return null;
        }



        private string AnalyseFileName(string filename, Int32 count)
        {   // %Date%		%MessageReference%	%MessageType%
            // %Increment%	%Time%				%Identifier%
            Int32 counter = 0;
            string[] keywords = { "D", "MR", "MT", "I", "T", "ID" };//,""};
            string[] temp = filename.Split('%');
            string result = "";
            foreach (string s in temp)
            {
                foreach (string key in keywords)
                {
                    if (s == key || string.Compare(s, key, true) == 0)
                    {
                        switch (s.ToLower())
                        {
                            case "d":
                                {
                                    string date = DateTime.Now.ToString("s", DateTimeFormatInfo.InvariantInfo);
                                    if (count > 0)
                                    {
                                        temp[counter] = string.Concat(count + 1, "_", date.Replace(':', '-'));
                                        break;
                                    }
                                    temp[counter] = date.Replace(':', '-');
                                    break;
                                }
                            case "mr":
                                {
                                    temp[counter] = parser.MessageProperties.msgRefNumber[count].ToString();
                                    break;
                                }
                            case "mt":
                                {
                                    temp[counter] = parser.MessageProperties.identifier.ToString(); //this._docType;
                                    break;
                                }
                            case "i":
                                {
                                    if (_useFileIncrement)
                                    {
                                        count = Increment;
                                    }
                                    else
                                        count++;

                                    temp[counter] = count.ToString();
                                    break;
                                }
                            case "t":
                                {
                                    string currentTime = DateTime.Now.TimeOfDay.ToString().Replace(":", ".");
                                    Thread.Sleep(10);
                                    temp[counter] = currentTime;
                                    break;
                                }
                            case "id":
                                {
                                    Guid id = Guid.NewGuid();
                                    temp[counter] = id.ToString();
                                    break;
                                }
                        }
                    }
                }
                result += temp[counter].ToString();
                counter++;
            }

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

    }

    /// <summary>
    /// A struct to hold the main properties of an EDIFACT message.
    /// This information is derived from the UNB and UNH segments of a message.
    /// </summary>
    public struct MessageProperties
    {
        public Int32 messageCount;
        public MessageTypeIdentifier identifier;
        public string code;         // EAN007
        public string version;      // D
        public string releaseNumber;    // 96A
        public string sender;       //
        public string receiver;     //
        public string date;         //
        public string time;         //
        public string msgNumber;        //
        public string[] msgRefNumber;   //
        public string[] fileNames;      //
    }
}
