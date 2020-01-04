using System.Threading.Tasks;
using Abp.Application.Services;
using CC.Blog.Authorization.Accounts.Dto;

namespace CC.Blog.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
