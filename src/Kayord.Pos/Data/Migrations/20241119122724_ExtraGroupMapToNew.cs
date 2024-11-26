using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtraGroupMapToNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE "MenuItemExtraGroup" mg
                    SET "ExtraGroupId" = a."ExtraGroupId" 
                FROM (
                SELECT
                    mg."MenuItemId",
                    mg."ExtraGroupId" "OldExtraGroupId",
                    ee."ExtraGroupId"
                FROM "MenuItemExtraGroup" mg
                LEFT JOIN "MenuItem" mi
                    ON mi."MenuItemId" = mg."MenuItemId"
                LEFT JOIN "ExtraGroup" e
                    ON e."ExtraGroupId" = mg."ExtraGroupId"
                LEFT JOIN "MenuSection" s
                    ON mi."MenuSectionId" = s."MenuSectionId"  
                LEFT JOIN "Menu" mm
                    ON mm."Id" = s."MenuId"
                LEFT JOIN "ExtraGroup" ee
                    ON ee."Name" = e."Name"
                    AND ee."OutletId" = mm."OutletId"
                WHERE mg."ExtraGroupId" IS NOT NULL
                AND mm."OutletId" <> e."OutletId"
                AND mm."OutletId" <> 1
                ) a
                WHERE mg."MenuItemId" = a."MenuItemId"
                AND mg."ExtraGroupId" = a."OldExtraGroupId"
            """);

            migrationBuilder.Sql("""
                UPDATE "MenuItemOptionGroup" mg
                    SET "OptionGroupId" = a."OptionGroupId" 
                FROM (
                SELECT
                    mg."MenuItemId",
                    mg."OptionGroupId" "OldOptionGroupId",
                    ee."OptionGroupId"
                FROM "MenuItemOptionGroup" mg
                LEFT JOIN "MenuItem" mi
                    ON mi."MenuItemId" = mg."MenuItemId"
                LEFT JOIN "OptionGroup" e
                    ON e."OptionGroupId" = mg."OptionGroupId"
                LEFT JOIN "MenuSection" s
                    ON mi."MenuSectionId" = s."MenuSectionId"  
                LEFT JOIN "Menu" mm
                    ON mm."Id" = s."MenuId"
                LEFT JOIN "OptionGroup" ee
                    ON ee."Name" = e."Name"
                    AND ee."OutletId" = mm."OutletId"
                WHERE mg."OptionGroupId" IS NOT NULL
                AND mm."OutletId" <> e."OutletId"
                AND mm."OutletId" <> 1
                ) a
                WHERE mg."MenuItemId" = a."MenuItemId"
                AND mg."OptionGroupId" = a."OldOptionGroupId"
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
