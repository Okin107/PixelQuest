using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class StageAddedAsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<decimal>(nullable: false),
                    XPPerMinute = table.Column<decimal>(nullable: false),
                    CoinsPerMinute = table.Column<decimal>(nullable: false),
                    FoodPerMinute = table.Column<decimal>(nullable: false),
                    GemsDropChancePerMinute = table.Column<decimal>(nullable: false),
                    RelicsDropChancePerMinute = table.Column<decimal>(nullable: false),
                    Difficulty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stage");
        }
    }
}
