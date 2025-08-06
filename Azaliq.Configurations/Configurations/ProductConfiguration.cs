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
                    Name = "Rose",
                    Description = "Classic long-stemmed red roses symbolizing love, romance, and admiration. A timeless floral gift perfect for Valentine's Day,    anniversaries,     or  just     to say 'I love you.'",
                    ImageUrl = "https://images.unsplash.com/photo-1496062031456-07b8f162a322?               q=80&w=765&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 10.00m,
                    Quantity = 100,
                    IsAvailable = false,
                    CategoryId = 1,
                    IsSameDayDeliveryAvailable = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Tulip",
                    Description = "Bright and cheerful tulips in a rainbow of colors. These springtime favorites are known for their simple beauty and are ideal for        celebrating    fresh  beginnings and happiness.",
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1677620614560-5f1b32416563?             q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 9.99m,
                    Quantity = 150,
                    IsAvailable = false,
                    CategoryId = 2,
                    IsSameDayDeliveryAvailable = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 3,
                    Name = "Sunflower",
                    Description = "Large, golden sunflowers that exude warmth, happiness, and positivity. A joyful bloom that brightens up any space and symbolizes     adoration   and       loyalty.",
                    ImageUrl = "https://images.unsplash.com/photo-1551945326-df678a97c3af?              q=80&w=735&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 14.00m,
                    Quantity = 80,
                    IsAvailable = false,
                    CategoryId = 5,
                    IsSameDayDeliveryAvailable = false,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 4,
                    Name = "Orchid",
                    Description = "Exotic and elegant, orchids are known for their long-lasting beauty and sophisticated look. Perfect for home décor, special gifts, or    as     a       centerpiece for luxury events.",
                    ImageUrl = "https://images.unsplash.com/photo-1610397648930-477b8c7f0943?               q=80&w=730&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 25.00m,
                    Quantity = 60,
                    IsAvailable = false,
                    CategoryId = 4,
                    IsSameDayDeliveryAvailable = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 5,
                    Name = "Lily",
                    Description = "Fragrant and graceful lilies available in white and pink shades. Popular for weddings, religious events, and sympathy arrangements due   to    their      serene and elegant charm.",
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1676068243733-df1880c2aef8?             q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 18.50m,
                    Quantity = 120,
                    IsAvailable = false,
                    CategoryId = 3,
                    IsSameDayDeliveryAvailable = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 6,
                    Name = "Daisy",
                    Description = "Simple yet charming daisies that radiate innocence and joy. Their classic white petals and sunny centers make them perfect for cheerful      bouquets     and     everyday smiles.",
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1677560614396-416d97638016?             q=80&w=688&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 7.25m,
                    Quantity = 200,
                    IsAvailable = false,
                    CategoryId = 7,
                    IsSameDayDeliveryAvailable = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 7,
                    Name = "Carnation",
                    Description = "Long-lasting carnations with ruffled petals, available in various vibrant colors. A favorite for mixed arrangements and symbolizing          fascination,     distinction, and love.",
                    ImageUrl = "https://images.unsplash.com/photo-1590700198862-f812474f4e0a?               q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 8.50m,
                    Quantity = 180,
                    IsAvailable = false,
                    CategoryId = 6,
                    IsSameDayDeliveryAvailable = false,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 8,
                    Name = "Peony",
                    Description = "Lush, romantic peonies with full, ruffled blooms in soft pastel tones. An elegant choice for weddings, anniversaries, or any     occasion        celebrating    love and beauty.",
                    ImageUrl = "https://images.unsplash.com/photo-1575178114667-c8a832c61f45?               q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 22.00m,
                    Quantity = 90,
                    IsAvailable = false,
                    CategoryId = 8,
                    IsSameDayDeliveryAvailable = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 9,
                    Name = "Chrysanthemum",
                    Description = "Versatile chrysanthemums in rich hues, popular in fall bouquets and traditional arrangements. These long-lasting blooms represent    joy,       longevity,    and fidelity.",
                    ImageUrl = "https://images.unsplash.com/photo-1536126080396-d775c5296e7d?               q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 10.00m,
                    Quantity = 140,
                    IsAvailable = false,
                    CategoryId = 9,
                    IsSameDayDeliveryAvailable = true,
                    IsDeleted = false
                },
                new Product
                {
                    Id = 10,
                    Name = "Gardenia",
                    Description = "Heavenly scented gardenias with glossy green leaves. Their creamy white petals make them a symbol of purity and elegance, perfect for        formal         occasions and gifts.",
                    ImageUrl = "https://images.unsplash.com/photo-1685669957476-12bbdf277a7d?               q=80&w=1074&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    Price = 19.99m,
                    Quantity = 70,
                    IsAvailable = false,
                    CategoryId = 10,
                    IsSameDayDeliveryAvailable = false,
                    IsDeleted = false
                },

            };
        }




    }
}

