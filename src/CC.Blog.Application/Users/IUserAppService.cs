using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CC.Blog.Roles.Dto;
using CC.Blog.Users.Dto;

namespace CC.Blog.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        /// <summary>
        /// ∑¢ÀÕ” œ‰—È÷§¬Î
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task SendRegisterCode(string email);
    }
}
