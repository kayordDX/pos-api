
using System.Net;
using System.Net.Mail;
using Kayord.Pos.Config;
using Microsoft.Extensions.Options;

namespace Kayord.Pos.Services;

public class EmailService : IEmailSender
{
    private readonly EmailConfig _emailConfig;
    public EmailService(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        if (string.IsNullOrEmpty(_emailConfig.Email))
        {
            throw new Exception("Email is empty in config");
        }

        var client = new SmtpClient(_emailConfig.Host, _emailConfig.Port)
        {
            EnableSsl = _emailConfig.EnableSsl,
            Credentials = new NetworkCredential(_emailConfig.Email, _emailConfig.Password)
        };

        return client.SendMailAsync(new MailMessage(from: _emailConfig.Email, to: email, subject, message));
    }
}