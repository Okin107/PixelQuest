using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class DiscordNameFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscordName",
                table: "Profile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscordName",
                table: "Profile");
        }
    }
}
