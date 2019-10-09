using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataTablesParser;
using Identity.API.Models;
using Identity.API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Identity.API.Data;
using Identity.Extensions;
using Microsoft.Extensions.Primitives;
using Identity.API.Filters;
using Microsoft.AspNetCore.Authorization;
using SharedLibraries;
using Identity.API.Services;
using Identity.API.Services.ModelDTOs;
using Serilog;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext dbContext;

        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            this._userManager = userManager;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets all users (active and/or locked)
        /// </summary>
        /// <returns>List of ApplicationUsers</returns>
        /// <param name="lockedUsers">True - get only locked users, False - get only active users and (default)Null - get all</param>
        /// <param name="groupIds">Ids of groups from which you want to get users</param>
        /// <param name="roleIds">Ids of roles from which you want to get users</param>
        [HttpGet]
        [PermissionClaimFilter(Permissions.UserRead)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationUserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromQuery]bool? lockedUsers = null, [FromQuery]int[] groupIds = null, [FromQuery]string[] roleIds = null, [FromQuery]string searchQuery = null)
        {
            try
            {
                var users = dbContext.Users
                    .Select(u=>u);

                if(lockedUsers != null)
                {
                    users = lockedUsers.Value ?
                        users.Where(user => user.LockoutEnd > DateTime.Now) // locked users
                        : users.Where(user => !user.LockoutEnd.HasValue || user.LockoutEnd <= DateTime.Now); // active users
                }

                if (groupIds != null && groupIds.Count() != 0)
                {
                    users = users.Include(u=>u.UserGroups).Where(u => !groupIds.Except(u.UserGroups.Select(ug => ug.GroupId)).Any()); // check if users contain all passed groups
                }

                if (roleIds != null && roleIds.Count() != 0)
                {
                    users = users.Where(u => dbContext.UserRoles.
                        Where(ur=>ur.UserId == u.Id).
                        Select(ur=> ur.RoleId).
                        ContainsAllItems(roleIds)
                    );
                }
               
                if (searchQuery != null && !string.IsNullOrWhiteSpace(searchQuery))
                {
                    users = users.Where(u => u.UserName.Contains(searchQuery) || u.FullName.Contains(searchQuery));
                }

                var userDTOs = users.Select(u => new ApplicationUserDTO
                {
                    FullName = u.FullName == null ? "" : u.FullName,
                    UserName = u.UserName,
                    Email = u.Email == null ? "" : u.Email,
                    PhoneNumber = u.PhoneNumber == null ? "" : u.PhoneNumber,
                    Id = u.Id,
                    //Locked = u.LockoutEnd == null ? false : u.LockoutEnd > DateTime.Now,  // TODO
                    ContactId = u.ContactId == null ? -1 : u.ContactId,
                    CreationDate = u.CreationDate
                });

                return Ok(await userDTOs.ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets users sorted and formated for datatable
        /// </summary>
        /// <returns></returns>
        /// <param name="model">Model with parameters</param>
        [HttpPost]
        [PermissionClaimFilter(Permissions.UserRead)]
        [Route("Datatable")]
        [ProducesResponseType(typeof(Results<ApplicationUserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetForDatatable([FromBody]UsersForDatatableDTO model)
        {
            try
            {
                var query = from u in dbContext.Users
                            select u;

                if (model.Locked != null)
                    if (model.Locked.Value)
                        query = query.Where(u => u.LockoutEnd > DateTime.Now);
                    else
                        query = query.Where(u => (u.LockoutEnd==null || u.LockoutEnd <= DateTime.Now));

                if (model.GroupIds != null && model.GroupIds.Any())
                    query = query.Where(u => u.UserGroups.Select(ug => ug.GroupId)
                                                         .ContainsAllItems(model.GroupIds));

                if (model.RoleIds != null && model.RoleIds.Any())
                    query = query.Where(u => dbContext.UserRoles.Where(ur => ur.UserId == u.Id)
                                                                .Select(ur => ur.RoleId)
                                                                .ContainsAllItems(model.RoleIds));

                var query2 = query.Select(u => new ApplicationUserDTO
                {
                    Email = u.Email == null ? "" : u.Email,
                    Id = u.Id,
                    UserName = u.UserName,
                    PhoneNumber = u.PhoneNumber == null ? "" : u.PhoneNumber,
                    FullName = u.FullName == null ? "" : u.FullName,
                    CreationDate = u.CreationDate
                });

                var keyValues = model.DtParameters.Select(kv => new KeyValuePair<string, StringValues>(kv.Key, new StringValues(kv.Value)));

                return Ok(new Parser<ApplicationUserDTO>(keyValues, query2).Parse());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets user with passed id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>ApplicationUser</returns>
        /// <response code="400">User with passed id not found</response>
        [HttpGet]
        [PermissionClaimFilter(Permissions.UserRead)]
        [Route("{id}")]
        [ProducesResponseType(typeof(ApplicationUser), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                    return BadRequest();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets users with passed ids
        /// </summary>
        /// <param name="ids">User ids</param>
        /// <returns>ApplicationUsers</returns>
        /// <response code="400">Users with passed ids not found</response>
        [HttpPost]
        [PermissionClaimFilter(Permissions.UserRead)]
        [Route("WithIds")]
        [ProducesResponseType(typeof(IEnumerable<ApplicationUser>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUsersWithIds([Required][FromBody]string[] ids)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            try
            {
                foreach (var id in ids)
                {
                    var user = await _userManager.FindByIdAsync(id);

                    if (user == null)
                        return BadRequest();

                    users.Add(user);
                }
                

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="model">NewApplicationUserDTO</param>
        /// <returns>Created user Id</returns>
        /// <response code="201">User created</response>
        /// <response code="400">User with that name or email already exists</response>
        /// <response code="409">Username/Email is already in use</response>
        [HttpPost]
        [PermissionClaimFilter(Permissions.UserCreate)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateUser([FromServices] IContactService contactService, NewApplicationUserDTO model)
        {
            // check if new properties are unique
            if (await IsUnique(model.UserName, model.Email) == false)
                return BadRequest(ModelState);

            try
            {
                int? contactId = null;
                // CREATING NEW CONTACT FOR THE USER
                if (model.ContactAssign?.ToLower() == "new")
                {
                    var id = await contactService.CreateContact(new NewContactDTO()
                    {
                        FirstName = model.NewContactFirstName,
                        LastName = model.NewContactLastName,
                        Email = model.NewContactEmail,
                        Phone = model.NewContactPhone
                    });

                    if (id < 1) // id invalid,operation didn't succeed
                    {
                        contactService.Validate(ModelState);
                        if (!ModelState.IsValid) // there are model errors for creating contacts
                            return BadRequest(ModelState);
                        else
                            return BadRequest();
                    }

                    contactId = id;
                }

                if (model.ContactAssign?.ToLower() == "existing")
                    contactId = model.ExistingContactId;

                // add user prefix based on application
                var username = model.AdministrationUser ? "admin_" + model.UserName : model.ContactUser ? "contact_" + model.UserName : model.UserName;

                // CREATE USER
                var res = await _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = username, 
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreationDate = DateTime.Now,
                    LockoutEnd = model.Active ? DateTime.Now : DateTime.Now.AddYears(100),
                    ContactId = contactId,
                    ContactUser = model.ContactUser,
                    AdministrationUser = model.AdministrationUser
                });

                if (!res.Succeeded) // failed to create user 
                {
                    if (model.ContactAssign?.ToLower() == "new") // rollback contact create
                        await contactService.DeleteContacts(new List<int>() { contactId.Value });
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }

                var user = await _userManager.FindByNameAsync(username);

                // ADD PASSWORD TO USER
                res = await _userManager.AddPasswordAsync(user, model.Password);
                if (!res.Succeeded)
                {
                    if (model.ContactAssign?.ToLower() == "new") // rollback contact create
                        await contactService.DeleteContacts(new List<int>() { contactId.Value });
                    await _userManager.DeleteAsync(user);
                    ModelState.AddModelError("Password", string.Join(", " , res.Errors.Select(e=>e.Description)));
                    return BadRequest(ModelState);
                }

                return CreatedAtAction(nameof(CreateUser), user.Id);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="model">UpdateApplicationUserDTO</param>
        /// <returns></returns>
        /// <response code="204">User updated</response>
        /// <response code="400">Couldn't find user with given id</response>
        /// <response code="409">New Username/Email is already in use</response>
        [HttpPut()]
        [PermissionClaimFilter(Permissions.UserUpdate)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromServices] IContactService contactService, [FromServices]ApplicationDbContext dbContext, UpdateApplicationUserDTO model)
        {
            //var userInDb = await _userManager.FindByIdAsync(model.Id);
            var userInDb = await dbContext.Users.AsTracking().SingleOrDefaultAsync(u => u.Id == model.Id);
            if (userInDb == null)
                return BadRequest();

            
            if (await IsUnique(username: userInDb.UserName == model.UserName ? "" : model.UserName, 
                               email: userInDb.Email == model.Email ? "" : model.Email)
                               == false)
                return BadRequest(ModelState);



            int? contactId = null;
            // CREATING NEW CONTACT FOR THE USER
            if (model.ContactAssign?.ToLower() == "new")
            {
                var id = await contactService.CreateContact(new NewContactDTO()
                {
                    FirstName = model.NewContactFirstName,
                    LastName = model.NewContactLastName,
                    Email = model.NewContactEmail,
                    Phone = model.NewContactPhone
                });

                if (id < 1) // id invalid,operation didn't succeed
                {
                    contactService.Validate(ModelState);
                    if (!ModelState.IsValid) // there are model errors for creating contacts
                        return BadRequest(ModelState);
                    else
                        return BadRequest();
                }

                contactId = id;
            }
            if (model.ContactAssign?.ToLower() == "existing")
                contactId = model.ExistingContactId;


            // UPDATE PROPERTIES
            userInDb.UserName = model.UserName;
            userInDb.Email = model.Email;
            userInDb.FirstName = model.FirstName;
            userInDb.LastName = model.LastName;
            userInDb.PhoneNumber = model.PhoneNumber;
            if (model.Active)
            {
                if (userInDb.LockoutEnd.HasValue)
                    userInDb.LockoutEnd = null;
            }
            else
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
            userInDb.ContactId = contactId;

            try
            {
                var res = await dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                if (model.ContactAssign.ToLower() == "new") // rollback contact create
                    await contactService.DeleteContacts(new List<int>() { contactId.Value });
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="ids">User Ids</param>
        /// <returns></returns>
        /// /// <response code="204">Successfully deleted user</response>
        /// <response code="400">Couldn't find user with given id</response>
        [HttpDelete]
        [PermissionClaimFilter(Permissions.UserDelete)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteUsers([Required][FromBody]string[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var userInDb = await dbContext.Users.FindAsync(id);
                    if (userInDb == null)
                        return BadRequest();
                    dbContext.Users.Remove(userInDb);
                }

                await dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Lockout user
        /// </summary>
        /// <returns></returns>
        /// /// <response code="204">User successfully locked out</response>
        /// <response code="400">Couldn't find user with given id</response>
        [HttpPost()]
        [PermissionClaimFilter(Permissions.UserUpdate)]
        [Route("Lock")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> LockoutUsers([Required]LockUsersDTO model)
        {
            try
            {
                foreach (var id in model.UserIds)
                {
                    var userInDb = await dbContext.Users.FindAsync(id);
                    if (userInDb == null)
                        return BadRequest();
                    userInDb.LockoutEnd = new DateTimeOffset(model.LockUntil.HasValue ? model.LockUntil.Value : DateTime.Now.AddYears(100));
                }

                dbContext.AddAuditCustomField("Title", "Locked users");
                await dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Lockout user
        /// </summary>
        /// <param name="ids">User Ids</param>
        /// <returns></returns>
        /// /// <response code="204">User successfully locked out</response>
        /// <response code="400">Couldn't find user with given id</response>
        [HttpPost()]
        [PermissionClaimFilter(Permissions.UserUpdate)]
        [Route("Unlock")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UnlockUsers([Required][FromBody]string[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var userInDb = await dbContext.Users.FindAsync(id);
                    if (userInDb == null)
                        return BadRequest();
                    userInDb.LockoutEnd = new DateTimeOffset(DateTime.Now);
                }

                await dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets ids of all the contacts that are conected to a user (taken)
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [PermissionClaimFilter(Permissions.UserRead,Permissions.ContactRead)]
        [Route("AssignedContactsIds")]
        [ProducesResponseType(typeof(IEnumerable<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetContactsAssignedToUsersIds()
        {
            return Ok(await dbContext.Users.Where(u => u.ContactId != null)
                            .Select(u => u.ContactId)
                            .ToListAsync());
        }

        /// <summary>
        /// Checks if all given properties are unique and sets ModelStateErrors for those who aren't
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<bool> IsUnique(string username = "", string email = "")
        {
            if (username != "" && (await _userManager.FindByNameAsync(username)) != null)
            {
                ModelState.AddModelError("username", "User with given username already exists");
            }

            if (!string.IsNullOrWhiteSpace(email) && (await _userManager.FindByEmailAsync(email)) != null)
            {
                ModelState.AddModelError("email", "User with given email already exists");
            }

            if (ModelState.IsValid)
                return true;
            else
                return false;
        }
    }
}
