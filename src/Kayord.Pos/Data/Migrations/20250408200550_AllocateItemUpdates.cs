using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllocateItemUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_item_division_division_id",
                table: "stock_allocate_item");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_item_stock_allocate_stock_allocate_id",
                table: "stock_allocate_item");

            migrationBuilder.DropIndex(
                name: "ix_stock_allocate_item_division_id",
                table: "stock_allocate_item");

            migrationBuilder.DropColumn(
                name: "allocate_amount",
                table: "stock_allocate_item");

            migrationBuilder.DropColumn(
                name: "division_id",
                table: "stock_allocate_item");

            migrationBuilder.AlterColumn<int>(
                name: "stock_allocate_id",
                table: "stock_allocate_item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_item_stock_allocate_stock_allocate_id",
                table: "stock_allocate_item",
                column: "stock_allocate_id",
                principalTable: "stock_allocate",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_item_stock_allocate_stock_allocate_id",
                table: "stock_allocate_item");

            migrationBuilder.AlterColumn<int>(
                name: "stock_allocate_id",
                table: "stock_allocate_item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "allocate_amount",
                table: "stock_allocate_item",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "division_id",
                table: "stock_allocate_item",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_stock_allocate_item_division_id",
                table: "stock_allocate_item",
                column: "division_id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_item_division_division_id",
                table: "stock_allocate_item",
                column: "division_id",
                principalTable: "division",
                principalColumn: "division_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_item_stock_allocate_stock_allocate_id",
                table: "stock_allocate_item",
                column: "stock_allocate_id",
                principalTable: "stock_allocate",
                principalColumn: "id");
        }
    }
}
