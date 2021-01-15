using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class EditedProfileOwnedCompanions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "OwnedCompanions",
                newName: "ProfileId");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "OwnedCompanions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedCompanions_ProfileId",
                table: "OwnedCompanions",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCompanions_Profile_ProfileId",
                table: "OwnedCompanions",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCompanions_Profile_ProfileId",
                table: "OwnedCompanions");

            migrationBuilder.DropIndex(
                name: "IX_OwnedCompanions_ProfileId",
                table: "OwnedCompanions");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "OwnedCompanions",
                newName: "ProfileID");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileID",
                table: "OwnedCompanions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
