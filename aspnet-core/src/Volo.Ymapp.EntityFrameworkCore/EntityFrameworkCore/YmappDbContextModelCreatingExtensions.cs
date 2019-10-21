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
            b.HasMany(x => x.ProductPictures);
            b.HasMany(x => x.ProductSpecs);
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
                b.HasMany(x => x.ProductStocks);
                b.HasMany(x => x.ProductPrice);

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

        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser : class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...

        }
    }
}