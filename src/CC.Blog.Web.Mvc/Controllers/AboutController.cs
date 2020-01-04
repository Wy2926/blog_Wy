using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using CC.Blog.Controllers;

namespace CC.Blog.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : BlogControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
