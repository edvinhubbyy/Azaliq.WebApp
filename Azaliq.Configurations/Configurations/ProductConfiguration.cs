using Azaliq.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Azaliq.GCommon.ValidationConstants.Product;

namespace Azaliq.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            entity
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Description)
                .HasMaxLength(DescriptionMaxLength);

            entity.Property(p => p.ImageUrl)
                .HasMaxLength(ImageUrlMaxLength);

            entity.Property(p => p.Price)
                .IsRequired();

            entity.Property(p => p.IsAvailable)
                .IsRequired();

            entity
                .HasOne(r => r.Category)
                .WithMany(r => r.Products)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.IsSameDayDeliveryAvailable)
                .IsRequired();

            entity.HasMany(p => p.Tags)
                .WithMany(t => t.Products);

            entity.HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasQueryFilter(p => p.IsDeleted == false);


            entity.HasData(GetSeedProducts());

        }

        private static Product[] GetSeedProducts()
        {
            return new[]
            {
                new Product
                {
                    Id = 1,
                    Name = "Red Rose Bouquet",
                    Description = "A beautiful bouquet of red roses",
                    Price = 49.99m,
                    CategoryId = 1,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 2,
                    Name = "Yellow Tulips",
                    Description = "Bright and cheerful yellow tulips",
                    Price = 39.99m,
                    CategoryId = 2,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1499744937866-d9e8f7e0ecf4?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 3,
                    Name = "White Lilies",
                    Description = "Elegant white lilies perfect for any occasion",
                    Price = 44.99m,
                    CategoryId = 3,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1501004318641-b39e6451bec6?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 4,
                    Name = "Pink Orchids",
                    Description = "Delicate pink orchids",
                    Price = 59.99m,
                    CategoryId = 4,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1497551060073-4c5ab6435f5f?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 5,
                    Name = "Sunflower Basket",
                    Description = "Bright sunflower basket",
                    Price = 35.00m,
                    CategoryId = 5,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1500534623283-312aade485b7?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 6,
                    Name = "Carnation Mix",
                    Description = "Colorful mix of carnations",
                    Price = 29.99m,
                    CategoryId = 6,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1494976388531-d1058494cdd8?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 7,
                    Name = "Daisy Delight",
                    Description = "Fresh daisies",
                    Price = 24.99m,
                    CategoryId = 7,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1465188035480-ff3f285f3bce?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 8,
                    Name = "Peony Love",
                    Description = "Soft pink peonies",
                    Price = 54.99m,
                    CategoryId = 8,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1528825871115-3581a5387919?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 9,
                    Name = "Chrysanthemum Charm",
                    Description = "Charming chrysanthemums",
                    Price = 27.99m,
                    CategoryId = 9,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1504384308090-c894fdcc538d?auto=format&fit=crop&w=500&q=60"
                },
                new Product
                {
                    Id = 10,
                    Name = "Gardenia Glow",
                    Description = "Fragrant gardenias",
                    Price = 39.99m,
                    CategoryId = 10,
                    ImageUrl =
                        "https://images.unsplash.com/photo-1472214103451-9374bd1c798e?auto=format&fit=crop&w=500&q=60"
                }
            };



        }
    }
}
