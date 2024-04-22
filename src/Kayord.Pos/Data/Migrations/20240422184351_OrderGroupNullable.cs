using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderGroupNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderGroup_OrderItem_OrderItemId",
                table: "OrderGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderGroup",
                table: "OrderGroup");

            migrationBuilder.DropIndex(
                name: "IX_OrderGroup_OrderItemId",
                table: "OrderGroup");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderGroup");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "OrderGroup");

            migrationBuilder.AddColumn<int>(
                name: "OrderGroupId",
                table: "OrderItem",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderGroupId",
                table: "OrderGroup",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderGroup",
                table: "OrderGroup",
                column: "OrderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderGroupId",
                table: "OrderItem",
                column: "OrderGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_OrderGroup_OrderGroupId",
                table: "OrderItem",
                column: "OrderGroupId",
                principalTable: "OrderGroup",
                principalColumn: "OrderGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_OrderGroup_OrderGroupId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderGroupId",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderGroup",
                table: "OrderGroup");

            migrationBuilder.DropColumn(
                name: "OrderGroupId",
                table: "OrderItem");

            migrationBuilder.AlterColumn<int>(
                name: "OrderGroupId",
                table: "OrderGroup",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "OrderGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderGroup",
                table: "OrderGroup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGroup_OrderItemId",
                table: "OrderGroup",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGroup_OrderItem_OrderItemId",
                table: "OrderGroup",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
