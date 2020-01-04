using Abp.AspNetCore.Mvc.ViewComponents;

namespace CC.Blog.Web.Views
{
    public abstract class BlogViewComponent : AbpViewComponent
    {
        protected BlogViewComponent()
        {
            LocalizationSourceName = BlogConsts.LocalizationSourceName;
        }
    }
}
