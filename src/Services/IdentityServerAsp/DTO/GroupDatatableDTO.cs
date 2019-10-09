using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class GroupDatatableDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string ParentGroupName { get; set; }
        public int ChildGroupCount { get; set; }
    }
}
