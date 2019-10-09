using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.DTOs.List
{
    public class ListParserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Parent { get; set; }

        public int NumberOfChildren { get; set; }

        public int NumberOfContacts { get; set; }
    }
}
