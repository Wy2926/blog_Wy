using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace CC.Blog.Authorization
{
    public class BlogAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Blogs, L("博客"));
            context.CreatePermission(PermissionNames.Pages_Blogs_Comment, L("博客评论"));
            context.CreatePermission(PermissionNames.Pages_Blogs_Admin, L("博客管理"));
            context.CreatePermission(PermissionNames.Pages_Sites, L("站点"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BlogConsts.LocalizationSourceName);
        }
    }
}
