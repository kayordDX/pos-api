using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class Cashuptypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashupConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashupConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashupUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashupUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashupUserItemType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    isAuto = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false),
                    CashupConfigId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashupUserItemType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashupUserItemType_CashupConfig_CashupConfigId",
                        column: x => x.CashupConfigId,
                        principalTable: "CashupConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashupUserItemType_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashupUserItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserCashupId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    CashupItemTypeId = table.Column<int>(type: "integer", nullable: false),
                    CashupItemTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashupUserItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashupUserItem_CashupUserItemType_CashupItemTypesId",
                        column: x => x.CashupItemTypesId,
                        principalTable: "CashupUserItemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashupUserItem_CashupUser_UserCashupId",
                        column: x => x.UserCashupId,
                        principalTable: "CashupUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashupUserItem_CashupItemTypesId",
                table: "CashupUserItem",
                column: "CashupItemTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_CashupUserItem_UserCashupId",
                table: "CashupUserItem",
                column: "UserCashupId");

            migrationBuilder.CreateIndex(
                name: "IX_CashupUserItemType_CashupConfigId",
                table: "CashupUserItemType",
                column: "CashupConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_CashupUserItemType_PaymentTypeId",
                table: "CashupUserItemType",
                column: "PaymentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashupUserItem");

            migrationBuilder.DropTable(
                name: "CashupUserItemType");

            migrationBuilder.DropTable(
                name: "CashupUser");

            migrationBuilder.DropTable(
                name: "CashupConfig");
        }
    }
}
