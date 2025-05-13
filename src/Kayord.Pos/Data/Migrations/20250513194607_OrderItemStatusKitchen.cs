using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemStatusKitchen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                update order_item_status set is_update_stock = true, is_update_stock_reverse = false where order_item_status_id = 2;
                update order_item_status set is_update_stock = true, is_update_stock_reverse = true where order_item_status_id = 4;
                update order_item_status set is_update_stock = false, is_update_stock_reverse = false where order_item_status_id = 5;
                update order_item_status set is_update_stock = false, is_update_stock_reverse = false where order_item_status_id = 7;
                update order_item_status set is_update_stock = false, is_update_stock_reverse = false where order_item_status_id = 8;
                delete from order_item_status where order_item_status_id > 8;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
