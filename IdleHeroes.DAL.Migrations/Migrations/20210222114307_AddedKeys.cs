using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reachedMaxStage",
                table: "Profile");

            migrationBuilder.AddColumn<double>(
                name: "ChanceToGetKey",
                table: "Stage",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StaticKeys",
                table: "Stage",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Keys",
                table: "Profile",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChanceToGetKey",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "StaticKeys",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "Keys",
                table: "Profile");

            migrationBuilder.AddColumn<bool>(
                name: "reachedMaxStage",
                table: "Profile",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
