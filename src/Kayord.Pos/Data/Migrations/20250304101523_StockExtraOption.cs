using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockExtraOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_extra_stock_unit_unit_id",
                table: "extra_stock");

            migrationBuilder.DropForeignKey(
                name: "fk_option_stock_unit_unit_id",
                table: "option_stock");

            migrationBuilder.DropIndex(
                name: "ix_option_stock_unit_id",
                table: "option_stock");

            migrationBuilder.DropIndex(
                name: "ix_extra_stock_unit_id",
                table: "extra_stock");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "option_stock");

            migrationBuilder.DropColumn(
                name: "unit_id",
                table: "option_stock");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "extra_stock");

            migrationBuilder.DropColumn(
                name: "unit_id",
                table: "extra_stock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "quantity",
                table: "option_stock",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "unit_id",
                table: "option_stock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "quantity",
                table: "extra_stock",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "unit_id",
                table: "extra_stock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_option_stock_unit_id",
                table: "option_stock",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "ix_extra_stock_unit_id",
                table: "extra_stock",
                column: "unit_id");

            migrationBuilder.AddForeignKey(
                name: "fk_extra_stock_unit_unit_id",
                table: "extra_stock",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_option_stock_unit_unit_id",
                table: "option_stock",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
