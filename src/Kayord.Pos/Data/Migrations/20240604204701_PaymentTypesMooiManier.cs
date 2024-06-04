using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTypesMooiManier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "PaymentType",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TipLevyPercentage",
                table: "PaymentType",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "OutletPaymentType",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false),
                    OutletId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutletPaymentType", x => new { x.OutletId, x.PaymentTypeId });
                    table.ForeignKey(
                        name: "FK_OutletPaymentType_Outlet_OutletId",
                        column: x => x.OutletId,
                        principalTable: "Outlet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutletPaymentType_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutletPaymentType_PaymentTypeId",
                table: "OutletPaymentType",
                column: "PaymentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutletPaymentType");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "PaymentType");

            migrationBuilder.DropColumn(
                name: "TipLevyPercentage",
                table: "PaymentType");
        }
    }
}
