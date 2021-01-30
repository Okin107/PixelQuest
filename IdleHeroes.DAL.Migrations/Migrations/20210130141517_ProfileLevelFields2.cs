using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class ProfileLevelFields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BoostCostIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BoostMaxLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoostCostIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "BoostMaxLevel",
                table: "Profile");
        }
    }
}
