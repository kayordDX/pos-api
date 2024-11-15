using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuItemAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "MenuSection",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MenuSection",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "MenuSection",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "MenuSection",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "MenuItem",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MenuItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "MenuItem",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "MenuItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Menu",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Menu",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Menu",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Menu",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "MenuSection");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MenuSection");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "MenuSection");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "MenuSection");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Menu");
        }
    }
}
