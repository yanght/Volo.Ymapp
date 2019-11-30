using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class addlineteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstLineImg",
                table: "KH_Lines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstLineImg",
                table: "KH_Lines");
        }
    }
}
