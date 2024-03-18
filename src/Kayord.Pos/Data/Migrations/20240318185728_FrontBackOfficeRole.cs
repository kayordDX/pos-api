using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class FrontBackOfficeRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBackOffice",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "isFrontLine",
                table: "UserRole");

            migrationBuilder.AddColumn<bool>(
                name: "isBackOffice",
                table: "Role",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isFrontLine",
                table: "Role",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBackOffice",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "isFrontLine",
                table: "Role");

            migrationBuilder.AddColumn<bool>(
                name: "isBackOffice",
                table: "UserRole",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isFrontLine",
                table: "UserRole",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
