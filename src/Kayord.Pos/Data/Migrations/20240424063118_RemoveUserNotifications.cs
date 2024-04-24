using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNotification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationUser",
                table: "NotificationUser");


            // Did not work needed the using statement
            // migrationBuilder.AlterColumn<string>(
            //     name: "Payload",
            //     table: "NotificationLog",
            //     type: "jsonb",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);

            migrationBuilder.Sql("""
                ALTER TABLE "NotificationLog"  ALTER COLUMN "Payload" TYPE jsonb USING "Payload"::jsonb;
            """);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationUser",
                table: "NotificationUser",
                columns: new[] { "UserId", "Token" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationUser",
                table: "NotificationUser");

            migrationBuilder.AlterColumn<string>(
                name: "Payload",
                table: "NotificationLog",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationUser",
                table: "NotificationUser",
                columns: new[] { "Token", "UserId" });

            migrationBuilder.CreateTable(
                name: "UserNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateRead = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateSent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    JSONContent = table.Column<string>(type: "jsonb", nullable: true),
                    Notification = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotification", x => x.Id);
                });
        }
    }
}
