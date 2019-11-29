using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class addkh10086entitys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KH_LineDayImages",
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
                    LineDayId = table.Column<long>(nullable: false),
                    ImgCode = table.Column<string>(maxLength: 50, nullable: true),
                    Continent = table.Column<string>(maxLength: 50, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    Sight = table.Column<string>(maxLength: 200, nullable: true),
                    ImgPath = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDayImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineDays",
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
                    DayNumber = table.Column<int>(nullable: false),
                    DayHotel = table.Column<string>(maxLength: 50, nullable: true),
                    Breakfast = table.Column<string>(maxLength: 50, nullable: true),
                    Lunch = table.Column<string>(maxLength: 50, nullable: true),
                    Dinner = table.Column<string>(nullable: true),
                    DayTraffic = table.Column<string>(maxLength: 50, nullable: true),
                    CityEnglish = table.Column<string>(maxLength: 50, nullable: true),
                    ScityDistance = table.Column<string>(maxLength: 50, nullable: true),
                    TrafficName = table.Column<string>(maxLength: 200, nullable: true),
                    Describe = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineDaySelfs",
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
                    LineDayId = table.Column<long>(nullable: false),
                    CountryName = table.Column<string>(maxLength: 50, nullable: true),
                    CityName = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Intro = table.Column<string>(type: "ntext", nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    Price = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDaySelfs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineDayShops",
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
                    LineDayId = table.Column<long>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    CityName = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Intro = table.Column<string>(type: "ntext", nullable: true),
                    ActivityTime = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDayShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineDayTraffics",
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
                    LineDayId = table.Column<long>(nullable: false),
                    TrafficCo = table.Column<string>(maxLength: 50, nullable: true),
                    TrafficNo = table.Column<string>(maxLength: 50, nullable: true),
                    TrafficTimeEnd = table.Column<string>(maxLength: 50, nullable: true),
                    TrafficTimeStart = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDayTraffics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineIntros",
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
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    ChannelType = table.Column<string>(maxLength: 50, nullable: true),
                    Describe = table.Column<string>(type: "ntext", nullable: true),
                    OrderNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineIntros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineRouteDates",
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
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateFinish = table.Column<DateTime>(nullable: false),
                    AgentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JieShouRiQi = table.Column<DateTime>(nullable: true),
                    ChildPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdultPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deposit = table.Column<decimal>(nullable: false),
                    WebsiteTags = table.Column<string>(maxLength: 50, nullable: true),
                    PlanNum = table.Column<int>(nullable: false),
                    SingleRoom = table.Column<decimal>(nullable: false),
                    OverseasJoinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetainCount = table.Column<string>(nullable: true),
                    FreeNum = table.Column<int>(nullable: false),
                    DateOffline = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineRouteDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_Lines",
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
                    LineCode = table.Column<string>(maxLength: 50, nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    CustomTitle = table.Column<string>(maxLength: 200, nullable: true),
                    Continent = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 200, nullable: true),
                    TxtTransitCity = table.Column<string>(maxLength: 200, nullable: true),
                    Sight = table.Column<string>(maxLength: 50, nullable: true),
                    LineType = table.Column<string>(maxLength: 50, nullable: true),
                    NumNight = table.Column<int>(nullable: false),
                    NumDay = table.Column<int>(nullable: false),
                    Visa = table.Column<string>(maxLength: 200, nullable: true),
                    ImgCode = table.Column<string>(maxLength: 2000, nullable: true),
                    ImgContinent = table.Column<string>(maxLength: 2000, nullable: true),
                    ImgCountry = table.Column<string>(maxLength: 2000, nullable: true),
                    ImgCity = table.Column<string>(maxLength: 2000, nullable: true),
                    PlaceLeave = table.Column<string>(maxLength: 50, nullable: true),
                    PlaceReturn = table.Column<string>(maxLength: 50, nullable: true),
                    Function = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_Lines", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KH_LineDayImages");

            migrationBuilder.DropTable(
                name: "KH_LineDays");

            migrationBuilder.DropTable(
                name: "KH_LineDaySelfs");

            migrationBuilder.DropTable(
                name: "KH_LineDayShops");

            migrationBuilder.DropTable(
                name: "KH_LineDayTraffics");

            migrationBuilder.DropTable(
                name: "KH_LineIntros");

            migrationBuilder.DropTable(
                name: "KH_LineRouteDates");

            migrationBuilder.DropTable(
                name: "KH_Lines");
        }
    }
}
