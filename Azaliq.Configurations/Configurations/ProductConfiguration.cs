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
                // — Roses (CategoryId = 1)
                new Product
                {
                    Id = 1, Name = "Classic Red Roses", Description = "A bouquet of long-stemmed red roses",
                    Price = 49.99m, CategoryId = 1,
                    ImageUrl =
                        "https://images.pexels.com/photos/931162/pexels-photo-931162.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 2, Name = "Pink Garden Roses", Description = "Soft pink garden roses", Price = 54.99m,
                    CategoryId = 1,
                    ImageUrl =
                        "https://images.pexels.com/photos/1073048/pexels-photo-1073048.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 3, Name = "White Avalanche Roses", Description = "Crisp white avalanche roses", Price = 59.99m,
                    CategoryId = 1,
                    ImageUrl =
                        "https://images.pexels.com/photos/213222/pexels-photo-213222.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Tulips (CategoryId = 2)
                new Product
                {
                    Id = 4, Name = "Yellow Tulip Bundle", Description = "Bright yellow tulips in a bundle",
                    Price = 39.99m, CategoryId = 2,
                    ImageUrl =
                        "https://images.pexels.com/photos/934070/pexels-photo-934070.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 5, Name = "Red & White Tulips", Description = "Mixed red and white tulips", Price = 42.99m,
                    CategoryId = 2,
                    ImageUrl =
                        "https://images.pexels.com/photos/5857509/pexels-photo-5857509.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 6, Name = "Pink Tulips", Description = "Soft pink tulips standing tall", Price = 37.99m,
                    CategoryId = 2,
                    ImageUrl =
                        "https://images.pexels.com/photos/315638/pexels-photo-315638.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Lilies (CategoryId = 3)
                new Product
                {
                    Id = 7, Name = "White Stargazer Lilies", Description = "Fragrant white stargazer lilies",
                    Price = 44.99m, CategoryId = 3,
                    ImageUrl =
                        "https://images.pexels.com/photos/1460886/pexels-photo-1460886.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 8, Name = "Orange Asiatic Lilies", Description = "Vibrant orange asiatic lilies",
                    Price = 46.99m, CategoryId = 3,
                    ImageUrl =
                        "https://images.pexels.com/photos/248526/pexels-photo-248526.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 9, Name = "Pink Oriental Lilies", Description = "Delicate pink oriental lilies",
                    Price = 48.99m, CategoryId = 3,
                    ImageUrl =
                        "https://images.pexels.com/photos/1544336/pexels-photo-1544336.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Orchids (CategoryId = 4)
                new Product
                {
                    Id = 10, Name = "Phalaenopsis Orchids", Description = "Elegant white Phalaenopsis orchids",
                    Price = 59.99m, CategoryId = 4,
                    ImageUrl =
                        "https://images.pexels.com/photos/931180/pexels-photo-931180.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 11, Name = "Pink Moth Orchids", Description = "Soft pink moth orchids", Price = 62.99m,
                    CategoryId = 4,
                    ImageUrl =
                        "https://images.pexels.com/photos/1637359/pexels-photo-1637359.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 12, Name = "Purple Cymbidium Orchids", Description = "Rich purple cymbidium orchids",
                    Price = 64.99m, CategoryId = 4,
                    ImageUrl =
                        "https://images.pexels.com/photos/257280/pexels-photo-257280.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Sunflowers (CategoryId = 5)
                new Product
                {
                    Id = 13, Name = "Single Sunflower Stem", Description = "Bright single sunflower stem",
                    Price = 25.00m, CategoryId = 5,
                    ImageUrl =
                        "https://images.pexels.com/photos/414274/pexels-photo-414274.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 14, Name = "Sunflower Bouquet", Description = "Cheerful bouquet of sunflowers", Price = 35.00m,
                    CategoryId = 5,
                    ImageUrl =
                        "https://images.pexels.com/photos/1030936/pexels-photo-1030936.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 15, Name = "Mini Sunflower Vase", Description = "Compact sunflowers in a glass vase",
                    Price = 29.99m, CategoryId = 5,
                    ImageUrl =
                        "https://images.pexels.com/photos/349758/pexels-photo-349758.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Carnations (CategoryId = 6)
                new Product
                {
                    Id = 16, Name = "Red Carnations", Description = "Bright red carnations", Price = 29.99m,
                    CategoryId = 6,
                    ImageUrl =
                        "https://images.pexels.com/photos/2898825/pexels-photo-2898825.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 17, Name = "White Carnations", Description = "Pure white carnations", Price = 31.99m,
                    CategoryId = 6,
                    ImageUrl =
                        "https://images.pexels.com/photos/1299898/pexels-photo-1299898.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 18, Name = "Pink Carnations", Description = "Soft pink carnations", Price = 27.99m,
                    CategoryId = 6,
                    ImageUrl =
                        "https://images.pexels.com/photos/4147446/pexels-photo-4147446.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Daisies (CategoryId = 7)
                new Product
                {
                    Id = 19, Name = "Classic Daisies", Description = "Fresh classic daisies", Price = 24.99m,
                    CategoryId = 7,
                    ImageUrl =
                        "https://images.pexels.com/photos/414171/pexels-photo-414171.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 20, Name = "Gerbera Daisies", Description = "Vibrant gerbera daisies", Price = 22.99m,
                    CategoryId = 7,
                    ImageUrl =
                        "https://images.pexels.com/photos/462117/pexels-photo-462117.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 21, Name = "Yellow Daisies", Description = "Sunny yellow daisies", Price = 20.99m,
                    CategoryId = 7,
                    ImageUrl =
                        "https://images.pexels.com/photos/413195/pexels-photo-413195.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Peonies (CategoryId = 8)
                new Product
                {
                    Id = 22, Name = "Pink Peony Cluster", Description = "Full cluster of pink peonies", Price = 54.99m,
                    CategoryId = 8,
                    ImageUrl =
                        "https://images.pexels.com/photos/931177/pexels-photo-931177.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 23, Name = "White Peonies", Description = "Elegant white peonies", Price = 56.99m,
                    CategoryId = 8,
                    ImageUrl =
                        "https://images.pexels.com/photos/1231265/pexels-photo-1231265.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 24, Name = "Coral Peonies", Description = "Soft coral-colored peonies", Price = 58.99m,
                    CategoryId = 8,
                    ImageUrl =
                        "https://images.pexels.com/photos/991447/pexels-photo-991447.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Chrysanthemums (CategoryId = 9)
                new Product
                {
                    Id = 25, Name = "Yellow Chrysanthemums", Description = "Sunny yellow chrysanthemums",
                    Price = 27.99m, CategoryId = 9,
                    ImageUrl =
                        "https://images.pexels.com/photos/675951/pexels-photo-675951.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 26, Name = "Purple Chrysanthemums", Description = "Rich purple chrysanthemums", Price = 29.99m,
                    CategoryId = 9,
                    ImageUrl =
                        "https://images.pexels.com/photos/116393/pexels-photo-116393.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 27, Name = "White Chrysanthemums", Description = "Crisp white chrysanthemums", Price = 25.99m,
                    CategoryId = 9,
                    ImageUrl =
                        "https://images.pexels.com/photos/939222/pexels-photo-939222.jpeg?auto=compress&cs=tinysrgb&h=800"
                },

                // — Gardenias (CategoryId = 10)
                new Product
                {
                    Id = 28, Name = "Classic Gardenias", Description = "Fragrant classic gardenias", Price = 39.99m,
                    CategoryId = 10,
                    ImageUrl =
                        "https://images.pexels.com/photos/937400/pexels-photo-937400.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 29, Name = "White Gardenia Bloom", Description = "Single white gardenia bloom", Price = 41.99m,
                    CategoryId = 10,
                    ImageUrl =
                        "https://images.pexels.com/photos/264727/pexels-photo-264727.jpeg?auto=compress&cs=tinysrgb&h=800"
                },
                new Product
                {
                    Id = 30, Name = "Gardenia Leaves & Flower", Description = "Gardenia flowers with green leaves",
                    Price = 43.99m, CategoryId = 10,
                    ImageUrl =
                        "https://images.pexels.com/photos/206420/pexels-photo-206420.jpeg?auto=compress&cs=tinysrgb&h=800"
                }
            };
        }




    }
}

