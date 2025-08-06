using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Azaliq.Data.Migrations
{
    public partial class DataSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { "Classic long-stemmed red roses symbolizing love, romance, and admiration. A timeless floral gift perfect for Valentine's Day,    anniversaries,     or  just     to say 'I love you.'", "https://images.unsplash.com/photo-1496062031456-07b8f162a322?               q=80&w=765&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Rose", 10.00m, 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 2, "Bright and cheerful tulips in a rainbow of colors. These springtime favorites are known for their simple beauty and are ideal for        celebrating    fresh  beginnings and happiness.", "https://plus.unsplash.com/premium_photo-1677620614560-5f1b32416563?             q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Tulip", 9.99m, 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[] { 5, "Large, golden sunflowers that exude warmth, happiness, and positivity. A joyful bloom that brightens up any space and symbolizes     adoration   and       loyalty.", "https://images.unsplash.com/photo-1551945326-df678a97c3af?              q=80&w=735&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Sunflower", 14.00m, 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 4, "Exotic and elegant, orchids are known for their long-lasting beauty and sophisticated look. Perfect for home décor, special gifts, or    as     a       centerpiece for luxury events.", "https://images.unsplash.com/photo-1610397648930-477b8c7f0943?               q=80&w=730&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Orchid", 25.00m, 60 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 3, "Fragrant and graceful lilies available in white and pink shades. Popular for weddings, religious events, and sympathy arrangements due   to    their      serene and elegant charm.", "https://plus.unsplash.com/premium_photo-1676068243733-df1880c2aef8?             q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Lily", 18.50m, 120 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 7, "Simple yet charming daisies that radiate innocence and joy. Their classic white petals and sunny centers make them perfect for cheerful      bouquets     and     everyday smiles.", "https://plus.unsplash.com/premium_photo-1677560614396-416d97638016?             q=80&w=688&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Daisy", 7.25m, 200 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[] { 6, "Long-lasting carnations with ruffled petals, available in various vibrant colors. A favorite for mixed arrangements and symbolizing          fascination,     distinction, and love.", "https://images.unsplash.com/photo-1590700198862-f812474f4e0a?               q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Carnation", 8.50m, 180 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 8, "Lush, romantic peonies with full, ruffled blooms in soft pastel tones. An elegant choice for weddings, anniversaries, or any     occasion        celebrating    love and beauty.", "https://images.unsplash.com/photo-1575178114667-c8a832c61f45?               q=80&w=687&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Peony", 22.00m, 90 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 9, "Versatile chrysanthemums in rich hues, popular in fall bouquets and traditional arrangements. These long-lasting blooms represent    joy,       longevity,    and fidelity.", "https://images.unsplash.com/photo-1536126080396-d775c5296e7d?               q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Chrysanthemum", 10.00m, 140 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[] { 10, "Heavenly scented gardenias with glossy green leaves. Their creamy white petals make them a symbol of purity and elegance, perfect for        formal         occasions and gifts.", "https://images.unsplash.com/photo-1685669957476-12bbdf277a7d?               q=80&w=1074&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Gardenia", 19.99m, 70 });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { "A bouquet of long-stemmed red roses", "https://images.pexels.com/photos/931162/pexels-photo-931162.jpeg?auto=compress&cs=tinysrgb&h=800", false, "Classic Red Roses", 49.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 1, "Soft pink garden roses", "https://images.pexels.com/photos/1073048/pexels-photo-1073048.jpeg?auto=compress&cs=tinysrgb&h=800", false, "Pink Garden Roses", 54.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[] { 1, "Crisp white avalanche roses", "https://images.pexels.com/photos/213222/pexels-photo-213222.jpeg?auto=compress&cs=tinysrgb&h=800", "White Avalanche Roses", 59.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 2, "Bright yellow tulips in a bundle", "https://images.pexels.com/photos/934070/pexels-photo-934070.jpeg?auto=compress&cs=tinysrgb&h=800", false, "Yellow Tulip Bundle", 39.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 2, "Mixed red and white tulips", "https://images.pexels.com/photos/5857509/pexels-photo-5857509.jpeg?auto=compress&cs=tinysrgb&h=800", false, "Red & White Tulips", 42.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 2, "Soft pink tulips standing tall", "https://images.pexels.com/photos/315638/pexels-photo-315638.jpeg?auto=compress&cs=tinysrgb&h=800", false, "Pink Tulips", 37.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[] { 3, "Fragrant white stargazer lilies", "https://images.pexels.com/photos/1460886/pexels-photo-1460886.jpeg?auto=compress&cs=tinysrgb&h=800", "White Stargazer Lilies", 44.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 3, "Vibrant orange asiatic lilies", "https://images.pexels.com/photos/248526/pexels-photo-248526.jpeg?auto=compress&cs=tinysrgb&h=800", false, "Orange Asiatic Lilies", 46.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[] { 3, "Delicate pink oriental lilies", "https://images.pexels.com/photos/1544336/pexels-photo-1544336.jpeg?auto=compress&cs=tinysrgb&h=800", false, "Pink Oriental Lilies", 48.99m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[] { 4, "Elegant white Phalaenopsis orchids", "https://images.pexels.com/photos/931180/pexels-photo-931180.jpeg?auto=compress&cs=tinysrgb&h=800", "Phalaenopsis Orchids", 59.99m, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsAvailable", "IsDeleted", "IsSameDayDeliveryAvailable", "Name", "Price", "Quantity" },
                values: new object[,]
                {
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
        }
    }
}
