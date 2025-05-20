using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockCategoryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bulk_upload_config",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outlet_id = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bulk_upload_config", x => x.id);
                });

            migrationBuilder.Sql("""
                CREATE OR REPLACE VIEW
                    "public"."vw_stock_category" AS
                    SELECT
                        sc.id,
                        sc.name,
                        scp.id AS parent_id,
                        scp.name AS parent_name,
                        sc.outlet_id,
                        concat_ws(' - '::text, sc.name, scp.name) AS display_name
                    FROM
                        stock_category sc
                    LEFT JOIN stock_category scp ON scp.id = sc.parent_id
                    WHERE
                        sc.parent_id IS NOT NULL;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bulk_upload_config");
        }
    }
}
