using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SharedLibraries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Models;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels.Role;

namespace WebMVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AppSettings> _settings;

        private readonly string _remoteServiceBaseUrl;
    
        private Dictionary<string, string[]> modelStateErrors;

        public UserService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;

            _remoteServiceBaseUrl = $"{_settings.Value.IdentityUrl}";
        }

        #region Users
        public async Task<string> CreateUserAsync(ApplicationUser newUser)
        {
            var uri = API.User.CreateUser(_remoteServiceBaseUrl);

            newUser.AdministrationUser = true;

            var newUserContent = new StringContent(JsonConvert.SerializeObject(newUser), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, newUserContent);

            if (response.TrySaveModelStateErrors(ref modelStateErrors))
            {
                return "";
            }

            return await response.Content.ReadAsStringAsync(); // created users Id
        }

        public async Task<string> CreateUserAsync(NewApplicationUserDTO newUser)
        {
            var uri = API.User.CreateUser(_remoteServiceBaseUrl);

            newUser.AdministrationUser = true;

            var newUserContent = new StringContent(JsonConvert.SerializeObject(newUser), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, newUserContent);

            if (response.TrySaveModelStateErrors(ref modelStateErrors))
            {
                return "";
            }

            return await response.Content.ReadAsStringAsync(); // created users Id
        }

        public async Task DeleteUsersAsync(string[] ids)
        {
            var uri = API.User.DeleteUser(_remoteServiceBaseUrl);
            
            var idsContent = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = idsContent
            };

            var response = await _httpClient.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            var uri = API.User.GetUser(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var user = JsonConvert.DeserializeObject<ApplicationUser>(responseString);

            return user;
        }

        public async Task<List<ApplicationUserDTO>> GetUsersAsync(bool? lockedUsers = null, int[] groupIds = null, string[] roleIds = null, string searchQuery = null)
        {
            var uri = API.User.GetUsers(_remoteServiceBaseUrl, lockedUsers, groupIds, roleIds, searchQuery);

            var responseString = await _httpClient.GetStringAsync(uri);

            var users = JsonConvert.DeserializeObject<List<ApplicationUserDTO>>(responseString);

            return users;
        }

        public async Task<string> GetUsersForDatatableAsync(IFormCollection datatableParams, bool? lockedUsers = null, int[] groupIds = null, string[] roleIds = null)
        {
            var uri = API.User.GetUsersForDatatable(_remoteServiceBaseUrl);

            var keyValues = datatableParams.Select(dt => new KeyValuePair<string, string>(dt.Key, dt.Value.ToString())).ToList();

            var dto = new
            {
                DtParameters = keyValues,
                Locked = lockedUsers,
                GroupIds = groupIds,
                RoleIds = roleIds
            };

            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<ApplicationUser>> GetUsersWithIdsAsync(string[] ids)
        {
            var uri = API.User.GetUsersWithIds(_remoteServiceBaseUrl);
          
            var content = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(await response.Content.ReadAsStringAsync());

            return users;
        }

        public async Task LockUsersAsync(string[] ids, DateTime? lockUntil = null)
        {
            var uri = API.User.DeactivateUsers(_remoteServiceBaseUrl);

            var data = new
            {
                UserIds = ids,
                LockUntil = lockUntil?.ToString("MM.dd.yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();
        }

        public async Task UnlockUsersAsync(string[] ids)
        {
            var uri = API.User.ActivateUsers(_remoteServiceBaseUrl);
            
            var idsContent = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, idsContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUserAsync(UpdateApplicationUserDTO updateUser)
        {
            var uri = API.User.UpdateUser(_remoteServiceBaseUrl);

            var updateUserContent = new StringContent(JsonConvert.SerializeObject(updateUser), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, updateUserContent);

            response.TrySaveModelStateErrors(ref modelStateErrors);
        }

        public async Task<int[]> GetAssignedContactsIds()
        {
            var uri = API.User.GetAssignedContactsIds(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var res = JsonConvert.DeserializeObject<int[]>(responseString);

            return res;
        }
        #endregion Users

        #region UserGroups
        public async Task<List<UserGroup>> GetAllGroupsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateGroupAsync(UserGroup newGroup)
        {
            var uri = API.UserGroup.CreateGroup(_remoteServiceBaseUrl);

            var newGroupContent = new StringContent(JsonConvert.SerializeObject(newGroup), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, newGroupContent);

            if (response.TrySaveModelStateErrors(ref modelStateErrors))
                return -1;

            return int.Parse(await response.Content.ReadAsStringAsync()); // created group Id
        }

        public async Task UpdateGroupAsync(UserGroup updateGroup)
        {
            string uri = API.UserGroup.UpdateGroup(_remoteServiceBaseUrl);

            var updateGroupContent = new StringContent(JsonConvert.SerializeObject(updateGroup), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, updateGroupContent);

            response.TrySaveModelStateErrors(ref modelStateErrors);
        }

        public async Task DeleteGroupsAsync(int[] ids, bool deleteSubGroups = false)
        {
            string uri = API.UserGroup.DeleteGroups(_remoteServiceBaseUrl, deleteSubGroups);

            var idsContent = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = idsContent
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<UserGroup> GetGroup(int id)
        {
            string uri = API.UserGroup.GetGroup(_remoteServiceBaseUrl, id);

            string responseString = await _httpClient.GetStringAsync(uri);

            UserGroup group = JsonConvert.DeserializeObject<UserGroup>(responseString);

            return group;
        }

        public async Task<string> GetGroupsForComboTreeAsync(int[] withIds = null)
        {
            var uri = API.UserGroup.GetGroupsForComboTree(_remoteServiceBaseUrl, withIds);

            var responseString = await _httpClient.GetStringAsync(uri);

            return responseString;
        }

        public async Task<string> GetGroupsForTreeTableAsync(int[] withIds = null)
        {
            var uri = API.UserGroup.GetGroupsForTreeTable(_remoteServiceBaseUrl, withIds);

            var responseString = await _httpClient.GetStringAsync(uri);

            return responseString;
        }

        public async Task<string> GetGroupsForDatatableAsync(IFormCollection datatableParams, int parentGroupId)
        {
            var uri = API.UserGroup.GetGroupsForDatatable(_remoteServiceBaseUrl, parentGroupId);
            
            var keyValues = datatableParams.Select(dt => new KeyValuePair<string, string>(dt.Key, dt.Value.ToString()));

            var content = new StringContent(JsonConvert.SerializeObject(keyValues), System.Text.Encoding.UTF8, "application/json");
            
            var responseString = await _httpClient.PostAsync(uri, content);

            var groups = await responseString.Content.ReadAsStringAsync();

            return groups;
        }

        public async Task<List<UserGroup>> GetGroupsOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ApplicationUserDTO>> GetUsersInGroupAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public async Task AddUsersToGroupsAsync(string[] userIds, int[] groupIds)
        {
            var uri = API.UserGroup.AddUsersToGroups(_remoteServiceBaseUrl);

            var dto = new
            {
                UserIds = userIds,
                GroupIds = groupIds
            };

            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveUsersFromGroupsAsync(string[] userIds, int[] groupIds)
        {
            var uri = API.UserGroup.RemoveUsersFromGroups(_remoteServiceBaseUrl);

            var dto = new 
            {
                UserIds = userIds,
                GroupIds = groupIds
            };

            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();
        }
        #endregion UserGroups

        #region Roles
        public async Task<Role> GetRole(string id)
        {
            var uri = API.Role.GetRole(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var role = JsonConvert.DeserializeObject<Role>(responseString);

            return role;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            var uri = API.Role.GetAllRoles(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var roles = JsonConvert.DeserializeObject<List<Role>>(responseString);

            return roles;
        }

        public async Task<List<Role>> GetRolesWithIdsAsync(string[] ids = null)
        {
            var uri = API.Role.GetRolesWithIds(_remoteServiceBaseUrl);

            if (ids == null)
                ids = new List<string>().ToArray(); // Cause it's not valid to serialize null, better empty array

            var idsContent = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, idsContent);

            response.EnsureSuccessStatusCode();

            var roles = JsonConvert.DeserializeObject<List<Role>>(await response.Content.ReadAsStringAsync());

            return roles;
        }

        public async Task<string> GetRolesForDatatableAsync(IFormCollection datatableParams)
        {
            var uri = API.Role.GetRolesForDatatable(_remoteServiceBaseUrl);

            var keyValues = datatableParams.Select(dt => new KeyValuePair<string, string>(dt.Key, dt.Value.ToString()));

            var content = new StringContent(JsonConvert.SerializeObject(keyValues), System.Text.Encoding.UTF8, "application/json");

            var responseString = await _httpClient.PostAsync(uri, content);

            var roles = await responseString.Content.ReadAsStringAsync();

            return roles;
        }

        public async Task<string> CreateRoleAsync(Role role)
        {
            var uri = API.Role.CreateRole(_remoteServiceBaseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(role), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            if (response.TrySaveModelStateErrors(ref modelStateErrors))
                return "";

            return await response.Content.ReadAsStringAsync(); // created role Id
        }

        public async Task UpdateRoleAsync(Role model)
        {
            var uri = API.Role.UpdateRole(_remoteServiceBaseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, content);

            response.TrySaveModelStateErrors(ref modelStateErrors);
        }

        public async Task DeleteRolesAsync(string[] roleIds)
        {
            var uri = API.Role.DeleteRole(_remoteServiceBaseUrl);

            var idsContent = new StringContent(JsonConvert.SerializeObject(roleIds), System.Text.Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = idsContent
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task AddUsersToRolesAsync(string[] userIds, string[] roleIds)
        {
            var uri = API.Role.AddUsersToRoles(_remoteServiceBaseUrl);

            var dto = new
            {
                UserIds = userIds,
                RoleIds = roleIds
            };

            var usersRolesContent = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, usersRolesContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveUsersFromRolesAsync(string[] userIds, string[] roleIds)
        {
            var uri = API.Role.RemoveUsersFromRoles(_remoteServiceBaseUrl);

            var dto = new
            {
                UserIds = userIds,
                RoleIds = roleIds
            };

            var usersRolesContent = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, usersRolesContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Role>> GetRolesOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ApplicationUserDTO>> GetUsersInRoleAsync(string roleId)
        {
            throw new NotImplementedException();
        }
        #endregion Roles

        public void Validate(ModelStateDictionary ModelState)
        {
            if (modelStateErrors == null)
                return;

            foreach (var model in modelStateErrors)
            {
                foreach (var error in model.Value)
                {
                    ModelState.AddModelError(model.Key.FirstCharToUpper(), error);
                }
            }
            modelStateErrors = null;
        }
    }
}
