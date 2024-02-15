using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class ItemListRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemExtra_Extra_ExtraId",
                table: "OrderItemExtra");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemExtra_ExtraId",
                table: "OrderItemExtra");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderItemExtra_ExtraId",
                table: "OrderItemExtra",
                column: "ExtraId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemExtra_Extra_ExtraId",
                table: "OrderItemExtra",
                column: "ExtraId",
                principalTable: "Extra",
                principalColumn: "ExtraId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
