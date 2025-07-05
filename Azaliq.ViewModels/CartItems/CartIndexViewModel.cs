using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.ViewModels.CartItems
{
    public class CartIndexViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}
