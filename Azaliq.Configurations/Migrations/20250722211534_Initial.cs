using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchivedUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ArchivedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "Full name of the user."),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "Address of the user."),
                    IsBanned = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                },
                comment: "ApplicationUser represents a user in the application.");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the Category.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the Category."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Filtering property")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                },
                comment: "Category entity represents a product category in the system.");

            migrationBuilder.CreateTable(
                name: "ProductsTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID of the ProductTag")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the ProductTag")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsTags", x => x.Id);
                },
                comment: "ProductTags");

            migrationBuilder.CreateTable(
                name: "ArchivedOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the ArchivedOrder."),
                    ArchivedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Foreign key to the ArchivedUser who placed the ArchivedOrder."),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the ArchivedOrder was placed."),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Status of the ArchivedOrder, indicating its current state in the order lifecycle."),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Total amount for the ArchivedOrder, calculated based on the products and their quantities."),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivedOrders_ArchivedUsers_ArchivedUserId",
                        column: x => x.ArchivedUserId,
                        principalTable: "ArchivedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ArchivedOrder entity represents a snapshot of a customer's order when the user is deleted.");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Manager identifier"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Manager's user entity"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "IsDeleted filtering entity")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Manager in the system");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the Order.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key to the User who placed the Order."),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the Order was placed."),
                    PickupTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Optional date and time when the Order is scheduled for pickup."),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Status of the Order, indicating its current state in the order lifecycle."),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Total amount for the Order, calculated based on the products and their quantities."),
                    IsDelivery = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates whether the Order is for delivery or pickup."),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Optional delivery address for the Order, if it is a delivery order."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates whether the Order has been deleted or is active."),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Added customer/order details:"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    CountryCode = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Order entity represents a customer's order in the system.");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Product ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Product name"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "Product description"),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Product image URL"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Product price"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Product quantity in stock"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the product is available for purchase"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Category ID to which the product belongs"),
                    IsSameDayDeliveryAvailable = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the product is available for same-day delivery"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the product is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Product entity represents a product in the system.");

            migrationBuilder.CreateTable(
                name: "ArchivedOrderProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArchivedOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedOrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivedOrderProducts_ArchivedOrders_ArchivedOrderId",
                        column: x => x.ArchivedOrderId,
                        principalTable: "ArchivedOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoresLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "ID of the Store")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Name of the Store"),
                    GoogleMapsUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Google Maps URL of the Store"),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "Address of the Store"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false, comment: "Phone number of the Store"),
                    CountryCode = table.Column<int>(type: "int", nullable: false, comment: "Country code of the Store"),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "City of the Store"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the Store is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoresLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoresLocations_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Store entity represents a store in the system.");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id is the unique identifier for the CartItem.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "ProductId is the foreign key linking to the Product associated with this CartItem."),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity is the number of units of the product in the cart.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "CartItem represents an item in a user's shopping cart, linking a product to a user with a specified quantity.");

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Favorite identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "UserId is the identifier of the user who favorited the product."),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "ProductId is the identifier of the product that has been favorited.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Favorite entity represents a user's favorite product in the system.");

            migrationBuilder.CreateTable(
                name: "OrdersProducts",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "Primary key for the OrderProduct entity."),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "Primary key for the OrderProduct entity."),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "The quantity of the product in the order.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                },
                comment: "OrderProduct a join entity that represents the many-to-many relationship between Order and Product.");

            migrationBuilder.CreateTable(
                name: "ProductProductTag",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductTag", x => new { x.ProductsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProductProductTag_ProductsTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "ProductsTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductTag_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id of the Review")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "Id of the Product that is being reviewed"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of the User who wrote the review"),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "The content of the review"),
                    Rating = table.Column<int>(type: "int", nullable: false, comment: "The rating given in the review, typically from 1 to 5 stars"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the review was created"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates whether the review has been deleted"),
                    ProductId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id");
                },
                comment: "Review entity represents a product review in the system.");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { -1, true, "Deleted Category" },
                    { 1, false, "Roses" },
                    { 2, false, "Tulips" },
                    { 3, false, "Lilies" },
                    { 4, false, "Orchids" },
                    { 5, false, "Sunflowers" },
                    { 6, false, "Carnations" },
                    { 7, false, "Daisies" },
                    { 8, false, "Peonies" },
                    { 9, false, "Chrysanthemums" },
                    { 10, false, "Gardenias" }
                });

            migrationBuilder.InsertData(
                table: "ProductsTags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fresh" },
                    { 2, "Popular" },
                    { 3, "Seasonal" },
                    { 4, "Gift" },
                    { 5, "Fragrant" },
                    { 6, "Wedding" },
                    { 7, "Decor" },
                    { 8, "Romantic" },
                    { 9, "Exotic" },
                    { 10, "Cheap" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsAvailable", "IsDeleted", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "A bouquet of long-stemmed red roses", "https://images.pexels.com/photos/931162/pexels-photo-931162.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Classic Red Roses", 49.99m, 0 },
                    { 2, 1, "Soft pink garden roses", "https://images.pexels.com/photos/1073048/pexels-photo-1073048.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Pink Garden Roses", 54.99m, 0 },
                    { 3, 1, "Crisp white avalanche roses", "https://images.pexels.com/photos/213222/pexels-photo-213222.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "White Avalanche Roses", 59.99m, 0 },
                    { 4, 2, "Bright yellow tulips in a bundle", "https://images.pexels.com/photos/934070/pexels-photo-934070.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Yellow Tulip Bundle", 39.99m, 0 },
                    { 5, 2, "Mixed red and white tulips", "https://images.pexels.com/photos/5857509/pexels-photo-5857509.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Red & White Tulips", 42.99m, 0 },
                    { 6, 2, "Soft pink tulips standing tall", "https://images.pexels.com/photos/315638/pexels-photo-315638.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Pink Tulips", 37.99m, 0 },
                    { 7, 3, "Fragrant white stargazer lilies", "https://images.pexels.com/photos/1460886/pexels-photo-1460886.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "White Stargazer Lilies", 44.99m, 0 },
                    { 8, 3, "Vibrant orange asiatic lilies", "https://images.pexels.com/photos/248526/pexels-photo-248526.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Orange Asiatic Lilies", 46.99m, 0 },
                    { 9, 3, "Delicate pink oriental lilies", "https://images.pexels.com/photos/1544336/pexels-photo-1544336.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Pink Oriental Lilies", 48.99m, 0 },
                    { 10, 4, "Elegant white Phalaenopsis orchids", "https://images.pexels.com/photos/931180/pexels-photo-931180.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Phalaenopsis Orchids", 59.99m, 0 },
                    { 11, 4, "Soft pink moth orchids", "https://images.pexels.com/photos/1637359/pexels-photo-1637359.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Pink Moth Orchids", 62.99m, 0 },
                    { 12, 4, "Rich purple cymbidium orchids", "https://images.pexels.com/photos/257280/pexels-photo-257280.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Purple Cymbidium Orchids", 64.99m, 0 },
                    { 13, 5, "Bright single sunflower stem", "https://images.pexels.com/photos/414274/pexels-photo-414274.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Single Sunflower Stem", 25.00m, 0 },
                    { 14, 5, "Cheerful bouquet of sunflowers", "https://images.pexels.com/photos/1030936/pexels-photo-1030936.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Sunflower Bouquet", 35.00m, 0 },
                    { 15, 5, "Compact sunflowers in a glass vase", "https://images.pexels.com/photos/349758/pexels-photo-349758.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Mini Sunflower Vase", 29.99m, 0 },
                    { 16, 6, "Bright red carnations", "https://images.pexels.com/photos/2898825/pexels-photo-2898825.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Red Carnations", 29.99m, 0 },
                    { 17, 6, "Pure white carnations", "https://images.pexels.com/photos/1299898/pexels-photo-1299898.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "White Carnations", 31.99m, 0 },
                    { 18, 6, "Soft pink carnations", "https://images.pexels.com/photos/4147446/pexels-photo-4147446.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Pink Carnations", 27.99m, 0 },
                    { 19, 7, "Fresh classic daisies", "https://images.pexels.com/photos/414171/pexels-photo-414171.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Classic Daisies", 24.99m, 0 },
                    { 20, 7, "Vibrant gerbera daisies", "https://images.pexels.com/photos/462117/pexels-photo-462117.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Gerbera Daisies", 22.99m, 0 },
                    { 21, 7, "Sunny yellow daisies", "https://images.pexels.com/photos/413195/pexels-photo-413195.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Yellow Daisies", 20.99m, 0 },
                    { 22, 8, "Full cluster of pink peonies", "https://images.pexels.com/photos/931177/pexels-photo-931177.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Pink Peony Cluster", 54.99m, 0 },
                    { 23, 8, "Elegant white peonies", "https://images.pexels.com/photos/1231265/pexels-photo-1231265.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "White Peonies", 56.99m, 0 },
                    { 24, 8, "Soft coral-colored peonies", "https://images.pexels.com/photos/991447/pexels-photo-991447.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Coral Peonies", 58.99m, 0 },
                    { 25, 9, "Sunny yellow chrysanthemums", "https://images.pexels.com/photos/675951/pexels-photo-675951.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Yellow Chrysanthemums", 27.99m, 0 },
                    { 26, 9, "Rich purple chrysanthemums", "https://images.pexels.com/photos/116393/pexels-photo-116393.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Purple Chrysanthemums", 29.99m, 0 },
                    { 27, 9, "Crisp white chrysanthemums", "https://images.pexels.com/photos/939222/pexels-photo-939222.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "White Chrysanthemums", 25.99m, 0 },
                    { 28, 10, "Fragrant classic gardenias", "https://images.pexels.com/photos/937400/pexels-photo-937400.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Classic Gardenias", 39.99m, 0 },
                    { 29, 10, "Single white gardenia bloom", "https://images.pexels.com/photos/264727/pexels-photo-264727.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "White Gardenia Bloom", 41.99m, 0 },
                    { 30, 10, "Gardenia flowers with green leaves", "https://images.pexels.com/photos/206420/pexels-photo-206420.jpeg?auto=compress&cs=tinysrgb&h=800", false, false, false, "Gardenia Leaves & Flower", 43.99m, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedOrderProducts_ArchivedOrderId",
                table: "ArchivedOrderProducts",
                column: "ArchivedOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedOrders_ArchivedUserId",
                table: "ArchivedOrders",
                column: "ArchivedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId_ProductId",
                table: "CartItems",
                columns: new[] { "UserId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_ProductId",
                table: "OrdersProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductTag_TagsId",
                table: "ProductProductTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId1",
                table: "Reviews",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresLocations_ManagerId",
                table: "StoresLocations",
                column: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedOrderProducts");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "OrdersProducts");

            migrationBuilder.DropTable(
                name: "ProductProductTag");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "StoresLocations");

            migrationBuilder.DropTable(
                name: "ArchivedOrders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductsTags");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "ArchivedUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
