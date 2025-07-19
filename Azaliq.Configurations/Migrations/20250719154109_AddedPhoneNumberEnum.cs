using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhoneNumberEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "StoresLocations");

            migrationBuilder.AddColumn<int>(
                name: "CountryCode",
                table: "StoresLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "StoresLocations",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "StoresLocations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "StoresLocations");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "StoresLocations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
