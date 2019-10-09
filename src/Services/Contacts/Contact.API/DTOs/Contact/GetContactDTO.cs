using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.DTOs.Contact
{
    public class GetContactDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool Active { get; set; }

        public Dictionary<int, string> Lists { get; set; }

        //for datatable
        public string Status { get; set; }
        public int NumberOfLists { get; set; }
    }
}
