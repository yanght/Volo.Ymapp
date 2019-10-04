using Volo.Ymapp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Volo.Ymapp.Permissions
{
    public class YmappPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(YmappPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(YmappPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<YmappResource>(name);
        }
    }
}
