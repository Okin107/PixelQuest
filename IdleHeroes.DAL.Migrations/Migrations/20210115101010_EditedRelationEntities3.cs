using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class EditedRelationEntities3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStageNumber",
                table: "Profile");

            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "Profile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profile_StageId",
                table: "Profile",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Stage_StageId",
                table: "Profile",
                column: "StageId",
                principalTable: "Stage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Stage_StageId",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Profile_StageId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Profile");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentStageNumber",
                table: "Profile",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
