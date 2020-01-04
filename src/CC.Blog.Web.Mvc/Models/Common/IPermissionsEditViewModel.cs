using System.Collections.Generic;
using CC.Blog.Roles.Dto;

namespace CC.Blog.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}