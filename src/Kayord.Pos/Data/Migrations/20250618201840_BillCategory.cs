using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class BillCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bill_category_id",
                table: "menu_item",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bill_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    outlet_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bill_category", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_menu_item_bill_category_id",
                table: "menu_item",
                column: "bill_category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_menu_item_bill_category_bill_category_id",
                table: "menu_item",
                column: "bill_category_id",
                principalTable: "bill_category",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menu_item_bill_category_bill_category_id",
                table: "menu_item");

            migrationBuilder.DropTable(
                name: "bill_category");

            migrationBuilder.DropIndex(
                name: "ix_menu_item_bill_category_id",
                table: "menu_item");

            migrationBuilder.DropColumn(
                name: "bill_category_id",
                table: "menu_item");
        }
    }
}
