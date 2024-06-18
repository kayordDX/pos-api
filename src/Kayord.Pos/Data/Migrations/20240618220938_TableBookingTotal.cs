using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableBookingTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashupItemTypesId",
                table: "CashUpUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUser_UserCashupId",
                table: "CashUpUserItem");

            migrationBuilder.DropIndex(
                name: "IX_CashUpUserItem_UserCashupId",
                table: "CashUpUserItem");

            migrationBuilder.RenameColumn(
                name: "UserCashupId",
                table: "CashUpUserItem",
                newName: "UserCashUpId");

            migrationBuilder.RenameColumn(
                name: "CashupItemTypesId",
                table: "CashUpUserItem",
                newName: "CashUpItemTypesId");

            migrationBuilder.RenameColumn(
                name: "CashupItemTypeId",
                table: "CashUpUserItem",
                newName: "CashUpItemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItem_CashupItemTypesId",
                table: "CashUpUserItem",
                newName: "IX_CashUpUserItem_CashUpItemTypesId");

            migrationBuilder.AddColumn<int>(
                name: "CashUpUserId",
                table: "TableBooking",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "TableBooking",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CashUpUserId",
                table: "CashUpUserItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TableBooking_CashUpUserId",
                table: "TableBooking",
                column: "CashUpUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TableBookingId",
                table: "Payment",
                column: "TableBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_CashUpUserItem_CashUpUserId",
                table: "CashUpUserItem",
                column: "CashUpUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashUpItemTypesId",
                table: "CashUpUserItem",
                column: "CashUpItemTypesId",
                principalTable: "CashUpUserItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CashUpUserItem_CashUpUser_CashUpUserId",
                table: "CashUpUserItem",
                column: "CashUpUserId",
                principalTable: "CashUpUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_TableBooking_TableBookingId",
                table: "Payment",
                column: "TableBookingId",
                principalTable: "TableBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooking_CashUpUser_CashUpUserId",
                table: "TableBooking",
                column: "CashUpUserId",
                principalTable: "CashUpUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUserItemType_CashUpItemTypesId",
                table: "CashUpUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CashUpUserItem_CashUpUser_CashUpUserId",
                table: "CashUpUserItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_TableBooking_TableBookingId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_TableBooking_CashUpUser_CashUpUserId",
                table: "TableBooking");

            migrationBuilder.DropIndex(
                name: "IX_TableBooking_CashUpUserId",
                table: "TableBooking");

            migrationBuilder.DropIndex(
                name: "IX_Payment_TableBookingId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_CashUpUserItem_CashUpUserId",
                table: "CashUpUserItem");

            migrationBuilder.DropColumn(
                name: "CashUpUserId",
                table: "TableBooking");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "TableBooking");

            migrationBuilder.DropColumn(
                name: "CashUpUserId",
                table: "CashUpUserItem");

            migrationBuilder.RenameColumn(
                name: "UserCashUpId",
                table: "CashUpUserItem",
                newName: "UserCashupId");

            migrationBuilder.RenameColumn(
                name: "CashUpItemTypesId",
                table: "CashUpUserItem",
                newName: "CashupItemTypesId");

            migrationBuilder.RenameColumn(
                name: "CashUpItemTypeId",
                table: "CashUpUserItem",
                newName: "CashupItemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CashUpUserItem_CashUpItemTypesId",
                table: "CashUpUserItem",
                newName: "IX_CashUpUserItem_CashupItemTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_CashUpUserItem_UserCashupId",
                table: "CashUpUserItem",
                column: "UserCashupId");

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
        }
    }
}
