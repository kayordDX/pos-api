using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItem",
                newName: "TableBookingId");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Option",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Extra",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Extra",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Extra",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Option_OrderItemId",
                table: "Option",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Extra_OrderItemId",
                table: "Extra",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extra_OrderItem_OrderItemId",
                table: "Extra",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Option_OrderItem_OrderItemId",
                table: "Option",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extra_OrderItem_OrderItemId",
                table: "Extra");

            migrationBuilder.DropForeignKey(
                name: "FK_Option_OrderItem_OrderItemId",
                table: "Option");

            migrationBuilder.DropIndex(
                name: "IX_Option_OrderItemId",
                table: "Option");

            migrationBuilder.DropIndex(
                name: "IX_Extra_OrderItemId",
                table: "Extra");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Option");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Extra");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Extra");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Extra");

            migrationBuilder.RenameColumn(
                name: "TableBookingId",
                table: "OrderItem",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "OrderItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
