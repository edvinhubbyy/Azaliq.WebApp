using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreThingsToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryCode",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Orders",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Orders");
        }
    }
}
