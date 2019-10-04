using Volo.Abp.Settings;

namespace Volo.Ymapp.Settings
{
    public class YmappSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(YmappSettings.MySetting1));
        }
    }
}
