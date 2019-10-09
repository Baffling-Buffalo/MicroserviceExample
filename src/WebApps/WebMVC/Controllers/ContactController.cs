using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTablesParser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using WebMVC.Attributes;
using WebMVC.Filters;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels;
using WebMVC.ViewModels.Contact;

namespace WebMVC.Controllers
{
    public class ContactController : Controller
    {
        private IContactService _contactService;
        private readonly IStringLocalizer _stringLocalizer;

        public ContactController(IContactService _contactService, IStringLocalizer<ContactController> _stringLocalizer)
        {
            this._contactService = _contactService;
            this._stringLocalizer = _stringLocalizer;
        }

        [PermissionClaimFilter("contact_read")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method that fills datatable with contacts
        /// </summary>
        /// <param name="listIds"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionClaimFilter("contact_read")]
        public async Task<string> GetContactsForDataTable(List<int> listIds = null, bool? active = null, List<int> excludeIds = null)
        {
            var result = await _contactService.GetContactsForDataTable(Request.Form, listIds, active, excludeIds);

            return result;
        }

        //GET
        [PermissionClaimFilter("contact_create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Method that creates new contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("contact_create")]
        public async Task<IActionResult> Create(ContactViewModel model)
        {
            var contactDTO = _contactService.MapContactVMToContactDTO(model);
            await _contactService.CreateContact(contactDTO);

            _contactService.Validate(ModelState);

            if (ModelState.IsValid)
            {
                TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Successfully created contact {0} {1}"], model.FirstName, model.LastName)));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        //GET
        [PermissionClaimFilter("contact_update")]
        public async Task<IActionResult> Update(int id)
        {
            var contact = await _contactService.GetContact(id);
            var model = _contactService.MapGetContactDTOToContactViewModel(contact);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("contact_update")]
        public async Task<IActionResult> Update(ContactViewModel model)
        {
            var contactDTO = _contactService.MapContactVMToContactDTO(model);

            await _contactService.UpdateContact(contactDTO);

            _contactService.Validate(ModelState);

            if (ModelState.IsValid)
            {
                TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Successfully updated contact {0} {1}"], model.FirstName, model.LastName)));
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
        [PermissionClaimFilter("contact_delete")]
        public async Task<IActionResult> Delete(List<int> contactIds)
        {
            if (contactIds.Count == 0)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, string.Format(_stringLocalizer["Please select at least one contact for this action"])));
                return RedirectToAction(nameof(Index));
            }

            await _contactService.DeleteContacts(contactIds);

            TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Contacts are successfully deleted"])));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactService.GetContact(id);
            var model = _contactService.MapGetContactDTOToContactDetailsViewModel(contact);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        public async Task<IActionResult> Activate(List<int> contactIds)
        {
            if (contactIds.Count == 0)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, string.Format(_stringLocalizer["Please select at least one contact for this action"])));
                return RedirectToAction(nameof(Index));
            }

            await _contactService.ActivateContacts(contactIds);

            TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Contacts are successfully activated"])));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        public async Task<IActionResult> Deactivate(List<int> contactIds)
        {
            if (contactIds.Count == 0)
            {
                TempData.Put("Toast", new Toast(ToastType.Warning, string.Format(_stringLocalizer["Please select at least one contact for this action"])));
                return RedirectToAction(nameof(Index));
            }

            await _contactService.DeactivateContacts(contactIds);

            TempData.Put("Toast", new Toast(ToastType.Success, string.Format(_stringLocalizer["Contacts are successfully deactivated"])));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [PermissionClaimFilter("contact_list_add")]
        public IActionResult AddToLists(List<int> contactIds)
        {
            var model = new ContactListViewModel
            {
                ContactIds = contactIds,
                Contacts = contactIds.Select(c => new SelectListItem("", c.ToString())).ToList(),
                ContactsString = GetSelectedContactsString(contactIds)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("contact_list_add")]
        public async Task<IActionResult> AddToLists(ContactListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Contacts = model.ContactIds.Select(c => new SelectListItem("", c.ToString())).ToList();
                model.ContactsString = GetSelectedContactsString(model.ContactIds);

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
        [PermissionClaimFilter("contact_list_remove")]
        public IActionResult RemoveFromLists(List<int> contactIds)
        {
            var model = new ContactListViewModel
            {
                ContactIds = contactIds,
                Contacts = contactIds.Select(c => new SelectListItem("", c.ToString())).ToList(),
                ContactsString = GetSelectedContactsString(contactIds),
                ListsOfContacts = GetListsOfContacts(contactIds)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoReturnOnModelInvalid]
        [PermissionClaimFilter("contact_list_remove")]
        public async Task<IActionResult> RemoveFromLists(ContactListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Contacts = model.ContactIds.Select(c => new SelectListItem("", c.ToString())).ToList();
                model.ContactsString = GetSelectedContactsString(model.ContactIds);
                model.ListsOfContacts = GetListsOfContacts(model.ContactIds);
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

        private string GetSelectedContactsString(List<int> contactIds)
        {
            string contactsString = "";
            _contactService.GetContacts(contactIds).Result.ForEach(c => contactsString += "(" + c.FirstName + " " + c.LastName + " - " + c.Email + "), ");
            contactsString = contactsString.Substring(0, contactsString.Length - 2);

            return contactsString;
        }

        private List<int> GetListsOfContacts(List<int> contactIds)
        {
            var listsOfContacts = new List<int>();
            _contactService.GetListsOfContacts(contactIds).Result.ForEach(c => listsOfContacts.Add(c.Id));

            return listsOfContacts;
        }
    }
}