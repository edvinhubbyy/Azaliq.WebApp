﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.ViewModels.Product
{
    public class ProductIndexViewModel
    {

        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }
        
        public int Quantity { get; set; }

        public bool IsSameDayDeliveryAvailable { get; set; }

    }
}
