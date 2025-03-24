using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class BulkStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu_item_bulk_stock",
                columns: table => new
                {
                    menu_item_id = table.Column<int>(type: "integer", nullable: false),
                    stock_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_item_bulk_stock", x => new { x.menu_item_id, x.stock_id });
                    table.ForeignKey(
                        name: "fk_menu_item_bulk_stock_menu_item_menu_item_id",
                        column: x => x.menu_item_id,
                        principalTable: "menu_item",
                        principalColumn: "menu_item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_menu_item_bulk_stock_stock_stock_id",
                        column: x => x.stock_id,
                        principalTable: "stock",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_menu_item_bulk_stock_stock_id",
                table: "menu_item_bulk_stock",
                column: "stock_id");

            migrationBuilder.Sql("""
            CREATE
            OR REPLACE FUNCTION public.get_menu_section_children (p_menu_id integer, p_menu_section_id integer) RETURNS TABLE (id integer) LANGUAGE plpgsql AS $function$
            BEGIN
                RETURN QUERY
                WITH RECURSIVE cte AS (
                    SELECT ms.menu_section_id 
                    FROM menu_section ms 
                    WHERE ms.menu_id = p_menu_id AND ms.menu_section_id = p_menu_section_id
                    UNION ALL
                    SELECT ms2.menu_section_id 
                    FROM menu_section ms2 
                    INNER JOIN cte ON ms2.parent_id = cte.menu_section_id
                )
                SELECT menu_section_id FROM cte;
            END;
            $function$
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_item_bulk_stock");
        }
    }
}
