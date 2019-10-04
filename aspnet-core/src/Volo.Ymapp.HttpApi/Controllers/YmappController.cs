using Volo.Ymapp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Ymapp.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class YmappController : AbpController
    {
        protected YmappController()
        {
            LocalizationResource = typeof(YmappResource);
        }
    }
}