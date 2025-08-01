using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels._2fa;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService : IEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;

    public EmailService(IEmailSender emailSender, IConfiguration configuration)
    {
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public async Task SendAsync(EmailViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.ToEmail) ||
            string.IsNullOrWhiteSpace(model.Subject) ||
            string.IsNullOrWhiteSpace(model.Message))
        {
            throw new ArgumentException("Email, subject, and message must be provided.");
        }

        await _emailSender.SendEmailAsync(model.ToEmail, model.Subject, model.Message);
    }

    public async Task SendEmailWithAttachmentAsync(string email, string subject, string message, byte[] attachmentData, string attachmentFileName)
    {
        var client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
        var from = new EmailAddress(_configuration["SendGrid:FromEmail"], "Azaliq Flower Shop");
        var to = new EmailAddress(email);

        var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

        msg.AddAttachment(attachmentFileName, Convert.ToBase64String(attachmentData));

        await client.SendEmailAsync(msg);
    }
}