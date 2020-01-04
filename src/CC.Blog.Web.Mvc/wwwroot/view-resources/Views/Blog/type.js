(function () {
    $(function () {

        var _blogService = abp.services.app.blog;
        var _$modal = $('#ArticleTypeCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({
            //rules: {
            //    Password: "required",
            //    ConfirmPassword: {
            //        equalTo: "#Password"
            //    }
            //}
        });

        $('#RefreshButton').click(function () {
            refreshArticleTypeList();
        });

        $('.delete-articleType').click(function () {
            var articleId = $(this).attr("data-articleType-id");
            var articleTitle = $(this).attr('data-articleType-title');

            deleteArticle(articleId, articleTitle);
        });

        $('.edit-articleType').click(function (e) {
            var articleTypeId = $(this).attr("data-articleType-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Blog/EditArticleTypeModal?articleTypeId=' + articleTypeId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#ArticleTypeEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var articleType = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            abp.ui.setBusy(_$modal);
            _blogService.articleTypeCreateOrUpdate(articleType).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new user!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshArticleTypeList() {
            location.reload(true); //reload page to see new user!
        }

        function deleteArticle(articleTypeId, articleTitle) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'Blog'), articleTitle),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _blogService.deleteArticleType(articleTypeId).done(function () {
                            refreshArticleTypeList();
                        });
                    }
                }
            );
        }
    });
})();
