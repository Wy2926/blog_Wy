(function ($) {

    var _blogService = abp.services.app.blog;
    var _$modal = $('#ArticleEditModal');
    var _$form = $('form[name=ArticleEditForm]');
    var _$body = $("#article-createbody");
    function save() {

        if (!_$form.valid()) {
            return;
        }

        var article = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        article.ArticleStrTags = article.ArticleStrTags.replace(/,/ig, '，').split("，");
        article.Html = editorEdit.txt.html();
        abp.ui.setBusy(_$form);
        _blogService.articleCreateOrUpdate(article).done(function () {
            _$modal.modal('hide');
            location.reload(true); //reload page to see edited user!
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    }

    //Handle save button click
    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    $.AdminBSB.input.activate(_$form);

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);