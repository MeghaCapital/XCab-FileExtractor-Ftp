using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace XCabService.SimpleEmailService;

public class SimpleEmailService : ISimpleEmailService
{
    private const string _SMTPSERVER = "smtp.postmarkapp.com";
    private const int _PORT = 587;
    private const string _USERNAME = "61d21d7f-31c9-40c6-bc70-2232ae82adfb";
    private const string _PASSWORD = "61d21d7f-31c9-40c6-bc70-2232ae82adfb";
    private const bool _USESSL = true;
    private const string _FROM = "xcab@ilogix.au";
    private const string _FROMNAME = "Xcab System";

    private ILogger<SimpleEmailService> _logger;

    public SimpleEmailService(ILogger<SimpleEmailService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendHtmlEmail(List<string> recipientEmails, string subject, string htmlBody,
        List<SimpleAttachment> attachments = null)
    {
        bool success = false;

        MailMessage msg = new MailMessage();

        msg.Subject = subject;
        msg.IsBodyHtml = true;

        var view = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
        msg.AlternateViews.Add(view);

        msg.From = new MailAddress(_FROM, _FROMNAME);
        msg.To.Add(string.Join(",", recipientEmails));

        using var sc = new SmtpClient(_SMTPSERVER);

        sc.Credentials = new System.Net.NetworkCredential(_USERNAME, _PASSWORD);
        sc.Port = _PORT;
        sc.EnableSsl = _USESSL;

        try
        {
            await sc.SendMailAsync(msg);
            success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email");
        }

        return success;
    }
}