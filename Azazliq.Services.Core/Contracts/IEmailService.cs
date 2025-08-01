using System.Net.Mail;
using Azaliq.ViewModels._2fa;

namespace Azaliq.Services.Core.Contracts
{
    public interface IEmailService
    {
        Task SendAsync(EmailViewModel model);
        Task SendEmailWithAttachmentAsync(string email, string subject, string message, byte[] attachmentData, string attachmentFileName);
    }

}
