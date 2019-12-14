using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updatelineaddstartdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateOffline",
                table: "KH_Lines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateStart",
                table: "KH_Lines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOffline",
                table: "KH_Lines");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "KH_Lines");
        }
    }
}
