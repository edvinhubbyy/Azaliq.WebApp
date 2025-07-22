using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupTime",
                table: "ArchivedOrders");

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
                name: "DeliveryAddress",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Optional delivery address for the ArchivedOrder, if it is a delivery order.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Optional delivery address for the ArchivedOrder, if it is a delivery order.");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "DeliveryAddress",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Optional delivery address for the ArchivedOrder, if it is a delivery order.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Optional delivery address for the ArchivedOrder, if it is a delivery order.");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "ArchivedOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupTime",
                table: "ArchivedOrders",
                type: "datetime2",
                nullable: true,
                comment: "Optional date and time when the ArchivedOrder is scheduled for pickup.");
        }
    }
}
