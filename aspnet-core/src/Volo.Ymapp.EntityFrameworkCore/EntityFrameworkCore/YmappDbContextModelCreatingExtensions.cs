using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users;
using Volo.Ymapp.Areas;
using Volo.Ymapp.Articles;
using Volo.Ymapp.Books;
using Volo.Ymapp.Categorys;
using Volo.Ymapp.CommonEnum;
using Volo.Ymapp.kh10086;
using Volo.Ymapp.Products;

namespace Volo.Ymapp.EntityFrameworkCore
{
    public static class YmappDbContextModelCreatingExtensions
    {
        public static void ConfigureYmapp(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(YmappConsts.DbTablePrefix + "YourEntities", YmappConsts.DbSchema);

            //    //...
            //});


            #region Common Model Config

            builder.Entity<Book>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "Books", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
            builder.Entity<Category>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "Categorys", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(20);
                b.Property(x => x.ParentId).IsRequired();
                b.Property(x => x.Sort).IsRequired().HasDefaultValue(0);
                b.Property(x => x.Type).IsRequired().HasDefaultValue(CategoryType.Undefined);
            });

            builder.Entity<Article>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "Articles", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Title).IsRequired().HasMaxLength(200);
                b.Property(x => x.CategoryId).IsRequired();
                b.Property(x => x.Sort).IsRequired().HasDefaultValue(0);
                b.Property(x => x.Author).HasMaxLength(20);
                b.Property(x => x.Describe).HasMaxLength(500);
                b.Property(x => x.MainContent).HasColumnType("ntext").IsRequired();
                b.Property(x => x.PictureUrl).HasMaxLength(200);
                b.Property(x => x.Recommend).IsRequired().HasDefaultValue(false);
                b.Property(x => x.Source).HasMaxLength(100);
            });

            builder.Entity<Area>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "Areas", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(20);
                b.Property(x => x.Level).IsRequired().HasDefaultValue(0);
                b.Property(x => x.ParentId).IsRequired().HasDefaultValue(Guid.Empty);
            });

            builder.Entity<Product>(b =>
        {
            b.ToTable(YmappConsts.DbTablePrefix + "Products", YmappConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.State).IsRequired().HasDefaultValue(ProductState.Normal);
            b.Property(x => x.CategoryId).IsRequired();
            b.Property(x => x.Code).HasMaxLength(20);
            b.Property(x => x.Description).HasColumnType("ntext");
        });

            builder.Entity<ProductArea>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "ProductAreas", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ProductId).IsRequired();
                b.Property(x => x.AreaId).IsRequired();
            });

            builder.Entity<ProductPicture>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "ProductPictures", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.PictureUrl).IsRequired().HasMaxLength(200);
                b.Property(x => x.ProductId).IsRequired();
            });

            builder.Entity<ProductSpec>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "ProductSpecs", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(20);
                b.Property(x => x.ProductId).IsRequired();
                b.Property(x => x.AreaId).IsRequired();

            });

            builder.Entity<ProductStock>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "ProductStocks", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ProductSpecId).IsRequired();
                b.Property(x => x.ProductId).IsRequired();
                b.Property(x => x.AreaId).IsRequired();
                b.Property(x => x.Stock).IsRequired().HasDefaultValue(0);
            });

            builder.Entity<ProductPrice>(b =>
            {
                b.ToTable(YmappConsts.DbTablePrefix + "ProductPrices", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ProductSpecId).IsRequired();
                b.Property(x => x.ProductId).IsRequired();
                b.Property(x => x.AreaId).IsRequired();
                b.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired().HasDefaultValue(0);
                b.Property(x => x.OrignPrice).HasColumnType("decimal(18,2)").IsRequired().HasDefaultValue(0);
            });

            #endregion

            #region
            builder.Entity<Line>(b =>
            {
                b.ToTable(YmappConsts.Kh10086DbTablePrefix + "Lines", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Continent).HasMaxLength(100);
                b.Property(x => x.Country).HasMaxLength(200);
                b.Property(x => x.CustomTitle).HasMaxLength(200);
                b.Property(x => x.Function).HasMaxLength(50);
                b.Property(x => x.ImgCity).HasMaxLength(2000);
                b.Property(x => x.ImgCode).HasMaxLength(2000);
                b.Property(x => x.ImgContinent).HasMaxLength(2000);
                b.Property(x => x.ImgCountry).HasMaxLength(2000);
                b.Property(x => x.LineCode).HasMaxLength(50);
                b.Property(x => x.LineType).HasMaxLength(50);
                b.Property(x => x.PlaceLeave).HasMaxLength(50);
                b.Property(x => x.PlaceReturn).HasMaxLength(50);
                b.Property(x => x.Sight).HasMaxLength(50);
                b.Property(x => x.Title).HasMaxLength(200);
                b.Property(x => x.TxtTransitCity).HasMaxLength(200);
                b.Property(x => x.Visa).HasMaxLength(200);
            });
            builder.Entity<LineDay>(b =>
            {
                b.ToTable(YmappConsts.Kh10086DbTablePrefix + "LineDays", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Breakfast).HasMaxLength(50);
                b.Property(x => x.CityEnglish).HasMaxLength(50);
                b.Property(x => x.DayHotel).HasMaxLength(50);
                b.Property(x => x.DayTraffic).HasMaxLength(50);
                b.Property(x => x.Describe).HasColumnType("ntext");
                b.Property(x => x.Lunch).HasMaxLength(50);
                b.Property(x => x.ScityDistance).HasMaxLength(50);
                b.Property(x => x.TrafficName).HasMaxLength(200);
            });
            builder.Entity<LineDayTraffic>(b =>
            {
                b.ToTable(YmappConsts.Kh10086DbTablePrefix + "LineDayTraffics", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.TrafficCo).HasMaxLength(50);
                b.Property(x => x.TrafficNo).HasMaxLength(50);
                b.Property(x => x.TrafficTimeEnd).HasMaxLength(50);
                b.Property(x => x.TrafficTimeStart).HasMaxLength(50);
            });
            builder.Entity<LineDaySelf>(b =>
            {
                b.ToTable(YmappConsts.Kh10086DbTablePrefix + "LineDaySelfs", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.CityName).HasMaxLength(50);
                b.Property(x => x.Content).HasColumnType("ntext");
                b.Property(x => x.CountryName).HasMaxLength(50);
                b.Property(x => x.Intro).HasColumnType("ntext");
                b.Property(x => x.Name).HasMaxLength(50);
                b.Property(x => x.Price).HasMaxLength(50);
            });
            builder.Entity<LineDayShop>(b =>
            {
                b.ToTable(YmappConsts.Kh10086DbTablePrefix + "LineDayShops", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).HasMaxLength(50);
                b.Property(x => x.Intro).HasColumnType("ntext");
                b.Property(x => x.CityName).HasMaxLength(50);
                b.Property(x => x.ActivityTime).HasMaxLength(50);
            });
            builder.Entity<LineDayImage>(b =>
            {
                b.ToTable(YmappConsts.Kh10086DbTablePrefix + "LineDayImages", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.City).HasMaxLength(50);
                b.Property(x => x.Continent).HasMaxLength(50);
                b.Property(x => x.Country).HasMaxLength(50);
                b.Property(x => x.ImgCode).HasMaxLength(50);
                b.Property(x => x.ImgPath).HasMaxLength(200);
                b.Property(x => x.Sight).HasMaxLength(200);
            });
            builder.Entity<LineIntro>(b =>
            {
                b.ToTable(YmappConsts.Kh10086DbTablePrefix + "LineIntros", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ChannelType).HasMaxLength(50);
                b.Property(x => x.Describe).HasColumnType("ntext");
                b.Property(x => x.Title).HasMaxLength(50);
            });
            builder.Entity<LineRouteDate>(b =>
            {
                b.ToTable(YmappConsts.Kh10086DbTablePrefix + "LineRouteDates", YmappConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.AdultPrice).HasColumnType("decimal(18,2)");
                b.Property(x => x.AgentPrice).HasColumnType("decimal(18,2)");
                b.Property(x => x.ChildPrice).HasColumnType("decimal(18,2)");
                b.Property(x => x.ProductCode).HasMaxLength(50);
                b.Property(x => x.TeamId).HasMaxLength(50);
                b.Property(x => x.WebsiteTags).HasMaxLength(50);
                b.Property(x => x.OverseasJoinPrice).HasColumnType("decimal(18,2)");
            });
            #endregion

        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser : class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...

        }
    }
}