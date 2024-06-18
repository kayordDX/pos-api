using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class CashuptypesNullId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashupUserItemType_CashupConfig_CashupConfigId",
                table: "CashupUserItemType");

            migrationBuilder.AlterColumn<int>(
                name: "CashupConfigId",
                table: "CashupUserItemType",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CashupUserItemType_CashupConfig_CashupConfigId",
                table: "CashupUserItemType",
                column: "CashupConfigId",
                principalTable: "CashupConfig",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashupUserItemType_CashupConfig_CashupConfigId",
                table: "CashupUserItemType");

            migrationBuilder.AlterColumn<int>(
                name: "CashupConfigId",
                table: "CashupUserItemType",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CashupUserItemType_CashupConfig_CashupConfigId",
                table: "CashupUserItemType",
                column: "CashupConfigId",
                principalTable: "CashupConfig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
