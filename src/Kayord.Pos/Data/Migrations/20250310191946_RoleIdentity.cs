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
                update user_role_outlet
                SET role_id = rr.role_id
                FROM user_role_outlet uro
                join role r
                on uro.role_id = r.role_id
                join role rr
                on r.name = rr.name
                and rr.outlet_id = uro.outlet_id
            """);

            migrationBuilder.Sql("""
                delete from role where outlet_id IS NULL
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
