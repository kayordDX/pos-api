using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemStockNotItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_stock_stock_item_stock_item_id",
                table: "menu_item_stock");

            migrationBuilder.RenameColumn(
                name: "stock_item_id",
                table: "menu_item_stock",
                newName: "stock_id");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_stock_stock_item_id",
                table: "menu_item_stock",
                newName: "ix_menu_item_stock_stock_id");

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_stock_stock_stock_id",
                table: "menu_item_stock",
                column: "stock_id",
                principalTable: "stock",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_stock_stock_stock_id",
                table: "menu_item_stock");

            migrationBuilder.RenameColumn(
                name: "stock_id",
                table: "menu_item_stock",
                newName: "stock_item_id");

            migrationBuilder.RenameIndex(
                name: "ix_menu_item_stock_stock_id",
                table: "menu_item_stock",
                newName: "ix_menu_item_stock_stock_item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_stock_stock_item_stock_item_id",
                table: "menu_item_stock",
                column: "stock_item_id",
                principalTable: "stock_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
