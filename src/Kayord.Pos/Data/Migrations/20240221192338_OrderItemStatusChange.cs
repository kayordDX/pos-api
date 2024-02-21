using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemStatusChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isKitchen",
                table: "OrderItemStatus",
                newName: "isComplete");

            migrationBuilder.AddColumn<bool>(
                name: "isBackOffice",
                table: "OrderItemStatus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isCancelled",
                table: "OrderItemStatus",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBackOffice",
                table: "OrderItemStatus");

            migrationBuilder.DropColumn(
                name: "isCancelled",
                table: "OrderItemStatus");

            migrationBuilder.RenameColumn(
                name: "isComplete",
                table: "OrderItemStatus",
                newName: "isKitchen");
        }
    }
}
