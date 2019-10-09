using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.DTOs.Contact
{
    public class ContactListDTO
    {
        [Required]
        public IEnumerable<int> ContactIds { get; set; }

        [Required]
        public IEnumerable<int> ListIds { get; set; }
    }
}
