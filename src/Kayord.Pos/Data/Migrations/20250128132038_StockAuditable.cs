using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "StockOrderStatus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "StockOrderStatus",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "StockOrderStatus",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "StockOrderStatus",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "StockOrderItem",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "StockOrderItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "StockOrderItem",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "StockOrderItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "StockOrder",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "StockOrder",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "StockOrder",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "StockOrder",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Stock",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Stock",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Stock",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Stock",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "StockOrderStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StockOrderStatus");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "StockOrderStatus");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "StockOrderStatus");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "StockOrderItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StockOrderItem");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "StockOrderItem");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "StockOrderItem");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "StockOrder");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "StockOrder");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "StockOrder");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "StockOrder");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Stock");
        }
    }
}
