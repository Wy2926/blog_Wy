using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Web.Models;
using CC.Blog.Controllers;
using CC.Blog.Sitemaps;
using CC.Blog.Sitemaps.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CC.Blog.Web.Mvc.Controllers
{
    [Route("")]
    [ApiController]
    public class SitemapController : BlogControllerBase
    {
        private readonly ISitemapAppService _sitemapAppService;
        public SitemapController(ISitemapAppService sitemapAppService)
        {
            _sitemapAppService = sitemapAppService;
        }

        [DontWrapResult]
        [HttpGet("sitemap.xml")]
        public async Task<Urlset> GetSitemap()
        {
            Response.ContentType = "text/xml, application/xml";
            return await _sitemapAppService.GetUrlsetAsync($"https://{Request.Host}");
        }
    }
}