using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableOrderItem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "TableOrder",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableOrder_OrderItemId",
                table: "TableOrder",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableOrder_OrderItem_OrderItemId",
                table: "TableOrder",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableOrder_OrderItem_OrderItemId",
                table: "TableOrder");

            migrationBuilder.DropIndex(
                name: "IX_TableOrder_OrderItemId",
                table: "TableOrder");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "TableOrder");
        }
    }
}
