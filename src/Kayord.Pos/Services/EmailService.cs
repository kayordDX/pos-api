
using System.Net;
using System.Net.Mail;
using Kayord.Pos.Config;
using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.Extensions.Options;

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

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        if (string.IsNullOrEmpty(_emailConfig.Email))
        {
            throw new Exception("Email is empty in config");
        }

        var log = await _dbContext.EmailLog.AddAsync(new EmailLog { Email = email, Subject = subject, Message = message });

        var client = new SmtpClient(_emailConfig.Host, _emailConfig.Port)
        {
            EnableSsl = _emailConfig.EnableSsl,
            Credentials = new NetworkCredential(_emailConfig.Email, _emailConfig.Password)
        };

        MailMessage msg = new(from: _emailConfig.Email, to: email, subject, message);
        // await using var stream = new MemoryStream();
        // msg.Attachments.Add(new Attachment(stream, "application/pdf"));

        await client.SendMailAsync(msg);

        log.Entity.IsSent = true;
        await _dbContext.SaveChangesAsync();
    }
}