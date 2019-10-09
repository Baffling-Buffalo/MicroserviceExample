using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services.ModelDTOs
{
    public class GetListDTO
    {
        public int Id { get; set; }

        public string ListName { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public Dictionary<int, string> ChildLists { get; set; }

        public Dictionary<int, string> Contacts { get; set; }

        //public int? NumberOfContacts { get; set; }

        //public int? NumberOfChildren { get; set; }

        //public List<string> Children { get; set; }
    }
}
