using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updatelineteamentity02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PlanNum",
                table: "KH_LineTeams",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "FreeNum",
                table: "KH_LineTeams",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "KH_LineDaySelfs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PlanNum",
                table: "KH_LineTeams",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "FreeNum",
                table: "KH_LineTeams",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "KH_LineDaySelfs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
