using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSupplierPlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_SupplierPlatform_SupplierPlatformId",
                table: "Supplier");

            migrationBuilder.DropTable(
                name: "SupplierPlatform");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_SupplierPlatformId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "SupplierPlatformId",
                table: "Supplier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierPlatformId",
                table: "Supplier",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SupplierPlatform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPlatform", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_SupplierPlatformId",
                table: "Supplier",
                column: "SupplierPlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_SupplierPlatform_SupplierPlatformId",
                table: "Supplier",
                column: "SupplierPlatformId",
                principalTable: "SupplierPlatform",
                principalColumn: "Id");
        }
    }
}
