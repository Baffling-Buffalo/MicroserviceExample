using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibraries
{
    public static class API
    {
        public static class User
        {
            public static string GetUsers(string baseUri, bool? lockedUsers = null, int[] groupIds = null, string[] roleIds = null, string searchQuery = null) {
                var url = $"{baseUri}/api/user?";

                if (lockedUsers != null)
                    url += $"lockedUsers={lockedUsers}";

                if (groupIds != null && groupIds.Count() > 0)
                {
                    foreach (var groupId in groupIds)
                    {
                        url += $"&groupIds={groupId}";
                    }
                }

                if (roleIds != null && roleIds.Count() > 0)
                {
                    foreach (var roleId in roleIds)
                    {
                        url += $"&roleIds={roleId}";
                    }
                }

                if (searchQuery != null && !string.IsNullOrWhiteSpace(searchQuery))
                    url += $"&searchQuery={searchQuery}";

                return url;
            }
            public static string GetUsersForDatatable(string baseUri) => $"{baseUri}/api/user/Datatable?";
            public static string GetUser(string baseUri, string userId) => $"{baseUri}/api/user/{userId}";
            public static string GetUsersWithIds(string baseUri) => $"{baseUri}/api/user/WithIds";
            public static string CreateUser(string baseUri) => $"{baseUri}/api/user";
            public static string UpdateUser(string baseUri) => $"{baseUri}/api/user";
            public static string DeleteUser(string baseUri) => $"{baseUri}/api/user";
            public static string DeactivateUsers(string baseUri, DateTime? lockUntil = null) => $"{baseUri}/api/user/Lock";
            public static string ActivateUsers(string baseUri) => $"{baseUri}/api/user/Unlock";
            public static string GetUsersForDT(string baseUri) => $"{baseUri}/api/user/DataTable";
            public static string GetAssignedContactsIds(string baseUri) => $"{baseUri}/api/user/AssignedContactsIds";
        }

        public static class Role
        {
            public static string GetRole(string baseUri, string id) => $"{baseUri}/api/role/{id}";
            public static string GetAllRoles(string baseUri) => $"{baseUri}/api/role";
            public static string GetRolesForDatatable(string baseUri) => $"{baseUri}/api/role/Datatable";
            public static string GetRolesWithIds(string baseUri) => $"{baseUri}/api/role/WithIds";
            public static string CreateRole(string baseUri) => $"{baseUri}/api/role";
            public static string UpdateRole(string baseUri) => $"{baseUri}/api/role";
            public static string DeleteRole(string baseUri) => $"{baseUri}/api/role";
            public static string GetRolesForDT(string baseUri) => $"{baseUri}/api/role/DataTable";
            public static string GetUsersInRole(string baseUri, string roleId) => $"{baseUri}/api/role/UsersInRole/{roleId}";
            public static string GetRolesOfUser(string baseUri, string userId) => $"{baseUri}/api/role/RolesOfUser/{userId}";
            public static string AddUsersToRoles(string baseUri) => $"{baseUri}/api/role/AddUsersToRoles";
            public static string RemoveUsersFromRoles(string baseUri) => $"{baseUri}/api/role/RemoveUsersFromRoles";
        }

        public static class RoleAction
        {
            public static string GetRoleActions(string baseUri, string roleId = null) => $"{baseUri}/api/roleaction{(roleId == null ? "" : $"?roleId={roleId}")}";
        }

        public static class UserGroup
        {
            public static string GetAllGroups(string baseUri) => $"{baseUri}/api/group";
            public static string GetGroupsForDatatable(string baseUri, int parentGroupId = 0) => $"{baseUri}/api/group/datatable/?parentGroupId={parentGroupId}";
            public static string GetGroup(string baseUri, int id) => $"{baseUri}/api/group/{id}";
            public static string CreateGroup(string baseUri) => $"{baseUri}/api/group";
            public static string UpdateGroup(string baseUri) => $"{baseUri}/api/group";
            public static string DeleteGroups(string baseUri, bool deleteSubGroups = false) => $"{baseUri}/api/group?deleteSubGroups={deleteSubGroups}";
            public static string GetGroupsForComboTree(string baseUri, int[] ids = null) => $"{baseUri}/api/group/ComboTreePlugin?ids={(ids!=null ? string.Join(',',ids) : "")}";
            public static string GetGroupsForTreeTable(string baseUri, int[] ids = null) => $"{baseUri}/api/group/TreeTable?ids={(ids != null ? string.Join(',', ids) : "")}";
            public static string GetUsersInGroup(string baseUri, string groupId) => $"{baseUri}/api/group/UsersInGroup/{groupId}";
            public static string GetGroupsOfUser(string baseUri, string userId) => $"{baseUri}/api/group/GroupsOfUser/{userId}";
            public static string AddUsersToGroups(string baseUri) => $"{baseUri}/api/group/AddUsersToGroups";
            public static string RemoveUsersFromGroups(string baseUri) => $"{baseUri}/api/group/RemoveUsersFromGroups";
        }

        public static class Contact
        {
            public static string GetContact(string baseUri, int contactId) => $"{baseUri}/api/c/contact/{contactId}";
            public static string GetContacts(string baseUri, List<int> contactIds = null) => $"{baseUri}/api/c/contact?contactIds={(contactIds != null ? string.Join(',', contactIds) : "")}";
            public static string GetContactsForDataTable(string baseUri) => $"{baseUri}/api/c/contact/datatable";
            public static string CreateContact(string baseUri) => $"{baseUri}/api/c/contact";
            public static string UpdateContact(string baseUri) => $"{baseUri}/api/c/contact";
            public static string DeleteContacts(string baseUri) => $"{baseUri}/api/c/contact";
            public static string ActivateContacts(string baseUri) => $"{baseUri}/api/c/contact/activate";
            public static string DeactivateContacts(string baseUri) => $"{baseUri}/api/c/contact/deactivate";
            public static string AddContactsToLists(string baseUri) => $"{baseUri}/api/c/contact/addcontactstolists";
            public static string RemoveContactsFromLists(string baseUri) => $"{baseUri}/api/c/contact/removecontactsfromlists";
            public static string Exists(string baseUri, int id) => $"{baseUri}/api/c/contact/exists/{id}";
        }

        public static class List
        {
            public static string GetList(string baseUri, int listId) => $"{baseUri}/api/c/list/{listId}";
            public static string GetLists(string baseUri, List<int> listIds = null) => $"{baseUri}/api/c/list?listIds={(listIds != null ? string.Join(',', listIds) : "")}";
            public static string GetListsOfContacts(string baseUri, List<int> contactIds = null) => $"{baseUri}/api/c/list/getlistsofcontacts?contactIds={(contactIds != null ? string.Join(',', contactIds) : "")}";
            public static string GetListsForDataTable(string baseUri, int? parentId) => $"{baseUri}/api/c/list/datatable?parentId={parentId}";
            public static string GetListsForComboTree(string baseUri, int[] listIds = null) => $"{baseUri}/api/c/list/combotreeplugin?listIds={(listIds != null ? string.Join(',', listIds) : "")}";
            public static string GetListsForListTree(string baseUri, int[] listIds = null) => $"{baseUri}/api/c/list/listtree?listIds={(listIds != null ? string.Join(',', listIds) : "")}";
            public static string CreateList(string baseUri) => $"{baseUri}/api/c/list";
            public static string UpdateList(string baseUri) => $"{baseUri}/api/c/list";
            public static string DeleteLists(string baseUri, bool deleteSubLists = false) => $"{baseUri}/api/c/list?deleteSubLists={deleteSubLists}";
            public static string AddContacts(string baseUri) => $"{baseUri}/api/list/c/addcontacts";
            public static string RemoveContacts(string baseUri) => $"{baseUri}/api/list/c/removecontacts";
        }

        public static class Form
        {
            public static string GetFormsForDataTable(string baseUri) => $"{baseUri}/api/f/form/datatable";
            public static string GetFrame(string baseUri) => $"{baseUri}/oz7/server";
            public static string Exists(string baseUri, int id) => $"{baseUri}/api/f/form/exists/{id}";
        }

        public static class FormGroup
        {
            public static string GetGroup(string baseUri, int groupId) => $"{baseUri}/api/f/group/{groupId}";
            public static string GetGroupsForComboTree(string baseUri, List<int> groupIds = null) => $"{baseUri}/api/f/group/combotreeplugin?groupIds={(groupIds != null ? string.Join(',', groupIds) : "")}";
            public static string GetGroupsForTreeTable(string baseUri) => $"{baseUri}/api/f/group/treetable";
            public static string CreateGroup(string baseUri) => $"{baseUri}/api/f/group";
            public static string UpdateGroup(string baseUri) => $"{baseUri}/api/f/group";
            public static string DeleteGroups(string baseUri) => $"{baseUri}/api/f/group";
        }

        public static class SessionDocument
        {
            public static string CreateSessionDocument(string baseUri) => $"{baseUri}/api/s/sessiondocument";
        }

        public static class SessionContact
        {
            public static string CreateSessionContact(string baseUri) => $"{baseUri}/api/s/sessioncontact";
        }

        public static class Log
        {
            public static string GetAuditLogsForDataTable(string baseUri) => $"{baseUri}/api/l/audit/datatable";
            public static string DeleteAuditGroups(string baseUri) => $"{baseUri}/api/l/audit";
        }
    }
}
