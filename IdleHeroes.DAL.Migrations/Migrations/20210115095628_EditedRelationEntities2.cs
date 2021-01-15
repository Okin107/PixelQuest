using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class EditedRelationEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companion_TavernCompanion_TavernCompanionId",
                table: "Companion");

            migrationBuilder.DropIndex(
                name: "IX_Companion_TavernCompanionId",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "TavernCompanionId",
                table: "Companion");

            migrationBuilder.AddColumn<int>(
                name: "CompanionId",
                table: "TavernCompanion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TavernId",
                table: "TavernCompanion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TavernCompanion_CompanionId",
                table: "TavernCompanion",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_TavernCompanion_TavernId",
                table: "TavernCompanion",
                column: "TavernId");

            migrationBuilder.AddForeignKey(
                name: "FK_TavernCompanion_Companion_CompanionId",
                table: "TavernCompanion",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TavernCompanion_Tavern_TavernId",
                table: "TavernCompanion",
                column: "TavernId",
                principalTable: "Tavern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TavernCompanion_Companion_CompanionId",
                table: "TavernCompanion");

            migrationBuilder.DropForeignKey(
                name: "FK_TavernCompanion_Tavern_TavernId",
                table: "TavernCompanion");

            migrationBuilder.DropIndex(
                name: "IX_TavernCompanion_CompanionId",
                table: "TavernCompanion");

            migrationBuilder.DropIndex(
                name: "IX_TavernCompanion_TavernId",
                table: "TavernCompanion");

            migrationBuilder.DropColumn(
                name: "CompanionId",
                table: "TavernCompanion");

            migrationBuilder.DropColumn(
                name: "TavernId",
                table: "TavernCompanion");

            migrationBuilder.AddColumn<int>(
                name: "TavernCompanionId",
                table: "Companion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companion_TavernCompanionId",
                table: "Companion",
                column: "TavernCompanionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companion_TavernCompanion_TavernCompanionId",
                table: "Companion",
                column: "TavernCompanionId",
                principalTable: "TavernCompanion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
