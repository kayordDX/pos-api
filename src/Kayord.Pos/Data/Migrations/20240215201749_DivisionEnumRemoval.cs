using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class DivisionEnumRemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Division",
                table: "MenuItem",
                newName: "DivisionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderCompleted",
                table: "OrderItem",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemStatusId",
                table: "OrderItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderReceived",
                table: "OrderItem",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    DivisionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DivisionName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.DivisionId);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemStatus",
                columns: table => new
                {
                    OrderItemStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemStatus", x => x.OrderItemStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RoleDivision",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    DivisionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDivision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleDivision_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Division",
                        principalColumn: "DivisionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_DivisionId",
                table: "MenuItem",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleDivision_DivisionId",
                table: "RoleDivision",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Division_DivisionId",
                table: "MenuItem",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Division_DivisionId",
                table: "MenuItem");

            migrationBuilder.DropTable(
                name: "OrderItemStatus");

            migrationBuilder.DropTable(
                name: "RoleDivision");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_DivisionId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "OrderCompleted",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderItemStatusId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "OrderReceived",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "MenuItem",
                newName: "Division");
        }
    }
}
