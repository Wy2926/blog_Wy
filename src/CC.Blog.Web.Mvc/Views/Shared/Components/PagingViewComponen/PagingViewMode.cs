using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Blog.Web.Views.Shared.Components.Paging
{
    public class PagingViewMode
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 每页多少条数据
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 一共多少条数据
        /// </summary>
        public int MaxPage { get; set; }
    }
}
