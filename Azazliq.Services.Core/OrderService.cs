using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Data.Models.Models.Enum;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Order;
using Microsoft.EntityFrameworkCore;
using static Azaliq.GCommon.ValidationConstants.General;

namespace Azaliq.Services.Core
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderViewModel>> GetOrdersByUserIdAsync(string userId)
        {
            var order = await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status.ToString(),
                    Items = o.Products.Select(i => new OrderItemViewModel
                    {
                        
                        ProductName = i.Product.Name,
                        ImageUrl = i.Product.ImageUrl ?? NoImageUrl,
                        Price = i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList()
                })
                .ToListAsync();

            return order;
        }

        public async Task PlaceOrderAsync(string userId, bool isDelivery, string? deliveryAddress)
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any()) return;

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                IsDelivery = isDelivery,
                DeliveryAddress = isDelivery ? deliveryAddress : null,
                TotalAmount = cartItems.Sum(i => i.Product.Price * i.Quantity),
                Products = cartItems.Select(i => new OrderProduct
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _context.Orders.AddAsync(order);

            // Clear cart
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status.ToString(),
                    Items = o.Products.Select(i => new OrderItemViewModel
                    {
                        ProductName = i.Product.Name,
                        ImageUrl = i.Product.ImageUrl ?? NoImageUrl,
                        Price = i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<bool> ChangeStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            if (Enum.TryParse(typeof(OrderStatus), newStatus, out var status))
            {
                order.Status = (OrderStatus)status;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<DeleteOrderModel?> GetOrderByIdForDeleteAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Products)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            return new DeleteOrderModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                Items = order.Products.Select(op => new OrderItemViewModel
                {
                    ProductName = op.Product.Name,
                    ImageUrl = op.Product.ImageUrl ?? "/images/no-image.jpg",
                    Price = op.Product.Price,
                    Quantity = op.Quantity
                }).ToList()
            };
        }

        public async Task<bool> SoftDeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return false;

            order.IsDeleted = true;

            await _context.SaveChangesAsync();
            return true;
        }

    }

}
