using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestingStoreChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoresLocations_Managers_ManagerId",
                table: "StoresLocations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ManagerId",
                table: "StoresLocations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "City of the Store");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryOption",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PickupStoreId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickupStoreId",
                table: "Orders",
                column: "PickupStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StoresLocations_PickupStoreId",
                table: "Orders",
                column: "PickupStoreId",
                principalTable: "StoresLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoresLocations_Managers_ManagerId",
                table: "StoresLocations",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StoresLocations_PickupStoreId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_StoresLocations_Managers_ManagerId",
                table: "StoresLocations");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PickupStoreId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryOption",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickupStoreId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "ManagerId",
                table: "StoresLocations",
                type: "uniqueidentifier",
                nullable: true,
                comment: "City of the Store",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoresLocations_Managers_ManagerId",
                table: "StoresLocations",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
