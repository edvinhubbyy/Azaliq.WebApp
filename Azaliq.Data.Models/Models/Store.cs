using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string GoogleMapsUrl { get; set; } = null!;
        
        public string Address { get; set; } = null!;

        public Guid? ManagerId { get; set; }

        public virtual Manager? Manager { get; set; }

        public string? Phone { get; set; }
    }
}