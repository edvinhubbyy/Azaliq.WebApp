using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    public partial class FixedReviewsProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Reviews");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId1",
                table: "Reviews",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId1",
                table: "Reviews",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
