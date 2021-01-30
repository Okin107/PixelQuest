using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class ProfileLevelFields4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSkillPoints",
                table: "Profile");

            migrationBuilder.AddColumn<double>(
                name: "SkillPointsSpent",
                table: "Profile",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillPointsSpent",
                table: "Profile");

            migrationBuilder.AddColumn<double>(
                name: "TotalSkillPoints",
                table: "Profile",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
