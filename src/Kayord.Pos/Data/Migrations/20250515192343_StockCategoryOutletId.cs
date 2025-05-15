using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockCategoryOutletId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "stock_category",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "stock_category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified",
                table: "stock_category",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_modified_by",
                table: "stock_category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "outlet_id",
                table: "stock_category",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created",
                table: "stock_category");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "stock_category");

            migrationBuilder.DropColumn(
                name: "last_modified",
                table: "stock_category");

            migrationBuilder.DropColumn(
                name: "last_modified_by",
                table: "stock_category");

            migrationBuilder.DropColumn(
                name: "outlet_id",
                table: "stock_category");
        }
    }
}
