using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdleHeroes.DAL.Migrations.Migrations
{
    public partial class TimeAddedToStage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToBeatSeconds",
                table: "Stage");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeToBeat",
                table: "Stage",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToBeat",
                table: "Stage");

            migrationBuilder.AddColumn<int>(
                name: "TimeToBeatSeconds",
                table: "Stage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
