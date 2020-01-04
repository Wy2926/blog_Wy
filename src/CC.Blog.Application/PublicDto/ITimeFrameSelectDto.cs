using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Blog.PublicDto
{
    public interface ITimeFrameSelectDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime? EndTime { get; set; }
    }
}
