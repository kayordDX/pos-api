using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class CashuptypesNullIdAndPayId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashupUserItemType_PaymentType_PaymentTypeId",
                table: "CashupUserItemType");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTypeId",
                table: "CashupUserItemType",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CashupUserItemType_PaymentType_PaymentTypeId",
                table: "CashupUserItemType",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashupUserItemType_PaymentType_PaymentTypeId",
                table: "CashupUserItemType");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTypeId",
                table: "CashupUserItemType",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CashupUserItemType_PaymentType_PaymentTypeId",
                table: "CashupUserItemType",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
