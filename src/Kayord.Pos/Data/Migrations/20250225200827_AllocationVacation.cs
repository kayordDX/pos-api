using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllocationVacation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockAllocateItemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAllocateItemStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockAllocateStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAllocateStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockItemAuditType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItemAuditType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockAllocate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    StockAllocateStatusId = table.Column<int>(type: "integer", nullable: false),
                    FromDivisionId = table.Column<int>(type: "integer", nullable: false),
                    ToDivisionId = table.Column<int>(type: "integer", nullable: false),
                    AssignedUserId = table.Column<int>(type: "integer", nullable: false),
                    AssignedUserUserId = table.Column<string>(type: "text", nullable: true),
                    FromUserId = table.Column<int>(type: "integer", nullable: false),
                    FromUserUserId = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Completed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAllocate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAllocate_Division_FromDivisionId",
                        column: x => x.FromDivisionId,
                        principalTable: "Division",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAllocate_Division_ToDivisionId",
                        column: x => x.ToDivisionId,
                        principalTable: "Division",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAllocate_StockAllocateStatus_StockAllocateStatusId",
                        column: x => x.StockAllocateStatusId,
                        principalTable: "StockAllocateStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAllocate_User_AssignedUserUserId",
                        column: x => x.AssignedUserUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_StockAllocate_User_FromUserUserId",
                        column: x => x.FromUserUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "StockItemAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockItemId = table.Column<int>(type: "integer", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    FromActual = table.Column<decimal>(type: "numeric", nullable: false),
                    ToActual = table.Column<decimal>(type: "numeric", nullable: false),
                    StockItemAuditTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItemAudit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockItemAudit_StockItemAuditType_StockItemAuditTypeId",
                        column: x => x.StockItemAuditTypeId,
                        principalTable: "StockItemAuditType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockAllocateItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    DivisionId = table.Column<int>(type: "integer", nullable: false),
                    AllocateAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Actual = table.Column<decimal>(type: "numeric", nullable: false),
                    StockAllocateItemStatusId = table.Column<int>(type: "integer", nullable: false),
                    Completed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StockAllocateId = table.Column<int>(type: "integer", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAllocateItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAllocateItem_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Division",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAllocateItem_StockAllocateItemStatus_StockAllocateItem~",
                        column: x => x.StockAllocateItemStatusId,
                        principalTable: "StockAllocateItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAllocateItem_StockAllocate_StockAllocateId",
                        column: x => x.StockAllocateId,
                        principalTable: "StockAllocate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockAllocateItem_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocate_AssignedUserUserId",
                table: "StockAllocate",
                column: "AssignedUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocate_FromDivisionId",
                table: "StockAllocate",
                column: "FromDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocate_FromUserUserId",
                table: "StockAllocate",
                column: "FromUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocate_StockAllocateStatusId",
                table: "StockAllocate",
                column: "StockAllocateStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocate_ToDivisionId",
                table: "StockAllocate",
                column: "ToDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocateItem_DivisionId",
                table: "StockAllocateItem",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocateItem_StockAllocateId",
                table: "StockAllocateItem",
                column: "StockAllocateId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocateItem_StockAllocateItemStatusId",
                table: "StockAllocateItem",
                column: "StockAllocateItemStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAllocateItem_StockId",
                table: "StockAllocateItem",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItemAudit_StockItemAuditTypeId",
                table: "StockItemAudit",
                column: "StockItemAuditTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockAllocateItem");

            migrationBuilder.DropTable(
                name: "StockItemAudit");

            migrationBuilder.DropTable(
                name: "StockAllocateItemStatus");

            migrationBuilder.DropTable(
                name: "StockAllocate");

            migrationBuilder.DropTable(
                name: "StockItemAuditType");

            migrationBuilder.DropTable(
                name: "StockAllocateStatus");
        }
    }
}
