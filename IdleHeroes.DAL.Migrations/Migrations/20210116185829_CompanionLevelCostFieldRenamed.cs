using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class CompanionLevelCostFieldRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextLevelCost",
                table: "Companion");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseLevelCost",
                table: "Companion",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseLevelCost",
                table: "Companion");

            migrationBuilder.AddColumn<double>(
                name: "NextLevelCost",
                table: "Companion",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
