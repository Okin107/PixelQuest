using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class SpecificAscendMultipliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncreaseMultiplier",
                table: "Companion",
                newName: "AscendMultiplier5");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "AscendMultiplier5",
                table: "Companion",
                newName: "IncreaseMultiplier");
        }
    }
}
