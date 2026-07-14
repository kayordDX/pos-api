using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.TickerQMigrations
{
    /// <inheritdoc />
    public partial class TickerQNext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_system_paused",
                schema: "ticker",
                table: "CronTickers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_system_paused",
                schema: "ticker",
                table: "CronTickers");
        }
    }
}
