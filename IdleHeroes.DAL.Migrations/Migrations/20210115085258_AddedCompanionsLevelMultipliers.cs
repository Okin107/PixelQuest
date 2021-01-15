using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedCompanionsLevelMultipliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "IncreaseMultiplier",
                table: "Companion",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LevelToMultiplyIncreases",
                table: "Companion",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncreaseMultiplier",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "LevelToMultiplyIncreases",
                table: "Companion");
        }
    }
}
