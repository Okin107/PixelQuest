using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class ProfileXPFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "XPBaseLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "XPIncreasePerLevel",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XPBaseLevel",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "XPIncreasePerLevel",
                table: "Profile");
        }
    }
}
