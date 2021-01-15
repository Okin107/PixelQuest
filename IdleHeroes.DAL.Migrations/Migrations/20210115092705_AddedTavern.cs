using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedTavern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanionCopies",
                table: "OwnedCompanions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanionLevel",
                table: "OwnedCompanions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tavern",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    LastRefresh = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tavern", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TavernCompanion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanionID = table.Column<int>(nullable: false),
                    FoodCost = table.Column<decimal>(nullable: false),
                    TavernId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TavernCompanion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TavernCompanion_Tavern_TavernId",
                        column: x => x.TavernId,
                        principalTable: "Tavern",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TavernCompanion_TavernId",
                table: "TavernCompanion",
                column: "TavernId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TavernCompanion");

            migrationBuilder.DropTable(
                name: "Tavern");

            migrationBuilder.DropColumn(
                name: "CompanionCopies",
                table: "OwnedCompanions");

            migrationBuilder.DropColumn(
                name: "CompanionLevel",
                table: "OwnedCompanions");
        }
    }
}
