using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class SnakeExtras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""DROP FUNCTION "getMenuSectionParents";""");
            migrationBuilder.Sql("""DROP FUNCTION "getMenuSectionChildren";""");

            migrationBuilder.Sql("""
            create or replace function "get_menu_section_children" (
                "p_menu_id" int,
                "p_menu_section_id" int
            ) 
            returns table (
                "id" int
            )
            language plpgsql
            as $$
            begin
            return query
            with RECURSIVE cte as 
            (
                select * from "menu_section" where "menu_id"="p_menu_id" AND "menu_section_id"="p_menu_section_id"
                union all
                select e.* from "menu_section" e inner join cte on "e"."parent_id"=cte."menu_section_id"
            )
            select "menu_section_id" from cte;
            end; $$
            """);

            migrationBuilder.Sql("""
            create or replace function "get_menu_section_parents" ("p_menu_id" integer, "p_menu_section_id" integer) 
            returns table (
            "id" integer
            ) 
            language plpgsql AS $function$
            begin
                return query
                with recursive pl("menu_section_id", parent) as (
                    select "menu_section"."menu_section_id", coalesce("parent_id", "menu_section"."menu_section_id")
                    from "menu_section" WHERE "menu_id"="p_menu_id" AND "menu_section"."menu_section_id" = "p_menu_section_id"
                    union
                    select pl."menu_section_id", coalesce("menu_section"."parent_id", pl."menu_section_id")
                    from pl
                    join "menu_section" on pl.parent = "menu_section"."menu_section_id"
                )
                select pl.parent
                from pl;
            end; $function$
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""DROP FUNCTION "get_menu_section_parents";""");
            migrationBuilder.Sql("""DROP FUNCTION "get_menu_section_children";""");
            migrationBuilder.Sql(
                """
                    CREATE
                    OR REPLACE FUNCTION public."getMenuSectionParents" ("p_menuId" integer, "p_menuSectionId" integer) RETURNS TABLE ("Id" integer) LANGUAGE plpgsql AS $function$
                        begin
                            return query
                            with recursive pl("MenuSectionId", parent) as (
                                select "MenuSection"."MenuSectionId", coalesce("ParentId", "MenuSection"."MenuSectionId")
                                from "MenuSection" WHERE "MenuId"="p_menuId" AND "MenuSection"."MenuSectionId" = "p_menuSectionId"
                                union
                                select pl."MenuSectionId", coalesce("MenuSection"."ParentId", pl."MenuSectionId")
                                from pl
                                join "MenuSection" on pl.parent = "MenuSection"."MenuSectionId"
                            )
                            select pl.parent
                            from pl;
                        end; $function$
                """
                        );

            migrationBuilder.Sql(
                """
                    create or replace function "getMenuSectionChildren" (
                        "p_menuId" int,
                        "p_menuSectionId" int
                    ) 
                    returns table (
                        "Id" int
                    )
                    language plpgsql
                    as $$
                    begin
                    	return query
                      with RECURSIVE cte as 
                      (
                        select * from "MenuSection" where "MenuId"="p_menuId" AND "MenuSectionId"="p_menuSectionId"
                        union all
                        select e.* from "MenuSection" e inner join cte on "e"."ParentId"=cte."MenuSectionId"
                      )
                      select "MenuSectionId" from cte;
                    end; $$
                """
            );
        }
    }
}
