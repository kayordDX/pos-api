using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostgresFunctionMenuId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""DROP FUNCTION "getMenuSectionParents";""");
            migrationBuilder.Sql("""DROP FUNCTION "getMenuSectionChildren";""");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""DROP FUNCTION "getMenuSectionParents";""");
            migrationBuilder.Sql("""DROP FUNCTION "getMenuSectionChildren";""");
        }
    }
}
