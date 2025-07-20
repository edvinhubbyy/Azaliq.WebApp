using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("ApplicationUser represents a user in the application.")]
    public class ApplicationUser : IdentityUser
    {
        [Comment("Full name of the user.")]
        public string FullName { get; set; } = null!;

        [Comment("Email address of the user.")]
        public string? Address { get; set; }

        [Comment("Phone number of the user.")]
        public ICollection<Order> Orders { get; set; }
            = new HashSet<Order>();
    }
}