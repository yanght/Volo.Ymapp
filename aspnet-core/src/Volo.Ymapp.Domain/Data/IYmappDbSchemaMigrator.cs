using System.Threading.Tasks;

namespace Volo.Ymapp.Data
{
    public interface IYmappDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
