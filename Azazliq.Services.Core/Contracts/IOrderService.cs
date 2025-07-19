using Azaliq.ViewModels.CartItems;
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

        Task<OrderDetailsViewModel?> GetOrderByIdAsync(int orderId);

        Task PlaceOrderAsync(OrderDetailsViewModel inputModel);
        // Admins controls
        Task<List<OrderViewModel>> GetAllOrdersAsync();

        Task<bool> ChangeStatusAsync(int orderId, string newStatus);

        Task<DeleteOrderModel?> GetOrderByIdForDeleteAsync(int orderId);
        Task<bool> SoftDeleteOrderAsync(int orderId);


    }

}
