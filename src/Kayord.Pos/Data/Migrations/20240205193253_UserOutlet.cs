using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserOutlet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clock_Staff_StaffId",
                table: "Clock");

            migrationBuilder.DropForeignKey(
                name: "FK_TableBooking_Staff_StaffId",
                table: "TableBooking");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_TableBooking_StaffId",
                table: "TableBooking");

            migrationBuilder.DropIndex(
                name: "IX_Clock_StaffId",
                table: "Clock");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "TableBooking");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Clock");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TableBooking",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Clock",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserOutlet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    isCurrent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOutlet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableBooking_UserId",
                table: "TableBooking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clock_UserId",
                table: "Clock",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clock_User_UserId",
                table: "Clock",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooking_User_UserId",
                table: "TableBooking",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clock_User_UserId",
                table: "Clock");

            migrationBuilder.DropForeignKey(
                name: "FK_TableBooking_User_UserId",
                table: "TableBooking");

            migrationBuilder.DropTable(
                name: "UserOutlet");

            migrationBuilder.DropIndex(
                name: "IX_TableBooking_UserId",
                table: "TableBooking");

            migrationBuilder.DropIndex(
                name: "IX_Clock_UserId",
                table: "Clock");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TableBooking");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Clock");

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "TableBooking",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Clock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StaffType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Outlet_OutletId",
                        column: x => x.OutletId,
                        principalTable: "Outlet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableBooking_StaffId",
                table: "TableBooking",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Clock_StaffId",
                table: "Clock",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_OutletId",
                table: "Staff",
                column: "OutletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clock_Staff_StaffId",
                table: "Clock",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooking_Staff_StaffId",
                table: "TableBooking",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
