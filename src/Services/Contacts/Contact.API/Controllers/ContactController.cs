using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using BuildingBlocks.EventBusProjects.EventBus.Extensions;
using Contact.API.DTOs.Contact;
using Contact.API.Extensions;
using Contact.API.Filter;
using Contact.API.IntegrationEvents.Events;
using Contact.API.Models;
using Contact.API.ViewModels.Contact;
using DataTablesParser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using SharedLibraries;

namespace Contact.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _contactContext;

        public ContactController(ContactContext contactsContext)
        {
            this._contactContext = contactsContext;
        }
        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <param name="contactIds"></param>
        /// <returns> list of contacts </returns>
        /// <response code="200">Contacts are returned successfully</response>
        /// <response code="500">Exeption while selecting data from dbContext</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetContactDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_read")]
        public async Task<IActionResult> Get([FromQuery]string contactIds = null)
        {
            List<GetContactDTO> contacts;
            try
            {
                IQueryable<Models.Contact> query = _contactContext.Contacts
                .Include(c => c.ContactLists);

                if (contactIds != null)
                {
                    List<int> ids = contactIds.Split(',').Select(int.Parse).ToList();
                    query = query.Where(c => ids.Contains(c.Id));
                }

                contacts = await query
                    .Select(c => new GetContactDTO()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        Phone = c.Phone,
                        Active = c.Active,
                        Lists = c.ContactLists.Select(cl => new { id = cl.FList, name = cl.FListNavigation.ListName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .ToListAsync();

                return Ok(contacts);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get contacts for datatable
        /// </summary>
        /// <returns>Datatable formatted object with contacts</returns>
        /// <response code="200">Contacts are returned successfully</response>
        /// <response code="400">List with passed listId could not be found</response>
        /// <response code="500">Exeption while selecting data from dbContext</response>
        [HttpPost]
        [Route("DataTable")]
        [ProducesResponseType(typeof(IQueryable<GetContactDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_read")]
        public async Task<IActionResult> GetContactsForDataTable([FromBody] ContactsDataTableDTO model)
        {
            IQueryable<GetContactDTO> contacts;

            try
            {
                var query = _contactContext.Contacts
                    .Include(c => c.ContactLists)
                    .ThenInclude(c => c.FListNavigation)
                    .Select(c => c);

                if (model.Active != null)
                    query = query.Where(c => c.Active == model.Active);

                //select contacts that are part of all passed lists
                if (model.ListIds.Count() > 0)
                {
                    //if list with passed listId doesn't exits, return BadRequest()
                    var lists = _contactContext.Lists.Select(l => l.Id).ContainsAllItems(model.ListIds);
                    if (!lists)
                        return BadRequest("Lists could not be found");

                    query = query
                        .Where(c => c.ContactLists.Select(cl => cl.FList).ContainsAllItems(model.ListIds));
                }

                //exclude contacts
                if (model.ExcludeIds.Count() > 0)
                    query = query.Where(c => !model.ExcludeIds.Contains(c.Id));

                contacts = query
                        .Select(c => new GetContactDTO()
                        {
                            Id = c.Id,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email,
                            Phone = c.Phone,
                            Status = c.Active ? "Active" : "Inactive",
                            NumberOfLists = c.ContactLists.Count
                        });

                var keyValues = model.DataTableParameters.Select(kv => new KeyValuePair<string, StringValues>(kv.Key, new StringValues(kv.Value)));
                return Ok(new Parser<GetContactDTO>(keyValues, contacts).Parse());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get contact with passed id
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns></returns>
        /// <response code="400">Contact with passed id could not be found</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetContactDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_read")]
        public async Task<IActionResult> GetContact(int id)
        {
            try
            {
                var contact = await _contactContext.Contacts
                    .Include(cl => cl.ContactLists)
                    .Where(c => c.Id == id)
                    .Select(c => new GetContactDTO()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        Phone = c.Phone,
                        Active = c.Active,
                        Lists = c.ContactLists.Select(cl => new { id = cl.FList, name = cl.FListNavigation.ListName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .SingleOrDefaultAsync();

                if (contact == null)
                    return BadRequest();

                return Ok(contact);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get all contacts that are members of list
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// <response code="200">Contacts retrieved successfully</response>
        /// <response code="204">There are no contacts that have passed list</response>
        /// <response code="400">List with given id could not be found</response>
        /// <response code="500">Exeption occured</response>
        [ProducesResponseType(typeof(List<GetContactDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Route("ContactsInList/{listId}")]
        [HttpGet]
        [PermissionClaimFilter("contact_read")]
        public async Task<IActionResult> GetContactsInList([Required]int listId)
        {
            try
            {
                var list = await _contactContext.Lists.SingleOrDefaultAsync(l => l.Id == listId);

                if (list == null)
                    return BadRequest();

                //select all contacts that have passed list
                var contacts = await _contactContext.Contacts
                    .Include(c => c.ContactLists)
                    .Where(c => c.ContactLists.Select(cl => cl.FList).Contains(listId))
                    .ToListAsync();

                if (contacts.Count == 0)
                    return NoContent();

                var result = contacts
                    .Select(c => new GetContactDTO()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        Phone = c.Phone,
                        Active = c.Active,
                        Lists = c.ContactLists.Select(cl => new { id = cl.FList, name = cl.FListNavigation.ListName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .ToList();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Contact created</response>
        /// <response code="400">List with passed listId could not be found; model not valid</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_create")]
        public async Task<IActionResult> Create(CreateContactDTO model)
        {
            //if contact with given email already exists, then add modal error
            var contactEmail = await _contactContext.Contacts.FirstOrDefaultAsync(c => c.Email == model.Email);
            if (contactEmail != null)
            {
                ModelState.AddModelError("Email", "Contact with given e-mail already exists");
                return BadRequest(ModelState);
            }

            var contact = new Models.Contact()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Active = model.Active
            };

            //add lists to contact
            if (model.ListIds != null)
            {
                //if list with passed id doesn't exist, then return BadRequest()
                var listsExist = _contactContext.Lists.Select(l => l.Id).ContainsAllItems(model.ListIds);
                if (!listsExist)
                    return BadRequest();

                contact.ContactLists = new List<ContactList>();
                foreach (var listId in model.ListIds)
                {
                    contact.ContactLists.Add(new ContactList { FContact = contact.Id, FList = listId });
                }
            }

            try
            {
                await _contactContext.Contacts.AddAsync(contact);
                await _contactContext.SaveChangesAsync();

                return CreatedAtAction(nameof(Create), contact.Id);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Contact updated</response>
        /// <response code="400">Contact with given id could not be found; model not valid</response>
        /// <response code="500">Exeption</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_update")]
        public async Task<IActionResult> Update(UpdateContactDTO model)
        {
            //if another contact already has given email, then add modal error
            var exist = await _contactContext.Contacts.AnyAsync(c => c.Email == model.Email && c.Id != model.Id);
            if (exist)
            {
                ModelState.AddModelError("Email", "Contact with given e-mail already exists");
                return BadRequest(ModelState);
            }

            var contact = await _contactContext.Contacts.Include(c => c.ContactLists).SingleOrDefaultAsync(c => c.Id == model.Id);

            if (contact == null)
                return BadRequest();

            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.Phone = model.Phone;
            contact.Active = model.Active;

            if (model.ListIds.Count() == 0)
            {
                //if none of the lists are checked, then remove all
                _contactContext.ContactLists.RemoveRange(_contactContext.ContactLists.Where(cl => cl.FContact == model.Id));
            }
            else
            {
                //if list with passed id doesn't exist, then return BadRequest()
                var listsExist = _contactContext.Lists.Select(l => l.Id).ContainsAllItems(model.ListIds);
                if (!listsExist)
                    return BadRequest();

                //remove lists that contact already has, but are not selected on the form
                var listsToRemove = contact.ContactLists.Where(cl => !model.ListIds.Any(listID => listID == cl.FList));
                if (listsToRemove != null)
                    _contactContext.RemoveRange(listsToRemove);

                //add lists that contact doesn't have, but are selected on the form
                var listsToAdd = model.ListIds.Where(listID => !contact.ContactLists.Any(cl => cl.FList == listID));
                foreach (var listId in listsToAdd)
                {
                    contact.ContactLists.Add(new ContactList { FContact = contact.Id, FList = listId });
                }
            }

            try
            {
                await _contactContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete contacts
        /// </summary>
        /// <param name="contactIds"></param>
        /// <returns></returns>
        /// <response code="200">Contacts deleted</response>
        /// <response code="400">Contact with given id could not be found</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_delete")]
        public async Task<IActionResult> Delete([Required, FromBody]List<int> contactIds, [FromServices] IEventBus eventBus)
        {
            foreach (var contactId in contactIds)
            {
                var contact = await _contactContext.Contacts.Include(c => c.ContactLists).SingleOrDefaultAsync(c => c.Id == contactId);

                if (contact == null)
                    return BadRequest("Contact with passed id doesn't exist");

                //remove all lists that current contact has
                _contactContext.ContactLists.RemoveRange(contact.ContactLists);

                //remove current contact from db
                _contactContext.Contacts.Remove(contact);
            }

            try
            {
                await _contactContext.SaveChangesAsync();
                eventBus.Publish(new ContactsDeletedIntegrationEvent(contactIds.ToArray())
                                        .WithCorrelationId(HttpContext.GetCorrelationId())
                                        .ByUser(HttpContext.User.Identity.Name));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Activate contacts
        /// </summary>
        /// <param name="contactIds"></param>
        /// <returns></returns>
        /// <response code="200">Contacts are activated</response>
        /// <response code="400">Contacts with given ids could not be found</response>
        [HttpPost]
        [Route("Activate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_update")]
        public async Task<IActionResult> Activate([Required, FromBody]List<int> contactIds)
        {
            foreach (int id in contactIds)
            {
                var contact = await _contactContext.Contacts.SingleOrDefaultAsync(c => c.Id == id);

                if (contact == null)
                    return BadRequest();

                contact.Active = true;
                _contactContext.Contacts.Update(contact);
            }

            try
            {
                await _contactContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Deactivate contacts
        /// </summary>
        /// <param name="contactIds"></param>
        /// <returns></returns>
        /// <response code="200">Contacts are deactivated</response>
        /// <response code="400">Contacts with given ids could not be found</response>
        [HttpPost]
        [Route("Deactivate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_update")]
        public async Task<IActionResult> Deactivate([Required, FromBody]List<int> contactIds)
        {
            foreach (int id in contactIds)
            {
                var contact = await _contactContext.Contacts.SingleOrDefaultAsync(c => c.Id == id);

                if (contact == null)
                    return BadRequest();

                contact.Active = false;
                _contactContext.Contacts.Update(contact);
            }

            try
            {
                await _contactContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Add contacts to lists
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Added successfully</response>
        /// <response code="400">Contact or list could not be found</response>
        [HttpPost]
        [Route("AddContactsToLists")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_list_add")]
        public async Task<IActionResult> AddContactsToLists(ContactListDTO model)
        {
            if (model.ContactIds == null || model.ListIds == null)
            {
                ModelState.AddModelError("ContactIds", "Select at least one contact");
                ModelState.AddModelError("ListIds", "Select at least one list");
                return BadRequest(ModelState);
            }

            List<Models.Contact> contacts = new List<Models.Contact>();
            //go through all passed contactIds
            foreach (var contactId in model.ContactIds)
            {
                //find contact
                var contact = await _contactContext.Contacts.Include(c => c.ContactLists).SingleOrDefaultAsync(c => c.Id == contactId);
                if (contact == null)
                    return BadRequest();
                contacts.Add(contact);
            }

            List<List> lists = new List<List>();
            //go through all passed listIds
            foreach (var listId in model.ListIds)
            {
                //find list
                var list = await _contactContext.Lists.SingleOrDefaultAsync(l => l.Id == listId);
                if (list == null)
                    return BadRequest();
                lists.Add(list);
            }

            foreach (var contact in contacts)
            {
                foreach (var list in lists)
                {
                    // if contact is not already in list, then add it
                    if (!await _contactContext.ContactLists.AnyAsync(c => c.FContact == contact.Id && c.FList == list.Id))
                        await _contactContext.ContactLists.AddAsync(new ContactList { FContact = contact.Id, FList = list.Id });
                }
            }

            try
            {
                await _contactContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Remove contacts from lists
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Removed successfully</response>
        /// <response code="400">Contact or list could not be found</response>
        [HttpPost]
        [Route("RemoveContactsFromLists")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("contact_list_remove")]
        public async Task<IActionResult> RemoveContactsFromLists(ContactListDTO model)
        {
            if (model.ContactIds == null || model.ListIds == null)
            {
                ModelState.AddModelError("ContactIds", "Select at least one contact");
                ModelState.AddModelError("ListIds", "Select at least one list");
                return BadRequest(ModelState);
            }

            List<Models.Contact> contacts = new List<Models.Contact>();
            //go through all passed contactIds
            foreach (var contactId in model.ContactIds)
            {
                //find contact
                var contact = await _contactContext.Contacts.Include(c => c.ContactLists).SingleOrDefaultAsync(c => c.Id == contactId);
                if (contact == null)
                    return BadRequest();
                contacts.Add(contact);
            }

            List<List> lists = new List<List>();
            //go through all passed listIds
            foreach (var listId in model.ListIds)
            {
                //find list
                var list = await _contactContext.Lists.SingleOrDefaultAsync(l => l.Id == listId);
                if (list == null)
                    return BadRequest();
                lists.Add(list);
            }

            foreach (var contact in contacts)
            {
                foreach (var list in lists)
                {
                    var contactList = await _contactContext.ContactLists.SingleOrDefaultAsync(c => c.FContact == contact.Id && c.FList == list.Id);
                    if (contactList != null)
                        _contactContext.ContactLists.Remove(contactList);
                }
            }

            try
            {
                await _contactContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Method returns true if contact with passed id exists, otherwise it returns false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Exists/{id}")]
        [HttpGet]
        public async Task<bool> Exists(int id)
        {
            var exists = false;

            var contactExists = await _contactContext.Contacts.AnyAsync(c => c.Id == id);

            if (contactExists)
                exists = true;

            return exists;
        }

        /// <summary>
        /// Gets lists
        /// </summary>
        /// <param name="ids">String with comma separated ids of lists</param>
        /// <returns>List of lists</returns>
        private async Task<List<List>> GetListsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
                return new List<List>();

            var idsToSelect = numIds
                .Select(id => id.Value);

            var items = await _contactContext.Lists.Where(l => idsToSelect.Contains(l.Id)).ToListAsync();

            return items;
        }
    }
}
