using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AffectsGrossBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecreaseBalance",
                table: "CashUpUserItemType");

            migrationBuilder.RenameColumn(
                name: "IncreaseBalance",
                table: "CashUpUserItemType",
                newName: "AffectsGrossBalance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AffectsGrossBalance",
                table: "CashUpUserItemType",
                newName: "IncreaseBalance");

            migrationBuilder.AddColumn<bool>(
                name: "DecreaseBalance",
                table: "CashUpUserItemType",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
