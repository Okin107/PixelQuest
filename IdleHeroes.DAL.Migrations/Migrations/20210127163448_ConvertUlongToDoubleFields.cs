using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class ConvertUlongToDoubleFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FoodCost",
                table: "TavernCompanion",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "XPPerMinute",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "RelicsDropChancePerMinute",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "RelicsAmount",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Number",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "GemsDropChancePerMinute",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "GemsAmount",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "FoodChancePerMinute",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "FoodAmount",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "CoinsPerMinute",
                table: "Stage",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "XPBaseLevel",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "XP",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Relics",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Level",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "IdleXP",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "IdleRelics",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "IdleGems",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "IdleFood",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "IdleCoins",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "HP",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Gems",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Food",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Coins",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "BaseDPS",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "HP",
                table: "Enemy",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "DPS",
                table: "Enemy",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "BaseLevelCost",
                table: "Enemy",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Armor",
                table: "Enemy",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Agility",
                table: "Enemy",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Accuracy",
                table: "Enemy",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "HP",
                table: "Companion",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "DPS",
                table: "Companion",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Armor",
                table: "Companion",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Agility",
                table: "Companion",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Accuracy",
                table: "Companion",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FoodCost",
                table: "TavernCompanion",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "XPPerMinute",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "RelicsDropChancePerMinute",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "RelicsAmount",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Number",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "GemsDropChancePerMinute",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "GemsAmount",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "FoodChancePerMinute",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "FoodAmount",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "CoinsPerMinute",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "XPBaseLevel",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "XP",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Relics",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Level",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "IdleXP",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "IdleRelics",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "IdleGems",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "IdleFood",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "IdleCoins",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "HP",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Gems",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Food",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Coins",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "BaseDPS",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "HP",
                table: "Enemy",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "DPS",
                table: "Enemy",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "BaseLevelCost",
                table: "Enemy",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Armor",
                table: "Enemy",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Agility",
                table: "Enemy",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Accuracy",
                table: "Enemy",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "HP",
                table: "Companion",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "DPS",
                table: "Companion",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Armor",
                table: "Companion",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Agility",
                table: "Companion",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Accuracy",
                table: "Companion",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
