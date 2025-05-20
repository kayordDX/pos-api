using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleOutletName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_outlet_outlet_id",
                table: "user_outlet",
                column: "outlet_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_outlet_outlet_outlet_id",
                table: "user_outlet",
                column: "outlet_id",
                principalTable: "outlet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_outlet_outlet_outlet_id",
                table: "user_outlet");

            migrationBuilder.DropIndex(
                name: "ix_user_outlet_outlet_id",
                table: "user_outlet");
        }
    }
}
