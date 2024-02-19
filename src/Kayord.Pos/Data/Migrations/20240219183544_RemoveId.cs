using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OrderItem",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemOption_OrderItemId",
                table: "OrderItemOption",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemExtra_ExtraId",
                table: "OrderItemExtra",
                column: "ExtraId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemExtra_OrderItemId",
                table: "OrderItemExtra",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemExtra_Extra_ExtraId",
                table: "OrderItemExtra",
                column: "ExtraId",
                principalTable: "Extra",
                principalColumn: "ExtraId",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemExtra_Extra_ExtraId",
                table: "OrderItemExtra");

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
                name: "IX_OrderItemExtra_ExtraId",
                table: "OrderItemExtra");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemExtra_OrderItemId",
                table: "OrderItemExtra");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OrderItem");
        }
    }
}
