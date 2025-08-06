using Azaliq.Data.Models.Models;

namespace Azaliq.Services.Core.Contracts
{
    public interface IPdfService
    {
        Task<byte[]> GenerateOrderPdfWithQrAsync(Order order);
    }

}
