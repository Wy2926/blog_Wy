$(function () {
    var _blogService = abp.services.app.blog;
    var _$form = $('form[name=select]');
    window.selectTable = selectTable;

    _$form.find('button[type="submit"]').click(function (e) {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }
        selectTable();
        return false;
    });

    function selectTable(page, pageSize) {
        var select = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        if (page != undefined) {
            select.page = page;
        }
        if (pageSize != undefined) {
            select.pageSize = pageSize;
        }
        $(".dataTable").load(goToPage(_$form.attr("action"), select) + " .dataTable>div");
    }

    function goToPage(path, obj) {
        var i, url = '';
        for (i in obj) url += '&' + i + '=' + obj[i];
        return path + url.replace(/./, '?');
    }
})