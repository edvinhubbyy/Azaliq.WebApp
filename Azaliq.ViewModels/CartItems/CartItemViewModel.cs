using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.ViewModels.CartItems
{
    public class CartItemViewModel
    {
        public int Id { get; set; }  // CartItem ID, useful for unique identification
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductImageUrl { get; set; }  // Nullable, in case no image
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Subtotal => Price * Quantity;
    }
}
