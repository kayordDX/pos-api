using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserOutletPinNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_outlet_pin_outlet_id",
                table: "user_outlet_pin",
                column: "outlet_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_outlet_pin_outlet_outlet_id",
                table: "user_outlet_pin",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_outlet_pin_user_user_id",
                table: "user_outlet_pin",
                column: "user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_outlet_pin_outlet_outlet_id",
                table: "user_outlet_pin");

            migrationBuilder.DropForeignKey(
                name: "fk_user_outlet_pin_user_user_id",
                table: "user_outlet_pin");

            migrationBuilder.DropIndex(
                name: "ix_user_outlet_pin_outlet_id",
                table: "user_outlet_pin");
        }
    }
}
