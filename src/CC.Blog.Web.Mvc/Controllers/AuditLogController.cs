using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CC.Blog.AuditLogs;
using CC.Blog.AuditLogs.Dto;
using CC.Blog.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CC.Blog.Web.Mvc.Controllers
{
    public class AuditLogController : BlogControllerBase
    {
        public readonly IAuditLogAppService _auditLogAppService;
        public AuditLogController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        public async Task<IActionResult> Index(AuditLogSelectDto selectDto)
        {
            var logs = await _auditLogAppService.GetAuditLogs(selectDto);
            ViewBag.Page = selectDto.Page;
            ViewBag.PageSize = selectDto.PageSize;
            ViewBag.TotalCount = logs.TotalCount;
            return View(logs);
        }

        public async Task<IActionResult> Details(long id)
        {
            var log = await _auditLogAppService.GetAuditLogDetails(id);
            return View(log);
        }
    }
}