using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockLocationFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemStock_Stock_StockId",
                table: "MenuItemStock");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItem_StockLocation_StockLocationId",
                table: "StockItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrder_StockLocation_StockLocationId",
                table: "StockOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderItem_StockItem_StockItemStockId_StockItemStockLoc~",
                table: "StockOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_StockLocation_StockLocationId",
                table: "Supplier");

            migrationBuilder.DropTable(
                name: "StockLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOrderItem",
                table: "StockOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_StockOrderItem_StockItemStockId_StockItemStockLocationId",
                table: "StockOrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemStock",
                table: "MenuItemStock");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemStock_StockId",
                table: "MenuItemStock");

            migrationBuilder.DropColumn(
                name: "StockItemId",
                table: "StockOrderItem");

            migrationBuilder.DropColumn(
                name: "StockItemStockId",
                table: "StockOrderItem");

            migrationBuilder.RenameColumn(
                name: "StockLocationId",
                table: "Supplier",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_StockLocationId",
                table: "Supplier",
                newName: "IX_Supplier_DivisionId");

            migrationBuilder.RenameColumn(
                name: "StockItemStockLocationId",
                table: "StockOrderItem",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "StockLocationId",
                table: "StockOrder",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrder_StockLocationId",
                table: "StockOrder",
                newName: "IX_StockOrder_DivisionId");

            migrationBuilder.RenameColumn(
                name: "StockLocationId",
                table: "StockItem",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_StockItem_StockLocationId",
                table: "StockItem",
                newName: "IX_StockItem_DivisionId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "MenuItemStock",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "MenuItemStock",
                newName: "StockItemStockId");

            migrationBuilder.AddColumn<int>(
                name: "StockItemId",
                table: "MenuItemStock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockItemDivisionId",
                table: "MenuItemStock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOrderItem",
                table: "StockOrderItem",
                columns: new[] { "StockOrderId", "StockId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemStock",
                table: "MenuItemStock",
                columns: new[] { "MenuItemId", "StockItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderItem_StockId",
                table: "StockOrderItem",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemStock_StockItemStockId_StockItemDivisionId",
                table: "MenuItemStock",
                columns: new[] { "StockItemStockId", "StockItemDivisionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemStock_StockItem_StockItemStockId_StockItemDivisionId",
                table: "MenuItemStock",
                columns: new[] { "StockItemStockId", "StockItemDivisionId" },
                principalTable: "StockItem",
                principalColumns: new[] { "StockId", "DivisionId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItem_Division_DivisionId",
                table: "StockItem",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrder_Division_DivisionId",
                table: "StockOrder",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderItem_Stock_StockId",
                table: "StockOrderItem",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Division_DivisionId",
                table: "Supplier",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemStock_StockItem_StockItemStockId_StockItemDivisionId",
                table: "MenuItemStock");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItem_Division_DivisionId",
                table: "StockItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrder_Division_DivisionId",
                table: "StockOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderItem_Stock_StockId",
                table: "StockOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Division_DivisionId",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOrderItem",
                table: "StockOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_StockOrderItem_StockId",
                table: "StockOrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemStock",
                table: "MenuItemStock");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemStock_StockItemStockId_StockItemDivisionId",
                table: "MenuItemStock");

            migrationBuilder.DropColumn(
                name: "StockItemId",
                table: "MenuItemStock");

            migrationBuilder.DropColumn(
                name: "StockItemDivisionId",
                table: "MenuItemStock");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "Supplier",
                newName: "StockLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_DivisionId",
                table: "Supplier",
                newName: "IX_Supplier_StockLocationId");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "StockOrderItem",
                newName: "StockItemStockLocationId");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "StockOrder",
                newName: "StockLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_StockOrder_DivisionId",
                table: "StockOrder",
                newName: "IX_StockOrder_StockLocationId");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "StockItem",
                newName: "StockLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_StockItem_DivisionId",
                table: "StockItem",
                newName: "IX_StockItem_StockLocationId");

            migrationBuilder.RenameColumn(
                name: "StockItemStockId",
                table: "MenuItemStock",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "MenuItemStock",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "StockItemId",
                table: "StockOrderItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockItemStockId",
                table: "StockOrderItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOrderItem",
                table: "StockOrderItem",
                columns: new[] { "StockOrderId", "StockItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemStock",
                table: "MenuItemStock",
                columns: new[] { "MenuItemId", "StockId" });

            migrationBuilder.CreateTable(
                name: "StockLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    OutletId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockLocation_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockLocation_Outlet_OutletId",
                        column: x => x.OutletId,
                        principalTable: "Outlet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderItem_StockItemStockId_StockItemStockLocationId",
                table: "StockOrderItem",
                columns: new[] { "StockItemStockId", "StockItemStockLocationId" });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemStock_StockId",
                table: "MenuItemStock",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLocation_AddressId",
                table: "StockLocation",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLocation_OutletId",
                table: "StockLocation",
                column: "OutletId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemStock_Stock_StockId",
                table: "MenuItemStock",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItem_StockLocation_StockLocationId",
                table: "StockItem",
                column: "StockLocationId",
                principalTable: "StockLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrder_StockLocation_StockLocationId",
                table: "StockOrder",
                column: "StockLocationId",
                principalTable: "StockLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderItem_StockItem_StockItemStockId_StockItemStockLoc~",
                table: "StockOrderItem",
                columns: new[] { "StockItemStockId", "StockItemStockLocationId" },
                principalTable: "StockItem",
                principalColumns: new[] { "StockId", "StockLocationId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_StockLocation_StockLocationId",
                table: "Supplier",
                column: "StockLocationId",
                principalTable: "StockLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
