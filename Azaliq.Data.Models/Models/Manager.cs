using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("Manager in the system")]
    public class Manager
    {
        [Comment("Manager identifier")]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [Comment("Manager's user entity")]
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        public virtual ICollection<Store> ManagedStores { get; set; }
            = new HashSet<Store>();
    }
}
