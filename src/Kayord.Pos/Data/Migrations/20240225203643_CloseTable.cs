using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class CloseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                table: "TableBooking",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderItemStatusId",
                table: "OrderItem",
                column: "OrderItemStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_OrderItemStatus_OrderItemStatusId",
                table: "OrderItem",
                column: "OrderItemStatusId",
                principalTable: "OrderItemStatus",
                principalColumn: "OrderItemStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_OrderItemStatus_OrderItemStatusId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderItemStatusId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "TableBooking");
        }
    }
}
