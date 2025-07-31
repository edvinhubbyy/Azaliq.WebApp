using Azaliq.Services.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Azaliq.Services.Core
{
    public class TestEmailService : ITestEmailService
    {
        private readonly IEmailSender _emailSender;

        public TestEmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendTestEmailAsync(string toEmail)
        {
            string subject = "Test Email from Azaliq";
            string message = "<h1>This is a test email</h1><p>If you received this, the email service works!</p>";

            await _emailSender.SendEmailAsync(toEmail, subject, message);
        }
    }
}
