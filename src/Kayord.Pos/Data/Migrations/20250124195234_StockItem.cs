using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleDivision_Division_DivisionId",
                table: "RoleDivision");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Location_LocationId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Location_LocationId",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_SupplierPlatform_SupplierPlatformId",
                table: "Supplier");

            migrationBuilder.DropTable(
                name: "InventoryOrder");

            migrationBuilder.Sql("""
            DROP TABLE "InventoryStock" CASCADE;
            """);

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropColumn(
                name: "Actual",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "IsBulkRecipe",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Threshold",
                table: "Stock");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Supplier",
                newName: "StockLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_LocationId",
                table: "Supplier",
                newName: "IX_Supplier_StockLocationId");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Stock",
                newName: "OutletId");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierPlatformId",
                table: "Supplier",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "OutletId",
                table: "Supplier",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "RoleDivision",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DivisionTypeId",
                table: "Division",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OutletId",
                table: "Division",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StockLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    OutletId = table.Column<int>(type: "integer", nullable: false)
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


            migrationBuilder.Sql("""
            INSERT INTO "StockLocation" ("Id", "AddressId", "OutletId", "Name")
            SELECT "Id", "AddressId", "OutletId", "Name" FROM "Location";
            """);

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Stock_LocationId",
                table: "Stock");



            migrationBuilder.CreateTable(
                name: "StockItem",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    StockLocationId = table.Column<int>(type: "integer", nullable: false),
                    Threshold = table.Column<decimal>(type: "numeric", nullable: false),
                    Actual = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItem", x => new { x.StockId, x.StockLocationId });
                    table.ForeignKey(
                        name: "FK_StockItem_StockLocation_StockLocationId",
                        column: x => x.StockLocationId,
                        principalTable: "StockLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockItem_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockItem_StockLocationId",
                table: "StockItem",
                column: "StockLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLocation_AddressId",
                table: "StockLocation",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StockLocation_OutletId",
                table: "StockLocation",
                column: "OutletId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleDivision_Division_DivisionId",
                table: "RoleDivision",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_StockLocation_StockLocationId",
                table: "Supplier",
                column: "StockLocationId",
                principalTable: "StockLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_SupplierPlatform_SupplierPlatformId",
                table: "Supplier",
                column: "SupplierPlatformId",
                principalTable: "SupplierPlatform",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
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
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Outlet_OutletId",
                        column: x => x.OutletId,
                        principalTable: "Outlet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql("""
            INSERT INTO "Location" ("Id", "AddressId", "OutletId", "Name")
            SELECT "Id", "AddressId", "OutletId", "Name" FROM "StockLocation";
            """);

            migrationBuilder.DropForeignKey(
                name: "FK_RoleDivision_Division_DivisionId",
                table: "RoleDivision");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_StockLocation_StockLocationId",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_SupplierPlatform_SupplierPlatformId",
                table: "Supplier");

            migrationBuilder.DropTable(
                name: "StockItem");

            migrationBuilder.DropTable(
                name: "StockLocation");

            migrationBuilder.DropColumn(
                name: "OutletId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "DivisionTypeId",
                table: "Division");

            migrationBuilder.DropColumn(
                name: "OutletId",
                table: "Division");

            migrationBuilder.RenameColumn(
                name: "StockLocationId",
                table: "Supplier",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_StockLocationId",
                table: "Supplier",
                newName: "IX_Supplier_LocationId");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "Stock",
                newName: "LocationId");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierPlatformId",
                table: "Supplier",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Actual",
                table: "Stock",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsBulkRecipe",
                table: "Stock",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Threshold",
                table: "Stock",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "RoleDivision",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");


            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    UnitId = table.Column<int>(type: "integer", nullable: false),
                    Actual = table.Column<decimal>(type: "numeric", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Threshold = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventory_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    UnitId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    GRVCode = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SupplierDeliveredName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryOrder_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryOrder_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryOrder_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryOrder_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryOrder_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryStock",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "integer", nullable: false),
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryStock", x => new { x.InventoryId, x.StockId });
                    table.ForeignKey(
                        name: "FK_InventoryStock_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryStock_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_LocationId",
                table: "Stock",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_LocationId",
                table: "Inventory",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_UnitId",
                table: "Inventory",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryOrder_InventoryId",
                table: "InventoryOrder",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryOrder_LocationId",
                table: "InventoryOrder",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryOrder_SupplierId",
                table: "InventoryOrder",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryOrder_UnitId",
                table: "InventoryOrder",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryOrder_UserId",
                table: "InventoryOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStock_StockId",
                table: "InventoryStock",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AddressId",
                table: "Location",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_OutletId",
                table: "Location",
                column: "OutletId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleDivision_Division_DivisionId",
                table: "RoleDivision",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Location_LocationId",
                table: "Stock",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Location_LocationId",
                table: "Supplier",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_SupplierPlatform_SupplierPlatformId",
                table: "Supplier",
                column: "SupplierPlatformId",
                principalTable: "SupplierPlatform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
