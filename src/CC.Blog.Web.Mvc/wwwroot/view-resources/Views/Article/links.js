(function () {
    $(function () {
        var _$modal = $('#linkModel');
        var _$form = _$modal.find('form');

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            var link = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            abp.ajax({
                url: abp.appPath +'api/services/app/Blog/FriendshipLinkApply',
                type: 'POST',
                data: JSON.stringify(link),
                contentType: 'application/json',
                success: function (content) {
                    abp.message.success(abp.utils.formatString(abp.localization.localize('网站：{0} 链接 {1}', 'Blog'), link.Name, link.Url), '申请成功，等待审核');
                },
                error: function (e) { }
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });
    });
})();
