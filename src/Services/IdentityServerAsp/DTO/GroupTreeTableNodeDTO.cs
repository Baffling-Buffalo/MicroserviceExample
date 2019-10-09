using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class GroupTreeTableNodeDTO
    {
        public List<GroupTreeTableNodeDTO> Children = new List<GroupTreeTableNodeDTO>();

        public bool ShouldSerializeChildren()
        {
            return (Children.Count > 0);
        }

        public GroupTreeTableNodeDTO Parent { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? NumberOfUsers { get; set; }

        public int? ParentId { get; set; }
    }
}
