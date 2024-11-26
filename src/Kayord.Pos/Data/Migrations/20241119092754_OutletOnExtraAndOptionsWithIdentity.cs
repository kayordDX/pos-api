using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kayord.Pos.Data.Migrations
{
    /// <inheritdoc />
    public partial class OutletOnExtraAndOptionsWithIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OutletId",
                table: "OptionGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OutletId",
                table: "Option",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MenuSection",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutletId",
                table: "ExtraGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OutletId",
                table: "Extra",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("""
                ALTER TABLE "Extra" ALTER "ExtraId" RESTART 100;
            """);
            migrationBuilder.Sql("""
                ALTER TABLE "ExtraGroup" ALTER "ExtraGroupId" RESTART 20;
            """);
            migrationBuilder.Sql("""
                ALTER TABLE "Option" ALTER "OptionId" RESTART 200;
            """);
            migrationBuilder.Sql("""
                ALTER TABLE "OptionGroup" ALTER "OptionGroupId" RESTART 50;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutletId",
                table: "OptionGroup");

            migrationBuilder.DropColumn(
                name: "OutletId",
                table: "Option");

            migrationBuilder.DropColumn(
                name: "OutletId",
                table: "ExtraGroup");

            migrationBuilder.DropColumn(
                name: "OutletId",
                table: "Extra");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MenuSection",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
