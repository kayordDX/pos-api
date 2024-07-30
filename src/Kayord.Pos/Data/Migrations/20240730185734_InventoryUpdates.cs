using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class InventoryUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryStock_Unit_UnitId",
                table: "InventoryStock");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemStock_Unit_UnitId",
                table: "MenuItemStock");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemStock_UnitId",
                table: "MenuItemStock");

            migrationBuilder.DropIndex(
                name: "IX_InventoryStock_UnitId",
                table: "InventoryStock");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "MenuItemStock");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "InventoryStock");

            migrationBuilder.AddColumn<decimal>(
                name: "Actual",
                table: "Stock",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Actual",
                table: "Inventory",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actual",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Actual",
                table: "Inventory");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "MenuItemStock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "InventoryStock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemStock_UnitId",
                table: "MenuItemStock",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStock_UnitId",
                table: "InventoryStock",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryStock_Unit_UnitId",
                table: "InventoryStock",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemStock_Unit_UnitId",
                table: "MenuItemStock",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
