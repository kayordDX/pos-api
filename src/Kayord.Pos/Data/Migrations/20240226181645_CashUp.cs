using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class CashUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableCashUp");

            migrationBuilder.CreateTable(
                name: "CashUp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashUpTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    TableCount = table.Column<int>(type: "integer", nullable: false),
                    CashUpBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    CashUpTotalPayments = table.Column<decimal>(type: "numeric", nullable: false),
                    SalesPeriodId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    SignOffUserId = table.Column<string>(type: "text", nullable: false),
                    SignOffDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashUp_SalesPeriod_SalesPeriodId",
                        column: x => x.SalesPeriodId,
                        principalTable: "SalesPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashUp_SalesPeriodId",
                table: "CashUp",
                column: "SalesPeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashUp");

            migrationBuilder.CreateTable(
                name: "TableCashUp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    TableBookingId = table.Column<int>(type: "integer", nullable: false),
                    CashUpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SalesAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableCashUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableCashUp_Outlet_OutletId",
                        column: x => x.OutletId,
                        principalTable: "Outlet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableCashUp_TableBooking_TableBookingId",
                        column: x => x.TableBookingId,
                        principalTable: "TableBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableCashUp_OutletId",
                table: "TableCashUp",
                column: "OutletId");

            migrationBuilder.CreateIndex(
                name: "IX_TableCashUp_TableBookingId",
                table: "TableCashUp",
                column: "TableBookingId");
        }
    }
}
