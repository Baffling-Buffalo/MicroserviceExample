using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Form.API.DTO
{
    public class UpdateGroupDTO
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

    }
}
