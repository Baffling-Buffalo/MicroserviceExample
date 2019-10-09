using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class NewGroupDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentGroupId { get; set; }
    }
}
