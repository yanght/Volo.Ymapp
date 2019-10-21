using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class addproduct01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductPrices_AppProducts_ProductId",
                table: "AppProductPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductStocks_AppProducts_ProductId",
                table: "AppProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_AppProductStocks_ProductId",
                table: "AppProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_AppProductPrices_ProductId",
                table: "AppProductPrices");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductStocks_ProductSpecId",
                table: "AppProductStocks",
                column: "ProductSpecId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductPrices_ProductSpecId",
                table: "AppProductPrices",
                column: "ProductSpecId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductPrices_AppProductSpecs_ProductSpecId",
                table: "AppProductPrices",
                column: "ProductSpecId",
                principalTable: "AppProductSpecs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductStocks_AppProductSpecs_ProductSpecId",
                table: "AppProductStocks",
                column: "ProductSpecId",
                principalTable: "AppProductSpecs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductPrices_AppProductSpecs_ProductSpecId",
                table: "AppProductPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductStocks_AppProductSpecs_ProductSpecId",
                table: "AppProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_AppProductStocks_ProductSpecId",
                table: "AppProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_AppProductPrices_ProductSpecId",
                table: "AppProductPrices");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductStocks_ProductId",
                table: "AppProductStocks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductPrices_ProductId",
                table: "AppProductPrices",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductPrices_AppProducts_ProductId",
                table: "AppProductPrices",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductStocks_AppProducts_ProductId",
                table: "AppProductStocks",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
