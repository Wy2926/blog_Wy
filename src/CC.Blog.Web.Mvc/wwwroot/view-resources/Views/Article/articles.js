var isloading = 1;
jQuery.ias({
    history: false,
    container: '.content',
    item: '.excerpt',
    pagination: '.pagination',
    next: '.next-page a',
    trigger: '查看更多',
    loader: '<div class="pagination-loading"><img src="images/blog/loading.gif" /></div>',
    triggerPageThreshold: '12',
    onPageChange: function () {

    },
    beforePageChange: function (scrollOffset, nextPageUrl) {
        //防止重复请求加载
        if (isloading != 1) {
            return false;
        }
        //出现“没有更多内容了”标签，还出现加载图标
        if ($("#articlelistdiv").html().indexOf("pagination1") > -1) {
            //修复出现“加载更多”，“没有更多内容了”标签同时出现bug
            $(".ias_trigger").remove();
            return false;
        }
        if (nextPageUrl.substr(-1, 1) > $("#PageCount1").html()) {

            return false;
        }

    },
    onRenderComplete: function () {
        $('.excerpt .thumb').lazyload({
            placeholder: '../images/occupying.png',
            threshold: 400
        });
        $('.excerpt img').attr('draggable', 'false');
        $('.excerpt a').attr('draggable', 'false');
        //保证每次都能加载到正确的页数数据
        var pagecounturl = "?page=" + $(".next-page:last").find("a").attr("href").substr(-1, 1);
        $(".next-page a").removeAttr("href").attr("href", pagecounturl)
        var pagecount = $(".next-page a").attr("href").substr(-1, 1);
        if (pagecount <= $("#PageCount1").html()) {
            //GetArticleList("", pagecount);
        }
        if (pagecount == $("#PageCount1").html()) {
            //由于控件生成“加载更多”按钮需要一点时间，导致检测不出，出现“加载更多”和“没有更多了”同时出现
            var removemore = setInterval(function () {
                if ($('.ias_trigger').prop('outerHTML') != undefined) {
                    $(".ias_trigger").remove();
                    clearInterval(removemore);//防止重复进入死循环消耗性能
                }
            }, 50);
        }
    }
});
var morenews = "";
function GetArticleList(strlinkname, intpagecount) {
    $.ajax({
        type: "POST",
        async: false,//不可以去掉否则，“加载更多”按钮会出现在数据之前
        url: "../Ashx/ArticleList.ashx",
        data: { LinkName: strlinkname, PageCount: intpagecount, Search: "" },
        success: function (data) {
            $("#articlelistdiv").append(data);
            var pagecounturl = "?page=" + $(".next-page1:last").find("a").attr("href").substr(-1, 1);
            $(".next-page a").removeAttr("href").attr("href", pagecounturl);
            isloading = 1;
        }
    })
}