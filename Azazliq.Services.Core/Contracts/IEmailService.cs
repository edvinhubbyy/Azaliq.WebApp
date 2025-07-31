using Azaliq.ViewModels._2fa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Services.Core.Contracts
{
    public interface IEmailService
    {
        Task SendAsync(EmailViewModel model);
    }
}
