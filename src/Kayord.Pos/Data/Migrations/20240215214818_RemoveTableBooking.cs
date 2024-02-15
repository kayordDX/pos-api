using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableOrder");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemOption_OptionId",
                table: "OrderItemOption",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemOption_OrderItemId",
                table: "OrderItemOption",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemExtra_ExtraId",
                table: "OrderItemExtra",
                column: "ExtraId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemExtra_OrderItemId",
                table: "OrderItemExtra",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_TableBookingId",
                table: "OrderItem",
                column: "TableBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_TableBooking_TableBookingId",
                table: "OrderItem",
                column: "TableBookingId",
                principalTable: "TableBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemExtra_Extra_ExtraId",
                table: "OrderItemExtra",
                column: "ExtraId",
                principalTable: "Extra",
                principalColumn: "ExtraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemExtra_OrderItem_OrderItemId",
                table: "OrderItemExtra",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemOption_Option_OptionId",
                table: "OrderItemOption",
                column: "OptionId",
                principalTable: "Option",
                principalColumn: "OptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemOption_OrderItem_OrderItemId",
                table: "OrderItemOption",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_TableBooking_TableBookingId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemExtra_Extra_ExtraId",
                table: "OrderItemExtra");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemExtra_OrderItem_OrderItemId",
                table: "OrderItemExtra");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemOption_Option_OptionId",
                table: "OrderItemOption");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemOption_OrderItem_OrderItemId",
                table: "OrderItemOption");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemOption_OptionId",
                table: "OrderItemOption");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemOption_OrderItemId",
                table: "OrderItemOption");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemExtra_ExtraId",
                table: "OrderItemExtra");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemExtra_OrderItemId",
                table: "OrderItemExtra");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_TableBookingId",
                table: "OrderItem");

            migrationBuilder.CreateTable(
                name: "TableOrder",
                columns: table => new
                {
                    TableOrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    OrderItemId = table.Column<int>(type: "integer", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TableBookingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableOrder", x => x.TableOrderId);
                    table.ForeignKey(
                        name: "FK_TableOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableOrder_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "OrderItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableOrder_CustomerId",
                table: "TableOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TableOrder_OrderItemId",
                table: "TableOrder",
                column: "OrderItemId");
        }
    }
}
