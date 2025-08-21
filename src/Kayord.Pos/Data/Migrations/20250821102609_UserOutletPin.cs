using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserOutletPin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_outlet_pin",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    outlet_id = table.Column<int>(type: "integer", nullable: false),
                    pin = table.Column<string>(type: "text", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    iv = table.Column<byte[]>(type: "bytea", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_outlet_pin", x => new { x.user_id, x.outlet_id });
                });

            migrationBuilder.Sql("""
                insert into feature(id, name) values(5, 'Counter Mode');
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_outlet_pin");

            migrationBuilder.Sql("""
                delete from feature where id = 5;
            """);
        }
    }
}
