using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MaybeAChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashUpItemTypesId",
                table: "CashUpUserItem");

            migrationBuilder.DropColumn(
                name: "CashUpItemTypeId",
                table: "CashUpUserItem");

            migrationBuilder.DropColumn(
                name: "ClosingBalance",
                table: "CashUpUserItem");

            migrationBuilder.RenameColumn(
                name: "OpeningBalance",
                table: "CashUpUserItem",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "CashUpItemTypesId",
                table: "CashUpUserItem",
                newName: "CashUpUserItemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItem_CashUpItemTypesId",
                table: "CashUpUserItem",
                newName: "IX_CashUpUserItem_CashUpUserItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashUpUserItemTypeId",
                table: "CashUpUserItem",
                column: "CashUpUserItemTypeId",
                principalTable: "CashUpUserItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashUpUserItemTypeId",
                table: "CashUpUserItem");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "CashUpUserItem",
                newName: "OpeningBalance");

            migrationBuilder.RenameColumn(
                name: "CashUpUserItemTypeId",
                table: "CashUpUserItem",
                newName: "CashUpItemTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItem_CashUpUserItemTypeId",
                table: "CashUpUserItem",
                newName: "IX_CashUpUserItem_CashUpItemTypesId");

            migrationBuilder.AddColumn<int>(
                name: "CashUpItemTypeId",
                table: "CashUpUserItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosingBalance",
                table: "CashUpUserItem",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashUpItemTypesId",
                table: "CashUpUserItem",
                column: "CashUpItemTypesId",
                principalTable: "CashUpUserItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
