using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updateproduct01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductAreas_AppProducts_ProductId",
                table: "AppProductAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductPictures_AppProducts_ProductId",
                table: "AppProductPictures");

            migrationBuilder.DropIndex(
                name: "IX_AppProductPictures_ProductId",
                table: "AppProductPictures");

            migrationBuilder.DropIndex(
                name: "IX_AppProductAreas_ProductId",
                table: "AppProductAreas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppProductPictures_ProductId",
                table: "AppProductPictures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductAreas_ProductId",
                table: "AppProductAreas",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductAreas_AppProducts_ProductId",
                table: "AppProductAreas",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductPictures_AppProducts_ProductId",
                table: "AppProductPictures",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
