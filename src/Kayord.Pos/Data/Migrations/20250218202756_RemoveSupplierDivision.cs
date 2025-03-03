using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSupplierDivision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Division_DivisionId",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_DivisionId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Supplier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "Supplier",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_DivisionId",
                table: "Supplier",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Division_DivisionId",
                table: "Supplier",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
