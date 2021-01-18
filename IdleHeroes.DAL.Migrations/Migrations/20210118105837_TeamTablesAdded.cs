using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class TeamTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Profile",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroTeamPosition = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamCompanion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanionId = table.Column<int>(nullable: true),
                    TeamPosition = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCompanion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamCompanion_Companion_CompanionId",
                        column: x => x.CompanionId,
                        principalTable: "Companion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamCompanion_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profile_TeamId",
                table: "Profile",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCompanion_CompanionId",
                table: "TeamCompanion",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCompanion_TeamId",
                table: "TeamCompanion",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Team_TeamId",
                table: "Profile",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Team_TeamId",
                table: "Profile");

            migrationBuilder.DropTable(
                name: "TeamCompanion");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Profile_TeamId",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Profile");
        }
    }
}
