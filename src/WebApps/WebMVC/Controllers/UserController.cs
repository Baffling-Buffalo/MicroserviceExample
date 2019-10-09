using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DataTablesParser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SharedLibraries;
using WebMVC.Attributes;
using WebMVC.Filters;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels;
using WebMVC.ViewModels.User;

namespace WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStringLocalizer stringLocalizer;
        private readonly IOptions<AppSettings> settings;

        public UserController(IUserService userService, IStringLocalizer<UserController> stringLocalizer, IOptions<AppSettings> settings)
        {
            _userService = userService;
            this.stringLocalizer = stringLocalizer;
            this.settings = settings;
        }

        [PermissionClaimFilter(Permissions.UserRead)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [PermissionClaimFilter(Permissions.UserRead)]
        public async Task<JsonResult> GetUsersForSelect2(bool? locked = null, int[] groupIds = null, string[] roleIds = null, string searchQuery = null)
        {
            var users = await _userService.GetUsersAsync(locked, groupIds, roleIds, searchQuery);
            return Json(users.Select(u => new
                {
                    id = u.Id,
                    text = u.UserName + " - " + u.FullName
            }
            ));
        }

        [HttpPost]
        [PermissionClaimFilter(Permissions.UserRead)]
        public async Task<string> UsersForDatatable(bool? locked = null, int[] groupIds = null, string[] roleIds = null)
        {
            var users = await _userService.GetUsersForDatatableAsync(Request.Form ,locked, groupIds, roleIds);

            return users;
        }

        [HttpGet]
        [PermissionClaimFilter(Permissions.UserCreate)]
        public async Task<IActionResult> Create()
        {
            ViewBag.AssignedContactIds = await _userService.GetAssignedContactsIds();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.UserCreate)]
        [NoReturnOnModelInvalid]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AssignedContactIds = _userService.GetAssignedContactsIds();
                return View(model);
            }

            await _userService.CreateUserAsync(new NewApplicationUserDTO
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username,
                ContactAssign = model.ContactAssign,
                NewContactEmail = model.NewContactEmail,
                ExistingContactId = model.ExistingContactId,
                NewContactFirstName = model.NewContactFirstName,
                NewContactLastName = model.NewContactLastName,
                NewContactPhone = model.NewContactPhone,
                Active = model.Active
            });

            _userService.Validate(ModelState);
            if (ModelState.IsValid)
            {
                TempData.Put("Toast", new Toast(ToastType.Success, string.Format(stringLocalizer["Successfully created user {0}"], model.Username)));
                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                ViewBag.AssignedContactIds = await _userService.GetAssignedContactsIds();
                return View(model);
            }
        }

        [HttpGet]
        [PermissionClaimFilter(Permissions.UserUpdate)]
        public async Task<IActionResult> Edit([FromServices]IContactService contactService, string id)
        {
            var user = await _userService.GetUserAsync(id);
            ViewBag.AssignedContactIds = await _userService.GetAssignedContactsIds();

            GetContactDTO currentContact = null;
            if (user.ContactId != null && user.ContactId > 0)
                currentContact = await contactService.GetContact(user.ContactId.Value);

            var model = new EditUserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Active = user.LockoutEnd == null ? true : user.LockoutEnd < DateTime.Now,
                ContactAssign = user.ContactId != null ? "Current" : ""
            };


            if (currentContact == null)
                model.HasContact = false;
            else
            {
                model.HasContact = true;
                model.CurrentContactEmail = currentContact.Email;
                model.CurrentContactFullName = currentContact.FirstName + " " + currentContact.LastName;
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.UserUpdate)]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            await _userService.UpdateUserAsync(new UpdateApplicationUserDTO
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Active = model.Active,
                ContactAssign = model.ContactAssign,
                ExistingContactId = model.ExistingContactId,
                NewContactFirstName = model.NewContactFirstName,
                NewContactLastName = model.NewContactLastName,
                NewContactPhone = model.NewContactPhone,
                NewContactEmail = model.NewContactEmail
            });

            _userService.Validate(ModelState);

            if (ModelState.IsValid)
            {
                TempData.Put("Toast", new Toast(ToastType.Success, string.Format(stringLocalizer["Successfully edited user {0}"], model.UserName)));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.AssignedContactIds = await _userService.GetAssignedContactsIds();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.UserUpdate)]
        public async Task<IActionResult> Deactivate(string[] userIds, string lockUntil = null)
        {
            if (userIds == null || userIds.Length < 1)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, stringLocalizer["Select atleast 1 user from table"]));
                return View(nameof(Index));
            }

            CultureInfo culture = new CultureInfo(settings.Value.DatepickerCulture);
            var styles = DateTimeStyles.None;
            DateTime? date = null;
            if (!string.IsNullOrWhiteSpace(lockUntil)) // if date is passed
            {
                if (DateTime.TryParse(lockUntil, culture, styles, out var dt)) 
                {
                    date = dt;
                }
                else // lockdate cannot be parsed
                {
                    TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Incorrect date format"])); // cannot parse date to current culture
                    return View(nameof(Index));
                }
            }

            await _userService.LockUsersAsync(userIds, date);

            TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully deactivated"]));
            return RedirectToAction(nameof(Index));
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.UserUpdate)]
        public async Task<IActionResult> Activate(string[] userIds)
        {
            if (userIds == null || userIds.Length < 1)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, stringLocalizer["Select atleast 1 user from table"]));
                return View(nameof(Index));
            }

            await _userService.UnlockUsersAsync(userIds);

            TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully activated"]));
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionClaimFilter(Permissions.UserDelete)]
        public async Task<IActionResult> Delete(string[] userIds)
        {
            if (userIds == null || userIds.Length < 1)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, stringLocalizer["Select atleast 1 user from table"]));
                return View(nameof(Index));
            }

            await _userService.DeleteUsersAsync(userIds);

            TempData.Put("Toast", new Toast(ToastType.Success, stringLocalizer["Successfully deleted"]));
            return RedirectToAction(nameof(Index));

        }

        private async Task<IEnumerable<SelectListItem>> GetUserSelectList(string[] selected)
        {
            var selectListItems = (await _userService.GetUsersAsync()).Select(u => new SelectListItem(u.UserName, u.Id));//, selected.Contains(u.Id)));

            return selectListItems;
        }
    }
}