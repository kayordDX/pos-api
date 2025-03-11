using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoleIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE role ALTER role_id RESTART 10;
            """);

            migrationBuilder.Sql("""
                delete from user_role_outlet where role_id = 6;
            """);

            migrationBuilder.Sql("""
                update role set role_type_id = 2 where role_id = 2;
                update role set role_type_id = 3 where role_id = 3;
                update role set role_type_id = 4 where role_id = 4;
                update role set role_type_id = 3 where role_id = 5;
            """);

            migrationBuilder.Sql("""
                insert into role(name, description, outlet_id, role_type_id)
                select 
                    r.name,
                    r.description,
                    o.id outlet_id,
                    r.role_type_id
                from outlet o
                left join role r
                on r.role_id <= 5
            """);

            migrationBuilder.Sql("""
                UPDATE user_role_outlet
                SET role_id = rr.role_id
                FROM role r, role rr
                WHERE user_role_outlet.role_id = r.role_id
                AND r.name = rr.name
                AND rr.outlet_id = user_role_outlet.outlet_id;
            """);

            migrationBuilder.Sql("""
                update role_division
                set role_id = a.role_id
                from (
                    select rd.id, rd.role_id old_role_id, rr.role_id
                    from role_division rd
                    join division d
                    on rd.division_id = d.division_id
                    join role r
                    on r.role_id = rd.role_id
                    join role rr
                    on d.outlet_id = rr.outlet_id
                    and rr.name = r.name
                ) a  
                where role_division.role_id = a.old_role_id
                and role_division.id = a.id;
            """);

            migrationBuilder.Sql("""
                delete from role where outlet_id IS NULL;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
