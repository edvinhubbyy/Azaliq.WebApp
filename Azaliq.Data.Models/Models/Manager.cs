using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.Data.Models.Models
{
    [Comment("Manager in the system")]
    public class Manager
    {
        [Comment("Manager identifier")]
        public Guid Id { get; set; }

        [Comment("Manager's user entity")]
        public string UserId { get; set; } = null!;

        [Comment("User's entity")]
        public virtual IdentityUser User { get; set; } = null!;

        [Comment("Managed store's entity")]
        public virtual ICollection<Store> ManagedStores { get; set; }
            = new HashSet<Store>();

        [Comment("IsDeleted filtering entity")]
        public bool IsDeleted { get; set; }
    }
}
