using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockVat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "menu_item_stock");

            migrationBuilder.AddColumn<bool>(
                name: "has_vat",
                table: "stock",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "has_vat",
                table: "stock");

            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "menu_item_stock",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
