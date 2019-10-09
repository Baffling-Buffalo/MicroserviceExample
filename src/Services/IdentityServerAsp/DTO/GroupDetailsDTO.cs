using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class GroupDetailsDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public GroupDTO ParentGroup { get; set; }
        public List<GroupDTO> ChildGroups { get; set; }
    }
}
