using Microsoft.AspNetCore.Antiforgery;
using CC.Blog.Controllers;

namespace CC.Blog.Web.Host.Controllers
{
    public class AntiForgeryController : BlogControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
