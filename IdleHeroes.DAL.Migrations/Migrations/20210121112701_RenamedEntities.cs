using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class RenamedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StageEnemies");

            migrationBuilder.CreateTable(
                name: "StageEnemy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnemyId = table.Column<int>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    StageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageEnemy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageEnemy_Enemy_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageEnemy_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StageEnemy_EnemyId",
                table: "StageEnemy",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_StageEnemy_StageId",
                table: "StageEnemy",
                column: "StageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StageEnemy");

            migrationBuilder.CreateTable(
                name: "StageEnemies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnemyId = table.Column<int>(type: "int", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageEnemies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageEnemies_Enemy_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageEnemies_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StageEnemies_EnemyId",
                table: "StageEnemies",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_StageEnemies_StageId",
                table: "StageEnemies",
                column: "StageId");
        }
    }
}
