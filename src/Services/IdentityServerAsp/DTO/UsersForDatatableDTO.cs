using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class UsersForDatatableDTO
    {
        /// <summary>
        /// Paremeters send by datatable lib as form params when triggering ajax call
        /// </summary>
        public IEnumerable<KeyValuePair<string,string>> DtParameters { get; set; }
        /// <summary>
        /// True - get only locked users, False - get only active users and (default)Null - get all
        /// </summary>
        public bool? Locked { get; set; }
        /// <summary>
        /// Ids of groups from which you want to get users
        /// </summary>
        public int[] GroupIds { get; set; }
        /// <summary>
        /// Ids of roles from which you want to get users
        /// </summary>
        public string[] RoleIds { get; set; }
    }
}
