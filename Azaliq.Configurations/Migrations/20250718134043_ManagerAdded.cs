using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azaliq.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManagerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "StoresLocations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Manager identifier"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Manager's user entity")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Manager in the system");

            migrationBuilder.CreateIndex(
                name: "IX_StoresLocations_ManagerId",
                table: "StoresLocations",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoresLocations_Managers_ManagerId",
                table: "StoresLocations",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoresLocations_Managers_ManagerId",
                table: "StoresLocations");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_StoresLocations_ManagerId",
                table: "StoresLocations");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "StoresLocations");
        }
    }
}
