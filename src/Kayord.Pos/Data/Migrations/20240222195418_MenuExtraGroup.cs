using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuExtraGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extra_MenuItem_MenuItemId",
                table: "Extra");

            migrationBuilder.DropIndex(
                name: "IX_Extra_MenuItemId",
                table: "Extra");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "Extra");

            migrationBuilder.AddColumn<int>(
                name: "ExtraGroupId",
                table: "Extra",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExtraGroup",
                columns: table => new
                {
                    ExtraGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraGroup", x => x.ExtraGroupId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemExtraGroup",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "integer", nullable: false),
                    ExtraGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemExtraGroup", x => new { x.ExtraGroupId, x.MenuItemId });
                    table.ForeignKey(
                        name: "FK_MenuItemExtraGroup_ExtraGroup_ExtraGroupId",
                        column: x => x.ExtraGroupId,
                        principalTable: "ExtraGroup",
                        principalColumn: "ExtraGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemExtraGroup_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Extra_ExtraGroupId",
                table: "Extra",
                column: "ExtraGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemExtraGroup_MenuItemId",
                table: "MenuItemExtraGroup",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extra_ExtraGroup_ExtraGroupId",
                table: "Extra",
                column: "ExtraGroupId",
                principalTable: "ExtraGroup",
                principalColumn: "ExtraGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extra_ExtraGroup_ExtraGroupId",
                table: "Extra");

            migrationBuilder.DropTable(
                name: "MenuItemExtraGroup");

            migrationBuilder.DropTable(
                name: "ExtraGroup");

            migrationBuilder.DropIndex(
                name: "IX_Extra_ExtraGroupId",
                table: "Extra");

            migrationBuilder.DropColumn(
                name: "ExtraGroupId",
                table: "Extra");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "Extra",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Extra_MenuItemId",
                table: "Extra",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extra_MenuItem_MenuItemId",
                table: "Extra",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId");
        }
    }
}
