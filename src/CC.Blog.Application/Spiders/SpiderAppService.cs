using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Abp.UI;
using CC.Blog.Spiders.Dto;
using CC.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace CC.Blog.Spiders
{
    /// <summary>
    /// 蜘蛛应用层（用于查询蜘蛛访问记录和提交链接给搜索引擎）
    /// </summary>
    public class SpiderAppService : AbpServiceBase, ISpiderAppService
    {
        private readonly IRepository<Spider, long> _spiderRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IClientInfoProvider _clientInfoProvider;
        public SpiderAppService(
            IRepository<Spider, long> spiderRepository,
            ICacheManager cacheManager,
            IClientInfoProvider clientInfoProvider)
        {
            _spiderRepository = spiderRepository;
            _cacheManager = cacheManager;
            _clientInfoProvider = clientInfoProvider;
        }

        #region 私有方法

        #endregion

        [RemoteService(false)]
        public async Task AddRecord(string url, string userAgent, int code)
        {
            string spiderName = SpiderTool.UaSelect(userAgent);
            if (spiderName == null)
                return;
            await _spiderRepository.InsertAsync(new Spider()
            {
                Ip = _clientInfoProvider.ClientIpAddress,
                Name = spiderName ?? "未知",
                Url = url,
                Code = code,
                UserAgent = userAgent,
                CreationTime = DateTime.Now
            });
        }

        public async Task CleaningRecords(DateTime? startTime, DateTime? endTime)
        {
            await _spiderRepository
                .DeleteAsync(p => (!startTime.HasValue || !(p.CreationTime >= startTime)) && (!endTime.HasValue || !(p.CreationTime <= endTime)));
        }

        public async Task<PagedResultDto<Spider>> GetSpidersAsync(SpiderSelectCondition condition)
        {
            var query = _spiderRepository
                .GetAll()
                .WhereIf(condition.Name != null, p => p.Name == condition.Name)
                .WhereIf(condition.StartTime.HasValue, p => p.CreationTime >= condition.StartTime.Value)
                .WhereIf(condition.EndTime.HasValue, p => p.CreationTime <= condition.EndTime.Value)
                .WhereIf(condition.Url != null, p => p.Url.Contains(condition.Url));
            var spiders = await query
                .OrderByDescending(p => p.CreationTime)
                .Page(condition.Page, condition.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();
            return new PagedResultDto<Spider>(count, spiders);
        }

        public async Task UpdateSubmitConfigAsync(SubmitSpiderConfig submitSpiderConfig)
        {
            await JsonConfig<SubmitSpiderConfig>.CreateOrUpdateSiteConfig(submitSpiderConfig);
        }

        public async Task<List<(string, string)>> SubmitLink(string url)
        {
            var config = JsonConfig<SubmitSpiderConfig>.GetSiteConfig();
            List<(string, string)> list = new List<(string, string)>();
            ResponseResult result = await SearchSubmit.SubmitBaiduAsync(new List<string>() { url }, "q0dCbyNXm9daxf71");
            if (result.SuccessUrl.Count == 0)
            {
                throw new UserFriendlyException(500, "提交链接失败");
            }
            list.Add(("百度", result.Error?.Message));
            return list;
        }

        public async Task<List<(string, string)>> SubmitLinks(List<string> urls)
        {
            var config = JsonConfig<SubmitSpiderConfig>.GetSiteConfig();
            List<(string, string)> list = new List<(string, string)>();
            ResponseResult result = await SearchSubmit.SubmitBaiduAsync(urls, config.BaiduKey);
            list.Add(("百度", result.Error?.Message));
            return list;
        }
    }
}
