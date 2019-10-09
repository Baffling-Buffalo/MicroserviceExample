using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class UsersGroupsIdsDTO
    {
        [Required]
        public string[] UserIds { get; set; }
        [Required]
        public int[] GroupIds { get; set; }
    }
}
