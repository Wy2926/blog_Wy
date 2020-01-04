(function () {
    $(function () {

        var _blogService = abp.services.app.blog;
        var _$modal = $('#BlogSiteConfigEditModal');
        var _$form = _$modal.find('form');
        var _$file = $("#file");
        _$form.validate({
            //rules: {
            //    Password: "required",
            //    ConfirmPassword: {
            //        equalTo: "#Password"
            //    }
            //}
        });

        $("#weChat").click(function () {
            _$file.trigger("click");
        })

        $("#deleteWeChat").click(function () {
            $("#weChat").attr('src','/images/up.png')
            $("#weChatValue").val('');
        })

        _$file.change(function () {
            ////取到文件对象
            //var file = _$file[0].files[0]
            ////放到img控件上，借助于filereader 中间的东西，文件阅读器
            ////生成一个文件阅读器对象赋值给filereader
            //var filereader = new FileReader()
            ////把文件读到filereader对象中
            ////读文件需要时间，需要文件读完再去操作img
            ////如果没这一步操作下面不一定变化
            //filereader.readAsDataURL(file)

            //filereader.onload = function () {
            //    $("#weChat").attr('src', filereader.result)
            //}
            var uploadData = new FormData();
            uploadData.append(_$file[0].name, _$file[0].files[0]);
            abp.ajax({
                url: '/Blog/OnPostUpload',
                type: 'POST',
                data: uploadData,
                processData: false,
                contentType: false,
                async: false,
                success: function (content) {
                    $("#weChat").attr('src', content.data[0])
                    $("#weChatValue").val(content.data[0]);
                },
                error: function (e) {
                    abp.message.error(e);
                }
            });
        })

        $('#RefreshButton').click(function () {
            refreshConfig();
        });

        $('#clearcache').click(function () {
            _blogService.clearCache().done(function () {
                abp.message.success("博客缓存清理完成", '缓存清理');
            });
        })

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var config = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            abp.ui.setBusy(_$modal);
            _blogService.updateSiteConfig(config).done(function () {
                _$modal.modal('hide');
                abp.message.success("站点配置修改成功", '修改完成');
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
