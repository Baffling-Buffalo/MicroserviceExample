using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services.ModelDTOs.Form
{
    public class GetGroupDTO
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public Dictionary<int, string> ChildGroups { get; set; }

        public Dictionary<int, string> Forms { get; set; }
    }
}
