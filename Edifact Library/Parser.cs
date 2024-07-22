//--------------------------------------------------------------
// <CreatedBy>
//     Anthony R Yates
//	   Feb. 26, 2005
//     Runtime Version: 1.1.4322.2032
//	   
//     Contains Parser Class
//			- encapsules the parsing of EDI messages
//			- creates the appropriate EDIFACT message class
//			- populates and returns EDIFACT message class
//
//		Optionally, could create MessageFactory Class, but why, why not?
//
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

using EDIFACT.D93A.DESADV;
using EDIFACT.D93A.INVOIC;
using EDIFACT.D93A.ORDERS;
using EDIFACT.D93A.ORDRSP;
using EDIFACT.D93A.PRICAT;
using EDIFACT.D96A.APERAK;
using EDIFACT.D96A.DESADV;
using EDIFACT.D96A.INVOIC;
using EDIFACT.D96A.ORDERS;
using EDIFACT.D96A.ORDRSP;
using EDIFACT.D96A.PRICAT;
using System;

//D93
//D96

namespace EDIFACT
{
    /// <summary>
    /// This class encapsulates the parsing of EDIFACT messages. 
    /// It also is the responsible for passing the array of segments derived from the
    /// message to the corresponding EDIFACT message class.
    /// </summary>
    public class Parser : IDisposable
    {

        public MessageProperties mp;
        private bool _alreadyDisposed = false;
        char[] CrLf = { '\r', '\n' }; //crlf.ToCharArray();

        public MessageProperties MessageProperties
        {
            get { return mp; }
            set { mp = value; }
        }

        private IMessage[] messageObject;
        private Segment[] arSegments;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Parser() { }

        /// <summary>
        /// Primary constructor, used for receiving a reference to a string representation of an
        /// EDIFACT message.
        /// </summary>
        /// <param name="rawMessage"></param>
        public Parser(ref string rawMessage)
        {

        }

        public SegmentCollection[] GetAllSegments(ref string rawMessage)
        {

            DateTime startTime = HighResClock.Now;

            arSegments = ParseDocument(ref rawMessage);

            TimeSpan duration = HighResClock.Now - startTime;
            Console.WriteLine("ParseDocument: {0}ms", duration.TotalMilliseconds);

            startTime = HighResClock.Now;

            SegmentCollection[] segments = CreateMessageObject(ref arSegments);

            duration = HighResClock.Now - startTime;
            Console.WriteLine("CreateMessageObject: {0}ms", duration.TotalMilliseconds);
            return segments;

        }

        /// <summary>
        /// This method parses an edi message, builds an array of <see cref="Segment"/>
        /// and populates the <see cref="MessageProperties"/> property.
        /// </summary>
        /// <param name="ediMessage">A reference to the string representation of the EDI message.</param>
        /// <returns></returns>
        public Segment[] ParseDocument(ref string ediMessage)
        {
            string[] te_mpSegments;
            Segment[] arSegments;
            //			string del = "'";
            char[] delim = new char[] { '\'' }; //del.ToCharArray();
            Int32 _messageCount = 0;


            for (Int32 j = 0; j < ediMessage.Length; j++)
            {
                if (IsDelimiter(ediMessage[j]))
                {
                    ediMessage = ediMessage.Remove(j, 1);

                    --j;
                }
            }
            /*if (ediMessage.Contains("NAD+UD"))
			{
				if (ediMessage.Count(x => x == '\'') > 1)
				{
					var lastIndex = ediMessage.LastIndexOf('\'');
					ediMessage = ediMessage.Substring(0, lastIndex).Replace("'", "")
		+ ediMessage.Substring(lastIndex);
				}
			}*/
            ediMessage = ediMessage.Trim(delim);
            te_mpSegments = ediMessage.Split(delim);

            //			System.Collections.ArrayList Fields 
            //				= new System.Collections.ArrayList(te_mpSegments.Length);
            //
            //			char [] fieldDel = new char[] {'+',':'};
            //			foreach(string s in te_mpSegments)
            //			{
            //				Fields.Add(s.Split(fieldDel));
            //			}
            //
            //			string d = Fields[0].ToString();
            //			foreach(string [] s in Fields)
            //			{
            //				foreach(string str in s)
            //				{
            //					if(str == "UNB" || str == "UNH"	|| str == "UNZ")
            //					Console.WriteLine(str);
            //				}
            //			}

            arSegments = new Segment[te_mpSegments.Length];

            for (int i = 0; i < te_mpSegments.Length; i++)
            {
                arSegments[i] = new Segment(ref te_mpSegments[i]);

                if (arSegments[i].Name == "UNB" || arSegments[i].Name == "UNH"
                    || arSegments[i].Name == "UNZ")
                {
                    switch (arSegments[i].Name)
                    {
                        case "UNB":
                            {   //UN Code, UN Version
                                if (arSegments[i].Fields.Count == 7)
                                {
                                    mp.code = arSegments[i].Fields.Item(0).Value;
                                    mp.version = arSegments[i].Fields.Item(1).Value;
                                    mp.sender = arSegments[i].Fields.Item(2).Value;
                                    mp.receiver = arSegments[i].Fields.Item(3).Value;
                                    mp.date = arSegments[i].Fields.Item(4).Value;
                                    mp.time = arSegments[i].Fields.Item(5).Value;
                                    mp.msgNumber = arSegments[i].Fields.Item(6).Value;
                                    continue;
                                }
                                else
                                {
                                    mp.code = arSegments[i].Fields.Item(0).Value;
                                    mp.version = arSegments[i].Fields.Item(1).Value;
                                    mp.sender = arSegments[i].Fields.Item(2).Value;
                                    mp.receiver = arSegments[i].Fields.Item(4).Value;
                                    mp.date = arSegments[i].Fields.Item(6).Value;
                                    mp.time = arSegments[i].Fields.Item(7).Value;
                                    mp.msgNumber = arSegments[i].Fields.Item(8).Value;
                                }
                                continue;
                            }
                        case "UNH":
                            {
                                ++_messageCount;
                                //Set MsgIdentifier
                                if (mp.identifier == MessageTypeIdentifier.UNDEFINED)
                                {
                                    mp.identifier = SetMessageType(arSegments[i].Fields.Item(1));
                                }

                                mp.version = arSegments[i].Fields.Item(2).Value.Trim();
                                mp.releaseNumber = arSegments[i].Fields.Item(3).Value.Trim();
                                //ReSize and Allocate new msgRefNumber
                                string[] te_mpArray = new string[_messageCount];
                                if (mp.msgRefNumber == null)
                                    mp.msgRefNumber = new string[_messageCount];
                                else
                                    mp.msgRefNumber.CopyTo(te_mpArray, 0);
                                te_mpArray[_messageCount - 1] = arSegments[i].Fields.Item(0).Value;
                                mp.msgRefNumber = te_mpArray;
                                continue;
                            }
                        case "UNZ":
                            {
                                continue;
                            }
                    }
                }
            }
            mp.messageCount = _messageCount;
            te_mpSegments = null;
            return arSegments;
        }

