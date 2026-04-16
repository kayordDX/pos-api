using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemDivision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_division_division_id",
                table: "menu_item");

            migrationBuilder.AlterColumn<int>(
                name: "division_id",
                table: "menu_item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_division_division_id",
                table: "menu_item",
                column: "division_id",
                principalTable: "division",
                principalColumn: "division_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_division_division_id",
                table: "menu_item");

            migrationBuilder.AlterColumn<int>(
                name: "division_id",
                table: "menu_item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_division_division_id",
                table: "menu_item",
                column: "division_id",
                principalTable: "division",
                principalColumn: "division_id");
        }
    }
}
