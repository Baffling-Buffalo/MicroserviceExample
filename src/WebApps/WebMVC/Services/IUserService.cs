using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels.Role;

namespace WebMVC.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Adds ModelErrors to passed ModelState if any were found and stored by last service method
        /// </summary>
        /// <param name="modelState"></param>
        void Validate(ModelStateDictionary modelState);

        Task<List<ApplicationUserDTO>> GetUsersAsync(bool? lockedUsers = null, int[] groupIds = null, string[] roleIds = null, string searchQuery = null);
        Task<ApplicationUser> GetUserAsync(string id);
        /// <summary>
        /// Needs to be called from datatable ajax serverside request, Gets JSON formatted object with users
        /// </summary>
        /// <param name="datatableParams">DT parameters sent by dt in as form params, can be accessed by Request.Form in controller</param>
        /// <param name="lockedUsers">null-get all users, false-active users, true-deactivated users</param>
        /// <param name="groupIds"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<string> GetUsersForDatatableAsync(IFormCollection datatableParams, bool? lockedUsers = null, int[] groupIds = null, string[] roleIds = null);
        Task<List<ApplicationUser>> GetUsersWithIdsAsync(string[] ids);
        Task<string> CreateUserAsync(ApplicationUser newUser);
        Task<string> CreateUserAsync(NewApplicationUserDTO newUser);
        Task UpdateUserAsync(UpdateApplicationUserDTO updateUser);
        Task DeleteUsersAsync(string[] ids);
        Task LockUsersAsync(string[] ids, DateTime? lockUntil = null);
        Task UnlockUsersAsync(string[] ids);
        Task<int[]> GetAssignedContactsIds();

        Task<List<UserGroup>> GetAllGroupsAsync();
        Task<UserGroup> GetGroup(int id);
        Task<string> GetGroupsForComboTreeAsync(int[] withIds = null);
        Task<string> GetGroupsForTreeTableAsync(int[] withIds = null);
        Task<List<ApplicationUserDTO>> GetUsersInGroupAsync(int groupId);
        Task<List<UserGroup>> GetGroupsOfUserAsync(string userId);
        Task<string> GetGroupsForDatatableAsync(IFormCollection datatableParams, int parengGroupId);
        Task<int> CreateGroupAsync(UserGroup newGroup);
        Task UpdateGroupAsync(UserGroup updateGroup);
        Task DeleteGroupsAsync(int[] ids, bool deleteSubGroups = false);
        Task AddUsersToGroupsAsync(string[] userIds, int[] groupIds);
        Task RemoveUsersFromGroupsAsync(string[] userIds, int[] groupIds);

        Task<List<Role>> GetAllRolesAsync();
        Task<Role> GetRole(string id);
        Task<List<Role>> GetRolesWithIdsAsync(string[] ids = null);
        Task<string> GetRolesForDatatableAsync(IFormCollection datatableParams);
        Task<string> CreateRoleAsync(Role role);
        Task DeleteRolesAsync(string[] roleIds);
        Task<List<Role>> GetRolesOfUserAsync(string userId);
        Task<List<ApplicationUserDTO>> GetUsersInRoleAsync(string roleId);
        Task UpdateRoleAsync(Role model);
        Task AddUsersToRolesAsync(string[] userIds, string[] roleIds);
        Task RemoveUsersFromRolesAsync(string[] userIds, string[] roleIds);
    }
}
