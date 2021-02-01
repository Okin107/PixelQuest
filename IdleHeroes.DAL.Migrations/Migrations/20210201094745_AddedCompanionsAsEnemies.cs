using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedCompanionsAsEnemies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StageEnemy_Enemy_EnemyId",
                table: "StageEnemy");

            migrationBuilder.DropTable(
                name: "Enemy");

            migrationBuilder.DropIndex(
                name: "IX_StageEnemy_EnemyId",
                table: "StageEnemy");

            migrationBuilder.DropColumn(
                name: "EnemyId",
                table: "StageEnemy");

            migrationBuilder.AddColumn<int>(
                name: "CompanionId",
                table: "StageEnemy",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconName",
                table: "StageEnemy",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Level",
                table: "StageEnemy",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StageEnemy",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RarirtyTier",
                table: "StageEnemy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StageEnemy_CompanionId",
                table: "StageEnemy",
                column: "CompanionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageEnemy_Companion_CompanionId",
                table: "StageEnemy",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StageEnemy_Companion_CompanionId",
                table: "StageEnemy");

            migrationBuilder.DropIndex(
                name: "IX_StageEnemy_CompanionId",
                table: "StageEnemy");

            migrationBuilder.DropColumn(
                name: "CompanionId",
                table: "StageEnemy");

            migrationBuilder.DropColumn(
                name: "IconName",
                table: "StageEnemy");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "StageEnemy");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StageEnemy");

            migrationBuilder.DropColumn(
                name: "RarirtyTier",
                table: "StageEnemy");

            migrationBuilder.AddColumn<int>(
                name: "EnemyId",
                table: "StageEnemy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Enemy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Accuracy = table.Column<double>(type: "float", nullable: false),
                    Agility = table.Column<double>(type: "float", nullable: false),
                    Armor = table.Column<double>(type: "float", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false),
                    DPS = table.Column<double>(type: "float", nullable: false),
                    DamageType = table.Column<int>(type: "int", nullable: false),
                    Element = table.Column<int>(type: "int", nullable: false),
                    HP = table.Column<double>(type: "float", nullable: false),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemy", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StageEnemy_EnemyId",
                table: "StageEnemy",
                column: "EnemyId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageEnemy_Enemy_EnemyId",
                table: "StageEnemy",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
