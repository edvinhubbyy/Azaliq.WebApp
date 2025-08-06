using System;
using Azaliq.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Azaliq.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250806173923_DataSeeder")]
    partial class DataSeeder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Azaliq.Data.Configurations.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Category.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Filtering property");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Name of the Category.");

                    b.HasKey("Id");

                    b.ToTable("Categories", t =>
                        {
                            t.HasComment("Category entity represents a product category in the system.");
                        });

                    b.HasData(
                        new
                        {
                            Id = -1,
                            IsDeleted = true,
                            Name = "Deleted Category"
                        },
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Roses"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "Tulips"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "Lilies"
                        },
                        new
                        {
                            Id = 4,
                            IsDeleted = false,
                            Name = "Orchids"
                        },
                        new
                        {
                            Id = 5,
                            IsDeleted = false,
                            Name = "Sunflowers"
                        },
                        new
                        {
                            Id = 6,
                            IsDeleted = false,
                            Name = "Carnations"
                        },
                        new
                        {
                            Id = 7,
                            IsDeleted = false,
                            Name = "Daisies"
                        },
                        new
                        {
                            Id = 8,
                            IsDeleted = false,
                            Name = "Peonies"
                        },
                        new
                        {
                            Id = 9,
                            IsDeleted = false,
                            Name = "Chrysanthemums"
                        },
                        new
                        {
                            Id = 10,
                            IsDeleted = false,
                            Name = "Gardenias"
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ArchivedOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Unique identifier for the ArchivedOrder.");

                    b.Property<Guid>("ArchivedUserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Foreign key to the ArchivedUser who placed the ArchivedOrder.");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and time when the ArchivedOrder was placed.");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasComment("Status of the ArchivedOrder, indicating its current state in the order lifecycle.");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total amount for the ArchivedOrder, calculated based on the products and their quantities.");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArchivedUserId");

                    b.ToTable("ArchivedOrders", t =>
                        {
                            t.HasComment("ArchivedOrder entity represents a snapshot of a customer's order when the user is deleted.");
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ArchivedOrderProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArchivedOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArchivedOrderId");

                    b.ToTable("ArchivedOrderProducts");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ArchivedUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ArchivedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("OriginalUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("ArchivedUsers");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Id is the unique identifier for the CartItem.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("ProductId is the foreign key linking to the Product associated with this CartItem.");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasComment("Quantity is the number of units of the product in the cart.");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId", "ProductId")
                        .IsUnique();

                    b.ToTable("CartItems", t =>
                        {
                            t.HasComment("CartItem represents an item in a user's shopping cart, linking a product to a user with a specified quantity.");
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Favorite identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("ProductId is the identifier of the product that has been favorited.");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("UserId is the identifier of the user who favorited the product.");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites", t =>
                        {
                            t.HasComment("Favorite entity represents a user's favorite product in the system.");
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Manager identifier");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("IsDeleted filtering entity");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Manager's user entity");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Managers", t =>
                        {
                            t.HasComment("Manager in the system");
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Unique identifier for the Order.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CountryCode")
                        .HasColumnType("int");

                    b.Property<string>("DeliveryAddress")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("Optional delivery address for the Order, if it is a delivery order.");

                    b.Property<int>("DeliveryOption")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Added customer/order details:");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates whether the Order has been deleted or is active.");

                    b.Property<bool>("IsDelivery")
                        .HasColumnType("bit")
                        .HasComment("Indicates whether the Order is for delivery or pickup.");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and time when the Order was placed.");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<int?>("PickupStoreId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PickupTime")
                        .HasColumnType("datetime2")
                        .HasComment("Optional date and time when the Order is scheduled for pickup.");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Status of the Order, indicating its current state in the order lifecycle.");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total amount for the Order, calculated based on the products and their quantities.");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Foreign key to the User who placed the Order.");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("PickupStoreId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", t =>
                        {
                            t.HasComment("Order entity represents a customer's order in the system.");
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasComment("Primary key for the OrderProduct entity.");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("Primary key for the OrderProduct entity.");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasComment("The quantity of the product in the order.");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrdersProducts", t =>
                        {
                            t.HasComment("OrderProduct a join entity that represents the many-to-many relationship between Order and Product.");
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Product ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasComment("Category ID to which the product belongs");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("Product description");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("Product image URL");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the product is available for purchase");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the product is deleted");

                    b.Property<bool>("IsSameDayDeliveryAvailable")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the product is available for same-day delivery");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Product name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Product price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasComment("Product quantity in stock");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products", t =>
                        {
                            t.HasComment("Product entity represents a product in the system.");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Classic long-stemmed red roses symbolizing love, romance, and admiration. A timeless floral gift perfect for Valentine's Day,    anniversaries,     or  just     to say 'I love you.'",
                            ImageUrl = "https://images.unsplash.com/photo-1496062031456-07b8f162a322?               q=80&w=765&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = true,
                            Name = "Rose",
                            Price = 10.00m,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Bright and cheerful tulips in a rainbow of colors. These springtime favorites are known for their simple beauty and are ideal for        celebrating    fresh  beginnings and happiness.",
                            ImageUrl = "https://plus.unsplash.com/premium_photo-1677620614560-5f1b32416563?             q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = true,
                            Name = "Tulip",
                            Price = 9.99m,
                            Quantity = 150
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 5,
                            Description = "Large, golden sunflowers that exude warmth, happiness, and positivity. A joyful bloom that brightens up any space and symbolizes     adoration   and       loyalty.",
                            ImageUrl = "https://images.unsplash.com/photo-1551945326-df678a97c3af?              q=80&w=735&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = false,
                            Name = "Sunflower",
                            Price = 14.00m,
                            Quantity = 80
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 4,
                            Description = "Exotic and elegant, orchids are known for their long-lasting beauty and sophisticated look. Perfect for home décor, special gifts, or    as     a       centerpiece for luxury events.",
                            ImageUrl = "https://images.unsplash.com/photo-1610397648930-477b8c7f0943?               q=80&w=730&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = true,
                            Name = "Orchid",
                            Price = 25.00m,
                            Quantity = 60
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 3,
                            Description = "Fragrant and graceful lilies available in white and pink shades. Popular for weddings, religious events, and sympathy arrangements due   to    their      serene and elegant charm.",
                            ImageUrl = "https://plus.unsplash.com/premium_photo-1676068243733-df1880c2aef8?             q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = true,
                            Name = "Lily",
                            Price = 18.50m,
                            Quantity = 120
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 7,
                            Description = "Simple yet charming daisies that radiate innocence and joy. Their classic white petals and sunny centers make them perfect for cheerful      bouquets     and     everyday smiles.",
                            ImageUrl = "https://plus.unsplash.com/premium_photo-1677560614396-416d97638016?             q=80&w=688&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = true,
                            Name = "Daisy",
                            Price = 7.25m,
                            Quantity = 200
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 6,
                            Description = "Long-lasting carnations with ruffled petals, available in various vibrant colors. A favorite for mixed arrangements and symbolizing          fascination,     distinction, and love.",
                            ImageUrl = "https://images.unsplash.com/photo-1590700198862-f812474f4e0a?               q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = false,
                            Name = "Carnation",
                            Price = 8.50m,
                            Quantity = 180
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 8,
                            Description = "Lush, romantic peonies with full, ruffled blooms in soft pastel tones. An elegant choice for weddings, anniversaries, or any     occasion        celebrating    love and beauty.",
                            ImageUrl = "https://images.unsplash.com/photo-1575178114667-c8a832c61f45?               q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = true,
                            Name = "Peony",
                            Price = 22.00m,
                            Quantity = 90
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 9,
                            Description = "Versatile chrysanthemums in rich hues, popular in fall bouquets and traditional arrangements. These long-lasting blooms represent    joy,       longevity,    and fidelity.",
                            ImageUrl = "https://images.unsplash.com/photo-1536126080396-d775c5296e7d?               q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = true,
                            Name = "Chrysanthemum",
                            Price = 10.00m,
                            Quantity = 140
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 10,
                            Description = "Heavenly scented gardenias with glossy green leaves. Their creamy white petals make them a symbol of purity and elegance, perfect for        formal         occasions and gifts.",
                            ImageUrl = "https://images.unsplash.com/photo-1685669957476-12bbdf277a7d?               q=80&w=1074&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            IsAvailable = false,
                            IsDeleted = false,
                            IsSameDayDeliveryAvailable = false,
                            Name = "Gardenia",
                            Price = 19.99m,
                            Quantity = 70
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ProductTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("ID of the ProductTag");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Name of the ProductTag");

                    b.HasKey("Id");

                    b.ToTable("ProductsTags", t =>
                        {
                            t.HasComment("ProductTags");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fresh"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Popular"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Seasonal"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Gift"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Fragrant"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Wedding"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Decor"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Romantic"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Exotic"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Cheap"
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Id of the Review");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("The content of the review");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2")
                        .HasComment("The date and time when the review was created");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates whether the review has been deleted");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasComment("Id of the Product that is being reviewed");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasComment("The rating given in the review, typically from 1 to 5 stars");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Id of the User who wrote the review");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews", t =>
                        {
                            t.HasComment("Review entity represents a product review in the system.");
                        });
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("ID of the Store");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasComment("Address of the Store");

                    b.Property<int>("CountryCode")
                        .HasColumnType("int")
                        .HasComment("Country code of the Store");

                    b.Property<string>("GoogleMapsUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Google Maps URL of the Store");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the Store is deleted");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Name of the Store");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)")
                        .HasComment("Phone number of the Store");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("StoresLocations", t =>
                        {
                            t.HasComment("Store entity represents a store in the system.");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProductProductTag", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("ProductsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ProductProductTag");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Address")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasComment("Address of the user.");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Full name of the user.");

                    b.Property<bool>("IsBanned")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.ToTable(t =>
                        {
                            t.HasComment("ApplicationUser represents a user in the application.");
                        });

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ArchivedOrder", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.ArchivedUser", "ArchivedUser")
                        .WithMany("Orders")
                        .HasForeignKey("ArchivedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArchivedUser");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ArchivedOrderProduct", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.ArchivedOrder", "ArchivedOrder")
                        .WithMany("Products")
                        .HasForeignKey("ArchivedOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArchivedOrder");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.CartItem", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Azaliq.Data.Models.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Favorite", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Azaliq.Data.Models.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Manager", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Order", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.Store", "PickupStore")
                        .WithMany("PickupOrders")
                        .HasForeignKey("PickupStoreId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Azaliq.Data.Models.Models.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PickupStore");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.OrderProduct", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Azaliq.Data.Models.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Product", b =>
                {
                    b.HasOne("Azaliq.Data.Configurations.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Review", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Azaliq.Data.Models.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Store", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.Manager", null)
                        .WithMany("ManagedStores")
                        .HasForeignKey("ManagerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductProductTag", b =>
                {
                    b.HasOne("Azaliq.Data.Models.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Azaliq.Data.Models.Models.ProductTag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Azaliq.Data.Configurations.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ArchivedOrder", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ArchivedUser", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Manager", b =>
                {
                    b.Navigation("ManagedStores");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Product", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.Store", b =>
                {
                    b.Navigation("PickupOrders");
                });

            modelBuilder.Entity("Azaliq.Data.Models.Models.ApplicationUser", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
