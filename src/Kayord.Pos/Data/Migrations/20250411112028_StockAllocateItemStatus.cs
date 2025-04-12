using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockAllocateItemStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM stock_allocate_item_status;
                INSERT INTO stock_allocate_item_status(id, name, created) values(1, 'Draft', NOW());
                INSERT INTO stock_allocate_item_status(id, name, created) values(2, 'Waiting', NOW());
                INSERT INTO stock_allocate_item_status(id, name, created) values(3, 'Cancelled', NOW());
                INSERT INTO stock_allocate_item_status(id, name, created) values(4, 'Approved', NOW());
                INSERT INTO stock_allocate_item_status(id, name, created) values(5, 'Rejected', NOW());
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
