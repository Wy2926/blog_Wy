using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using CC.Blog.Spiders.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blog.Spiders
{
    /// <summary>
    /// 蜘蛛应用层接口（用于查询蜘蛛访问记录和提交链接给搜索引擎）
    /// </summary>
    public interface ISpiderAppService : IApplicationService, ITransientDependency
    {
        /// <summary>
        /// 添加蜘蛛记录（自动过滤非蜘蛛，目前只通过userAgent过滤）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userAgent"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task AddRecord(string url,string userAgent, int code);

        /// <summary>
        /// 清理蜘蛛记录
        /// </summary>
        /// <param name="startTime">开始时间，为空表示不限制</param>
        /// <param name="EndTime">结束时间，为空表示不限制</param>
        /// <returns></returns>
        Task CleaningRecords(DateTime? startTime, DateTime? EndTime);

        /// <summary>
        /// 蜘蛛记录查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<PagedResultDto<Spider>> GetSpidersAsync(SpiderSelectCondition condition);

        /// <summary>
        /// 提交链接到搜索引擎
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<List<(string, string)>> SubmitLink(string url);

        /// <summary>
        /// 提交链接到搜索引擎
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<List<(string, string)>> SubmitLinks(List<string> urls);

        /// <summary>
        /// 修改提交配置
        /// </summary>
        /// <param name="submitSpiderConfig"></param>
        /// <returns></returns>
        Task UpdateSubmitConfigAsync(SubmitSpiderConfig submitSpiderConfig);
    }
}
