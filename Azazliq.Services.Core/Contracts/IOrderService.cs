﻿using Azaliq.Data.Models.Models;
using Azaliq.ViewModels.Order;

namespace Azaliq.Services.Core.Contracts
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetOrdersByUserIdAsync(string userId);

        Task<OrderDetailsViewModel?> GetOrderByIdAsync(int orderId);

        Task<Order?> GetOrderEntityByIdAsync(int orderId);

        Task<Order> PlaceOrderAsync(OrderDetailsViewModel model, string userId);

        // Admins controls
        Task<List<OrderViewModel>> GetAllOrdersAsync();

        Task<bool> ReorderAsync(int orderId, string userId);

        Task<bool> ChangeStatusAsync(int orderId, string newStatus);

        Task<DeleteOrderModel?> GetOrderByIdForDeleteAsync(int orderId);
        Task<bool> SoftDeleteOrderAsync(int orderId);


    }

}
