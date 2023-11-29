using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClockToOutlet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clock_SalesPeriod_SalesPeriodId",
                table: "Clock");

            migrationBuilder.RenameColumn(
                name: "SalesPeriodId",
                table: "Clock",
                newName: "OutletId");

            migrationBuilder.RenameIndex(
                name: "IX_Clock_SalesPeriodId",
                table: "Clock",
                newName: "IX_Clock_OutletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clock_Outlet_OutletId",
                table: "Clock",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clock_Outlet_OutletId",
                table: "Clock");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "Clock",
                newName: "SalesPeriodId");

            migrationBuilder.RenameIndex(
                name: "IX_Clock_OutletId",
                table: "Clock",
                newName: "IX_Clock_SalesPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clock_SalesPeriod_SalesPeriodId",
                table: "Clock",
                column: "SalesPeriodId",
                principalTable: "SalesPeriod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
