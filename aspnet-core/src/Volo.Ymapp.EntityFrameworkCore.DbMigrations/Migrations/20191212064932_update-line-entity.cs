using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updatelineentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "KH_Lines",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Recommend",
                table: "KH_Lines",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Tag",
                table: "KH_Lines",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "KH_Lines");

            migrationBuilder.DropColumn(
                name: "Recommend",
                table: "KH_Lines");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "KH_Lines");
        }
    }
}
