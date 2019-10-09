using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityModel;
using Identity.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataTablesParser;
using Identity.API.DTO;
using Microsoft.EntityFrameworkCore;
using Identity.Extensions;
using System.Security.Claims;
using SharedLibraries;
using Identity.API.Filters;
using Microsoft.Extensions.Primitives;
using Identity.API.Data;

namespace Identity.API.Quickstart.Account
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")] //COMEBACK
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this._roleManager = roleManager;
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns>All roles</returns>
        [HttpGet]
        [PermissionClaimFilter(Permissions.RoleRead)]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            List<RoleDTO> roles = new List<RoleDTO>();

            try
            {
                roles = await _roleManager.Roles.Select(r =>
                 new RoleDTO()
                 {
                     Id = r.Id,
                     Name = r.Name,
                     Description = r.Description,
                     SystemRole = r.Predefined,
                     RoleActions = _roleManager.GetClaimsAsync(r).Result
                                    .Where(c => c.Type == "permission")
                                    .Select(c => c.Value)
                                    .ToArray()
                 }).ToListAsync();

                return Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="400">Couldn't find any role with given ids</response>
        [HttpGet("{id}")]
        [PermissionClaimFilter(Permissions.RoleRead)]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetRoleById(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                    return BadRequest();
                var dto = new RoleDTO()
                {
                    Description = role.Description,
                    Id = role.Id,
                    Name = role.Name,
                    SystemRole = role.Predefined,
                    RoleActions = (await _roleManager.GetClaimsAsync(role))
                                    .Where(c => c.Type == "permission")
                                    .Select(c => c.Value)
                                    .ToArray()
                };
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// Roles formated for datatable
        /// </summary>
        /// <returns>Datatable formated object with roles</returns>
        [HttpPost]
        [Route("DataTable")]
        [PermissionClaimFilter(Permissions.RoleRead)]
        [ProducesResponseType(typeof(Results<RoleDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetRolesForDataTable(IEnumerable<KeyValuePair<string, string>> datatableParams)
        {
            IQueryable<RoleDTO> roles;

            try
            {
                roles = _roleManager.Roles.Select(r =>
                new RoleDTO()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description == null ? "" : r.Description,
                    SystemRole = r.Predefined
                });

                var keyValues = datatableParams.Select(kv => new KeyValuePair<string, StringValues>(kv.Key, kv.Value));
                return Ok(new Parser<RoleDTO>(keyValues, roles).Parse());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets roles by ids
        /// </summary>
        /// <param name="ids">Array of string ids</param>
        /// <returns>RoleDTOs found by ids</returns>
        /// <response code="400">Couldn't find any role with given ids</response>
        [HttpPost]
        [Route("WithIds")]
        [PermissionClaimFilter(Permissions.RoleRead)]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetRolesWithIds([FromBody]string[] ids = null)
        {
            var ide = HttpContext.User;

            try
            {
                List<RoleDTO> roles;

                // Take by ids
                if (ids != null && ids.Count() > 0)
                {
                    roles = await _roleManager.Roles.Where(r => ids.Any(id => id == r.Id))
                        .Select(r => new RoleDTO()
                        {
                            Name = r.Name,
                            Id = r.Id,
                            Description = r.Description
                        })
                        .ToListAsync();
                    if (!roles.Any())
                        return BadRequest();
                }
                // Take all
                else
                    roles = await _roleManager.Roles.Select(r =>
                        new RoleDTO()
                        {
                            Name = r.Name,
                            Id = r.Id,
                            Description = r.Description
                        })
                        .ToListAsync();

                foreach (var role in roles)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(await _roleManager.FindByIdAsync(role.Id));
                    role.RoleActions = roleClaims.Select(ra => string.Join('-', ra.Value.Split('_'))).ToArray();
                }

                return Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets roles of the user
        /// </summary>
        /// <param name="userId">Id of the user you want to get roles from</param>
        /// <returns>Roles attached to the user</returns>
        /// <response code="400">Couldn't find user with given id</response>
        /// <response code="204">User has no roles</response>
        [HttpGet]
        [Route("RolesOfUser{userId}")]
        [PermissionClaimFilter(Permissions.RoleRead)]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetRolesOfUser(string userId, [FromServices]UserManager<ApplicationUser> userManager)
        {
            List<RoleDTO> roles = new List<RoleDTO>();

            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                    return BadRequest();

                var roleNames = await userManager.GetRolesAsync(user);

                if (roleNames.Count == 0)
                    return NoContent();

                foreach (var role in roleNames)
                {
                    var userRole = await _roleManager.FindByNameAsync(role);
                    roles.Add(new RoleDTO() { Id = userRole.Id, Name = userRole.Name, Description = userRole.Description });
                }

                foreach (var role in roles)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(await _roleManager.FindByIdAsync(role.Id));
                    role.RoleActions = roleClaims.Select(ra => string.Join('-', ra.Value.Split('_'))).ToArray();
                }

                return Ok(roles);
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
        /// <response code="400">Role with id not found</response>
        /// <response code="204">Role has no users in it</response>
        [HttpGet]
        [Route("UsersInRole/{id}")]
        [PermissionClaimFilter(Permissions.RoleRead, Permissions.UserRead)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationUserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUsersInRole(string id, [FromServices]UserManager<ApplicationUser> userManager)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                    return BadRequest();

                var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);

                if (usersInRole.Count() == 0)
                    return NoContent();

                usersInRole
                    .Select(u => new ApplicationUserDTO
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        Id = u.Id,
                        Locked = u.LockoutEnd >= DateTime.Now,
                    });

                return Ok(usersInRole);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new role
        /// </summary>
        /// <returns>Created role id</returns>
        /// <response code="201">Role created</response>
        /// <response code="409">Role with that name already exists</response>
        [HttpPost]
        [PermissionClaimFilter(Permissions.RoleCreate)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateRole(NewRoleDTO model)
        {
            var roleExists = await _roleManager.RoleExistsAsync(model.Name); // Check if role with same name exists
            if (roleExists)
            {
                ModelState.AddModelError("Name", "Role with given name already exists");
                return Conflict(ModelState);
            }

            // check if roleactions are valid
            var allRoleActions = SharedLibraries.Permissions.GetPermissions().Select(a => a.Name);
            if (!allRoleActions.ContainsAllItems(model.RoleActions))
                return BadRequest();

            try
            {
                // create role
                var res = await _roleManager.CreateAsync(new ApplicationRole()
                {
                    Name = model.Name,
                    Description = model.Description
                });
                if (!res.Succeeded)
                    return StatusCode((int)HttpStatusCode.InternalServerError);

                // add claims based on roleactions to role
                var createdRole = await _roleManager.FindByNameAsync(model.Name);
                foreach (var roleAction in model.RoleActions.Distinct())
                {
                    var res2 = await _roleManager.AddClaimAsync(createdRole, new Claim("permission", roleAction));
                    if (res2.Succeeded)
                        continue;
                    else // if error occurs during claim add, delete role and return
                    {
                        await _roleManager.DeleteAsync(createdRole);
                        return StatusCode((int)HttpStatusCode.InternalServerError);
                    }
                }

                return CreatedAtAction(nameof(CreateRole), (await _roleManager.FindByNameAsync(model.Name)).Id);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update role 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Role updated</response>
        /// <response code="400">Couldn't find role or role action with given id</response>
        /// <response code="409">Role with given name already exists</response>
        [HttpPut()]
        [PermissionClaimFilter(Permissions.RoleUpdate)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateRole(UpdateRoleDTO model, [FromServices]ApplicationDbContext dbContext)
        {
            var roleInDb = await dbContext.Roles.AsTracking().SingleOrDefaultAsync(r => r.Id == model.Id);
            if (roleInDb == null)
                return BadRequest();

            if (roleInDb.Name != model.Name && await _roleManager.FindByNameAsync(model.Name) != null)
            {
                ModelState.AddModelError("Name", "Role with given name already exists");
                return Conflict(ModelState);
            }

            roleInDb.Name = model.Name;
            roleInDb.Description = model.Description;

            try
            {
                await dbContext.SaveChangesAsync();

                // check if roleactions are valid
                if(model.RoleActions != null)
                {
                    var allRoleActions = Permissions.GetPermissions().Select(a => a.Name);
                    if(!allRoleActions.ContainsAllItems(model.RoleActions)) // if permissions are passed but not all exist in system
                        return BadRequest();
                }

                var oldActions = (await _roleManager.GetClaimsAsync(roleInDb)).Where(cl=>cl.Type== "permission");
                var newActions = model.RoleActions != null ? model.RoleActions.Distinct().Select(ra=> new Claim("permission", ra)) : new List<Claim>();

                foreach (var actionToRemove in oldActions.Where(oa=> !newActions.Any(na=> na.Value == oa.Value))) // remove old permissions which do not exist in passed permissions
                {
                    var res2 = await _roleManager.RemoveClaimAsync(roleInDb, actionToRemove);
                }

                foreach (var actionToAdd in newActions.Where(na => !oldActions.Any(oa => na.Value == oa.Value))) // add passed permissions which do not exist in old permissions
                {
                    var res2 = await _roleManager.AddClaimAsync(roleInDb, actionToAdd);
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// /// <response code="204">Successfully deleted</response>
        /// <response code="400">Couldn't find role with given id</response>
        [HttpDelete]
        [PermissionClaimFilter(Permissions.RoleDelete)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RemoveRoles([Required][FromBody]string[] ids)
        {
            try
            {
                List<ApplicationRole> rolesToDelete = new List<ApplicationRole>();
                foreach (var id in ids)
                {
                    var roleInDb = await _roleManager.FindByIdAsync(id);
                    if (roleInDb == null)
                        return BadRequest();
                    if (roleInDb.Predefined)
                        if (!User.IsInRole("admin") && !User.HasClaim("permission", Permissions.SystemRoleDelete)) {
                            return Forbid(); // user is not admin and doesn't have permission to delete system roles
                        }
                    rolesToDelete.Add(roleInDb);
                }
                foreach (var role in rolesToDelete)
                {
                    var res = await _roleManager.DeleteAsync(role);
                    if (!res.Succeeded)
                        return StatusCode((int)HttpStatusCode.InternalServerError);
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Adds users to roles
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Added successfully</response>
        /// <response code="400">Some user or role not found</response>
        [HttpPost]
        [Route("AddUsersToRoles")]
        [PermissionClaimFilter(Permissions.AddUserToRole)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddUsersToRoles(UsersRolesIdsDTO model, [FromServices]UserManager<ApplicationUser> userManager)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var userId in model.UserIds)
            {
                var userInDb = await userManager.FindByIdAsync(userId);
                if (userInDb == null)
                    return BadRequest();
                users.Add(userInDb);
            }
            List<ApplicationRole> roles = new List<ApplicationRole>();
            foreach (var roleId in model.RoleIds)
            {
                var roleInDb = await _roleManager.FindByIdAsync(roleId);
                if (roleInDb == null)
                    return BadRequest();
                roles.Add(roleInDb);
            }

            try
            {
                foreach (var user in users) // add each user to each role
                {
                    foreach (var role in roles)
                    {
                        await userManager.AddToRoleAsync(user, role.Name);
                    }
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Remove users from roles
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// /// <response code="200">Removed successfully</response>
        /// <response code="400">Some user or role not found</response>
        [HttpPost]
        [Route("RemoveUsersFromRoles")]
        [PermissionClaimFilter(Permissions.RemoveUserFromRole)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RemoveUsersFromRoles(UsersRolesIdsDTO model, [FromServices]UserManager<ApplicationUser> userManager)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var userId in model.UserIds)
            {
                var userInDb = await userManager.FindByIdAsync(userId);
                if (userInDb == null)
                    return BadRequest();
                users.Add(userInDb);
            }
            List<ApplicationRole> roles = new List<ApplicationRole>();
            foreach (var roleId in model.RoleIds)
            {
                var roleInDb = await _roleManager.FindByIdAsync(roleId);
                if (roleInDb == null)
                    return BadRequest();
                roles.Add(roleInDb);
            }

            try
            {
                foreach (var user in users) // add each user to each role
                {
                    foreach (var role in roles)
                    {
                        await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets actions with their names, on which the action system is based by including these names in the access_token claims, which are checked upon trying to execute action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RoleActions")]
        [PermissionClaimFilter(Permissions.RoleRead)]
        [ProducesResponseType(typeof(IEnumerable<SharedLibraries.Permission>), (int)HttpStatusCode.OK)]
        public IActionResult GetRoleActions()
        {
            return base.Ok(SharedLibraries.Permissions.GetPermissions());
        }
    }
}