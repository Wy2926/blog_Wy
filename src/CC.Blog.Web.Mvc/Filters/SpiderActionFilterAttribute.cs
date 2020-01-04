using Abp.Dependency;
using Castle.Core.Logging;
using CC.Blog.Spiders;
using CC.Helper.Expand;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Blog.Web.Filters
{
    public class SpiderActionFilterAttribute : Attribute, IAsyncActionFilter, ITransientDependency
    {
        private readonly ISpiderAppService _spiderAppService;
        public ILogger Logger { get; set; }
        public SpiderActionFilterAttribute(ISpiderAppService spiderAppService)
        {
            _spiderAppService = spiderAppService;
            Logger = NullLogger.Instance;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            //记录蜘蛛记录
            var request = context.HttpContext.Request;
            await _spiderAppService.AddRecord(request.GetAbsoluteUri(), request.Headers["User-Agent"], context.HttpContext.Response.StatusCode);
        }
    }
}
