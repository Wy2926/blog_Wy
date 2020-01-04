using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Blog.PublicDto
{
    public interface IPagingSelectDto
    {
        /// <summary>
        /// 第N页
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        int PageSize { get; set; }
    }
}
