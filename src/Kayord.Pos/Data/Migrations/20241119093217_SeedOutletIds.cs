using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedOutletIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE "ExtraGroup" SET "OutletId" = 1;
                UPDATE "Extra" SET "OutletId" = 1;

                INSERT INTO "ExtraGroup" ("Name", "OutletId")
                SELECT 
                    eg."Name",
                o."Id" "OutletId"
                FROM "ExtraGroup" eg
                LEFT JOIN "Outlet" o
                ON o."Id" <> eg."OutletId";

                INSERT INTO "Extra" ("Name", "PositionId", "Price", "ExtraGroupId", "OutletId")
                SELECT
                    e."Name",
                e."PositionId",
                e."Price",
                ee."ExtraGroupId",
                o."Id"
                FROM "Extra" e
                LEFT JOIN "Outlet" o
                    ON o."Id" <> e."OutletId"
                LEFT JOIN "ExtraGroup" eg
                    ON eg."ExtraGroupId" = e."ExtraGroupId"
                LEFT JOIN "ExtraGroup" ee
                    ON eg."Name" = ee."Name"
                AND ee."OutletId" = o."Id";
            """);

            migrationBuilder.Sql("""
                UPDATE "OptionGroup" SET "OutletId" = 1;
                UPDATE "Option" SET "OutletId" = 1;

                INSERT INTO "OptionGroup" ("Name", "MinSelections", "MaxSelections", "OutletId")
                SELECT 
                    eg."Name",
                eg."MinSelections",
                eg."MaxSelections",
                o."Id" "OutletId"
                FROM "OptionGroup" eg
                LEFT JOIN "Outlet" o
                ON o."Id" <> eg."OutletId";

                INSERT INTO "Option" ("Name", "PositionId", "Price", "OptionGroupId", "OutletId")
                SELECT
                    e."Name",
                e."PositionId",
                e."Price",
                ee."OptionGroupId",
                o."Id"
                FROM "Option" e
                LEFT JOIN "Outlet" o
                    ON o."Id" <> e."OutletId"
                LEFT JOIN "OptionGroup" eg
                    ON eg."OptionGroupId" = e."OptionGroupId"
                LEFT JOIN "OptionGroup" ee
                    ON eg."Name" = ee."Name"
                AND ee."OutletId" = o."Id";
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
