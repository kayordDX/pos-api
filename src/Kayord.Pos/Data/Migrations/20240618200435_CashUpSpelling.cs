using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class CashUpSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashupUserItem_CashupUserItemType_CashupItemTypesId",
                table: "CashupUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CashupUserItem_CashupUser_UserCashupId",
                table: "CashupUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CashupUserItemType_CashupConfig_CashupConfigId",
                table: "CashupUserItemType");

            migrationBuilder.DropForeignKey(
                name: "FK_CashupUserItemType_PaymentType_PaymentTypeId",
                table: "CashupUserItemType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashupUserItemType",
                table: "CashupUserItemType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashupUserItem",
                table: "CashupUserItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashupUser",
                table: "CashupUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashupConfig",
                table: "CashupConfig");

            migrationBuilder.RenameTable(
                name: "CashupUserItemType",
                newName: "CashUpUserItemType");

            migrationBuilder.RenameTable(
                name: "CashupUserItem",
                newName: "CashUpUserItem");

            migrationBuilder.RenameTable(
                name: "CashupUser",
                newName: "CashUpUser");

            migrationBuilder.RenameTable(
                name: "CashupConfig",
                newName: "CashUpConfig");

            migrationBuilder.RenameIndex(
                name: "IX_CashupUserItemType_PaymentTypeId",
                table: "CashUpUserItemType",
                newName: "IX_CashUpUserItemType_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CashupUserItemType_CashupConfigId",
                table: "CashUpUserItemType",
                newName: "IX_CashUpUserItemType_CashupConfigId");

            migrationBuilder.RenameIndex(
                name: "IX_CashupUserItem_UserCashupId",
                table: "CashUpUserItem",
                newName: "IX_CashUpUserItem_UserCashupId");

            migrationBuilder.RenameIndex(
                name: "IX_CashupUserItem_CashupItemTypesId",
                table: "CashUpUserItem",
                newName: "IX_CashUpUserItem_CashupItemTypesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUpUserItemType",
                table: "CashUpUserItemType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUpUserItem",
                table: "CashUpUserItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUpUser",
                table: "CashUpUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashUpConfig",
                table: "CashUpConfig",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashupItemTypesId",
                table: "CashUpUserItem",
                column: "CashupItemTypesId",
                principalTable: "CashUpUserItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItem_CashUpUser_UserCashupId",
                table: "CashUpUserItem",
                column: "UserCashupId",
                principalTable: "CashUpUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItemType_CashUpConfig_CashupConfigId",
                table: "CashUpUserItemType",
                column: "CashupConfigId",
                principalTable: "CashUpConfig",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItemType_PaymentType_PaymentTypeId",
                table: "CashUpUserItemType",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashupItemTypesId",
                table: "CashUpUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUser_UserCashupId",
                table: "CashUpUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItemType_CashUpConfig_CashupConfigId",
                table: "CashUpUserItemType");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItemType_PaymentType_PaymentTypeId",
                table: "CashUpUserItemType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUpUserItemType",
                table: "CashUpUserItemType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUpUserItem",
                table: "CashUpUserItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUpUser",
                table: "CashUpUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashUpConfig",
                table: "CashUpConfig");

            migrationBuilder.RenameTable(
                name: "CashUpUserItemType",
                newName: "CashupUserItemType");

            migrationBuilder.RenameTable(
                name: "CashUpUserItem",
                newName: "CashupUserItem");

            migrationBuilder.RenameTable(
                name: "CashUpUser",
                newName: "CashupUser");

            migrationBuilder.RenameTable(
                name: "CashUpConfig",
                newName: "CashupConfig");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItemType_PaymentTypeId",
                table: "CashupUserItemType",
                newName: "IX_CashupUserItemType_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItemType_CashupConfigId",
                table: "CashupUserItemType",
                newName: "IX_CashupUserItemType_CashupConfigId");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItem_UserCashupId",
                table: "CashupUserItem",
                newName: "IX_CashupUserItem_UserCashupId");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItem_CashupItemTypesId",
                table: "CashupUserItem",
                newName: "IX_CashupUserItem_CashupItemTypesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashupUserItemType",
                table: "CashupUserItemType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashupUserItem",
                table: "CashupUserItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashupUser",
                table: "CashupUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashupConfig",
                table: "CashupConfig",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashupUserItem_CashupUserItemType_CashupItemTypesId",
                table: "CashupUserItem",
                column: "CashupItemTypesId",
                principalTable: "CashupUserItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashupUserItem_CashupUser_UserCashupId",
                table: "CashupUserItem",
                column: "UserCashupId",
                principalTable: "CashupUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashupUserItemType_CashupConfig_CashupConfigId",
                table: "CashupUserItemType",
                column: "CashupConfigId",
                principalTable: "CashupConfig",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashupUserItemType_PaymentType_PaymentTypeId",
                table: "CashupUserItemType",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "PaymentTypeId");
        }
    }
}
