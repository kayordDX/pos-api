using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignedUserOnAllocateItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "assigned_user_id",
                table: "stock_allocate_item",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_stock_allocate_item_assigned_user_id",
                table: "stock_allocate_item",
                column: "assigned_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_item_user_assigned_user_id",
                table: "stock_allocate_item",
                column: "assigned_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_item_user_assigned_user_id",
                table: "stock_allocate_item");

            migrationBuilder.DropIndex(
                name: "ix_stock_allocate_item_assigned_user_id",
                table: "stock_allocate_item");

            migrationBuilder.DropColumn(
                name: "assigned_user_id",
                table: "stock_allocate_item");
        }
    }
}
