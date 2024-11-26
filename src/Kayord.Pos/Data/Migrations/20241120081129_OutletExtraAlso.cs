using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OutletExtraAlso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                with t as (
                    SELECT
                        o."Id", o."OutletId", o."ExtraGroupId" "OldExtraGroupId", ee."ExtraGroupId"
                    FROM "OutletExtraGroup" o
                    JOIN "ExtraGroup" e
                        ON e."ExtraGroupId" = o."ExtraGroupId"  
                    JOIN "ExtraGroup" ee
                        ON ee."Name" = e."Name"
                        AND o."OutletId" = ee."OutletId"
                    WHERE o."ExtraGroupId" <> "ee"."ExtraGroupId"
                    AND o."OutletId" = ee."OutletId"
                )
                UPDATE "OutletExtraGroup" AS o
                    SET "ExtraGroupId" = t."ExtraGroupId"
                FROM t
                WHERE o."Id" = t."Id"
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
