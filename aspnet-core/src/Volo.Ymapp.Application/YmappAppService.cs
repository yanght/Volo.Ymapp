using System;
using System.Collections.Generic;
using System.Text;
using Volo.Ymapp.Localization;
using Volo.Abp.Application.Services;

namespace Volo.Ymapp
{
    /* Inherit your application services from this class.
     */
    public abstract class YmappAppService : ApplicationService
    {
        protected YmappAppService()
        {
            LocalizationResource = typeof(YmappResource);
        }
    }
}
