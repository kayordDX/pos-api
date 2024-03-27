
using System.Net;
using Kayord.Pos.Config;
using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Kayord.Pos.Services;

public class EmailService : IEmailSender
{
    private readonly EmailConfig _emailConfig;
    private readonly AppDbContext _dbContext;
    public EmailService(IOptions<EmailConfig> emailConfig, AppDbContext dbContext)
    {
        _emailConfig = emailConfig.Value;
        _dbContext = dbContext;
    }

    public async Task SendEmailAsync(string toEmail, string toName, string subject, string message, AttachmentCollection? attachments = null)
    {
        List<MailboxAddress> emails = new() { new MailboxAddress(toName, toEmail) };
        await SendAsync(emails, subject, message, attachments);
    }

    private async Task SendAsync(List<MailboxAddress> emails, string subject, string message, AttachmentCollection? attachments = null)
    {
        if (string.IsNullOrEmpty(_emailConfig.Email))
        {
            throw new Exception("Email is empty in config");
        }

        var email = string.Join(";", emails.Select(x => x.Address));

        var log = await _dbContext.EmailLog.AddAsync(new EmailLog { Email = email, Subject = subject, Message = message });

        var mail = new MimeMessage();
        mail.From.Add(new MailboxAddress(_emailConfig.Name, _emailConfig.Email));
        mail.To.AddRange(emails);
        mail.Subject = subject;

        var builder = new BodyBuilder
        {
            TextBody = message
        };

        if (attachments != null)
        {
            foreach (var attachment in attachments)
            {
                builder.Attachments.Add(attachment);
            }
        }

        mail.Body = builder.ToMessageBody();

        using var client = new SmtpClient();

        client.Connect(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);

        // Note: only needed if the SMTP server requires authentication
        client.Authenticate(_emailConfig.Email, _emailConfig.Password);

        client.Send(mail);
        client.Disconnect(true);


        log.Entity.IsSent = true;
        await _dbContext.SaveChangesAsync();
    }
}