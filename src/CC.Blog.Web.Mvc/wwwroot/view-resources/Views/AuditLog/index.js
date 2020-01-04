(function () {

})

$(function () {
    var _auditLogService = abp.services.app.auditLog;
    var body = $('body');
    var _$form = $('form[name=select]');
    body.delegate('#clearHis', "click", function (e) {
        e.preventDefault();
        if (!_$form.valid()) {
            return;
        }
        var auditLog =
        {
            startTime: _$form.find("input[name=startTime]").val(),
            endTime: _$form.find("input[name=endTime]").val(),
        }

        abp.message.confirm(
            "你确定清理审计日志记录吗",
            function (isConfirmed) {
                if (isConfirmed) {
                    _auditLogService.cleaningRecords(auditLog.startTime, auditLog.endTime).done(function () {
                        abp.message.success("清理完成")
                        //location.reload(true); //reload page to see edited user!
                    })
                }
            }
        );
    })

    body.delegate('.select-log', "click", function (e) {
        var logId = $(this).attr("data-auditlogid");
        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'AuditLog/Details?id=' + logId,
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {
                $('#AuditLogDetailsModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });
})