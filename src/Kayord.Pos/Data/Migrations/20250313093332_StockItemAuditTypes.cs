using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockItemAuditTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""                
                delete from stock_item_audit_type;
                insert into stock_item_audit_type(id, name) values(1, 'Order Menu Item');
                insert into stock_item_audit_type(id, name) values(2, 'Order Extra');
                insert into stock_item_audit_type(id, name) values(3, 'Order Option');
                insert into stock_item_audit_type(id, name) values(4, 'Stock Allocate');
                insert into stock_item_audit_type(id, name) values(5, 'Stock Order');
                insert into stock_item_audit_type(id, name) values(6, 'Stock Update');
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
