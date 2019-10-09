using Audit.Core;
using Audit.EntityFramework;
using DataTablesParser;
using Identity.API.Data;
using Identity.API.DTO;
using Identity.API.Infrastructure;
using Identity.API.Models;
using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public GroupController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        { 
            this.db = db;
        }

        /// <summary>
        /// Get all user groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetGroups()
        {
            try
            {
                var groups = db.Groups.Select(gr =>
                    new GroupDTO()
                    {
                        Name = gr.Name,
                        Description = gr.Description,
                        Id = gr.GroupId,
                        ParentGroupId = gr.ParentId,
                        ChildGroupCount = gr.ChildGroups.Count
                    }
                );

                return Ok(groups);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get group with specific id
        /// </summary>
        /// <param name="id">Group id</param>
        /// <returns></returns>
        /// <response code="400">Group with id not found</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GroupDetailsDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var group = await db.Groups.Include(g=>g.ChildGroups).Include(g=>g.Parent).FirstOrDefaultAsync(g=>g.GroupId==id);

                if (group == null)
                    return BadRequest();

                var dto = new GroupDetailsDTO()
                {
                    Id = group.GroupId,
                    Name = group.Name,
                    Description = group.Description,
                    ParentGroup = new GroupDTO(){
                        Name = group.Parent?.Name,
                        Description = group.Parent?.Description,
                        Id = (group.Parent?.GroupId).GetValueOrDefault()
                    },
                    ChildGroups = group.ChildGroups.Select(cg=> new GroupDTO()
                    {
                        Name = cg.Name,
                        Description = cg.Description,
                        Id = cg.GroupId
                    }).ToList()
                };

                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets groups formatted for combotree plugin
        /// </summary>
        /// <param name="ids">Ids of groups you want to get</param>
        /// <returns></returns>
        /// <response code="400">Group with id not found</response>
        [HttpGet]
        [Route("ComboTreePlugin")]
        [ProducesResponseType(typeof(IEnumerable<ComboTreeNodeDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetGroupsForComboTree([FromQuery]string ids = null)
        {
            try
            {
                IQueryable<Group> groupQuery = null;

                if (!string.IsNullOrEmpty(ids)) // Specific groups by ids
                {
                    groupQuery = GetGroupsByIds(ids);
                    if (!groupQuery.Any())
                    {
                        return BadRequest("ids value invalid. Must be comma-separated list of numbers");
                    }
                }
                else // ALl groups
                {
                    groupQuery = db.Groups;
                }
                
                var groups = await groupQuery.Select(g => new Group()
                {
                    GroupId = g.GroupId,
                    Name = g.Name,
                    ParentId = g.ParentId
                }).ToListAsync();

                var groupsTree = RawCollectionToTree(groups);

                return Ok(groupsTree);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get groups formatted for datatable
        /// </summary>
        /// <param name="parentGroupId">Id of group whos children you want to get</param>
        /// <returns></returns>
        /// <response code="400">Group with id not found</response>
        [HttpPost]
        [Route("Datatable")]
        [ProducesResponseType(typeof(Results<GroupDatatableDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetGroupsForDatatable(IEnumerable<KeyValuePair<string,string>> datatableParams, int parentGroupId = 0)
        {
            try
            {
                var query = from g in db.Groups
                            select g;

                if (parentGroupId != 0)
                {
                    if (db.Groups.Find(parentGroupId) == null)
                        return BadRequest();
                    query = query.Where(g => g.Parent.GroupId == parentGroupId);
                }

                var query2 = query.Select(g => new GroupDatatableDTO
                {
                    Description = g.Description == null ? "" : g.Description,
                    Id = g.GroupId,
                    Name = g.Name,
                    ParentGroupName = g.Parent.Name == null ? "" : g.Parent.Name,
                    ChildGroupCount = g.ChildGroups.Count
                });

                var keyValues = datatableParams.Select(kv => new KeyValuePair<string, StringValues>(kv.Key, kv.Value));
                var parser = new Parser<GroupDatatableDTO>(keyValues, query2);

                return Ok(parser.Parse());

            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets groups for tree table plugin
        /// </summary>
        /// <param name="ids">Group ids</param>
        /// <returns></returns>
        /// <response code="400">Group with id not found</response>
        [HttpGet]
        [Route("TreeTable")]
        [ProducesResponseType(typeof(IEnumerable<GroupTreeTableNodeDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetGroupsForTreeTable([FromQuery] string ids = null)
        {
            IQueryable<Group> groups;

            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    groups = GetGroupsByIds(ids);

                    if (!groups.Any())
                        return BadRequest();
                }
                else
                {
                    groups = db.Groups.Include(g=>g.UserGroups);
                }

                var tree = GetTreeTableNodes(await groups.ToListAsync());

                return Ok(tree);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get child groups of a group
        /// </summary>
        /// <param name="id">Parent group id</param>
        /// <returns></returns>
        /// <response code="400">Group with id not found</response>
        /// <response code="204">Group has no child groups</response>
        [HttpGet]
        [Route("ChildGroups/{id}")]
        [ProducesResponseType(typeof(IEnumerable<GroupDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetChildGroups(int id)
        {
            try
            {
                var group = await db.Groups.FindAsync(id);

                if (group == null)
                    return BadRequest();

                var childGroups = db.Groups
                    .Where(g => g.ParentId == id)
                    .Select(g => new GroupDTO()
                    {
                        Name = g.Name,
                        Description = g.Description,
                        Id = g.GroupId,
                        ParentGroupId = g.ParentId,
                        ChildGroupCount = g.ChildGroups.Count
                    });

                return Ok(childGroups);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get users in group
        /// </summary>
        /// <param name="id">Group id</param>
        /// <returns></returns>
        /// <response code="400">Group with id not found</response>
        /// <response code="204">Group has no users in it</response>
        [HttpGet]
        [Route("UsersInGroup/{id}")]
        [ProducesResponseType(typeof(IEnumerable<ApplicationUserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUsersInGroup(int id)
        {
            try
            {
                var group = await db.Groups
                    .Include(g => g.UserGroups)
                    .ThenInclude(ug => ug.User)
                    .SingleOrDefaultAsync(g => g.GroupId == id);

                if (group == null)
                    return BadRequest();

                if (group.UserGroups.Count() == 0)
                    return NoContent();

                var usersInGroup = group
                    .UserGroups
                    .Select(ug => new ApplicationUserDTO
                    {
                        FirstName = ug.User.FirstName,
                        LastName = ug.User.LastName,
                        UserName = ug.User.UserName,
                        Email = ug.User.Email,
                        PhoneNumber = ug.User.PhoneNumber,
                        Id = ug.User.Id,
                        Locked = ug.User.LockoutEnd >= DateTime.Now
                    });
                
                return Ok(usersInGroup);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get groups user is in
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        /// <response code="400">User with id not found</response>
        /// <response code="204">User isn't in any group</response>
        [HttpPost]
        [Route("GroupsOfUsers")]
        [ProducesResponseType(typeof(IEnumerable<GroupDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetGroupsOfUser([FromBody]string[] ids, bool common = false)
        {
            var usersGroups = await db.Users
                .Include(u => u.UserGroups)
                .Where(u => ids.Contains(u.Id))
                .Select(u => u.UserGroups.Select(ug => ug.GroupId))
                .ToArrayAsync();

            HashSet<int> commonGroupIds;
            if (usersGroups.Count() > 1)
            {
                commonGroupIds = usersGroups
                    .Skip(1)
                    .Aggregate(
                        new HashSet<int>(usersGroups.First()),
                        (h, e) => { h.IntersectWith(e); return h; }
                    );
            }
            else
            {
                commonGroupIds = usersGroups.First().ToHashSet();
            }

            var commonGroups = await db.Groups.Where(g => commonGroupIds.Contains(g.GroupId))
                .ToListAsync();

            return Ok(commonGroups);
        }

        /// <summary>
        /// Create new group
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Created group Id</returns>
        /// <response code="400">Parent group not found</response>
        /// <response code="201">Group created - returns group id</response>
        [HttpPost()]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateGroup(NewGroupDTO model)
        {
            try
            {
                if (model.ParentGroupId.HasValue && db.Groups.Find(model.ParentGroupId.Value) == null)  // Parent group with id not found
                {
                    return BadRequest();
                }

                var groupWithNameExists = await db.Groups.AnyAsync(gr => gr.Name == model.Name);
                if (groupWithNameExists)
                {
                    ModelState.AddModelError("Name", "Group with given name already exists");
                    return Conflict(ModelState);
                }

                var createdGroup = await db.Groups.AddAsync(new Group()
                {
                    Name = model.Name,
                    Description = model.Description,
                    ParentId = model.ParentGroupId,                    
                });

                await db.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateGroup), createdGroup.Entity.GroupId);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Adds users to groups
        /// </summary>
        /// <param name="model">User ids and Group ids</param>
        /// <returns></returns>
        /// <response code="400">User or group not found</response>
        /// <response code="200">Users added to groups</response>
        [HttpPost()]
        [Route("AddUsersToGroups")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddUsersToGroups(UsersGroupsIdsDTO model, [FromServices] IAuditLogService auditLogger)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var userId in model.UserIds)
            {
                var userInDb = await db.Users.FindAsync(userId);
                if (userInDb == null)
                    return BadRequest();
                users.Add(userInDb);
            }
            List<Group> groups = new List<Group>();
            foreach (var groupId in model.GroupIds)
            {
                var groupInDb = await db.Groups.FindAsync(groupId);
                if (groupInDb == null)
                    return BadRequest();
                groups.Add(groupInDb);
            }

            try
            {
                db.AuditDisabled = true;
                foreach (var user in users) // add each user to each group
                {
                    foreach (var group in groups)
                    {
                        if (!await db.UserGroups.AnyAsync(ug => ug.UserId == user.Id && ug.GroupId == group.GroupId)) // if not already in group
                        {
                            await auditLogger.CreateAsync($"Added user: {user.UserName} to group: {group.Name}", "Adding users to groups", "User"); // log for each change
                            //await auditLogger.CreateAsync($"Added user: {user.UserName} to group: {group.Name}", "Adding users to groups", "User"); // log for each change
                            await db.UserGroups.AddAsync(new UserGroup { UserId = user.Id, GroupId = group.GroupId });
                        }
                    }
                }
                await db.SaveChangesAsync();
                await auditLogger.SaveAsync(); // save all logs after successful save
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Adds users to groups
        /// </summary>
        /// <param name="model">User ids and Group ids</param>
        /// <returns></returns>
        /// <response code="400">User or group not found</response>
        /// <response code="200">Users added to groups</response>
        [HttpPost()]
        [Route("RemoveUsersFromGroups")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RemoveUsersFromGroups(UsersGroupsIdsDTO model, [FromServices] IAuditLogService auditLogger)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var userId in model.UserIds)
            {
                var userInDb = await db.Users.FindAsync(userId);
                if (userInDb == null)
                    return BadRequest();
                users.Add(userInDb);
            }
            List<Group> groups = new List<Group>();
            foreach (var groupId in model.GroupIds)
            {
                var groupInDb = await db.Groups.FindAsync(groupId);
                if (groupInDb == null)
                    return BadRequest();
                groups.Add(groupInDb);
            }

            try
            {
                db.AuditDisabled = true; // doing custom logging
                foreach (var user in users) // remove each user from each group
                {
                    foreach (var group in groups)
                    {
                        await auditLogger.CreateAsync($"Removed user: {user.UserName} from group: {group.Name}", "Removing users from groups", "User"); // log for each change

                        var userGroup = await db.UserGroups.FirstOrDefaultAsync(ug => ug.UserId == user.Id && ug.GroupId == group.GroupId);
                        if (userGroup != null)
                            db.UserGroups.Remove(userGroup);
                    }
                }

                await db.SaveChangesAsync();
                await auditLogger.SaveAsync(); // save all logs after successful save

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Edit group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// /// <response code="200">Group updated</response>
        /// <response code="400">Couldn't find group with given id</response>
        /// <response code="409">Group with given name already exists</response>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EditGroup(UpdateGroupDTO model)
        {
            try
            {
                var groupInDb = await db.Groups.AsTracking().SingleOrDefaultAsync(g => g.GroupId == model.Id);
                if (groupInDb == null)
                    return BadRequest();

                if (model.ParentGroupId.HasValue) // Parent group is passed
                {
                    if (!IsParentValid(groupInDb, model.ParentGroupId.Value))
                        return BadRequest(ModelState);
                }

                var groupWithNameExists = await db.Groups.AnyAsync(gr => gr.Name == model.Name && gr.GroupId != model.Id);
                if (groupWithNameExists)
                {
                    ModelState.AddModelError("Name", "Group with given name already exists");
                    return Conflict(ModelState);
                }

                groupInDb.Name = model.Name;
                groupInDb.ParentId = model.ParentGroupId; 
                groupInDb.Description = model.Description;


                await db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete Groups
        /// </summary>
        /// <param name="ids">Ids of groups to be deleted</param>
        /// <returns></returns>
        /// /// <response code="204">Successfully deleted</response>
        /// <response code="400">Couldn't find group with given id</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RemoveGroups([Required][FromBody]int[] ids, [FromQuery]bool deleteSubGroups = false)
        {
            try
            {
                foreach (var id in ids)
                {
                    var groupInDb = await db.Groups
                        .Include(g=>g.ChildGroups)
                        .SingleOrDefaultAsync(g=>g.GroupId == id);
                    if (groupInDb == null)
                        return BadRequest();

                    if(deleteSubGroups) // delete all groups nested in this group
                    {
                        await RemoveSubGroups(groupInDb.GroupId);
                    }
                    else // Set parent id of child groups to parent of this group
                    {
                        foreach (var childGroup in groupInDb.ChildGroups)
                        {
                            childGroup.ParentId = groupInDb.ParentId;
                        }
                    }

                    db.Groups.Remove(groupInDb);
                }

                await db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        private async Task RemoveSubGroups(int id)
        {
            var groupInDb = await db.Groups
                        .Include(g => g.ChildGroups)
                        .SingleOrDefaultAsync(g => g.GroupId == id);

            if (groupInDb.ChildGroups.Count == 0)
                return;

            foreach (var childGroup in groupInDb.ChildGroups)
            {
                db.Remove(childGroup);
                await RemoveSubGroups(childGroup.GroupId);
            }
        }

        /// <summary>
        /// Checks if parent exist, is not same as edited group and if parent is not sub of the group being edited. Sets modelstate errors
        /// </summary>
        /// <param name="editedGroup"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private bool IsParentValid(Group editedGroup, int parentId)
        {
            var parent = db.Groups.Find(parentId);
            if (parent == null)
                return false;

            if (parent.GroupId == editedGroup.GroupId)
            { // Can't be self parent
                ModelState.AddModelError("ParentGroupId", "This group can't be parent to itself");
                return false;
            }

            // check parent is not a sub of group being edited
            while(parent.ParentId.HasValue)
            {
                parent = db.Groups.Find(parent.ParentId);
                if(parent.GroupId == editedGroup.GroupId)
                {
                    ModelState.AddModelError("ParentGroupId", "Child groups of this group can't be it's parent");
                    return false;
                }
            }

            return true;
        }

        private IQueryable<Group> GetGroupsByIds(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return null;
            }

            var idsToSelect = numIds
                .Select(id => id.Value);

            var items = db.Groups
                .Where(g => idsToSelect.Contains(g.GroupId));

            return items;
        }

        private IEnumerable<ComboTreeNodeDTO> RawCollectionToTree(List<Group> collection)
        {
            var treeDictionary = new Dictionary<int?, ComboTreeNodeDTO>();

            collection.ForEach(x => treeDictionary.Add(x.GroupId, new ComboTreeNodeDTO { Id = x.GroupId.ToString(), ParentId = x.ParentId, Name = x.Name }));

            foreach (var item in treeDictionary.Values)
            {
                if (item.ParentId != null)
                {
                    ComboTreeNodeDTO proposedParent;

                    if (treeDictionary.TryGetValue(item.ParentId, out proposedParent))
                    {
                        item.Parent = proposedParent;
                        proposedParent.Children.Add(item);
                    }
                }
            }
            return treeDictionary.Values.Where(x => x.Parent == null);
        }

        private IEnumerable<GroupTreeTableNodeDTO> GetTreeTableNodes(List<Group> collection)
        {
            var treeDictionary = new Dictionary<int?, GroupTreeTableNodeDTO>();

            collection.ForEach(x => treeDictionary.Add(x.GroupId, new GroupTreeTableNodeDTO { Id = x.GroupId, ParentId = x.ParentId, Name = x.Name, Description = x.Description, NumberOfUsers = x.UserGroups.Count }));

            foreach (var item in treeDictionary.Values)
            {
                if (item.ParentId != null)
                {
                    GroupTreeTableNodeDTO proposedParent;

                    if (treeDictionary.TryGetValue(item.ParentId, out proposedParent))
                    {
                        item.Parent = proposedParent;
                        proposedParent.Children.Add(item);
                    }
                }
            }
            return treeDictionary.Values.Where(x => x.Parent == null);
        }
    }
}
