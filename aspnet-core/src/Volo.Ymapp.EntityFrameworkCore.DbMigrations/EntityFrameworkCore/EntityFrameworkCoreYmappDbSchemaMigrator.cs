using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Ymapp.Data;
using Volo.Abp.DependencyInjection;

namespace Volo.Ymapp.EntityFrameworkCore
{
    [Dependency(ReplaceServices = true)]
    public class EntityFrameworkCoreYmappDbSchemaMigrator 
        : IYmappDbSchemaMigrator, ITransientDependency
    {
        private readonly YmappMigrationsDbContext _dbContext;

        public EntityFrameworkCoreYmappDbSchemaMigrator(YmappMigrationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}