using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class DivisionDuplicationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_role_division_role_id_division_id",
                table: "role_division",
                columns: new[] { "role_id", "division_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_division_division_name_outlet_id",
                table: "division",
                columns: new[] { "division_name", "outlet_id" },
                unique: true,
                filter: "\"is_deleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_role_division_role_id_division_id",
                table: "role_division");

            migrationBuilder.DropIndex(
                name: "ix_division_division_name_outlet_id",
                table: "division");
        }
    }
}
