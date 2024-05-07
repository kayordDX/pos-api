using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustmentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Adjustment_AdjustmentTypeId",
                table: "Adjustment",
                column: "AdjustmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adjustment_AdjustmentType_AdjustmentTypeId",
                table: "Adjustment",
                column: "AdjustmentTypeId",
                principalTable: "AdjustmentType",
                principalColumn: "AdjustmentTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adjustment_AdjustmentType_AdjustmentTypeId",
                table: "Adjustment");

            migrationBuilder.DropIndex(
                name: "IX_Adjustment_AdjustmentTypeId",
                table: "Adjustment");
        }
    }
}
