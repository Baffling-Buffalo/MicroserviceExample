using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Form.API.DTO;
using Form.API.Extensions;
using Form.API.Filter;
using Form.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Form.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class GroupController : ControllerBase
    {
        private readonly FormContext _formContext;

        public GroupController(FormContext _formContext)
        {
            this._formContext = _formContext;
        }

        /// <summary>
        /// Get group with passed id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetGroupDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("formGroup_read")]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var group = await _formContext.Groups
                    .Include(ig => ig.OzItemGroups)
                    .Where(g => g.Id == id)
                    .Select(g => new GetGroupDTO()
                    {
                        Id = g.Id,
                        GroupName = g.GroupName,
                        Description = g.Description,
                        ParentId = g.ParentId,
                        ParentName = g.Parent.GroupName,
                        Forms = g.OzItemGroups.Select(ig => new { id = ig.FOzItemId, name = ig.FOzItem.Name }).ToDictionary(x => x.id, x => x.name),
                        ChildGroups = g.ChildGroups.Select(c => new { id = c.Id, name = c.GroupName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .SingleOrDefaultAsync();

                if (group == null)
                    return BadRequest();

                return Ok(group);
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
        [PermissionClaimFilter("formGroup_read")]
        public async Task<IActionResult> GetGroupsForComboTreePlugin([FromQuery] string groupIds = null)
        {
            List<Group> groups;

            try
            {
                if (!string.IsNullOrEmpty(groupIds))
                {
                    groups = await GetGroupsByIdsAsync(groupIds);

                    if (!groups.Any())
                        return BadRequest();
                }
                else
                {
                    groups = await _formContext.Groups.ToListAsync();
                }

                var tree = GetComboTreeNodeList(groups);

                return Ok(tree);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get groups for tree table
        /// </summary>
        /// <returns>comboTreePlugin formatted json with lists</returns>
        /// <response code="200">json returned successfully</response>
        /// <response code="400">Passed list of ids is not valid. Must be comma-separated list of numbers</response>
        /// <response code="500">Exeption while selecting data from _contactContext</response>
        [HttpGet]
        [Route("TreeTable")]
        [ProducesResponseType(typeof(IEnumerable<GroupTreeNode>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("formGroup_read")]
        public async Task<IActionResult> GetGroupsForTreeTable()
        {
            List<Group> groups;

            try
            {
                groups = await _formContext.Groups.Include(g=>g.OzItemGroups).ToListAsync();

                var tree = GetGroupTree(groups);

                return Ok(tree);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Group created</response>
        /// <response code="400"></response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("formGroup_create")]
        public async Task<IActionResult> Create(CreateGroupDTO model)
        {
            if (model.ParentId.HasValue)
            {
                var parentGroupExists = _formContext.Groups.Any(g => g.Id == model.ParentId);
                if (!parentGroupExists)
                    return BadRequest();
            }

            var groupExists = _formContext.Groups.Any(g => g.GroupName == model.GroupName);
            if (groupExists)
                ModelState.AddModelError("GroupName", "Group with given name already exists");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = new Group()
            {
                GroupName = model.GroupName,
                Description = model.Description,
                ParentId = model.ParentId
            };

            try
            {
                await _formContext.Groups.AddAsync(group);
                await _formContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Group updated</response>
        /// <response code="400"></response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("formGroup_update")]
        public async Task<IActionResult> Update(UpdateGroupDTO model)
        {
            var group = await _formContext.Groups.FindAsync(model.Id);
            if (group == null)
                return BadRequest();

            if (model.ParentId.HasValue)
            {
                if (!IsParentValid(group, model.ParentId.Value))
                    return BadRequest(ModelState);
            }

            var groupExists = await _formContext.Groups.AnyAsync(g => g.GroupName == model.GroupName && g.Id != model.Id);
            if (groupExists)
                ModelState.AddModelError("GroupName", "Group with given name already exists");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            group.GroupName = model.GroupName;
            group.Description = model.Description;
            group.ParentId = model.ParentId;

            try
            {
                _formContext.Groups.Update(group);
                await _formContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete groups
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        /// <response code="200">Groups deleted</response>
        /// <response code="400">Group with given id could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [PermissionClaimFilter("formGroup_delete")]
        public async Task<IActionResult> Delete([FromBody]List<int> groupIds)
        {
            try
            {
                var successfullyDeleted = new Dictionary<int, string>();
                var unsuccessfullyDeleted = new Dictionary<int, string>();

                foreach (var groupId in groupIds)
                {
                    var group = await _formContext.Groups.Include(c => c.ChildGroups).Include(ig => ig.OzItemGroups).SingleOrDefaultAsync(g => g.Id == groupId);

                    if (group == null)
                        return BadRequest();

                    if (group.ChildGroups.Count == 0 && group.OzItemGroups.Count == 0)
                    {
                        successfullyDeleted.Add(group.Id, group.GroupName);
                        _formContext.Groups.Remove(group);
                    }
                    else
                    {
                        unsuccessfullyDeleted.Add(group.Id, group.GroupName);
                    }
                }

                var dto = new DeleteDTO();
                dto.SuccessfullyDeleted = successfullyDeleted;
                dto.UnsuccessfullyDeleted = unsuccessfullyDeleted;

                await _formContext.SaveChangesAsync();
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets all lists
        /// </summary>
        /// <param name="groupsIds">String with comma separated ids of groups</param>
        /// <returns>List of lists</returns>
        private async Task<List<Group>> GetGroupsByIdsAsync(string groupsIds)
        {
            var numIds = groupsIds.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
                return new List<Group>();

            var idsToSelect = numIds.Select(id => id.Value);

            var items = await _formContext.Groups.Include(g => g.OzItemGroups).Where(g => idsToSelect.Contains(g.Id)).ToListAsync();

            return items;
        }

        /// <summary>
        /// Creates tree for ComboTreePlugin
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private IEnumerable<ComboTreeNode> GetComboTreeNodeList(List<Group> collection)
        {
            var treeDictionary = new Dictionary<int?, ComboTreeNode>();

            collection.ForEach(x => treeDictionary.Add(x.Id, new ComboTreeNode { Id = x.Id, ParentId = x.ParentId, Name = x.GroupName }));

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
        private IEnumerable<GroupTreeNode> GetGroupTree(List<Group> collection)
        {
            var treeDictionary = new Dictionary<int?, GroupTreeNode>();

            collection.ForEach(x => treeDictionary.Add(x.Id, new GroupTreeNode { Id = x.Id, ParentId = x.ParentId, GroupName = x.GroupName, Description = x.Description, NumberOfForms = x.OzItemGroups.Count }));

            foreach (var item in treeDictionary.Values)
            {
                if (item.ParentId != null)
                {
                    GroupTreeNode proposedParent;

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
        /// Checks if parent is valid (it cannot be a group that updated group already has as its subgroup; it cannot be updated group itself)
        /// </summary>
        /// <param name="updatedGroup"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private bool IsParentValid(Group updatedGroup, int parentId)
        {
            bool valid = true;

            var parentGroup = _formContext.Groups.SingleOrDefault(g => g.Id == parentId);
            if (parentGroup == null)
                return false;

            if (parentGroup.Id == updatedGroup.Id)
            {
                valid = false;
                ModelState.AddModelError("ParentId", "Group cannot be its own parent");
            }

            // check parent is not a sub of group being edited
            while (parentGroup.ParentId.HasValue)
            {
                parentGroup = _formContext.Groups.Find(parentGroup.ParentId);
                if (parentGroup.Id == updatedGroup.Id)
                {
                    ModelState.AddModelError("ParentId", "Group cannot have its subgroups for parent");
                    valid = false;
                }
            }
            return valid;
        }
    }
}