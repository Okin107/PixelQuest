using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class StageStaticRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StaticCoins",
                table: "Stage",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StaticFood",
                table: "Stage",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StaticGems",
                table: "Stage",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StaticRelics",
                table: "Stage",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StaticXP",
                table: "Stage",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaticCoins",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "StaticFood",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "StaticGems",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "StaticRelics",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "StaticXP",
                table: "Stage");
        }
    }
}
