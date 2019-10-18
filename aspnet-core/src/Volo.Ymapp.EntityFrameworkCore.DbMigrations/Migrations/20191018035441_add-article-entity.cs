using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class addarticleentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Author = table.Column<string>(maxLength: 20, nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Recommend = table.Column<bool>(nullable: false, defaultValue: false),
                    PictureUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Describe = table.Column<string>(maxLength: 500, nullable: true),
                    MainContent = table.Column<string>(type: "ntext", nullable: true),
                    Sort = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArticles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppArticles");
        }
    }
}
