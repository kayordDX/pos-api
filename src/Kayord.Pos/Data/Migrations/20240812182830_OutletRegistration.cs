using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OutletRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Outlet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Outlet",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Registration",
                table: "Outlet",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Outlet");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Outlet");

            migrationBuilder.DropColumn(
                name: "Registration",
                table: "Outlet");
        }
    }
}
