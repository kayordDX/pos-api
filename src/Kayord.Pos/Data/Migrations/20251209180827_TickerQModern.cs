using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class TickerQModern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CronTickerOccurrences",
                schema: "ticker");

            migrationBuilder.DropTable(
                name: "TimeTickers",
                schema: "ticker");

            migrationBuilder.DropTable(
                name: "CronTickers",
                schema: "ticker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ticker");

            migrationBuilder.CreateTable(
                name: "CronTickers",
                schema: "ticker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    expression = table.Column<string>(type: "text", nullable: true),
                    function = table.Column<string>(type: "text", nullable: true),
                    init_identifier = table.Column<string>(type: "text", nullable: true),
                    request = table.Column<byte[]>(type: "bytea", nullable: true),
                    retries = table.Column<int>(type: "integer", nullable: false),
                    retry_intervals = table.Column<int[]>(type: "integer[]", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cron_tickers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TimeTickers",
                schema: "ticker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_parent = table.Column<Guid>(type: "uuid", nullable: true),
                    batch_run_condition = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    elapsed_time = table.Column<long>(type: "bigint", nullable: false),
                    exception = table.Column<string>(type: "text", nullable: true),
                    executed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    execution_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    function = table.Column<string>(type: "text", nullable: true),
                    init_identifier = table.Column<string>(type: "text", nullable: true),
                    lock_holder = table.Column<string>(type: "text", nullable: true),
                    locked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    request = table.Column<byte[]>(type: "bytea", nullable: true),
                    retries = table.Column<int>(type: "integer", nullable: false),
                    retry_count = table.Column<int>(type: "integer", nullable: false),
                    retry_intervals = table.Column<int[]>(type: "integer[]", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time_tickers", x => x.id);
                    table.ForeignKey(
                        name: "fk_time_tickers_time_tickers_batch_parent",
                        column: x => x.batch_parent,
                        principalSchema: "ticker",
                        principalTable: "TimeTickers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CronTickerOccurrences",
                schema: "ticker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cron_ticker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    elapsed_time = table.Column<long>(type: "bigint", nullable: false),
                    exception = table.Column<string>(type: "text", nullable: true),
                    executed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    execution_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lock_holder = table.Column<string>(type: "text", nullable: true),
                    locked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    retry_count = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cron_ticker_occurrences", x => x.id);
                    table.ForeignKey(
                        name: "fk_cron_ticker_occurrences_cron_tickers_cron_ticker_id",
                        column: x => x.cron_ticker_id,
                        principalSchema: "ticker",
                        principalTable: "CronTickers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CronTickerOccurrence_CronTickerId",
                schema: "ticker",
                table: "CronTickerOccurrences",
                column: "cron_ticker_id");

            migrationBuilder.CreateIndex(
                name: "IX_CronTickerOccurrence_ExecutionTime",
                schema: "ticker",
                table: "CronTickerOccurrences",
                column: "execution_time");

            migrationBuilder.CreateIndex(
                name: "IX_CronTickerOccurrence_Status_ExecutionTime",
                schema: "ticker",
                table: "CronTickerOccurrences",
                columns: new[] { "status", "execution_time" });

            migrationBuilder.CreateIndex(
                name: "UQ_CronTickerId_ExecutionTime",
                schema: "ticker",
                table: "CronTickerOccurrences",
                columns: new[] { "cron_ticker_id", "execution_time" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CronTickers_Expression",
                schema: "ticker",
                table: "CronTickers",
                column: "expression");

            migrationBuilder.CreateIndex(
                name: "ix_time_tickers_batch_parent",
                schema: "ticker",
                table: "TimeTickers",
                column: "batch_parent");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTicker_ExecutionTime",
                schema: "ticker",
                table: "TimeTickers",
                column: "execution_time");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTicker_Status_ExecutionTime",
                schema: "ticker",
                table: "TimeTickers",
                columns: new[] { "status", "execution_time" });
        }
    }
}
