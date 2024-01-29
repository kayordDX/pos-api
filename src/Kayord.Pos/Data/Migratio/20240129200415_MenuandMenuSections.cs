using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migratio
{
    /// <inheritdoc />
    public partial class MenuandMenuSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MenuItem",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Division",
                table: "MenuItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MenuSectionId",
                table: "MenuItem",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuSectionId",
                table: "Menu",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Extra",
                columns: table => new
                {
                    ExtraId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MenuItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extra", x => x.ExtraId);
                    table.ForeignKey(
                        name: "FK_Extra_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "MenuItemId");
                });

            migrationBuilder.CreateTable(
                name: "MenuSection",
                columns: table => new
                {
                    MenuSectionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuSection", x => x.MenuSectionId);
                    table.ForeignKey(
                        name: "FK_MenuSection_MenuSection_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MenuSection",
                        principalColumn: "MenuSectionId");
                });

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MenuItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_Option_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "MenuItemId");
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MenuItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tag_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "MenuItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuSectionId",
                table: "MenuItem",
                column: "MenuSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuSectionId",
                table: "Menu",
                column: "MenuSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Extra_MenuItemId",
                table: "Extra",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuSection_ParentId",
                table: "MenuSection",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_MenuItemId",
                table: "Option",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MenuItemId",
                table: "Tag",
                column: "MenuItemId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_MenuSection_MenuSectionId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuSection_MenuSectionId",
                table: "MenuItem");

            migrationBuilder.DropTable(
                name: "Extra");

            migrationBuilder.DropTable(
                name: "MenuSection");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_MenuSectionId",
                table: "MenuItem");

            migrationBuilder.DropIndex(
                name: "IX_Menu_MenuSectionId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Division",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "MenuSectionId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "MenuSectionId",
                table: "Menu");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MenuItem",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
