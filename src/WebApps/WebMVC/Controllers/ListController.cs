using System;
using System.Collections.Generic;
using System.Linq;
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
using WebMVC.ViewModels.List;

namespace WebMVC.Controllers
{
    public class ListController : Controller
    {
        private IContactService _contactService;
        public IStringLocalizer<ListController> _stringLocalizer { get; }

        public ListController(IContactService _contactService, IStringLocalizer<ListController> _stringLocalizer)
        {
            this._contactService = _contactService;
            this._stringLocalizer = _stringLocalizer;
        }

        [PermissionClaimFilter(Permissions.ListRead)]
        public async Task<IActionResult> Index()
        {
            var lists = await _contactService.GetListsForListTree();
          
            return View(lists);
        }

        /// <summary>
        /// Method that fills datatable with lists
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionClaimFilter("list_read")]
        public async Task<string> GetListsForDataTable(int? parentId = null)
        {
            var result = await _contactService.GetListsForDataTable(Request.Form, parentId);

            return result;
        }

        [HttpPost]
        [PermissionClaimFilter("list_read")]
        public async Task<string> GetListsForComboTreePlugin(int[] listIds)
        {
            return await _contactService.GetListsForComboTreePlugin(listIds);
        }

        [PermissionClaimFilter("list_create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("list_create")]
        public async Task<IActionResult> Create(ListViewModel model)
        {
            var listDTO = _contactService.MapListVMToListDTO(model);
            await _contactService.CreateList(listDTO);

            _contactService.Validate(ModelState);

            if (ModelState.IsValid)
            {
                TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Successfully created list {0}"], model.ListName)));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        [PermissionClaimFilter("list_update")]
        public async Task<IActionResult> Update(int id)
        {
            var list = await _contactService.GetList(id);
            var model = _contactService.MapGetListDTOToListViewModel(list);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("list_update")]
        public async Task<IActionResult> Update(ListViewModel model)
        {
            var listDTO = _contactService.MapListVMToListDTO(model);

            await _contactService.UpdateList(listDTO);

            _contactService.Validate(ModelState);

            if (ModelState.IsValid)
            {
                TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Successfully updated list {0}"], model.ListName)));
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
        [PermissionClaimFilter("list_delete")]
        public async Task<IActionResult> Delete(List<int> listIds)
        {
            if (listIds.Count == 0)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, string.Format(_stringLocalizer["Please select at least one list for this action"])));
                return RedirectToAction(nameof(Index));
            }

            var lists = await _contactService.DeleteLists(listIds);

            List<Toast> toasts = new List<Toast>();

            if (lists.SuccessfullyDeleted.Count>0)
                toasts.Add(new Toast(ToastType.Success, string.Format(_stringLocalizer["Successfully deleted lists: {0}"], string.Join(',', lists.SuccessfullyDeleted.Values))));
            if (lists.UnsuccessfullyDeleted.Count>0)
                toasts.Add(new Toast(ToastType.Error, string.Format(_stringLocalizer["Unsuccessfully deleted lists: {0}"], string.Join(',', lists.UnsuccessfullyDeleted.Values))));

            TempData.Put("Toasts", toasts);

            return RedirectToAction(nameof(Index));
        }

        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> Details(int id)
        {
            var list = await _contactService.GetList(id);
            var model = _contactService.MapGetListDTOToListDetailsViewModel(list);

            return View(model);
        }

        #region Add/RemoveContacts: For the future
        [HttpGet]
        public async Task<IActionResult> AddContacts(List<int> listIds)
        {
            var model = new ContactListViewModel
            {
                ListIds = listIds,
                ListsString = GetSelectedListsString(listIds),
                Lists = listIds.Select(c => new SelectListItem("", c.ToString())).ToList(),
                Contacts = (await _contactService.GetContacts()).Select(c => new SelectListItem(c.FirstName + " " + c.LastName + "(" + c.Email + ")", c.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        public async Task<IActionResult> AddContacts(ContactListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Contacts = (await _contactService.GetContacts()).Select(c => new SelectListItem(c.FirstName + " " + c.LastName + "(" + c.Email + ")", c.Id.ToString())).ToList();
                model.Lists = model.ListIds.Select(c => new SelectListItem("", c.ToString())).ToList();
                model.ListsString = GetSelectedListsString(model.ListIds.ToList());
                return View(model);
            }

            var dto = new ContactListDTO
            {
                ContactIds = model.ContactIds,
                ListIds = model.ListIds
            };

            await _contactService.AddContactsToLists(dto);

            TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Contacts are successfully added to lists"])));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveContacts(List<int> listIds)
        {
            var listsString = GetSelectedListsString(listIds);

            var model = new ContactListViewModel
            {
                ListIds = listIds,
                ListsString = listsString,
                Lists = listIds.Select(c => new SelectListItem("", c.ToString())).ToList(),
                Contacts = (await _contactService.GetContacts()).Select(c => new SelectListItem(c.FirstName + " " + c.LastName + "(" + c.Email + ")", c.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        public async Task<IActionResult> RemoveContacts(ContactListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var listsString = GetSelectedListsString(model.ListIds.ToList());

                model.Contacts = (await _contactService.GetContacts()).Select(c => new SelectListItem(c.FirstName + " " + c.LastName + "(" + c.Email + ")", c.Id.ToString())).ToList();
                model.Lists = model.ListIds.Select(c => new SelectListItem("", c.ToString())).ToList();
                model.ListsString = listsString;
                return View(model);
            }

            var dto = new ContactListDTO
            {
                ContactIds = model.ContactIds,
                ListIds = model.ListIds
            };

            await _contactService.RemoveContactsFromLists(dto);

            TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Contacts are successfully removed from lists"])));
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private string GetSelectedListsString(List<int> listIds)
        {
            string listsString = "";
            _contactService.GetLists(listIds).Result.ForEach(l => listsString += "(" + l.ListName + ") , ");
            listsString = listsString.Substring(0, listsString.Length - 2);

            return listsString;
        }
    }
}