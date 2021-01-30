using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class ProfileLevelFields3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Accuracy",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AccuracyLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Agility",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AgilityLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ArmorLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DPSLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HPLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accuracy",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "AccuracyLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "Agility",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "AgilityLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "ArmorLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "DPSLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "HPLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "MaxLevel",
                table: "Profile");
        }
    }
}
