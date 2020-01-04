(function () {

})

$(function () {
    var _spiderService = abp.services.app.spider;
    var body = $('body');
    var _$form = $('form[name=select]');
    body.delegate('#clearHis', "click", function (e) {
        e.preventDefault();
        if (!_$form.valid()) {
            return;
        }
        var spider =
        {
            startTime: _$form.find("input[name=startTime]").val(),
            endTime: _$form.find("input[name=endTime]").val(),
        }

        abp.message.confirm(
            "你确定清理蜘蛛记录吗",
            function (isConfirmed) {
                if (isConfirmed) {
                    _spiderService.cleaningRecords(spider.startTime, spider.endTime).done(function () {
                        abp.message.success("清理完成")
                        //location.reload(true); //reload page to see edited user!
                    })
                }
            }
        );
    })
})