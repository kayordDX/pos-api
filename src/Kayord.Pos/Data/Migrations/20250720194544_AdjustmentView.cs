using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustmentView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"""
                CREATE OR REPLACE VIEW vw_adjustment AS
                SELECT
                    table_booking_id,
                    sum(amount) AS adjustment_amount
                FROM
                    adjustment
                GROUP BY
                    table_booking_id;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
