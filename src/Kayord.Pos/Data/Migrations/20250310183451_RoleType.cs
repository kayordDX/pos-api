using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "role_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_front_line = table.Column<bool>(type: "boolean", nullable: false),
                    is_back_office = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_type", x => x.id);
                });

            migrationBuilder.Sql("""
                INSERT INTO role_type (id, name, is_front_line, is_back_office, description)    VALUES (1, 'guest', false, false, 'Guest');
                INSERT INTO role_type (id, name, is_front_line, is_back_office, description)    VALUES (2, 'front', true, false, 'Front Line User');
                INSERT INTO role_type (id, name, is_front_line, is_back_office, description)    VALUES (3, 'back', false, true, 'Back Office User');
                INSERT INTO role_type (id, name, is_front_line, is_back_office, description)    VALUES (4, 'manager', true, true, 'Manager');
            """);

            migrationBuilder.DropColumn(
                name: "is_back_office",
                table: "role");

            migrationBuilder.DropColumn(
                name: "is_front_line",
                table: "role");

            migrationBuilder.AddColumn<DateTime>(
                name: "created",
                table: "role",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "role",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified",
                table: "role",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_modified_by",
                table: "role",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "role_type_id",
                table: "role",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("""
                UPDATE role SET role_type_id = 1;
            """);

            migrationBuilder.CreateIndex(
                name: "ix_role_role_type_id",
                table: "role",
                column: "role_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_role_role_type_role_type_id",
                table: "role",
                column: "role_type_id",
                principalTable: "role_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_role_role_type_role_type_id",
                table: "role");

            migrationBuilder.DropTable(
                name: "role_type");

            migrationBuilder.DropIndex(
                name: "ix_role_role_type_id",
                table: "role");

            migrationBuilder.DropColumn(
                name: "created",
                table: "role");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "role");

            migrationBuilder.DropColumn(
                name: "last_modified",
                table: "role");

            migrationBuilder.DropColumn(
                name: "last_modified_by",
                table: "role");

            migrationBuilder.DropColumn(
                name: "role_type_id",
                table: "role");

            migrationBuilder.AddColumn<bool>(
                name: "is_back_office",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_front_line",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
