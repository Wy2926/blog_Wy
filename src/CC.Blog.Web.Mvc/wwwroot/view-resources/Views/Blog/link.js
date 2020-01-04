(function () {
    $(function () {

        var _blogService = abp.services.app.blog;
        var _$modal = $('#FriendshipLinkCreateModal');
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
            refreshLinkList();
        });

        $('.delete-friendshipLink').click(function () {
            var linkId = $(this).attr("data-friendshipLink-id");
            var linkTitle = $(this).attr('data-friendshipLink-title');

            deleteLink(linkId, linkTitle);
        });

        $('.edit-friendshipLink').click(function (e) {
            var linkId = $(this).attr("data-friendshipLink-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Blog/EditFriendshipLinkModal?linkId=' + linkId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#FriendshipLinkEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        $('button[data-link-toExamine]').click(function () {
            var linkId = $(this).attr("data-link-id");
            var title = $(this).attr('data-link-title');
            var toExamine = $(this).attr('data-link-toExamine');
            var btnText = "未通过";
            var str = "不通过审核";
            if (toExamine == "1") {
                str = "通过审核";
                btnText = "已通过";
            }
            var _this = $(this);
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('你确定友链 {0} {1}', 'Blog'), title, str),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _blogService.friendshipLinkToExamine(linkId, toExamine).done(function () {
                            _this.removeAttr("class");
                            _this.addClass("btn");
                            if (toExamine == "1") {
                                _this.attr("data-link-toExamine", 2);
                                _this.addClass("btn-success");
                            }
                            else {
                                _this.attr("data-link-toExamine", 1);
                                _this.addClass("btn-danger");
                            }
                            _this.siblings().remove();
                            _this.text(btnText);
                            abp.message.success(abp.utils.formatString(abp.localization.localize('友链：{0} 已确定 {1}', 'Blog'), title, str), '审核完成');
                        });
                    }
                }
            );
        })

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var link = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            abp.ui.setBusy(_$modal);
            _blogService.friendshipLinkCreateOrUpdate(link).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new user!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshLinkList() {
            location.reload(true); //reload page to see new user!
        }

        function deleteLink(linkId, linkTitle) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'Blog'), linkTitle),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _blogService.deleteFriendshipLink(linkId).done(function () {
                            refreshLinkList();
                        });
                    }
                }
            );
        }
    });
})();
