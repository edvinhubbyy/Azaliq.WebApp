using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.Data.Models.Models.Enum
{
    public enum OrderStatus
    {
        Pending = 0,         // Order placed, not yet processed
        Confirmed = 1,       // Confirmed by store, preparing flowers
        ReadyForPickup = 2,  // Prepared, waiting for customer
        OutForDelivery = 3,  // In delivery van (if IsDelivery = true)
        Completed = 4,       // Picked up or delivered
        Cancelled = 5,       // Cancelled by customer or florist
    }
}
