using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class StageAndProfileModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Relics",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Level",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gems",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Coins",
                table: "Profile",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseDPS",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentStageNumber",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Food",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastRewardsCollected",
                table: "Profile",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MaximumIdleRewardHours",
                table: "Profile",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "XP",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseDPS",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "CurrentStageNumber",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "Food",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "LastRewardsCollected",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "MaximumIdleRewardHours",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "XP",
                table: "Profile");

            migrationBuilder.AlterColumn<double>(
                name: "Relics",
                table: "Profile",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Profile",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Gems",
                table: "Profile",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Coins",
                table: "Profile",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
