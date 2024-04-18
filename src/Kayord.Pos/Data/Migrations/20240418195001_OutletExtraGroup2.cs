using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OutletExtraGroup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OutletExtraGroup_ExtraGroupId",
                table: "OutletExtraGroup",
                column: "ExtraGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OutletExtraGroup_OutletId",
                table: "OutletExtraGroup",
                column: "OutletId");

            migrationBuilder.AddForeignKey(
                name: "FK_OutletExtraGroup_ExtraGroup_ExtraGroupId",
                table: "OutletExtraGroup",
                column: "ExtraGroupId",
                principalTable: "ExtraGroup",
                principalColumn: "ExtraGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutletExtraGroup_Outlet_OutletId",
                table: "OutletExtraGroup",
                column: "OutletId",
                principalTable: "Outlet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutletExtraGroup_ExtraGroup_ExtraGroupId",
                table: "OutletExtraGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_OutletExtraGroup_Outlet_OutletId",
                table: "OutletExtraGroup");

            migrationBuilder.DropIndex(
                name: "IX_OutletExtraGroup_ExtraGroupId",
                table: "OutletExtraGroup");

            migrationBuilder.DropIndex(
                name: "IX_OutletExtraGroup_OutletId",
                table: "OutletExtraGroup");
        }
    }
}
