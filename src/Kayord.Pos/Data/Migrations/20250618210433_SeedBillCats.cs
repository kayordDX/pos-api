using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedBillCats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO public.bill_category (name, outlet_id)
                SELECT DISTINCT
                 d.friendly_name AS name,
                 s.outlet_id
            FROM
             menu_item mi
            JOIN
             division d ON mi.division_id = d.division_id
            JOIN
                 section s ON mi.menu_section_id = s.id
            WHERE
                d.friendly_name IS NOT NULL
             AND NOT EXISTS (
                SELECT 1
                FROM bill_category bc
             WHERE bc.name = d.friendly_name
              AND bc.outlet_id = s.outlet_id
                );
  
            UPDATE menu_item
            SET bill_category_id = bc.id
            FROM division d, section s, bill_category bc
            WHERE menu_item.division_id = d.division_id
             AND menu_item.menu_section_id = s.id
            AND bc.name = d.friendly_name
            AND bc.outlet_id = s.outlet_id;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
