using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class TavernHireTrackingHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TavernPurchase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TavernCompanionId = table.Column<int>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    TavernId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TavernPurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TavernPurchase_TavernCompanion_TavernCompanionId",
                        column: x => x.TavernCompanionId,
                        principalTable: "TavernCompanion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TavernPurchase_Tavern_TavernId",
                        column: x => x.TavernId,
                        principalTable: "Tavern",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TavernPurchase_TavernCompanionId",
                table: "TavernPurchase",
                column: "TavernCompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_TavernPurchase_TavernId",
                table: "TavernPurchase",
                column: "TavernId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TavernPurchase");
        }
    }
}
