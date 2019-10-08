using Volo.Abp.Reflection;

namespace Volo.Ymapp.Permissions
{
    public static class YmappPermissions
    {
        public const string GroupName = "Ymapp";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

        public static class Books
        {
            public const string Default = GroupName + ".Books";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(YmappPermissions));
        }
    }
}