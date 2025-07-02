﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Data.Models.Models
{
    public class StoreLocation
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string GoogleMapsUrl { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Phone { get; set; }
    }

}
