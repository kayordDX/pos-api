using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockOrderItemStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM "StockOrderItemStatus" WHERE "Id" > 1;
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (2, 'Done', NOW());
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (3, 'Cancelled', NOW());
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM "StockOrderItemStatus" WHERE "Id" > 1;
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (2, 'Received', NOW());
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (3, 'Partial', NOW());
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (4, 'Returned', NOW());
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (5, 'Cancelled', NOW());
            """);
        }
    }
}
