using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stock_order_item_id",
                table: "stock_item_audit",
                newName: "stock_order_id");

            migrationBuilder.RenameColumn(
                name: "stock_allocate_item_id",
                table: "stock_item_audit",
                newName: "stock_id");

            migrationBuilder.AddColumn<int>(
                name: "stock_allocate_id",
                table: "stock_item_audit",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stock_allocate_id",
                table: "stock_item_audit");

            migrationBuilder.RenameColumn(
                name: "stock_order_id",
                table: "stock_item_audit",
                newName: "stock_order_item_id");

            migrationBuilder.RenameColumn(
                name: "stock_id",
                table: "stock_item_audit",
                newName: "stock_allocate_item_id");
        }
    }
}
