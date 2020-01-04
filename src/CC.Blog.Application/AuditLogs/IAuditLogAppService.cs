using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Dependency;
using CC.Blog.AuditLogs.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blog.AuditLogs
{
    public interface IAuditLogAppService : IApplicationService, ITransientDependency
    {
        /// <summary>
        /// 大型项目的审计日志量会十分大，所以最起码要分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(AuditLogSelectDto input);

        /// <summary>
        /// 获取日志详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AuditLog> GetAuditLogDetails(long id);

        /// <summary>
        /// 清楚该时间段的日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task CleaningRecords(DateTime? startTime, DateTime? endTime);

        ///// <summary>
        ///// 一定要提供Excel下载功能，一般建议是按照时间段选取
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);

        ///// <summary>
        ///// 提供全部审计日志的Excel下载，因为数据量会比较大，需要在服务器先压缩好，再提供给客户端下载。
        ///// </summary>
        ///// <returns></returns>
        //Task<FileDto> GetAuditLogsToExcel();

        //List<AuditLogListDto> GetAllAuditLogs(); //错误案例示范，大型项目的审计日志量会十分大，所以最起码要分页
    }
}
