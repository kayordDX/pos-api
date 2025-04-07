using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockAllocateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_user_assigned_user_user_id",
                table: "stock_allocate");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_user_from_user_user_id",
                table: "stock_allocate");

            migrationBuilder.DropIndex(
                name: "ix_stock_allocate_assigned_user_user_id",
                table: "stock_allocate");

            migrationBuilder.DropIndex(
                name: "ix_stock_allocate_from_user_user_id",
                table: "stock_allocate");

            migrationBuilder.DropColumn(
                name: "assigned_user_user_id",
                table: "stock_allocate");

            migrationBuilder.DropColumn(
                name: "from_user_user_id",
                table: "stock_allocate");

            migrationBuilder.AlterColumn<string>(
                name: "from_user_id",
                table: "stock_allocate",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "assigned_user_id",
                table: "stock_allocate",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "to_outlet_id",
                table: "stock_allocate",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_stock_allocate_assigned_user_id",
                table: "stock_allocate",
                column: "assigned_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_allocate_from_user_id",
                table: "stock_allocate",
                column: "from_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_user_assigned_user_id",
                table: "stock_allocate",
                column: "assigned_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_user_from_user_id",
                table: "stock_allocate",
                column: "from_user_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_user_assigned_user_id",
                table: "stock_allocate");

            migrationBuilder.DropForeignKey(
                name: "fk_stock_allocate_user_from_user_id",
                table: "stock_allocate");

            migrationBuilder.DropIndex(
                name: "ix_stock_allocate_assigned_user_id",
                table: "stock_allocate");

            migrationBuilder.DropIndex(
                name: "ix_stock_allocate_from_user_id",
                table: "stock_allocate");

            migrationBuilder.DropColumn(
                name: "to_outlet_id",
                table: "stock_allocate");

            migrationBuilder.AlterColumn<int>(
                name: "from_user_id",
                table: "stock_allocate",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "assigned_user_id",
                table: "stock_allocate",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "assigned_user_user_id",
                table: "stock_allocate",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "from_user_user_id",
                table: "stock_allocate",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_stock_allocate_assigned_user_user_id",
                table: "stock_allocate",
                column: "assigned_user_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_allocate_from_user_user_id",
                table: "stock_allocate",
                column: "from_user_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_user_assigned_user_user_id",
                table: "stock_allocate",
                column: "assigned_user_user_id",
                principalTable: "user",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_allocate_user_from_user_user_id",
                table: "stock_allocate",
                column: "from_user_user_id",
                principalTable: "user",
                principalColumn: "user_id");
        }
    }
}
