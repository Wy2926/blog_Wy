using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Blog.Web.Views.Shared.Components.Paging
{
    /// <summary>
    /// 分页组件
    /// </summary>
    public class PagingViewComponen : BlogViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            int maxCount = ViewBag.TotalCount / ViewBag.PageSize;
            if (ViewBag.TotalCount % ViewBag.PageSize > 0)
            {
                maxCount++;
            }
            ViewBag.MaxPage = maxCount;
            return Task.FromResult(View() as IViewComponentResult);
        }
    }
}
