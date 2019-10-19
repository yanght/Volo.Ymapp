using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updatearticleentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MainContent",
                table: "AppArticles",
                type: "ntext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppArticles_CategoryId",
                table: "AppArticles",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppArticles_AppCategorys_CategoryId",
                table: "AppArticles",
                column: "CategoryId",
                principalTable: "AppCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppArticles_AppCategorys_CategoryId",
                table: "AppArticles");

            migrationBuilder.DropIndex(
                name: "IX_AppArticles_CategoryId",
                table: "AppArticles");

            migrationBuilder.AlterColumn<string>(
                name: "MainContent",
                table: "AppArticles",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext");
        }
    }
}
