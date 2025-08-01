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
                .Include(o => o.User)
                .Include(o => o.PickupStore) // Include pickup store info
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

                FullName = order.FullName ?? order.User.FullName,
                Email = order.Email ?? order.User.Email,
                Phone = order.Phone,
                CountryCode = order.CountryCode,

                // Address only for courier orders
                Address = order.DeliveryOption == DeliveryOptions.Courier ? order.DeliveryAddress : string.Empty,
                City = order.DeliveryOption == DeliveryOptions.Courier ? order.City : string.Empty,
                ZipCode = order.DeliveryOption == DeliveryOptions.Courier ? order.ZipCode : string.Empty,

                PickupStoreId = order.PickupStoreId,
                PickupStoreUrl = order.PickupStore != null
                    ? $"/Store/Map/{order.PickupStore.Id}"
                    : null,
                
                DeliveryOption = order.DeliveryOption.ToString(),

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

        public async Task<Order?> GetOrderEntityByIdAsync(int orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.Products)
                .ThenInclude(op => op.Product)  // Include the actual Product info inside OrderProducts
                .Include(o => o.PickupStore)       // Include PickupStore details if any
                .FirstOrDefaultAsync(o => o.Id == orderId && !o.IsDeleted);
        }


        public async Task<Order> PlaceOrderAsync(OrderDetailsViewModel inputModel, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));

            var cartItems = await _dbContext.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                throw new InvalidOperationException("Cart is empty.");

            // Validate stock before placing order
            foreach (var item in cartItems)
            {
                if (item.Quantity > item.Product.Quantity)
                    throw new InvalidOperationException($"Not enough stock for product: {item.Product.Name}");
            }

            var deliveryOption = Enum.Parse<DeliveryOptions>(inputModel.DeliveryOption);

            // Validate pickup store if pickup delivery option
            if (deliveryOption == DeliveryOptions.PickupFromStore)
            {
                if (inputModel.PickupStoreId == null)
                    throw new InvalidOperationException("Pickup store must be selected for pickup orders.");

                var storeExists = await _dbContext.StoresLocations.AnyAsync(s => s.Id == inputModel.PickupStoreId);
                if (!storeExists)
                    throw new InvalidOperationException("Invalid pickup store selected.");
            }

            var order = new Order
            {
                UserId = userId,
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                Phone = inputModel.Phone,
                CountryCode = inputModel.CountryCode,

                DeliveryOption = deliveryOption,
                PickupStoreId = inputModel.PickupStoreId,

                // Use empty string instead of null if not courier
                DeliveryAddress = deliveryOption == DeliveryOptions.Courier ? inputModel.Address : string.Empty,
                City = deliveryOption == DeliveryOptions.Courier ? inputModel.City : string.Empty,
                ZipCode = deliveryOption == DeliveryOptions.Courier ? inputModel.ZipCode : string.Empty,

                OrderDate = DateTime.UtcNow,
                IsDeleted = false,
                Status = OrderStatus.Pending,
                Products = new List<OrderProduct>()
            };

            foreach (var item in cartItems)
            {
                order.Products.Add(new OrderProduct
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });

                // Decrease stock
                item.Product.Quantity -= item.Quantity;
            }

            _dbContext.Orders.Add(order);
            _dbContext.CartItems.RemoveRange(cartItems);

            await _dbContext.SaveChangesAsync();

            return order;
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