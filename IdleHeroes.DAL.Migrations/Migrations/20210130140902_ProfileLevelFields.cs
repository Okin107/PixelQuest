using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class ProfileLevelFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseDPS",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "AccuracyIncreasePerLevel",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "AgilityIncreasePerLevel",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "ArmorIncreasePerLevel",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "BaseLevelCost",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "DPSIncreasePerLevel",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "HPIncreasePerLevel",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "IncreaseMultiplier",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "LevelCostIncrease",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "LevelToMultiplyIncreases",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "MaxLevel",
                table: "Enemy");

            migrationBuilder.AddColumn<double>(
                name: "AccuracyBoostLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AccuracyBoostLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AgilityBoostLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AgilityBoostLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Armor",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ArmorBoostLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ArmorBoostLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DPS",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DPSBoostLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DPSBoostLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HPBoostLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HPBoostLevelIncrease",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SkillPointsAvailable",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalSkillPoints",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccuracyBoostLevel",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "AccuracyBoostLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "AgilityBoostLevel",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "AgilityBoostLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "Armor",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "ArmorBoostLevel",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "ArmorBoostLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "DPS",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "DPSBoostLevel",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "DPSBoostLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "HPBoostLevel",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "HPBoostLevelIncrease",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "SkillPointsAvailable",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "TotalSkillPoints",
                table: "Profile");

            migrationBuilder.AddColumn<double>(
                name: "BaseDPS",
                table: "Profile",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AccuracyIncreasePerLevel",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AgilityIncreasePerLevel",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ArmorIncreasePerLevel",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BaseLevelCost",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DPSIncreasePerLevel",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HPIncreasePerLevel",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "IncreaseMultiplier",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LevelCostIncrease",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LevelToMultiplyIncreases",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxLevel",
                table: "Enemy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
