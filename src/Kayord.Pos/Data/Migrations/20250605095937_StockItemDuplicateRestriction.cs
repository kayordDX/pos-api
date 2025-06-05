using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockItemDuplicateRestriction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_stock_item_stock_id",
                table: "stock_item");

            migrationBuilder.CreateIndex(
                name: "ix_stock_item_stock_id_division_id",
                table: "stock_item",
                columns: new[] { "stock_id", "division_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_stock_item_stock_id_division_id",
                table: "stock_item");

            migrationBuilder.CreateIndex(
                name: "ix_stock_item_stock_id",
                table: "stock_item",
                column: "stock_id");
        }
    }
}
