using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form.API.DTO
{
    public class CreateGroupDTO
    {
        public string GroupName { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

    }
}
