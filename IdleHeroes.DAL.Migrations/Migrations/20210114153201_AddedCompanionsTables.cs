using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedCompanionsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Lore = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    XP = table.Column<decimal>(nullable: false),
                    XPFirstLevel = table.Column<double>(nullable: false),
                    XPIncreasePerLevel = table.Column<double>(nullable: false),
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
                    table.PrimaryKey("PK_Companion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnedCompanions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileID = table.Column<int>(nullable: false),
                    CompanionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedCompanions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companion");

            migrationBuilder.DropTable(
                name: "OwnedCompanions");
        }
    }
}
