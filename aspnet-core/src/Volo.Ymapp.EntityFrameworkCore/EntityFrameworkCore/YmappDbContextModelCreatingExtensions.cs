using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users;
using Volo.Ymapp.Books;
using Volo.Ymapp.Categorys;

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
        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser : class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...
            
        }
    }
}