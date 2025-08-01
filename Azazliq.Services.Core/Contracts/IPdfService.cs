using Azaliq.Data.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Services.Core.Contracts
{
    public interface IPdfService
    {
        Task<byte[]> GenerateOrderPdfWithQrAsync(Order order);
    }

}
