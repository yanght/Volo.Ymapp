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

            var books = myGroup.AddPermission(YmappPermissions.Books.Default, L("Permission:Books"));

            books.AddChild(YmappPermissions.Books.Create, L("Permission:Create"));
            books.AddChild(YmappPermissions.Books.Update, L("Permission:Update"));
            books.AddChild(YmappPermissions.Books.Delete, L("Permission:Delete"));
            books.AddChild(YmappPermissions.Books.ManagePermissions, L("Permission:ManagePermissions"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<YmappResource>(name);
        }
    }
}
