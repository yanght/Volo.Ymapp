using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updatelineentity08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AgentPrice",
                table: "KH_Lines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChildPrice",
                table: "KH_Lines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustomerPrice",
                table: "KH_Lines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Deposit",
                table: "KH_Lines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverseasJoinPrice",
                table: "KH_Lines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SingleRoom",
                table: "KH_Lines",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentPrice",
                table: "KH_Lines");

            migrationBuilder.DropColumn(
                name: "ChildPrice",
                table: "KH_Lines");

            migrationBuilder.DropColumn(
                name: "CustomerPrice",
                table: "KH_Lines");

            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "KH_Lines");

            migrationBuilder.DropColumn(
                name: "OverseasJoinPrice",
                table: "KH_Lines");

            migrationBuilder.DropColumn(
                name: "SingleRoom",
                table: "KH_Lines");
        }
    }
}
