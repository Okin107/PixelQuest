using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class EditTavern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TavernCompanion_Tavern_TavernId",
                table: "TavernCompanion");

            migrationBuilder.DropIndex(
                name: "IX_TavernCompanion_TavernId",
                table: "TavernCompanion");

            migrationBuilder.DropColumn(
                name: "TavernId",
                table: "TavernCompanion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TavernId",
                table: "TavernCompanion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TavernCompanion_TavernId",
                table: "TavernCompanion",
                column: "TavernId");

            migrationBuilder.AddForeignKey(
                name: "FK_TavernCompanion_Tavern_TavernId",
                table: "TavernCompanion",
                column: "TavernId",
                principalTable: "Tavern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
