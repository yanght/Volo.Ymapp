using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updateentity02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LineCode",
                table: "KH_LineTeams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineCode",
                table: "KH_LineIntros",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "KH_LineDayTraffics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LineCode",
                table: "KH_LineDayTraffics",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "KH_LineDayShops",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LineCode",
                table: "KH_LineDayShops",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "KH_LineDaySelfs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LineCode",
                table: "KH_LineDaySelfs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineCode",
                table: "KH_LineDays",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "KH_LineDayImages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LineCode",
                table: "KH_LineDayImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineCode",
                table: "KH_LineTeams");

            migrationBuilder.DropColumn(
                name: "LineCode",
                table: "KH_LineIntros");

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "KH_LineDayTraffics");

            migrationBuilder.DropColumn(
                name: "LineCode",
                table: "KH_LineDayTraffics");

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "KH_LineDayShops");

            migrationBuilder.DropColumn(
                name: "LineCode",
                table: "KH_LineDayShops");

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "KH_LineDaySelfs");

            migrationBuilder.DropColumn(
                name: "LineCode",
                table: "KH_LineDaySelfs");

            migrationBuilder.DropColumn(
                name: "LineCode",
                table: "KH_LineDays");

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "KH_LineDayImages");

            migrationBuilder.DropColumn(
                name: "LineCode",
                table: "KH_LineDayImages");
        }
    }
}
