using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Volo.Ymapp.Data
{
    /* This is used if database provider does't define
     * IYmappDbSchemaMigrator implementation.
     */
    public class NullYmappDbSchemaMigrator : IYmappDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}