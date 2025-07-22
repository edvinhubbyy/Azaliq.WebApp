using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class Testttting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Added customer/order details:");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Optional delivery address for the ArchivedOrder, if it is a delivery order.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Added customer/order details:",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Optional delivery address for the ArchivedOrder, if it is a delivery order.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
