using Abp.Authorization;
using CC.Blog.Authorization.Roles;
using CC.Blog.Authorization.Users;

namespace CC.Blog.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
