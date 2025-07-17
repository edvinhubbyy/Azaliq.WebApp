using Azaliq.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Services.Core.Contracts
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetOrdersByUserIdAsync(string userId);

        Task PlaceOrderAsync(string userId, bool isDelivery, string? deliveryAddress);

        // Admins controls
        Task<List<OrderViewModel>> GetAllOrdersAsync();

        Task<bool> ChangeStatusAsync(int orderId, string newStatus);

        Task<bool> DeleteOrderAsync(int orderId);
    }

}
