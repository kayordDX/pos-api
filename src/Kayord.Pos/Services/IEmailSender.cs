using MimeKit;

namespace Kayord.Pos.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string toEmail, string toName, string subject, string message, AttachmentCollection? attachments = null);
}