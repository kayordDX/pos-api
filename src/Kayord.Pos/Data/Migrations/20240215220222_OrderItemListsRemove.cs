using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemListsRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemExtra_OrderItem_OrderItemId",
                table: "OrderItemExtra");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemOption_OrderItem_OrderItemId",
                table: "OrderItemOption");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemOption_OrderItemId",
                table: "OrderItemOption");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemExtra_OrderItemId",
                table: "OrderItemExtra");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderItemOption_OrderItemId",
                table: "OrderItemOption",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemExtra_OrderItemId",
                table: "OrderItemExtra",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemExtra_OrderItem_OrderItemId",
                table: "OrderItemExtra",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemOption_OrderItem_OrderItemId",
                table: "OrderItemOption",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
