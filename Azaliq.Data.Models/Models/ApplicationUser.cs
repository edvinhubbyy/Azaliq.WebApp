using Microsoft.AspNetCore.Identity;

namespace Azaliq.Data.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;

        public string? Address { get; set; } 

        public ICollection<Order> Orders { get; set; }
            = new HashSet<Order>();
    }
}