using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedCompanionsAsStageRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanionId",
                table: "Stage",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "reachedMaxStage",
                table: "Profile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Stage_CompanionId",
                table: "Stage",
                column: "CompanionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stage_Companion_CompanionId",
                table: "Stage",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stage_Companion_CompanionId",
                table: "Stage");

            migrationBuilder.DropIndex(
                name: "IX_Stage_CompanionId",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "CompanionId",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "reachedMaxStage",
                table: "Profile");
        }
    }
}
