using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class RoleDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] RoleActions { get; set; }
        /// <summary>
        /// Role is predefined and can not be deleted
        /// </summary>
        public bool SystemRole { get; set; }
    }
}
