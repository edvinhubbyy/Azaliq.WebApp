using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
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
                    { 1, 1, "A beautiful bouquet of red roses", "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=500&q=60", false, false, false, "Red Rose Bouquet", 49.99m, 0 },
                    { 2, 2, "Bright and cheerful yellow tulips", "https://images.unsplash.com/photo-1499744937866-d9e8f7e0ecf4?auto=format&fit=crop&w=500&q=60", false, false, false, "Yellow Tulips", 39.99m, 0 },
                    { 3, 3, "Elegant white lilies perfect for any occasion", "https://images.unsplash.com/photo-1501004318641-b39e6451bec6?auto=format&fit=crop&w=500&q=60", false, false, false, "White Lilies", 44.99m, 0 },
                    { 4, 4, "Delicate pink orchids", "https://images.unsplash.com/photo-1497551060073-4c5ab6435f5f?auto=format&fit=crop&w=500&q=60", false, false, false, "Pink Orchids", 59.99m, 0 },
                    { 5, 5, "Bright sunflower basket", "https://images.unsplash.com/photo-1500534623283-312aade485b7?auto=format&fit=crop&w=500&q=60", false, false, false, "Sunflower Basket", 35.00m, 0 },
                    { 6, 6, "Colorful mix of carnations", "https://images.unsplash.com/photo-1494976388531-d1058494cdd8?auto=format&fit=crop&w=500&q=60", false, false, false, "Carnation Mix", 29.99m, 0 },
                    { 7, 7, "Fresh daisies", "https://images.unsplash.com/photo-1465188035480-ff3f285f3bce?auto=format&fit=crop&w=500&q=60", false, false, false, "Daisy Delight", 24.99m, 0 },
                    { 8, 8, "Soft pink peonies", "https://images.unsplash.com/photo-1528825871115-3581a5387919?auto=format&fit=crop&w=500&q=60", false, false, false, "Peony Love", 54.99m, 0 },
                    { 9, 9, "Charming chrysanthemums", "https://images.unsplash.com/photo-1504384308090-c894fdcc538d?auto=format&fit=crop&w=500&q=60", false, false, false, "Chrysanthemum Charm", 27.99m, 0 },
                    { 10, 10, "Fragrant gardenias", "https://images.unsplash.com/photo-1472214103451-9374bd1c798e?auto=format&fit=crop&w=500&q=60", false, false, false, "Gardenia Glow", 39.99m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductsTags",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
