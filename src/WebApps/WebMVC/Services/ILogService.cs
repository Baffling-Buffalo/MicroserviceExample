using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Controllers;
using WebMVC.Services.ModelDTOs;
using WebMVC.Services.ModelDTOs.Form;
using WebMVC.ViewModels.FormGroup;

namespace WebMVC.Services
{
    public interface ILogService
    {
        Task<string> GetAuditLogsForDatatable(IFormCollection formCollection, DateTime? startDate, DateTime? endDate);
        Task DeleteAuditGroups(string[] correlationIds);
    }
}
