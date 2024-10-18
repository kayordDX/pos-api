using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCategory", x => x.Id);
                });

            migrationBuilder.Sql($"""
                INSERT INTO "StockCategory" ("Id", "Name") VALUES (1, 'Default');
            """);

            migrationBuilder.AddColumn<int>(
                name: "StockCategoryId",
                table: "Stock",
                type: "integer",
                nullable: false,
                defaultValue: 1
            );

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockCategoryId",
                table: "Stock",
                column: "StockCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_StockCategory_StockCategoryId",
                table: "Stock",
                column: "StockCategoryId",
                principalTable: "StockCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_StockCategory_StockCategoryId",
                table: "Stock");

            migrationBuilder.DropTable(
                name: "StockCategory");

            migrationBuilder.DropIndex(
                name: "IX_Stock_StockCategoryId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "StockCategoryId",
                table: "Stock");
        }
    }
}
