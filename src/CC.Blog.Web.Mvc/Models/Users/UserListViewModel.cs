using System.Collections.Generic;
using CC.Blog.Roles.Dto;
using CC.Blog.Users.Dto;

namespace CC.Blog.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
