using CC.Blog.PublicDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Blog.AuditLogs.Dto
{
    public class AuditLogSelectDto : ITimeFrameSelectDto, IPagingSelectDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 是否有异常
        /// </summary>
        public bool? IsException { get; set; }

        /// <summary>
        /// 最小耗时（ms）
        /// </summary>
        public int? MinExecutionDuration { get; set; }

        /// <summary>
        /// 最大耗时（ms）
        /// </summary>
        public int? MaxExecutionDuration { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
