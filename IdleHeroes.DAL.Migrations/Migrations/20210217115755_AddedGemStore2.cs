using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class AddedGemStore2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCompanions_Companion_CompanionId",
                table: "OwnedCompanions");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCompanions_Profile_ProfileId",
                table: "OwnedCompanions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamCompanion_OwnedCompanions_OwnedCompanionId",
                table: "TeamCompanion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedCompanions",
                table: "OwnedCompanions");

            migrationBuilder.RenameTable(
                name: "OwnedCompanions",
                newName: "OwnedCompanion");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedCompanions_ProfileId",
                table: "OwnedCompanion",
                newName: "IX_OwnedCompanion_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedCompanions_CompanionId",
                table: "OwnedCompanion",
                newName: "IX_OwnedCompanion_CompanionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedCompanion",
                table: "OwnedCompanion",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountPercentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemEffect = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreItem_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreItem_StoreId",
                table: "StoreItem",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCompanion_Companion_CompanionId",
                table: "OwnedCompanion",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCompanion_Profile_ProfileId",
                table: "OwnedCompanion",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamCompanion_OwnedCompanion_OwnedCompanionId",
                table: "TeamCompanion",
                column: "OwnedCompanionId",
                principalTable: "OwnedCompanion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCompanion_Companion_CompanionId",
                table: "OwnedCompanion");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCompanion_Profile_ProfileId",
                table: "OwnedCompanion");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamCompanion_OwnedCompanion_OwnedCompanionId",
                table: "TeamCompanion");

            migrationBuilder.DropTable(
                name: "StoreItem");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedCompanion",
                table: "OwnedCompanion");

            migrationBuilder.RenameTable(
                name: "OwnedCompanion",
                newName: "OwnedCompanions");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedCompanion_ProfileId",
                table: "OwnedCompanions",
                newName: "IX_OwnedCompanions_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedCompanion_CompanionId",
                table: "OwnedCompanions",
                newName: "IX_OwnedCompanions_CompanionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedCompanions",
                table: "OwnedCompanions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCompanions_Companion_CompanionId",
                table: "OwnedCompanions",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCompanions_Profile_ProfileId",
                table: "OwnedCompanions",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamCompanion_OwnedCompanions_OwnedCompanionId",
                table: "TeamCompanion",
                column: "OwnedCompanionId",
                principalTable: "OwnedCompanions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
