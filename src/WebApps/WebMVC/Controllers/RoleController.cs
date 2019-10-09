using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
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
    public class RoleController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStringLocalizer<RoleController> stringLocalizer;

        public RoleController(IUserService userService, IStringLocalizer<RoleController> stringLocalizer)
        {
            this._userService = userService;
            this.stringLocalizer = stringLocalizer;
        }

        [PermissionClaimFilter(Permissions.RoleRead)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [PermissionClaimFilter(Permissions.RoleCreate)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [PermissionClaimFilter(Permissions.RoleCreate)]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            await _userService.CreateRoleAsync(new Role()
            {
                Description = model.Description,
                Name = model.Name,
                RoleActions = model.RoleActions
            });

            _userService.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(model);

            TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully created role: "] + model.Name));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [PermissionClaimFilter(Permissions.RoleUpdate)]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _userService.GetRole(id);
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name,
                RoleActions = role.RoleActions,
                SystemRole = role.SystemRole
            });
        }

        [HttpPost]
        [PermissionClaimFilter(Permissions.RoleUpdate)]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            await _userService.UpdateRoleAsync(new Role()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                RoleActions = model.RoleActions
            });

            _userService.Validate(ModelState);
            if (!ModelState.IsValid)
                return View(model);

            TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully edited role: "] + model.Name));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [PermissionClaimFilter(Permissions.RoleRead)]
        public async Task<IActionResult> Details(string id)
        {
            var role = await _userService.GetRole(id);
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Description = role.Description,
                Name = role.Name,
                RoleActions = role.RoleActions,
                SystemRole = role.SystemRole
            });
        }

        [HttpPost]
        [PermissionClaimFilter(Permissions.RoleDelete)]
        public async Task<IActionResult> Delete(string[] roleIds)
        {
            //TODO!
            if (roleIds == null || !roleIds.Any())
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, stringLocalizer["Select atleast 1 role from table"]));
                return RedirectToAction(nameof(Index));
            }

            await _userService.DeleteRolesAsync(roleIds);

            TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully deleted"]));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter(Permissions.AddUserToRole)]
        public async Task<IActionResult> AddUsersToRoles(UsersRolesViewModel model)
        {
            ModelState.Clear(); // as post request automaticly adds model errors on call

            if (model.UserIds != null && model.UserIds.Any())
            {
                var userSelects = await GetSelectUsers(model.UserIds);
                ViewBag.UserSelectItems = userSelects;
                model.UserStrings = userSelects.Select(us => us.Text).ToArray();
            }

            return View(model);
        }

        [HttpPost]
        [NoReturnOnModelInvalid]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.AddUserToRole)]
        public async Task<IActionResult> AddUsersToRolesSubmit(UsersRolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserSelectItems = await GetSelectUsers(model.UserIds);
                return View(nameof(AddUsersToRoles), model);
            }

            await _userService.AddUsersToRolesAsync(model.UserIds, model.RoleIds);

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
        [PermissionClaimFilter(Permissions.RemoveUserFromRole)]
        public async Task<IActionResult> RemoveUsersFromRoles(UsersRolesViewModel model)
        {
            ModelState.Clear(); // as post request automaticly adds model errors on call

            if (model.UserIds != null && model.UserIds.Any())
            {
                var userSelects = await GetSelectUsers(model.UserIds);
                ViewBag.UserSelectItems = userSelects;
                model.UserStrings = userSelects.Select(us => us.Text).ToArray();
            }

            return View(model);
        }

        [HttpPost]
        [NoReturnOnModelInvalid]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.RemoveUserFromRole)]
        public async Task<IActionResult> RemoveUsersFromRolesSubmit(UsersRolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserSelectItems = await GetSelectUsers(model.UserIds);
                return View(nameof(RemoveUsersFromRoles), model);
            }

            await _userService.RemoveUsersFromRolesAsync(model.UserIds, model.RoleIds);

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

        private async Task<IEnumerable<SelectListItem>> GetUserSelectList(string[] selected)
        {
            var selectListItems = (await _userService.GetUsersAsync()).Select(u => new SelectListItem(u.UserName, u.Id));//, selected.Contains(u.Id)));
            
            return selectListItems;
        }

        [PermissionClaimFilter(Permissions.RoleRead)]
        public async Task<string> GetRolesForComboTree(string[] withIds)
        {
            var roles = await _userService.GetRolesWithIdsAsync(withIds);

            var treeDictionary = new Dictionary<string, ComboTreeNodeDTO>();

            roles.ForEach(x => treeDictionary.Add(x.Id, new ComboTreeNodeDTO { Id = x.Id, ParentId = null, Name = x.Name }));

            return JsonConvert.SerializeObject(treeDictionary.Values.Where(x => x.Parent == null));
        }

        [PermissionClaimFilter(Permissions.RoleRead)]
        public async Task<string> GetRolesForDatatable()
        {
            var res = await _userService.GetRolesForDatatableAsync(Request.Form);

            return res;
        }

        [PermissionClaimFilter(Permissions.RoleRead)]
        public List<PermissionSubjectViewModel> RoleActions()
        {
            var actionSubjects = SharedLibraries.Permissions.GetPermissionSubjects();
            var res = new List<PermissionSubjectViewModel>();
            foreach (var @as in actionSubjects) // Localize names
            {
                res.Add(new PermissionSubjectViewModel()
                {
                    Context = stringLocalizer[@as.Context],
                    Name = stringLocalizer[@as.Name],
                    PermissionClaimValues = @as.PermissionNames.ToArray(),
                    PermissionNames = @as.PermissionNames.Select(pn => stringLocalizer[pn].ToString()).ToArray()
                });
            }
            return res;

        }

        private async Task<IEnumerable<SelectListItem>> GetSelectUsers(string[] selected)
        {
            var selectListItems = (await _userService.GetUsersWithIdsAsync(selected)).Select(u => new SelectListItem(u.UserName + " - " + u.FirstName + " " + u.LastName, u.Id));//, selected.Contains(u.Id)));

            return selectListItems;
        }
    }
}