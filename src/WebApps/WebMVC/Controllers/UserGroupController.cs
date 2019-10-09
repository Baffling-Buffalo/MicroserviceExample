using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SharedLibraries;
using WebMVC.Attributes;
using WebMVC.Filters;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels;
using WebMVC.ViewModels.UserGroup;

namespace WebMVC.Controllers
{
    public class UserGroupController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStringLocalizer<UserGroupController> stringLocalizer;

        public UserGroupController(IUserService userService, IStringLocalizer<UserGroupController> stringLocalizer)
        {
            _userService = userService;
            this.stringLocalizer = stringLocalizer;
        }
        
        [PermissionClaimFilter(Permissions.UserGroupRead)]
        public async Task<IActionResult> Index()
        {
            return View(nameof(Index),await _userService.GetGroupsForTreeTableAsync());
        }

        [HttpPost]
        [PermissionClaimFilter(Permissions.UserGroupRead)]
        public async Task<string> GroupsForDatatable(int parentGroupId)
        {
            var res = await _userService.GetGroupsForDatatableAsync(Request.Form, parentGroupId);

            return res;
        }

        [PermissionClaimFilter(Permissions.UserGroupCreate)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.UserGroupCreate)]
        public async Task<IActionResult> Create(UserGroupViewModel model)
        {
            await _userService.CreateGroupAsync(new UserGroup()
            {
                Description = model.Description,
                Name = model.Name,
                ParentGroupId = model.ParentGroupId
            });

            _userService.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction(nameof(Index));
        }

        [PermissionClaimFilter(Permissions.UserGroupUpdate)]
        public async Task<IActionResult> Edit(int id)
        {
            var group = await _userService.GetGroup(id);

            return View(new UserGroupViewModel()
            {
                Id = group.Id,
                ParentGroupId = group.ParentGroup.Id,
                Description = group.Description,
                Name = group.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.UserGroupUpdate)]
        public async Task<IActionResult> Edit(UserGroupViewModel model)
        {
            await _userService.UpdateGroupAsync(new UserGroup()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                ParentGroupId = model.ParentGroupId
            });

            _userService.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.UserGroupDelete)]
        public async Task<IActionResult> Delete(int[] groupIds, bool deleteSubGroups = false)
        {
            await _userService.DeleteGroupsAsync(groupIds, deleteSubGroups);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [PermissionClaimFilter(Permissions.UserGroupRead)]
        public async Task<IActionResult> Details(int id)
        {
            var group = await _userService.GetGroup(id);
            var viewModel = new UserGroupDetailsViewModel()
            {
                ChildGroups = string.Join(", ", group.ChildGroups.Select(cg => cg.Name)),
                Description = group.Description,
                Id = group.Id,
                Name = group.Name,
                ParentGroup = group.ParentGroup.Name
            };
            return View(nameof(Details), viewModel);
        }

        [HttpPost]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter(Permissions.AddUserToGroup)]
        public async Task<IActionResult> AddUsersToGroups(UsersGroupsViewModel model)
        {
            if (model.UserIds != null && model.UserIds.Any())
            {
                var userSelects = await GetSelectUsers(model.UserIds);
                ViewBag.UserSelectItems = userSelects;
                model.UserStrings =  userSelects.Select(us => us.Text).ToArray();
            }

            ModelState.Clear(); // as post request automaticly adds model errors on call
           
            return View(model);
        }

        [HttpPost]
        [NoReturnOnModelInvalid]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.AddUserToGroup)]
        public async Task<IActionResult> AddUsersToGroupsSubmit(UsersGroupsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserSelectItems = await GetSelectUsers(model.UserIds);
                return View(nameof(RemoveUsersFromGroups), model);
            }

            await _userService.AddUsersToGroupsAsync(model.UserIds, model.GroupIds);

            if (model.ReturnUrl != "")
            {
                TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully added"]));
                return RedirectToAction("Index", model.ReturnUrl.Split('/')[1]);
            }
            else
            {
                TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully added"]));
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter(Permissions.RemoveUserFromGroup)]
        public async Task<IActionResult> RemoveUsersFromGroups(UsersGroupsViewModel model)
        {
            if (model.UserIds != null && model.UserIds.Any())
            {
                var userSelects = await GetSelectUsers(model.UserIds);
                ViewBag.UserSelectItems = userSelects;
                model.UserStrings = userSelects.Select(us => us.Text).ToArray();
            }

            ModelState.Clear(); // as post request automaticly adds model errors on call

            return View(model);
        }

        [HttpPost]
        [NoReturnOnModelInvalid]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.RemoveUserFromGroup)]
        public async Task<IActionResult> RemoveUsersFromGroupsSubmit(UsersGroupsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserSelectItems = await GetSelectUsers(model.UserIds);
                return View(nameof(RemoveUsersFromGroups), model);
            }

            await _userService.RemoveUsersFromGroupsAsync(model.UserIds, model.GroupIds);

            if (model.ReturnUrl != "")
            {
                TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully removed"]));
                return RedirectToAction("Index", model.ReturnUrl.Split('/')[1]);
            }
            else
            {
                TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully removed"]));
                return RedirectToAction("Index", "User");
            }
        }

        [PermissionClaimFilter(Permissions.UserGroupRead)]
        public async Task<string> GetGroupsForComboTree(int[] withIds)
        {
            return await _userService.GetGroupsForComboTreeAsync(withIds);
        }

        private async Task<IEnumerable<SelectListItem>> GetUserSelectList()
        {
            var selectListItems = (await _userService.GetUsersAsync()).Select(u => new SelectListItem(u.UserName, u.Id));//, selected.Contains(u.Id)));
            
            return selectListItems;
        }

        private async Task<IEnumerable<SelectListItem>> GetSelectUsers(string[] selected)
        {
            var selectListItems = (await _userService.GetUsersWithIdsAsync(selected)).Select(u => new SelectListItem(u.UserName + " - " + u.FirstName + " " + u.LastName, u.Id));//, selected.Contains(u.Id)));

            return selectListItems;
        }
    }
}