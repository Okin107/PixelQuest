using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class RemovedSpecificAscendMultipliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AscendMultiplier1",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "AscendMultiplier2",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "AscendMultiplier3",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "AscendMultiplier4",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "AscendMultiplier5",
                table: "Companion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AscendMultiplier1",
                table: "Companion",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AscendMultiplier2",
                table: "Companion",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AscendMultiplier3",
                table: "Companion",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AscendMultiplier4",
                table: "Companion",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AscendMultiplier5",
                table: "Companion",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
