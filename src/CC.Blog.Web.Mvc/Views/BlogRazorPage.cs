using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace CC.Blog.Web.Views
{
    public abstract class BlogRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected BlogRazorPage()
        {
            LocalizationSourceName = BlogConsts.LocalizationSourceName;
        }
    }
}
