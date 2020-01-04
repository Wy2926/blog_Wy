(function () {
    $(function () {

        var _blogService = abp.services.app.blog;
        var body = $('body');
        body.delegate('#RefreshButton', "click", function () {
            refreshArticleCommentList();
        });

        body.delegate('.delete-comment', "click", function () {
            var commentId = $(this).attr("data-comment-id");
            var commentTitle = $(this).attr('data-comment-title');

            deleteComment(commentId, commentTitle);
        });
        body.delegate('button[data-comment-toExamine]', "click", function () {
            var commentId = $(this).attr("data-comment-id");
            var title = $(this).attr('data-comment-title');
            var toExamine = $(this).attr('data-comment-toExamine');
            var btnText = "未通过";
            var str = "不通过审核";
            if (toExamine == "1") {
                str = "通过审核";
                btnText = "已通过";
            }
            var _this = $(this);
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('你确定评论 {0} {1}', 'Blog'), title, str),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _blogService.articleCommentToExamine(commentId, toExamine).done(function () {
                            _this.removeAttr("class");
                            _this.addClass("btn");
                            if (toExamine == "1") {
                                _this.attr("data-comment-toExamine", 2);
                                _this.addClass("btn-success");
                            }
                            else {
                                _this.attr("data-comment-toExamine", 1);
                                _this.addClass("btn-danger");
                            }
                            _this.siblings().remove();
                            _this.text(btnText);
                            abp.message.success(abp.utils.formatString(abp.localization.localize('评论：{0} 已确定 {1}', 'Blog'), title, str), '审核完成');
                        });
                    }
                }
            );
        })

        function refreshArticleCommentList() {
            location.reload(true); //reload page to see new user!
        }

        function deleteComment(commentId, commentTitle) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'Blog'), commentTitle),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _blogService.deleteArticleComment(commentId).done(function () {
                            refreshArticleCommentList();
                        });
                    }
                }
            );
        }
    });
})();
