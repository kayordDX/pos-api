using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestartIdentityColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE adjustment_type_outlet ALTER "id" RESTART 150;
                ALTER TABLE adjustment_type ALTER adjustment_type_id RESTART 150;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
