using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using CC.Blog.Authorization;
using CC.Blog.Controllers;
using CC.Blog.Spiders;
using CC.Blog.Spiders.Dto;
using CC.Helper;
using Microsoft.AspNetCore.Mvc;

namespace CC.Blog.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Sites)]
    public class SpiderController : BlogControllerBase
    {
        public readonly ISpiderAppService _spiderAppService;
        public SpiderController(ISpiderAppService spiderAppService)
        {
            _spiderAppService = spiderAppService;
        }

        public async Task<IActionResult> Index(SpiderSelectCondition spiderSelect)
        {
            var spiders = await _spiderAppService.GetSpidersAsync(spiderSelect);
            ViewBag.Page = spiderSelect.Page;
            ViewBag.TotalCount = spiders.TotalCount;
            ViewBag.PageSize = spiderSelect.PageSize;
            return View(spiders);
        }

        public async Task<IActionResult> Config()
        {
            var config = await JsonConfig<SubmitSpiderConfig>.GetSiteConfigAsync();
            return View(config);
        }
    }
}