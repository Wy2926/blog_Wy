using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CC.Blog.MultiTenancy.Dto;

namespace CC.Blog.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

