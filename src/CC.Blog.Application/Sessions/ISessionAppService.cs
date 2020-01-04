using System.Threading.Tasks;
using Abp.Application.Services;
using CC.Blog.Sessions.Dto;

namespace CC.Blog.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
