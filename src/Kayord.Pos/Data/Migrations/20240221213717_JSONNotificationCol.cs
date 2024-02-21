using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class JSONNotificationCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JSON",
                table: "UserNotification");

            migrationBuilder.AddColumn<string>(
                name: "JSONContent",
                table: "UserNotification",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JSONContent",
                table: "UserNotification");

            migrationBuilder.AddColumn<string>(
                name: "JSON",
                table: "UserNotification",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
