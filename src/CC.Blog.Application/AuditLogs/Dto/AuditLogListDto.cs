using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Auditing;
namespace CC.Blog.AuditLogs.Dto
{
    [AutoMapFrom(typeof(AuditLog))]
    public class AuditLogListDto
    {
        //
        // 摘要:
        //     Start time of the method execution.
        public virtual long Id { get; set; }

        //
        // 摘要:
        //     Start time of the method execution.
        public virtual DateTime ExecutionTime { get; set; }

        //
        // 摘要:
        //     Name (generally computer name) of the client.
        public virtual string ClientName { get; set; }
        //
        // 摘要:
        //     IP address of the client.
        public virtual string ClientIpAddress { get; set; }
        //
        // 摘要:
        //     Total duration of the method call as milliseconds.
        public virtual int ExecutionDuration { get; set; }

        //
        // 摘要:
        //     Executed method name.
        public virtual string MethodName { get; set; }
        //
        // 摘要:
        //     Service (class/interface) name.
        public virtual string ServiceName { get; set; }
        //
        // 摘要:
        //     UserId.
        public virtual long? UserId { get; set; }

        //
        // 摘要:
        //     Abp.Auditing.AuditInfo.CustomData.
        public virtual string CustomData { get; set; }
    }
}
