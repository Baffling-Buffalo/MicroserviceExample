using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.DTOs.List
{
    public class CreateListDTO
    {
        public string ListName { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<int> ContactIds { get; set; }
    }
}
