using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SharedLibraries;
using WebMVC.Attributes;
using WebMVC.Filters;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels;
using WebMVC.ViewModels.Role;

namespace WebMVC.Controllers
{
    public class AuditLogController : Controller
    {
        private readonly ILogService _logService;
        private readonly IStringLocalizer<AuditLogController> stringLocalizer;

        public AuditLogController(ILogService logService, IStringLocalizer<AuditLogController> stringLocalizer)
        {
            this._logService = logService;
            this.stringLocalizer = stringLocalizer;
        }

        //[PermissionClaimFilter(Permissions.RoleRead)]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<string> AuditsForDatatable([FromServices]IOptions<AppSettings> settings, string startDateString, string endDateString)
        {
            CultureInfo culture = new CultureInfo(settings.Value.DatepickerCulture);
            var styles = DateTimeStyles.None;
            DateTime? startDate = null;
            if (!string.IsNullOrWhiteSpace(startDateString)) // if date is passed
            {
                if (DateTime.TryParse(startDateString, culture, styles, out var dt))
                {
                    startDate = dt;
                }
            }
            DateTime? endDate = null;
            if (!string.IsNullOrWhiteSpace(endDateString)) // if date is passed
            {
                if (DateTime.TryParse(endDateString, culture, styles, out var dt))
                {
                    endDate = dt;
                }
            }

            return await _logService.GetAuditLogsForDatatable(Request.Form, startDate, endDate);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string[] auditGroupIds)
        {
            if (auditGroupIds == null || auditGroupIds.Length < 1)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, stringLocalizer["Select atleast 1 audit from table"]));
                return View(nameof(Index));
            }

            await _logService.DeleteAuditGroups(auditGroupIds);

            TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully deleted"]));
            return RedirectToAction(nameof(Index));
        }
    }
}