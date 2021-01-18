using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AscendTiersModified3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamCompanion_Companion_CompanionId",
                table: "TeamCompanion");

            migrationBuilder.DropIndex(
                name: "IX_TeamCompanion_CompanionId",
                table: "TeamCompanion");

            migrationBuilder.DropColumn(
                name: "CompanionId",
                table: "TeamCompanion");

            migrationBuilder.AddColumn<int>(
                name: "OwnedCompanionId",
                table: "TeamCompanion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamCompanion_OwnedCompanionId",
                table: "TeamCompanion",
                column: "OwnedCompanionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamCompanion_OwnedCompanions_OwnedCompanionId",
                table: "TeamCompanion",
                column: "OwnedCompanionId",
                principalTable: "OwnedCompanions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamCompanion_OwnedCompanions_OwnedCompanionId",
                table: "TeamCompanion");

            migrationBuilder.DropIndex(
                name: "IX_TeamCompanion_OwnedCompanionId",
                table: "TeamCompanion");

            migrationBuilder.DropColumn(
                name: "OwnedCompanionId",
                table: "TeamCompanion");

            migrationBuilder.AddColumn<int>(
                name: "CompanionId",
                table: "TeamCompanion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamCompanion_CompanionId",
                table: "TeamCompanion",
                column: "CompanionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamCompanion_Companion_CompanionId",
                table: "TeamCompanion",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
