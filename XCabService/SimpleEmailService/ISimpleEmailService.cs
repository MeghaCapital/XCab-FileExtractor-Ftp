namespace XCabService.SimpleEmailService;

public interface ISimpleEmailService
{
    Task<bool> SendHtmlEmail(List<string> recipientEmails, string subject, string htmlBody, List<SimpleAttachment> attachments = null); 
}