using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_table_booking_user_id",
                table: "table_booking");

            migrationBuilder.DropIndex(
                name: "ix_order_item_table_booking_id",
                table: "order_item");

            migrationBuilder.CreateIndex(
                name: "ix_user_outlet_user_id_is_current",
                table: "user_outlet",
                columns: new[] { "user_id", "is_current" });

            migrationBuilder.CreateIndex(
                name: "ix_table_booking_user_id_close_date",
                table: "table_booking",
                columns: new[] { "user_id", "close_date" });

            migrationBuilder.CreateIndex(
                name: "ix_order_item_table_booking_id_order_item_status_id",
                table: "order_item",
                columns: new[] { "table_booking_id", "order_item_status_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_user_outlet_user_id_is_current",
                table: "user_outlet");

            migrationBuilder.DropIndex(
                name: "ix_table_booking_user_id_close_date",
                table: "table_booking");

            migrationBuilder.DropIndex(
                name: "ix_order_item_table_booking_id_order_item_status_id",
                table: "order_item");

            migrationBuilder.CreateIndex(
                name: "ix_table_booking_user_id",
                table: "table_booking",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_table_booking_id",
                table: "order_item",
                column: "table_booking_id");
        }
    }
}
