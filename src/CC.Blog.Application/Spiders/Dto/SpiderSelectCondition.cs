using CC.Blog.PublicDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Blog.Spiders.Dto
{
    /// <summary>
    /// 蜘蛛查询条件
    /// </summary>
    public class SpiderSelectCondition : IPagingSelectDto, ITimeFrameSelectDto
    {
        /// <summary>
        /// 蜘蛛所属分类（百度/等等）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 第N页（默认1）
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 每页大小（默认30）
        /// </summary>
        public int PageSize { get; set; } = 20;

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
