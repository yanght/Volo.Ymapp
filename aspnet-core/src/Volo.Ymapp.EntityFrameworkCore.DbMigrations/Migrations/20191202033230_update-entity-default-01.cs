using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updateentitydefault01 : Migration
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
                    ImgCode = table.Column<string>(nullable: true),
                    Continent = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Sight = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true)
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
                    DayHotel = table.Column<string>(nullable: true),
                    Breakfast = table.Column<string>(nullable: true),
                    Lunch = table.Column<string>(nullable: true),
                    Dinner = table.Column<string>(nullable: true),
                    DayTraffic = table.Column<string>(nullable: true),
                    CityEnglish = table.Column<string>(nullable: true),
                    ScityDistance = table.Column<string>(nullable: true),
                    TrafficName = table.Column<string>(nullable: true),
                    Describe = table.Column<string>(nullable: true)
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
                    CountryName = table.Column<string>(nullable: true),
                    CityName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Intro = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true)
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
                    CityName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Intro = table.Column<string>(nullable: true),
                    ActivityTime = table.Column<string>(nullable: true)
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
                    TrafficCo = table.Column<string>(nullable: true),
                    TrafficNo = table.Column<string>(nullable: true),
                    TrafficTimeEnd = table.Column<string>(nullable: true),
                    TrafficTimeStart = table.Column<string>(nullable: true)
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
                    Title = table.Column<string>(nullable: true),
                    ChannelType = table.Column<string>(nullable: true),
                    Describe = table.Column<string>(nullable: true),
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
                    TeamId = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateFinish = table.Column<DateTime>(nullable: false),
                    AgentPrice = table.Column<decimal>(nullable: false),
                    JieShouRiQi = table.Column<DateTime>(nullable: true),
                    ChildPrice = table.Column<decimal>(nullable: false),
                    AdultPrice = table.Column<decimal>(nullable: false),
                    Deposit = table.Column<decimal>(nullable: false),
                    WebsiteTags = table.Column<string>(nullable: true),
                    PlanNum = table.Column<int>(nullable: false),
                    SingleRoom = table.Column<decimal>(nullable: false),
                    OverseasJoinPrice = table.Column<decimal>(nullable: false),
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
                    LineCode = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    CustomTitle = table.Column<string>(nullable: true),
                    Continent = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    TxtTransitCity = table.Column<string>(nullable: true),
                    Sight = table.Column<string>(nullable: true),
                    LineType = table.Column<string>(nullable: true),
                    NumNight = table.Column<int>(nullable: false),
                    NumDay = table.Column<int>(nullable: false),
                    Visa = table.Column<string>(nullable: true),
                    ImgCode = table.Column<string>(nullable: true),
                    ImgContinent = table.Column<string>(nullable: true),
                    ImgCountry = table.Column<string>(nullable: true),
                    ImgCity = table.Column<string>(nullable: true),
                    PlaceLeave = table.Column<string>(nullable: true),
                    PlaceReturn = table.Column<string>(nullable: true),
                    Function = table.Column<string>(nullable: true),
                    FirstLineImg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_Lines", x => x.Id);
                });

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
                    TeamId = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Function = table.Column<string>(nullable: true),
                    Continent = table.Column<string>(nullable: true),
                    PlaceLeave = table.Column<string>(nullable: true),
                    PlaceReturn = table.Column<string>(nullable: true),
                    DateStart = table.Column<string>(nullable: true),
                    DateFinish = table.Column<string>(nullable: true),
                    DayNum = table.Column<int>(nullable: false),
                    AirCompany = table.Column<string>(nullable: true),
                    AirShortName = table.Column<string>(nullable: true),
                    CustomerPrice = table.Column<decimal>(nullable: false),
                    AgentPrice = table.Column<decimal>(nullable: false),
                    ChildPrice = table.Column<decimal>(nullable: false),
                    SingleRoom = table.Column<decimal>(nullable: false),
                    OverseasJoinPrice = table.Column<decimal>(nullable: false),
                    Deposit = table.Column<decimal>(nullable: false),
                    PlanNum = table.Column<decimal>(nullable: false),
                    FreeNum = table.Column<decimal>(nullable: false),
                    WebsiteTags = table.Column<string>(nullable: true),
                    DateOffline = table.Column<string>(nullable: true),
                    DeptCode = table.Column<string>(nullable: true),
                    DeptName = table.Column<string>(nullable: true),
                    PostersImg = table.Column<string>(nullable: true),
                    PostersData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineTeams", x => x.Id);
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

            migrationBuilder.DropTable(
                name: "KH_LineTeams");
        }
    }
}
