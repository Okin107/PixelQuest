using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedTavernTiers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Tier",
                table: "Tavern",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TierBaseCost",
                table: "Tavern",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TierCostIncrease",
                table: "Tavern",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tier",
                table: "Tavern");

            migrationBuilder.DropColumn(
                name: "TierBaseCost",
                table: "Tavern");

            migrationBuilder.DropColumn(
                name: "TierCostIncrease",
                table: "Tavern");
        }
    }
}
