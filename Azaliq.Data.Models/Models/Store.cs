using Azaliq.Data.Models.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("Store entity represents a store in the system.")]
    public class Store
    {
        [Comment("ID of the Store")]
        public int Id { get; set; }

        [Comment("Name of the Store")]
        public string Name { get; set; } = null!;

        [Comment("Google Maps URL of the Store")]
        public string GoogleMapsUrl { get; set; } = null!;

        [Comment("Address of the Store")]
        public string Address { get; set; } = null!;

        [Comment("Phone number of the Store")]
        public string PhoneNumber { get; set; } = null!;

        [Comment("Country code of the Store")]
        public CountryCode CountryCode { get; set; }

        public ICollection<Order> PickupOrders { get; set; } 
            = new HashSet<Order>();

        [Comment("Indicates if the Store is deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}