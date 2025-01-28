using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockOrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    StockOrderStatusId = table.Column<int>(type: "integer", nullable: false),
                    StockLocationId = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockOrder_StockLocation_StockLocationId",
                        column: x => x.StockLocationId,
                        principalTable: "StockLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockOrder_StockOrderStatus_StockOrderStatusId",
                        column: x => x.StockOrderStatusId,
                        principalTable: "StockOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockOrder_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockOrderItem",
                columns: table => new
                {
                    StockOrderId = table.Column<int>(type: "integer", nullable: false),
                    StockItemId = table.Column<int>(type: "integer", nullable: false),
                    StockItemStockId = table.Column<int>(type: "integer", nullable: false),
                    StockItemStockLocationId = table.Column<int>(type: "integer", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    Actual = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOrderItem", x => new { x.StockOrderId, x.StockItemId });
                    table.ForeignKey(
                        name: "FK_StockOrderItem_StockItem_StockItemStockId_StockItemStockLoc~",
                        columns: x => new { x.StockItemStockId, x.StockItemStockLocationId },
                        principalTable: "StockItem",
                        principalColumns: new[] { "StockId", "StockLocationId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockOrderItem_StockOrder_StockOrderId",
                        column: x => x.StockOrderId,
                        principalTable: "StockOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockOrder_StockLocationId",
                table: "StockOrder",
                column: "StockLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOrder_StockOrderStatusId",
                table: "StockOrder",
                column: "StockOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOrder_SupplierId",
                table: "StockOrder",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderItem_StockItemStockId_StockItemStockLocationId",
                table: "StockOrderItem",
                columns: new[] { "StockItemStockId", "StockItemStockLocationId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockOrderItem");

            migrationBuilder.DropTable(
                name: "StockOrder");

            migrationBuilder.DropTable(
                name: "StockOrderStatus");
        }
    }
}
