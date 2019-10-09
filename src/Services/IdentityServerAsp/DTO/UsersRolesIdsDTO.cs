using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.DTO
{
    public class UsersRolesIdsDTO
    {
        [Required]
        public List<string> UserIds { get; set; }
        [Required]
        public List<string> RoleIds { get; set; }
    }
}
