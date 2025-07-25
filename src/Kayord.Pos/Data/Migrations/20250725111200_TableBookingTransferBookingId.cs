using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableBookingTransferBookingId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "table_booking_id",
                table: "table_booking_transfer",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "table_booking_id",
                table: "table_booking_transfer");
        }
    }
}
