using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebMVC.Attributes;
using WebMVC.Filters;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels.FormGroup;

namespace WebMVC.Controllers
{
    public class FormGroupController : Controller
    {
        private readonly IFormService _formService;
        private readonly IStringLocalizer<FormGroupController> _stringLocalizer;

        public FormGroupController(IFormService _formService, IStringLocalizer<FormGroupController> _stringLocalizer)
        {
            this._formService = _formService;
            this._stringLocalizer = _stringLocalizer;
        }

        [PermissionClaimFilter("formGroup_read")]
        public async Task<IActionResult> Index()
        {
            var groups = await _formService.GetGroupsForTreeTable();
            return View(groups);
        }

        [HttpPost]
        [PermissionClaimFilter("formGroup_read")]
        public async Task<string> GetGroupsForComboTreePlugin(int[] groupIds)
        {
            return await _formService.GetGroupsForComboTreePlugin(groupIds.ToList());
        }

        //GET
        [PermissionClaimFilter("formGroup_create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("formGroup_create")]
        public async Task<IActionResult> Create(FormGroupViewModel model)
        {
            var groupDTO = _formService.MapGroupVMToGroupDTO(model);
            await _formService.CreateGroup(groupDTO);

            _formService.Validate(ModelState);

            if (ModelState.IsValid)
            {
                TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Successfully created group {0}"], model.GroupName)));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        //GET
        [PermissionClaimFilter("formGroup_update")]
        public async Task<IActionResult> Update(int id)
        {
            var group = await _formService.GetGroup(id);
            var model = _formService.MapGetGroupDTOToFormGroupViewModel(group);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("formGroup_update")]
        public async Task<IActionResult> Update(FormGroupViewModel model)
        {
            var groupDTO = _formService.MapFormGroupVMToGroupDTO(model);

            await _formService.UpdateGroup(groupDTO);

            _formService.Validate(ModelState);

            if (ModelState.IsValid)
            {
                TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Successfully updated group {0}"], model.GroupName)));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("formGroup_delete")]
        public async Task<IActionResult> Delete(List<int> groupIds)
        {
            if (groupIds.Count == 0)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, string.Format(_stringLocalizer["Please select at least one group for this action"])));
                return RedirectToAction(nameof(Index));
            }

            var groups = await _formService.DeleteGroups(groupIds);

            List<Toast> toasts = new List<Toast>();

            if (groups.SuccessfullyDeleted.Count > 0)
                toasts.Add(new Toast(ToastType.Success, string.Format(_stringLocalizer["Successfully deleted groups: {0}"], string.Join(',', groups.SuccessfullyDeleted.Values))));
            if (groups.UnsuccessfullyDeleted.Count > 0)
                toasts.Add(new Toast(ToastType.Error, string.Format(_stringLocalizer["Unsuccessfully deleted groups: {0}"], string.Join(',', groups.UnsuccessfullyDeleted.Values))));

            TempData.Put("Toasts", toasts);

            return RedirectToAction(nameof(Index));
        }
    }
}