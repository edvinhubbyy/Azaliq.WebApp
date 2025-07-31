using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels._2fa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Azaliq.Services.Core
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
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
    }
}
