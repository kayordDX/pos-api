using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnumStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isAuto",
                table: "CashUpUserItemType",
                newName: "IsAuto");

            migrationBuilder.AddColumn<int>(
                name: "AdjustmentId",
                table: "CashUpUserItemType",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdjustmentTypeId",
                table: "CashUpUserItemType",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CashUpUserItemRule",
                table: "CashUpUserItemType",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CashUpUserItemType_AdjustmentTypeId",
                table: "CashUpUserItemType",
                column: "AdjustmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItemType_AdjustmentType_AdjustmentTypeId",
                table: "CashUpUserItemType",
                column: "AdjustmentTypeId",
                principalTable: "AdjustmentType",
                principalColumn: "AdjustmentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItemType_AdjustmentType_AdjustmentTypeId",
                table: "CashUpUserItemType");

            migrationBuilder.DropIndex(
                name: "IX_CashUpUserItemType_AdjustmentTypeId",
                table: "CashUpUserItemType");

            migrationBuilder.DropColumn(
                name: "AdjustmentId",
                table: "CashUpUserItemType");

            migrationBuilder.DropColumn(
                name: "AdjustmentTypeId",
                table: "CashUpUserItemType");

            migrationBuilder.DropColumn(
                name: "CashUpUserItemRule",
                table: "CashUpUserItemType");

            migrationBuilder.RenameColumn(
                name: "IsAuto",
                table: "CashUpUserItemType",
                newName: "isAuto");
        }
    }
}
