using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services.ModelDTOs
{
    public class ListDTO
    {
        public int Id { get; set; }

        public string ListName { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<int> ContactIds { get; set; }
    }
}