        private bool IsDelimiter(char c)
        {
            if (c.Equals(CrLf[0]) || c.Equals(CrLf[1]))
                return true;
            return false;
        }
        private SegmentCollection[] CreateMessageObject(ref Segment[] segments)
        {
            SegmentCollection[] sc = new SegmentCollection[mp.messageCount];
            Int32 scCount = 0;

            //Loops through segments and spits them into segment collections.
            //Each Segment Collection will become a single message.
            for (Int32 j = 0; j < arSegments.Length; j++)
            {
                string name = arSegments[j].Name;

                if (name == "UNA" || name == "UNB" ||
                    name == "UNG" || name == "UNE" ||
                    name == "UNZ") continue;
                if (name == "UNH") { sc[scCount] = new SegmentCollection(); }
                if (name == "UNT")
                {
                    sc[scCount].Add(segments[j]);
                    if (j == arSegments.Length - 1)
                        break;
                    scCount++;
                    continue;
                }
                sc[scCount].Add(segments[j]);
            }

            //Create as many message objects as needed.
            return sc;

            //messageObject = new IMessage[mp.messageCount];

            //Loop over segment collections and create as many edi messages.

        }


        public IMessage[] GetMessages()
        {
            if (this.messageObject != null)
                return messageObject;
            return null;
        }


        private MessageTypeIdentifier SetMessageType(Field fld)
        {
            switch (fld.Value)
            {
                case "APERAK":
                    return MessageTypeIdentifier.APERAK;
                case "DESADV":
                    return MessageTypeIdentifier.DESADV;
                case "INVOIC":
                    return MessageTypeIdentifier.INVOIC;
                case "ORDERS":
                    return MessageTypeIdentifier.ORDERS;
                case "ORDRSP":
                    return MessageTypeIdentifier.ORDRSP;
                case "PRICAT":
                    return MessageTypeIdentifier.PRICAT;
            }
            return MessageTypeIdentifier.UNDEFINED;
        }


        private IMessage GetMessageType(string release, MessageTypeIdentifier message)
        {
            if (release == "93A")
            {
                switch (message)
                {
                    case MessageTypeIdentifier.DESADV: { return new D93A_DESADV(); }
                    case MessageTypeIdentifier.INVOIC: { return new D93A_INVOIC(); }
                    case MessageTypeIdentifier.ORDERS: { return new D93A_ORDERS(); }
                    case MessageTypeIdentifier.ORDRSP: { return new D93A_ORDRSP(); }
                    case MessageTypeIdentifier.PRICAT: { return new D93A_PRICAT(); }
                }
                throw new Exception("No Message Type Defined: No message identifier specifier for this message");
            }
            else if (release == "96A")
            {
                switch (message)
                {
                    case MessageTypeIdentifier.DESADV: { return new D96A_DESADV(); }
                    case MessageTypeIdentifier.APERAK: { return new D96A_APERAK(); }
                    case MessageTypeIdentifier.INVOIC: { return new D96A_INVOIC(); }
                    case MessageTypeIdentifier.ORDERS: { return new D96A_ORDERS(); }
                    case MessageTypeIdentifier.ORDRSP: { return new D96A_ORDRSP(); }
                    case MessageTypeIdentifier.PRICAT: { return new D96A_PRICAT(); }
                }
                throw new Exception("No Message Type Defined: No message identifier specifier for this message");
            }
            throw new Exception("Unidentified release number.");
        }


        ~Parser()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            //Don't dispose more than once
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {//Free managed resources here:
                messageObject = null;
                arSegments = null;
            }
            //Free unmanaged resources here:
            //Set disposed flag:
            _alreadyDisposed = true;
        }
    }
}
