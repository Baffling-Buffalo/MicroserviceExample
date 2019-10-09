using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SharedLibraries
{
    public class Permission
    {
        public Permission(string name, string subject, string context)
        {
            Name = name;
            Subject = subject;
            Context = context;
        }

        public string Name { get; set; }
        public string Subject { get; set; }
        public string Context { get; set; }
    }

    /// <summary>
    /// From the perspective of object of the action claims
    /// </summary>
    public class PermissionSubject
    {
        public string Context { get; set; }
        public string Name { get; set; }
        public List<string> PermissionNames { get; set; }
    }


    public class Permissions
    {
        public const string UserCreate = "user_create";
        public const string UserRead = "user_read";
        public const string UserUpdate = "user_update";
        public const string UserDelete = "user_delete";
        public const string UserGroupCreate = "userGroup_create";
        public const string UserGroupRead = "userGroup_read";
        public const string UserGroupUpdate = "userGroup_update";
        public const string UserGroupDelete = "userGroup_delete";
        public const string AddUserToGroup = "user_userGroup_add";
        public const string RemoveUserFromGroup = "user_userGroup_remove";
        public const string RoleCreate = "role_create";
        public const string RoleRead = "role_read";
        public const string RoleUpdate = "role_update";
        public const string RoleDelete = "role_delete";
        public const string SystemRoleDelete = "system_role_delete";
        public const string SystemRoleUpdate = "system_role_update";
        public const string AddUserToRole = "user_role_add";
        public const string RemoveUserFromRole = "user_role_remove";
        public const string ContactCreate = "contact_create";
        public const string ContactRead = "contact_read";
        public const string ContactUpdate = "contact_update";
        public const string ContactDelete = "contact_delete";
        public const string ListCreate = "list_create";
        public const string ListRead = "list_read";
        public const string ListUpdate = "list_update";
        public const string ListDelete = "list_delete";
        public const string AddContactToList = "contact_list_add";
        public const string RemoveContactFromList = "contact_list_remove";
        public const string FormRead = "form_read";
        public const string FormGroupRead = "formGroup_read";
        public const string FormGroupCreate = "formGroup_create";
        public const string FormGroupUpdate = "formGroup_update";
        public const string FormGroupDelete = "formGroup_delete";
        public const string AuditRead = "audit_read";
        public const string AuditDelete = "audit_delete";

        public static List<Permission> GetPermissions()
        {
            return new List<Permission>()
            {
                new Permission(UserCreate, "Users", "User Management"),
                new Permission(UserRead, "Users", "User Management"),
                new Permission(UserUpdate, "Users", "User Management"),
                new Permission(UserDelete, "Users", "User Management"),

                new Permission(UserGroupCreate, "User Groups", "User Management"),
                new Permission(UserGroupRead, "User Groups", "User Management"),
                new Permission(UserGroupUpdate, "User Groups", "User Management"),
                new Permission(UserGroupDelete, "User Groups", "User Management"),

                new Permission(AddUserToGroup, "User Groups", "User Management"),
                new Permission(RemoveUserFromGroup, "User Groups", "User Management"),

                new Permission(RoleCreate, "Roles", "User Management"),
                new Permission(RoleRead, "Roles", "User Management"),
                new Permission(RoleUpdate, "Roles", "User Management"),
                new Permission(RoleDelete, "Roles", "User Management"),

                new Permission(AddUserToRole, "Roles", "User Management"),
                new Permission(RemoveUserFromRole, "Roles", "User Management"),

                new Permission(ContactCreate, "Contacts", "Contact Management"),
                new Permission(ContactRead, "Contacts", "Contact Management"),
                new Permission(ContactUpdate, "Contacts", "Contact Management"),
                new Permission(ContactDelete, "Contacts", "Contact Management"),

                new Permission(ListCreate, "Contact Lists", "Contact Management"),
                new Permission(ListRead, "Contact Lists", "Contact Management"),
                new Permission(ListUpdate, "Contact Lists", "Contact Management"),
                new Permission(ListDelete, "Contact Lists", "Contact Management"),

                new Permission(AddContactToList, "Contact Lists", "Contact Management"),
                new Permission(RemoveContactFromList, "Contact Lists", "Contact Management"),

                new Permission(FormRead, "Forms", "Form Management"),

                new Permission(FormGroupRead, "Form Groups", "Form Management"),
                new Permission(FormGroupCreate, "Form Groups", "Form Management"),
                new Permission(FormGroupUpdate, "Form Groups", "Form Management"),
                new Permission(FormGroupDelete, "Form Groups", "Form Management"),

                new Permission(AuditRead, "Audit Logs", "Logging"),
                new Permission(AuditDelete, "Audit Logs", "Logging"),

            };
        }

        public static List<PermissionSubject> GetPermissionSubjects()
        {
            var actions = GetPermissions();
            var res = actions.GroupBy(a => a.Subject, a => a.Name, (s, n) => new PermissionSubject() { Name = s, PermissionNames = n.ToList() }).ToList();
            foreach (var r in res)
            {
                r.Context = actions.FirstOrDefault(a => a.Subject == r.Name).Context;
            }
            return res.ToList();
        }
    }
}
