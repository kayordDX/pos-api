using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemOptionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_MenuItem_MenuItemId",
                table: "Option");

            migrationBuilder.DropIndex(
                name: "IX_Option_MenuItemId",
                table: "Option");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "Option");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Option",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OptionGroupId",
                table: "Option",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Option",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "MenuItemOptionGroup",
                columns: table => new
                {
                    MenuItemOptionGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuItemId = table.Column<int>(type: "integer", nullable: false),
                    OptionGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemOptionGroup", x => x.MenuItemOptionGroupId);
                });

            migrationBuilder.CreateTable(
                name: "OptionGroup",
                columns: table => new
                {
                    OptionGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MinSelections = table.Column<int>(type: "integer", nullable: false),
                    MaxSelections = table.Column<int>(type: "integer", nullable: false),
                    MenuItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionGroup", x => x.OptionGroupId);
                    table.ForeignKey(
                        name: "FK_OptionGroup_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "MenuItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Option_OptionGroupId",
                table: "Option",
                column: "OptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionGroup_MenuItemId",
                table: "OptionGroup",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Option_OptionGroup_OptionGroupId",
                table: "Option",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "OptionGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_OptionGroup_OptionGroupId",
                table: "Option");

            migrationBuilder.DropTable(
                name: "MenuItemOptionGroup");

            migrationBuilder.DropTable(
                name: "OptionGroup");

            migrationBuilder.DropIndex(
                name: "IX_Option_OptionGroupId",
                table: "Option");

            migrationBuilder.DropColumn(
                name: "OptionGroupId",
                table: "Option");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Option");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Option",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "Option",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Option_MenuItemId",
                table: "Option",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Option_MenuItem_MenuItemId",
                table: "Option",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId");
        }
    }
}
