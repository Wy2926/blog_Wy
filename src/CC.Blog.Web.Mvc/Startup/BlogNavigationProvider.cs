using Abp.Application.Navigation;
using Abp.Localization;
using CC.Blog.Authorization;

namespace CC.Blog.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class BlogNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "people",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Blog,
                        L("博客"),
                        icon: "info"
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.Article,
                            L("文章"),
                            url: "/Blog",
                            icon: "info"
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.ArticleType,
                            L("文章分类"),
                            url: "/Blog/Type",
                            icon: "info",
                            requiredPermissionName: PermissionNames.Pages_Blogs_Admin
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.Comment,
                            L("文章评论"),
                            url: "/Blog/Comment",
                            icon: "info",
                            requiredPermissionName: PermissionNames.Pages_Blogs_Admin
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.Proposal,
                            L("文章建议"),
                            url: "/Blog/Proposal",
                            icon: "info",
                            requiredPermissionName: PermissionNames.Pages_Blogs_Admin
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.FriendshipLink,
                            L("友情链接"),
                            url: "/Blog/Flike",
                            icon: "info",
                            requiredPermissionName: PermissionNames.Pages_Sites
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.BlogSiteConfig,
                            L("站点配置"),
                            url: "/Blog/Config",
                            icon: "info",
                            requiredPermissionName: PermissionNames.Pages_Sites
                        )
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.SearchEngines,
                        L("搜索引擎"),
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Sites
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.SearchSpiderHistory,
                            L("蜘蛛历史"),
                            url: "/Spider/Index",
                            icon: "info",
                            requiredPermissionName: PermissionNames.Pages_Sites
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            PageNames.SearchSubmitConfig,
                            L("提交配置"),
                            url: "/Spider/Config",
                            icon: "info",
                            requiredPermissionName: PermissionNames.Pages_Sites
                        )
                    )
                )
                .AddItem(
                        new MenuItemDefinition(
                            PageNames.AuditLogs,
                            L("日志管理"),
                            url: "/AuditLog",
                            icon: "info",
                            requiredPermissionName: PermissionNames.Pages_Sites
                        )
                    )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        icon: "info"
                    )
                ).AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "MultiLevelMenu",
                        L("MultiLevelMenu"),
                        icon: "menu"
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetBoilerplate",
                            new FixedLocalizableString("ASP.NET Boilerplate")
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetBoilerplateHome",
                                new FixedLocalizableString("Home"),
                                url: "https://aspnetboilerplate.com?ref=abptmpl"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetBoilerplateTemplates",
                                new FixedLocalizableString("Templates"),
                                url: "https://aspnetboilerplate.com/Templates?ref=abptmpl"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetBoilerplateSamples",
                                new FixedLocalizableString("Samples"),
                                url: "https://aspnetboilerplate.com/Samples?ref=abptmpl"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetBoilerplateDocuments",
                                new FixedLocalizableString("Documents"),
                                url: "https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl"
                            )
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetZero",
                            new FixedLocalizableString("ASP.NET Zero")
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetZeroHome",
                                new FixedLocalizableString("Home"),
                                url: "https://aspnetzero.com?ref=abptmpl"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetZeroDescription",
                                new FixedLocalizableString("Description"),
                                url: "https://aspnetzero.com/?ref=abptmpl#description"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetZeroFeatures",
                                new FixedLocalizableString("Features"),
                                url: "https://aspnetzero.com/?ref=abptmpl#features"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetZeroPricing",
                                new FixedLocalizableString("Pricing"),
                                url: "https://aspnetzero.com/?ref=abptmpl#pricing"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetZeroFaq",
                                new FixedLocalizableString("Faq"),
                                url: "https://aspnetzero.com/Faq?ref=abptmpl"
                            )
                        ).AddItem(
                            new MenuItemDefinition(
                                "AspNetZeroDocuments",
                                new FixedLocalizableString("Documents"),
                                url: "https://aspnetzero.com/Documents?ref=abptmpl"
                            )
                        )
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BlogConsts.LocalizationSourceName);
        }
    }
}
