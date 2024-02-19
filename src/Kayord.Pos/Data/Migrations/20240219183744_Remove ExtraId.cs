using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtraId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
