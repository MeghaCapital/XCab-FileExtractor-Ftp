using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;


namespace Data
{
    public class AddressDetail
    {
        public string AddressLine1;
        public string? AddressLine2;
        public string? AddressLine3;
        public string? AddressLine4;
        public string? AddressLine5;
        public string Suburb;
        public string? Postcode;
    };

    // Added by GA 26April2017
    public class JobExtras1
    {
        public string ExtraPuInformation;
        public string ExtraDelInformation;
        public string SpecialInstructions;
    }
    public enum ConnectionChoice
    {
        [Description("192.168.160.10")]
        Internal_Melbourne,
        [Description("192.168.163.10")]
        Internal_Sydney,
        [Description("192.168.167.10")]
        Internal_Brisbane,
        [Description("192.168.169.3")]
        Internal_Perth,
        [Description("192.168.165.3")]
        Internal_Adelaide,
        [Description("203.48.241.85")]
        External_Melbourne,
        [Description("203.48.241.84")]
        External_Sydney,
        [Description("203.48.241.88")]
        External_Brisbane,
        [Description("203.48.241.89")]
        External_Perth,
        INVALID
    };
    public enum BoolChoice
    {
        [Description("Y")]
        Yes,
        [Description("N")]
        No
    };

    public static class Overrides
    {
        public static string CleanUp(this string str)
        {
            if (str == null)
                return "";
            var rgx = new Regex("[^a-zA-Z0-9 -']");
            return rgx.Replace(str, "");
        }
    }

    public class Booker
    {
        /// <summary>
        /// Builds an XML string appropriate to sending a booking to TPlus 
        /// </summary>
        /// <param name="serverIP">The IP Address of the TPlus Server that you wish to connect to.</param>
        /// <param name="clientCode">The client code of the client that you are booking on behalf of.</param>
        /// <param name="serviceCode">A valid Service Code for the job being booked.</param>
        /// <param name="fromAddress">An AddressDetail object representing the 'From' address of the job.</param>
        /// <param name="toAddress">An AddressDetail object representing the 'To' address of the job.</param>
        /// <param name="totalPieces">An integer representing the number of pieces to be picked up.</param>
        /// <param name="totalWeight">A double representing the weight in KG of the total items to be picked up.</param>
        /// <param name="userNumber">The 'user number' that Capital has provided you.</param>
        /// <param name="ref1">Any reference that you wish to provide.</param>
        /// <param name="ref2">Any reference that you wish to provide.</param>
        /// <param name="caller">A reportable field. Preferably the contact name for the job (i.e. 'Peter Jones').</param>
        /// <param name="pod_email">An email address to which the POD information for the job will be sent upon completion.</param>
        /// <param name="senderHostname">An IP address of FQDN relating to the system that is sending the request.</param>
        /// <param name="isAdvance">
        /// A choice of 'Yes' or 'No' to indicate if the job is intended to be an Advance Booking. 
        /// If 'Yes', the Advanced Date and Time details must be provided.
        /// </param>
        /// <param name="dateIn">The date of the Advance Booking. Must be in format: 1YYMMDD </param>
        /// <param name="hourIn">The hour of the Pickup</param>
        /// <param name="minuteIn">The minutes of the hour for the Pickup</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string BuildXML(
            ConnectionChoice serverIP,
            string clientCode,
            string serviceCode,
            AddressDetail fromAddress,
            AddressDetail toAddress,
            int totalPieces,
            double totalWeight,
            int userNumber,
            string ref1 = "",
            string ref2 = "",
            string caller = "",
            string pod_email = "",
            string senderHostname = "HOSTNAME_NOT_PROVIDED",
            BoolChoice isAdvance = BoolChoice.No,
            string dateIn = "",
            string hourIn = "",
            string minuteIn = ""
            )
        {

            var encoding = new ASCIIEncoding();
            string xmlRequest = null;
            string AdvanceFlag = null;
            string dayIn = null;

            if (isAdvance == BoolChoice.Yes)
            {
                AdvanceFlag = "A";
                dayIn = "255";
            }
            else
            {
                AdvanceFlag = "N";
                dayIn = "";
                dateIn = "";
                hourIn = "";
                minuteIn = "";
            }
            try
            {
                //We need to build the XML string that will be sent to TPlus.
                xmlRequest = "<FREIGHT_REQUEST STATUS=\"\" STIMULUS=\"\" RETRY_NR=\"\" REQTYPE=\"BOOK CHILD\" REPLY=\"\" FROM_SITE=\""
                    + senderHostname + "\" TO_SITE=\""
                    + EnumDescription(serverIP) + "\" CLIENT_CODE=\""
                    + clientCode.ToUpper().CleanUp() + "\">" + "<BOOKING B_USERNO=\""
                    + userNumber.ToString() + "\" B_CUST=\""
                    + clientCode.ToUpper().CleanUp() + "\" B_SERVICE=\""
                    + serviceCode.ToUpper().CleanUp() + "\" " + "B_DELVF1=\""
                    + fromAddress.AddressLine1.CleanUp() + "\" " + "B_DELVF2=\""
                    + fromAddress.AddressLine2.CleanUp() + "\" " + "B_DELVF3=\""
                    + fromAddress.AddressLine3.CleanUp() + "\" " + "B_DELVF4=\""
                    + fromAddress.AddressLine4.CleanUp() + "\" " + "B_DELVF5=\""
                    + fromAddress.AddressLine5.CleanUp() + "\" " + "B_FRS_PCODE=\""
                    + fromAddress.Postcode.CleanUp() + "\" " + "B_FRS=\""
                    + fromAddress.Suburb.ToUpper().CleanUp() + "\" " + "B_DELVT1=\""
                    + toAddress.AddressLine1.CleanUp() + "\" " + "B_DELVT2=\""
                    + toAddress.AddressLine2.CleanUp() + "\" " + "B_DELVT3=\""
                    + toAddress.AddressLine3.CleanUp() + "\" " + "B_DELVT4=\""
                    + toAddress.AddressLine4.CleanUp() + "\" " + "B_DELVT5=\""
                    + toAddress.AddressLine5.CleanUp() + "\" " + "B_TOS_PCODE=\""
                    + toAddress.Postcode + "\" " + "B_TOS=\""
                    + toAddress.Suburb.ToUpper().CleanUp() + "\" " + "B_QTY=\""
                    //+ totalPieces.ToString() + "\" " + "B_S_WEIGHT=\"0\" " + "B_S_PIECES=\""
                    + totalPieces.ToString() + "\" " + "B_S_WEIGHT=\"" + totalWeight.ToString() + "\" "
                    + "B_S_PIECES=\""
                    + totalPieces.ToString() + "\" " + "B_LEGS=\"1\" " + "B_RETURN=\"\" " + "B_REQPOD=\"\" " + "B_CALLER=\""
                    + caller.CleanUp() + "\" " + "B_REF=\""
                    + ref1.CleanUp() + "\" " + "B_REF2=\""
                    + ref2.CleanUp() + "\" " + "B_ADVANCE=\""
                    + EnumDescription(isAdvance) + "\" " + "B_BTYPE=\""
                    + AdvanceFlag + "\" " + "B_REQHR=\""
                    + hourIn + "\" " + "B_REQMIN=\""
                    + minuteIn + "\" " + "B_REQDAY=\""
                    + dayIn + "\" " + "B_REQDATE=\""
                    + dateIn + "\">" + "<POD_ADVICE " + "POD_EMAIL=\""
                    + pod_email + "\">" + "</POD_ADVICE>" + "</BOOKING>" + "</FREIGHT_REQUEST>";
            }
            catch (Exception e)
            {
                Core.Logger.Log("Exception Occurred:" + e.Message, "TPLUS Uploader");
            }

            //Add the signature to the existing URI.
            return xmlRequest;
        }

