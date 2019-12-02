using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updatelineteamentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KH_LineTeams",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    LineId = table.Column<long>(nullable: false),
                    TeamId = table.Column<string>(maxLength: 50, nullable: true),
                    ProductCode = table.Column<string>(maxLength: 50, nullable: true),
                    ProductName = table.Column<string>(maxLength: 50, nullable: true),
                    Function = table.Column<string>(maxLength: 50, nullable: true),
                    Continent = table.Column<string>(maxLength: 200, nullable: true),
                    PlaceLeave = table.Column<string>(maxLength: 50, nullable: true),
                    PlaceReturn = table.Column<string>(maxLength: 50, nullable: true),
                    DateStart = table.Column<string>(nullable: true),
                    DateFinish = table.Column<string>(nullable: true),
                    DayNum = table.Column<int>(nullable: false),
                    AirCompany = table.Column<string>(nullable: true),
                    AirShortName = table.Column<string>(nullable: true),
                    CustomerPrice = table.Column<decimal>(nullable: false),
                    AgentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChildPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SingleRoom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OverseasJoinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlanNum = table.Column<decimal>(nullable: false),
                    FreeNum = table.Column<decimal>(nullable: false),
                    WebsiteTags = table.Column<string>(maxLength: 50, nullable: true),
                    DateOffline = table.Column<string>(nullable: true),
                    DeptCode = table.Column<string>(maxLength: 50, nullable: true),
                    DeptName = table.Column<string>(maxLength: 50, nullable: true),
                    PostersImg = table.Column<string>(maxLength: 200, nullable: true),
                    PostersData = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineTeams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KH_LineTeams");
        }
    }
}
