using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Volo.Ymapp.Categorys
{
    public class CategoryDataSeedContributor : IDataSeedContributor, ITransientDependency
    {

        public CategoryDataSeedContributor()
        {
            
        }


        public Task SeedAsync(DataSeedContext context)
        {
            return Task.FromResult(0);
        }
    }
}
