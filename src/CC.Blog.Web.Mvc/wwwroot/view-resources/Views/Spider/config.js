(function () {
    $(function () {

        var _spiderService = abp.services.app.spider;
        var _$modal = $('#SpiderConfigEditModal');
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
            refreshConfig();
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var config = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            abp.ui.setBusy(_$modal);
            _spiderService.updateSubmitConfig(config).done(function () {
                _$modal.modal('hide');
                abp.message.success("配置修改成功", '修改完成');
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshConfig() {
            location.reload(true); //reload page to see new user!
        }
    });
})();
