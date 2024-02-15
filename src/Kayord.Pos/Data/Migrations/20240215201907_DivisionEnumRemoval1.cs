using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class DivisionEnumRemoval1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Division_DivisionId",
                table: "MenuItem");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "MenuItem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Division_DivisionId",
                table: "MenuItem",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Division_DivisionId",
                table: "MenuItem");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "MenuItem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Division_DivisionId",
                table: "MenuItem",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
