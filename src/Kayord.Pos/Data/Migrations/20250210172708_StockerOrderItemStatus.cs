using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockerOrderItemStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OrderAmount",
                table: "StockOrderItem",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockOrderItemStatusId",
                table: "StockOrderItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StockOrderItemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOrderItemStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderItem_StockOrderItemStatusId",
                table: "StockOrderItem",
                column: "StockOrderItemStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderItem_StockOrderItemStatus_StockOrderItemStatusId",
                table: "StockOrderItem",
                column: "StockOrderItemStatusId",
                principalTable: "StockOrderItemStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("""
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (1, 'Pending', NOW());
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (2, 'Received', NOW());
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (3, 'Partial', NOW());
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (4, 'Returned', NOW());
                INSERT INTO "StockOrderItemStatus" ("Id", "Name", "Created") VALUES (5, 'Cancelled', NOW());
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderItem_StockOrderItemStatus_StockOrderItemStatusId",
                table: "StockOrderItem");

            migrationBuilder.DropTable(
                name: "StockOrderItemStatus");

            migrationBuilder.DropIndex(
                name: "IX_StockOrderItem_StockOrderItemStatusId",
                table: "StockOrderItem");

            migrationBuilder.DropColumn(
                name: "OrderAmount",
                table: "StockOrderItem");

            migrationBuilder.DropColumn(
                name: "StockOrderItemStatusId",
                table: "StockOrderItem");

            migrationBuilder.Sql("""
                DELETE FROM "StockOrderItemStatus";
            """);
        }
    }
}
