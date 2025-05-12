using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemStatusWasteHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                insert into "order_item_status" ("assign_group", "is_back_office", "is_billable", "is_cancelled", "is_complete", "is_front_line", "is_history", "is_notify", "is_update_stock", "is_update_stock_reverse", "order_item_status_id", "priority", "status") values (false, false, false, true, false, true, false, true, false, false, 9, 0, 'History Waste');
                insert into "order_item_status" ("assign_group", "is_back_office", "is_billable", "is_cancelled", "is_complete", "is_front_line", "is_history", "is_notify", "is_update_stock", "is_update_stock_reverse", "order_item_status_id", "priority", "status") values (false, false, false, true, false, true, false, true, true, true, 10, 0, 'History Cancel');
                update order_item_status set is_update_stock = true, is_update_stock_reverse = true where order_item_status_id = 7;
                update order_item_status set is_update_stock = true, is_update_stock_reverse = false where order_item_status_id = 8;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                delete from order_item_status where order_item_status_id > 8                
            """);
        }
    }
}