        public static string EnumDescription(Enum EnumConstant)
        {
            var fi = EnumConstant.GetType().GetField(EnumConstant.ToString());
            var aattr = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (aattr.Length > 0)
            {
                return aattr[0].Description;
            }
            else
            {
                return EnumConstant.ToString();
            }
        }
        public class TPLUSReturnStatus
        {
            public string Response { get; set; }
            public bool Success { get; set; }
            public TPLUSReturnStatus()
            {
                Success = false;
                Response = string.Empty;
            }
        }
        public static TPLUSReturnStatus Connect(ConnectionChoice server, String message, Int32 port = 3584)
        {
            var returnStatus = new TPLUSReturnStatus();
            try
            {
                // Create a TcpClient. 
                // Note, for this client to work you need to have a TcpServer  
                // connected to the same address as specified by the server, port 
                // combination. 

                var client = new TcpClient(EnumDescription(server), port);

                // Translate the passed message into ASCII and store it as a Byte array. 
                var data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing. 
                var stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                //#Debugging: Check the message that was sent
                //Console.WriteLine("Sent: {0}", message)

                // String to store the response ASCII representation. 
                var responseData = string.Empty;

                // Iterate through response to get complete response string whilst stream.DataAvailable is True
                var myReadBuffer = new byte[1025];
                var myCompleteMessage = new StringBuilder();
                do
                {
                    var numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                } while (stream.DataAvailable);

                // Populate the response to a string
                responseData = myCompleteMessage.ToString();

                // Close everything.
                stream.Close();
                client.Close();
                returnStatus.Response = "<TPlusTransaction><SentToTPlus>" + message + "</SentToTPlus><TPlusResponse>" + responseData + "</TPlusResponse></TPlusTransaction>";
                returnStatus.Success = true;

            }
            catch (ArgumentNullException e)
            {
                Core.Logger.Log(
                 "ArgumentNullException: " + e.ToString(), "TPlusUploader");
            }
            catch (SocketException e)
            {
                Core.Logger.Log("SocketException: " + e.ToString(), "TPlusUploader");
            }
            return returnStatus;

        }
    }
}