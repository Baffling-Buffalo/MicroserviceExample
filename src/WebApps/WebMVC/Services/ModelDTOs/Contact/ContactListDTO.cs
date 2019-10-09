using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services.ModelDTOs
{
    public class ContactListDTO
    {
        public IEnumerable<int> ContactIds { get; set; }

        public IEnumerable<int> ListIds { get; set; }
    }
}
