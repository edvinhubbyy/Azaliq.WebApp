using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartsItems_AspNetUsers_UserId",
                table: "CartsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartsItems_Products_ProductId",
                table: "CartsItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartsItems",
                table: "CartsItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CartsItems");

            migrationBuilder.RenameTable(
                name: "CartsItems",
                newName: "CartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartsItems_UserId_ProductId",
                table: "CartItems",
                newName: "IX_CartItems_UserId_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartsItems_ProductId",
                table: "CartItems",
                newName: "IX_CartItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_UserId",
                table: "CartItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_UserId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartsItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_UserId_ProductId",
                table: "CartsItems",
                newName: "IX_CartsItems_UserId_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ProductId",
                table: "CartsItems",
                newName: "IX_CartsItems_ProductId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CartsItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartsItems",
                table: "CartsItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartsItems_AspNetUsers_UserId",
                table: "CartsItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartsItems_Products_ProductId",
                table: "CartsItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
