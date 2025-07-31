using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Azaliq.Services.Core
{
    public class CustomEmailSender : IEmailSender
    {
        private readonly string _sendGridApiKey;

        public CustomEmailSender(string sendGridApiKey)
        {
            if (string.IsNullOrWhiteSpace(sendGridApiKey))
            {
                throw new ArgumentException("SendGrid API key must be provided.", nameof(sendGridApiKey));
            }

            _sendGridApiKey = sendGridApiKey;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject cannot be null or empty.", nameof(subject));
            if (string.IsNullOrWhiteSpace(htmlMessage))
                throw new ArgumentException("Message cannot be null or empty.", nameof(htmlMessage));

            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("metodievivan672@gmail.com", "Azaliq");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlMessage);
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                throw new InvalidOperationException(
                    $"Failed to send email via SendGrid. StatusCode: {response.StatusCode}. Body: {responseBody}");
            }
        }
    }
}