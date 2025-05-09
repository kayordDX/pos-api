using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsUpdateStockReverse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_update_stock_reverse",
                table: "order_item_status",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("""
                insert into order_item_status("assign_group", "is_back_office", "is_billable", "is_cancelled", "is_complete", "is_front_line", "is_history", "is_notify", "is_update_stock", "order_item_status_id", "priority", "status") values (false, false, false, true, false, true, false, true, false, 8, 0, 'Kitchen Waste')
            """);

            migrationBuilder.Sql("""
                update order_item_status set is_update_stock_reverse = true, is_update_stock = true where order_item_status_id = 4                
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_update_stock_reverse",
                table: "order_item_status");

            migrationBuilder.Sql("""
                delete from order_item_status where order_item_status_id = 8;                
            """);
        }
    }
}
