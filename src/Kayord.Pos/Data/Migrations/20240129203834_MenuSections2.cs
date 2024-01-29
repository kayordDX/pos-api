using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuSections2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuSection_MenuSectionId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuSection_MenuSectionId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_MenuId",
                table: "MenuItem");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuSectionId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "MenuSectionId",
                table: "Menu");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "MenuSection",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MenuSectionId",
                table: "MenuItem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuSection_MenuId",
                table: "MenuSection",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_MenuSection_MenuSectionId",
                table: "MenuItem",
                column: "MenuSectionId",
                principalTable: "MenuSection",
                principalColumn: "MenuSectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSection_Menu_MenuId",
                table: "MenuSection",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuSection_MenuSectionId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSection_Menu_MenuId",
                table: "MenuSection");

            migrationBuilder.DropIndex(
                name: "IX_MenuSection_MenuId",
                table: "MenuSection");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "MenuSection");

            migrationBuilder.AlterColumn<int>(
                name: "MenuSectionId",
                table: "MenuItem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "MenuItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MenuSectionId",
                table: "Menu",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuId",
                table: "MenuItem",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuSectionId",
                table: "Menu",
                column: "MenuSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_MenuSection_MenuSectionId",
                table: "Menu",
                column: "MenuSectionId",
                principalTable: "MenuSection",
                principalColumn: "MenuSectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_MenuSection_MenuSectionId",
                table: "MenuItem",
                column: "MenuSectionId",
                principalTable: "MenuSection",
                principalColumn: "MenuSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
