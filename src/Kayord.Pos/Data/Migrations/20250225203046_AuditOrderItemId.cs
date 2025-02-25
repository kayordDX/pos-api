using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditOrderItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "StockItemAudit",
                type: "integer",
                nullable: true);

            migrationBuilder.Sql("""
                INSERT INTO "DivisionType" ("Id", "DivisionName") VALUES (1, 'Default');
                INSERT INTO "DivisionType" ("Id", "DivisionName") VALUES (2, 'Goods Receiving');
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "StockItemAudit");

            migrationBuilder.Sql("""
                DELETE FROM "DivisionType";
            """);
        }
    }
}
