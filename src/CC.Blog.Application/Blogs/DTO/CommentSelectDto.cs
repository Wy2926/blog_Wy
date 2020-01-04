using CC.Blog.PublicDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Blog.Blogs.DTO
{
    /// <summary>
    /// 评论查询Dto
    /// </summary>
    public class CommentSelectDto : ITimeFrameSelectDto, IPagingSelectDto
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public ToExamine? ToExamine { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
