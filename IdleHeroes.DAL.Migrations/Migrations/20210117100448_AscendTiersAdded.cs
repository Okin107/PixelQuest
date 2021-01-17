using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AscendTiersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AscendCopiesTierIncrease",
                table: "Companion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AscendTier",
                table: "Companion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseAscendCopiesNeeded",
                table: "Companion",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AscendCopiesTierIncrease",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "AscendTier",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "BaseAscendCopiesNeeded",
                table: "Companion");
        }
    }
}
