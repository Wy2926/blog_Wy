using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using CC.Blog.AuditLogs.Dto;
using System.Linq;
using Abp.Linq.Extensions;
using CC.Blog.PublicDto.Expand;
using Microsoft.EntityFrameworkCore;
using Abp;
using CC.Blog.Authorization;
using Abp.Authorization;

namespace CC.Blog.AuditLogs
{
    [AbpAuthorize(PermissionNames.Pages_Sites)]
    public class AuditLogAppService : AbpServiceBase, IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        private readonly IClientInfoProvider _clientInfoProvider;

        public AuditLogAppService(IRepository<AuditLog, long> auditLogRepository,
            IClientInfoProvider clientInfoProvider)
        {
            _auditLogRepository = auditLogRepository;
            _clientInfoProvider = clientInfoProvider;
        }

        public Task<AuditLog> GetAuditLogDetails(long id)
        {
            return _auditLogRepository.FirstOrDefaultAsync(id);
        }

        public async Task CleaningRecords(DateTime? startTime, DateTime? endTime)
        {
            await _auditLogRepository
                .DeleteAsync(p => (!startTime.HasValue || !(p.ExecutionTime >= startTime)) && (!endTime.HasValue || !(p.ExecutionTime <= endTime)));
        }

        public async Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(AuditLogSelectDto input)
        {
            var query = _auditLogRepository
                  .GetAll()
                  .WhereIf(input.UserId.HasValue, p => p.UserId.Value == input.UserId)
                  .WhereIf(input.IsException.HasValue, p => input.IsException.Value ? p.Exception != null : p.Exception == null)
                  .WhereIf(!string.IsNullOrEmpty(input.ServerName), p => p.ServiceName.Contains(input.ServerName))
                  .WhereIf(!string.IsNullOrEmpty(input.MethodName), p => p.MethodName.Contains(input.MethodName))
                  .WhereIf(!string.IsNullOrEmpty(input.Ip), p => p.ClientIpAddress.Contains(input.Ip))
                  .WhereIf(input.MaxExecutionDuration.HasValue, p => p.ExecutionDuration <= input.MaxExecutionDuration)
                  .WhereIf(input.MinExecutionDuration.HasValue, p => p.ExecutionDuration >= input.MinExecutionDuration)
                  .WhereIf(input.StartTime.HasValue, p => p.ExecutionTime >= input.StartTime)
                  .WhereIf(input.EndTime.HasValue, p => p.ExecutionTime <= input.EndTime);
            var logs = await query
                .OrderByDescending(p => p.ExecutionTime)
                .WherePaging(input)
                .ToListAsync();
            int count = await query.CountAsync();
            return new PagedResultDto<AuditLogListDto>(count, ObjectMapper.Map<List<AuditLogListDto>>(logs));
        }


    }
}
