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
        private readonly ApplicationDbContext _dbContext;
        
        public OrderService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<OrderViewModel>> GetOrdersByUserIdAsync(string userId)
        {
            var order = await _dbContext.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
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

        public async Task<OrderDetailsViewModel?> GetOrderByIdAsync(int orderId)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Products)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User) // assuming ApplicationUser has the extra properties
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            var model = new OrderDetailsViewModel
            {
                OrderId = order.Id,
                OrderNumber = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                IsDelivery = order.IsDelivery,
                DeliveryAddress = order.DeliveryAddress,

                FullName = order.FullName ?? order.User.FullName,  // from order or user
                Email = order.Email ?? order.User.Email,
                Phone = order.Phone,
                CountryCode = order.CountryCode.ToString(),
                Address = order.DeliveryAddress,
                City = order.City,
                ZipCode = order.ZipCode,

                Items = order.Products.Select(oi => new OrderItemViewModel
                {
                    OrderItemId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Product.Price,
                    ImageUrl = oi.Product.ImageUrl ?? NoImageUrl,
                }).ToList()
            };

            return model;
        }


        public async Task PlaceOrderAsync(OrderDetailsViewModel inputModel, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
            }

            var cartItems = await _dbContext.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return; // No items in cart
            }

            var order = new Order
            {
                UserId = userId, // <- assign userId here
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                Phone = inputModel.Phone,
                DeliveryAddress = inputModel.Address,
                City = inputModel.City,
                ZipCode = inputModel.ZipCode,
                OrderDate = DateTime.UtcNow,
                IsDeleted = false,
                Status = OrderStatus.Pending
            };

            foreach (var item in cartItems)
            {
                order.Products.Add(new OrderProduct
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            _dbContext.Orders.Add(order);
            _dbContext.CartItems.RemoveRange(cartItems);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status != null ? o.Status.ToString() : "Unknown",
                    Items = o.Products
                        .Where(i => i.Product != null)
                        .Select(i => new OrderItemViewModel
                        {
                            ProductName = i.Product!.Name ?? "Unknown product",
                            ImageUrl = i.Product!.ImageUrl ?? NoImageUrl,
                            Price = i.Product!.Price,
                            Quantity = i.Quantity
                        })
                        .ToList()
                })
                .ToListAsync();
        }


        public async Task<bool> ReorderAsync(int orderId, string userId)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
                return false;

            foreach (var item in order.Products)
            {
                var existingCartItem = await _dbContext.CartItems
                    .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == item.ProductId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += item.Quantity;
                    _dbContext.CartItems.Update(existingCartItem);
                }
                else
                {
                    _dbContext.CartItems.Add(new CartItem
                    {
                        UserId = userId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }



        public async Task<bool> ChangeStatusAsync(int orderId, string newStatus)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            if (Enum.TryParse(typeof(OrderStatus), newStatus, out var status))
            {
                order.Status = (OrderStatus)status;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<DeleteOrderModel?> GetOrderByIdForDeleteAsync(int orderId)
        {
            var order = await _dbContext.Orders
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
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return false;

            order.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }

    }

}
