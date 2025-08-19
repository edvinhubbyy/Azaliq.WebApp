using Azaliq.Data.Configurations;
using Azaliq.Data.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Azaliq.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItem> CartItems { get; set; } = null!;

        public virtual DbSet<Order> Orders { get; set; } = null!;

        public virtual DbSet<Category> Categories { get; set; } = null!;

        public virtual DbSet<OrderProduct> OrdersProducts { get; set; } = null!;

        public virtual DbSet<Product> Products { get; set; } = null!;

        public virtual DbSet<ProductTag> ProductsTags { get; set; } = null!;

        public virtual DbSet<Store> StoresLocations { get; set; } = null!;

        public virtual DbSet<Favorite> Favorites { get; set; } = null!;

        public virtual DbSet<Manager> Managers { get; set; } = null!;
        
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        // Archives

        public DbSet<ArchivedUser> ArchivedUsers { get; set; } = null!;

        public DbSet<ArchivedOrder> ArchivedOrders { get; set; } = null!;

        public DbSet<ArchivedOrderProduct> ArchivedOrderProducts { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
