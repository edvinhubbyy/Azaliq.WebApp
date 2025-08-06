using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    public partial class StoreLocation : Migration
    {
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
