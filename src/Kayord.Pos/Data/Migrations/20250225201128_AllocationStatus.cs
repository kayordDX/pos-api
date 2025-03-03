using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllocationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO "StockAllocateStatus" ("Id", "Name", "Created") VALUES (1, 'Pending', NOW());
                INSERT INTO "StockAllocateStatus" ("Id", "Name", "Created") VALUES (2, 'In Progress', NOW());
                INSERT INTO "StockAllocateStatus" ("Id", "Name", "Created") VALUES (3, 'Done', NOW());

                INSERT INTO "StockAllocateItemStatus" ("Id", "Name", "Created") VALUES (1, 'Waiting', NOW());
                INSERT INTO "StockAllocateItemStatus" ("Id", "Name", "Created") VALUES (2, 'Approved', NOW());
                INSERT INTO "StockAllocateItemStatus" ("Id", "Name", "Created") VALUES (3, 'Rejected', NOW());

                INSERT INTO "StockItemAuditType" ("Id", "Name") VALUES (1, 'Order Item');
                INSERT INTO "StockItemAuditType" ("Id", "Name") VALUES (2, 'Stock Allocate');
                INSERT INTO "StockItemAuditType" ("Id", "Name") VALUES (3, 'Stock Order');
                INSERT INTO "StockItemAuditType" ("Id", "Name") VALUES (4, 'Stock Update');
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM "StockAllocateStatus";
                DELETE FROM "StockAllocateItemStatus";
                DELETE FROM "StockItemAuditType";
            """);
        }
    }
}
