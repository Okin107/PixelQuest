using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedEnemyTablesModifiedRewardsFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodPerMinute",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "CompanionAscendTier",
                table: "OwnedCompanions");

            migrationBuilder.DropColumn(
                name: "CompanionCopies",
                table: "OwnedCompanions");

            migrationBuilder.DropColumn(
                name: "CompanionLevel",
                table: "OwnedCompanions");

            migrationBuilder.DropColumn(
                name: "AscendTier",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "XP",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "XPFirstLevel",
                table: "Companion");

            migrationBuilder.DropColumn(
                name: "XPIncreasePerLevel",
                table: "Companion");

            migrationBuilder.AddColumn<decimal>(
                name: "FoodAmount",
                table: "Stage",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FoodChancePerMinute",
                table: "Stage",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GemsAmount",
                table: "Stage",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RelicsAmount",
                table: "Stage",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Copies",
                table: "OwnedCompanions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "OwnedCompanions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RarirtyTier",
                table: "OwnedCompanions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RarityTier",
                table: "Companion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Enemy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Lore = table.Column<string>(nullable: true),
                    IconName = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    MaxLevel = table.Column<double>(nullable: false),
                    LevelToMultiplyIncreases = table.Column<double>(nullable: false),
                    IncreaseMultiplier = table.Column<double>(nullable: false),
                    BaseLevelCost = table.Column<decimal>(nullable: false),
                    LevelCostIncrease = table.Column<double>(nullable: false),
                    Element = table.Column<int>(nullable: false),
                    DamageType = table.Column<int>(nullable: false),
                    Class = table.Column<int>(nullable: false),
                    DPS = table.Column<decimal>(nullable: false),
                    DPSIncreasePerLevel = table.Column<double>(nullable: false),
                    Accuracy = table.Column<decimal>(nullable: false),
                    AccuracyIncreasePerLevel = table.Column<double>(nullable: false),
                    HP = table.Column<decimal>(nullable: false),
                    HPIncreasePerLevel = table.Column<double>(nullable: false),
                    Armor = table.Column<decimal>(nullable: false),
                    ArmorIncreasePerLevel = table.Column<double>(nullable: false),
                    Agility = table.Column<decimal>(nullable: false),
                    AgilityIncreasePerLevel = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageEnemies",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StageEnemies");

            migrationBuilder.DropTable(
                name: "Enemy");

            migrationBuilder.DropColumn(
                name: "FoodAmount",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "FoodChancePerMinute",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "GemsAmount",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "RelicsAmount",
                table: "Stage");

            migrationBuilder.DropColumn(
                name: "Copies",
                table: "OwnedCompanions");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "OwnedCompanions");

            migrationBuilder.DropColumn(
                name: "RarirtyTier",
                table: "OwnedCompanions");

            migrationBuilder.DropColumn(
                name: "RarityTier",
                table: "Companion");

            migrationBuilder.AddColumn<decimal>(
                name: "FoodPerMinute",
                table: "Stage",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CompanionAscendTier",
                table: "OwnedCompanions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanionCopies",
                table: "OwnedCompanions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanionLevel",
                table: "OwnedCompanions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AscendTier",
                table: "Companion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "XP",
                table: "Companion",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "XPFirstLevel",
                table: "Companion",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "XPIncreasePerLevel",
                table: "Companion",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
