using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockPeriodSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stock_period_snapshot",
                columns: table => new
                {
                    stock_item_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stock_id = table.Column<int>(type: "integer", nullable: false),
                    division_id = table.Column<int>(type: "integer", nullable: false),
                    threshold = table.Column<decimal>(type: "numeric", nullable: false),
                    actual = table.Column<decimal>(type: "numeric", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    outlet_id = table.Column<int>(type: "integer", nullable: false),
                    sales_period_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_period_snapshot", x => x.stock_item_id);
                    table.ForeignKey(
                        name: "fk_stock_period_snapshot_division_division_id",
                        column: x => x.division_id,
                        principalTable: "division",
                        principalColumn: "division_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_stock_period_snapshot_stock_stock_id",
                        column: x => x.stock_id,
                        principalTable: "stock",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_stock_period_snapshot_division_id",
                table: "stock_period_snapshot",
                column: "division_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_period_snapshot_stock_id",
                table: "stock_period_snapshot",
                column: "stock_id");

            migrationBuilder.Sql("""
                create or replace procedure sp_capture_stock_period_snapshot(IN p_sales_period_id integer, IN p_created_by text, IN p_outlet_id integer)
                    language plpgsql
                as
                $$
                BEGIN
                    -- Optional: prevent duplicate snapshots for the same period/outlet
                    DELETE FROM stock_period_snapshot
                    WHERE sales_period_id = p_sales_period_id
                      AND outlet_id = p_outlet_id;

                    INSERT INTO stock_period_snapshot (
                          stock_item_id,
                          stock_id,
                          division_id,
                          threshold,
                          actual,
                          updated,
                          outlet_id,
                          sales_period_id,
                          created_by,
                          created
                    )
                    SELECT
                          si.id,
                          si.stock_id,
                          si.division_id,
                          si.threshold,
                          si.actual,
                          si.updated,
                          d.outlet_id,
                          p_sales_period_id,
                          p_created_by,
                          NOW()
                    FROM stock_item si
                    JOIN division d
                      ON d.division_id = si.division_id
                    WHERE d.outlet_id = p_outlet_id;

                END;
                $$;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock_period_snapshot");
        }
    }
}
