using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using Contact.API.DTOs;
using Contact.API.DTOs.Contact;
using Contact.API.DTOs.List;
using Contact.API.Extensions;
using Contact.API.Filter;
using Contact.API.Models;
using DataTablesParser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ContactContext _contactContext;

        public ListController(IEventBus eventBus, ContactContext contactContext)
        {
            this._eventBus = eventBus;
            this._contactContext = contactContext;
        }

        /// <summary>
        /// Get all lists
        /// </summary>
        /// <returns> list of lists </returns>
        /// <response code="200">Lists are returned successfully</response>
        /// <response code="500">Exeption while selecting data from contactContext</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetListDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> Get([FromQuery] string listIds = null)
        {
            List<GetListDTO> lists;
            try
            {
                IQueryable<List> query = _contactContext.Lists.Include(c => c.ContactLists);

                if (listIds != null)
                {
                    List<int> ids = listIds.Split(',').Select(int.Parse).ToList();
                    query = query.Where(c => ids.Contains(c.Id));
                }

                lists = await query
                    .Select(l => new GetListDTO()
                    {
                        Id = l.Id,
                        ListName = l.ListName,
                        Description = l.Description,
                        ParentId = l.ParentId,
                        ParentName = l.Parent.ListName,
                        Contacts = l.ContactLists.Select(cl => new { id = cl.FContact, name = cl.FContactNavigation.FirstName + " " + cl.FContactNavigation.LastName }).ToDictionary(x => x.id, x => x.name),
                        ChildLists = l.ChildLists.Select(c => new { id = c.Id, name = c.ListName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .ToListAsync();

                return Ok(lists);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get list with passed id
        /// </summary>
        /// <param name="id">List id</param>
        /// <returns></returns>
        /// <response code="400">List with passed id could not be found</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetListDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> GetList(int id)
        {
            try
            {
                var list = await _contactContext.Lists
                    .Include(cl => cl.ContactLists)
                    .Where(l => l.Id == id)
                    .Select(l => new GetListDTO()
                    {
                        Id = l.Id,
                        ListName = l.ListName,
                        Description = l.Description,
                        ParentId = l.ParentId,
                        ParentName = l.Parent.ListName,
                        Contacts = l.ContactLists.Select(cl => new { id = cl.FContact, name = cl.FContactNavigation.FirstName + " " + cl.FContactNavigation.LastName }).ToDictionary(x => x.id, x => x.name),
                        ChildLists = l.ChildLists.Select(c => new { id = c.Id, name = c.ListName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .SingleOrDefaultAsync();

                if (list == null)
                    return BadRequest();

                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get lists for datatable
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lists are returned successfully</response>
        /// <response code="400">List with passed listId could not be found</response>
        /// <response code="500">Exeption while selecting data from dbContext</response>
        [HttpPost]
        [Route("DataTable")]
        [ProducesResponseType(typeof(IQueryable<ListParserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> GetListsForDataTable([FromQuery]int? parentId = null)
        {
            IQueryable<ListParserDTO> lists;

            try
            {
                var query = _contactContext.Lists
                    .Include(l => l.ContactLists)
                    .Select(l => l);

                if (parentId != null)
                {
                    query = query
                        .Where(l => l.ParentId == parentId);
                }

                lists = query
                        .Select(l => new ListParserDTO()
                        {
                            Id = l.Id,
                            Name = l.ListName,
                            Description = l.Description,
                            Parent = l.Parent.ListName,
                            NumberOfChildren = l.ChildLists.Count,
                            NumberOfContacts = l.ContactLists.Count
                        });

                var parser = new Parser<ListParserDTO>(Request.Form, lists);

                return Ok(parser.Parse());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get lists for combroTreePlugin
        /// </summary>
        /// <returns>comboTreePlugin formatted json with lists</returns>
        /// <response code="200">json returned successfully</response>
        /// <response code="400">Passed list of ids is not valid. Must be comma-separated list of numbers</response>
        /// <response code="500">Exeption while selecting data from _contactContext</response>
        [HttpGet]
        [Route("ComboTreePlugin")]
        [ProducesResponseType(typeof(IEnumerable<ComboTreeNode>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> GetListsForComboTreePlugin([FromQuery] string listIds = null)
        {
            List<List> lists;

            try
            {
                if (!string.IsNullOrEmpty(listIds))
                {
                    lists = await GetListsByIdsAsync(listIds);

                    if (!lists.Any())
                        return BadRequest();
                }
                else
                {
                    lists = await _contactContext.Lists.ToListAsync();
                }

                var tree = GetComboTreeNodeList(lists);

                return Ok(tree);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get lists for combroTreePlugin
        /// </summary>
        /// <returns>comboTreePlugin formatted json with lists</returns>
        /// <response code="200">json returned successfully</response>
        /// <response code="400">Passed list of ids is not valid. Must be comma-separated list of numbers</response>
        /// <response code="500">Exeption while selecting data from _contactContext</response>
        [HttpGet]
        [Route("ListTree")]
        [ProducesResponseType(typeof(IEnumerable<ListTreeNode>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> GetListsForListTree([FromQuery] string listIds = null)
        {
            List<List> lists;

            try
            {
                if (!string.IsNullOrEmpty(listIds))
                {
                    lists = await GetListsByIdsAsync(listIds);

                    if (!lists.Any())
                        return BadRequest();
                }
                else
                {
                    lists = await _contactContext.Lists.Include(l => l.ContactLists).ToListAsync();
                }

                var tree = GetListTreeNodeList(lists);

                return Ok(tree);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get all lists that passed contact has
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        /// <response code="200">Lists retrieved successfully</response>
        /// <response code="204">There are no lists that passed contact has</response>
        /// <response code="400">Contact with given id could not be found</response>
        /// <response code="500">Exeption occured</response>
        [ProducesResponseType(typeof(List<GetListDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Route("ListsInContact/{contactId}")]
        [HttpGet]
        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> GetListsInContact([Required]int contactId)
        {
            try
            {
                var contact = await _contactContext.Contacts.SingleOrDefaultAsync(c => c.Id == contactId);

                if (contact == null)
                    return BadRequest();

                //select all lists that passed contact has
                var lists = _contactContext.Lists
                    .Include(l => l.ContactLists)
                    .Include(l => l.ChildLists)
                    .Where(l => l.ContactLists.Select(cl => cl.FContact).Contains(contactId));

                if (lists.Count() == 0)
                    return NoContent();

                var result = lists
                    .Select(l => new GetListDTO()
                    {
                        Id = l.Id,
                        ListName = l.ListName,
                        Description = l.Description,
                        ParentId = l.ParentId,
                        ParentName = l.Parent.ListName,
                        Contacts = l.ContactLists.Select(cl => new { id = cl.FContact, name = cl.FContactNavigation.FirstName + " " + cl.FContactNavigation.LastName }).ToDictionary(x => x.id, x => x.name),
                        ChildLists = l.ChildLists.Select(c => new { id = c.Id, name = c.ListName }).ToDictionary(x => x.id, x => x.name)
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
        /// Get union of lists that passed contacts have
        /// </summary>
        /// <param name="contactIds"></param>
        /// <returns></returns>
        /// <response code="200">Lists retrieved successfully</response>
        /// <response code="500">Exeption occured</response>
        [ProducesResponseType(typeof(List<GetListDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Route("GetListsOfContacts")]
        [HttpGet]
        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> GetListsOfContacts([FromQuery] string contactIds = null)
        {
            List<GetListDTO> lists;
            try
            {
                IQueryable<List> query = _contactContext.Lists.Include(c => c.ContactLists);

                if (contactIds != null)
                {
                    List<int> ids = contactIds.Split(',').Select(int.Parse).ToList();
                    query = query.Where(l => l.ContactLists.Any(cl=> ids.Contains(cl.FContact))); //select lists that passed contacts have (union)
                }

                lists = await query
                    .Select(l => new GetListDTO()
                    {
                        Id = l.Id,
                        ListName = l.ListName,
                        Description = l.Description,
                        ParentId = l.ParentId,
                        ParentName = l.Parent.ListName,
                        Contacts = l.ContactLists.Select(cl => new { id = cl.FContact, name = cl.FContactNavigation.FirstName + " " + cl.FContactNavigation.LastName }).ToDictionary(x => x.id, x => x.name),
                        ChildLists = l.ChildLists.Select(c => new { id = c.Id, name = c.ListName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .ToListAsync();

                return Ok(lists);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get children of a list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Children returned successfully</response>
        /// <response code="400">List with passed id could not be found</response>
        /// <response code="500">Exeption occured</response>
        [HttpGet]
        [Route("GetChildren/{id}")]
        [ProducesResponseType(typeof(List<GetListDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_read")]
        public async Task<IActionResult> GetChildren(int id)
        {
            try
            {
                var list = await _contactContext.Lists.SingleOrDefaultAsync(l => l.Id == id);

                if (list == null)
                    return BadRequest();

                var children = _contactContext.Lists
                    .Where(l => l.ParentId == id)
                    .Select(l => new GetListDTO()
                    {
                        Id = l.Id,
                        ListName = l.ListName,
                        Description = l.Description,
                        ParentId = l.ParentId,
                        ParentName = l.Parent.ListName,
                        Contacts = l.ContactLists.Select(cl => new { id = cl.FContact, name = cl.FContactNavigation.FirstName + " " + cl.FContactNavigation.LastName }).ToDictionary(x => x.id, x => x.name),
                        ChildLists = l.ChildLists.Select(c => new { id = c.Id, name = c.ListName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .ToListAsync();

                return Ok(children);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">List created</response>
        /// <response code="409">Model not valid</response>
        /// <response code="400">ParentId could not be found; contact with passed contactId could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_create")]
        public async Task<IActionResult> Create(CreateListDTO model)
        {
            var listExists = await _contactContext.Lists.AnyAsync(l => l.ListName == model.ListName);
            if (listExists)
                ModelState.AddModelError("ListName", "List with given name already exists");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var list = new List()
            {
                ListName = model.ListName,
                Description = model.Description,
                ParentId = model.ParentId
            };

            #region old with contacts
            ////add contacts to lists
            //if (model.ContactIds != null)
            //{
            //    //if contact with passed id doesn't exist, then return BadRequest()
            //    var contactExists = _contactContext.Contacts.Select(c => c.Id).ContainsAllItems(model.ContactIds);
            //    if (!contactExists)
            //        return BadRequest();

            //    list.ContactLists = new List<ContactList>();

            //    foreach (var contactId in model.ContactIds)
            //    {
            //        list.ContactLists.Add(new ContactList { FContact = contactId, FList = list.Id });
            //    }
            //}
            #endregion

            try
            {
                await _contactContext.Lists.AddAsync(list);
                await _contactContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">List updated</response>
        /// <response code="400">Model is not valid; List with given id could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_update")]
        public async Task<IActionResult> Update(UpdateListDTO model)
        {
            var list = await _contactContext.Lists.FindAsync(model.Id);
            if (list == null)
                return BadRequest();

            if (model.ParentId.HasValue)
            {
                if (!IsParentValid(list, model.ParentId.Value))
                    return BadRequest(ModelState);
            }

            var listExists = await _contactContext.Lists.AnyAsync(l => l.ListName == model.ListName && l.Id != model.Id);
            if (listExists)
                ModelState.AddModelError("ListName", "List with given name already exists");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            list.ListName = model.ListName;
            list.Description = model.Description;
            list.ParentId = model.ParentId;

            try
            {
                _contactContext.Lists.Update(list);
                await _contactContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            #region Old with contacts
            //await CheckIfListIsItsOwnParent(model.Id, model.ParentId);
            //await CheckIfListAlreadyExists(model.ListName, model.Id);

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var list = await _contactContext.Lists.Include(l => l.ContactLists).SingleOrDefaultAsync(l => l.Id == model.Id);

            //if (list == null)
            //    return BadRequest();

            //list.ListName = model.ListName;
            //list.Description = model.Description;
            //list.ParentId = model.ParentId;

            //var listHasContacts = list.ContactLists.Count > 0;

            //if (model.ContactIds == null)
            //{
            //    if (listHasContacts)
            //    {
            //        //if none of the contacts are checked, then remove all
            //        _contactContext.ContactLists.RemoveRange(_contactContext.ContactLists.Where(cl => cl.FList == model.Id));
            //    }
            //}
            //else
            //{
            //    //if contact with passed id doesn't exist, then return BadRequest()
            //    var contactExists = _contactContext.Contacts.Select(c => c.Id).ContainsAllItems(model.ContactIds);

            //    if (!contactExists)
            //        return BadRequest();

            //    if (listHasContacts)
            //    {
            //        //remove contacts that list already has, but are not selected on the form
            //        var contactsToRemove = list.ContactLists.Where(cl => !model.ContactIds.Any(contactId => contactId == cl.FContact));
            //        if (contactsToRemove != null)
            //            _contactContext.RemoveRange(contactsToRemove);
            //    }

            //    //add contacts that list doesn't have, but are selected on the form
            //    var contactsToAdd = model.ContactIds.Where(contactId => !list.ContactLists.Any(cl => cl.FContact == contactId));
            //    foreach (var contactId in contactsToAdd)
            //    {
            //        list.ContactLists.Add(new ContactList { FContact = contactId, FList = list.Id });
            //    }
            //}

            //try
            //{
            //    await _contactContext.SaveChangesAsync();
            //    return Ok();
            //}
            //catch (Exception)
            //{
            //    return StatusCode((int)HttpStatusCode.InternalServerError);
            //}
            #endregion
        }

        /// <summary>
        /// Delete lists
        /// </summary>
        /// <param name="listIds"></param>
        /// <param name="deleteSubLists"></param>
        /// <returns></returns>
        /// <response code="200">List deleted</response>
        /// <response code="400">List with given id could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("list_delete")]
        public async Task<IActionResult> Delete([Required][FromBody]List<int> listIds)
        {
            try
            {
                var successfullyDeleted = new Dictionary<int, string>();
                var unsuccessfullyDeleted = new Dictionary<int, string>();

                foreach (var listId in listIds)
                {
                    var list = await _contactContext.Lists
                        .Include(c => c.ContactLists)
                        .Include(c => c.ChildLists)
                        .SingleOrDefaultAsync(c => c.Id == listId);

                    if (list == null)
                        return BadRequest();

                    if (list.ChildLists.Count == 0 && list.ContactLists.Count == 0)
                    {
                        successfullyDeleted.Add(list.Id, list.ListName);
                        _contactContext.Lists.Remove(list);
                    }
                    else
                    {
                        unsuccessfullyDeleted.Add(list.Id, list.ListName);
                    }
                }

                var dto = new DeleteDTO();
                dto.SuccessfullyDeleted = successfullyDeleted;
                dto.UnsuccessfullyDeleted = unsuccessfullyDeleted;

                await _contactContext.SaveChangesAsync();
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Creates tree for ComboTreePlugin
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private IEnumerable<ComboTreeNode> GetComboTreeNodeList(List<List> collection)
        {
            var treeDictionary = new Dictionary<int?, ComboTreeNode>();

            collection.ForEach(x => treeDictionary.Add(x.Id, new ComboTreeNode { Id = x.Id, ParentId = x.ParentId, Name = x.ListName }));

            foreach (var item in treeDictionary.Values)
            {
                if (item.ParentId != null)
                {
                    ComboTreeNode proposedParent;

                    if (treeDictionary.TryGetValue(item.ParentId, out proposedParent))
                    {
                        item.Parent = proposedParent;
                        proposedParent.Children.Add(item);
                    }
                }
            }
            return treeDictionary.Values.Where(x => x.Parent == null);
        }

        /// <summary>
        /// Creates tree for Jquery TreeTable Plugin
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private IEnumerable<ListTreeNode> GetListTreeNodeList(List<List> collection)
        {
            var treeDictionary = new Dictionary<int?, ListTreeNode>();

            collection.ForEach(x => treeDictionary.Add(x.Id, new ListTreeNode { Id = x.Id, ParentId = x.ParentId, ListName = x.ListName, Description = x.Description, NumberOfContacts = x.ContactLists.Count }));

            foreach (var item in treeDictionary.Values)
            {
                if (item.ParentId != null)
                {
                    ListTreeNode proposedParent;

                    if (treeDictionary.TryGetValue(item.ParentId, out proposedParent))
                    {
                        item.Parent = proposedParent;
                        proposedParent.Children.Add(item);
                    }
                }
            }
            return treeDictionary.Values.Where(x => x.Parent == null);
        }

        /// <summary>
        /// Gets all lists
        /// </summary>
        /// <param name="listIds">String with comma separated ids of lists</param>
        /// <returns>List of lists</returns>
        private async Task<List<List>> GetListsByIdsAsync(string listIds)
        {
            var numIds = listIds.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
                return new List<List>();
            
            var idsToSelect = numIds.Select(id => id.Value);

            var items = await _contactContext.Lists.Include(l => l.ContactLists).Where(l => idsToSelect.Contains(l.Id)).ToListAsync();

            return items;
        }

        /// <summary>
        /// Get contacts
        /// </summary>
        /// <param name="contactIds">String with comma separated ids of contacts</param>
        /// <returns>List of contacts</returns>
        private async Task<List<Models.Contact>> GetContactsByIdsAsync(string contactIds)
        {
            var numIds = contactIds.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
                return new List<Models.Contact>();

            var idsToSelect = numIds.Select(id => id.Value);

            var items = await _contactContext.Contacts.Include(c => c.ContactLists).Where(c => idsToSelect.Contains(c.Id)).ToListAsync();

            return items;
        }


        /// <summary>
        /// Deleted all children that passed list has
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task DeleteSubLists(int id)
        {
            var list = await _contactContext.Lists
                        .Include(l => l.ContactLists)
                        .Include(l => l.ChildLists)
                        .SingleOrDefaultAsync(l => l.Id == id);

            if (list.ChildLists.Count == 0)
                return;

            foreach (List child in list.ChildLists)
            {
                _contactContext.ContactLists.RemoveRange(child.ContactLists);
                _contactContext.Lists.Remove(child);
                await DeleteSubLists(child.Id);
            }
        }

        /// <summary>
        /// Checks if parent is valid (it cannot be a list that updated list already has as its sublist; it cannot be updated list itself)
        /// </summary>
        /// <param name="updatedList"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private bool IsParentValid(List updatedList, int parentId)
        {
            bool valid = true;

            var parent = _contactContext.Lists.SingleOrDefault(l => l.Id == parentId);
            if (parent == null)
                return false;

            if (parent.Id == updatedList.Id)
            {
                valid = false;
                ModelState.AddModelError("ParentId", "List cannot be its own parent");
            }

            // check parent is not a sub of group being edited
            while (parent.ParentId.HasValue)
            {
                parent = _contactContext.Lists.Find(parent.ParentId);
                if (parent.Id == updatedList.Id)
                {
                    ModelState.AddModelError("ParentId", "List cannot have its sublists for parent");
                    valid = false;
                }
            }
            return valid;
        }

    }
}