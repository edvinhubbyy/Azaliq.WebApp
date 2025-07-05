using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.ViewModels.Store
{
    public class StoreLocationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string GoogleMapsUrl { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
