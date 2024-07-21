using Core.Helpers;
using Core.Models.Error;
using Core.Models.Slack;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Core
{
    public class Logger
    {
        public static string Source = "XCAB";
        public static string LogName = "Application";
        public static string SEvent;
        private const string SlackWebHookUrl = "https://hooks.slack.com/services/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";
        public static void SendHtmlEmailNotification(string Message, string Title, string To, string cc = "")
        {
            if (Message == null) throw new ArgumentNullException(nameof(Message));
            var htmlHead =
                string.Format("<html><body><font face= \"georgia\" color=\"red\"><b>*** XCab Error Notification ***</b></font>"
                              + "<br><br><p><font face=\"georgia\" color=\"blue\">Notification: </font>"
                              + "<font face = \"georgia\" color = \"green\">" + "Email Notification" + "</font>"
                              + "</p><p><font face =\"georgia\" color=\"blue\">Message: </font>"
                              + "<font face=\"georgia\" color=\"green\">" + Message
                              + @"</font></p>"
                              +
                              "<br><font face=\"georgia\" color=\"darkcyan\"> For more information Please filter Windows Event Logs using Id 234 on the server."
                              + "<br><b>&copy; Capital Transport XCab Engine</b></body></html>");



            const string from = "xCab@capitaltransport.com.au";
            var message = new MailMessage(from, To)
            {
                Subject = Title,
                Body = htmlHead,
                IsBodyHtml = true,

            };
            #if DEBUG
            if (cc.Length > 0)
                message.CC.Add(cc);
            #endif
            var client = new SmtpClient("smtprelay.challengelogistics.com.au") { UseDefaultCredentials = true };
            // Credentials are necessary if the server requires the client  
            // to authenticate before it will send e-mail on the client's behalf.

            try
            {
                client.Send(message);
            }
            catch (Exception)
            {
                //swallow
            }
        }
        public static async Task Log(string text, string name, bool sendEmail = false)
        {
#if !DEBUG
            /*    if (Source.Contains("Local"))
                    return;*/
            //SEvent = name + ":" + text;
            //if (!EventLog.SourceExists(Source))
            //    EventLog.CreateEventSource(Source, LogName);
            //check if this is an exception message
            if (text.ToLower().Contains("fail") || text.ToLower().Contains("exception") ||
                text.ToLower().Contains("error"))
            {
                //EventLog.WriteEntry(Source, SEvent, EventLogEntryType.Error, 234);
                await LogSlackError(text, name + ":", SlackChannel.GeneralErrors);
            }
            else if (text.ToLower().Contains("soap request to tplus:"))
            {
                //EventLog.WriteEntry(Source, SEvent, EventLogEntryType.Information, 234);
                await LogSlackError(text, name + ":", SlackChannel.XmlRequestSoap);
            }
            //remove email notification
            //if (sendEmail)
            //    SendHtmlEmail(text, name);
#endif
#if DEBUG

            //check if this is an exception message
            await LogSlackError(text, name + ":", SlackChannel.GeneralErrors);
            if (text.ToLower().Contains("fail") || text.ToLower().Contains("exception") ||
                text.ToLower().Contains("error"))
            {
                //   EventLog.WriteEntry(Source, sEvent, EventLogEntryType.Error, 234);
                //also send an email here
                // SendHtmlEmail(text, name);
            }
#endif
        }

        private static async Task LogSlackError(string message, string filename, SlackChannel channel,
            ErrorType errorType = ErrorType.General)
        {
            var environment = "Production";
#if DEBUG
            var buildType = "Debug";
            var userName = System.Environment.UserName;
            var machineName = System.Environment.MachineName;
            var environmentStringBuilder = new StringBuilder();
            environmentStringBuilder.Append(buildType);
            environmentStringBuilder.Append(" [User=");
            environmentStringBuilder.Append(userName);
            environmentStringBuilder.Append(",Machine=");
            environmentStringBuilder.Append(machineName + "]");
            environment = environmentStringBuilder.ToString();
#endif
            var sourceStringBuilder = new StringBuilder();
            sourceStringBuilder.Append(Source);
            sourceStringBuilder.Append("-");
            sourceStringBuilder.Append(environment);
            var errMessage =
                $"*{sourceStringBuilder.ToString()}* Raised A *{errorType.GetValueAttr()}*\r\n`{DateTime.Now}`\r\n```{filename} {message}```";
            var slackPayload = new SlackPayload
            {
                Text = errMessage,
                Channel = channel.GetValueAttr()
            };
            await PostMessage(slackPayload);
        }

        public static async Task LogSlackErrorFromApp(string appSource, string message, string filename, SlackChannel channel,
            ErrorType errorType = ErrorType.General)
        {
            try
            {
                var environment = "Production";
#if DEBUG
                var buildType  = "Debug";
                var userName = System.Environment.UserName;
                var machineName = System.Environment.MachineName;
                var environmentStringBuilder = new StringBuilder();
                environmentStringBuilder.Append(buildType);
                environmentStringBuilder.Append(" [User=");
                environmentStringBuilder.Append(userName);
                environmentStringBuilder.Append(",Machine=");
                environmentStringBuilder.Append(machineName+"]");
                environment = environmentStringBuilder.ToString();
#endif
                var sourceStringBuilder = new StringBuilder();
                sourceStringBuilder.Append(appSource);
                sourceStringBuilder.Append("-");
                sourceStringBuilder.Append(environment);
                var errMessage =
                    $"*{sourceStringBuilder.ToString()}* Raised A *{errorType.GetValueAttr()}*\r\n`{DateTime.Now}`\r\n```{filename} {message}```";
                var slackPayload = new SlackPayload
                {
                    Text = errMessage,
                    Channel = channel.GetValueAttr()
                };
                await PostMessage(slackPayload);
            }
            catch (Exception)
            {
                //swallow
            }
        }
        public static async Task LogSlackNotificationFromApp(string appSource, string message, string filename, SlackChannel channel,
           ErrorType errorType = ErrorType.General)
        {
            try
            {
                var environment = "Production";               
#if DEBUG
                var buildType = "Debug";
                var userName = System.Environment.UserName;
                var machineName = System.Environment.MachineName;
                var environmentStringBuilder = new StringBuilder();
                environmentStringBuilder.Append(buildType);
                environmentStringBuilder.Append(" [User=");
                environmentStringBuilder.Append(userName);
                environmentStringBuilder.Append(",Machine=");
                environmentStringBuilder.Append(machineName + "]");
                environment = environmentStringBuilder.ToString();
#endif
                var sourceStringBuilder = new StringBuilder();
                sourceStringBuilder.Append(appSource);
                sourceStringBuilder.Append("-");
                sourceStringBuilder.Append(environment);
                var errMessage =
                    $"*{sourceStringBuilder.ToString()}* Raised A *Notification*\r\n`{DateTime.Now}`\r\n```{filename} {message}```";
                var slackPayload = new SlackPayload
                {
                    Text = errMessage,
                    Channel = channel.GetValueAttr()
                };
                await PostMessage(slackPayload);
            }
            catch (Exception)
            {
                //swallow
            }
        }
        
        private static async Task PostMessage(SlackPayload payload)
        {            
            try
            {                
                using (var client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;                                   
                    var json = JsonConvert.SerializeObject(payload);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(SlackWebHookUrl, stringContent);
                //        client.UploadValues(
                //            "https://hooks.slack.com/services/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", "POST",
                //            data);
                }
            }
            catch (Exception)
            {
                //swallow  
            }
        }
    }
}
