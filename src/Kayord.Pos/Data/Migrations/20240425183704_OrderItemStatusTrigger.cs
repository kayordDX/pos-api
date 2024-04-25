using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemStatusTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                     CREATE
                        OR REPLACE FUNCTION public.order_item_status_trigger_function () RETURNS trigger LANGUAGE plpgsql AS $function$
                        BEGIN
                            IF NEW."OrderItemStatusId" <> OLD."OrderItemStatusId" OR TG_OP = 'INSERT' THEN
                                INSERT INTO "OrderItemStatusLog" ("OrderItemId", "OrderItemStatusId","StatusDate")
                                VALUES (NEW."OrderItemId", NEW."OrderItemStatusId",CURRENT_TIMESTAMP);
                            END IF;
                            RETURN NEW;
                        END;
                        $function$
                     """);
            migrationBuilder.Sql("""
                     CREATE TRIGGER order_item_status_trigger
                        AFTER INSERT OR UPDATE ON "OrderItem"
                        FOR EACH ROW
                        EXECUTE FUNCTION order_item_status_trigger_function();
                     """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
        DROP TRIGGER order_item_status_trigger ON "OrderItem"
        """);
            migrationBuilder.Sql("DROP FUNCTION public.order_item_status_trigger_function");

        }
    }
}
