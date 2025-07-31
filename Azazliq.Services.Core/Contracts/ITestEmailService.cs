using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Services.Core.Contracts
{

    public interface ITestEmailService
    {
        Task SendTestEmailAsync(string toEmail);
    }
}
