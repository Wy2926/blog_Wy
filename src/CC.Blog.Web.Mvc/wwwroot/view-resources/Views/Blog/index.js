(function () {
    $(function () {

        var _blogService = abp.services.app.blog;
        var body = $('body');
        var _$modal = $('#ArticleCreateModal');
        var _$form = _$modal.find('form');
        var reg = new RegExp("(^|&)edit=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != undefined && r.length > 3) {
            $.ajax({
                url: abp.appPath + 'Blog/EditArticleModal?articleId=' + r[2],
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#ArticleEditModal div.modal-content').html(content);
                    $('#ArticleEditModal').attr("style", "display: block; padding-left: 17px;");
                },
                error: function (e) { }
            });
        }
        _$form.validate({
            //rules: {
            //    Password: "required",
            //    ConfirmPassword: {
            //        equalTo: "#Password"
            //    }
            //}
        });

        body.delegate('#RefreshButton', "click", function () {
            refreshArticleList();
        });

        body.delegate('.delete-article', "click", function () {
            var articleId = $(this).attr("data-article-id");
            var articleTitle = $(this).attr('data-article-title');

            deleteArticle(articleId, articleTitle);
        });

        body.delegate('.edit-article', "click", function (e) {
            var articleId = $(this).attr("data-article-id");

            e.preventDefault();
            $.ajax({
                url: 'Blog/EditArticleModal?articleId=' + articleId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#ArticleEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var article = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            article.ArticleStrTags = article.ArticleStrTags.replace(/,/ig, '，').split("，");
            article.html = editorCreate.txt.html();
            abp.ui.setBusy(_$modal);
            _blogService.articleCreateOrUpdate(article).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new user!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshArticleList() {
            location.reload(true); //reload page to see new user!
        }

        function deleteArticle(articleId, articleTitle) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'Blog'), articleTitle),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _blogService.deleteArticle(articleId).done(function () {
                            refreshArticleList();
                        });
                    }
                }
            );
        }
    });
})();
