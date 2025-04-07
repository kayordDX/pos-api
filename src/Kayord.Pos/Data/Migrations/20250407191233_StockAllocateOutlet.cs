using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockAllocateOutlet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_stock_allocate_outlet_id",
                table: "stock_allocate",
                column: "outlet_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_allocate_to_outlet_id",
                table: "stock_allocate",
                column: "to_outlet_id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_outlet_outlet_id",
                table: "stock_allocate",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_outlet_to_outlet_id",
                table: "stock_allocate",
                column: "to_outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_outlet_outlet_id",
                table: "stock_allocate");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_outlet_to_outlet_id",
                table: "stock_allocate");

            migrationBuilder.DropIndex(
                name: "ix_stock_allocate_outlet_id",
                table: "stock_allocate");

            migrationBuilder.DropIndex(
                name: "ix_stock_allocate_to_outlet_id",
                table: "stock_allocate");
        }
    }
}
