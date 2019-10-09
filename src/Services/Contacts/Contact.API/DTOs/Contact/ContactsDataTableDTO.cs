using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.DTOs.Contact
{
    public class ContactsDataTableDTO
    {
        public IEnumerable<KeyValuePair<string, string>> DataTableParameters { get; set; }

        public bool? Active { get; set; }

        public IEnumerable<int> ListIds { get; set; }

        public IEnumerable<int> ExcludeIds { get; set; }
    }
}
