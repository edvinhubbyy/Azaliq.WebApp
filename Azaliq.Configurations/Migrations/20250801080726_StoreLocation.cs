using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class StoreLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StoresLocations_PickupStoreId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StoresLocations_PickupStoreId",
                table: "Orders",
                column: "PickupStoreId",
                principalTable: "StoresLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StoresLocations_PickupStoreId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StoresLocations_PickupStoreId",
                table: "Orders",
                column: "PickupStoreId",
                principalTable: "StoresLocations",
                principalColumn: "Id");
        }
    }
}
