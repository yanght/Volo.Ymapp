using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volo.Ymapp.Migrations
{
    public partial class updateentitydefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KH_LineDayImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Continent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImgPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineDayId = table.Column<long>(type: "bigint", nullable: false),
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    Sight = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDayImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineDays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Breakfast = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CityEnglish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DayHotel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    DayTraffic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Describe = table.Column<string>(type: "ntext", nullable: true),
                    Dinner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    Lunch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ScityDistance = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrafficName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineDaySelfs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "ntext", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intro = table.Column<string>(type: "ntext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineDayId = table.Column<long>(type: "bigint", nullable: false),
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDaySelfs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineDayShops",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intro = table.Column<string>(type: "ntext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineDayId = table.Column<long>(type: "bigint", nullable: false),
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDayShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineDayTraffics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineDayId = table.Column<long>(type: "bigint", nullable: false),
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    TrafficCo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrafficNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrafficTimeEnd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrafficTimeStart = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineDayTraffics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineIntros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Describe = table.Column<string>(type: "ntext", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    OrderNum = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineIntros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineRouteDates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdultPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AgentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChildPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateFinish = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOffline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeNum = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    JieShouRiQi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    OverseasJoinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlanNum = table.Column<int>(type: "int", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RetainCount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleRoom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TeamId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WebsiteTags = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineRouteDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_Lines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Continent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstLineImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImgCity = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImgCode = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImgContinent = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImgCountry = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumDay = table.Column<int>(type: "int", nullable: false),
                    NumNight = table.Column<int>(type: "int", nullable: false),
                    PlaceLeave = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlaceReturn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Sight = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TxtTransitCity = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Visa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_Lines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KH_LineTeams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AirCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Continent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateFinish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOffline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayNum = table.Column<int>(type: "int", nullable: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeptCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeptName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeNum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LineId = table.Column<long>(type: "bigint", nullable: false),
                    OverseasJoinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlaceLeave = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlaceReturn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlanNum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostersData = table.Column<string>(type: "ntext", nullable: true),
                    PostersImg = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SingleRoom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TeamId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WebsiteTags = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH_LineTeams", x => x.Id);
                });
        }
    }
}
