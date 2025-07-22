using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "ArchivedOrders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ArchivedOrders");

            migrationBuilder.DropColumn(
                name: "IsDelivery",
                table: "ArchivedOrders");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Added customer/order details:",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Added customer/order details:");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Added customer/order details:",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Added customer/order details:");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryCode",
                table: "ArchivedOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ArchivedOrders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether the ArchivedOrder has been deleted or is active.");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivery",
                table: "ArchivedOrders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether the ArchivedOrder is for delivery or pickup.");
        }
    }
}
