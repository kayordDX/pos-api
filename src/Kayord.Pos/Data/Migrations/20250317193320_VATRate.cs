using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class VATRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stock_allocate_item_id",
                table: "stock_item_audit",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stock_order_item_id",
                table: "stock_item_audit",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_available",
                table: "option",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_available",
                table: "extra",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "vat_rate",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vat_rate", x => x.id);
                });

            migrationBuilder.Sql("""
                insert into vat_rate(id, value, start_date, end_date) values(1, 0.15, '2018-04-01', '2025-05-01');
                insert into vat_rate(id, value, start_date, end_date) values(2, 0.155, '2025-05-01', '2026-04-01');
                insert into vat_rate(id, value, start_date, end_date) values(3, 0.16, '2026-04-01', '2045-12-31');
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vat_rate");

            migrationBuilder.DropColumn(
                name: "stock_allocate_item_id",
                table: "stock_item_audit");

            migrationBuilder.DropColumn(
                name: "stock_order_item_id",
                table: "stock_item_audit");

            migrationBuilder.DropColumn(
                name: "is_available",
                table: "option");

            migrationBuilder.DropColumn(
                name: "is_available",
                table: "extra");
        }
    }
}
