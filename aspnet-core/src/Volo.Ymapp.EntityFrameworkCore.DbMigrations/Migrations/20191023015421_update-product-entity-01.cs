using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updateproductentity01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAreas_AppProducts_ProductId",
                table: "AppAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductPrices_AppProductSpecs_ProductSpecId",
                table: "AppProductPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductSpecs_AppProducts_ProductId",
                table: "AppProductSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_AppProductStocks_AppProductSpecs_ProductSpecId",
                table: "AppProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_AppProductStocks_ProductSpecId",
                table: "AppProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_AppProductSpecs_ProductId",
                table: "AppProductSpecs");

            migrationBuilder.DropIndex(
                name: "IX_AppProductPrices_ProductSpecId",
                table: "AppProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_AppAreas_ProductId",
                table: "AppAreas");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "AppAreas");

            migrationBuilder.CreateTable(
                name: "AppProductAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    AreaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProductAreas_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProductAreas_ProductId",
                table: "AppProductAreas",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProductAreas");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "AppAreas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProductStocks_ProductSpecId",
                table: "AppProductStocks",
                column: "ProductSpecId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductSpecs_ProductId",
                table: "AppProductSpecs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductPrices_ProductSpecId",
                table: "AppProductPrices",
                column: "ProductSpecId");

            migrationBuilder.CreateIndex(
                name: "IX_AppAreas_ProductId",
                table: "AppAreas",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppAreas_AppProducts_ProductId",
                table: "AppAreas",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductPrices_AppProductSpecs_ProductSpecId",
                table: "AppProductPrices",
                column: "ProductSpecId",
                principalTable: "AppProductSpecs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductSpecs_AppProducts_ProductId",
                table: "AppProductSpecs",
                column: "ProductId",
                principalTable: "AppProducts",
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
    }
}
