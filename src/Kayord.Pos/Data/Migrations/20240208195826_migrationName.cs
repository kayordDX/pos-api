using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class migrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionGroup_MenuItem_MenuItemId",
                table: "OptionGroup");

            migrationBuilder.DropIndex(
                name: "IX_OptionGroup_MenuItemId",
                table: "OptionGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemOptionGroup",
                table: "MenuItemOptionGroup");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "OptionGroup");

            migrationBuilder.DropColumn(
                name: "MenuItemOptionGroupId",
                table: "MenuItemOptionGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemOptionGroup",
                table: "MenuItemOptionGroup",
                columns: new[] { "OptionGroupId", "MenuItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOptionGroup_MenuItemId",
                table: "MenuItemOptionGroup",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOptionGroup_MenuItem_MenuItemId",
                table: "MenuItemOptionGroup",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOptionGroup_OptionGroup_OptionGroupId",
                table: "MenuItemOptionGroup",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "OptionGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOptionGroup_MenuItem_MenuItemId",
                table: "MenuItemOptionGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOptionGroup_OptionGroup_OptionGroupId",
                table: "MenuItemOptionGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemOptionGroup",
                table: "MenuItemOptionGroup");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemOptionGroup_MenuItemId",
                table: "MenuItemOptionGroup");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "OptionGroup",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuItemOptionGroupId",
                table: "MenuItemOptionGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemOptionGroup",
                table: "MenuItemOptionGroup",
                column: "MenuItemOptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionGroup_MenuItemId",
                table: "OptionGroup",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionGroup_MenuItem_MenuItemId",
                table: "OptionGroup",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId");
        }
    }
}
