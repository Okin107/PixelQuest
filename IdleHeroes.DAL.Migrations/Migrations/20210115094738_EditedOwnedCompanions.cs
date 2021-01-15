using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class EditedOwnedCompanions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanionID",
                table: "OwnedCompanions",
                newName: "CompanionId");

            migrationBuilder.AlterColumn<int>(
                name: "CompanionId",
                table: "OwnedCompanions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedCompanions_CompanionId",
                table: "OwnedCompanions",
                column: "CompanionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCompanions_Companion_CompanionId",
                table: "OwnedCompanions",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCompanions_Companion_CompanionId",
                table: "OwnedCompanions");

            migrationBuilder.DropIndex(
                name: "IX_OwnedCompanions_CompanionId",
                table: "OwnedCompanions");

            migrationBuilder.RenameColumn(
                name: "CompanionId",
                table: "OwnedCompanions",
                newName: "CompanionID");

            migrationBuilder.AlterColumn<int>(
                name: "CompanionID",
                table: "OwnedCompanions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
