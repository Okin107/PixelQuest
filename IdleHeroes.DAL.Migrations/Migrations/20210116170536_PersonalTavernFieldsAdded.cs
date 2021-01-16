using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class PersonalTavernFieldsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscordID",
                table: "Profile",
                newName: "DiscordId");

            migrationBuilder.AddColumn<int>(
                name: "TavernId",
                table: "Profile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profile_TavernId",
                table: "Profile",
                column: "TavernId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Tavern_TavernId",
                table: "Profile",
                column: "TavernId",
                principalTable: "Tavern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Tavern_TavernId",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Profile_TavernId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "TavernId",
                table: "Profile");

            migrationBuilder.RenameColumn(
                name: "DiscordId",
                table: "Profile",
                newName: "DiscordID");
        }
    }
}
