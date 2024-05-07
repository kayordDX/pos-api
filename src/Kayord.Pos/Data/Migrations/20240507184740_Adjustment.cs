using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adjustment",
                columns: table => new
                {
                    AdjustmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdjustmentTypeId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    TableBookingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjustment", x => x.AdjustmentId);
                    table.ForeignKey(
                        name: "FK_Adjustment_TableBooking_TableBookingId",
                        column: x => x.TableBookingId,
                        principalTable: "TableBooking",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdjustmentType",
                columns: table => new
                {
                    AdjustmentTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentType", x => x.AdjustmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AdjustmentTypeOutlet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdjustmentTypeId = table.Column<int>(type: "integer", nullable: false),
                    OutletId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentTypeOutlet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdjustmentTypeOutlet_AdjustmentType_AdjustmentTypeId",
                        column: x => x.AdjustmentTypeId,
                        principalTable: "AdjustmentType",
                        principalColumn: "AdjustmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdjustmentTypeOutlet_Outlet_OutletId",
                        column: x => x.OutletId,
                        principalTable: "Outlet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adjustment_TableBookingId",
                table: "Adjustment",
                column: "TableBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustmentTypeOutlet_AdjustmentTypeId",
                table: "AdjustmentTypeOutlet",
                column: "AdjustmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjustmentTypeOutlet_OutletId",
                table: "AdjustmentTypeOutlet",
                column: "OutletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adjustment");

            migrationBuilder.DropTable(
                name: "AdjustmentTypeOutlet");

            migrationBuilder.DropTable(
                name: "AdjustmentType");
        }
    }
}
