using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updatecategory01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "KH_Lines");

            migrationBuilder.AddColumn<string>(
                name: "LineCategoryType",
                table: "KH_Lines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineCategoryType",
                table: "KH_Lines");

            migrationBuilder.AddColumn<long>(
                name: "Tag",
                table: "KH_Lines",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
