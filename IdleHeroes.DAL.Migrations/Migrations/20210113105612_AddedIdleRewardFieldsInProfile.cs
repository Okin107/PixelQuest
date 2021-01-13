using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedIdleRewardFieldsInProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "IdleCoins",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdleFood",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdleGems",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdleRelics",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IdleXP",
                table: "Profile",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdleCoins",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "IdleFood",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "IdleGems",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "IdleRelics",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "IdleXP",
                table: "Profile");
        }
    }
}
