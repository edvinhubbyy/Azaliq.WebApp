using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentsAddedToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "StoresLocations",
                comment: "Store entity represents a store in the system.");

            migrationBuilder.AlterTable(
                name: "Reviews",
                comment: "Review entity represents a product review in the system.");

            migrationBuilder.AlterTable(
                name: "ProductsTags",
                comment: "ProductTags");

            migrationBuilder.AlterTable(
                name: "Products",
                comment: "Product entity represents a product in the system.");

            migrationBuilder.AlterTable(
                name: "OrdersProducts",
                comment: "OrderProduct a join entity that represents the many-to-many relationship between Order and Product.");

            migrationBuilder.AlterTable(
                name: "Orders",
                comment: "Order entity represents a customer's order in the system.");

            migrationBuilder.AlterTable(
                name: "Favorites",
                comment: "Favorite entity represents a user's favorite product in the system.");

            migrationBuilder.AlterTable(
                name: "Categories",
                comment: "Category entity represents a product category in the system.");

            migrationBuilder.AlterTable(
                name: "CartItems",
                comment: "CartItem represents an item in a user's shopping cart, linking a product to a user with a specified quantity.");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "ApplicationUser represents a user in the application.");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StoresLocations",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                comment: "Phone number of the Store",
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StoresLocations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Name of the Store",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "ManagerId",
                table: "StoresLocations",
                type: "uniqueidentifier",
                nullable: true,
                comment: "City of the Store",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoresLocations",
                type: "bit",
                nullable: false,
                comment: "Indicates if the Store is deleted",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleMapsUrl",
                table: "StoresLocations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "Google Maps URL of the Store",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "CountryCode",
                table: "StoresLocations",
                type: "int",
                nullable: false,
                comment: "Country code of the Store",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "StoresLocations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                comment: "Address of the Store",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "StoresLocations",
                type: "int",
                nullable: false,
                comment: "ID of the Store",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Id of the User who wrote the review",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                comment: "The rating given in the review, typically from 1 to 5 stars",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Reviews",
                type: "int",
                nullable: false,
                comment: "Id of the Product that is being reviewed",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                comment: "Indicates whether the review has been deleted",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                comment: "The date and time when the review was created",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "The content of the review",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Reviews",
                type: "int",
                nullable: false,
                comment: "Id of the Review",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductsTags",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Name of the ProductTag",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductsTags",
                type: "int",
                nullable: false,
                comment: "ID of the ProductTag",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                comment: "Product quantity in stock",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Product price",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Product name",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSameDayDeliveryAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                comment: "Indicates if the product is available for same-day delivery",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                comment: "Indicates if the product is deleted",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                comment: "Indicates if the product is available for purchase",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                comment: "Product image URL",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "Product description",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                comment: "Category ID to which the product belongs",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                comment: "Product ID",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrdersProducts",
                type: "int",
                nullable: false,
                comment: "The quantity of the product in the order.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrdersProducts",
                type: "int",
                nullable: false,
                comment: "Primary key for the OrderProduct entity.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrdersProducts",
                type: "int",
                nullable: false,
                comment: "Primary key for the OrderProduct entity.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Foreign key to the User who placed the Order.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                comment: "Total amount for the Order, calculated based on the products and their quantities.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Status of the Order, indicating its current state in the order lifecycle.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PickupTime",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                comment: "Optional date and time when the Order is scheduled for pickup.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                comment: "Date and time when the Order was placed.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelivery",
                table: "Orders",
                type: "bit",
                nullable: false,
                comment: "Indicates whether the Order is for delivery or pickup.",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                comment: "Indicates whether the Order has been deleted or is active.",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Added customer/order details:",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "Optional delivery address for the Order, if it is a delivery order.",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Orders",
                type: "int",
                nullable: false,
                comment: "Unique identifier for the Order.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Managers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "IsDeleted filtering entity",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favorites",
                type: "nvarchar(450)",
                nullable: false,
                comment: "UserId is the identifier of the user who favorited the product.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Favorites",
                type: "int",
                nullable: false,
                comment: "ProductId is the identifier of the product that has been favorited.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Favorites",
                type: "int",
                nullable: false,
                comment: "Favorite identifier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Name of the Category.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                comment: "Filtering property",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "Unique identifier for the Category.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "CartItems",
                type: "int",
                nullable: false,
                comment: "Quantity is the number of units of the product in the cart.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "int",
                nullable: false,
                comment: "ProductId is the foreign key linking to the Product associated with this CartItem.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CartItems",
                type: "int",
                nullable: false,
                comment: "Id is the unique identifier for the CartItem.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                comment: "Full name of the user.",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                comment: "Email address of the user.",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "StoresLocations",
                oldComment: "Store entity represents a store in the system.");

            migrationBuilder.AlterTable(
                name: "Reviews",
                oldComment: "Review entity represents a product review in the system.");

            migrationBuilder.AlterTable(
                name: "ProductsTags",
                oldComment: "ProductTags");

            migrationBuilder.AlterTable(
                name: "Products",
                oldComment: "Product entity represents a product in the system.");

            migrationBuilder.AlterTable(
                name: "OrdersProducts",
                oldComment: "OrderProduct a join entity that represents the many-to-many relationship between Order and Product.");

            migrationBuilder.AlterTable(
                name: "Orders",
                oldComment: "Order entity represents a customer's order in the system.");

            migrationBuilder.AlterTable(
                name: "Favorites",
                oldComment: "Favorite entity represents a user's favorite product in the system.");

            migrationBuilder.AlterTable(
                name: "Categories",
                oldComment: "Category entity represents a product category in the system.");

            migrationBuilder.AlterTable(
                name: "CartItems",
                oldComment: "CartItem represents an item in a user's shopping cart, linking a product to a user with a specified quantity.");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "ApplicationUser represents a user in the application.");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StoresLocations",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13,
                oldComment: "Phone number of the Store");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StoresLocations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Name of the Store");

            migrationBuilder.AlterColumn<Guid>(
                name: "ManagerId",
                table: "StoresLocations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "City of the Store");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoresLocations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates if the Store is deleted");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleMapsUrl",
                table: "StoresLocations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "Google Maps URL of the Store");

            migrationBuilder.AlterColumn<int>(
                name: "CountryCode",
                table: "StoresLocations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Country code of the Store");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "StoresLocations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldComment: "Address of the Store");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "StoresLocations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "ID of the Store")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Id of the User who wrote the review");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The rating given in the review, typically from 1 to 5 stars");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id of the Product that is being reviewed");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates whether the review has been deleted");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The date and time when the review was created");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "The content of the review");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id of the Review")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductsTags",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Name of the ProductTag");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductsTags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "ID of the ProductTag")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Product quantity in stock");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Product price");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Product name");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSameDayDeliveryAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates if the product is available for same-day delivery");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates if the product is deleted");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates if the product is available for purchase");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldComment: "Product image URL");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "Product description");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Category ID to which the product belongs");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Product ID")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrdersProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The quantity of the product in the order.");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrdersProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key for the OrderProduct entity.");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrdersProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key for the OrderProduct entity.");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Foreign key to the User who placed the Order.");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "Total amount for the Order, calculated based on the products and their quantities.");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Status of the Order, indicating its current state in the order lifecycle.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PickupTime",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Optional date and time when the Order is scheduled for pickup.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Date and time when the Order was placed.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelivery",
                table: "Orders",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates whether the Order is for delivery or pickup.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates whether the Order has been deleted or is active.");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Added customer/order details:");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Optional delivery address for the Order, if it is a delivery order.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Unique identifier for the Order.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Managers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "IsDeleted filtering entity");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favorites",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "UserId is the identifier of the user who favorited the product.");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Favorites",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "ProductId is the identifier of the product that has been favorited.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Favorites",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Favorite identifier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Name of the Category.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Filtering property");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Unique identifier for the Category.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Quantity is the number of units of the product in the cart.");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "ProductId is the foreign key linking to the Product associated with this CartItem.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Id is the unique identifier for the CartItem.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true,
                oldComment: "Full name of the user.");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true,
                oldComment: "Email address of the user.");
        }
    }
}
