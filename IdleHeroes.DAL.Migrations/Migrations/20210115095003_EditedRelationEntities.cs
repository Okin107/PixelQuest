using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class EditedRelationEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanionID",
                table: "TavernCompanion");

            migrationBuilder.AddColumn<int>(
                name: "TavernCompanionId",
                table: "Companion",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CompanionID",
                table: "TavernCompanion",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
