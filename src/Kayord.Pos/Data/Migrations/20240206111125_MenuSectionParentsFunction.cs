using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class MenuSectionParentsFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // getMenuSectionParents
            migrationBuilder.Sql(
                """
                    create or replace function "getMenuSectionParents" (
                        "p_menuSectionId" int
                    ) 
                    returns table (
                        "Id" int
                    )
                    language plpgsql
                    as $$
                    begin
                        return query
                        with recursive pl("MenuSectionId", parent) as (
                            select "MenuSection"."MenuSectionId", coalesce("ParentId", "MenuSection"."MenuSectionId")
                            from "MenuSection" WHERE "MenuSection"."MenuSectionId" = "p_menuSectionId"
                            union
                            select pl."MenuSectionId", coalesce("MenuSection"."ParentId", pl."MenuSectionId")
                            from pl
                            join "MenuSection" on pl.parent = "MenuSection"."MenuSectionId"
                        )
                        select pl.parent
                        from pl;
                    end; $$
                """
            );

            migrationBuilder.Sql(
                """
                    create or replace function "getMenuSectionChildren" (
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
                        select * from "MenuSection" where "MenuSectionId"="p_menuSectionId"
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
