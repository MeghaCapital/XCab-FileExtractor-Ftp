using Core;
using Core.Helpers;
using Core.Logging.SeriLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Data
{
    /// <summary>
    /// Send Email Functionality
    /// </summary>
    public static class SendMail
    {
        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="toEmailAddress"></param>
        public static void SendHtmlEmail(string Message, string toEmailAddress)
        {
            var htmlHead =
                string.Format("<html><body><font face= \"georgia\" color=\"#FA8072\"><b>XCab Notification</b></font><br>"
                              + "<br><font face = \"georgia\" color = \"#2E86C1\">" + Message + "</font></p>"
                              + "<font face=\"georgia\" color=\"darkcyan\">"
                              + "<br><b>&copy; Capital Transport XCab Engine</b></font></body></html>");

            var to = "";
            if (!string.IsNullOrEmpty(toEmailAddress))
            {
                to = toEmailAddress.Trim();
            }
            const string from = EmailFromAddress;
            var message = new MailMessage(from, to)
            {
                Subject = "xCab Message - Running on " + Environment.MachineName + " Server",
                Body = htmlHead,
                IsBodyHtml = true
            };
            var client = new SmtpClient(SmtpHostname) { UseDefaultCredentials = true };
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
        /// <summary>
        /// Send Email 
        /// </summary>
        /// <param name="htmlMessage"></param>
        /// <param name="toEmailAddress"></param>
        /// <param name="subject"></param>
        public static void SendHtmlEmail(string htmlMessage, string toEmailAddress, string subject)
        {
            var cc = "";
            const string from = EmailFromAddress;
            var message = new MailMessage(from, toEmailAddress)
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            var client = new SmtpClient(SmtpHostname) { UseDefaultCredentials = true };
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        /// <summary>
        /// Send HTML Email with attachments
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="attachmentFilename"></param>
        /// <param name="messageSubject"></param>
        /// <param name="toEmailAddress"></param>
        /// <param name="deleteAttchements"></param>
        public static void SendHtmlEMailWithAttachmentCSV(string Message, string attachmentFilename, string messageSubject, string toEmailAddress, bool deleteAttchements = true)
        {
            var htmlHead =
                string.Format("<html><body><font face= \"georgia\" color=\"#FA8072\"><b>XCab Route Notification</b></font><br>"
                              + "<br><font face = \"georgia\" color = \"#2E86C1\">" + Message + "</font></p>"
                              + "<font face=\"georgia\" color=\"darkcyan\">"
                              + "<br><b>&copy; Capital Transport XCab Service</b></font></body></html>");

            var to = "";
            if (!string.IsNullOrEmpty(toEmailAddress))
            {
                to = toEmailAddress.Trim();
            }
            string from = EmailFromAddress;
            var message = new MailMessage(from, to)
            {
                Subject = messageSubject,
                Body = htmlHead,
                IsBodyHtml = true
            };

            var fs = new FileStream(attachmentFilename, FileMode.Open);

            if (fs != null)
            {
                var attachment = new Attachment(fs, attachmentFilename.Substring(attachmentFilename.LastIndexOf('\\') + 1).Split('.')[0].ToString() + ".csv", "text/text");
                var disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                disposition.FileName = attachmentFilename.Substring(attachmentFilename.LastIndexOf('\\') + 1).Split('.')[0].ToString() + ".csv";
                disposition.Size = new FileInfo(attachmentFilename).Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                message.Attachments.Add(attachment);
            }

            var client = new SmtpClient(SmtpHostname) { UseDefaultCredentials = true };

            try
            {
                client.Send(message);
                message.Dispose();
                if (deleteAttchements)
                    File.Delete(Path.GetFileName(attachmentFilename));
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
        /// <summary>
        /// Send HTML Email With Attachments
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="attachmentFilename"></param>
        /// <param name="messageSubject"></param>
        /// <param name="toEmailAddress"></param>
        /// <param name="cc"></param>
        public static void SendHtmlEMailWithAttachment(string Message, string attachmentFilename, string messageSubject, string toEmailAddress, string cc = "")
        {
            var htmlHead =
                string.Format("<html><body><font face= \"georgia\" color=\"#FA8072\"><b>XCab Notification</b></font><br>"
                              + "<br><font face = \"georgia\" color = \"#2E86C1\">" + Message + "</font></p>"
                              + "<font face=\"georgia\" color=\"darkcyan\">"
                              + "<br><b>&copy; Capital Transport XCab Service</b></font></body></html>");

            var to = "";
            if (!string.IsNullOrEmpty(toEmailAddress))
            {
                to = toEmailAddress.Trim();
            }

            string from = EmailFromAddress;
            var message = new MailMessage(from, to)
            {
                Subject = messageSubject,
                Body = htmlHead,

                IsBodyHtml = true
            };
            #if DEBUG
            if (cc.Length > 0)
                message.CC.Add(cc);
            #endif
            if (attachmentFilename != null)
            {
                var attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                var disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                disposition.FileName = Path.GetFileName(attachmentFilename);
                disposition.Size = new FileInfo(attachmentFilename).Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                message.Attachments.Add(attachment);
            }

            var client = new SmtpClient(SmtpHostname) { UseDefaultCredentials = true };
            try
            {
                client.Send(message);
                message.Dispose();
                File.Delete(Path.GetFileName(attachmentFilename));
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
        /// <summary>
        /// Send HTML Email Text
        /// </summary>
        /// <param name="message"></param>
        /// <param name="attachmentFiles"></param>
        /// <param name="messageSubject"></param>
        /// <param name="toEmailAddress"></param>
        /// <param name="ccEmailAddress"></param>
        public static void SendHtmlEMailWithAttachment(string message, List<AttachmentFile> attachmentFiles, string messageSubject, string[] toEmailAddress, string[] ccEmailAddress = null)
        {
            var htmlHead =
            string.Format("<html><body><font face= \"georgia\" color=\"#FA8072\"><b>XCab Notification</b></font><br>"
                              + "<br><font face = \"georgia\" color = \"#2E86C1\">" + message + "</font></p>"
                              + "<font face=\"georgia\" color=\"darkcyan\">"
                              + "<br><b>&copy; Capital Transport XCab Service</b></font></body></html>");


            string from = EmailFromAddress;
            var mailMessage = new MailMessage(from, toEmailAddress[0], messageSubject, htmlHead)
            {
                IsBodyHtml = true
            };

            if (toEmailAddress != null && toEmailAddress.Length > 1)
                for (int intI = 1; intI < toEmailAddress.Length; intI++)
                    mailMessage.To.Add(toEmailAddress[intI]);


            if (ccEmailAddress != null && ccEmailAddress.Length > 1)
                foreach (var ccEmaill in ccEmailAddress)
                    mailMessage.CC.Add(ccEmaill);

            foreach (var attachmentFile in attachmentFiles)
            {
                try
                {
                    var PathNameOrdered = attachmentFile.Name;
                    File.WriteAllText(PathNameOrdered, attachmentFile.Content, Encoding.ASCII);
                    var attachment = new Attachment(PathNameOrdered, MediaTypeNames.Application.Octet);
                    var disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(PathNameOrdered);
                    disposition.ModificationDate = File.GetLastWriteTime(PathNameOrdered);
                    disposition.ReadDate = File.GetLastAccessTime(PathNameOrdered);
                    disposition.FileName = Path.GetFileName(PathNameOrdered);
                    disposition.Size = new FileInfo(PathNameOrdered).Length;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    mailMessage.Attachments.Add(attachment);
                }
                catch (Exception e)
                {
                    Debug.Write(e.Message);
                }
            }

            var client = new SmtpClient(SmtpHostname) { UseDefaultCredentials = true };

            try
            {
                client.Send(mailMessage);
                mailMessage.Dispose();
                foreach (var attachmentFile in attachmentFiles)
                {
                    File.Delete(Path.GetFileName(attachmentFile.Name));
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        /// <summary>
        /// Send email with attachment
        /// </summary>
        /// <param name="fileName"></param>       
        /// <param name="toEmailAddresses"></param>
        /// <param name="ccEmailAddresses"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// /// <param name="textFileGenerator"></param>
        /// <param name="IsBodyHtml"></param>
        public static void SendEmailWithAttachment(string fileName, string[] toEmailAddresses, string[] ccEmailAddresses, string subject, string body, bool IsBodyHtml = false)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EmailFromAddress);
            foreach (string email in toEmailAddresses)
            {
                mail.To.Add(email);
            }
            foreach (string cc in ccEmailAddresses)
            {
                mail.CC.Add(cc);
            }
            mail.Subject = subject ?? "CSV";
            mail.Body = body ?? string.Empty;
            mail.IsBodyHtml = IsBodyHtml;
            try
            {
                if (fileName != null)
                {
                    var pathNameOrdered = fileName;
                    var attachment = new Attachment(pathNameOrdered, MediaTypeNames.Application.Octet);
                    var disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(pathNameOrdered);
                    disposition.ModificationDate = File.GetLastWriteTime(pathNameOrdered);
                    disposition.ReadDate = File.GetLastAccessTime(pathNameOrdered);
                    disposition.FileName = Path.GetFileName(pathNameOrdered);
                    disposition.Size = new FileInfo(pathNameOrdered).Length;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    mail.Attachments.Add(attachment);
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                RollingLogger.WriteToTrackingViaEmailApiLogs($"Unable to send the email. Message: {e.Message}", ELogTypes.Error);
            }
            SmtpClient client = new SmtpClient(SmtpHostname) { UseDefaultCredentials = true };
            try
            {
                client.Send(mail);
                RollingLogger.WriteToTrackingViaEmailApiLogs($"Sent email for file - {fileName}", ELogTypes.Information);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                RollingLogger.WriteToTrackingViaEmailApiLogs($"Unable to send the email. Message- {ex.Message}", ELogTypes.Error);
            }
            finally
            {
                mail.Dispose();
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }

        private const string EmailFromAddress = "xCab@capitaltransport.com.au";
        private const string SmtpHostname = "smtprelay.challengelogistics.com.au";
    }
}
